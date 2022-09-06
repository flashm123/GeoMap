using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace MapWpfApplication
{
    // <summary>
    // Help to get json with geo data using OSM API
    // </summary>
    public class OpenStreetMapDataHelper : IGetDataHelper
    {
        private static string getUrlTemplate = "https://nominatim.openstreetmap.org/search?q={0}&format=json&polygon_geojson=1";

        // <summary>
        // Perform GET request to OSM service
        // </summary>
        public string GetGeoData(string address)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(getUrlTemplate, address));
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            request.UserAgent = "Mozilla/5.0";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}
