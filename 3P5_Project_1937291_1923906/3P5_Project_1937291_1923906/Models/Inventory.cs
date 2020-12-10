using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _3P5_Project_1937291_1923906.Models
{
    public class Inventory
    {
        private List<Item> _items;

        public Inventory() 
        {
            Items = new List<Item>();
        }

        public List<Item> Items
        {
            get { return _items; }
            private set { _items = value; }
        }

        public string GeneralReport()
        {
            throw new NotImplementedException();
        }

        public string ShoppingList()
        {
            throw new NotImplementedException();
        }

        public void LoadItems(string filePath)
        {
            try
            {
                string[] newItems = File.ReadAllLines(filePath);

                foreach(string data in newItems)
                {
                    Item newItem = new Item();
                    newItem.ItemData = data;
                    Items.Add(newItem);
                }
            }

            catch(Exception e)
            {
                throw e;
            }
        }

        public void SaveItems(string destination)
        {
            try
            {
                StringBuilder data = new StringBuilder();
                foreach(Item newItem in Items)
                    data.AppendLine(newItem.ItemData);

                File.WriteAllText(destination, data.ToString());
            }

            catch
            {
                throw new ArgumentException("Couldn't save to file. Try again");
            }
        }
    }
}
