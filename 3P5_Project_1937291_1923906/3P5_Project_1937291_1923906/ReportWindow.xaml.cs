using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using _3P5_Project_1937291_1923906.Models;
using Microsoft.Win32;
using System.IO;

namespace _3P5_Project_1937291_1923906
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        private Inventory inventory;
        public ReportWindow(Inventory inventory_)
        {
            InitializeComponent();
            inventory = inventory_;

            cmbCategories.ItemsSource = Enum.GetValues(typeof(Inventory.Category));
            cmbSuppliers.ItemsSource = Inventory.Suppliers;
            dgItems.ItemsSource = inventory.Items;
        }

        // Prints the inventory's report to a txt file
        private void PrintReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveResultDialog = new SaveFileDialog();
                saveResultDialog.Filter = "txt|*.txt";
                if (saveResultDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveResultDialog.FileName, inventory.GeneralReport());
                    Close();
                }
            }
            catch
            {
                MessageBox.Show("Couldn't save the report, please try again", "General Report", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
