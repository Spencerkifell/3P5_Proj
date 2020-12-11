﻿using System;
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

        public MainWindow()
        {
            InitializeComponent();
            inventory = new Inventory();
            dgItems.ItemsSource = inventory.Items;
            cmbSuppliers.ItemsSource = Inventory.Suppliers;
            cmbCategories.ItemsSource = Enum.GetValues(typeof(Inventory.Category));
        }

        public void SetTitle(string extra)
        {
            Title = $"{BaseTitle} - {extra}";
        }

        //Opens and loads csv file data into inventory
        private void LoadItems_Click(object sender, RoutedEventArgs e)
        {
            //Check if current file is saved
            if (CanLoad())
            {
                Load();
            }
        }

        private void Load()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "csv|*.csv|txt|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                saveLocation = openFileDialog.FileName;

                inventory.Items.Clear();
                inventory.LoadItems(saveLocation);

                dgItems.Items.Refresh();
                hasModifications = false;
                SetTitle(openFileDialog.SafeFileName);
            }
        }

        //Return false if can't load return true if you can load file
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
                    saveResult = OpenSave();

                if (saveResult)
                    SaveData();

                return saveResult;
            }
            return true;
        }

        private void SaveItems_Click(object sender, RoutedEventArgs e)
        {
            //Check if its a new file (NOT DONE)
            if (string.IsNullOrEmpty(saveLocation))
            {
                OpenSave();
            }

            SaveData();
        }

        //Returns false if save window is cancelled, return true if save windown openned a file
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

        private void AddItems_Click(object sender, RoutedEventArgs e)
        {
            AddData addDataWindow = new AddData(inventory);
            addDataWindow.ShowDialog();

            dgItems.Items.Refresh();

            hasModifications = addDataWindow.hasChanged;
        }

        private void RemoveRow_Click(object sender, RoutedEventArgs e)
        {
            List<Item> items = new List<Item>();
            foreach(var item in dgItems.SelectedItems)
            {
                if (item is Item)
                    items.Add(item as Item);
            }

            if(items != null && items.Count > 0)
            {
                foreach(Item item in items)
                {
                    inventory.Items.Remove(item);
                }
                dgItems.Items.Refresh();
                hasModifications = true;
            }
        }

        private void dgItems_CurrentCellEdit(object sender, EventArgs e)
        {
            hasModifications = true;
        }

        private void dgItems_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid != null)
                if (Key.Delete == e.Key)
                    hasModifications = true;
        }

        private void btnAddQuantity_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemoveQuantity_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
