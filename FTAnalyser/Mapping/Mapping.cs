using FTAnalyzer.Forms;
using SharpMap.Forms;
using SharpMap.Rendering.Decoration.ScaleBar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FTAnalyzer.Mapping
{
    public class MapHelper
    {
        private static MapHelper instance;
        private FamilyTree ft = FamilyTree.Instance;
        
        private MapHelper()
        {
        }

        public static MapHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MapHelper();
                }
                return instance;
            }
        }

        public void SetScaleBar(MapBox mapBox1)
        {
            if (Properties.MappingSettings.Default.HideScaleBar)
            {
                if (mapBox1.Map.Decorations.Count > 0)
                    mapBox1.Map.Decorations.RemoveAt(0);
            }
            else
            {
                ScaleBar scalebar = new ScaleBar();
                scalebar.BackgroundColor = Color.White;
                scalebar.RoundedEdges = true;
                mapBox1.Map.Decorations.Add(scalebar);
            }
            mapBox1.Refresh();
        }

        public void mnuHideScaleBar_Click(ToolStripMenuItem mnuHideScaleBar, MapBox mapBox1)
        {
            Properties.MappingSettings.Default.HideScaleBar = mnuHideScaleBar.Checked;
            Properties.MappingSettings.Default.Save();
            SetScaleBar(mapBox1);
        }

        public void CheckIfGeocodingNeeded(Form form)
        {
            int notsearched = (FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.NOT_SEARCHED)) - 1);
            if (notsearched > 0 && !ft.Geocoding)
            {
                DialogResult res = MessageBox.Show("You have " + notsearched + " places with no map location do you want to search Google for the locations?",
                                                   "Geocode Locations", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    form.Cursor = Cursors.WaitCursor;
                    StartGeocoding();
                    form.Cursor = Cursors.Default;
                }
            }
        }

        public void StartGeocoding()
        {
            if (!ft.Geocoding) // don't geocode if another geocode session in progress
            {
                GeocodeLocations geo = new GeocodeLocations();
                MainForm.DisposeDuplicateForms(geo);
                geo.Show();
                geo.StartGeoCoding(false);
                geo.BringToFront();
                geo.Focus();
            }
        }

    }
}
