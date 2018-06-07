using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoundManagementSystem
{
    public class Expense
    {
        public string id { get; set; }
        public string type { get; set; }
        public double amount { get; set; }
        public string notes { get; set; }

        public Expense(string id, string type, double amount, string notes)
        {
            this.id = id;
            this.type = type;
            this.amount = amount;
            this.notes = notes;

        }
    }
}
