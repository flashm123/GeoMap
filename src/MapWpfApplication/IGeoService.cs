using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
