using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using MapWpfApplication.DeserializationModels;

namespace MapWpfApplication
{
    // <summary>
    /// This class defines Region model to deserialize data from json
    /// </summary>
    [DataContract]
    class Region<T>
    {
        [DataMember(Name = "display_name")]
        public string DisplayName
        {
            get;
            set;
        }

        [DataMember(Name = "geojson")]
        public GeoJson<T> GeoJson
        {
            get;
            set;
        }

    }
}
