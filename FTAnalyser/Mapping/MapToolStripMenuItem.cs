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

        public MapToolStripMenuItem(HttpTileSource tileSource, string name, string text, LinkLabelType linkLabelType, EventHandler eventHandler) 
        {
            TileSource = tileSource;
            Name = name;
            Text = text;
            LinkLabelType = linkLabelType;
            CheckOnClick = true;
            Size = new Size(164, 22);
            Tag = linkLabelType;
            Click += eventHandler;
        }

        public HttpTileSource TileSource { get => tileSource; set => tileSource = value; }
        public LinkLabelType LinkLabelType { get => linkLabelType; set => linkLabelType = value; }
    }
}
