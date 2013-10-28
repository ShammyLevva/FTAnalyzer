using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BruTile.Web;
using System.Globalization;
using BruTile;

namespace FTAnalyzer.Mapping
{
    public class BingOSRequest : BingRequest
    {
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly char _mapType;

        private const string VersionBingMaps = "517";
        public BingOSRequest(string baseUrl, string token, BingMapType mapType)
            : base(baseUrl, token, mapType)
        {
            this._baseUrl = baseUrl;
            this._token = token;
            this._mapType = 'r'; // ToMapTypeChar(mapType);
        }

        public Uri GetUri(TileInfo info)
        {
            string url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}" + "{2}.jpeg?g={4}&productSet=mmOS&token={3}",
                    _baseUrl, _mapType, TileXyToQuadKey(info.Index.Col, info.Index.Row, info.Index.Level), _token, VersionBingMaps);
            return new Uri(url);
        }

        /// <summary>
        /// Converts tile XY coordinates into a QuadKey at a specified level of detail.
        /// </summary>
        /// <param name="tileX">Tile X coordinate.</param>
        /// <param name="tileY">Tile Y coordinate.</param>
        /// <param name="levelOfDetail">Level of detail, from 1 (lowest detail)
        /// to 23 (highest detail).</param>
        /// <returns>A string containing the QuadKey.</returns>
        /// Stole this methode from this nice blog: http://www.silverlightshow.net/items/Virtual-earth-deep-zooming.aspx. PDD.
        private static string TileXyToQuadKey(int tileX, int tileY, int levelOfDetail)
        {
            var quadKey = new StringBuilder();
            for (int i = levelOfDetail; i > 0; i--)
            {
                char digit = '0';
                int mask = 1 << (i - 1);

                if ((tileX & mask) != 0)
                {
                    digit++;
                }

                if ((tileY & mask) != 0)
                {
                    digit++;
                    digit++;
                }
                quadKey.Append(digit);
            }
            return quadKey.ToString();
        }
    }
}
