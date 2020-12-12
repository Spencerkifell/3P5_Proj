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
        public SearchWindow(Inventory _inventory)
        {
            inventory = _inventory;
            hasChanged = false;

            InitializeComponent();

            cmbSupplier.ItemsSource = Inventory.Suppliers;
            cmbCategory.ItemsSource = Enum.GetValues(typeof(Inventory.Category));
        }

        private void cmbSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
