using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using FTAnalyzer.Mapping;
using FTAnalyzer.Utilities;
using GeoAPI.Geometries;
using SharpMap.Data;
using SharpMap.Data.Providers;
using SharpMap.Layers;
using SharpMap.Styles;
using System.IO;
using System.Web;

namespace FTAnalyzer.Forms
{
    public partial class LifeLine : Form
    {
        private FamilyTree ft = FamilyTree.Instance;
        private MapHelper mh = MapHelper.Instance;
        private Color backgroundColour;
        private FeatureDataTable lifelines;
        private VectorLayer linesLayer;
        private LabelLayer labelLayer;
        private TearDropLayer points;
        private TearDropLayer selections;
        bool isLoading;
        bool isQuerying;
        private IProgress<string> outputText;

        public LifeLine(IProgress<string> outputText)
        {
            InitializeComponent();
            Top = Top + WindowHelper.TopTaskbarOffset;
            isLoading = true;
            isQuerying = false;
            this.outputText = outputText;
            mnuMapStyle.Setup(linkLabel1, mapBox1, tbOpacity);
            mapZoomToolStrip.Items.Add(mnuMapStyle);
            foreach (ToolStripItem item in mapZoomToolStrip.Items)
                item.Enabled = true;
            //mapZoomToolStrip.Renderer = new CustomToolStripRenderer();
            mapZoomToolStrip.Items[2].ToolTipText = "Zoom out of Map"; // fix bug in SharpMapUI component
            mapZoomToolStrip.Items[4].ToolTipText = "Draw rectangle by dragging mouse to specify zoom area";
            for (int i = 7; i <= 10; i++)
                mapZoomToolStrip.Items[i].Visible = false;
            backgroundColour = mapZoomToolStrip.Items[0].BackColor;
            mnuHideScaleBar.Checked = Properties.MappingSettings.Default.HideScaleBar;
            SetupMap();
            dgFacts.AutoGenerateColumns = false;
            dgIndividuals.AutoGenerateColumns = false;
            dgIndividuals.DataSource = new SortableBindingList<Individual>(ft.AllIndividuals);
            dgIndividuals.Sort(dgIndividuals.Columns["BirthDate"], ListSortDirection.Ascending);
            dgIndividuals.Sort(dgIndividuals.Columns["SortedName"], ListSortDirection.Ascending);
            DatabaseHelper.GeoLocationUpdated += new EventHandler(DatabaseHelper_GeoLocationUpdated);
            int splitheight = (int)Application.UserAppDataRegistry.GetValue("Lifeline Facts Splitter Distance", -1);
            if (splitheight != -1)
                splitContainerFacts.SplitterDistance = this.Height - splitheight;
            splitContainerMap.SplitterDistance = (int)Application.UserAppDataRegistry.GetValue("Lifeline Map Splitter Distance", splitContainerMap.SplitterDistance);
        }

        void DatabaseHelper_GeoLocationUpdated(object location, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => DatabaseHelper_GeoLocationUpdated(location, e)));
                return;
            }
            BuildMap();
        }

        void SetupMap()
        {
            lifelines = new FeatureDataTable();
            lifelines.Columns.Add("MapLifeLine", typeof(MapLifeLine));
            lifelines.Columns.Add("LineCap", typeof(string));
            lifelines.Columns.Add("Label", typeof(string));
            lifelines.Columns.Add("ViewPort", typeof(Envelope));

            GeometryFeatureProvider lifelinesGFP = new GeometryFeatureProvider(lifelines);

            VectorStyle linestyle = new VectorStyle
            {
                Line = new Pen(Color.Green, 2f)
            };
            linestyle.Line.MiterLimit = 0;
            linesLayer = new VectorLayer("LifeLines")
            {
                DataSource = lifelinesGFP,
                Style = linestyle
            };

            Dictionary<string, IStyle> styles = new Dictionary<string, IStyle>();
            VectorStyle line = new VectorStyle
            {
                PointColor = new SolidBrush(Color.Green),
                PointSize = 2
            };
            styles.Add(MapLifeLine.LINE, line);

            VectorStyle startPoint = new VectorStyle
            {
                PointColor = new SolidBrush(Color.Green),
                PointSize = 2,
                Symbol = Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\arrow-right.png"))
            };
            styles.Add(MapLifeLine.START, startPoint);

            VectorStyle endPoint = new VectorStyle
            {
                PointColor = new SolidBrush(Color.Green),
                PointSize = 2,
                Symbol = Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\arrow-left.png"))
            };
            styles.Add(MapLifeLine.END, endPoint);

            linesLayer.Theme = new SharpMap.Rendering.Thematics.UniqueValuesTheme<string>("LineCap", styles, line);

            mapBox1.Map.Layers.Add(linesLayer);

            labelLayer = new LabelLayer("Label")
            {
                DataSource = lifelinesGFP,
                Enabled = true,
                //Specifiy field that contains the label string.
                LabelColumn = "Label",
                TextRenderingHint = TextRenderingHint.AntiAlias,
                SmoothingMode = SmoothingMode.AntiAlias
            };
            LabelStyle style = new LabelStyle
            {
                ForeColor = Color.Black,
                Font = new Font(FontFamily.GenericSerif, 14, FontStyle.Bold),
                HorizontalAlignment = LabelStyle.HorizontalAlignmentEnum.Center,
                VerticalAlignment = LabelStyle.VerticalAlignmentEnum.Bottom,
                CollisionDetection = true,
                Offset = new PointF(0, 25),
                Halo = new Pen(Color.Yellow, 3)
            };
            labelLayer.Style = style;
            mapBox1.Map.Layers.Add(labelLayer);

            points = new TearDropLayer("Points");
            mapBox1.Map.Layers.Add(points);
            selections = new TearDropLayer("Selections");
            mapBox1.Map.VariableLayers.Add(selections);

            mh.AddParishLayers(mapBox1.Map);
            mh.SetScaleBar(mapBox1);
            mapBox1.Map.MinimumZoom = 500;
            mapBox1.Map.MaximumZoom = 50000000;
            mapBox1.QueryGrowFactor = 30;
            mapBox1.Map.ZoomToExtents();
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
        }

        void DgIndividuals_SelectionChanged(object sender, EventArgs e)
        {
            if (!isLoading)
                BuildMap();
        }

        void BuildMap()
        {
            this.Cursor = Cursors.WaitCursor;
            lifelines.Clear();
            points.Clear();
            List<IDisplayFact> displayFacts = new List<IDisplayFact>();
            foreach (DataGridViewRow row in dgIndividuals.SelectedRows)
            {
                Individual ind = row.DataBoundItem as Individual;
                if (ind.AllGeocodedFacts.Count > 0)
                {
                    displayFacts.AddRange(ind.AllGeocodedFacts);
                    MapLifeLine line = new MapLifeLine(ind);
                    line.AddFeatureDataRow(lifelines);
                    points.AddFeatureDataRows(ind);
                }
            }
            dgFacts.DataSource = new SortableBindingList<IDisplayFact>(displayFacts);
            txtCount.Text = dgIndividuals.SelectedRows.Count + " Individual(s) selected, " + dgFacts.RowCount + " Geolocated fact(s) displayed";

            Envelope expand = mh.GetExtents(lifelines);
            mapBox1.Map.ZoomToBox(expand);
            if (mapBox1.Map.Zoom < mapBox1.Map.MaximumZoom)
            {
                expand.ExpandBy(mapBox1.Map.PixelSize * 40);
                mapBox1.Map.ZoomToBox(expand);
            }
            RefreshMap();
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
            this.Cursor = Cursors.Default;
        }

        void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SpecialMethods.VisitWebsite(e.Link.LinkData as string);
        }

        void DgFacts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                this.Cursor = Cursors.WaitCursor;
                IDisplayFact fact = (IDisplayFact)dgFacts.CurrentRow.DataBoundItem;
                mh.OpenGeoLocations(fact.Location, outputText);
                this.Cursor = Cursors.Default;
            }
        }

        void AddAllFamilyMembersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Individual ind = dgIndividuals.CurrentRow.DataBoundItem as Individual;
            isLoading = true;
            foreach (Individual i in ft.GetFamily(ind))
                SelectIndividual(i);
            isLoading = false;
            BuildMap();
        }

        void SelectIndividual(Individual i)
        {
            DataGridViewRow row = dgIndividuals.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["IndividualID"].Value.ToString().Equals(i.IndividualID)).FirstOrDefault();
            if (row != null)
                dgIndividuals.Rows[row.Index].Selected = true;
        }

        void SelectIndividuals(Func<Individual, List<Individual>> method)
        {
            this.Cursor = Cursors.WaitCursor;
            isLoading = true;
            Individual ind = dgIndividuals.CurrentRow.DataBoundItem as Individual;
            foreach (Individual i in method(ind))
                SelectIndividual(i);
            isLoading = false;
            BuildMap();
            this.Cursor = Cursors.Default;
        }

        void SelectAllAncestorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectIndividuals(ft.GetAncestors);
        }

        void SelectAllDescendantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectIndividuals(ft.GetDescendants);
        }

        void SelectAllRelationsfamilyAncestorsDescendantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectIndividuals(ft.GetAllRelations);
        }

        void LifeLine_FormClosed(object sender, FormClosedEventArgs e)
        {
            DatabaseHelper.GeoLocationUpdated -= DatabaseHelper_GeoLocationUpdated;
            this.Dispose();
        }

        void DgIndividuals_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                Individual ind = (Individual)dgIndividuals.Rows[e.RowIndex].DataBoundItem;
                if (ind.GeoLocationCount == 0)
                    e.ToolTipText = ind.Name + " has no geolocated facts to show on map";
                else
                    e.ToolTipText = "Click to display " + ind.Name + "'s geolocated facts on the map. Right click to add their relatives";
            }
        }

        void DgFacts_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                e.ToolTipText = "Double click to edit location.";
            }
        }

        void LifeLine_Load(object sender, EventArgs e)
        {
            int Width = (int)Application.UserAppDataRegistry.GetValue("Lifeline size - width", this.Width);
            int Height = (int)Application.UserAppDataRegistry.GetValue("Lifeline size - height", this.Height);
            int Top = (int)Application.UserAppDataRegistry.GetValue("Lifeline position - top", this.Top);
            int Left = (int)Application.UserAppDataRegistry.GetValue("Lifeline position - left", this.Left);
            this.Width = Width;
            this.Height = Height;
            this.Top = Top;
            this.Left = Left;
            isLoading = false; // only turn off building map if completely done initializing
            if (dgIndividuals.RowCount > 0)
            {   // update map using first row as selected row
                BuildMap();
            }
            mh.CheckIfGeocodingNeeded(this, outputText);
            SpecialMethods.SetFonts(this);
        }

        void HideLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hideLabelsToolStripMenuItem.Checked)
                mapBox1.Map.Layers.Remove(labelLayer);
            else
                mapBox1.Map.Layers.Add(labelLayer);
            RefreshMap();
        }

        void MnuHideScaleBar_Click(object sender, EventArgs e)
        {
            mh.MnuHideScaleBar_Click(mnuHideScaleBar, mapBox1);
        }

        void SplitContainerFacts_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SplitContainer splitter = (SplitContainer)sender;
            Application.UserAppDataRegistry.SetValue("Lifeline Facts Splitter Distance", this.Height - splitter.SplitterDistance);
        }

        void SplitContainerMap_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SplitContainer splitter = (SplitContainer)sender;
            Application.UserAppDataRegistry.SetValue("Lifeline Map Splitter Distance", splitter.SplitterDistance);
        }

        void DgFacts_SelectionChanged(object sender, EventArgs e)
        {
            if (!isQuerying)
                UpdateSelection();
        }

        void UpdateSelection()
        {
            selections.AddSelections(dgFacts.SelectedRows);
            RefreshMap();
        }

        void BtnSelect_Click(object sender, EventArgs e)
        {
            btnSelect.Checked = true;
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.QueryPoint;
        }

        void MapBox1_ActiveToolChanged(SharpMap.Forms.MapBox.Tools tool)
        {
            if (mapBox1.ActiveTool != SharpMap.Forms.MapBox.Tools.QueryPoint)
                btnSelect.Checked = false;
        }

        void MapBox1_MapQueried(FeatureDataTable data)
        {
            this.Cursor = Cursors.WaitCursor;
            isQuerying = true;
            dgFacts.ClearSelection();
            foreach (FeatureDataRow row in data)
            {
                if (row["DisplayFact"] != null && row["Colour"].ToString() == TearDropLayer.GREY)
                {
                    DisplayFact dispFact = (DisplayFact)row["DisplayFact"];
                    SelectFact(dispFact);
                }
            }
            isQuerying = false;
            this.Cursor = Cursors.Default;
        }

        void SelectFact(DisplayFact dispFact)
        {
            foreach (DataGridViewRow row in dgFacts.Rows)
            {
                DisplayFact rowFact = (DisplayFact)row.DataBoundItem;
                if(rowFact.Equals(dispFact))
                    dgFacts.Rows[row.Index].Selected = true;
            }
        }

        void MapBox1_MouseMove(Coordinate worldPos, MouseEventArgs imagePos)
        {
            string tooltip = string.Empty;
            Envelope infoPoint = new Envelope(worldPos.CoordinateValue);
            infoPoint.ExpandBy(mapBox1.Map.PixelSize * 30);
            foreach (Layer layer in mapBox1.Map.Layers)
            {
                if (layer is TearDropLayer tdl)
                {
                    FeatureDataSet ds = new FeatureDataSet();
                    if (!tdl.DataSource.IsOpen)
                        tdl.DataSource.Open();
                    tdl.DataSource.ExecuteIntersectionQuery(infoPoint, ds);
                    tdl.DataSource.Close();
                    foreach (FeatureDataRow row in ds.Tables[0].Rows)
                    {
                        MapLocation line = (MapLocation)row["MapLocation"];
                        string colour = (string)row["Colour"];
                        if (colour == TearDropLayer.GREY)
                            tooltip += line.ToString() + "\n";
                    }
                }
            }
            if (!tooltip.Equals(mapTooltip.GetToolTip(mapBox1)))
                mapTooltip.SetToolTip(mapBox1, tooltip);
        }

        void LifeLine_Move(object sender, EventArgs e)
        {
            SavePosition();
        }

        void LifeLine_Resize(object sender, EventArgs e)
        {
            SavePosition();
        }

        void SavePosition()
        {
            if (!isLoading && this.WindowState == FormWindowState.Normal)
            {  //only save window size if not maximised or minimised
                Application.UserAppDataRegistry.SetValue("Lifeline size - width", this.Width);
                Application.UserAppDataRegistry.SetValue("Lifeline size - height", this.Height);
                Application.UserAppDataRegistry.SetValue("Lifeline position - top", this.Top);
                Application.UserAppDataRegistry.SetValue("Lifeline position - left", this.Left);
            }
        }

        void ResetFormToDefaultSizeAndPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isLoading = true;
            this.Height = 682;
            this.Width = 1139;
            this.Top = 50;
            this.Left = 50;
            isLoading = false;
            SavePosition();
        }

        void TbOpacity_Scroll(object sender, EventArgs e)
        {
            RefreshMap();
        }

        void SetOpacity()
        {
            if (mapBox1 != null && mapBox1.Map != null && mapBox1.Map.BackgroundLayer.Count > 1)
            {
                float opacity = tbOpacity.Value / 100.0f;
                TileAsyncLayer layer = (TileAsyncLayer)mapBox1.Map.BackgroundLayer[1];
                layer.SetOpacity(opacity);
            }
        }

        void RefreshMap()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => RefreshMap()));
                return;
            }
            SetOpacity();
            mapBox1.Refresh();
        }
    }
}

