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

namespace FTAnalyzer.Mapping
{
    public class ClusterLayer
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
            clusterer.Recluster(Math.Max(map.Envelope.Width, map.Envelope.Height) / 35.0);
        }

        private void SetupMap()
        {
            FactLocations = new FeatureDataTable();
            FactLocations.Columns.Add("MapLocation", typeof(MapLocation));
            FactLocations.Columns.Add("Label", typeof(string));

            clusterer = new MarkerClusterer(FactLocations);
            GeometryFeatureProvider factLocationGFP = new GeometryFeatureProvider(clusterer.FeatureDataTable);

            clusterLayer = new VectorLayer("Clusters");
            clusterLayer.DataSource = factLocationGFP;

            Dictionary<string, IStyle> styles = new Dictionary<string, IStyle>();

            VectorStyle feature = new VectorStyle();
            feature.PointColor = new SolidBrush(Color.Red);
            feature.PointSize = 20;
            feature.Symbol = Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\teardrop_blue.png"));
            feature.SymbolOffset = new PointF(0.0f, -17.0f);
            styles.Add(MapCluster.FEATURE, feature);

            VectorStyle cluster = new VectorStyle();
            cluster.PointColor = new SolidBrush(Color.ForestGreen);
            cluster.PointSize = 20;
            cluster.Symbol = Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\people35.png"));
            styles.Add(MapCluster.CLUSTER, cluster);

            VectorStyle unknown = new VectorStyle();
            unknown.PointColor = new SolidBrush(Color.Black);
            unknown.PointSize = 10;
            styles.Add(MapCluster.UNKNOWN, unknown);

            clusterLayer.Theme = new SharpMap.Rendering.Thematics.UniqueValuesTheme<string>("Cluster", styles, unknown);
            map.Layers.Add(clusterLayer);

            labelLayer = new LabelLayer("Label");
            labelLayer.DataSource = factLocationGFP;
            labelLayer.Enabled = true;
            //Specifiy field that contains the label string.
            labelLayer.LabelColumn = "Label";
            labelLayer.TextRenderingHint = TextRenderingHint.AntiAlias;
            labelLayer.SmoothingMode = SmoothingMode.AntiAlias;
            LabelStyle style = new LabelStyle();
            style.ForeColor = Color.Black;
            style.Font = new Font(FontFamily.GenericSerif, 14, FontStyle.Bold);
            style.HorizontalAlignment = LabelStyle.HorizontalAlignmentEnum.Center;
            style.VerticalAlignment = LabelStyle.VerticalAlignmentEnum.Bottom;
            style.CollisionDetection = true;
            style.Offset = new PointF(2, 22);
            style.Halo = new Pen(Color.Yellow, 3);
            labelLayer.Style = style;
            map.Layers.Add(labelLayer);
        }
    }
}
