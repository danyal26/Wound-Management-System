using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoundManagementSystem
{
    public class VisitMedication
    {
        public string patientVisitId { get; set; }
        public string medicationId { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }

        public VisitMedication(string patientVisitId, string medicationId, int quantity, double price)
        {
            this.patientVisitId = patientVisitId;
            this.medicationId = medicationId;
            this.quantity = quantity;
            this.price = price;
        }
    }
}
