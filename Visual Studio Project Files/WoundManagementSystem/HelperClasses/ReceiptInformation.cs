using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoundManagementSystem
{
    public class ReceiptInformation
    {
        public PatientVisit visit { get; set; }
        public ObservableCollection<Medicine> medList { get; set; }
        public ObservableCollection<Service> serviceList { get; set; }
        public double totalPayment { get; set; }

        public ReceiptInformation(PatientVisit visit, ObservableCollection<Medicine> medList, 
            ObservableCollection<Service> serviceList, double totalPayment)
        {
            this.visit = visit;
            this.medList = medList;
            this.serviceList = serviceList;
            this.totalPayment = totalPayment;
        }
    }
}
