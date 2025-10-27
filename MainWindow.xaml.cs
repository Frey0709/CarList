// Author: Claude Joeffrey Aldenson R. De Guzman
// Created: Oct 26, 2025
// Description: Backend of the Car Inventory application, that will handle events and methods for user inputs and displaying them.

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;

namespace CarList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// List to store all cars
        /// </summary>
        private List<Car> carList = new List<Car>();
        /// <summary>
        /// Kyle code
        /// Constructor fir the form.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            PopulateYears();
            SetDefaults();
        }
        /// <summary>
        /// Kyle code
        /// Populates the year combo box with the last 50 years.
        /// </summary>
        public void PopulateYears()
        {
            var currentYear = DateTime.Now.Year;

            for (int year = currentYear; year >= currentYear - 50; year--)
            {
                comboYear.Items.Add(year);
            }
        }
        /// <summary>
        /// Kyle code
        /// Set all form controls back to their default state.
        /// </summary>
        public void SetDefaults()
        {
            comboMake.SelectedIndex = 0;
            textModel.Clear();
            comboYear.SelectedIndex = 0;
            textPrice.Clear();
            checkIsNew.IsChecked = false;


            listViewCars.SelectedIndex = -1;

            comboMake.Focus();
        }
        /// <summary>
        /// Kyle code
        /// When Reset is clicked, reset stuff.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetClick(object sender, RoutedEventArgs e)
        {
            SetDefaults();
        }
        /// <summary>
        /// Kyle code
        /// Me close form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Updates the list of cars when clicked, by adding a new car or updating an existing car, refreshing list and resetting the form
        /// Visual Studio Intellisense
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterClick(object sender, RoutedEventArgs e)
        {
            // Validate the input if false then exit the method
            if (!ValidateInputs(out string make, out string model, out int year, out decimal price, out bool isNew))
                return;
            
            // If no car is selected then Create a new car
            if (listViewCars.SelectedIndex == -1)
            {
                Car newCar = new Car(make, model, year, price, isNew);
                carList.Add(newCar);
                textOutput.Text = "Car added successfully.";
            }
            else
            {
                Car selectedCar = carList[listViewCars.SelectedIndex];
                selectedCar.Make = make;
                selectedCar.Model = model;
                selectedCar.Year = year;
                selectedCar.Price = price;
                selectedCar.IsNew = isNew;
                textOutput.Text = "Car updated successfully.";
            }
            RefreshList();
            SetDefaults();
        }
        /// <summary>
        /// Refreshes the form to list the new cars
        /// </summary>
        private void RefreshList()
        {
            listViewCars.Items.Clear();
            foreach (Car c in carList)
            {
                listViewCars.Items.Add(c);
            }
        }

        /// <summary>
        /// Handles Selection changes and populates the form with the car details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void CarSelect(object sender, SelectionChangedEventArgs e)
        {
            if (listViewCars.SelectedIndex != -1)
            {
                Car selectedCar = carList[listViewCars.SelectedIndex];
                comboMake.Text = selectedCar.Make;
                textModel.Text = selectedCar.Model;
                comboYear.Text = selectedCar.Year.ToString();
                textPrice.Text = selectedCar.Price.ToString();
                checkIsNew.IsChecked = selectedCar.IsNew;
            }
        }

        /// <summary>
        /// Validates the inputs of the user
        /// </summary>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="year"></param>
        /// <param name="price"></param>
        /// <param name="isNew"></param>
        /// <returns></returns>
        private bool ValidateInputs(out string make, out string model, out int year, out decimal price, out bool isNew)
        {
            make = comboMake.Text;
            model = textModel.Text;
            isNew = checkIsNew.IsChecked == true;

            textOutput.Text = "";

            if (string.IsNullOrWhiteSpace(make))
            {
                textOutput.Text += "Please select a car.\n";
                year = 0; price = 0; return false;
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                textOutput.Text += "Please enter a valid model.\n";
                year = 0; price = 0; return false;
            }

            if (comboYear.SelectedItem == null || !int.TryParse(comboYear.SelectedItem.ToString(), out year))
            {
                textOutput.Text += "Please select a valid year.\n";
                year = 0; price = 0; return false;
            }

            if (!decimal.TryParse(textPrice.Text, NumberStyles.Currency | NumberStyles.Number, CultureInfo.CurrentCulture, out price))
            {
                textOutput.Text += "Please enter a valid price.\n";
                year = 0; price = 0; return false;
            }
            return true;
        }
    }
}