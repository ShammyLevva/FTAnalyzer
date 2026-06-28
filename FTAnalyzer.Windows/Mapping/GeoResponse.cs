using Newtonsoft.Json;

namespace FTAnalyzer.Mapping
{
    public class GeoResponse
    {
        [JsonIgnore]
        public string Status { get; set; } = string.Empty;

        [JsonProperty("results")]
        public CResult[] Results { get; set; } = [];

        public class CResult
        {
            [JsonProperty("types")]
            public string[] Types { get; set; } = [];

            [JsonProperty("formattedAddress")]
            public string ReturnAddress { get; set; } = string.Empty;

            [JsonProperty("addressComponents")]
            public CAddress[] Addresses { get; set; } = [];

            [JsonProperty("location")]
            public CGeometry.CLocation Location { get; set; } = new();

            [JsonProperty("viewport")]
            public CGeometry.CViewPort ViewPort { get; set; } = new();

            [JsonProperty("granularity")]
            public string Granularity { get; set; } = string.Empty;

            [JsonProperty("placeId")]
            public string PlaceId { get; set; } = string.Empty;

            [JsonIgnore]
            public bool IsApproximateMatch => string.Equals(Granularity, "APPROXIMATE", StringComparison.OrdinalIgnoreCase);

            [JsonIgnore]
            public CGeometry Geometry => new(this);

            public class CGeometry
            {
                readonly CResult _result;

                public CGeometry() => _result = new CResult();

                internal CGeometry(CResult result) => _result = result;

                public CLocation Location
                {
                    get => _result.Location;
                    set => _result.Location = value;
                }

                public CViewPort ViewPort
                {
                    get => _result.ViewPort;
                    set => _result.ViewPort = value;
                }

                public class CLocation
                {
                    [JsonProperty("latitude")]
                    public double Lat { get; set; }

                    [JsonProperty("longitude")]
                    public double Long { get; set; }
                }

                public class CViewPort
                {
                    [JsonProperty("low")]
                    public CLocation SouthWest { get; set; }

                    [JsonProperty("high")]
                    public CLocation NorthEast { get; set; }

                    public CViewPort()
                    {
                        NorthEast = new CLocation();
                        SouthWest = new CLocation();
                    }
                }
            }
        }

        public class CAddress
        {
            [JsonProperty("types")]
            public string[] Types { get; set; } = [];

            [JsonProperty("longText")]
            public string LongName { get; set; } = string.Empty;

            [JsonProperty("shortText")]
            public string ShortName { get; set; } = string.Empty;
        }
    }
}
