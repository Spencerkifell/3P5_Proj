using _3P5_Project_1937291_1923906.Models;
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

namespace _3P5_Project_1937291_1923906
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        Inventory inventory;
        public bool hasChanged;
        public List<Item> searchResult;

        public SearchWindow(Inventory _inventory)
        {
            inventory = _inventory;
            hasChanged = false;

            InitializeComponent();

            cmbSupplier.ItemsSource = Inventory.Suppliers;
            cmbCategory.ItemsSource = Enum.GetValues(typeof(Inventory.Category));
        }

        // Returns a string representing all the error messages while checking the form
        // Returns empty if no errors
        private string CheckForm()
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(txtItemName.Text))
                errors.AppendLine("Name cannot be empty");

            bool isNum = int.TryParse(txtAvailableQty.Text, out int availableQuantity);
            if (isNum)
            {
                if (availableQuantity < 0)
                    errors.AppendLine($"Available Quantity ({txtAvailableQty.Text}) cannot be negative");
            }
            else
                errors.AppendLine($"Available Quantity ({(string.IsNullOrEmpty(txtAvailableQty.Text) ? "Empty" : txtAvailableQty.Text)}) must be an integer");

            isNum = int.TryParse(txtMinimumQty.Text, out int minimumQuantity);
            if (isNum)
            {
                if (minimumQuantity < 1)
                    errors.AppendLine($"Minimum Quantity ({txtMinimumQty.Text}) cannot be below 1");
            }
            else
                errors.AppendLine($"Minimum Quantity ({(string.IsNullOrEmpty(txtMinimumQty.Text) ? "Empty" : txtMinimumQty.Text)}) must be an integer");

            return errors.ToString();
        }

        // Searches the inventory for a specific item match
        // Changes dialog result based on if the search gave any items back
        private void btnAdvancedSearch_Click(object sender, RoutedEventArgs e)
        {
            string potentialErrors = CheckForm();
            if (!string.IsNullOrEmpty(potentialErrors))
                MessageBox.Show(potentialErrors, "S.A.T Emporium", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                searchResult = inventory.SearchItems(txtItemName.Text, int.Parse(txtAvailableQty.Text), int.Parse(txtMinimumQty.Text), txtItemLocation.Text, cmbSupplier.SelectedItem.ToString(), (Inventory.Category)cmbCategory.SelectedItem);
                DialogResult = searchResult.Count > 0 ? true : false;
            }
        }
    }
}
