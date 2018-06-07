using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WoundManagementSystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContactsPage : Page
    {
        private ObservableCollection<Contact> contactList = new ObservableCollection<Contact>();
        private ObservableCollection<Contact> filteredList = new ObservableCollection<Contact>();

        public ContactsPage()
        {
            this.InitializeComponent();
            getList();

            PanelAddModify.Visibility = Visibility.Collapsed;
            PanelDetails.Visibility = Visibility.Visible;
            BtnCancel.Visibility = Visibility.Visible;
            NoSelectionPanel.Visibility = Visibility.Visible;
        }

        private void getList()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT* FROM contacts";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string id = reader["ContactID"].ToString();
                string name = reader["Name"].ToString();
                string phone = reader["Phone"].ToString();
                string email = reader["Email"].ToString();


                Contact contact = new Contact(id, name, phone, email);

                contactList.Add(contact);
            }

            conn.Close();
        }

        private void removeContact(Contact contact)
        {
            contactList.Remove(contact);

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"DELETE FROM contacts WHERE ContactID = '{contact.contactId}';";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void BtnToggleNav_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Dashboard));
        }

        private void BtnAddContact_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Add Contact";

            PanelDetails.Visibility = Visibility.Collapsed;
            PanelAddModify.Visibility = Visibility.Visible;
            BtnCancel.Visibility = Visibility.Collapsed;
            NoSelectionPanel.Visibility = Visibility.Collapsed;
            BtnModifyContact.Visibility = Visibility.Collapsed;
            ContactsListView.IsEnabled = false;
            BtnAddContact.IsEnabled = false;
            BtnAdd.Visibility = Visibility.Visible;
            TxtSearch.IsEnabled = false;
        }

        private void BtnModify_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Modify Contact";

            PanelDetails.Visibility = Visibility.Collapsed;
            PanelAddModify.Visibility = Visibility.Visible;
            BtnCancel.Visibility = Visibility.Collapsed;
            NoSelectionPanel.Visibility = Visibility.Collapsed;

            BtnAdd.Visibility = Visibility.Collapsed;
            BtnModifyContact.Visibility = Visibility.Visible;

            Contact contact = (Contact)ContactsListView.SelectedItem;

            TxtName.Text = contact.name;
            TxtContact.Text = contact.phone;
            TxtEmail.Text = contact.email;

            ContactsListView.IsEnabled = false;
            BtnAddContact.IsEnabled = false;
            TxtSearch.IsEnabled = false;
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            // Dialog 2
            PanelDialog2.Visibility = Visibility.Visible;
            TxtDialogMessage2.Text = "Are you sure you want to remove this contact?";
            Overlay.Visibility = Visibility.Visible;

            //String message = "Are you sure you want to remove the contact?";
            //CustomTwoButtonDialog removeConfirmDialog = new CustomTwoButtonDialog("Remove Contact?", "No", "Yes", message);

            //ContentDialogResult result = await removeConfirmDialog.ShowAsync();

            //if (result == ContentDialogResult.Primary)
            //{
            //    removeContact((Contact)ContactsListView.SelectedItem);
            //}

            //if (ContactsListView.SelectedItem != null)
            //{
            //    PanelDetails.Visibility = Visibility.Visible;
            //    NoSelectionPanel.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    PanelDetails.Visibility = Visibility.Collapsed;
            //    NoSelectionPanel.Visibility = Visibility.Visible;
            //}


        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Contact contact = (Contact)e.ClickedItem;

            LblName.Text = contact.name;
            LblID.Text = contact.contactId;
            LblContact.Text = contact.phone;
            LblEmail.Text = contact.email;

            PanelDetails.Visibility = Visibility.Visible;
            PanelAddModify.Visibility = Visibility.Collapsed;
            BtnCancel.Visibility = Visibility.Visible;
            NoSelectionPanel.Visibility = Visibility.Collapsed;
        }

        private void BtnAddModifyCancel_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Contacts";

            if (ContactsListView.SelectedItem != null)
            {
                PanelDetails.Visibility = Visibility.Visible;
                NoSelectionPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                PanelDetails.Visibility = Visibility.Collapsed;
                NoSelectionPanel.Visibility = Visibility.Visible;
            }


            PanelAddModify.Visibility = Visibility.Collapsed;

            BtnCancel.Visibility = Visibility.Visible;

            clearFormValues();

            ContactsListView.IsEnabled = true;
            BtnAddContact.IsEnabled = true;
            TxtSearch.IsEnabled = true;

            PanelError.Visibility = Visibility.Collapsed;
        }

        private bool validateFields()
        {
            bool numberValidity = true;

            if (!long.TryParse(TxtContact.Text, out long price))
            {
                numberValidity = false;
            }

            if (TxtName.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please enter a name.";
                return false;
            }
            else if (TxtContact.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please enter a contact.";
                return false;
            }
            else if (numberValidity == false)
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Contact can only contain numbers.";
                return false;
            }
            else if (TxtEmail.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please an email address.";
                return false;
            }
            else
            {
                PanelError.Visibility = Visibility.Collapsed;
                return true;
            }
        }

        private void insertContact(Contact contact)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"INSERT INTO contacts VALUES('{contact.contactId}','{contact.name}','{contact.phone}','{contact.email}')";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (validateFields())
            {
                Contact contact = new Contact(IDManager.getNextContactId(), TxtName.Text, TxtContact.Text, TxtEmail.Text);
                insertContact(contact);
                clearFormValues();
                //CustomMessageDialog.createDialog("Contact Added", "OK", "A new contact has been added.");

                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "A new contact has been added.";
                Overlay.Visibility = Visibility.Visible;

                PanelAddModify.Visibility = Visibility.Collapsed;
                BtnCancel.Visibility = Visibility.Visible;

                ContactsListView.IsEnabled = true;
                BtnAddContact.IsEnabled = true;
                TxtSearch.IsEnabled = true;

                contactList.Add(contact);

                LblTitle.Text = "Contacts";

                if (ContactsListView.SelectedItem != null)
                {
                    PanelDetails.Visibility = Visibility.Visible;
                    NoSelectionPanel.Visibility = Visibility.Collapsed;
                }
                else
                {
                    PanelDetails.Visibility = Visibility.Collapsed;
                    NoSelectionPanel.Visibility = Visibility.Visible;
                }
            }
        }

        private void BtnModifyContact_Click(object sender, RoutedEventArgs e)
        {

            Contact contact = (Contact)ContactsListView.SelectedItem;

            if (validateFields())
            {
                contact.name = TxtName.Text;
                contact.phone = TxtContact.Text;
                contact.email = TxtEmail.Text;

                modifyContact(contact);

                clearFormValues();

                //CustomMessageDialog.createDialog("Contact Modified", "OK", "Contact has been modified.");

                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "Contact has been modified.";
                Overlay.Visibility = Visibility.Visible;

                PanelDetails.Visibility = Visibility.Collapsed;
                PanelAddModify.Visibility = Visibility.Collapsed;
                NoSelectionPanel.Visibility = Visibility.Visible;
                BtnCancel.Visibility = Visibility.Visible;

                ContactsListView.IsEnabled = true;
                BtnAddContact.IsEnabled = true;
                TxtSearch.IsEnabled = true;

                LblTitle.Text = "Contacts";
            }
        }

        private void modifyContact(Contact contact)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = $"Update contacts SET Name='{contact.name}', Phone='{contact.phone}'," +
                $"Email='{contact.email}' WHERE ContactID = '{contact.contactId}';";


            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            int index = -1;

            foreach (var cont in contactList)
            {
                if (cont.contactId.Equals(contact.contactId))
                {
                    index = contactList.IndexOf(cont);
                }
            }

            if (index >= 0)
            {
                contactList[index] = contact;
            }


        }

        private void clearFormValues()
        {
            TxtName.Text = String.Empty;
            TxtContact.Text = String.Empty;
            TxtEmail.Text = String.Empty;
        }

        private void BtnErrorClose_Click(object sender, RoutedEventArgs e)
        {
            PanelError.Visibility = Visibility.Collapsed;
        }

        private void BtnDialogOK_Click(object sender, RoutedEventArgs e)
        {
            PanelDialog.Visibility = Visibility.Collapsed;
            Overlay.Visibility = Visibility.Collapsed;
        }

        private void BtnDialogOK2_Click(object sender, RoutedEventArgs e)
        {
            PanelDialog2.Visibility = Visibility.Collapsed;
            Overlay.Visibility = Visibility.Collapsed;

            removeContact((Contact)ContactsListView.SelectedItem);

            if (ContactsListView.SelectedItem != null)
            {
                PanelDetails.Visibility = Visibility.Visible;
                NoSelectionPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                PanelDetails.Visibility = Visibility.Collapsed;
                NoSelectionPanel.Visibility = Visibility.Visible;
            }
        }

        private void BtnDialogCancel2_Click(object sender, RoutedEventArgs e)
        {
            PanelDialog2.Visibility = Visibility.Collapsed;
            Overlay.Visibility = Visibility.Collapsed;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            filteredList.Clear();
            string searchText = TxtSearch.Text;

            if (searchText != "")
            {
                foreach (var contact in contactList)
                {
                    if (contact.name.ToLower().StartsWith(searchText.ToLower()))
                    {
                        filteredList.Add(contact);
                    }
                }

                ContactsListView.ItemsSource = filteredList;

                if (filteredList.Count == 0)
                {
                    PanelNoResult.Visibility = Visibility.Visible;
                }
                else
                {
                    PanelNoResult.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                ContactsListView.ItemsSource = contactList;
                PanelNoResult.Visibility = Visibility.Collapsed;
            }
        }

        // NAVIGATION BUTTONS
        private void NavPatients_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PatientPage));
        }

        private void NavStaff_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StaffPage));
        }

        private void NavStock_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Stock));
        }

        private void NavPriceLists_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PriceLists));
        }

        private void NavServices_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ServicesPage));
        }

        private void NavContacts_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ContactsPage));
        }

        private void NavFinances_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Finances));
        }

        private void NavForms_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FormPage));
        }

        private void NavLogout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }
    }
}
