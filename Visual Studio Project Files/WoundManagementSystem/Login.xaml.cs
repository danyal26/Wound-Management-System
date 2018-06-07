using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MySql.Data.MySqlClient;
using Windows.UI.Popups;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WoundManagementSystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }

        private bool userExists()
        {
            bool exists = false;

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM staff WHERE StaffID = '" + TxtStaffID.Text + "' AND " +
                "Password = '" + TxtPassword.Password.ToString() + "';";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            int counter = 0;

            while (reader.Read())
            {
                string staffId = reader["StaffID"].ToString();
                string stat = reader["Status"].ToString();
                string title = reader["Title"].ToString();
                string name = reader["Name"].ToString();
                string surname = reader["Surname"].ToString();
                string dob = reader["DateOfBirth"].ToString();
                string sex = reader["Sex"].ToString();
                string address = reader["Address"].ToString();
                string tellNo = reader["TelNo"].ToString();
                string email = reader["Email"].ToString();
                string rate = reader["Rate"].ToString();
                string occurence = reader["Occurrence"].ToString();
                string password = reader["Password"].ToString();
                string rights = reader["Rights"].ToString();

                Staff staff = new Staff(staffId, stat, title, name, surname, dob, sex,
                    address, tellNo, rate, email, occurence, password, rights);

                BtnLogin.Tag = staff;

                counter++;
            }

            if (counter == 1)
                exists = true;

            conn.Close();
            return exists;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            performLogin();
        }

        private void BtnErrorClose_Click(object sender, RoutedEventArgs e)
        {
            PanelError.Visibility = Visibility.Collapsed;
        }

        private void performLogin()
        {
            if (userExists())
            {
                Staff newUser = (Staff)BtnLogin.Tag;
                MyGlobals.userRights = newUser.rights;
                this.Frame.Navigate(typeof(Dashboard), newUser);
            }
            else
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Your ID and Password do not match!";
            }
        }
    }
}
