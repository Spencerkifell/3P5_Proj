using System;
using System.Collections.Generic;
using System.IO;
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

namespace _3P5_Project_1937291_1923906
{
    /// <summary>
    /// Interaction logic for ShoppingListWindow.xaml
    /// </summary>
    public partial class ShoppingListWindow : Window
    {
        private Inventory inventory;
        public ShoppingListWindow(Inventory inventory_)
        {
            InitializeComponent();
            inventory = inventory_;

            cmbCategories.ItemsSource = Enum.GetValues(typeof(Inventory.Category));
            cmbSuppliers.ItemsSource = Inventory.Suppliers;
            dgItems.ItemsSource = inventory.GetShoppingListItems();
        }

        private void PrintShopping_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveResultDialog = new SaveFileDialog();
                saveResultDialog.Filter = "txt|*.txt";
                if (saveResultDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveResultDialog.FileName, inventory.ShoppingList());
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
