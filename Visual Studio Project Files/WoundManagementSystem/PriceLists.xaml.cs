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
    public sealed partial class PriceLists : Page
    {
        private ObservableCollection<Medicine> buyingList = new ObservableCollection<Medicine>();
        private ObservableCollection<Medicine> sellingList = new ObservableCollection<Medicine>();

        private ObservableCollection<Medicine> filteredBuyingList = new ObservableCollection<Medicine>();
        private ObservableCollection<Medicine> filteredSellingList = new ObservableCollection<Medicine>();

        // CONSTRUCTOR
        public PriceLists()
        {
            this.InitializeComponent();
            getBuyingList();
            getSellingList();

            BtnModifyBuying.IsEnabled = false;
            BtnRemoveBuying.IsEnabled = false;

            BtnModifySelling.IsEnabled = false;
            BtnRemoveSelling.IsEnabled = false;
        }

        // METHODS

        private void getBuyingList()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT* FROM medication_buying_list";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string id = reader["MedicationID"].ToString();
                string name = reader["MedicationName"].ToString();
                string strprice = reader["Price"].ToString();

                double price = Double.Parse(strprice);

                Medicine medicine = new Medicine(id, name, 0, price);

                buyingList.Add(medicine);
            }

            conn.Close();
        }

        private void getSellingList()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT* FROM medication_selling_list";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string id = reader["MedicationID"].ToString();
                string name = reader["MedicationName"].ToString();
                string strprice = reader["Price"].ToString();

                double price = Double.Parse(strprice);

                Medicine medicine = new Medicine(id, name, 0, price);

                sellingList.Add(medicine);
            }

            conn.Close();
        }

        private void clearFields()
        {
            TxtName.Text = String.Empty;
            TxtPrice.Text = String.Empty;
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
                TxtErrorMessage.Text = "Please enter a medication name.";
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

        private void modifyMedicine(Medicine medicine)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = $"Update medication_buying_list SET MedicationName='{medicine.name}', Price='{medicine.price}'" +
                $"WHERE MedicationID='{medicine.id}'";


            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            int index = -1;

            foreach (var med in buyingList)
            {
                if (med.id.Equals(medicine.id))
                {
                    index = buyingList.IndexOf(med);
                }
            }

            if (index >= 0)
            {
                buyingList[index] = medicine;
            }
        }

        private void modifySellingMedicine(Medicine medicine)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = $"Update medication_selling_list SET MedicationName='{medicine.name}', Price='{medicine.price}'" +
                $"WHERE MedicationID='{medicine.id}'";


            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            int index = -1;

            foreach (var med in sellingList)
            {
                if (med.id.Equals(medicine.id))
                {
                    index = sellingList.IndexOf(med);
                }
            }

            if (index >= 0)
            {
                sellingList[index] = medicine;
            }
        }

        private void removeMedicine(Medicine medicine)
        {
            buyingList.Remove(medicine);

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"DELETE FROM medication_buying_list WHERE MedicationID = '{medicine.id}';";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void removeSellingMedicine(Medicine medicine)
        {
            sellingList.Remove(medicine);

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"DELETE FROM medication_selling_list WHERE MedicationID = '{medicine.id}';";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // BUTTONS

        private void BtnAddBuyingMedicine_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Add Medicine";

            Overlay1.Visibility = Visibility.Visible;
            Overlay2.Visibility = Visibility.Visible;
            PanelAddModify.Visibility = Visibility.Visible;

            BtnAddThisBuying.Visibility = Visibility.Visible;
            BtnModifyThisBuying.Visibility = Visibility.Collapsed;
            BtnAddThisSelling.Visibility = Visibility.Collapsed;
            BtnModifyThisSelling.Visibility = Visibility.Collapsed;
            BtnAddBuyingCancel.Visibility = Visibility.Visible;
            BtnAddSellingCancel.Visibility = Visibility.Collapsed;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Dashboard));
        }

        private void BtnToggleNav_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        private void BuyingListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            BtnModifyBuying.IsEnabled = true;
            BtnRemoveBuying.IsEnabled = true;
        }

        private void BtnAddSellingMedicine_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Add Medicine";

            Overlay1.Visibility = Visibility.Visible;
            Overlay2.Visibility = Visibility.Visible;
            PanelAddModify.Visibility = Visibility.Visible;

            BtnAddThisBuying.Visibility = Visibility.Collapsed;
            BtnModifyThisBuying.Visibility = Visibility.Collapsed;
            BtnAddThisSelling.Visibility = Visibility.Visible;
            BtnModifyThisSelling.Visibility = Visibility.Collapsed;
            BtnAddBuyingCancel.Visibility = Visibility.Collapsed;
            BtnAddSellingCancel.Visibility = Visibility.Visible;
        }

        private void BtnAddThisBuying_Click(object sender, RoutedEventArgs e)
        {
            if (validateFields())
            {
                Medicine medicine = new Medicine(IDManager.getNextBuyingListId(), TxtName.Text, 0, Double.Parse(TxtPrice.Text));

                MySqlConnectionStringBuilder builder = DBConnection.Connect();

                MySqlConnection conn = new MySqlConnection(builder.ToString());
                String qry = $"INSERT INTO medication_buying_list " +
                    $"VALUES('{medicine.id}','{medicine.name}','{medicine.price}')";
                MySqlCommand cmd = new MySqlCommand(qry, conn);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                //CustomMessageDialog.createDialog("Medication Added", "OK", "A new medicine has been added.");

                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "A new medicine has been added.";
                Overlay.Visibility = Visibility.Visible;

                Overlay1.Visibility = Visibility.Collapsed;
                Overlay2.Visibility = Visibility.Collapsed;
                PanelAddModify.Visibility = Visibility.Collapsed;

                buyingList.Add(medicine);

                LblTitle.Text = "Price Lists";

                clearFields();
            }


        }

        private void BtnAddBuyingCancel_Click(object sender, RoutedEventArgs e)
        {
            Overlay1.Visibility = Visibility.Collapsed;
            Overlay2.Visibility = Visibility.Collapsed;
            PanelAddModify.Visibility = Visibility.Collapsed;

            LblTitle.Text = "Price Lists";

            clearFields();
        }

        private void BtnRemoveBuyingMedicine_Click(object sender, RoutedEventArgs e)
        {
            // Dialog 2
            BtnDialogOK2.Tag = "buying";
            PanelDialog2.Visibility = Visibility.Visible;
            TxtDialogMessage2.Text = "Are you sure you want to remove this medicine?";
            Overlay.Visibility = Visibility.Visible;

        }

        private void BtnModifyBuyingMedicine_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Modify Medicine";

            Overlay1.Visibility = Visibility.Visible;
            Overlay2.Visibility = Visibility.Visible;
            PanelAddModify.Visibility = Visibility.Visible;

            BtnAddThisBuying.Visibility = Visibility.Collapsed;
            BtnModifyThisBuying.Visibility = Visibility.Visible;
            BtnAddThisSelling.Visibility = Visibility.Collapsed;
            BtnModifyThisSelling.Visibility = Visibility.Collapsed;

            BtnAddBuyingCancel.Visibility = Visibility.Visible;
            BtnAddSellingCancel.Visibility = Visibility.Collapsed;

            Medicine selectedMed = (Medicine)BuyingListView.SelectedItem;

            TxtName.Text = selectedMed.name;
            TxtPrice.Text = selectedMed.price + "";
        }

        private void BtnModifyThisBuying_Click(object sender, RoutedEventArgs e)
        {
            Medicine selectedMedicine = (Medicine)BuyingListView.SelectedItem;

            if (validateFields())
            {
                selectedMedicine.name = TxtName.Text;
                selectedMedicine.price = Double.Parse(TxtPrice.Text);

                modifyMedicine(selectedMedicine);

                clearFields();

                Overlay1.Visibility = Visibility.Collapsed;
                Overlay2.Visibility = Visibility.Collapsed;
                PanelAddModify.Visibility = Visibility.Collapsed;

                //CustomMessageDialog.createDialog("Medicine Modified", "OK", "Medicine has been modified.");

                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "Medicine has been modified.";
                Overlay.Visibility = Visibility.Visible;

                if (BuyingListView.SelectedItem == null)
                {
                    BtnModifyBuying.IsEnabled = false;
                    BtnRemoveBuying.IsEnabled = false;
                }

                LblTitle.Text = "Price Lists";
            }
        }

        private void BtnAddThisSelling_Click(object sender, RoutedEventArgs e)
        {
            if (validateFields())
            {
                Medicine medicine = new Medicine(IDManager.getNextSellingListId(), TxtName.Text, 0, Double.Parse(TxtPrice.Text));

                MySqlConnectionStringBuilder builder = DBConnection.Connect();

                MySqlConnection conn = new MySqlConnection(builder.ToString());
                String qry = $"INSERT INTO medication_selling_list " +
                    $"VALUES('{medicine.id}','{medicine.name}','{medicine.price}')";
                MySqlCommand cmd = new MySqlCommand(qry, conn);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                //CustomMessageDialog.createDialog("Medication Added", "OK", "A new medicine has been added.");

                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "A new medicine has been added.";
                Overlay.Visibility = Visibility.Visible;

                Overlay1.Visibility = Visibility.Collapsed;
                Overlay2.Visibility = Visibility.Collapsed;
                PanelAddModify.Visibility = Visibility.Collapsed;

                sellingList.Add(medicine);

                LblTitle.Text = "Price Lists";

                clearFields();
            }
        }

        private void BtnModifyThisSelling_Click(object sender, RoutedEventArgs e)
        {
            Medicine selectedMedicine = (Medicine)SellingListView.SelectedItem;

            if (validateFields())
            {
                selectedMedicine.name = TxtName.Text;
                selectedMedicine.price = Double.Parse(TxtPrice.Text);

                modifySellingMedicine(selectedMedicine);

                clearFields();

                Overlay1.Visibility = Visibility.Collapsed;
                Overlay2.Visibility = Visibility.Collapsed;
                PanelAddModify.Visibility = Visibility.Collapsed;

                //CustomMessageDialog.createDialog("Medicine Modified", "OK", "Medicine has been modified.");

                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "Medicine has been modified.";
                Overlay.Visibility = Visibility.Visible;

                if (SellingListView.SelectedItem == null)
                {
                    BtnModifySelling.IsEnabled = false;
                    BtnRemoveSelling.IsEnabled = false;
                }

                LblTitle.Text = "Price Lists";
            }
        }

        private void BtnAddSellingCancel_Click(object sender, RoutedEventArgs e)
        {
            Overlay1.Visibility = Visibility.Collapsed;
            Overlay2.Visibility = Visibility.Collapsed;
            PanelAddModify.Visibility = Visibility.Collapsed;

            LblTitle.Text = "Price Lists";

            clearFields();
        }

        private void SellingListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            BtnModifySelling.IsEnabled = true;
            BtnRemoveSelling.IsEnabled = true;
        }

        private void BtnModifySelling_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Modify Medicine";

            Overlay1.Visibility = Visibility.Visible;
            Overlay2.Visibility = Visibility.Visible;
            PanelAddModify.Visibility = Visibility.Visible;

            BtnAddThisBuying.Visibility = Visibility.Collapsed;
            BtnModifyThisBuying.Visibility = Visibility.Collapsed;
            BtnAddThisSelling.Visibility = Visibility.Collapsed;
            BtnModifyThisSelling.Visibility = Visibility.Visible;

            BtnAddBuyingCancel.Visibility = Visibility.Collapsed;
            BtnAddSellingCancel.Visibility = Visibility.Visible;

            Medicine selectedMed = (Medicine)SellingListView.SelectedItem;

            TxtName.Text = selectedMed.name;
            TxtPrice.Text = selectedMed.price + "";
        }

        private void BtnRemoveSelling_Click(object sender, RoutedEventArgs e)
        {
            // Dialog 2
            BtnDialogOK2.Tag = "selling";
            PanelDialog2.Visibility = Visibility.Visible;
            TxtDialogMessage2.Text = "Are you sure you want to remove this medicine?";
            Overlay.Visibility = Visibility.Visible;
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

            if (BtnDialogOK2.Tag.Equals("buying"))
            {
                removeMedicine((Medicine)BuyingListView.SelectedItem);

                BtnModifyBuying.IsEnabled = false;
                BtnRemoveBuying.IsEnabled = false;
            }
            else if (BtnDialogOK2.Tag.Equals("selling"))
            {
                removeSellingMedicine((Medicine)SellingListView.SelectedItem);

                BtnModifySelling.IsEnabled = false;
                BtnRemoveSelling.IsEnabled = false;
            }

        }

        private void BtnDialogCancel2_Click(object sender, RoutedEventArgs e)
        {
            PanelDialog2.Visibility = Visibility.Collapsed;
            Overlay.Visibility = Visibility.Collapsed;
        }

        private void TxtSearchBuying_TextChanged(object sender, TextChangedEventArgs e)
        {
            filteredBuyingList.Clear();
            string searchText = TxtSearchBuying.Text;

            if (searchText != "")
            {
                foreach (var med in buyingList)
                {
                    if (med.name.ToLower().StartsWith(searchText.ToLower()))
                    {
                        filteredBuyingList.Add(med);
                    }
                }

                BuyingListView.ItemsSource = filteredBuyingList;

                if (filteredBuyingList.Count == 0)
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
                BuyingListView.ItemsSource = buyingList;
                PanelNoResult.Visibility = Visibility.Collapsed;
            }
        }

        private void TxtSearchSelling_TextChanged(object sender, TextChangedEventArgs e)
        {
            filteredSellingList.Clear();
            string searchText = TxtSearchSelling.Text;

            if (searchText != "")
            {
                foreach (var med in sellingList)
                {
                    if (med.name.ToLower().StartsWith(searchText.ToLower()))
                    {
                        filteredSellingList.Add(med);
                    }
                }

                SellingListView.ItemsSource = filteredSellingList;

                if (filteredSellingList.Count == 0)
                {
                    PanelNoResult2.Visibility = Visibility.Visible;
                }
                else
                {
                    PanelNoResult2.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                SellingListView.ItemsSource = sellingList;
                PanelNoResult2.Visibility = Visibility.Collapsed;
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
