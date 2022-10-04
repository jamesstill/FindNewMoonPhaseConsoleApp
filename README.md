# FindNewMoonPhaseConsoleApp

.NET Core 6 console app showing how to find the instant (UTC) of all new moons in a date range. Based on Meeus' algorithm and Chapront's ELP-2000/82 theory with polynomial expressions for ∆T (Delta T) from the NASA eclipse web site. This is a companion project to the [SolarEclipseConsoleApp](https://github.com/jamesstill/SolarEclipseConsoleApp) where new moon dates are hard-coded in the [NewMoonData class](https://github.com/jamesstill/SolarEclipseConsoleApp/blob/master/SolarEclipseConsoleApp/NewMoonData.cs) but needn't be if this algorithm were used.

## Authors

[James Still](http://www.squarewidget.com)

## License

This project is licensed under the MIT License.

## Acknowledgments

* Jean Meeus, [Astronomical Algorithms, 2nd Ed. (1998)](https://shopatsky.com/products/astronomical-algorithms-2nd-edition)
* [NASA Eclipse Site - Heliophysics Science Division](https://eclipse.gsfc.nasa.gov/SEhelp/deltatpoly2004.html)
