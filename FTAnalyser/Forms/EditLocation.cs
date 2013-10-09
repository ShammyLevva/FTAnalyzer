using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTAnalyzer.Mapping;
using System.Web;
using SharpMap.Data;
using BruTile.Web;
using SharpMap.Layers;
using SharpMap.Data.Providers;
using SharpMap.Styles;
using System.IO;
using GeoAPI.Geometries;
using GeoAPI.CoordinateSystems.Transformations;

namespace FTAnalyzer.Forms
{
    public partial class EditLocation : Form
    {
        private FeatureDataTable pointTable;
        private VectorLayer pointLayer;
        private bool dragging;

        public EditLocation(FactLocation location)
        {
            InitializeComponent();
            mapZoomToolStrip.Items[2].ToolTipText = "Zoom out of Map"; // fix bug in SharpMapUI component
            mapZoomToolStrip.Items[10].Visible = false;
            this.Text = "Editing : " + location.ToString();
            dragging = false;
            SetupMap(location);
        }

        private void SetupMap(FactLocation fl)
        {
            // Add Google maps layer to map control.
            HttpUtility.SetDefaultProxy();
            mapBox1.Map.BackgroundLayer.Add(new TileAsyncLayer(
                new GoogleTileSource(GoogleMapType.GoogleMap), "GoogleMap"));

            pointTable = new FeatureDataTable();
            pointTable.Columns.Add("Label", typeof(string));

            FeatureDataRow row = pointTable.NewRow();
            row["Label"] = fl.ToString();
            row.Geometry = new NetTopologySuite.Geometries.Point(fl.Longitude, fl.Latitude);
            pointTable.AddRow(row);

            GeometryFeatureProvider pointGFP = new GeometryFeatureProvider(pointTable);

            pointLayer = new VectorLayer("Point to Edit");
            pointLayer.Style.Symbol = Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\teardrop_blue.png"));
            pointLayer.DataSource = pointGFP;
            pointLayer.CoordinateTransformation = MapTransforms.Transform();
            pointLayer.ReverseCoordinateTransformation = MapTransforms.ReverseTransform();

            IMathTransform transform = pointLayer.CoordinateTransformation.MathTransform;
            GeoResponse.CResult.CGeometry.CViewPort vp = fl.ViewPort;
            Envelope expand;
            if (vp.NorthEast.Lat == 0 && vp.NorthEast.Long == 0 && vp.SouthWest.Lat == 0 && vp.SouthWest.Long == 0)
                expand = new Envelope(-25000000, 25000000, -17000000, 17000000);
            else
            {
                Envelope bbox = new Envelope(vp.NorthEast.Long, vp.SouthWest.Long, vp.NorthEast.Lat, vp.SouthWest.Lat);
                expand = new Envelope(transform.Transform(bbox.TopLeft()), transform.Transform(bbox.BottomRight()));
            }
            mapBox1.Map.Layers.Add(pointLayer);
            mapBox1.Map.MinimumZoom = 1000;
            mapBox1.Map.MaximumZoom = 50000000;
            mapBox1.Map.ZoomToBox(expand);
            mapBox1.Refresh();
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.QueryPoint;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void mapBox1_MouseDown(Coordinate worldPos, MouseEventArgs imagePos)
        {
            Console.WriteLine("MouseDown");
            //select point if reasonably near mouse and flag drag started
            // update mouse pointer to show teardrop
            Cursor.Current = new Cursor(Path.Combine(Application.StartupPath, @"Resources\Icons\teardrop_blue.cur"));
            dragging = true;

        }

        private void mapBox1_MouseMove(Coordinate worldPos, MouseEventArgs imagePos)
        {
            // only do anything if dragging active
            // https://sharpmap.codeplex.com/discussions/347771 suggests 
            // "repen edited geometry to point of mousemove" not sure what that means
            if (dragging)
            {
                Console.WriteLine("MouseMove - Dragging");
            }
        }

        private void mapBox1_MouseUp(Coordinate worldPos, MouseEventArgs imagePos)
        {
            Console.WriteLine("MouseUp");
            // if dragging then update geometry to new mouse position
            // revert mouse point to default
            if (dragging)
            {
                Console.WriteLine("Arrived :" + worldPos.X + ", " + worldPos.Y);
                Cursor.Current = Cursors.Default;
                dragging = false;
            }
        }
    }
}
