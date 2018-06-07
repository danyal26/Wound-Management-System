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
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WoundManagementSystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewPatientVisit : Page
    {
        private ObservableCollection<Medicine> medList = new ObservableCollection<Medicine>();
        private ObservableCollection<Service> servList = new ObservableCollection<Service>();

        private ObservableCollection<VisitDocumentInformation> docsCollection = new ObservableCollection<VisitDocumentInformation>();
        private ObservableCollection<VisitPhotoInformation> photosCollection = new ObservableCollection<VisitPhotoInformation>();

        PatientVisit thisVisit = null;

        StorageFolder localFolder = ApplicationData.Current.LocalFolder;

        public ViewPatientVisit()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            thisVisit = (PatientVisit)e.Parameter;

            getVisitDetails(thisVisit);
            getVisitMeds(thisVisit);
            getVisitServices(thisVisit);
            getDocumentsList(thisVisit);
            getPhotosList(thisVisit);

            checkIfAssessmentExists();
        }

        // GET MAIN VISIT INFO
        private void getVisitDetails(PatientVisit visit)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT patient_visits.*, patients.Name, patients.Surname FROM patient_visits " +
                "JOIN patients ON patient_visits.PatientID = patients.PatientID " +
                "WHERE patient_visits.PatientVisitID = '" + visit.id + "'; ";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string id = reader["PatientVisitID"].ToString();
                string pId = reader["PatientID"].ToString();
                string notes = reader["Notes"].ToString();
                string date = reader["Date"].ToString();
                string time = reader["Time"].ToString();
                string incomeId = reader["IncomeID"].ToString();
                string afterOpening = reader["AfterOpening"].ToString();
                string actions = reader["ActionsPerformed"].ToString();
                string beforeClosing = reader["BeforeClosing"].ToString();
                string name = reader["Name"].ToString();
                string surname = reader["Surname"].ToString();
                string type = reader["Type"].ToString();

                LblName.Text = name + " " + surname;
                LblDateTime.Text = date + " " + time;
                LblDateTime.Text = DateTimeManager.DateToShort(date) + " | " + time;

                LblAfterOpeningText.Text = afterOpening;
                LblActionsTakenText.Text = actions;
                LblBeforeClosingText.Text = beforeClosing;
                LblNotesText.Text = notes;

            }
        }

        //GET VISIT MEDICATION LIST
        private void getVisitMeds(PatientVisit visit)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();
            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = "SELECT patient_visit_medication.MedicationID, patient_visit_medication.Quantity, " +
                "stock.MedicationName, stock.Price FROM patient_visit_medication " +
                "JOIN stock " +
                "ON patient_visit_medication.MedicationID = stock.MedicationID " +
                "WHERE PatientVisitID = '" + visit.id + "';";

            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string medId = reader["MedicationID"].ToString();
                string name = reader["MedicationName"].ToString();
                string stringQty = reader["Quantity"].ToString();
                string stringPrice = reader["Price"].ToString();

                int qty = int.Parse(stringQty);
                double price = double.Parse(stringPrice);

                Medicine med = new Medicine(medId, name, qty, price);

                medList.Add(med);

            }
        }

        // CHECK ASSESSMENT
        private bool burnsExists()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();
            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = $"SELECT * FROM burns_visit WHERE PatientBurnsVisitID = '{thisVisit.id}';";

            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            int counter = 0;

            while (reader.Read())
            {
                counter++;

            }

            if (counter > 0) return true;
            else return false;
        }

        private bool ulcerExists()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();
            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = $"SELECT * FROM chronic_ulcer_visit WHERE PatientChronicUlcerVisitID = '{thisVisit.id}';";

            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            int counter = 0;

            while (reader.Read())
            {
                counter++;

            }

            if (counter > 0) return true;
            else return false;
        }

        private bool diabeticFootExists()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();
            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = $"SELECT * FROM diabetic_foot_visit WHERE PatientDiabeticFootVisitID = '{thisVisit.id}';";

            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            int counter = 0;

            while (reader.Read())
            {
                counter++;

            }

            if (counter > 0) return true;
            else return false;
        }

        private void checkIfAssessmentExists()
        {
            bool exists = false;

            if (burnsExists())
            {
                exists = true;
            }
            else if (ulcerExists())
            {
                exists = true;
            }
            else if (diabeticFootExists())
            {
                exists = true;
            }

            if (!exists)
            {
                BtnViewAssessment.IsEnabled = false;
            }
            else
            {
                BtnViewAssessment.IsEnabled = true;
            }
        }

        //GET VISIT SERVICES LIST
        private void getVisitServices(PatientVisit visit)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();
            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = "SELECT patient_visit_services.ServiceID, services.ServiceType, services.ServiceName, " +
                "services.Price FROM patient_visit_services JOIN services " +
                "ON patient_visit_services.ServiceID = services.ServiceID " +
                "WHERE patient_visit_services.PatientVisitID = '" + visit.id + "';";

            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string id = reader["ServiceID"].ToString();
                string type = reader["ServiceType"].ToString();
                string name = reader["ServiceName"].ToString();
                string stringPrice = reader["Price"].ToString();
                double price = double.Parse(stringPrice);

                Service serv = new Service(id, type, name, price);

                servList.Add(serv);

            }
        }

        // GET DOCS
        private async void getDocumentsList(PatientVisit visit)
        {
            StorageFolder patientFolder = (StorageFolder)await localFolder.TryGetItemAsync(visit.patientId);

            if (patientFolder != null)
            {
                StorageFolder visitFolder = (StorageFolder)await patientFolder.TryGetItemAsync(visit.id);

                if (visitFolder != null)
                {
                    StorageFolder docsFolder = (StorageFolder)await visitFolder.TryGetItemAsync("Documents");

                    if (docsFolder != null)
                    {
                        var files = await docsFolder.GetFilesAsync();

                        foreach (var item in files)
                        {
                            docsCollection.Add(new VisitDocumentInformation(item, item.Name));
                        }
                    }

                }
            }

        }

        // GET PHOTOS
        private async void getPhotosList(PatientVisit visit)
        {
            StorageFolder patientFolder = (StorageFolder)await localFolder.TryGetItemAsync(visit.patientId);

            if (patientFolder != null)
            {
                StorageFolder visitFolder = (StorageFolder)await patientFolder.TryGetItemAsync(visit.id);

                if (visitFolder != null)
                {
                    StorageFolder photosFolder = (StorageFolder)await visitFolder.TryGetItemAsync("Photos");

                    if (photosFolder != null)
                    {
                        var files = await photosFolder.GetFilesAsync();

                        foreach (var item in files)
                        {
                            var stream = await item.OpenReadAsync();
                            var image = new BitmapImage();
                            image.SetSource(stream);
                            photosCollection.Add(new VisitPhotoInformation(item, item.Name, image));
                        }
                    }

                }
            }

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PatientPage));
        }

        private void BtnToggleNav_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        private void BtViewReceipt_Click(object sender, RoutedEventArgs e)
        {

            ReceiptInformation info = new ReceiptInformation(thisVisit, medList, servList, getTotalPayment());

            Frame.Navigate(typeof(Receipt), info);
        }

        private double getTotalPayment()
        {
            double total = 0;

            foreach (var medicine in medList)
            {
                total += medicine.price;
            }

            foreach (var service in servList)
            {
                total += service.price;
            }

            return total;
        }

        private void BtnViewAssessment_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ViewAssessment), thisVisit);
        }

        private void ListPhotos_ItemClick(object sender, ItemClickEventArgs e)
        {
            VisitPhotoInformation info = (VisitPhotoInformation)e.ClickedItem;
            BitmapImage image = info.image;
            ImgLargePhoto.Source = image;

            TxtLargePhotoName.Text = info.fileName;

            Overlay.Visibility = Visibility.Visible;
            PanelLargePhoto.Visibility = Visibility.Visible;
        }

        private void BtnClosePhoto_Click(object sender, RoutedEventArgs e)
        {
            Overlay.Visibility = Visibility.Collapsed;
            PanelLargePhoto.Visibility = Visibility.Collapsed;
        }
    }
}
