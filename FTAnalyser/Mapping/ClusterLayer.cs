using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using SharpMap;
using SharpMap.Data;
using SharpMap.Data.Providers;
using SharpMap.Layers;
using SharpMap.Styles;
using GeoAPI.Geometries;

namespace FTAnalyzer.Mapping
{
    public class ClusterLayer : IDisposable
    {
        public FeatureDataTable FactLocations { get; private set; }
        private VectorLayer clusterLayer;
        private LabelLayer labelLayer;
        private MarkerClusterer clusterer;
        private Map map;

        public ClusterLayer(Map map)
        {
            this.map = map;
            SetupMap();
        }

        public void Clear()
        {
            FactLocations.Clear();
        }

        public void Refresh()
        {
            Envelope bounds = map.Envelope;
            double gridSize = Math.Max(map.Envelope.Width, map.Envelope.Height) / 20.0;
            bounds.ExpandBy(gridSize);
            clusterer.Recluster(gridSize, bounds);
        }

        public Envelope Bounds
        {
            get { return clusterLayer.Envelope; }
        }

        private void SetupMap()
        {
            FactLocations = new FeatureDataTable();
            FactLocations.Columns.Add("MapLocation", typeof(MapLocation));
            FactLocations.Columns.Add("ViewPort", typeof(Envelope));
            FactLocations.Columns.Add("Label", typeof(string));

            clusterer = new MarkerClusterer(FactLocations);
            GeometryFeatureProvider factLocationGFP = new GeometryFeatureProvider(clusterer.FeatureDataTable);

            clusterLayer = new VectorLayer("Clusters")
            {
                DataSource = factLocationGFP
            };

            Dictionary<string, IStyle> styles = new Dictionary<string, IStyle>();

            VectorStyle feature = new VectorStyle
            {
                PointColor = new SolidBrush(Color.Red),
                PointSize = 20,
                Symbol = Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\teardrop_blue.png")),
                SymbolOffset = new PointF(0.0f, -17.0f)
            };
            styles.Add(MapCluster.FEATURE, feature);

            VectorStyle cluster = new VectorStyle
            {
                PointColor = new SolidBrush(Color.ForestGreen),
                PointSize = 20,
                Symbol = Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\people35.png"))
            };
            styles.Add(MapCluster.CLUSTER, cluster);

            VectorStyle unknown = new VectorStyle
            {
                PointColor = new SolidBrush(Color.Black),
                PointSize = 10
            };
            styles.Add(MapCluster.UNKNOWN, unknown);

            clusterLayer.Theme = new SharpMap.Rendering.Thematics.UniqueValuesTheme<string>("Cluster", styles, unknown);
            map.Layers.Add(clusterLayer);

            labelLayer = new LabelLayer("Label")
            {
                DataSource = factLocationGFP,
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
                Offset = new PointF(0, 17),
                Halo = new Pen(Color.Yellow, 3)
            };
            labelLayer.Style = style;
            map.Layers.Add(labelLayer);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                FactLocations.Dispose();
                clusterLayer.Dispose();
                labelLayer.Dispose();
                clusterer.Dispose();
                map.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
