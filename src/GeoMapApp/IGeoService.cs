using System.Collections.Generic;

namespace MapWpfApplication
{
    interface IGeoService
    {
        List<Place> Places
        {
            get;
        }

        void CreateGeoModel(string json);
    }
}
