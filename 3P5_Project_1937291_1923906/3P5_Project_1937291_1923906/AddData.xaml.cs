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

namespace _3P5_Project_1937291_1923906
{
    /// <summary>
    /// Interaction logic for AddData.xaml
    /// </summary>
    public partial class AddData : Window
    {
        Inventory inventory;
        public bool hasChanged;

        public AddData(Inventory _inventory)
        {
            inventory = _inventory;
            hasChanged = false;

            InitializeComponent();

            cmbSupplier.ItemsSource = Inventory.Suppliers;
            cmbCategory.ItemsSource = Enum.GetValues(typeof(Inventory.Category));
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string errors = CheckForm();
            if (string.IsNullOrEmpty(errors))
            {
                Item newItem = new Item(txtName.Text, int.Parse(txtAvailableQty.Text), int.Parse(txtMinimumQty.Text), txtLocation.Text, cmbSupplier.Text, (Inventory.Category)cmbCategory.SelectedIndex);
                inventory.Items.Add(newItem);
                hasChanged = true;
                MessageBox.Show("Item Successfully Added.", "S.A Emporium", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                MessageBox.Show($"Couldn't Add item, an error has occured: \n{errors}", "Error Adding an Item", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string CheckForm()
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(txtName.Text))
                errors.AppendLine("Name cannot be empty");
            
            bool isNum = int.TryParse(txtAvailableQty.Text, out int availableQuantity);
            if (isNum)
            {
                if (availableQuantity < 0)
                    errors.AppendLine($"Available Quantity ({txtAvailableQty.Text}) cannot be negative");
            }
            else
            {
                errors.AppendLine($"Available Quantity ({(string.IsNullOrEmpty(txtAvailableQty.Text) ? "Empty" : txtAvailableQty.Text)}) must be an integer");
            }

            isNum = int.TryParse(txtMinimumQty.Text, out int minimumQuantity);
            if (isNum)
            {
                if (minimumQuantity < 1)
                    errors.AppendLine($"Minimum Quantity ({txtMinimumQty.Text}) cannot be below 1");
            }
            else
            {
                errors.AppendLine($"Minimum Quantity ({(string.IsNullOrEmpty(txtMinimumQty.Text) ? "Empty" : txtMinimumQty.Text)}) must be an integer");
            }

            return errors.ToString();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e) => ClearForm();

        private void ClearForm()
        {
            txtName.Text = string.Empty;
            txtAvailableQty.Text = string.Empty;
            txtMinimumQty.Text = string.Empty;
            txtLocation.Text = string.Empty;
            cmbSupplier.Text = string.Empty;
            cmbCategory.SelectedIndex = 10;
        }
    }
}
