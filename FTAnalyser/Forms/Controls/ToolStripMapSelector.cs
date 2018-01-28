using System;
using System.Windows.Forms;
using SharpMap.Layers;
using SharpMap.Forms;
using System.Drawing;
using System.IO;
using BruTile.Predefined;
using FTAnalyzer.Mapping;

namespace FTAnalyzer.Forms.Controls
{
    public class ToolStripMapSelector : ToolStripDropDownButton
    {
        private LinkLabel copyrightLabel;
        private MapBox mapbox;
        private MapToolStripMenuItem mnuOpenStreetMap;
        private MapToolStripMenuItem mnuOpenHistoricMap;
        private MapToolStripMenuItem mnuBingMapAerial;
        private MapToolStripMenuItem mnuBingMapRoads;
        private MapToolStripMenuItem mnuBingMapHybrid;
        private MapToolStripMenuItem mnuNLS1843_1882;
        private MapToolStripMenuItem mnuNLS1885_1900;
        private MapToolStripMenuItem mnuNLS1921_1930;
        
        public ToolStripMapSelector()
            : base("Map style")
        { }

        public void Setup(LinkLabel label, MapBox mapbox)
        {
            this.copyrightLabel = label;
            this.mapbox = mapbox;
            SetupDropdown();
            GetCurrentMapPreference();
        }

        public void GetCurrentMapPreference()
        {
            string mapPreference = Application.UserAppDataRegistry.GetValue("Default Map Background", "mnuBingMapRoads").ToString();
            foreach (ToolStripMenuItem menu in this.DropDownItems)
            {
                if (mapPreference.Equals(menu.Name))
                {
                    _Click(menu, null);
                    break;
                }
            }
        }

        private void SetupDropdown()
        {
            TileSourceFactory factory = new TileSourceFactory();
            mnuOpenStreetMap = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.OpenStreetMap), LinkLabelType.OSM);
            mnuOpenHistoricMap = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.OpenHistoricMap), LinkLabelType.OSM);
            mnuBingMapAerial = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.BingAerial), LinkLabelType.BING);
            mnuBingMapRoads = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.BingRoads), LinkLabelType.BING);
            mnuBingMapHybrid = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.BingHybrid),  LinkLabelType.BING);
            mnuNLS1843_1882 = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.NLS_1843_1882_OS_6in), LinkLabelType.NLS);
            mnuNLS1885_1900 = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.NLS_1885_1900_OS_1in), LinkLabelType.NLS);
            mnuNLS1921_1930 = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.NLS_1921_1930_OS_6in), LinkLabelType.NLS);

            mnuOpenStreetMap.SetupMapToolStripMenuItem("mnuOpenStreetMap", "Open Street Map", new EventHandler(_Click));
            mnuOpenHistoricMap.SetupMapToolStripMenuItem("mnuOpenHistoricMap", "Open Historical Map 1920-1940 UK", new EventHandler(_Click));
            mnuBingMapAerial.SetupMapToolStripMenuItem("mnuBingMapAerial", "Aerial Bing Map", new EventHandler(_Click));
            mnuBingMapRoads.SetupMapToolStripMenuItem("mnuBingMapRoads", "Roads Bing Map", new EventHandler(_Click));
            mnuBingMapHybrid.SetupMapToolStripMenuItem("mnuBingMapHybrid", "Hybrid Bing Map", new EventHandler(_Click));
            mnuNLS1843_1882.SetupMapToolStripMenuItem("mnuNLS1843_1882", "NLS 1843-1882 OS 6in UK Map", new EventHandler(_Click));
            mnuNLS1885_1900.SetupMapToolStripMenuItem("mnuNLS1885_1900", "NLS 1885-1900 OS 1in UK Map", new EventHandler(_Click));
            mnuNLS1921_1930.SetupMapToolStripMenuItem("mnuNLS1921_1930", "NLS 1921-1930 OS 6in Scotland Map", new EventHandler(_Click));
            
            // Setup map selector menu
            DisplayStyle = ToolStripItemDisplayStyle.Text;
            DropDownItems.AddRange(new ToolStripItem[] {
            mnuOpenStreetMap,
            mnuOpenHistoricMap,
            mnuBingMapAerial,
            mnuBingMapRoads,
            mnuBingMapHybrid,
            mnuNLS1843_1882,
            mnuNLS1885_1900,
            mnuNLS1921_1930,
            });
            ImageTransparentColor = Color.Magenta;
            Name = "mnuMapStyle";
            Size = new Size(71, 22);
            Text = "Map style";
        }

        public enum LinkLabelType { GOOGLE, BING, OSM, NLS }

        public void UpdateLinkLabel(LinkLabelType type)
        {
            LinkLabel.Link link = new LinkLabel.Link();
            copyrightLabel.Links.Clear();
            switch (type)
            {
                case LinkLabelType.GOOGLE:
                    link.LinkData = "http://www.google.com/intl/en_ALL/help/terms_maps.html";
                    copyrightLabel.Text = "© Google - Terms of Use";
                    break;
                case LinkLabelType.BING:
                    link.LinkData = "http://www.microsoft.com/maps/product/terms.html";
                    copyrightLabel.Text = "© Bing Maps";
                    break;
                case LinkLabelType.OSM:
                    link.LinkData = "http://www.openstreetmap.org/copyright";
                    copyrightLabel.Text = "© OpenStreetMap";
                    break;
                case LinkLabelType.NLS:
                    link.LinkData = "http://maps.nls.uk/projects/api/index.html#licence";
                    copyrightLabel.Text = "© NLS";
                    break;
            }
            copyrightLabel.Links.Add(link);
        }

        private void _Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem menu in DropDownItems)
                menu.Checked = false;
            if(mapbox.Map.BackgroundLayer.Count > 0)
                mapbox.Map.BackgroundLayer.RemoveAt(0);
            MapToolStripMenuItem selectedOption = (MapToolStripMenuItem)sender;
            mapbox.Map.BackgroundLayer.Add(new TileAsyncLayer(selectedOption.TileSource, selectedOption.Name));
            selectedOption.Checked = true;
            UpdateLinkLabel(selectedOption.LinkLabelType);
            Application.UserAppDataRegistry.SetValue("Default Map Background", selectedOption.Name);
            mapbox.Refresh();
        }
    }
}
