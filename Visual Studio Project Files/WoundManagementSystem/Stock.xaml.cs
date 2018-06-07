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
    public sealed partial class Stock : Page
    {
        private ObservableCollection<Medicine> medList = new ObservableCollection<Medicine>();
        private ObservableCollection<Medicine> filteredList = new ObservableCollection<Medicine>();

        public Stock()
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
            String qry = "SELECT* FROM stock";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string id = reader["MedicationID"].ToString();
                string name = reader["MedicationName"].ToString();
                string strqQty = reader["QuantityAvailable"].ToString();
                string strPrice = reader["Price"].ToString();

                double price = Double.Parse(strPrice);
                int qty = int.Parse(strqQty);

                Medicine medicine = new Medicine(id, name, qty, price);

                medList.Add(medicine);
            }

            conn.Close();
        }

        private void removeMedicine(Medicine medicine)
        {
            medList.Remove(medicine);

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"DELETE FROM stock WHERE MedicationID = '{medicine.id}';";
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

        private void BtnAddMedicine_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Add Stock";

            PanelDetails.Visibility = Visibility.Collapsed;
            PanelAddModify.Visibility = Visibility.Visible;
            BtnCancel.Visibility = Visibility.Collapsed;
            NoSelectionPanel.Visibility = Visibility.Collapsed;
            BtnModifyMedicine.Visibility = Visibility.Collapsed;
            ListViewStock.IsEnabled = false;
            BtnAddMedicine.IsEnabled = false;
            BtnAdd.Visibility = Visibility.Visible;
            TxtSearch.IsEnabled = false;
        }

        private void BtnModify_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Modify Stock";

            PanelDetails.Visibility = Visibility.Collapsed;
            PanelAddModify.Visibility = Visibility.Visible;
            BtnCancel.Visibility = Visibility.Collapsed;
            NoSelectionPanel.Visibility = Visibility.Collapsed;

            BtnAdd.Visibility = Visibility.Collapsed;
            BtnModifyMedicine.Visibility = Visibility.Visible;

            Medicine medicine = (Medicine)ListViewStock.SelectedItem;

            TxtName.Text = medicine.name;
            TxtPrice.Text = medicine.price + "";
            TxtQty.Text = medicine.quantity + "";

            ListViewStock.IsEnabled = false;
            BtnAddMedicine.IsEnabled = false;
            TxtSearch.IsEnabled = false;
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            // Dialog 2
            PanelDialog2.Visibility = Visibility.Visible;
            TxtDialogMessage2.Text = "Are you sure you want to remove this medicine?";
            Overlay.Visibility = Visibility.Visible;

        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Medicine medicine = (Medicine)e.ClickedItem;

            LblName.Text = medicine.name;
            LblID.Text = medicine.id;
            LblPrice.Text = "Rs" + medicine.price.ToString();
            LblQty.Text = medicine.quantity.ToString();

            PanelDetails.Visibility = Visibility.Visible;
            PanelAddModify.Visibility = Visibility.Collapsed;
            BtnCancel.Visibility = Visibility.Visible;
            NoSelectionPanel.Visibility = Visibility.Collapsed;
        }

        private void BtnAddModifyCancel_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Stock";

            if (ListViewStock.SelectedItem != null)
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

            ListViewStock.IsEnabled = true;
            BtnAddMedicine.IsEnabled = true;
            TxtSearch.IsEnabled = true;
            PanelError.Visibility = Visibility.Collapsed;
        }

        private bool validateFields()
        {

            bool priceValidity = true;
            bool qtyValidity = true;

            if (!double.TryParse(TxtPrice.Text, out double price))
            {
                priceValidity = false;
            }

            if (!int.TryParse(TxtQty.Text, out int qty))
            {
                qtyValidity = false;
            }

            if (TxtName.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please enter a medication name.";
                return false;
            }
            else if (TxtQty.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please enter a quantity.";
                return false;
            }
            else if (qtyValidity == false)
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Quantity can only contain numbers.";
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

        private void insertMedicine(Medicine medicine)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"INSERT INTO stock VALUES('{medicine.id}','{medicine.name}','{medicine.quantity}','{medicine.price}')";
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
                int qty = int.Parse(TxtQty.Text);

                Medicine medicine = new Medicine(IDManager.getNextMedId(), TxtName.Text, qty, price);
                insertMedicine(medicine);
                TxtName.Text = "";
                TxtPrice.Text = "";
                TxtQty.Text = "";
                //CustomMessageDialog.createDialog("Medicine Added", "OK", "A new medicine has been added.");

                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "A new medicine has been added.";
                Overlay.Visibility = Visibility.Visible;

                PanelAddModify.Visibility = Visibility.Collapsed;
                BtnCancel.Visibility = Visibility.Visible;

                ListViewStock.IsEnabled = true;
                BtnAddMedicine.IsEnabled = true;
                TxtSearch.IsEnabled = true;

                medList.Add(medicine);

                LblTitle.Text = "Stock";

                if (ListViewStock.SelectedItem != null)
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

        private void BtnModifyMedicine_Click(object sender, RoutedEventArgs e)
        {

            Medicine medicine = (Medicine)ListViewStock.SelectedItem;

            if (validateFields())
            {
                medicine.name = TxtName.Text;
                medicine.quantity = int.Parse(TxtQty.Text);
                medicine.price = Double.Parse(TxtPrice.Text);

                modifyMedicine(medicine);

                clearFormValues();

                //CustomMessageDialog.createDialog("Stock Modified", "OK", "Stock has been modified.");

                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "Stock has been modified.";
                Overlay.Visibility = Visibility.Visible;

                PanelDetails.Visibility = Visibility.Collapsed;
                PanelAddModify.Visibility = Visibility.Collapsed;
                NoSelectionPanel.Visibility = Visibility.Visible;
                BtnCancel.Visibility = Visibility.Visible;

                ListViewStock.IsEnabled = true;
                BtnAddMedicine.IsEnabled = true;
                TxtSearch.IsEnabled = true;

                LblTitle.Text = "Stock";
            }
        }

        private void modifyMedicine(Medicine medicine)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = $"Update stock SET MedicationName='{medicine.name}', QuantityAvailable='{medicine.quantity}'," +
                $"Price='{medicine.price}' WHERE MedicationID = '{medicine.id}';";


            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            int index = -1;

            foreach (var med in medList)
            {
                if (med.id.Equals(medicine.id))
                {
                    index = medList.IndexOf(med);
                }
            }

            if (index >= 0)
            {
                medList[index] = medicine;
            }


        }

        private void clearFormValues()
        {
            TxtName.Text = String.Empty;
            TxtPrice.Text = String.Empty;
            TxtQty.Text = String.Empty;
        }

        private void BtnErrorClose_Click(object sender, RoutedEventArgs e)
        {
            PanelError.Visibility = Visibility.Collapsed;
        }

        private void BtnDialogOK2_Click(object sender, RoutedEventArgs e)
        {
            removeMedicine((Medicine)ListViewStock.SelectedItem);

            PanelDialog2.Visibility = Visibility.Collapsed;
            Overlay.Visibility = Visibility.Collapsed;

            if (ListViewStock.SelectedItem != null)
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

        private void BtnDialogOK_Click(object sender, RoutedEventArgs e)
        {
            PanelDialog.Visibility = Visibility.Collapsed;
            Overlay.Visibility = Visibility.Collapsed;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            filteredList.Clear();
            string searchText = TxtSearch.Text;

            if (searchText != "")
            {
                foreach (var medicine in medList)
                {
                    if (medicine.name.ToLower().StartsWith(searchText.ToLower()))
                    {
                        filteredList.Add(medicine);
                    }
                }

                ListViewStock.ItemsSource = filteredList;

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
                ListViewStock.ItemsSource = medList;
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
