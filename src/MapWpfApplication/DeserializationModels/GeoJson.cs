using System.Runtime.Serialization;

namespace MapWpfApplication.DeserializationModels
{
    // <summary>
    /// This class defines model for points and the Type of the region's figure:
    /// Polygon, MultiPolygon, PointString, LineString, MultiLineString
    /// </summary>
    [DataContract]
    class GeoJson<T>
    {
        [DataMember(Name = "type")]
        public string Type
        {
            get;
            set;
        }

        [DataMember(Name = "coordinates")]
        public T Coordinates
        {
            get;
            set;
        }
    }
}
