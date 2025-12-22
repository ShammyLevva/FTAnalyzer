using BruTile.Web;
using System.ComponentModel;
using static FTAnalyzer.Forms.Controls.ToolStripMapSelector;

namespace FTAnalyzer.Forms.Controls
{
    public class MapToolStripMenuItem : ToolStripMenuItem
    {
        public MapToolStripMenuItem(HttpTileSource tileSource, LinkLabelType linkLabelType)
            : base()
        {
            TileSource = tileSource;
            LinkLabelType = linkLabelType;
            Tag = linkLabelType;
        }

        public void SetupMapToolStripMenuItem(string name, string text, EventHandler eventHandler)
        {
            Name = name;
            Text = text;
            Click += eventHandler;
        }

        [DefaultValue(null)]
        public HttpTileSource TileSource { get; set; }
        [DefaultValue(null)]
        public LinkLabelType LinkLabelType { get; set; }
    }
}
