using System;

namespace Project.Core.Utilities
{
    public static class TemperatureConverter
    {
        /// <summary>
        /// Converts Fahrenheit to Celsius.
        /// </summary>
        /// <param name="fahrenheit">Temperature in Fahrenheit.</param>
        /// <returns>Temperature in Celsius.</returns>
        public static double ConvertFahrenheitToCelsius(double fahrenheit)
        {
            var celsius = (fahrenheit - 32) * 5 / 9;
            return Math.Round(celsius, 2);
        }
    }
}
