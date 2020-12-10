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
    public enum Category
    {
        Computers,
        Components,
        Monitors,
        Printers,
        Security,
        Cameras,
        Headphones,
        Games,
        Phones,
        Cables,
        Uncategorized
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Inventory inventory;

        string saveLocation = null;
        public MainWindow()
        {
            InitializeComponent();
            inventory = new Inventory();
            dgItems.ItemsSource = inventory.Items;
        }

        //Opens and loads csv file data into inventory
        private void LoadItems_Click(object sender, RoutedEventArgs e)
        {
            //Check if current file is saved (NOT DONE)
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "csv|*.csv|txt|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                saveLocation = openFileDialog.FileName;
                inventory.LoadItems(saveLocation);
                dgItems.Items.Refresh();
            }
        }

        private void SaveItems_Click(object sender, RoutedEventArgs e)
        {
            //Check if its a new file (NOT DONE)
            if (String.IsNullOrEmpty(saveLocation))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "csv|*.csv|txt|*.txt";
                if (saveFileDialog.ShowDialog() == true)
                    saveLocation = saveFileDialog.FileName;
            }

            SaveData();
        }

        private void SaveData()
        {
            try
            {
                inventory.SaveItems(saveLocation);
            }catch(Exception e)
            {
                MessageBox.Show("Couldn't write to file.\nError:\n" + e.Message, "Couldn't Save to File", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
