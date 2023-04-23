using System.Net;

namespace FTAnalyzer.Mapping
{
    internal class GoogleClient
    {
        HttpClient Client { get; }

        public GoogleClient()
        {
            HttpClientHandler handler = new()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            };
            Client = new(handler)
            {
                Timeout = new TimeSpan(0, 0, 5)
            };
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) => Client.SendAsync(request);
    }
}
