// Author: Claude Joeffrey Aldenson R. De Guzman | Kyle Chapman
// Created: Oct 26, 2025
// Updated: Nov 16, 2025
// Description: Backend of the Car Inventory application, that handles events and methods for user inputs and displaying them.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CarList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // List to store all vehicles
        private List<Vehicle> vehicleList = new List<Vehicle>();
        private const int YEAR_RANGE = 50;

        public MainWindow()
        {
            InitializeComponent();
            PopulateYears();
            SetDefaults();
            UpdateStatus("Program opened.");
        }

        /// <summary>
        /// Populates the year combo box with the last 50 years.
        /// </summary>
        public void PopulateYears()
        {
            int currentYear = DateTime.Now.Year;
            for (int year = currentYear; year >= currentYear - YEAR_RANGE; year--)
            {
                comboYear.Items.Add(year);
            }
        }

        /// <summary>
        /// Sets all form controls back to default state.
        /// </summary>
        public void SetDefaults()
        {
            comboVehicleType.SelectedIndex = 0; // Car selected by default
            comboMake.SelectedIndex = 0;
            textModel.Clear();
            comboYear.SelectedIndex = 0;
            textPrice.Clear();
            checkIsNew.IsChecked = false;

            listViewCars.SelectedIndex = -1;

            UnHighlight(comboMake);
            UnHighlight(textModel);
            UnHighlight(comboYear);
            UnHighlight(textPrice);

            comboMake.Focus();
        }

        /// <summary>
        /// Resets the form when Reset button clicked.
        /// </summary>
        private void ResetClick(object sender, RoutedEventArgs e)
        {
            SetDefaults();
            UpdateStatus("Form reset.");
        }

        /// <summary>
        /// Exits the program when Exit button clicked.
        /// </summary>
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            UpdateStatus("Program closed.");
            Close();
        }

        /// <summary>
        /// Adds or updates a vehicle when Enter button clicked.
        /// </summary>
        private void EnterClick(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs(out string make, out string model, out int year, out decimal price, out bool isNew))
                return;

            try
            {
                Vehicle vehicle;

                // Determine vehicle type from combo box
                string type = comboVehicleType.Text;
                if (listViewCars.SelectedIndex == -1)
                {
                    if (type == "Car")
                        vehicle = new Car(make, model, year, price, isNew);
                    else
                        vehicle = new Motorcycle(make, model, year, price, isNew);

                    vehicleList.Add(vehicle);
                    textOutput.Text = $"{type} added successfully.";
                    UpdateStatus($"New {type} added.");
                }
                else
                {
                    vehicle = vehicleList[listViewCars.SelectedIndex];
                    vehicle.Make = make;
                    vehicle.Model = model;
                    vehicle.Year = year;
                    vehicle.Price = price;
                    vehicle.IsNew = isNew;
                    textOutput.Text = $"{vehicle.Type} updated successfully.";
                    UpdateStatus($"{vehicle.Type} updated.");
                }

                RefreshList();
                UpdateStatistics();
                SetDefaults();
            }
            catch (ArgumentNullException ex)
            {
                textOutput.Text = ex.Message;
                ErrorHighlight(textModel);
                UpdateStatus("Error: Model cannot be empty.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                textOutput.Text = ex.Message;
                ErrorHighlight(textPrice);
                UpdateStatus("Error: Price cannot be negative.");
            }
            catch (Exception ex)
            {
                textOutput.Text = $"Unexpected error: {ex.Message}";
                UpdateStatus("Error: Unexpected issue occurred.");
            }
        }

        /// <summary>
        /// Refreshes the ListView of vehicles.
        /// </summary>
        private void RefreshList()
        {
            listViewCars.Items.Clear();
            foreach (Vehicle v in vehicleList)
            {
                listViewCars.Items.Add(v);
            }
        }

        /// <summary>
        /// Populates form controls when a vehicle is selected from the list.
        /// </summary>
        private void CarSelect(object sender, SelectionChangedEventArgs e)
        {
            if (listViewCars.SelectedIndex != -1)
            {
                Vehicle selectedVehicle = vehicleList[listViewCars.SelectedIndex];
                comboVehicleType.Text = selectedVehicle.Type;
                comboMake.Text = selectedVehicle.Make;
                textModel.Text = selectedVehicle.Model;
                comboYear.Text = selectedVehicle.Year.ToString();
                textPrice.Text = selectedVehicle.Price.ToString();
                checkIsNew.IsChecked = selectedVehicle.IsNew;

                UpdateStatus($"{selectedVehicle.Type} selected from list.");
            }
        }

        /// <summary>
        /// Validates user inputs for vehicle properties.
        /// </summary>
        private bool ValidateInputs(out string make, out string model, out int year, out decimal price, out bool isNew)
        {
            make = comboMake.Text;
            model = textModel.Text;
            isNew = checkIsNew.IsChecked == true;

            textOutput.Text = "";

            if (string.IsNullOrWhiteSpace(make))
            {
                textOutput.Text += "Please select a car make.\n";
                ErrorHighlight(comboMake);
                year = 0;
                price = 0;
                return false;
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                textOutput.Text += "Please enter a valid model.\n";
                ErrorHighlight(textModel);
                year = 0;
                price = 0;
                return false;
            }

            if (comboYear.SelectedItem == null || !int.TryParse(comboYear.SelectedItem.ToString(), out year))
            {
                textOutput.Text += "Please select a valid year.\n";
                ErrorHighlight(comboYear);
                year = 0;
                price = 0;
                return false;
            }

            if (!decimal.TryParse(textPrice.Text, NumberStyles.Currency | NumberStyles.Number, CultureInfo.CurrentCulture, out price))
            {
                textOutput.Text += "Please enter a valid price.\n";
                ErrorHighlight(textPrice);
                year = 0;
                price = 0;
                return false;
            }

            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
            }

            return true;
        }


        /// <summary>
        /// Highlights a control to indicate an error.
        /// </summary>
        private void ErrorHighlight(Control controlInError)
        {
            controlInError.BorderBrush = Brushes.Red;
            controlInError.Background = Brushes.MistyRose;
            controlInError.Focus();

            if (controlInError is TextBox tb)
            {
                tb.SelectAll();
            }
        }

        /// <summary>
        /// Removes highlight from a control.
        /// </summary>
        private void UnHighlight(Control controlToClear)
        {
            controlToClear.BorderBrush = Brushes.Gray;
            controlToClear.Background = Brushes.White;
        }

        /// <summary>
        /// Updates statistics labels.
        /// </summary>
        private void UpdateStatistics()
        {
            int totalVehicles = vehicleList.Count;
            decimal totalPrice = vehicleList.Sum(v => v.Price);
            decimal averagePrice = totalVehicles > 0 ? totalPrice / totalVehicles : 0;

            labelTotalVehicles.Content = $"Total Vehicles: {totalVehicles}";
            labelTotalPrice.Content = $"Total Price: {totalPrice:C}";
            labelAveragePrice.Content = $"Average Price: {averagePrice:C}";
        }

        /// <summary>
        /// Updates the status bar message.
        /// </summary>
        private void UpdateStatus(string message)
        {
            statusMessage.Text = $"{DateTime.Now:T} - {message}";
        }

        /// <summary>
        /// Handles tab selection changes to update the status or statistics.
        /// </summary>
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabControlMain.SelectedItem is TabItem selectedTab)
            {
                switch (selectedTab.Header.ToString())
                {
                    case "Vehicle List":
                        UpdateStatus("Vehicle List viewed.");
                        break;
                    case "Statistics":
                        UpdateStatistics();
                        UpdateStatus("Statistics viewed.");
                        break;
                    default:
                        UpdateStatus("Add Vehicles tab selected.");
                        break;
                }
            }
        }
    }
}