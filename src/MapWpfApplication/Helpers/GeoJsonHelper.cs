using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapWpfApplication.Helpers
{
    class GeoJsonHelper
    {
        // <summary>
        // Removes the start bracket "[" and the end bracket "]"
        // </summary>
        private static string ModifyGeoJson(string json)
        {
            var newJson = new StringBuilder(json);
            newJson = newJson.Remove(0, 1);
            newJson = newJson.Remove(newJson.Length - 1, 1);

            return newJson.ToString();
        }

        // <summary>
        // Splits regions json into the region records
        // </summary>
        // TODO: use Regex to split the json and leave the separator
        public static string[] SplitGeoJsonIntoRegions(string json)
        {
            string separator = "}}";
            string[] regions = ModifyGeoJson(json)
                              .Split(new string[] { "}}," }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < regions.Length - 1; i++)
            {
                if (!regions[i].Contains(separator))
                {
                    regions[i] += separator;
                }
            }

            return regions;
        }
    }
}
