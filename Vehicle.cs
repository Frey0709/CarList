// Author: Claude Joeffrey Aldenson R. De Guzman
// Created: Oct 26, 2025
// Updated: Nov 16, 2025
// Description: Defines a generic vehicle to be inherited by different vehicle types.
using System;

namespace CarList
{
    internal abstract class Vehicle
    {
        // Declared and initialized Variables
        protected string make = String.Empty;
        protected string model = String.Empty;
        protected int year = DateTime.Now.Year;
        protected decimal price = 0.0M;
        protected bool isNew = false;

        private static int count = 0;
        protected int id = count;

        /// <summary>
        /// Default constructor
        /// Kyle code
        /// </summary>
        public Vehicle()
        {
            count++;
            id = count;
        }

        /// <summary>
        /// Vehicle make
        /// Kyle code
        /// </summary>
        public string Make { get { return make; } set { make = value; } }

        /// <summary>
        /// Vehicle model
        /// Kyle code
        /// </summary>
        public string Model { get { return model; } set { model = value; } }

        /// <summary>
        /// Vehicle year
        /// Kyle code
        /// </summary>
        public int Year { get { return year; } set { year = value; } }

        /// <summary>
        /// Vehicle price
        /// Kyle code
        /// </summary>
        public decimal Price { get { return price; } set { price = value; } }

        /// <summary>
        /// Is the vehicle new
        /// Kyle code
        /// </summary>
        public bool IsNew { get { return isNew; } set { isNew = value; } }

        /// <summary>
        /// Vehicle unique ID
        /// Kyle code
        /// </summary>
        public int IdentificationNumber { get { return id; } }

        /// <summary>
        /// Total number of vehicles
        /// Kyle code
        /// </summary>
        public static int Count { get { return count; } }

        /// <summary>
        /// Vehicle type for inheritance differentiation
        /// Kyle code
        /// </summary>
        public abstract string Type { get; }

        /// <summary>
        /// Returns a string version of the Vehicle
        /// Kyle code
        /// </summary>
        public override string ToString()
        {
            return Year + " " + Make + " " + Model;
        }

        /// <summary>
        /// Vehicle honk method, can be overridden
        /// Kyle code
        /// </summary>
        public virtual string Honk()
        {
            return "Beep beep!";
        }
    }
}