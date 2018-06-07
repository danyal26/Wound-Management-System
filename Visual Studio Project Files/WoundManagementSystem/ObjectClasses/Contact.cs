using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoundManagementSystem
{
    public class Contact
    {
        public string contactId { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }

        public Contact(string contactId, string name, string phone, string email)
        {
            this.contactId = contactId;
            this.name = name;
            this.phone = phone;
            this.email = email;
        }
    }
}
