using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoundManagementSystem
{
    public class Patient
    {
        public string patientId { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string dateOfBirth { get; set; }
        public string sex { get; set; }
        public string address { get; set; }
        public string contact { get; set; }
        public string email { get; set; }
        public string occupation { get; set; }

        public Patient(string patientId, string title, string name, string surname, string dateOfBirth, 
            string sex, string address, string contact, string email, string occupation)
        {
            this.patientId = patientId;
            this.title = title;
            this.name = name;
            this.surname = surname;
            this.dateOfBirth = dateOfBirth;
            this.sex = sex;
            this.address = address;
            this.contact = contact;
            this.email = email;
            this.occupation = occupation;
        }
    }
}
