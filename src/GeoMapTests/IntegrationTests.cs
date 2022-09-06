using MapWpfApplication;
using Moq;
using NUnit.Framework;
using System;

namespace GeoMapTests
{
    public class IntegrationTests
    {
        [TestCase("Red Square", "red_square_data.txt", 10, 23, 5)]
        [TestCase("Red Square", "red_square_data.txt", 10, 1, 100)]
        [TestCase("Madrid", "madrid_data.txt", 9, 102, 17)]
        [TestCase("Madrid", "madrid_data.txt", 9, 348, -5)]
        [TestCase(" ", "empty_data.txt", 0, 0, 3)]
        public void VerifyGeoServiceReturnsCorrectPlacesAndPointsCount(string address, string data, int placesCount, int pointsCount, int frequency)
        {
            // Arrange
            var geoMapViewModel = new GeoMapViewModel();
            var json = System.IO.File.ReadAllText(Environment.CurrentDirectory + "\\TestData\\" + data);
            var getDataHelperMock = new Mock<IGetDataHelper>();

            getDataHelperMock.Setup(osm => osm.GetGeoData(address)).Returns(json);
            geoMapViewModel.getDataHelper = getDataHelperMock.Object;

            // Act
            var places = geoMapViewModel.GetData(address, frequency);

            // Assert
            Assert.AreEqual(placesCount, places.Count);
            Assert.AreEqual(pointsCount, (places.Count == 0) ? 0 : places[0].Points.Count);
        }
    }
}