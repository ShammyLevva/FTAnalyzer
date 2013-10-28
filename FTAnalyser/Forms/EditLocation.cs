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
using FTAnalyzer.Utilities;
using System.Data.SQLite;

namespace FTAnalyzer.Forms
{
    public partial class EditLocation : Form
    {
        private FeatureDataTable pointTable;
        private VectorLayer pointLayer;
        private FactLocation location;
        private FactLocation originalLocation;
        private FeatureDataRow pointFeature;
        private bool iconSelected;
        private bool pointUpdated;
        private bool dataUpdated;

        public EditLocation(FactLocation location)
        {
            InitializeComponent();
            mapZoomToolStrip.Items[2].ToolTipText = "Zoom out of Map"; // fix bug in SharpMapUI component
            mapZoomToolStrip.Items[10].Visible = false;
            this.location = location;
            this.originalLocation = FactLocation.TEMP;
            CopyLocationDetails(location, originalLocation);
            this.Text = "Editing : " + location.ToString();
            iconSelected = false;
            pointUpdated = false;
            dataUpdated = false;
            SetupMap();
        }

        private void CopyLocationDetails(FactLocation from, FactLocation to)
        {
            to.Latitude = from.Latitude;
            to.Longitude = from.Longitude;
            to.ViewPort.NorthEast.Lat = from.ViewPort.NorthEast.Lat;
            to.ViewPort.NorthEast.Long = from.ViewPort.NorthEast.Long;
            to.ViewPort.SouthWest.Lat = from.ViewPort.SouthWest.Lat;
            to.ViewPort.SouthWest.Long = from.ViewPort.SouthWest.Long;
            to.GeocodeStatus = from.GeocodeStatus;
        }

        private void SetupMap()
        {
            // Add Google maps layer to map control.
            //HttpUtility.SetDefaultProxy();
            mapBox1.Map.BackgroundLayer.Add(new TileAsyncLayer(
                new GoogleTileSource(GoogleMapType.GoogleMap), "GoogleMap"));
            //      new BingTileSource(BingRequest.UrlBing, "", BingMapType.Roads),"BingMap"));
            pointTable = new FeatureDataTable();
            pointTable.Columns.Add("Label", typeof(string));
            
            GeometryFeatureProvider pointGFP = new GeometryFeatureProvider(pointTable);

            pointLayer = new VectorLayer("Point to Edit");
            pointLayer.Style.Symbol = Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\teardrop_blue.png"));
            pointLayer.Style.SymbolOffset = new PointF(0.0f, -17.0f);
            pointLayer.DataSource = pointGFP;
            pointLayer.CoordinateTransformation = MapTransforms.Transform();
            pointLayer.ReverseCoordinateTransformation = MapTransforms.ReverseTransform();

            mapBox1.Map.Layers.Add(pointLayer);
            mapBox1.Map.MinimumZoom = 500;
            mapBox1.Map.MaximumZoom = 50000000;
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
            ResetMap();
        }

        private void ResetMap()
        {
            CopyLocationDetails(originalLocation, location); 
            pointTable.Clear();
            pointTable.AddRow(GetRow(location.Longitude, location.Latitude));

            IMathTransform transform = pointLayer.CoordinateTransformation.MathTransform;
            GeoResponse.CResult.CGeometry.CViewPort vp = location.ViewPort;
            Envelope expand;
            if (vp.NorthEast.Lat == 0 && vp.NorthEast.Long == 0 && vp.SouthWest.Lat == 0 && vp.SouthWest.Long == 0)
                expand = new Envelope(-25000000, 25000000, -17000000, 17000000);
            else
            {
                Envelope bbox = new Envelope(vp.NorthEast.Long, vp.SouthWest.Long, vp.NorthEast.Lat, vp.SouthWest.Lat);
                expand = new Envelope(transform.Transform(bbox.TopLeft()), transform.Transform(bbox.BottomRight()));
            }
            mapBox1.Map.ZoomToBox(expand);
            mapBox1.Refresh();
        }

        private FeatureDataRow GetRow(double p1, double p2)
        {
            pointFeature = pointTable.NewRow();
            pointFeature["Label"] = location.ToString();
            pointFeature.Geometry = new NetTopologySuite.Geometries.Point(p1, p2);
            return pointFeature;
        }
        
        private void mapBox1_MouseClick(object sender, MouseEventArgs e)
        {
            GeoAPI.Geometries.Coordinate c1 = mapBox1.Map.ImageToWorld(new PointF(e.X - 21.0f, e.Y - 34.0f));
            GeoAPI.Geometries.Coordinate c2 = mapBox1.Map.ImageToWorld(new PointF(e.X + 21.0f, e.Y + 34.0f));
            IMathTransform transform = pointLayer.ReverseCoordinateTransformation.MathTransform;
            c1 = transform.Transform(c1);
            c2 = transform.Transform(c2);
            Envelope env = new Envelope(c1, c2);
            if (iconSelected && e.Button == MouseButtons.Right)
            {
                // we have finished and are saving icon
                mapBox1.Cursor = Cursors.Default;
                GeoAPI.Geometries.Coordinate c = mapBox1.Map.ImageToWorld(new PointF(e.X, e.Y + 17.0f));
                c = transform.Transform(c);
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

        private void EditLocation_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If point updated and we are warning then warn
            if (pointUpdated)
            {
                DialogResult result = DialogResult.Yes;
                if (Application.UserAppDataRegistry.GetValue("Ask to update database", "True").Equals("True"))
                {
                    result = MessageBox.Show("Do you want to save this new position", "Save changes", 
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                }
                if (result == DialogResult.Cancel)
                    e.Cancel = true;
                else if (result == DialogResult.Yes)
                {
                    UpdateDatabase();
                }
            }
        }

        private void UpdateDatabase()
        {
            IMathTransform transform = pointLayer.ReverseCoordinateTransformation.MathTransform;
            Envelope env = new Envelope(transform.Transform(mapBox1.Map.Envelope.TopLeft()),
                                        transform.Transform(mapBox1.Map.Envelope.BottomRight()));

            DatabaseHelper dbh = DatabaseHelper.Instance;
            SQLiteCommand updateCmd = dbh.UpdatePointGeocode();
            location.Latitude = pointFeature.Geometry.Coordinate.Y;
            location.Longitude = pointFeature.Geometry.Coordinate.X;
            location.ViewPort.NorthEast.Lat = env.Top();
            location.ViewPort.NorthEast.Long = env.Right();
            location.ViewPort.SouthWest.Lat = env.Bottom();
            location.ViewPort.SouthWest.Long = env.Left();
            location.GeocodeStatus = FactLocation.Geocode.GEDCOM_USER;

            updateCmd.Parameters[0].Value = location.Latitude;
            updateCmd.Parameters[1].Value = location.Longitude;
            updateCmd.Parameters[2].Value = location.ViewPort.NorthEast.Lat;
            updateCmd.Parameters[3].Value = location.ViewPort.NorthEast.Long;
            updateCmd.Parameters[4].Value = location.ViewPort.SouthWest.Lat;
            updateCmd.Parameters[5].Value = location.ViewPort.SouthWest.Long;
            updateCmd.Parameters[6].Value = location.GeocodeStatus;
            updateCmd.Parameters[7].Value = location.ToString();
            updateCmd.ExecuteNonQuery();
            pointUpdated = false;
            dataUpdated = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateDatabase();
            MessageBox.Show("Data for " + location.ToString() + " updated.", "Save new location");
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            ResetMap();
            if (dataUpdated)
                UpdateDatabase();
            dataUpdated = false;
            pointUpdated = false;
        }

        private void mnuMapStyle_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem menu in mnuMapStyle.DropDownItems)
                menu.Checked = false;
            mapBox1.Map.BackgroundLayer.RemoveAt(0);
            if (sender == mnuGoogleMap)
            {
                mapBox1.Map.BackgroundLayer.Add(new TileAsyncLayer(
                        new GoogleTileSource(GoogleMapType.GoogleMap), "GoogleMap"));
                mnuGoogleMap.Checked = true;
            }
            else if (sender == mnuGoogleSatellite)
            {
                mapBox1.Map.BackgroundLayer.Add(new TileAsyncLayer(
                        new GoogleTileSource(GoogleMapType.GoogleSatellite), "GoogleSatellite"));
                mnuGoogleSatellite.Checked = true;
            }
            else if (sender == mnuOpenStreetMap)
            {
                mapBox1.Map.BackgroundLayer.Add(new TileAsyncLayer(
                    new BruTile.Web.OsmTileSource(), "OpenStreetMap"));
                mnuOpenStreetMap.Checked = true;
            }
            else if (sender == mnuBingMapAerial)
            {
                mapBox1.Map.BackgroundLayer.Add(new TileAsyncLayer(
                    new BruTile.Web.BingTileSource(BingRequest.UrlBing, null, BingMapType.Aerial), "BingMapAerial"));
                mnuBingMapAerial.Checked = true;
            }
            else if (sender == mnuBingMapRoads)
            {
                mapBox1.Map.BackgroundLayer.Add(new TileAsyncLayer(
                    new BruTile.Web.BingTileSource(BingRequest.UrlBing, null, BingMapType.Roads), "BingMapRoads"));
                mnuBingMapRoads.Checked = true;
            }
            else if (sender == mnuBingMapHybrid)
            {
                mapBox1.Map.BackgroundLayer.Add(new TileAsyncLayer(
                    new BruTile.Web.BingTileSource(BingRequest.UrlBing, null, BingMapType.Hybrid), "BingMapHybrid"));
                mnuBingMapHybrid.Checked = true;
            }
            else if (sender == mnuBingMapOS)
            {
                mapBox1.Map.BackgroundLayer.Add(new TileAsyncLayer(
                    new BingOSTileSource(BingRequest.UrlBing, null, BingMapType.Roads), "BingMapOS"));
                mnuBingMapOS.Checked = true;
            }
            mapBox1.Refresh();
        }
    }
}
