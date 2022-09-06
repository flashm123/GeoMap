namespace MapWpfApplication
{
    /// <summary>
    /// This struct defines Point model.
    /// </summary>
    public struct Point
    {
        public double Lattitude
        {
            get;
            set;
        }

        public double Longtittude
        {
            get;
            set;
        }

        public Point(double lattitude, double longtittude)
            : this()
        {
            Lattitude = lattitude;
            Longtittude = longtittude;
        }
    }
}
