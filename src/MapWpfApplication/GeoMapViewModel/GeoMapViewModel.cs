using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MapWpfApplication
{
    /// <summary>
    /// Interaction logic with GeoMapViewModel
    /// </summary>
    class GeoMapViewModel : INotifyPropertyChanged
    {
        private readonly IGeoService geoService;

        private string address;

        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                this.OnPropertyChanged("Address");
            }
        }

        public GeoMapViewModel()
        {
            geoService = new OpenStreetMapService();
        }

        /// <summary>
        /// Gets regions received by OSM and transform them into the model objects, reduce points by frequency
        /// </summary>
        public List<Place> GetData(string address, int frequency)
        {
            var jsonGeoData = HttpHelper.GetGeoData(address);

            geoService.CreateGeoModel(jsonGeoData);

            // if the place list count equals to zero then we don't find anything by the provided query
            if (geoService.Places.Count > 0)
            {
                // Assign Address property to the place name to setup full Address name to the UI.
                // So, the address that is input by user will be changed by the found place name (bounding)
                Address = geoService.Places[0].Name;
                ReducePoints(geoService.Places, frequency);
            }

            return geoService.Places;
        }

        /// <summary>
        /// Removes some points according to the frequency
        /// e.g: frequency = 3, so the method will remove points with indexes: [0, 1, 3, 4 ... , ]
        /// </summary>
        private void ReducePoints(List<Place> places, int frequency)
        {
            var indexesToRemove = new List<int>();

            foreach (var place in places)
            {
                indexesToRemove.Clear();

                for (int i = 0; i < place.Points.Count; i++)
                {
                    if ((i + 1) % frequency != 0)
                    {
                        indexesToRemove.Add(i);
                    }
                }

                foreach (var index in indexesToRemove.OrderByDescending(index => index))
                {
                    place.Points.RemoveAt(index);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// This method is called by the Set accessor of Address property to update the binding control.
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
    }
}