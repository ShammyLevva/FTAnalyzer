using System.Runtime.Serialization;

namespace FTAnalyzer.Mapping
{
    [DataContract]
    public class GeoResponse
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }
        [DataMember(Name = "results")]
        public CResult[] Results { get; set; }

        [DataContract]
        public class CResult
        {
            [DataMember(Name = "types")]
            public string[] Types { get; set; }
            [DataMember(Name = "formatted_address")]
            public string ReturnAddress { get; set; }
            [DataMember(Name = "address_components")]
            public CAddress[] Addresses { get; set; }
            [DataMember(Name = "geometry")]
            public CGeometry Geometry { get; set; }

            [DataContract]
            public class CGeometry
            {
                [DataMember(Name = "location")]
                public CLocation Location { get; set; }
                [DataMember(Name = "viewport")]
                public CViewPort ViewPort { get; set; }

                [DataContract]
                public class CLocation
                {
                    [DataMember(Name = "lat")]
                    public double Lat { get; set; }
                    [DataMember(Name = "lng")]
                    public double Long { get; set; }
                }

                [DataContract]
                public class CViewPort
                {
                    [DataMember(Name = "southwest")]
                    public CLocation SouthWest { get; set; }
                    [DataMember(Name = "northeast")]
                    public CLocation NorthEast { get; set; }

                    public CViewPort()
                    {
                        NorthEast = new CLocation();
                        SouthWest = new CLocation();
                    }
                }
            }

            [DataMember(Name = "partial_match")]
            public bool PartialMatch { get; set; }
        }

        [DataContract]
        public class CAddress
        {
            [DataMember(Name = "types")]
            public string[] Types { get; set; }
            [DataMember(Name = "long_name")]
            public string LongName { get; set; }
            [DataMember(Name = "short_name")]
            public string ShortName { get; set; }
        }
    }
}
