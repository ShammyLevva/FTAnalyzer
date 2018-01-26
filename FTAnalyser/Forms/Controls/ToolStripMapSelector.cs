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
        private ToolStripMenuItem mnuGoogleMap;
        private ToolStripMenuItem mnuGoogleSatellite;
        private ToolStripMenuItem mnuOpenStreetMap;
        private ToolStripMenuItem mnuBingMapAerial;
        private ToolStripMenuItem mnuBingMapRoads;
        private ToolStripMenuItem mnuBingMapHybrid;
        private ToolStripMenuItem mnuBingMapOS;
        private ToolStripMenuItem mnuNLSOSHistoric;

        public ToolStripMapSelector()
            : base("Map style")
        {
            //this.DisplayStyle = ToolStripItemDisplayStyle.Text;
            //this.ImageTransparentColor = Color.Magenta;
            //this.Name = "mnuMapStyle";
            //this.Size = new System.Drawing.Size(71, 22);
        }

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
            mnuGoogleMap = new ToolStripMenuItem();
            mnuGoogleSatellite = new ToolStripMenuItem();
            mnuOpenStreetMap = new MapToolStripMenuItem(KnownTileSources.Create(KnownTileSource.OpenStreetMap), "mnuOpenStreetMap", "Open Street Map", LinkLabelType.OSM, new EventHandler(_Click));
            mnuBingMapAerial = new MapToolStripMenuItem(KnownTileSources.Create(KnownTileSource.BingAerial), "mnuBingMapAerial", "Aerial Bing Map", LinkLabelType.BING, new EventHandler(_Click));
            mnuBingMapRoads = new MapToolStripMenuItem(KnownTileSources.Create(KnownTileSource.BingRoads), "mnuBingMapRoads", "Roads Bing Map", LinkLabelType.BING, new EventHandler(_Click));
            mnuBingMapHybrid = new MapToolStripMenuItem(KnownTileSources.Create(KnownTileSource.BingHybrid), "mnuBingMapHybrid", "Hybrid Bing Map", LinkLabelType.BING, new EventHandler(_Click));
            mnuBingMapOS = new ToolStripMenuItem();
            mnuNLSOSHistoric = new ToolStripMenuItem();

            // Setup map selector menu
            DisplayStyle = ToolStripItemDisplayStyle.Text;
            DropDownItems.AddRange(new ToolStripItem[] {
            //mnuGoogleMap,
            //mnuGoogleSatellite,
            mnuOpenStreetMap,
            mnuBingMapAerial,
            mnuBingMapRoads,
            mnuBingMapHybrid,
            //mnuBingMapOS,
            //mnuNLSOSHistoric
            });
            ImageTransparentColor = Color.Magenta;
            Name = "mnuMapStyle";
            Size = new Size(71, 22);
            Text = "Map style";
            // 
            // mnuGoogleMap
            // 
            mnuGoogleMap.Name = "mnuGoogleMap";
            mnuGoogleMap.Text = "Google Map";
            // 
            // mnuGoogleSatellite
            // 
            mnuGoogleSatellite.Name = "mnuGoogleSatellite";
            mnuGoogleSatellite.Text = "Google Satellite";
            // 
            // 
            // mnuBingMapOS
            // 
            mnuBingMapOS.Name = "mnuBingMapOS";
            mnuBingMapOS.Text = "OS Bing Map (UK)";
            // 
            // mnuNLSOSHistoric
            // 
            mnuNLSOSHistoric.Name = "mnuNLSOSHistoric";
            mnuNLSOSHistoric.Text = "NLS OS 1920-1947 (UK)";
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
