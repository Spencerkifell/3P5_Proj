using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3P5_Project_1937291_1923906.Models
{
    class Item
    {
        private string _itemName;
        private int _availableQuantity;
        private int _minimumQuanity;
        private string _location;
        private string _supplier;
        private Category _itemCategory;

        public Item() { }
        
        public Item(string name_, int availableQuanity_, int minimumQuantity_, string location_, string supplier_, Category category_)
        {
            ItemName = name_;
            AvailableQuanity = availableQuanity_;
            MinimumQuanity = minimumQuantity_;
            Location = location_;
            Supplier = supplier_;
            ItemCategory = category_;
        }

        public string ItemName
        {
            get { return _itemName; }
            private set { _itemName = value; }
        }

        public int AvailableQuanity
        {
            get { return _availableQuantity; }
            set
            {
                if (value >= 0)
                    _availableQuantity = value;
                else
                    throw new ArgumentException("Value cannot be negative.", "_availableQuanity");
            }
        }

        public int MinimumQuanity
        {
            get { return _minimumQuanity; }
            private set
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

        public Category ItemCategory
        {
            get { return _itemCategory;  }
            set
            {
                //if((int)_itemCategory >= 0 && (int)_itemCategory <= )
                _itemCategory = value;
            }
        }

        public String ItemData
        {
            get { return string.Format($"{ItemName},{AvailableQuanity},{MinimumQuanity},{Location},{ItemCategory}"); }

            set
            {
                string[] lineData = value.Split(',');

                try
                {
                    ItemName = lineData[0];
                    AvailableQuanity = int.Parse(lineData[1]);
                    MinimumQuanity = int.Parse(lineData[2]);
                    Location = lineData[3];

                    ItemCategory = Category.Uncategorized;
                    foreach(Category cat in Enum.GetValues(typeof(Category)))
                    {
                        if (cat.ToString().ToUpper() == lineData[4].ToUpper())
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
            return base.ToString();
        }
    }
}
