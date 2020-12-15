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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using _3P5_Project_1937291_1923906.Models;

namespace _3P5_Project_1937291_1923906
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string BaseTitle = "S.A.T Emporium";

        Inventory inventory;

        string saveLocation = null;
        bool hasModifications = false;

        List<Item> searchResults;

        public MainWindow()
        {
            InitializeComponent();
            inventory = new Inventory();
            
            dgItems.ItemsSource = inventory.Items;
            cmbSuppliers.ItemsSource = Inventory.Suppliers;
            cmbCategories.ItemsSource = Enum.GetValues(typeof(Inventory.Category));
        }

        // Sets the program's title
        public void SetTitle(string extra)
        {
            Title = $"{BaseTitle} - {extra}";
        }

        // Button event to load a file into the inventory
        private void LoadItems_Click(object sender, RoutedEventArgs e)
        {
            // Check if current file is saved
            if (CanLoad())
                Load();
        }

        //Opens the file dialog to load a file
        private void Load()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "csv|*.csv|txt|*.txt";
                if (openFileDialog.ShowDialog() == true)
                {
                    saveLocation = openFileDialog.FileName;

                    inventory.Items.Clear();
                    inventory.LoadItems(saveLocation);

                    dgItems.ItemsSource = inventory.Items;
                    dgItems.Items.Refresh();

                    searchResults = null;
                    hasModifications = false;
                    SetTitle(openFileDialog.SafeFileName);
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.Message, "Couldn't Load File", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Return false if can't load return true if you can load file
        private bool CanLoad()
        {
            if (hasModifications)
            {
                bool saveResult = false;
                MessageBoxResult res = MessageBox.Show("Content has not been saved yet, would you like to save?", "Would you like to save?", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                if (res == MessageBoxResult.No)
                    return true;
                if (res == MessageBoxResult.Cancel)
                    return false;
                if (res == MessageBoxResult.Yes)
                {
                    if (saveLocation != null)
                        SaveData();
                    else
                        saveResult = OpenSave();
                }

                if (saveResult)
                    SaveData();

                return saveResult;
            }
            return true;
        }

        // Button event that saves the inventory into a chosen file
        private void SaveItemsAs_Click(object sender, RoutedEventArgs e)
        {
            if (OpenSave())
                SaveData();
        }

        // Button event that saves the inventory into a file
        private void SaveItems_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(saveLocation))
            {
                if (OpenSave())
                    SaveData();
            }
            else
                SaveData();
        }

        // Returns true if user chose a save location, returns false otherwise
        private bool OpenSave()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "csv|*.csv|txt|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                saveLocation = saveFileDialog.FileName;
                hasModifications = false;
                SetTitle(saveFileDialog.SafeFileName);
                return true;
            }

            return false;
        }

        // Saves the inventory into a given file
        private void SaveData()
        {
            try
            {
                inventory.SaveItems(saveLocation);
                hasModifications = false;
            }
            catch(Exception e)
            {
                MessageBox.Show("Couldn't write to file.\nError:\n" + e.Message, "Couldn't Save to File", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Button event that allows users to add one or multiple new items to the inventory
        private void AddItems_Click(object sender, RoutedEventArgs e)
        {
            AddData addDataWindow = new AddData(inventory);
            addDataWindow.ShowDialog();

            SearchInventory();

            hasModifications = addDataWindow.hasChanged;
        }

        // Button event that removes one or multiple items from the inventory
        private void RemoveRow_Click(object sender, RoutedEventArgs e)
        {
            List<Item> items = new List<Item>();
            foreach(var item in dgItems.SelectedItems)
            {
                items.Add(item as Item);
            }

            if(items != null && items.Count > 0)
            {
                if (searchResults != null)
                {
                    foreach (Item item in items)
                    {
                        searchResults.Remove(item);
                        inventory.Items.Remove(item);
                    }
                }
                else
                {
                    foreach (Item item in items)
                    {
                        inventory.Items.Remove(item);
                    }
                }

                dgItems.Items.Refresh();
                hasModifications = true;
            }
        }

        // Datagrid's cell event that makes sure modifications are saved
        private void dgItems_CurrentCellEdit(object sender, EventArgs e)
        {
            hasModifications = true;
        }

        // Button event that adds 1 to a row's available quantity
        private void btnAddQuantity_Click(object sender, RoutedEventArgs e)
        {
            List<Item> items = new List<Item>();
            foreach (Item item in dgItems.SelectedItems)
            {
                if (item is Item)
                    items.Add(item as Item);
            }

            if (items != null && items.Count > 0)
            {
                foreach (Item item in items)
                {
                    item.AvailableQuanity += 1;
                }

                dgItems.Items.Refresh();
                hasModifications = true;
            }
        }

        // Button event that removes 1 on a row's available quantity
        private void btnRemoveQuantity_Click(object sender, RoutedEventArgs e)
        {
            List<Item> items = new List<Item>();
            foreach (Item item in dgItems.SelectedItems)
            {
                if (item is Item)
                    items.Add(item as Item);
            }

            if (items != null && items.Count > 0)
            {
                foreach (Item item in items)
                {
                    item.AvailableQuanity -= 1;
                }

                dgItems.Items.Refresh();
                hasModifications = true;
            }
        }

        // Window close event that asks if user wants to save their modifications before leaving
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!CanLoad())
                e.Cancel = true;
        }

        // Menu Item event that generates the inventory's general report and asks if the user wants to save it to a file
        private void GenerateGeneralReport_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow reportWindow = new ReportWindow(inventory);
            reportWindow.ShowDialog();
        }

        // Menu Item event that generates the inventory's shopping list and asks if the user wants to save it to a file
        private void GenerateShoppingList_Click(object sender, RoutedEventArgs e)
        {
            ShoppingListWindow shoppingWindow = new ShoppingListWindow(inventory);
            shoppingWindow.ShowDialog();
        }

        private void AdvancedSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow addSearchWindow = new SearchWindow(inventory);
            if (addSearchWindow.ShowDialog() == true)
            {
                searchResults = addSearchWindow.searchResult;
                dgItems.ItemsSource = searchResults;
                dgItems.Items.Refresh();
            }
        }

        // On each key press, search in the inventory for precise item name and display matches in the datagrid
        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchInventory();
        }

        private void SearchInventory()
        {
            searchResults = inventory.SearchItems(txtSearch.Text);

            if (searchResults != null)
                dgItems.ItemsSource = searchResults;
            else
                dgItems.ItemsSource = inventory.Items;

            dgItems.Items.Refresh();
        }

        private void CancelSearch_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = string.Empty;

            searchResults = null;
            dgItems.ItemsSource = inventory.Items;
            dgItems.Items.Refresh();
        }
    }
}
