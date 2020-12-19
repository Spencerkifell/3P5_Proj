using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3P5_Project_1937291_1923906.Models;

namespace _3P5_Project_1937291_1923906.Models
{
    // Class that stores and modifies an item's attributes (quantity, name, etc)
    public class Item
    {
        private const string NO_SUPPLIER_MESSAGE = "";
        private const string NO_LOCATION_MESSAGE = "";

        private string _itemName;
        private int _availableQuantity;
        private int _minimumQuanity;
        private string _location;
        private string _supplier;
        private Inventory.Category _itemCategory;

        public Item() { }
        
        public Item(string name_, int availableQuanity_, int minimumQuantity_, string location_, string supplier_, Inventory.Category category_)
        {
            ItemName = name_;
            AvailableQuantity = availableQuanity_;
            MinimumQuantity = minimumQuantity_;
            Location = location_;
            Supplier = supplier_;
            ItemCategory = category_;
        }

        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }

        public int AvailableQuantity
        {
            get { return _availableQuantity; }
            set
            {
                if (value >= 0)
                    _availableQuantity = value;
                else
                    _availableQuantity = 0;
            }
        }

        public int MinimumQuantity
        {
            get { return _minimumQuanity; }
            set
            {
                if (value >= 1)
                    _minimumQuanity = value;
                else
                    throw new ArgumentException("Value cannot be less than 1", "_minimumQuantity");
            }
        }

        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public string Supplier
        {
            get { return _supplier; }
            set { _supplier = value; }
        }

        public Inventory.Category ItemCategory
        {
            get { return _itemCategory;  }
            set
            {
                _itemCategory = value;
            }
        }

        public String ItemData
        {
            get { return string.Format($"{ItemName},{AvailableQuantity},{MinimumQuantity},{(string.IsNullOrEmpty(Location) ? NO_LOCATION_MESSAGE : Location)},{(string.IsNullOrEmpty(Supplier) ? NO_SUPPLIER_MESSAGE: Supplier)},{ItemCategory}"); }

            set
            {
                string[] lineData = value.Split(',');

                try
                {
                    ItemName = lineData[0];
                    AvailableQuantity = int.Parse(lineData[1]);
                    MinimumQuantity = int.Parse(lineData[2]);
                    Location = string.IsNullOrEmpty(lineData[3]) ? NO_LOCATION_MESSAGE : lineData[3].ToUpper() == NO_LOCATION_MESSAGE.ToUpper() ? null : lineData[3];

                    Supplier = NO_SUPPLIER_MESSAGE;
                    foreach(string supplier in Inventory.Suppliers)
                    {
                        if(supplier.ToUpper() == lineData[4].ToUpper())
                        {
                            Supplier = supplier;
                            break;
                        }
                    }

                    ItemCategory = Inventory.Category.Uncategorized;
                    foreach(Inventory.Category cat in Enum.GetValues(typeof(Inventory.Category)))
                    {
                        if (cat.ToString().ToUpper() == lineData[5].ToUpper())
                        {
                            ItemCategory = cat;
                            break;
                        }
                    }

                }catch
                {
                    throw new ArgumentException("CSV data was improper. Please fix data before loading.");
                }
            }
        }

        public override string ToString()
        {
            return $"Name: {ItemName, -20} Available Quantity: {AvailableQuantity, -6} Minimum Quantity: {MinimumQuantity, -6}";
        }
    }
}
