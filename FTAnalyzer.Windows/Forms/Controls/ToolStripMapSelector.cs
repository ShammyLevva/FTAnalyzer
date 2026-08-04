using FTAnalyzer.Mapping;
using FTAnalyzer.Shared.Utilities;
using Microsoft.Win32;
using SharpMap.Forms;
using SharpMap.Layers;
using System.Diagnostics.CodeAnalysis;

namespace FTAnalyzer.Forms.Controls
{
    public class ToolStripMapSelector : ToolStripDropDownButton
    {
        LinkLabel copyrightLabel = new();
        MapBox mapbox = new();
        TrackBar opacitySlider = new();
        readonly string defaultMap = "mnuOpenStreetMap";
        MapToolStripMenuItem mnuOpenStreetMap;
        MapToolStripMenuItem mnuOpenHistoricMap;
        MapToolStripMenuItem mnuBingMapAerial;
        MapToolStripMenuItem mnuBingMapRoads;
        MapToolStripMenuItem mnuBingMapHybrid;
        MapToolStripMenuItem mnuUsgsHistorical;
        ArcGisImageServerRequest usgsRequest;
        //MapToolStripMenuItem mnuNLS1843_1882;
        //MapToolStripMenuItem mnuNLS1885_1900;
        //MapToolStripMenuItem mnuNLS1921_1930;

        const string UsgsYearRegistryKey = "USGS Historical Map Year";

        readonly NumericUpDown yearControl = new()
        {
            Minimum = UsgsHistoricalMap.MinYear,
            Maximum = UsgsHistoricalMap.MaxYear,
            Value = RegistrySettings.GetIntRegistryValue(UsgsYearRegistryKey, UsgsHistoricalMap.DefaultYear),
            Width = 50,
        };

        public ToolStripControlHost YearSelector { get; }

        public ToolStripMapSelector()
            : base("Map style")
        {
            YearSelector = new ToolStripControlHost(yearControl) { Visible = false };
            yearControl.ValueChanged += YearControl_ValueChanged;
            SetupDropdown();
        }

        public void Setup(LinkLabel label, MapBox mapbox, TrackBar opacitySlider)
        {
            copyrightLabel = label;
            this.mapbox = mapbox;
            this.opacitySlider = opacitySlider;
            RefreshUsgsAvailability();
            GetCurrentMapPreference();
        }

        // Called once from Setup(), and available for a form to re-call after newly geocoding
        // locations, so the option doesn't stay disabled until the map form is reopened.
        public void RefreshUsgsAvailability()
        {
            mnuUsgsHistorical.Enabled = FactLocation.HasUSLocations;
            mnuUsgsHistorical.ToolTipText = mnuUsgsHistorical.Enabled ? null : "No US locations found in this tree";
        }

        public void GetCurrentMapPreference()
        {
            string mapPreference = RegistrySettings.GetStringRegistryValue("Default Map Background", defaultMap);
            foreach (ToolStripMenuItem menu in DropDownItems)
            {
                if (mapPreference.Equals(menu.Name, StringComparison.OrdinalIgnoreCase))
                {
                    Ctrl_Click(menu, EventArgs.Empty);
                    break;
                }
            }
        }

        [MemberNotNull(nameof(mnuOpenStreetMap), nameof(mnuOpenHistoricMap), nameof(mnuBingMapAerial), nameof(mnuBingMapRoads), nameof(mnuBingMapHybrid), nameof(mnuUsgsHistorical), nameof(usgsRequest))]
        void SetupDropdown()
        {
            TileSourceFactory factory = new();
            mnuOpenStreetMap = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.OpenStreetMap), LinkLabelType.OSM);
            mnuOpenHistoricMap = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.OpenHistoricMap), LinkLabelType.OSM);
            mnuBingMapAerial = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.BingAerial), LinkLabelType.BING);
            mnuBingMapRoads = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.BingRoads), LinkLabelType.BING);
            mnuBingMapHybrid = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.BingHybrid), LinkLabelType.BING);
            usgsRequest = TileSourceFactory.CreateUsgsHistoricalRequest();
            usgsRequest.HistoricalYear = (int)yearControl.Value;
            mnuUsgsHistorical = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.UsgsHistorical, usgsRequest), LinkLabelType.USGS);
            //mnuNLS1843_1882 = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.NLS_1843_1882_OS_6in), LinkLabelType.NLS);
            //mnuNLS1885_1900 = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.NLS_1885_1900_OS_1in), LinkLabelType.NLS);
            //mnuNLS1921_1930 = new MapToolStripMenuItem(factory.CreateTileSource(TileSourceFactory.TileType.NLS_1921_1930_OS_6in), LinkLabelType.NLS);

            mnuOpenStreetMap.SetupMapToolStripMenuItem("mnuOpenStreetMap", "Open Street Map", new EventHandler(Ctrl_Click));
            mnuOpenHistoricMap.SetupMapToolStripMenuItem("mnuOpenHistoricMap", "Open Historical Map 1920-1940 UK", new EventHandler(Ctrl_Click));
            mnuBingMapAerial.SetupMapToolStripMenuItem("mnuBingMapAerial", "Aerial Bing Map", new EventHandler(Ctrl_Click));
            mnuBingMapRoads.SetupMapToolStripMenuItem("mnuBingMapRoads", "Roads Bing Map", new EventHandler(Ctrl_Click));
            mnuBingMapHybrid.SetupMapToolStripMenuItem("mnuBingMapHybrid", "Hybrid Bing Map", new EventHandler(Ctrl_Click));
            mnuUsgsHistorical.SetupMapToolStripMenuItem("mnuUsgsHistorical", "USGS Historical Topo Map (US)", new EventHandler(Ctrl_Click));
            //mnuNLS1843_1882.SetupMapToolStripMenuItem("mnuNLS1843_1882", "NLS 1843-1882 OS 6in UK Map", new EventHandler(Ctrl_Click));
            //mnuNLS1885_1900.SetupMapToolStripMenuItem("mnuNLS1885_1900", "NLS 1885-1900 OS 1in UK Map", new EventHandler(Ctrl_Click));
            //mnuNLS1921_1930.SetupMapToolStripMenuItem("mnuNLS1921_1930", "NLS 1921-1930 OS 6in Scotland Map", new EventHandler(Ctrl_Click));

            // Setup map selector menu
            DisplayStyle = ToolStripItemDisplayStyle.Text;
            DropDownItems.AddRange(
                mnuOpenStreetMap,
                mnuOpenHistoricMap,
                mnuBingMapAerial,
                mnuBingMapRoads,
                mnuBingMapHybrid,
                mnuUsgsHistorical
                //mnuNLS1843_1882,
                //mnuNLS1885_1900,
                //mnuNLS1921_1930
            );
            ImageTransparentColor = Color.Magenta;
            Name = "mnuMapStyle";
            Size = new Size(71, 22);
            Text = "Map style";
        }

        public enum LinkLabelType { GOOGLE, BING, OSM, NLS, USGS }

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
                case LinkLabelType.USGS:
                    link.LinkData = UsgsHistoricalMap.AttributionUrl;
                    copyrightLabel.Text = "USGS Historical Topographic Map Collection, via Esri Living Atlas (public domain)";
                    break;
            }
            copyrightLabel.Links.Add(link);
        }

        MapToolStripMenuItem? selectedMap;

        void Ctrl_Click(object? sender, EventArgs e)
        {
            foreach (ToolStripMenuItem menu in DropDownItems)
                menu.Checked = false;
            if (sender is MapToolStripMenuItem selectedOption)
            {
                selectedMap = selectedOption;
                selectedOption.Checked = true;
                bool isOpenStreetMap = string.Equals(selectedOption.Name, mnuOpenStreetMap.Name, StringComparison.OrdinalIgnoreCase);
                opacitySlider.Visible = !isOpenStreetMap;
                YearSelector.Visible = string.Equals(selectedOption.Name, mnuUsgsHistorical.Name, StringComparison.OrdinalIgnoreCase);
                UpdateLinkLabel(selectedOption.LinkLabelType);
                string backgroundName = selectedOption.Name ?? defaultMap;
                RegistrySettings.SetRegistryValue("Default Map Background", backgroundName, RegistryValueKind.String);
                RebuildSelectedLayer();
            }
        }

        // Rebuilds the background tile layer(s) for whatever is currently selected. Split out of
        // Ctrl_Click so the year control can also trigger it: TileAsyncLayer's own render cache is
        // keyed by tile index only, with no notion of "year", so mutating usgsRequest.HistoricalYear
        // and calling mapbox.Refresh() alone would very likely repaint stale cached tiles - the
        // layer instance itself must be rebuilt, same as switching basemaps.
        void RebuildSelectedLayer()
        {
            if (selectedMap is null)
                return;
            while (mapbox.Map.BackgroundLayer.Count > 0)
                mapbox.Map.BackgroundLayer.RemoveAt(0);
            bool isOpenStreetMap = string.Equals(selectedMap.Name, mnuOpenStreetMap.Name, StringComparison.OrdinalIgnoreCase);
            if (!isOpenStreetMap && opacitySlider.Value < opacitySlider.Maximum)
                mapbox.Map.BackgroundLayer.Add(new TileAsyncLayer(mnuOpenStreetMap.TileSource, mnuOpenStreetMap.Name ?? string.Empty));
            TileAsyncLayer mapLayer = new(selectedMap.TileSource, selectedMap.Name ?? string.Empty)
            {
                OnlyRedrawWhenComplete = true,
            };
            mapbox.Map.BackgroundLayer.Add(mapLayer);
            mapbox.Refresh();
        }

        void YearControl_ValueChanged(object? sender, EventArgs e)
        {
            int year = (int)yearControl.Value;
            usgsRequest.HistoricalYear = year;
            RegistrySettings.SetRegistryValue(UsgsYearRegistryKey, year, RegistryValueKind.DWord);
            if (selectedMap is not null && string.Equals(selectedMap.Name, mnuUsgsHistorical.Name, StringComparison.OrdinalIgnoreCase))
                RebuildSelectedLayer();
        }

        // Adds or removes the OpenStreetMap base layer depending on whether the opacity
        // slider needs it to show through. At full opacity the base layer would be
        // completely hidden, so we avoid loading it to halve the tile requests.
        public void UpdateOpacityLayer()
        {
            if (selectedMap is null || string.Equals(selectedMap.Name, mnuOpenStreetMap.Name, StringComparison.OrdinalIgnoreCase))
                return;
            bool needsOsmBase = opacitySlider.Value < opacitySlider.Maximum;
            bool hasOsmBase = mapbox.Map.BackgroundLayer.Count > 1;
            if (needsOsmBase && !hasOsmBase)
                mapbox.Map.BackgroundLayer.Insert(0, new TileAsyncLayer(mnuOpenStreetMap.TileSource, mnuOpenStreetMap.Name ?? string.Empty));
            else if (!needsOsmBase && hasOsmBase)
                mapbox.Map.BackgroundLayer.RemoveAt(0);
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
                mnuUsgsHistorical.Dispose();
                YearSelector.Dispose();
                //mnuNLS1843_1882.Dispose();
                //mnuNLS1885_1900.Dispose();
                //mnuNLS1921_1930.Dispose();
            }
            catch (Exception) { }
        }
    }
}
