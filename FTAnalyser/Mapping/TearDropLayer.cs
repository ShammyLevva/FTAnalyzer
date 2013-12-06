using GeoAPI.Geometries;
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

namespace FTAnalyzer.Mapping
{
    public class TearDropLayer : IDisposable
    {
        public FeatureDataTable TearDropLocations { get; private set; }
        public Image Icon { get; private set; }
        private VectorLayer tearDropsLayer;
        private Map map;

        public static readonly string RED = "Teardrop_Red.png", BLACK = "Teardrop_Black.png", LIGHT_GREEN = "Teardrop_LightGreen.png", GREY = "Grey";

        public TearDropLayer(Map map, bool isQueryEnabled)
        {
            this.map = map;
            SetupMap(isQueryEnabled);
        }

        private void SetupMap(bool isQueryEnabled)
        {
            TearDropLocations = new FeatureDataTable();
            TearDropLocations.Columns.Add("MapLocation", typeof(MapLocation));
            TearDropLocations.Columns.Add("ViewPort", typeof(Envelope));
            TearDropLocations.Columns.Add("Colour", typeof(string));

            GeometryFeatureProvider TearDropLocationGFP = new GeometryFeatureProvider(TearDropLocations);

            tearDropsLayer = new VectorLayer("Teardrops");
            tearDropsLayer.DataSource = TearDropLocationGFP;
            
            Dictionary<string, IStyle> styles = new Dictionary<string, IStyle>();
            VectorStyle birth = new VectorStyle();
            birth.PointColor = new SolidBrush(Color.Red);
            birth.PointSize = 10;
            birth.Symbol = Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\", RED));
            birth.SymbolOffset = new PointF(0.0f, -17.0f);
            styles.Add(RED, birth);

            VectorStyle death = new VectorStyle();
            death.PointColor = new SolidBrush(Color.Black);
            death.PointSize = 10;
            death.Symbol = Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\", BLACK));
            death.SymbolOffset = new PointF(0.0f, -17.0f);
            styles.Add(BLACK, death);

            VectorStyle selected = new VectorStyle();
            selected.PointColor = new SolidBrush(Color.LightGreen);
            selected.PointSize = 10;
            selected.Symbol = Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\", LIGHT_GREEN));
            selected.SymbolOffset = new PointF(0.0f, -17.0f);
            styles.Add(LIGHT_GREEN, selected);

            VectorStyle point = new VectorStyle();
            point.PointColor = new SolidBrush(Color.DarkGray);
            point.PointSize = 8;
            styles.Add(GREY, point);

            tearDropsLayer.Theme = new SharpMap.Rendering.Thematics.UniqueValuesTheme<string>("Colour", styles, point);
            tearDropsLayer.IsQueryEnabled = isQueryEnabled;
            map.VariableLayers.Add(tearDropsLayer);
        }

        public void Clear()
        {
            TearDropLocations.Clear();
        }

        public void AddFeatureDataRows(Individual ind)
        {
            foreach(DisplayFact f in ind.AllGeocodedFacts)
            {
                MapLocation ml = new MapLocation(ind, f.Fact, f.FactDate);
                AddFeatureDataRow(ml, GREY);
            }
            if (ind.BirthFact != null)
            {
                MapLocation birth = new MapLocation(ind, ind.BirthFact, ind.BirthDate);
                AddFeatureDataRow(birth, RED);
            }
            if (ind.DeathFact != null)
            {
                MapLocation death = new MapLocation(ind, ind.DeathFact, ind.DeathDate);
                AddFeatureDataRow(death, BLACK);
            }
        }

        public void AddSelections(DataGridViewSelectedRowCollection rows)
        {
            Clear();
            foreach (DataGridViewRow row in rows)
            {
                DisplayFact dispFact = row.DataBoundItem as DisplayFact;
                MapLocation ml = new MapLocation(dispFact.Ind, dispFact.Fact, dispFact.FactDate);
                AddFeatureDataRow(ml, LIGHT_GREEN);
            }
        }
        
        private FeatureDataRow AddFeatureDataRow(MapLocation loc, string colour)
        {
            GeoResponse.CResult.CGeometry.CViewPort vp = loc.Location.ViewPort;
            FeatureDataRow r = TearDropLocations.NewRow();
            r["MapLocation"] = loc;
            r["ViewPort"] = new Envelope(vp.NorthEast.Long, vp.SouthWest.Long, vp.NorthEast.Lat, vp.SouthWest.Lat);
            r["Colour"] = colour;
            r.Geometry = loc.Geometry;
            TearDropLocations.AddRow(r);
            return r;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                TearDropLocations.Dispose();
                tearDropsLayer.Dispose();
                map.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
