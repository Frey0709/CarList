using CarList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarList
{
    internal class Motorcycle : Vehicle
    {
        private bool hasSidecar;
        public Motorcycle() : base()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Motorcycle"/> class with specified details.
        /// </summary>
        /// <param name="make">The manufacturer of the motorcycle.</param>
        /// <param name="model">The model name of the motorcycle.</param>
        /// <param name="year">The year the motorcycle was manufactured.</param>
        /// <param name="price">The price of the motorcycle.</param>
        /// <param name="isNew"><see langword="true"/> if the motorcycle is new; otherwise, <see langword="false"/>.</param>
        /// <param name="hasSidecar"><see langword="true"/> if the motorcycle has a sidecar; otherwise, <see langword="false"/>.</param>
        public Motorcycle(string make, string model, int year, decimal price, bool isNew, bool hasSidecar)
            : base()
        {
            Make = make;
            Model = model;
            Year = year;
            Price = price;
            IsNew = isNew;
            this.hasSidecar = hasSidecar;
        }
        /// <summary>
        /// Gets or sets a value indicating whether the vehicle has a sidecar.
        /// </summary>
        public bool HasSidecar
        {
            get
            {
                return hasSidecar;
            }
            set
            {
                hasSidecar = value;
            }
        }
        /// <summary>
        /// Emits a honking sound to signal or alert others.
        /// </summary>
        /// <remarks>This method outputs a predefined honking sound to the console. It can be used in
        /// scenarios where an audible alert is needed.</remarks>
        public void Honk()
        {
            Console.WriteLine("Beep Beep!");
        }
    }
}
