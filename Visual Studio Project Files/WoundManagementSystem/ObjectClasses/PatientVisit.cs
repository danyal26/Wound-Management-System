using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoundManagementSystem
{
    public class PatientVisit
    {
        public string id { get; set; }
        public string patientId { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string notes { get; set; }
        public string incomeId { get; set; }
        public string afterOpening { get; set; }
        public string actionsTaken { get; set; }
        public string beforeClosing { get; set; }
        public string type { get; set; }

        public PatientVisit(string id, string patientId, string date, string time, 
            string notes, string incomeId, string afterOpening, string actionsTaken,
            string beforeClosing, string type)
        {
            this.id = id;
            this.patientId = patientId;
            this.date = date;
            this.time = time;
            this.notes = notes;
            this.incomeId = incomeId;
            this.afterOpening = afterOpening;
            this.actionsTaken = actionsTaken;
            this.beforeClosing = beforeClosing;
            this.type = type;
        }
    }
}
