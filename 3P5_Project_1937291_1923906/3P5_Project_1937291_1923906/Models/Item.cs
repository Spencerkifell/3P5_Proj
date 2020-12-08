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
        private List<string> _supplier;
        private Category _itemCategory;
        
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
            Cables
        }

        public Item() { }
        
        public Item(int availableQuanity_, int minimumQuantity_, string location_, string supplier_, Category category_)
        {
            AvailableQuanity = availableQuanity_;
            MinimumQuanity = minimumQuantity_;
            Location = location_;
            Supplier.Add(supplier_);
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
            private set
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
            private set { _location = value; }
        }

        public List<string> Supplier
        {
            get { return _supplier; }
            private set { _supplier = value; }
        }

        public Category ItemCategory
        {
            get { return _itemCategory;  }
            private set
            {
                //if((int)_itemCategory >= 0 && (int)_itemCategory <= )
                _itemCategory = value;
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
