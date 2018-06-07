using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoundManagementSystem
{
    public class Appointment
    {
        public string appointmentId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string contact { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string type { get; set; }
        public string notes { get; set; }
        public string patientId { get; set; }
        

        public Appointment(string appointmentId, string name, string surname, string contact, string date, string time, string type, string notes, string patientId)
        {
            this.appointmentId = appointmentId;
            this.name = name;
            this.surname = surname;
            this.contact = contact;
            this.date = date;
            this.time = time;
            this.type = type;
            this.notes = notes;
            this.patientId = patientId;
        }

        
        
    }
}
