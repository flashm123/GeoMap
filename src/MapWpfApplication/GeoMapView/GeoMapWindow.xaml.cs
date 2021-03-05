using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GMap.NET;
using System.Collections;
using GMap.NET.WindowsPresentation;
using System.Web;
using System.Runtime.Serialization.Json;
using MapWpfApplication.Helpers;
using System.Net;
using GMap.NET.MapProviders;
using System.ComponentModel;
using Microsoft.Win32;

namespace MapWpfApplication
{
    /// <summary>
    /// Interaction logic with GeoMapWindow.xaml
    /// </summary>
    public partial class GeoMapWindow : Window
    {
        private readonly GeoMapViewModel geoMapViewModel;

        private List<Place> places;

        public GeoMapWindow()
        {
            InitializeComponent();

            places = new List<Place>();
            geoMapViewModel = new GeoMapViewModel();

            this.DataContext = geoMapViewModel;
        }

        /// <summary>
        /// Set the Map settings
        /// </summary>
        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapProvider = BingMapProvider.Instance;

            // Initialize map:
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;

            mapView.MinZoom = 2;
            mapView.MaxZoom = 17;
            // Whole world zoom
            mapView.Zoom = 3;
            // Lets the map use the mousewheel to zoom
            mapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            // Lets the user drag the map
            mapView.CanDragMap = true;
            // Lets the user drag the map with the left mouse button
            mapView.DragButton = MouseButton.Left;
        }

        /// <summary>
        /// Remove prevoisuly drawed region
        /// </summary>
        private void ClearMap()
        {
            mapView.Markers.Clear();
        }

        /// <summary>
        /// Draw the first region in the places list on the map
        /// TODO: Make opportunity to draw the selected by user region
        /// </summary>
        private void DrawRegion()
        {
            // Declare List for pointlatlang
            List<PointLatLng> pointlatlang = new List<PointLatLng>();

            // Now we draw only the first found region
            // TODO: consider to make a control to display all regions there.
            // Suggest user to choose what region to display on the map.
            foreach (var point in places[0].Points)
            {
                pointlatlang.Add(new PointLatLng(point.Longtittude, point.Lattitude));
            }

            // Declare polygon in gmap
            GMapPolygon polygon = new GMapPolygon(pointlatlang);

            polygon.ZIndex = 1;

            mapView.RegenerateShape(polygon);

            // Setting line style
            (polygon.Shape as Path).Stroke = Brushes.Blue;
            (polygon.Shape as Path).StrokeThickness = 5.5;
            (polygon.Shape as Path).Effect = null;

            // To add polygon in gmap
            mapView.Markers.Add(polygon);
            mapView.Position = new PointLatLng(places[0].Points[0].Longtittude, places[0].Points[0].Lattitude);
        }

        /// <summary>
        /// Handler method for search button click event
        /// </summary>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            places.Clear();
            ClearMap();

            if (tbAddress.Text != string.Empty || !string.IsNullOrWhiteSpace(tbAddress.Text))
            {
                int frequency = 0;

                if (tbFrequency.Text != string.Empty || !string.IsNullOrWhiteSpace(tbFrequency.Text))
                {
                    if (int.TryParse(tbFrequency.Text, out frequency))
                    {
                        if (frequency <= 0)
                        {
                            MessageBox.Show(MessageHelper.FrequencyMoreThanZero);
                            return;
                        }

                        places = geoMapViewModel.GetData(tbAddress.Text, frequency);

                        if (places.Count == 0)
                        {
                            MessageBox.Show(MessageHelper.NoResults);
                            return;
                        }

                        // Check that the first place has more than 1 point. 
                        // Because GMap doesn't have functionality to draw a single point now.
                        if (places[0].Points.Count <= 1)
                        {
                            MessageBox.Show(MessageHelper.FrequencyIsTooBig);
                            return;
                        }

                        DrawRegion();
                    }
                    else
                    {
                        MessageBox.Show(MessageHelper.FrequencyNotCorrect);
                    }
                }
                else
                {
                    MessageBox.Show(MessageHelper.FrequencyNotInput);
                }
            }
            else
            {
                MessageBox.Show(MessageHelper.AddressNotCorrect);
            }
        }

        /// <summary>
        /// Handler method for cancel button click event. Clear map and places. Clear input controls.
        /// </summary>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearMap();
            places.Clear();

            tbAddress.Text = string.Empty;
            tbFrequency.Text = string.Empty;
        }

        /// <summary>
        /// Handler method for save place coordinates to the file.
        /// </summary>
        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            // That means that we have some region displayed on the map,
            // otherwise we don't have anything to save to the file
            if (mapView.Markers.Count != 0)
            {
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = ".txt";
                saveFileDialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

                if (saveFileDialog.ShowDialog() == true)
                {
                    System.IO.File.WriteAllLines(saveFileDialog.FileName, places[0].Points.Select(point => "[" + point.Lattitude + " , " + point.Longtittude + "]"));
                }
            }
            else
            {
                MessageBox.Show(MessageHelper.NoDataOnMap);
            }
        }
    }
}
