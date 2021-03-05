using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapWpfApplication.Helpers;
using System.Runtime.Serialization.Json;

namespace MapWpfApplication
{
    /// <summary>
    /// This class defines OpenStreetMap service.
    /// </summary>
    class OpenStreetMapService : IGeoService
    {
        private List<Place> places;

        // <summary>
        // The list of places
        // </summary>
        public List<Place> Places
        {
            get
            {
                if (places == null)
                {
                    places = new List<Place>();
                }
                return places;
            }
        }

        // <summary>
        // Gets the points for each region and the region name and keeps them to the place list
        // </summary>
        public void CreateGeoModel(string json)
        {
            var regionRecords = GeoJsonHelper.SplitGeoJsonIntoRegions(json);

            foreach (var regionRecord in regionRecords)
            {
                if (regionRecord.Contains("MultiPolygon"))
                {
                    var region = GetRegion<List<List<List<double[]>>>>(regionRecord);

                    var placeName = region.DisplayName;
                    var points = region.GeoJson
                             .Coordinates
                             .SelectMany(listOfList => listOfList)
                             .SelectMany(list => list)
                             .Select(point => new Point(point[0], point[1]))
                             .ToList<Point>();

                    Places.Add(new Place(placeName, points));
                }
                else
                {
                    if (regionRecord.Contains("Polygon"))
                    {
                        var region = GetRegion<List<List<double[]>>>(regionRecord);

                        var placeName = region.DisplayName;
                        var points = region.GeoJson
                                 .Coordinates[0]
                                 .Select(point => new Point(point[0], point[1]))
                                 .ToList<Point>();

                        Places.Add(new Place(placeName, points));
                    }
                }
            }
        }

        // <summary>
        // Deserializes the json region to the Region object
        // </summary>
        private Region<T> GetRegion<T>(string json)
        {
            var memoryStream = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(json));

            var region = new Region<T>();
            var deserializer = new DataContractJsonSerializer(typeof(Region<T>));

            region = deserializer.ReadObject(memoryStream) as Region<T>;

            return region;
        }
    }
}
