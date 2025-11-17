// Author: Claude Joeffrey Aldenson R. De Guzman
// Created: Oct 26, 2025
// Updated: Nov 16, 2025
// Description: Defines a Car class that inherits from Vehicle.

using System;

namespace CarList
{
    internal class Car : Vehicle
    {
        /// <summary>
        /// Default constructor
        /// Kyle code
        /// </summary>
        public Car() : base() { }

        /// <summary>
        /// Parametrized constructor
        /// Kyle code
        /// </summary>
        public Car(string make, string model, int year, decimal price, bool isNew)
        {
            Make = make;
            Model = model;
            Year = year;
            Price = price;
            IsNew = isNew;
        }

        /// <summary>
        /// Vehicle type
        /// Kyle code
        /// </summary>
        public override string Type => "Car";

        /// <summary>
        /// Car-specific honk
        /// Kyle code
        /// </summary>
        public override string Honk()
        {
            return "Car goes beep!";
        }
    }
}