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
    }
}
