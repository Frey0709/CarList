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

namespace CarList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructor fir the form.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            PopulateYears();
            SetDefaults();
        }
        /// <summary>
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
        /// When Reset is clicked, reset stuff.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetClick(object sender, RoutedEventArgs e)
        {
            SetDefaults();
        }
        /// <summary>
        /// Me close form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}