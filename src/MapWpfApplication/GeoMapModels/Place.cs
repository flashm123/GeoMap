using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapWpfApplication
{
    /// <summary>
    /// This class defines Places model.
    /// Contains the place name and the points to pass them to View to display on the map
    /// </summary>
    class Place
    {
        public Place(string name, List<Point> points)
        {
            Name = name;
            Points = points;
        }

        /// <summary>
        /// The name of the place
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The list of coordinates of the place
        /// </summary>
        public List<Point> Points
        {
            get;
            set;
        }

        ~Place()
        {
        }
    }
}
