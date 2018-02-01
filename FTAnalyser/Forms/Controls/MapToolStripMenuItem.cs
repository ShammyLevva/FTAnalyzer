using System;
using System.Windows.Forms;
using System.Drawing;
using BruTile.Web;
using static FTAnalyzer.Forms.Controls.ToolStripMapSelector;

namespace FTAnalyzer.Mapping
{
    public class MapToolStripMenuItem : ToolStripMenuItem
    {
        private HttpTileSource tileSource;
        private LinkLabelType linkLabelType;

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

        public HttpTileSource TileSource { get => tileSource; set => tileSource = value; }
        public LinkLabelType LinkLabelType { get => linkLabelType; set => linkLabelType = value; }
    }
}
