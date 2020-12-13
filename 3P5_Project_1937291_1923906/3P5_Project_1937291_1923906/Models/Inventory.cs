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
        public readonly static string[] Suppliers = new string[]
        {
            "",
            "Costco",
            "Walmart",
            "Best Buy",
            "Amazon",
            "AlieExpress",
            "Canada Computers",
            "Future Shop"
        };

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

        //Provides string representing all the items in the inventory
        public string GeneralReport()
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine("GENERAL REPORT");
            output.AppendLine();

            foreach(Item item in Items)
            {
                output.AppendLine(item.ToString());
            }

            output.AppendLine();
            output.AppendLine($"Total Number of Items in Inventory: {Items.Count}");

            return output.ToString();
        }

        //Returns a string representing all the items in the inventory with available quantity below minimum
        public string ShoppingList()
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine("SHOPPING LIST");
            output.AppendLine();

            int count = 0;
            foreach (Item item in Items)
            {
                if (item.AvailableQuanity < item.MinimumQuanity)
                {
                    output.AppendLine(item.ToString());
                    count++;
                }
            }

            output.AppendLine();
            output.AppendLine($"Total Number of Items in Inventory: {Items.Count}");
            output.AppendLine($"Total Number of Items Below Minimum Quantity: {count}");
            output.AppendLine();

            return output.ToString();
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
            catch
            {
                throw new ArgumentException($"Couldn't load data from file {filePath}");
            }
        }

        public void SaveItems(string destination)
        {
            try
            {
                if (!string.IsNullOrEmpty(destination))
                {
                    StringBuilder data = new StringBuilder();
                    foreach (Item newItem in Items)
                        data.AppendLine(newItem.ItemData);

                    File.WriteAllText(destination, data.ToString());
                }
            }

            catch
            {
                throw new ArgumentException($"Couldn't save to file {destination}");
            }
        }

        // Given a string value, returns a list of items that loosely matches the given string value in the inventory.
        // Returns null if key is an empty/null string
        public List<Item> SearchItems(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                List<Item> newList = new List<Item>();
                foreach (Item item in Items)
                {
                    if (item is Item)
                        newList.Add(item);
                }

                char[] charArray = key.ToUpper().ToCharArray();

                // Go Through each character of the name to search
                for (int i = 0; i < charArray.Length; i++)
                {
                    bool hasRemoved = false;
                    // Go through remaining items in the list
                    for (int k = newList.Count - 1; k >= 0; k--)
                    {
                        // If the item is too small to be compared, keep it
                        if (i >= newList[k].ItemName.Length)
                            continue;

                        // If the demanded character doesn't exist in the item name's specific position, remove it
                        if (charArray[i] != newList[k].ItemName.ToUpper()[i])
                        {
                            newList.RemoveAt(k);
                            hasRemoved = true;
                        }
                    }

                    // If nothing has been removed, no point in looping
                    if (!hasRemoved)
                        break;
                }

                return newList;
            }
            else
            {
                return null;
            }
        }

        // Given a number of item attributes, returns a list of items that matches the given attributes in the inventory.
        // Returns null if none were found
        public List<Item> SearchItems(string txtItemName, int availableQuantity, int minimumQty, string itemLocation, string supplier, Category requestedCategory)
        {
            List<Item> newList = SearchItems(txtItemName);

            
            for (int i = newList.Count - 1; i >= 0; i--)
            {
                if (newList.Count.Equals(0))
                    return null;

                if (minimumQty != newList[i].MinimumQuanity || availableQuantity != newList[i].AvailableQuanity || itemLocation.ToUpper() != newList[i].Location.ToUpper() || supplier.ToUpper() != newList[i].Supplier.ToUpper() || requestedCategory != newList[i].ItemCategory)
                    newList.RemoveAt(i);
            }

            return newList;
        }
    }
}
