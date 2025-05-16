using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopInventory
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; } // HP / Laptop
        public int Quantity { get; set; }
    }
}