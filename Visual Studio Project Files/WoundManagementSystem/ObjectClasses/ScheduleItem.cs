using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoundManagementSystem
{
    public class ScheduleItem
    {
        public string id { get; set; }
        public string date { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string staffID { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string contact { get; set; }

        public ScheduleItem(string id, string date, string startTime, string endTime, string staffID,
            string name, string surname, string contact)
        {
            this.id = id;
            this.date = date;
            this.startTime = startTime;
            this.endTime = endTime;
            this.staffID = staffID;
            this.name = name;
            this.surname = surname;
            this.contact = contact;
        }
    }
}
