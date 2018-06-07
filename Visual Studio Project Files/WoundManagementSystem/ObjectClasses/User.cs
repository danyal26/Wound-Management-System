using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoundManagementSystem
{
    public class User
    {
        public string userId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string password { get; set; }
        public string rights { get; set; }
        public string staffId { get; set; }

        public User(string userId, string name, string surname, string password, string rights, string staffId)
        {
            this.userId = userId;
            this.name = name;
            this.surname = surname;
            this.password = password;
            this.rights = rights;
            this.staffId = staffId;
        }
    }
}
