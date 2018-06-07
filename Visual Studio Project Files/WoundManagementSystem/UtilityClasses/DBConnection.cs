using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WoundManagementSystem
{
    public static class DBConnection
    {
        public static MySqlConnectionStringBuilder Connect()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.UserID = "root";
            builder.Password = "root";
            builder.Database = "woundmanagementsystem";
            builder.SslMode = MySqlSslMode.None;
            builder.CharacterSet = "utf8";

            return builder;
        }
    }
}
