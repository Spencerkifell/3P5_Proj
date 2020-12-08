using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3P5_Project_1937291_1923906.Models
{
    class Inventory
    {
        private List<Item> _items;

        public Inventory() { }

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

        public bool LoadItems(string filePath)
        {
            throw new NotImplementedException();
        }

        public bool SaveItems(string destination)
        {
            throw new NotImplementedException();
        }
    }
}
