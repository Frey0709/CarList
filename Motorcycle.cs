// Author: Claude Joeffrey Aldenson R. De Guzman
// Created: Oct 26, 2025
// Description: Defines a Motorcycle class that inherits from Vehicle.

using System;

namespace CarList
{
    internal class Motorcycle : Vehicle
    {
        /// <summary>
        /// Default constructor
        /// Kyle code
        /// </summary>
        public Motorcycle() : base() { }

        /// <summary>
        /// Parametrized constructor
        /// Kyle code
        /// </summary>
        public Motorcycle(string make, string model, int year, decimal price, bool isNew)
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
        public override string Type => "Motorcycle";

        /// <summary>
        /// Motorcycle-specific honk
        /// Kyle code
        /// </summary>
        public override string Honk()
        {
            return "Motorcycle goes vroom!";
        }
    }
}
