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
            try
            {
                Item newItem;
                string itemName = string.Empty;
                int availableQty = 0;
                int minimumQty = 0;
                int selectedIndex = cmbCategory.SelectedIndex;
                string location = string.Empty;
                string supplier = string.Empty;

                bool nameTrue = true;
                bool availableTrue = true;
                bool minimumTrue = true;
                bool locationTrue = true;
                bool supplierTrue = true;

                if (txtName.Text != string.Empty) //Can't be null
                    itemName = txtName.Text;
                else
                    nameTrue = false;

                if (!int.TryParse(txtAvailableQty.Text, out availableQty)) //Can't be less than 0
                    availableTrue = false;

                if (!int.TryParse(txtMinimumQty.Text, out minimumQty)) //Can't be less than 1
                    minimumTrue = false;

                if (txtLocation.Text != string.Empty) //Can be null
                    location = txtLocation.Text;
                else
                    locationTrue = false;

                if (cmbSupplier.Text != string.Empty) //Can be null
                    supplier = cmbSupplier.Text;
                else
                    supplierTrue = false;

                if (nameTrue && availableTrue && minimumTrue)
                {
                    newItem = new Item(itemName, availableQty, minimumQty, location, supplier, (Inventory.Category)selectedIndex);
                    inventory.Items.Add(newItem);
                    hasChanged = true;

                    MessageBox.Show("Item Successfully Added.", "S.A Emporium", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Transaction Not Successful\n\n");
                    sb.Append($"Item Name Entered: {nameTrue}\n");
                    sb.Append($"Available Quantity Entered: {availableTrue}\n");
                    sb.Append($"Minimum Quantity Entered: {minimumTrue}\n");
                    sb.Append($"(Optional) Location Entered: {locationTrue}\n");
                    sb.Append($"(Optional) Supplier Entered: {supplierTrue}");
                    sb.Append("\n\nPlease Make Sure To Fill in Everything Required.");
                    MessageBox.Show(sb.ToString(), "S.A Emporium", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
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
