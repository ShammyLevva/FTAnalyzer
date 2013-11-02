using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using SharpMap.Layers;
using FTAnalyzer.Mapping;
using BruTile.Web;
using SharpMap.Forms;
using System.Drawing;

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

        public ToolStripMapSelector()
            : base()
        {
            this.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Name = "mnuMapStyle";
            this.Size = new System.Drawing.Size(71, 22);
            this.Text = "Map style";
        }

        public void Setup(LinkLabel label, MapBox mapbox)
        {
            this.copyrightLabel = label;
            this.mapbox = mapbox;
            SetupDropdown();
            UpdateLinkLabel(LinkLabelType.GOOGLE);
        }

        private void SetupDropdown()
        {
            this.mnuGoogleMap = new ToolStripMenuItem();
            this.mnuGoogleSatellite = new ToolStripMenuItem();
            this.mnuOpenStreetMap = new ToolStripMenuItem();
            this.mnuBingMapAerial = new ToolStripMenuItem();
            this.mnuBingMapRoads = new ToolStripMenuItem();
            this.mnuBingMapHybrid = new ToolStripMenuItem();
            this.mnuBingMapOS = new ToolStripMenuItem();

            // Setup map selector menu
            this.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.DropDownItems.AddRange(new ToolStripItem[] {
            this.mnuGoogleMap,
            this.mnuGoogleSatellite,
            this.mnuOpenStreetMap,
            this.mnuBingMapAerial,
            this.mnuBingMapRoads,
            this.mnuBingMapHybrid,
            this.mnuBingMapOS});
            this.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Name = "mnuMapStyle";
            this.Size = new System.Drawing.Size(71, 22);
            this.Text = "Map style";
            // 
            // mnuGoogleMap
            // 
            this.mnuGoogleMap.Checked = true;
            this.mnuGoogleMap.CheckOnClick = true;
            this.mnuGoogleMap.CheckState = CheckState.Checked;
            this.mnuGoogleMap.Name = "mnuGoogleMap";
            this.mnuGoogleMap.Size = new System.Drawing.Size(164, 22);
            this.mnuGoogleMap.Text = "Google Map";
            this.mnuGoogleMap.Click += new System.EventHandler(this._Click);
            // 
            // mnuGoogleSatellite
            // 
            this.mnuGoogleSatellite.CheckOnClick = true;
            this.mnuGoogleSatellite.Name = "mnuGoogleSatellite";
            this.mnuGoogleSatellite.Size = new System.Drawing.Size(164, 22);
            this.mnuGoogleSatellite.Text = "Google Satellite";
            this.mnuGoogleSatellite.Visible = false;
            this.mnuGoogleSatellite.Click += new System.EventHandler(this._Click);
            // 
            // mnuOpenStreetMap
            // 
            this.mnuOpenStreetMap.CheckOnClick = true;
            this.mnuOpenStreetMap.Name = "mnuOpenStreetMap";
            this.mnuOpenStreetMap.Size = new System.Drawing.Size(164, 22);
            this.mnuOpenStreetMap.Text = "Open Street Map";
            this.mnuOpenStreetMap.Click += new System.EventHandler(this._Click);
            // 
            // mnuBingMapAerial
            // 
            this.mnuBingMapAerial.CheckOnClick = true;
            this.mnuBingMapAerial.Name = "mnuBingMapAerial";
            this.mnuBingMapAerial.Size = new System.Drawing.Size(164, 22);
            this.mnuBingMapAerial.Text = "Aerial Bing Map";
            this.mnuBingMapAerial.Click += new System.EventHandler(this._Click);
            // 
            // mnuBingMapRoads
            // 
            this.mnuBingMapRoads.CheckOnClick = true;
            this.mnuBingMapRoads.Name = "mnuBingMapRoads";
            this.mnuBingMapRoads.Size = new System.Drawing.Size(164, 22);
            this.mnuBingMapRoads.Text = "Roads Bing Map";
            this.mnuBingMapRoads.Click += new System.EventHandler(this._Click);
            // 
            // mnuBingMapHybrid
            // 
            this.mnuBingMapHybrid.CheckOnClick = true;
            this.mnuBingMapHybrid.Name = "mnuBingMapHybrid";
            this.mnuBingMapHybrid.Size = new System.Drawing.Size(164, 22);
            this.mnuBingMapHybrid.Text = "Hybrid Bing Map";
            this.mnuBingMapHybrid.Click += new System.EventHandler(this._Click);
            // 
            // mnuBingMapOS
            // 
            this.mnuBingMapOS.Name = "mnuBingMapOS";
            this.mnuBingMapOS.Size = new System.Drawing.Size(164, 22);
            this.mnuBingMapOS.Text = "OS Bing Map";
            this.mnuBingMapOS.Visible = true;
            this.mnuBingMapOS.Click += new System.EventHandler(this._Click);
        }

        public enum LinkLabelType { GOOGLE, BING, OSM }

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
            }
            copyrightLabel.Links.Add(link);
        }

        private void _Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem menu in this.DropDownItems)
                menu.Checked = false;
            mapbox.Map.BackgroundLayer.RemoveAt(0);
            if (sender == mnuGoogleMap)
            {
                mapbox.Map.BackgroundLayer.Add(new TileAsyncLayer(
                        new GoogleTileSource(GoogleMapType.GoogleMap), "GoogleMap"));
                mnuGoogleMap.Checked = true;
                UpdateLinkLabel(LinkLabelType.GOOGLE);
            }
            else if (sender == mnuGoogleSatellite)
            {
                mapbox.Map.BackgroundLayer.Add(new TileAsyncLayer(
                        new GoogleTileSource(GoogleMapType.GoogleSatellite), "GoogleSatellite"));
                mnuGoogleSatellite.Checked = true;
                UpdateLinkLabel(LinkLabelType.GOOGLE);
            }
            else if (sender == mnuOpenStreetMap)
            {
                mapbox.Map.BackgroundLayer.Add(new TileAsyncLayer(
                    new BruTile.Web.OsmTileSource(), "OpenStreetMap"));
                mnuOpenStreetMap.Checked = true;
                UpdateLinkLabel(LinkLabelType.OSM);
            }
            else if (sender == mnuBingMapAerial)
            {
                mapbox.Map.BackgroundLayer.Add(new TileAsyncLayer(
                    new BruTile.Web.BingTileSource(BingRequest.UrlBing, null, BingMapType.Aerial), "BingMapAerial"));
                mnuBingMapAerial.Checked = true;
                UpdateLinkLabel(LinkLabelType.BING);
            }
            else if (sender == mnuBingMapRoads)
            {
                mapbox.Map.BackgroundLayer.Add(new TileAsyncLayer(
                    new BruTile.Web.BingTileSource(BingRequest.UrlBing, null, BingMapType.Roads), "BingMapRoads"));
                mnuBingMapRoads.Checked = true;
                UpdateLinkLabel(LinkLabelType.BING);
            }
            else if (sender == mnuBingMapHybrid)
            {
                mapbox.Map.BackgroundLayer.Add(new TileAsyncLayer(
                    new BruTile.Web.BingTileSource(BingRequest.UrlBing, null, BingMapType.Hybrid), "BingMapHybrid"));
                mnuBingMapHybrid.Checked = true;
                UpdateLinkLabel(LinkLabelType.BING);
            }
            else if (sender == mnuBingMapOS)
            {
                mapbox.Map.BackgroundLayer.Add(new TileAsyncLayer(
                    new BruTile.Web.BingTileSource(
                        new BruTile.Web.BingRequest(BingRequest.UrlBing, null, BingMapType.OS, "517"), null),
                        "BingMapOS"));
                mnuBingMapOS.Checked = true;
                UpdateLinkLabel(LinkLabelType.BING);
            }
            mapbox.Refresh();
        }
    }
}
