using FTAnalyzer.Mapping;
using SharpMap.Forms;
using SharpMap.Layers;

namespace FTAnalyzer.Forms.Controls
{
    public class ToolStripMapSelector : ToolStripDropDownButton
    {
        LinkLabel copyrightLabel;
        MapBox mapbox;
        TrackBar opacitySlider;
        readonly string defaultMap = "mnuOpenStreetMap";
        MapToolStripMenuItem mnuOpenStreetMap;
        MapToolStripMenuItem mnuOpenHistoricMap;
        MapToolStripMenuItem mnuBingMapAerial;
        MapToolStripMenuItem mnuBingMapRoads;
        MapToolStripMenuItem mnuBingMapHybrid;
        MapToolStripMenuItem mnuNLS1843_1882;
        MapToolStripMenuItem mnuNLS1885_1900;
        MapToolStripMenuItem mnuNLS1921_1930;

        public ToolStripMapSelector()
            : base("Map style")
        { }

        public void Setup(LinkLabel label, MapBox mapbox, TrackBar opacitySlider)
        {
            copyrightLabel = label;
            this.mapbox = mapbox;
            this.opacitySlider = opacitySlider;
            SetupDropdown();
            GetCurrentMapPreference();
        }

        public void GetCurrentMapPreference()
        {
            string mapPreference = Application.UserAppDataRegistry.GetValue("Default Map Background", defaultMap).ToString() ?? defaultMap;
            foreach (ToolStripMenuItem menu in DropDownItems)
            {
                if (mapPreference.Equals(menu.Name))
                {
                    Ctrl_Click(menu, null);
                    break;
                }
            }
        }

        void SetupDropdown()
        {
            TileSourceFactory factory = new();
            mnuOpenStreetMap = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.OpenStreetMap), LinkLabelType.OSM);
            mnuOpenHistoricMap = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.OpenHistoricMap), LinkLabelType.OSM);
            mnuBingMapAerial = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.BingAerial), LinkLabelType.BING);
            mnuBingMapRoads = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.BingRoads), LinkLabelType.BING);
            mnuBingMapHybrid = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.BingHybrid), LinkLabelType.BING);
            mnuNLS1843_1882 = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.NLS_1843_1882_OS_6in), LinkLabelType.NLS);
            mnuNLS1885_1900 = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.NLS_1885_1900_OS_1in), LinkLabelType.NLS);
            mnuNLS1921_1930 = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.NLS_1921_1930_OS_6in), LinkLabelType.NLS);

            mnuOpenStreetMap.SetupMapToolStripMenuItem("mnuOpenStreetMap", "Open Street Map", new EventHandler(Ctrl_Click));
            mnuOpenHistoricMap.SetupMapToolStripMenuItem("mnuOpenHistoricMap", "Open Historical Map 1920-1940 UK", new EventHandler(Ctrl_Click));
            mnuBingMapAerial.SetupMapToolStripMenuItem("mnuBingMapAerial", "Aerial Bing Map", new EventHandler(Ctrl_Click));
            mnuBingMapRoads.SetupMapToolStripMenuItem("mnuBingMapRoads", "Roads Bing Map", new EventHandler(Ctrl_Click));
            mnuBingMapHybrid.SetupMapToolStripMenuItem("mnuBingMapHybrid", "Hybrid Bing Map", new EventHandler(Ctrl_Click));
            mnuNLS1843_1882.SetupMapToolStripMenuItem("mnuNLS1843_1882", "NLS 1843-1882 OS 6in UK Map", new EventHandler(Ctrl_Click));
            mnuNLS1885_1900.SetupMapToolStripMenuItem("mnuNLS1885_1900", "NLS 1885-1900 OS 1in UK Map", new EventHandler(Ctrl_Click));
            mnuNLS1921_1930.SetupMapToolStripMenuItem("mnuNLS1921_1930", "NLS 1921-1930 OS 6in Scotland Map", new EventHandler(Ctrl_Click));

            // Setup map selector menu
            DisplayStyle = ToolStripItemDisplayStyle.Text;
            DropDownItems.AddRange(
                mnuOpenStreetMap,
                mnuOpenHistoricMap,
                mnuBingMapAerial,
                mnuBingMapRoads,
                mnuBingMapHybrid,
                mnuNLS1843_1882,
                mnuNLS1885_1900,
                mnuNLS1921_1930
            );
            ImageTransparentColor = Color.Magenta;
            Name = "mnuMapStyle";
            Size = new Size(71, 22);
            Text = "Map style";
        }

        public enum LinkLabelType { GOOGLE, BING, OSM, NLS }

        public void UpdateLinkLabel(LinkLabelType type)
        {
            LinkLabel.Link link = new();
            copyrightLabel.Links.Clear();
            switch (type)
            {
                case LinkLabelType.GOOGLE:
                    link.LinkData = "https://www.google.com/intl/en_ALL/help/terms_maps.html";
                    copyrightLabel.Text = "© Google - Terms of Use";
                    break;
                case LinkLabelType.BING:
                    link.LinkData = "https://www.microsoft.com/maps/product/terms.html";
                    copyrightLabel.Text = "© Bing Maps";
                    break;
                case LinkLabelType.OSM:
                    link.LinkData = "https://www.openstreetmap.org/copyright";
                    copyrightLabel.Text = "© OpenStreetMap";
                    break;
                case LinkLabelType.NLS:
                    link.LinkData = "https://maps.nls.uk/projects/api/index.html#licence";
                    copyrightLabel.Text = "© NLS";
                    break;
            }
            copyrightLabel.Links.Add(link);
        }

        void Ctrl_Click(object? sender, EventArgs e)
        {
            foreach (ToolStripMenuItem menu in DropDownItems)
                menu.Checked = false;
            while (mapbox.Map.BackgroundLayer.Count > 0)
                mapbox.Map.BackgroundLayer.RemoveAt(0);
            if (sender is MapToolStripMenuItem selectedOption)
            {
                mapbox.Map.BackgroundLayer.Add(new TileAsyncLayer(mnuOpenStreetMap.TileSource, mnuOpenStreetMap.Name ?? string.Empty));
                TileAsyncLayer mapLayer = new(selectedOption.TileSource, selectedOption.Name ?? string.Empty)
                {
                    OnlyRedrawWhenComplete = true,
                };
                mapbox.Map.BackgroundLayer.Add(mapLayer);
                selectedOption.Checked = true;
                opacitySlider.Visible = !selectedOption.Name.Equals(mnuOpenStreetMap.Name);
                UpdateLinkLabel(selectedOption.LinkLabelType);
                Application.UserAppDataRegistry.SetValue("Default Map Background", selectedOption.Name);
                mapbox.Refresh();
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                base.Dispose(disposing);
                mnuOpenStreetMap.Dispose();
                mnuOpenHistoricMap.Dispose();
                mnuBingMapAerial.Dispose();
                mnuBingMapRoads.Dispose();
                mnuBingMapHybrid.Dispose();
                mnuNLS1843_1882.Dispose();
                mnuNLS1885_1900.Dispose();
                mnuNLS1921_1930.Dispose();
            }
            catch (Exception) { }
        }
    }
}
