# GeoMap

This application is used to search for locations and display them on the political world map. 
It developed on C #, WPF technology using MVVM pattern and data binding. The app uses .NET 4.0

The map is displayed using GMap.NET version 1.9.0.0.

The user enters the address and the points frequency and clicks "Search". The system sends a GET request to the OSM service and receives json, which contains a set of regions that match the entered request. Application deserializes json to object. 
Next, a list of localities is passed to the View. In my opinion, it is a good idea to give the user an opportunity  to choose which region to display, but at the moment the application displays the very first found region. 

The frequency reduces the number of points returned by the service through the algorithm: we find indices that are not multiples of the frequency, and then remove points with such indices from the list of points.

The map can be scaled with the mouse wheel and moved with the left mouse button.
Points can be saved to a text file by clicking the "Save place to file" button.



Demonstration:
![Moscow](https://user-images.githubusercontent.com/59667317/110163052-9e8be480-7e00-11eb-927b-387b08acf3b0.PNG)



The class diagram:
![image](https://user-images.githubusercontent.com/59667317/110145176-33cfae80-7dea-11eb-9f71-25fe288a45c4.png)
