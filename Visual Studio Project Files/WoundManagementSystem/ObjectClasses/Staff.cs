using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoundManagementSystem
{
    public class Staff
    {
        public string staffID { get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string dob { get; set; }
        public string sex { get; set; }
        public string address { get; set; }
        public string contact { get; set; }
        public string rate { get; set; }
        public string email { get; set; }
        public string occurence { get; set; }
        public string rights { get; set; }
        public string password { get; set; }


        public Staff(string staffID, string status, string title, string name, 
            string surname, string dob, string sex, string address, string contact, 
            string rate, string email, string occurence, string password, string rights)
        {
            this.staffID = staffID;
            this.status = status;
            this.title = title;
            this.name = name;
            this.surname = surname;
            this.dob = dob;
            this.sex = sex;
            this.address = address;
            this.contact = contact;
            this.rate = rate;
            this.email = email;
            this.occurence = occurence;
            this.password = password;
            this.rights = rights;
        }
    }
}
