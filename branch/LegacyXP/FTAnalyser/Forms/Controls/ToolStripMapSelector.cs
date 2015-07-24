﻿using System;
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
using System.IO;

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
            string mapPreference = Application.UserAppDataRegistry.GetValue("Default Map Background", "mnuGoogleMap").ToString();
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
            this.mnuGoogleMap = new ToolStripMenuItem();
            this.mnuGoogleSatellite = new ToolStripMenuItem();
            this.mnuOpenStreetMap = new ToolStripMenuItem();
            this.mnuBingMapAerial = new ToolStripMenuItem();
            this.mnuBingMapRoads = new ToolStripMenuItem();
            this.mnuBingMapHybrid = new ToolStripMenuItem();
            this.mnuBingMapOS = new ToolStripMenuItem();
            this.mnuNLSOSHistoric = new ToolStripMenuItem();

            // Setup map selector menu
            this.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.DropDownItems.AddRange(new ToolStripItem[] {
            this.mnuGoogleMap,
            this.mnuGoogleSatellite,
            this.mnuOpenStreetMap,
            this.mnuBingMapAerial,
            this.mnuBingMapRoads,
            this.mnuBingMapHybrid,
            this.mnuBingMapOS,
            this.mnuNLSOSHistoric});
            this.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Name = "mnuMapStyle";
            this.Size = new System.Drawing.Size(71, 22);
            this.Text = "Map style";
            // 
            // mnuGoogleMap
            // 
            this.mnuGoogleMap.CheckOnClick = true;
            this.mnuGoogleMap.Name = "mnuGoogleMap";
            this.mnuGoogleMap.Size = new System.Drawing.Size(164, 22);
            this.mnuGoogleMap.Text = "Google Map";
            this.mnuGoogleMap.Tag = LinkLabelType.GOOGLE;
            this.mnuGoogleMap.Click += new System.EventHandler(this._Click);
            // 
            // mnuGoogleSatellite
            // 
            this.mnuGoogleSatellite.CheckOnClick = true;
            this.mnuGoogleSatellite.Name = "mnuGoogleSatellite";
            this.mnuGoogleSatellite.Size = new System.Drawing.Size(164, 22);
            this.mnuGoogleSatellite.Text = "Google Satellite";
            this.mnuGoogleSatellite.Tag = LinkLabelType.GOOGLE;
            this.mnuGoogleSatellite.Click += new System.EventHandler(this._Click);
            this.mnuGoogleSatellite.Visible = false;
            // 
            // mnuOpenStreetMap
            // 
            this.mnuOpenStreetMap.CheckOnClick = true;
            this.mnuOpenStreetMap.Name = "mnuOpenStreetMap";
            this.mnuOpenStreetMap.Size = new System.Drawing.Size(164, 22);
            this.mnuOpenStreetMap.Text = "Open Street Map";
            this.mnuOpenStreetMap.Tag = LinkLabelType.OSM;
            this.mnuOpenStreetMap.Click += new System.EventHandler(this._Click);
            // 
            // mnuBingMapAerial
            // 
            this.mnuBingMapAerial.CheckOnClick = true;
            this.mnuBingMapAerial.Name = "mnuBingMapAerial";
            this.mnuBingMapAerial.Size = new System.Drawing.Size(164, 22);
            this.mnuBingMapAerial.Text = "Aerial Bing Map";
            this.mnuBingMapAerial.Tag = LinkLabelType.BING;
            this.mnuBingMapAerial.Click += new System.EventHandler(this._Click);
            // 
            // mnuBingMapRoads
            // 
            this.mnuBingMapRoads.CheckOnClick = true;
            this.mnuBingMapRoads.Name = "mnuBingMapRoads";
            this.mnuBingMapRoads.Size = new System.Drawing.Size(164, 22);
            this.mnuBingMapRoads.Text = "Roads Bing Map";
            this.mnuBingMapRoads.Tag = LinkLabelType.BING;
            this.mnuBingMapRoads.Click += new System.EventHandler(this._Click);
            // 
            // mnuBingMapHybrid
            // 
            this.mnuBingMapHybrid.CheckOnClick = true;
            this.mnuBingMapHybrid.Name = "mnuBingMapHybrid";
            this.mnuBingMapHybrid.Size = new System.Drawing.Size(164, 22);
            this.mnuBingMapHybrid.Text = "Hybrid Bing Map";
            this.mnuBingMapHybrid.Tag = LinkLabelType.BING;
            this.mnuBingMapHybrid.Click += new System.EventHandler(this._Click);
            // 
            // mnuBingMapOS
            // 
            this.mnuBingMapOS.CheckOnClick = true;
            this.mnuBingMapOS.Name = "mnuBingMapOS";
            this.mnuBingMapOS.Size = new System.Drawing.Size(164, 22);
            this.mnuBingMapOS.Text = "OS Bing Map (UK)";
            this.mnuBingMapOS.Tag = LinkLabelType.BING;
            this.mnuBingMapOS.Click += new System.EventHandler(this._Click);
            // 
            // mnuNLSOSHistoric
            // 
            this.mnuNLSOSHistoric.CheckOnClick = true;
            this.mnuNLSOSHistoric.Name = "mnuNLSOSHistoric";
            this.mnuNLSOSHistoric.Size = new System.Drawing.Size(164, 22);
            this.mnuNLSOSHistoric.Text = "NLS OS 1920-1947 (UK)";
            this.mnuNLSOSHistoric.Tag = LinkLabelType.NLS;
            this.mnuNLSOSHistoric.Click += new System.EventHandler(this._Click);
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
            foreach (ToolStripMenuItem menu in this.DropDownItems)
                menu.Checked = false;
            if(mapbox.Map.BackgroundLayer.Count > 0)
                mapbox.Map.BackgroundLayer.RemoveAt(0);
            if (sender == mnuGoogleMap)
            {
                mapbox.Map.BackgroundLayer.Add(new TileAsyncLayer(
                        new GoogleTileSource(GoogleMapType.GoogleMap), "GoogleMap",
                        new Color(), true, Path.Combine(Path.GetTempPath(), "GoogleTileCache")));
                mnuGoogleMap.Checked = true;
                UpdateLinkLabel(LinkLabelType.GOOGLE);
            }
            else if (sender == mnuGoogleSatellite)
            {
                mapbox.Map.BackgroundLayer.Add(new TileAsyncLayer(
                        new GoogleTileSource(GoogleMapType.GoogleSatellite), "GoogleSatellite",
                        new Color(), true, Path.Combine(Path.GetTempPath(), "GoogleTileCache")));
                mnuGoogleSatellite.Checked = true;
                UpdateLinkLabel(LinkLabelType.GOOGLE);
                mnuGoogleSatellite.Visible = false;
            }
            else if (sender == mnuOpenStreetMap)
            {
                mapbox.Map.BackgroundLayer.Add(new TileAsyncLayer(
                    new OsmTileSource(), "OpenStreetMap",
                        new Color(), true, Path.Combine(Path.GetTempPath(), "OSMTileCache")));
                mnuOpenStreetMap.Checked = true;
                UpdateLinkLabel(LinkLabelType.OSM);
            }
            else if (sender == mnuBingMapAerial)
            {
                mapbox.Map.BackgroundLayer.Add(new TileAsyncLayer(
                    new BingTileSource(BingRequest.UrlBing, null, BingMapType.Aerial), "BingMapAerial",
                        new Color(), true, Path.Combine(Path.GetTempPath(), "BingTileCache")));
                mnuBingMapAerial.Checked = true;
                UpdateLinkLabel(LinkLabelType.BING);
            }
            else if (sender == mnuBingMapRoads)
            {
                mapbox.Map.BackgroundLayer.Add(new TileAsyncLayer(
                    new BingTileSource(BingRequest.UrlBing, null, BingMapType.Roads), "BingMapRoads",
                        new Color(), true, Path.Combine(Path.GetTempPath(), "BingTileCache")));
                mnuBingMapRoads.Checked = true;
                UpdateLinkLabel(LinkLabelType.BING);
            }
            else if (sender == mnuBingMapHybrid)
            {
                mapbox.Map.BackgroundLayer.Add(new TileAsyncLayer(
                    new BingTileSource(BingRequest.UrlBing, null, BingMapType.Hybrid), "BingMapHybrid",
                        new Color(), true, Path.Combine(Path.GetTempPath(), "BingTileCache")));
                mnuBingMapHybrid.Checked = true;
                UpdateLinkLabel(LinkLabelType.BING);
            }
            else if (sender == mnuBingMapOS)
            {
                mapbox.Map.BackgroundLayer.Add(new TileAsyncLayer(
                    new BingTileSource(new BingRequest(BingRequest.UrlBing, null, BingMapType.OS), null), "BingMapOS",
                        new Color(), true, Path.Combine(Path.GetTempPath(), "BingTileCache")));
                mnuBingMapOS.Checked = true;
                UpdateLinkLabel(LinkLabelType.BING);
            }
            else if (sender == mnuNLSOSHistoric)
            {
                mapbox.Map.BackgroundLayer.Add(new TileAsyncLayer(
                    new OsmTileSource(new NLSRequest(), null), "NLSOSHistoric",
                        new Color(), true, Path.Combine(Path.GetTempPath(), "OSMTileCache")));
                mnuNLSOSHistoric.Checked = true;
                UpdateLinkLabel(LinkLabelType.NLS);
            }
            Application.UserAppDataRegistry.SetValue("Default Map Background", ((ToolStripMenuItem)sender).Name);
            mapbox.Refresh();
        }

        public BruTile.TileSchema Schema
        {
            get 
            {
                //BruTile.TileLayer layer = mapbox.Map.BackgroundLayer[0];
                return null;
            }
        }
    }
}
