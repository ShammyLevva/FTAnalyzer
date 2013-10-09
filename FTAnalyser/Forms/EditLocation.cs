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

        public EditLocation(FactLocation location)
        {
            InitializeComponent();
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
            GeoResponse.CResult.CGeometry.CViewPort vp = location.Location.ViewPort;
            Envelope bbox = new Envelope(vp.NorthEast.Lat, vp.NorthEast.Long, vp.SouthWest.Lat, vp.SouthWest.Long);
            Envelope expand = new Envelope(transform.Transform(bbox.TopLeft()), transform.Transform(bbox.BottomRight()));
                    
            mapBox1.Map.Layers.Add(pointLayer);
            mapBox1.Map.MinimumZoom = 1000;
            mapBox1.Map.MaximumZoom = 50000000;
            mapBox1.Map.ZoomToBox(expand);
            mapBox1.Refresh();
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
        }
    }
}
