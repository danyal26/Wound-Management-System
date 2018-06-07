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
    public sealed partial class ServicesPage : Page
    {
        private ObservableCollection<Service> servList = new ObservableCollection<Service>();
        private ObservableCollection<Service> filteredList = new ObservableCollection<Service>();

        public ServicesPage()
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
            String qry = "SELECT* FROM services";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string id = reader["ServiceID"].ToString();
                string type = reader["ServiceType"].ToString();
                string name = reader["ServiceName"].ToString();
                string strPrice = reader["Price"].ToString();

                double price = Double.Parse(strPrice);

                Service service = new Service(id, type, name, price);

                servList.Add(service);
            }

            conn.Close();
        }

        private void removeService(Service service)
        {
            servList.Remove(service);

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"DELETE FROM services WHERE ServiceID = '{service.id}';";
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

        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Add Service";

            PanelDetails.Visibility = Visibility.Collapsed;
            PanelAddModify.Visibility = Visibility.Visible;
            BtnCancel.Visibility = Visibility.Collapsed;
            NoSelectionPanel.Visibility = Visibility.Collapsed;
            BtnModifyService.Visibility = Visibility.Collapsed;
            ListViewServices.IsEnabled = false;
            BtnAddService.IsEnabled = false;
            BtnAdd.Visibility = Visibility.Visible;
            TxtSearch.IsEnabled = false;
        }

        private void BtnModify_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Modify Service";

            PanelDetails.Visibility = Visibility.Collapsed;
            PanelAddModify.Visibility = Visibility.Visible;
            BtnCancel.Visibility = Visibility.Collapsed;
            NoSelectionPanel.Visibility = Visibility.Collapsed;

            BtnAdd.Visibility = Visibility.Collapsed;
            BtnModifyService.Visibility = Visibility.Visible;

            Service service = (Service)ListViewServices.SelectedItem;

            TxtName.Text = service.name;
            TxtPrice.Text = service.price + "";
            TxtType.Text = service.type;

            ListViewServices.IsEnabled = false;
            BtnAddService.IsEnabled = false;
            TxtSearch.IsEnabled = false;
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            // Dialog 2
            PanelDialog2.Visibility = Visibility.Visible;
            TxtDialogMessage2.Text = "Are you sure you want to remove this service?";
            Overlay.Visibility = Visibility.Visible;

        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Service service = (Service)e.ClickedItem;

            LblName.Text = service.name;
            LblID.Text = service.id;
            LblPrice.Text = "Rs" + service.price.ToString();
            LblType.Text = service.type;

            PanelDetails.Visibility = Visibility.Visible;
            PanelAddModify.Visibility = Visibility.Collapsed;
            BtnCancel.Visibility = Visibility.Visible;
            NoSelectionPanel.Visibility = Visibility.Collapsed;
        }

        private void BtnAddModifyCancel_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Services";

            if (ListViewServices.SelectedItem != null)
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

            ListViewServices.IsEnabled = true;
            BtnAddService.IsEnabled = true;
            TxtSearch.IsEnabled = true;

            PanelError.Visibility = Visibility.Collapsed;
        }

        private bool validateFields()
        {
            bool priceValidity = true;

            if (!double.TryParse(TxtPrice.Text, out double price))
            {
                priceValidity = false;
            }

            if (TxtName.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please enter a service name.";
                return false;
            }
            else if (TxtType.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please enter a service type.";
                return false;
            }
            else if (TxtPrice.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please enter a price.";
                return false;
            }
            else if (priceValidity == false)
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Price can only contain numbers.";
                return false;
            }
            else
            {
                PanelError.Visibility = Visibility.Collapsed;
                return true;
            }
        }

        private void insertService(Service service)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"INSERT INTO services VALUES('{service.id}','{service.type}','{service.name}','{service.price}')";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (validateFields())
            {
                double price = Double.Parse(TxtPrice.Text);
                Service service = new Service(IDManager.getNextServiceId(), TxtType.Text, TxtName.Text, price);
                insertService(service);
                TxtName.Text = "";
                TxtPrice.Text = "";
                TxtType.Text = "";
                //CustomMessageDialog.createDialog("Service Added", "OK", "A new service has been added.");

                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "A new service has been added.";
                Overlay.Visibility = Visibility.Visible;

                PanelAddModify.Visibility = Visibility.Collapsed;
                BtnCancel.Visibility = Visibility.Visible;

                ListViewServices.IsEnabled = true;
                BtnAddService.IsEnabled = true;
                TxtSearch.IsEnabled = true;

                servList.Add(service);

                LblTitle.Text = "Services";

                if (ListViewServices.SelectedItem != null)
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

        private void BtnModifyService_Click(object sender, RoutedEventArgs e)
        {

            Service service = (Service)ListViewServices.SelectedItem;

            if (validateFields())
            {
                service.name = TxtName.Text;
                service.type = TxtType.Text;
                service.price = Double.Parse(TxtPrice.Text);

                modifyService(service);

                clearFormValues();

                //CustomMessageDialog.createDialog("Service Modified", "OK", "Service has been modified.");

                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "The service has been modified.";
                Overlay.Visibility = Visibility.Visible;

                PanelDetails.Visibility = Visibility.Collapsed;
                PanelAddModify.Visibility = Visibility.Collapsed;
                NoSelectionPanel.Visibility = Visibility.Visible;
                BtnCancel.Visibility = Visibility.Visible;

                ListViewServices.IsEnabled = true;
                BtnAddService.IsEnabled = true;
                TxtSearch.IsEnabled = true;

                LblTitle.Text = "Services";
            }
            else
            {
                CustomMessageDialog.createDialog("Error", "OK", "Please fill in all fields.");
            }
        }

        private void modifyService(Service service)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = $"Update services SET ServiceType='{service.type}', ServiceName='{service.name}'," +
                $"Price='{service.price}' WHERE ServiceID = '{service.id}';";


            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            int index = -1;

            foreach (var serv in servList)
            {
                if (serv.id.Equals(service.id))
                {
                    index = servList.IndexOf(serv);
                }
            }

            if (index >= 0)
            {
                servList[index] = service;
            }


        }

        private void clearFormValues()
        {
            TxtName.Text = String.Empty;
            TxtPrice.Text = String.Empty;
            TxtType.Text = String.Empty;
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

            removeService((Service)ListViewServices.SelectedItem);

            if (ListViewServices.SelectedItem != null)
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
                foreach (var service in servList)
                {
                    if (service.name.ToLower().StartsWith(searchText.ToLower()))
                    {
                        filteredList.Add(service);
                    }
                }

                ListViewServices.ItemsSource = filteredList;

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
                ListViewServices.ItemsSource = servList;
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
