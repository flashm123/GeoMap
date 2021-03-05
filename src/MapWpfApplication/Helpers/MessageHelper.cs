using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapWpfApplication
{
    class MessageHelper
    {
        public static string FrequencyMoreThanZero = "Frequency should be more than zero";
        public static string NoResults = "We didn't find anything." + Environment.NewLine + "Please try to change the address to search.";
        public static string FrequencyIsTooBig = "The input frequency is too big," + Environment.NewLine + "Please try to decrease the frequency.";
        public static string FrequencyNotCorrect = "Please input the correct Frequency";
        public static string FrequencyNotInput = "Please input the Frequency";
        public static string AddressNotCorrect = "Please input the correct Address";
        public static string NoDataOnMap = "There is no any data on the map. Please perform search.";
    }
}
