using NetTopologySuite.Geometries;
using SharpMap;
using SharpMap.Data;
using SharpMap.Data.Providers;
using SharpMap.Layers;
using SharpMap.Styles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//  http://thydzik.com/thydzikGoogleMap/markerlink.php?text=1&color=5680FC - sets up colour teardrops

namespace FTAnalyzer.Mapping
{
    public class TearDropLayer : VectorLayer
    {
        public FeatureDataTable TearDropLocations { get; private set; }
        public Image Icon { get; private set; }
        
        public static string RED = "Teardrop_Red.png", BLACK = "Teardrop_Black.png", LIGHT_GREEN = "Teardrop_LightGreen.png", GREY = "Grey";

        public TearDropLayer(string title) : base(title)
        {
            TearDropLocations = new FeatureDataTable();
            TearDropLocations.Columns.Add("MapLocation", typeof(MapLocation));
            TearDropLocations.Columns.Add("DisplayFact", typeof(DisplayFact));
            TearDropLocations.Columns.Add("ViewPort", typeof(Envelope));
            TearDropLocations.Columns.Add("Colour", typeof(string));

            GeometryFeatureProvider TearDropLocationGFP = new GeometryFeatureProvider(TearDropLocations);
            this.DataSource = TearDropLocationGFP;

            Dictionary<string, IStyle> styles = new Dictionary<string, IStyle>();
            VectorStyle birth = new VectorStyle
            {
                PointColor = new SolidBrush(Color.Red),
                PointSize = 8,
                Symbol = Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\", RED)),
                SymbolOffset = new PointF(0.0f, -17.0f)
            };
            styles.Add(RED, birth);

            VectorStyle death = new VectorStyle
            {
                PointColor = new SolidBrush(Color.Black),
                PointSize = 8,
                Symbol = Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\", BLACK)),
                SymbolOffset = new PointF(0.0f, -17.0f)
            };
            styles.Add(BLACK, death);

            VectorStyle selected = new VectorStyle
            {
                PointColor = new SolidBrush(Color.LightGreen),
                PointSize = 8,
                Symbol = Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\", LIGHT_GREEN)),
                SymbolOffset = new PointF(0.0f, -17.0f)
            };
            styles.Add(LIGHT_GREEN, selected);

            VectorStyle point = new VectorStyle
            {
                PointColor = new SolidBrush(Color.DarkGray),
                PointSize = 6
            };
            styles.Add(GREY, point);

            this.Theme = new SharpMap.Rendering.Thematics.UniqueValuesTheme<string>("Colour", styles, point);
        }

        public void Clear()
        {
            TearDropLocations.Clear();
        }

        public void AddFeatureDataRows(Individual ind)
        {
            foreach (DisplayFact f in ind.AllGeocodedFacts)
            {
                MapLocation ml = new MapLocation(ind, f.Fact, f.FactDate);
                AddFeatureDataRow(f, ml, GREY);
            }
            if (ind.BirthFact != null)
            {
                MapLocation birth = new MapLocation(ind, ind.BirthFact, ind.BirthDate);
                AddFeatureDataRow(null, birth, RED);
            }
            if (ind.DeathFact != null)
            {
                MapLocation death = new MapLocation(ind, ind.DeathFact, ind.DeathDate);
                AddFeatureDataRow(null, death, BLACK);
            }
        }

        public void AddSelections(DataGridViewSelectedRowCollection rows)
        {
            Clear();
            foreach (DataGridViewRow row in rows)
            {
                DisplayFact dispFact = row.DataBoundItem as DisplayFact;
                MapLocation ml = new MapLocation(dispFact.Ind, dispFact.Fact, dispFact.FactDate);
                AddFeatureDataRow(dispFact, ml, LIGHT_GREEN);
            }
        }

        private FeatureDataRow AddFeatureDataRow(DisplayFact dispfact, MapLocation loc, string colour)
        {
            GeoResponse.CResult.CGeometry.CViewPort vp = loc.Location.ViewPort;
            FeatureDataRow r = TearDropLocations.NewRow();
            r["DisplayFact"] = dispfact;
            r["MapLocation"] = loc;
            r["ViewPort"] = new Envelope(vp.NorthEast.Long, vp.SouthWest.Long, vp.NorthEast.Lat, vp.SouthWest.Lat);
            r["Colour"] = colour;
            r.Geometry = loc.Geometry;
            TearDropLocations.AddRow(r);
            return r;
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    TearDropLocations.Dispose();
                }
            }
            catch (Exception) { }
        }
    }
}
