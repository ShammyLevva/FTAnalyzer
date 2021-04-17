using FTAnalyzer.Forms.Controls;
using FTAnalyzer.Mapping;
using FTAnalyzer.Utilities;
using NetTopologySuite.Geometries;
using SharpMap.Data;
using SharpMap.Data.Providers;
using SharpMap.Layers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using System.Windows.Forms;

namespace FTAnalyzer.Forms
{
    public partial class EditLocation : Form
    {
        FeatureDataTable pointTable;
        VectorLayer pointLayer;
        List<GdalRasterLayer> customMapLayers;
        readonly FactLocation location;
        readonly FactLocation originalLocation;
        FeatureDataRow pointFeature;
        bool iconSelected;
        bool pointUpdated;
        bool dataUpdated;
        public bool UserSavedPoint { get; private set; }

        public EditLocation(FactLocation location)
        {
            InitializeComponent();
            Top += NativeMethods.TopTaskbarOffset;
            customMapLayers = new List<GdalRasterLayer>();
            mnuMapStyle.Setup(linkLabel1, mapBox1, tbOpacity);
            mapZoomToolStrip.Items.Add(mnuMapStyle);
            mapZoomToolStrip.Items[2].ToolTipText = "Zoom out of Map"; // fix bug in SharpMapUI component
            mapZoomToolStrip.Items[10].Visible = false;
            this.location = location;
            originalLocation = FactLocation.TEMP;
            btnCustomMap.Visible = (Properties.MappingSettings.Default.CustomMapPath.Length > 0);
            FactLocation.CopyLocationDetails(location, originalLocation);
            Text = $"Editing : {location}";
            iconSelected = false;
            pointUpdated = false;
            dataUpdated = false;
            UserSavedPoint = false;
            SetupMap();
            SetLocation();
        }

        void SetupMap()
        {
            pointTable = new FeatureDataTable();
            pointTable.Columns.Add("Label", typeof(string));

            GeometryFeatureProvider pointGFP = new GeometryFeatureProvider(pointTable);

            pointLayer = new VectorLayer("Point to Edit");
            pointLayer.Style.Symbol = Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\teardrop_blue.png"));
            pointLayer.Style.SymbolOffset = new PointF(0.0f, -17.0f);
            pointLayer.DataSource = pointGFP;

            MapHelper.Instance.AddParishLayers(mapBox1.Map);
            mapBox1.Map.VariableLayers.Add(pointLayer);
            mapBox1.Map.MinimumZoom = 500;
            mapBox1.Map.MaximumZoom = 50000000;
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
        }

        void ResetMap()
        {
            FactLocation.CopyLocationDetails(originalLocation, location);
            SetLocation();
        }

        private FeatureDataRow GetRow(double p1, double p2)
        {
            pointFeature = pointTable.NewRow();
            pointFeature["Label"] = location.ToString();
            pointFeature.Geometry = new NetTopologySuite.Geometries.Point(p1, p2);
            return pointFeature;
        }

        void MapBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Coordinate c1 = mapBox1.Map.ImageToWorld(new PointF(e.X - 21.0f, e.Y - 34.0f));
            Coordinate c2 = mapBox1.Map.ImageToWorld(new PointF(e.X + 21.0f, e.Y + 34.0f));
            Envelope env = new Envelope(c1, c2);
            if (iconSelected && e.Button == MouseButtons.Right)
            {
                // we have finished and are saving icon
                mapBox1.Cursor = Cursors.Default;
                Coordinate c = mapBox1.Map.ImageToWorld(new PointF(e.X, e.Y + 17.0f));
                pointFeature.Geometry = new NetTopologySuite.Geometries.Point(c);
                mapBox1.Refresh();
                iconSelected = false;
                pointUpdated = true;
            }
            else if (env.Contains(pointFeature.Geometry.Coordinate))
            {
                mapBox1.Cursor = new Cursor(Path.Combine(Application.StartupPath, @"Resources\Icons\teardrop_blue.cur"));
                iconSelected = true;
            }
        }

        void EditLocation_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If point updated and we are warning then warn
            if (pointUpdated)
            {
                UserSavedPoint = false;
                DialogResult result = DialogResult.Yes;
                if (Application.UserAppDataRegistry.GetValue("Ask to update database", "True").Equals("True"))
                {
                    result = MessageBox.Show("Do you want to save this new position", "Save changes",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                }
                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    FactLocation.CopyLocationDetails(originalLocation, location);
                }
                else if (result == DialogResult.No)
                {
                    FactLocation.CopyLocationDetails(originalLocation, location);
                    pointUpdated = false;
                }
                else if (result == DialogResult.Yes)
                {
                    UpdateDatabase();
                    UserSavedPoint = true;
                }
            }
        }

        void UpdateDatabase()
        {
            Envelope env = new Envelope(mapBox1.Map.Envelope.TopLeft(), mapBox1.Map.Envelope.BottomRight());
            Coordinate point = MapTransforms.ReverseTransformCoordinate(pointFeature.Geometry.Coordinate);
            location.Latitude = point.Y;
            location.Longitude = point.X;
            location.LatitudeM = pointFeature.Geometry.Coordinate.Y;
            location.LongitudeM = pointFeature.Geometry.Coordinate.X;
            location.ViewPort.NorthEast.Lat = env.Top();
            location.ViewPort.NorthEast.Long = env.Right();
            location.ViewPort.SouthWest.Lat = env.Bottom();
            location.ViewPort.SouthWest.Long = env.Left();
            location.PixelSize = mapBox1.Map.PixelSize;
            location.FoundLocation = string.Empty;
            location.GeocodeStatus = FactLocation.Geocode.GEDCOM_USER;
            location.FoundLevel = -2;
            DatabaseHelper.UpdateGeocode(location);
            pointUpdated = false;
            dataUpdated = true;
        }

        void BtnSave_Click(object sender, EventArgs e)
        {
            UpdateDatabase();
            UserSavedPoint = true;
            TreeViewHandler.Instance.RefreshTreeNodeIcon(location);
            MessageBox.Show($"Data for {location} updated.", "Save new location");
        }

        void BtnSaveExit_Click(object sender, EventArgs e)
        {
            UpdateDatabase();
            UserSavedPoint = true;
            TreeViewHandler.Instance.RefreshTreeNodeIcon(location);
            Close();
        }

        void BtnReload_Click(object sender, EventArgs e)
        {
            ResetMap();
            if (dataUpdated)
                UpdateDatabase();
            dataUpdated = false;
            pointUpdated = false;
            UserSavedPoint = false;
        }

        void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => SpecialMethods.VisitWebsite(e.Link.LinkData as string);

        void MapBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            bool zoomed = false;
            if (e.Button == MouseButtons.Left && mapBox1.Map.Zoom > mapBox1.Map.MinimumZoom)
            {
                zoomed = true;
                mapBox1.Map.Zoom *= 1d / 1.5d;
            }
            else if (e.Button == MouseButtons.Right && mapBox1.Map.Zoom < mapBox1.Map.MaximumZoom)
            {
                zoomed = true;
                mapBox1.Map.Zoom *= 1.5d;
            }
            if (zoomed)
            {
                Coordinate p = mapBox1.Map.ImageToWorld(new PointF(e.X, e.Y));
                mapBox1.Map.Center.X = p.X;
                mapBox1.Map.Center.Y = p.Y;
            }
            //Console.WriteLine("Pixel : " + mapBox1.Map.PixelSize);
        }

        void BtnSearch_Click(object sender, EventArgs e) => GoogleLocationSearch();

        void GoogleLocationSearch()
        {
            if (txtSearch.Text.Length > 0)
            {
                FactLocation loc = FactLocation.LookupLocation(txtSearch.Text);
                if (!loc.IsGeoCoded(false)) // if not geocoded then try database
                    DatabaseHelper.GetLocationDetails(loc);
                if (loc.IsGeoCoded(false))
                {
                    FactLocation.CopyLocationDetails(loc, location);
                    SetLocation();
                    pointUpdated = true;
                }
                else
                {
                    GeoResponse res = GoogleMap.GoogleGeocode(null, txtSearch.Text, 8);
                    if (res.Status == "OK" && !(res.Results[0].Geometry.Location.Lat == 0 && res.Results[0].Geometry.Location.Long == 0))
                    {
                        loc.Latitude = res.Results[0].Geometry.Location.Lat;
                        loc.Longitude = res.Results[0].Geometry.Location.Long;
                        Coordinate mpoint = MapTransforms.TransformCoordinate(new Coordinate(loc.Longitude, loc.Latitude));
                        loc.LongitudeM = mpoint.X;
                        loc.LatitudeM = mpoint.Y;
                        loc.ViewPort = MapTransforms.TransformViewport(res.Results[0].Geometry.ViewPort);
                        loc.GeocodeStatus = res.Results[0].PartialMatch ? FactLocation.Geocode.PARTIAL_MATCH : FactLocation.Geocode.MATCHED;
                        FactLocation.CopyLocationDetails(loc, location);
                        SetLocation();
                        pointUpdated = true;
                    }
                    else
                        MessageBox.Show($"Google didn't find {txtSearch.Text}", "Failed Google Lookup");
                }
            }
        }

        void SetLocation()
        {
            pointTable.Clear();
            pointTable.AddRow(GetRow(location.LongitudeM, location.LatitudeM));

            GeoResponse.CResult.CGeometry.CViewPort vp = location.ViewPort;
            Envelope expand;
            if (vp.NorthEast.Lat == 0 && vp.NorthEast.Long == 0 && vp.SouthWest.Lat == 0 && vp.SouthWest.Long == 0)
                expand = new Envelope(-25000000, 25000000, -17000000, 17000000);
            else
                expand = new Envelope(vp.NorthEast.Long, vp.SouthWest.Long, vp.NorthEast.Lat, vp.SouthWest.Lat);
            Coordinate p = new Coordinate(location.LongitudeM, location.LatitudeM);
            Envelope point = new Envelope(p, p);
            point.ExpandBy(mapBox1.Map.PixelSize * 40);
            if (!expand.Contains(point))
            {
                mapBox1.Map.ZoomToBox(expand);
                expand.ExpandToInclude(point);
                expand.ExpandBy(mapBox1.Map.PixelSize * 40);
            }
            mapBox1.Map.ZoomToBox(expand);
            mapBox1.Refresh();
        }

        void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                GoogleLocationSearch();
        }

        void BtnEdit_Click(object sender, EventArgs e)
        {
            mapBox1.Cursor = new Cursor(Path.Combine(Application.StartupPath, @"Resources\Icons\teardrop_blue.cur"));
            iconSelected = true;
        }

        void BtnCustomMap_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            RemoveCustomMapLayers();
            customMapLayers = btnCustomMap.Checked ? LoadGeoReferencedImages() : new List<GdalRasterLayer>();
            mapBox1.Refresh();
            Cursor = Cursors.Default;
        }

        void RemoveCustomMapLayers()
        {
            if (customMapLayers.Count > 0)
            {
                foreach (GdalRasterLayer layer in customMapLayers)
                    mapBox1.Map.Layers.Remove(layer);
            }
        }

        private List<GdalRasterLayer> LoadGeoReferencedImages()
        {
            List<GdalRasterLayer> layers = new List<GdalRasterLayer>();
            string[] files = Directory.GetFiles(Properties.MappingSettings.Default.CustomMapPath, "*.tif", SearchOption.TopDirectoryOnly);
            foreach (string filename in files)
            {
                GdalRasterLayer layer = new GdalRasterLayer(filename, filename);
                layers.Add(layer);
                mapBox1.Map.Layers.Add(layer);
            }
            return layers;
        }

        void EditLocation_FormClosed(object sender, FormClosedEventArgs e) => Dispose();

        void TbOpacity_Scroll(object sender, EventArgs e) => RefreshMap();

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
            if (InvokeRequired)
            {
                Invoke(new Action(() => RefreshMap()));
                return;
            }
            SetOpacity();
            mapBox1.Refresh();
        }

        void EditLocation_Load(object sender, EventArgs e) => SpecialMethods.SetFonts(this);
    }
}
