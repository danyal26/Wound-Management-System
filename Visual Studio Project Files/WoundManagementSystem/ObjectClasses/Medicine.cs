using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoundManagementSystem
{
    public class Medicine
    {
        public string id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }

        public Medicine(string id, string name, int quantity, double price)
        {
            this.id = id;
            this.name = name;
            this.quantity = quantity;
            this.price = price;
        }
    }

    
}
