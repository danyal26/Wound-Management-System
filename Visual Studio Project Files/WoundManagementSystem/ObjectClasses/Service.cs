using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoundManagementSystem
{
    public class Service
    {
        public string id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public double price { get; set; }

        public Service(string id, string type, string name, double price)
        {
            this.id = id;
            this.type = type;
            this.name = name;
            this.price = price;
        }
    }
}
