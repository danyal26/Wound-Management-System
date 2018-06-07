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

using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using WoundManagementSystem.ObjectClasses;
using Windows.UI.Popups;
using System.Text;
using Windows.Storage.AccessCache;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WoundManagementSystem
{

    public sealed partial class AddVisitDetails : Page
    {

        private ObservableCollection<Medicine> medList = new ObservableCollection<Medicine>();
        private ObservableCollection<Service> serviceList = new ObservableCollection<Service>();

        private ObservableCollection<Medicine> patientMedList = new ObservableCollection<Medicine>();
        private ObservableCollection<Service> patientServiceList = new ObservableCollection<Service>();

        private ObservableCollection<VisitDocumentInformation> docsCollection = new ObservableCollection<VisitDocumentInformation>();
        private ObservableCollection<VisitDocumentInformation> photosCollection = new ObservableCollection<VisitDocumentInformation>();

        private VisitAssessment thisAssessment;
        private PatientVisit thisVisit;
        private Appointment thisAppointment;

        private string fileName;
        private StorageFile file;

        StorageFolder localFolder = ApplicationData.Current.LocalFolder;

        public AddVisitDetails()
        {
            this.InitializeComponent();

            NoMedsPanel.Visibility = Visibility.Visible;
            NoServicePanel.Visibility = Visibility.Visible;
            NoPhotosPanel.Visibility = Visibility.Visible;
            NoDocsPanel.Visibility = Visibility.Visible;

            getMedicationList();
            getServicesList();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Appointment)
            {
                thisAppointment = (Appointment)e.Parameter;
                LblName.Text = thisAppointment.name + " " + thisAppointment.surname;
                LblDateTime.Text = DateTimeManager.DateToShort(thisAppointment.date) + " | " + thisAppointment.time;

            }

        }

        // INSERT VISIT INTO DB

        private void addPatientVisit(PatientVisit visit)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = "INSERT INTO patient_visits VALUES('" + visit.id + "','" + visit.patientId + "','" +
                visit.notes + "','" + visit.date + "','" + visit.time + "','" + visit.incomeId + "','" +
                visit.afterOpening + "','" + visit.actionsTaken + "','" + visit.beforeClosing + "', '" + visit.type + "');";

            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void addPatientMedication(string patientVisitId)
        {

            foreach (var medicine in patientMedList)
            {
                string id = IDManager.getNextVisitMedId();
                MySqlConnectionStringBuilder builder = DBConnection.Connect();

                MySqlConnection conn = new MySqlConnection(builder.ToString());

                String qry = "INSERT INTO patient_visit_medication VALUES('" + id + "','" +
                    patientVisitId + "','" + medicine.id + "','" + medicine.quantity + "')";

                MySqlCommand cmd = new MySqlCommand(qry, conn);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void addPatientServices(string patientVisitId)
        {
            foreach (var service in patientServiceList)
            {
                string id = IDManager.getNextVisitServiceId();

                MySqlConnectionStringBuilder builder = DBConnection.Connect();

                MySqlConnection conn = new MySqlConnection(builder.ToString());

                String qry = "INSERT INTO patient_visit_services VALUES('" + id + "','" +
                    patientVisitId + "','" + service.id + "')";

                MySqlCommand cmd = new MySqlCommand(qry, conn);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        // CREATE VISIT
        private PatientVisit createPatientVisit(Appointment appointment, string incomeId)
        {
            string nextPatientVisitId = IDManager.getNextPatientVisitId();

            string patientId = appointment.patientId;
            string date = appointment.date;
            string time = appointment.time;
            string type = appointment.type;
            PatientVisit visit = new PatientVisit(nextPatientVisitId, patientId, date, time, TxtNotes.Text, incomeId,
                TxtAfterOpening.Text, TxtActionsTaken.Text, TxtBeforeClosing.Text, type);

            return visit;
        }

        private double getTotalPayment()
        {
            double total = 0;

            foreach (var medicine in patientMedList)
            {
                total += medicine.price;
            }

            foreach (var service in patientServiceList)
            {
                total += service.price;
            }

            return total;
        }

        private bool medExistsInList(Medicine med)
        {
            foreach (var medicine in patientMedList)
            {
                if (medicine.id.Equals(med.id))
                {
                    return true;
                }
            }

            return false;
        }

        private bool serviceExists(Service serv)
        {
            foreach (var service in patientServiceList)
            {
                if (service.id.Equals(serv.id))
                {
                    return true;
                }
            }

            return false;
        }

        // INCREMENT EXISTING MEDICINE

        private void incrementMedQty(Medicine med)
        {
            int indexToRemove = 0;
            int currentIndex = 0;
            Medicine updateMed = null;

            foreach (var medicine in patientMedList)
            {
                if (medicine.id.Equals(med.id))
                {
                    updateMed = new Medicine(medicine.id, medicine.name, medicine.quantity + 1, medicine.price);
                    indexToRemove = currentIndex;

                }
                currentIndex++;
            }
            patientMedList.RemoveAt(indexToRemove);
            patientMedList.Add(updateMed);
        }

        // MEDICATION LSIT

        private void getMedicationList()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM stock";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string id = reader["MedicationID"].ToString();
                string name = reader["MedicationName"].ToString();
                string stringQuantity = reader["QuantityAvailable"].ToString();
                string stringPrice = reader["Price"].ToString();

                int quantity = int.Parse(stringQuantity);
                double price = double.Parse(stringPrice);

                if (quantity > 0)
                {
                    Medicine newMedicine = new Medicine(id, name, quantity, price);

                    medList.Add(newMedicine);
                }
            }

            conn.Close();
        }

        // SERVICES LIST

        private void getServicesList()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM services";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string id = reader["ServiceID"].ToString();
                string type = reader["ServiceType"].ToString();
                string name = reader["ServiceName"].ToString();
                string strPrice = reader["Price"].ToString();

                double price = double.Parse(strPrice);

                Service newService = new Service(id, type, name, price);

                serviceList.Add(newService);
            }

            conn.Close();
        }

        // INSERT INTO INCOME

        private string saveIncome()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();
            MySqlConnection conn = new MySqlConnection(builder.ToString());

            string incomeId = IDManager.getNextIncomeId();
            string total = getTotalPayment().ToString();

            string qry = "INSERT INTO income VALUES('" + incomeId + "','Visit','" + total + "','')";

            MySqlCommand cmd = new MySqlCommand(qry, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return incomeId;

        }

        // INSERT ASSESSMENT INTO DB

        private void insertVisitAssessment(PatientVisit pv)
        {

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = "";

            if (thisAssessment is BurnsVisit)
            {
                BurnsVisit v = thisAssessment as BurnsVisit;
                qry = $"INSERT INTO burns_visit VALUES('{v.VisitID}','{v.Purpose}','{v.Description}','{v.Manner}'," +
                    $"'{v.DiabetesMellitus}','{v.Hypertension}','{v.IHD}','{v.Asthma}','{v.Thyroid}','{v.CVA}','{v.DVT}'," +
                    $"'{v.Allergy}','{v.Smoking}','{v.Pallor}','{v.Jaundice}','{v.Clubbing}','{v.Pulses}','{v.Cardiovascular}'," +
                    $"'{v.Respiratory}','{v.Abdominal}','{v.Neurological}','{v.BP}','{v.Pulse}','{v.Temperature}','{v.RBS}'," +
                    $"'{v.Site}','{v.Size}','{v.Depth}','{v.Exudate}','{v.Circumferential}','{v.PeripheralCirculation}'," +
                    $"'{pv.id}')";
            }
            else if (thisAssessment is ChronicUlcerVisit)
            {
                ChronicUlcerVisit v = thisAssessment as ChronicUlcerVisit;
                qry = $"INSERT INTO chronic_ulcer_visit VALUES('{v.VisitID}','{v.Purpose}','{v.DurationOfUlcer}','{v.Treatment}'," +
                    $"'{v.DiabetesMellitus}','{v.Hypertension}','{v.IHD}','{v.Asthma}','{v.Thyroid}','{v.CVA}','{v.DVT}'," +
                    $"'{v.Allergy}','{v.Smoking}','{v.Pallor}','{v.Jaundice}','{v.Clubbing}','{v.Pulses}','{v.Cardiovascular}'," +
                    $"'{v.Respiratory}','{v.Abdominal}','{v.Neurological}','{v.BP}','{v.Pulse}','{v.Temperature}','{v.RBS}'," +
                    $"'{v.Site}','{v.Size}','{v.Depth}','{v.ExtentOfUndermining}','{v.WoundSurface}','{v.Exudate}','{v.PeriwoundTissue}'," +
                    $"'{pv.id}')";
            }
            else if (thisAssessment is DiabeticFootVisit)
            {
                DiabeticFootVisit v = thisAssessment as DiabeticFootVisit;
                qry = $"INSERT INTO diabetic_foot_visit VALUES('{v.VisitID}','{v.Purpose}','{v.DurationOfUlcer}','{v.Treatment}'," +
                    $"'{v.Claudication}','{v.PainAtRest}','{v.Hypesthesia}','{v.Hyperesthesia}','{v.Paraesthesia}','{v.Dysesthesia}'," +
                    $"'{v.RadicularPain}','{v.Anhydrosis}','{v.DiabetesMellitus}','{v.Hypertension}','{v.IHD}','{v.Asthma}','{v.Thyroid}','{v.CVA}','{v.DVT}'," +
                    $"'{v.Allergy}','{v.Smoking}','{v.Pallor}','{v.Jaundice}','{v.Clubbing}','{v.Pulses}','{v.Cardiovascular}'," +
                    $"'{v.Respiratory}','{v.Abdominal}','{v.Neurological}','{v.BP}','{v.Pulse}','{v.Temperature}','{v.RBS}'," +
                    $"'{v.Site}','{v.Size}','{v.Depth}','{v.ExtentOfUndermining}','{v.WoundSurface}','{v.Exudate}','{v.PeriwoundTissue}'," +
                    $"'{v.Callus}','{v.BrittleNail}','{v.HammerToe}','{v.Fissures}','{v.HairGrowth}'," +
                    $"'{v.CyanosisOfToes}','{v.PallorOfFoot}','{pv.id}')"; ;
            }

            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private async Task addPatientPhotos()
        {

            //check if patient folder exists
            StorageFolder patientFolder = (StorageFolder)await localFolder.TryGetItemAsync(thisVisit.patientId);
            if (patientFolder != null)
            {
                //check if date folder exists
                StorageFolder visitFolder = (StorageFolder)await patientFolder.TryGetItemAsync(thisVisit.id);
                if (visitFolder != null)
                {
                    //check if documents folder exists
                    StorageFolder photosFolder = (StorageFolder)await visitFolder.TryGetItemAsync("Photos");
                    if (photosFolder != null)
                    {

                        foreach (var item in photosCollection)
                        {
                            file = item.file;
                            fileName = item.fileName;

                            await file.CopyAsync(photosFolder, fileName);
                            //TxtActionsTaken.Text += fileName;
                        }
                    }
                }
            }
        }

        private async Task addPatientDocuments()
        {

            //check if patient folder exists
            StorageFolder patientFolder = (StorageFolder)await localFolder.TryGetItemAsync(thisVisit.patientId);
            if (patientFolder != null)
            {
                //check if date folder exists
                StorageFolder visitFolder = (StorageFolder)await patientFolder.TryGetItemAsync(thisVisit.id);
                if (visitFolder != null)
                {
                    //check if documents folder exists
                    StorageFolder docsFolder = (StorageFolder)await visitFolder.TryGetItemAsync("Documents");
                    if (docsFolder != null)
                    {

                        foreach (var item in docsCollection)
                        {
                            file = item.file;
                            fileName = item.fileName;

                            await file.CopyAsync(docsFolder, fileName);
                            //TxtActionsTaken.Text += fileName;
                        }
                    }
                }
            }
        }

        private async Task createFolders()
        {
            StorageFolder patientFolder = (StorageFolder)await localFolder.TryGetItemAsync(thisVisit.patientId);
            if (patientFolder == null)
            {
                await localFolder.CreateFolderAsync(thisVisit.patientId);

                patientFolder = (StorageFolder)await localFolder.TryGetItemAsync(thisVisit.patientId);
                await patientFolder.CreateFolderAsync(thisVisit.id);

                StorageFolder visitFolder = (StorageFolder)await patientFolder.TryGetItemAsync(thisVisit.id);
                await visitFolder.CreateFolderAsync("Documents");
                await visitFolder.CreateFolderAsync("Photos");
            }
        }

        private bool validateVisitDetails()
        {
            if (TxtAfterOpening.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "'After Opening' cannot be left blank.";
                return false;
            }
            else if (TxtActionsTaken.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "'Actions Taken' cannot be left blank.";
                return false;
            }
            else if (TxtBeforeClosing.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "'Before Closing' cannot be left blank.";
                return false;
            }
            else if (patientMedList.Count < 1 && patientServiceList.Count < 1)
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please add at least one medication or one service.";
                return false;
            }
            else
            {
                PanelError.Visibility = Visibility.Collapsed;
                return true;
            }
        }

        private void updateStock()
        {
            foreach (var medicine in patientMedList)
            {
                string id = IDManager.getNextVisitMedId();
                MySqlConnectionStringBuilder builder = DBConnection.Connect();

                MySqlConnection conn = new MySqlConnection(builder.ToString());

                String qry = $"UPDATE stock SET QuantityAvailable=QuantityAvailable-'{medicine.quantity}' " +
                    $"WHERE MedicationID='{medicine.id}';";

                MySqlCommand cmd = new MySqlCommand(qry, conn);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        // ************************************* BUTTONS ***************************************************
        #region Buttons

        private void BtnAddMed_Click(object sender, RoutedEventArgs e)
        {
            PanelMedList.Visibility = Visibility.Visible;
            PanelAddMedServ.Visibility = Visibility.Collapsed;
        }

        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            PanelServicesList.Visibility = Visibility.Visible;
            PanelAddMedServ.Visibility = Visibility.Collapsed;
        }

        private async void BtnAddPic_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            file = await openPicker.PickSingleFileAsync();


            if (file != null)
            {
                string fileToken = StorageApplicationPermissions.FutureAccessList.Add(file);

                PanelPhotoName.Visibility = Visibility.Visible;
                Overlay.Visibility = Visibility.Visible;
            }
        }

        private async void BtnAddDoc_Click(object sender, RoutedEventArgs e)
        {

            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".doc");
            openPicker.FileTypeFilter.Add(".docx");
            openPicker.FileTypeFilter.Add(".pdf");
            file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                string fileToken = StorageApplicationPermissions.FutureAccessList.Add(file);

                PanelDocName.Visibility = Visibility.Visible;
                Overlay.Visibility = Visibility.Visible;

            }
        }

        private int countMedInStock(Medicine medicine)
        {
            int counter = 0;

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"SELECT * FROM stock WHERE MedicationID = '{medicine.id}'";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string qty = reader["QuantityAvailable"].ToString();

                counter = int.Parse(qty);
            }

            conn.Close();

            return counter;
        }

        // Add Medicine to list
        private void ListAddMed_ItemClick(object sender, ItemClickEventArgs e)
        {
            var med = (Medicine)e.ClickedItem;
            int medCount = med.quantity;

            if (medCount > 0)
            {
                if (medExistsInList(med))
                {
                    //Change quantity to +1 and add to list
                    incrementMedQty(med);
                }
                else
                {
                    patientMedList.Add(new Medicine(med.id, med.name, 1, med.price));
                }

                med.quantity = medCount - 1;
                PanelAddMedServ.Visibility = Visibility.Visible;
                PanelMedList.Visibility = Visibility.Collapsed;
                NoMedsPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "The selected item is out of stock.";
                Overlay.Visibility = Visibility.Visible;
            }


        }

        // Add Service to list
        private void ListAddService_ItemClick(object sender, ItemClickEventArgs e)
        {
            var serv = (Service)e.ClickedItem;

            if (serviceExists(serv))
            {
                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "The selected service has already been added.";
                Overlay.Visibility = Visibility.Visible;
            }
            else
            {
                patientServiceList.Add(serv);
            }

            PanelAddMedServ.Visibility = Visibility.Visible;
            PanelServicesList.Visibility = Visibility.Collapsed;
            NoServicePanel.Visibility = Visibility.Collapsed;
        }

        // Add Assessment
        private void BtnNewAssessment_Click(object sender, RoutedEventArgs e)
        {
            changeComponentVisibility();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void BtnToggleNav_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        // FILE NAME ENTERED BUTTONS
        private void BtnDocNameDone_Click(object sender, RoutedEventArgs e)
        {

            if (TxtDocName.Text.Equals(""))
            {
                TxtErrorMessage3.Text = "Please enter a name for the document.";
            }
            else
            {
                TxtErrorMessage3.Text = "";

                fileName = TxtDocName.Text + file.FileType;

                docsCollection.Add(new VisitDocumentInformation(file, fileName));

                PanelDocName.Visibility = Visibility.Collapsed;
                Overlay.Visibility = Visibility.Collapsed;

                TxtDocName.Text = string.Empty;

                NoDocsPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnPhotoNameDone_Click(object sender, RoutedEventArgs e)
        {

            if (TxtPhotoName.Text.Equals(""))
            {
                TxtErrorMessage4.Text = "Please enter a name for the photo.";
            }
            else
            {
                TxtErrorMessage4.Text = "";

                fileName = TxtPhotoName.Text + file.FileType;

                photosCollection.Add(new VisitDocumentInformation(file, fileName));

                PanelPhotoName.Visibility = Visibility.Collapsed;
                Overlay.Visibility = Visibility.Collapsed;

                TxtPhotoName.Text = string.Empty;

                NoPhotosPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnCancelMedList_Click(object sender, RoutedEventArgs e)
        {
            PanelMedList.Visibility = Visibility.Collapsed;
            PanelAddMedServ.Visibility = Visibility.Visible;
        }

        private void BtnCancelServicesList_Click(object sender, RoutedEventArgs e)
        {
            PanelServicesList.Visibility = Visibility.Collapsed;
            PanelAddMedServ.Visibility = Visibility.Visible;
        }

        // GENERATE RECEIPT

        private void BtnReceipt_Click(object sender, RoutedEventArgs e)
        {
            if (validateVisitDetails())
            {
                PanelDialog2.Visibility = Visibility.Visible;
                TxtDialogMessage2.Text = "Please confirm that all details have been entered correctly. " +
                    "This action cannot be undone.";
            }

        }

        private async void BtnDialogOK2_Click(object sender, RoutedEventArgs e)
        {
            string incomeId = saveIncome();

            thisVisit = createPatientVisit(thisAppointment, incomeId);

            addPatientVisit(thisVisit);
            addPatientMedication(thisVisit.id);
            addPatientServices(thisVisit.id);

            updateStock();

            await createFolders();
            await addPatientDocuments();
            await addPatientPhotos();

            if (thisAssessment != null)
            {
                insertVisitAssessment(thisVisit);
            }

            double totalPayment = getTotalPayment();

            ReceiptInformation info = new ReceiptInformation(thisVisit, patientMedList, patientServiceList, totalPayment);

            Frame.Navigate(typeof(Receipt), info);
        }

        private void BtnDialogCancel2_Click(object sender, RoutedEventArgs e)
        {
            PanelDialog2.Visibility = Visibility.Collapsed;
        }
        #endregion

        // ***************************************** ASSESSMENT ********************************************
        #region Assessment

        // CHANGE PANELS
        private void changeComponentVisibility()
        {
            PanelAssessment.Visibility = Visibility.Visible;
            MainSplitView.Visibility = Visibility.Collapsed;

            switch (thisAppointment.type)
            {
                case "Burn":
                    Row6.Visibility = Visibility.Collapsed;
                    Row7.Visibility = Visibility.Collapsed;
                    Row8.Visibility = Visibility.Collapsed;
                    Row9.Visibility = Visibility.Collapsed;
                    Row18.Visibility = Visibility.Collapsed;
                    Row19.Visibility = Visibility.Collapsed;
                    Row20.Visibility = Visibility.Collapsed;
                    Row21.Visibility = Visibility.Collapsed;
                    Row22.Visibility = Visibility.Collapsed;
                    break;
                case "Chronic Ulcer":
                    Row2.Visibility = Visibility.Collapsed;
                    Row6.Visibility = Visibility.Collapsed;
                    Row7.Visibility = Visibility.Collapsed;
                    Row9.Visibility = Visibility.Collapsed;
                    Row17.Visibility = Visibility.Collapsed;
                    Row18.Visibility = Visibility.Collapsed;
                    Row19.Visibility = Visibility.Collapsed;
                    Row21.Visibility = Visibility.Collapsed;
                    break;
                case "Diabetic Foot":
                    Row2.Visibility = Visibility.Collapsed;
                    Row17.Visibility = Visibility.Collapsed;
                    break;

                default:
                    break;
            }
        }


        // *************** CREATE VISITS *******************
        private void createVisitAssessment()
        {
            // Get values from common variables
            string diabetes = convertToggleValue(ToggleDiabetes.IsOn) + ". " + TxtCommentDiabetes.Text;
            string purpose = TxtPurpose.Text;
            string hypertension = convertToggleValue(ToggleHypertension.IsOn) + ". " + TxtCommentHypertension.Text;
            string ihd = convertToggleValue(ToggleIHD.IsOn) + ". " + TxtCommentIHD.Text;
            string asthma = convertToggleValue(ToggleAsthma.IsOn) + ". " + TxtCommentAsthma.Text;
            string thyroid = convertToggleValue(ToggleThyroid.IsOn) + ". " + TxtCommentThyroid.Text;
            string cva = convertToggleValue(ToggleCVA.IsOn) + ". " + TxtCommentCVA.Text;
            string dvt = convertToggleValue(ToggleDVT.IsOn) + ". " + TxtCommentDVT.Text;
            string allergy = convertToggleValue(ToggleAllergy.IsOn) + ". " + TxtCommentAllergy.Text;
            string smoking = convertToggleValue(ToggleSmoking.IsOn) + ". " + TxtCommentSmoking.Text;
            string pallor = convertToggleValue(TogglePallor.IsOn) + ". " + TxtCommentPallor.Text;
            string jaundice = convertToggleValue(ToggleJaundice.IsOn) + ". " + TxtCommentJaundice.Text;
            string clubbing = convertToggleValue(ToggleClubbing.IsOn) + ". " + TxtCommentClubbing.Text;
            string pulses = convertToggleValue(TogglePulses.IsOn) + ". " + TxtCommentPulses.Text;
            string cardiovascular = convertToggleValue(ToggleCardiovascular.IsOn) + ". " + TxtCommentCardiovascular.Text;
            string respiratory = convertToggleValue(ToggleRespiratory.IsOn) + ". " + TxtCommentRespiratory.Text;
            string abdominal = convertToggleValue(ToggleAbdominal.IsOn) + ". " + TxtCommentAbdominal.Text;
            string neurological = convertToggleValue(ToggleNeurological.IsOn) + ". " + TxtCommentNeurological.Text;
            string bp = TxtBP.Text + ". " + TxtCommentBP.Text;
            string pulse = TxtPulse.Text + ". " + TxtCommentPulse.Text;
            string temperature = TxtTemp.Text + ". " + TxtCommentTemperature.Text;
            string rbs = TxtRBS.Text + ". " + TxtCommentRBS.Text;
            string site = TxtSite.Text;
            string size = TxtSize.Text;
            string depth = TxtDepth.Text;
            string woundexudate = TxtWoundExudate.Text;

            VisitAssessment commonVisit = new VisitAssessment("", "", purpose, diabetes, hypertension,
                ihd, asthma, thyroid, cva, dvt, allergy, smoking, pallor, jaundice, clubbing, pulses, cardiovascular,
                respiratory, abdominal, neurological, bp, pulse, temperature, rbs, site, size, depth, woundexudate);

            switch (thisAppointment.type)
            {
                case "Burn":
                    thisAssessment = createBurnsVisit(commonVisit);
                    break;
                case "Chronic Ulcer":
                    thisAssessment = createUlcerVisit(commonVisit);
                    break;
                case "Diabetic Foot":
                    thisAssessment = createDiabeticFootVisit(commonVisit);
                    break;
                default:
                    break;
            }
        }

        private BurnsVisit createBurnsVisit(VisitAssessment commonVisit)
        {
            string id = IDManager.getNextBurnsVisitID();
            string description = TxtDescription.Text;
            string manner = TxtManner.Text;
            string circumferential = TxtCircumferential.Text;
            string circulation = TxtCirculation.Text;

            BurnsVisit visit = new BurnsVisit(description, manner, circumferential, circulation, id,
                commonVisit.PatientVisitID, commonVisit.Purpose, commonVisit.DiabetesMellitus, commonVisit.Hypertension,
                commonVisit.IHD, commonVisit.Asthma, commonVisit.Thyroid, commonVisit.CVA, commonVisit.DVT,
                commonVisit.Allergy, commonVisit.Smoking, commonVisit.Pallor, commonVisit.Jaundice,
                commonVisit.Clubbing, commonVisit.Pulses, commonVisit.Cardiovascular, commonVisit.Respiratory,
                commonVisit.Abdominal, commonVisit.Neurological, commonVisit.BP, commonVisit.Pulse,
                commonVisit.Temperature, commonVisit.RBS, commonVisit.Site, commonVisit.Size, commonVisit.Depth,
                commonVisit.Exudate);

            return visit;
        }

        private ChronicUlcerVisit createUlcerVisit(VisitAssessment commonVisit)
        {
            string id = IDManager.getNextUlcerID();
            string durationUlcer = TxtDurationUlcer.Text + ". " + TxtCommentDurationUlcer.Text;
            string treatment = convertToggleValue(ToggleTreatment.IsOn) + ". " + TxtCommentTreatment.Text;
            string undermining = TxtUndermining.Text;
            string surface = TxtWoundSurface.Text;
            string periwound = TxtPeriwound.Text;

            ChronicUlcerVisit visit = new ChronicUlcerVisit(durationUlcer, treatment, id,
                commonVisit.PatientVisitID, commonVisit.Purpose, commonVisit.DiabetesMellitus, commonVisit.Hypertension,
                commonVisit.IHD, commonVisit.Asthma, commonVisit.Thyroid, commonVisit.CVA, commonVisit.DVT,
                commonVisit.Allergy, commonVisit.Smoking, commonVisit.Pallor, commonVisit.Jaundice,
                commonVisit.Clubbing, commonVisit.Pulses, commonVisit.Cardiovascular, commonVisit.Respiratory,
                commonVisit.Abdominal, commonVisit.Neurological, commonVisit.BP, commonVisit.Pulse,
                commonVisit.Temperature, commonVisit.RBS, commonVisit.Site, commonVisit.Size, commonVisit.Depth,
                undermining, surface, periwound, commonVisit.Exudate);

            return visit;
        }

        private DiabeticFootVisit createDiabeticFootVisit(VisitAssessment commonVisit)
        {
            string id = IDManager.getNextDiabeticFootID();
            string durationUlcer = TxtDurationUlcer.Text;
            string treatment = convertToggleValue(ToggleTreatment.IsOn) + ". " + TxtCommentTreatment.Text;
            string claudication = convertToggleValue(ToggleClaudication.IsOn) + ". " + TxtCommentClaudication.Text;
            string painAtRest = convertToggleValue(TogglePainAtRest.IsOn) + ". " + TxtCommentPainAtRest.Text;
            string hypesthesia = convertToggleValue(ToggleHypesthesia.IsOn) + ". " + TxtCommentHypesthesia.Text;
            string hyperesthesia = convertToggleValue(ToggleHyperesthesia.IsOn) + ". " + TxtCommentHyperesthesia.Text;
            string paraesthesia = convertToggleValue(ToggleParaesthesia.IsOn) + ". " + TxtCommentParaesthesia.Text;
            string dysesthesia = convertToggleValue(ToggleDysesthesia.IsOn) + ". " + TxtCommentDysesthesia.Text;
            string radPain = convertToggleValue(ToggleRadPain.IsOn) + ". " + TxtCommentRadPain.Text;
            string anhydrosis = convertToggleValue(ToggleAnhydrosis.IsOn) + ". " + TxtCommentAnhydrosis.Text;
            string undermining = TxtUndermining.Text;
            string surface = TxtWoundSurface.Text;
            string periwound = TxtPeriwound.Text;
            string callus = TxtCallus.Text;
            string brittleNail = TxtBrittleNail.Text;
            string hammertoe = TxtHammerToe.Text;
            string fissures = TxtFissures.Text;
            string lossHairGrowth = TxtLossHairGrowth.Text;
            string cyanosis = TxtCyanosis.Text;
            string pallorfoot = TxtPallorFoot.Text;

            DiabeticFootVisit visit = new DiabeticFootVisit(durationUlcer, treatment, claudication, painAtRest,
                hyperesthesia, hyperesthesia, paraesthesia, dysesthesia, radPain, anhydrosis, undermining, surface,
                periwound, callus, brittleNail, hammertoe, fissures, lossHairGrowth, cyanosis, pallorfoot, id,
                commonVisit.PatientVisitID, commonVisit.Purpose, commonVisit.DiabetesMellitus, commonVisit.Hypertension,
                commonVisit.IHD, commonVisit.Asthma, commonVisit.Thyroid, commonVisit.CVA, commonVisit.DVT,
                commonVisit.Allergy, commonVisit.Smoking, commonVisit.Pallor, commonVisit.Jaundice,
                commonVisit.Clubbing, commonVisit.Pulses, commonVisit.Cardiovascular, commonVisit.Respiratory,
                commonVisit.Abdominal, commonVisit.Neurological, commonVisit.BP, commonVisit.Pulse,
                commonVisit.Temperature, commonVisit.RBS, commonVisit.Site, commonVisit.Size, commonVisit.Depth,
                commonVisit.Exudate);

            return visit;
        }

        // ************* TOGGLE CONVERSIONS ****************

        private string convertToggleValue(bool isOn)
        {
            if (isOn)
                return "Yes";
            else
                return "No";
        }

        private bool convertStringToToggle(string val)
        {
            bool isOn = false;

            if (val.Substring(0, 3) == "Yes")
                isOn = true;
            else if (val.Substring(0, 2) == "No")
                isOn = false;

            return isOn;
        }

        // *************** VALIDATION *******************

        private bool validateFields()
        {
            bool validity = false;

            if (checkCommonFields())
            {
                switch (thisAppointment.type)
                {
                    case "Burn":
                        validity = checkBurn();
                        break;
                    case "Chronic Ulcer":
                        validity = checkUlcer();
                        break;
                    case "Diabetic Foot":
                        validity = checkDiabetic();
                        break;
                    default:
                        break;
                }
            }

            return validity;

        }

        private bool checkCommonFields()
        {

            if (TxtPurpose.Text.Equals("") || TxtBP.Text.Equals("") || TxtPulse.Text.Equals("") ||
                TxtTemp.Text.Equals("") || TxtRBS.Text.Equals("") || TxtSite.Text.Equals("") ||
                TxtSize.Text.Equals("") || TxtDepth.Text.Equals("") || TxtWoundExudate.Text.Equals(""))
            {
                PanelError2.Visibility = Visibility.Visible;
                TxtErrorMessage2.Text = "Please complete all required fields.";
                return false;
            }
            else
            {
                PanelError2.Visibility = Visibility.Collapsed;
                return true;
            }
        }

        private bool checkBurn()
        {

            if (TxtCircumferential.Text.Equals("") || TxtCirculation.Text.Equals(""))
            {
                PanelError2.Visibility = Visibility.Visible;
                TxtErrorMessage2.Text = "Please complete all required fields.";
                return false;
            }
            else
            {
                PanelError2.Visibility = Visibility.Collapsed;
                return true;
            }
        }

        private bool checkUlcer()
        {
            if (TxtDurationUlcer.Text.Equals("") || TxtUndermining.Text.Equals("") ||
                TxtWoundSurface.Text.Equals("") || TxtPeriwound.Text.Equals(""))
            {
                PanelError2.Visibility = Visibility.Visible;
                TxtErrorMessage2.Text = "Please complete all required fields.";
                return false;
            }
            else
            {
                PanelError2.Visibility = Visibility.Collapsed;
                return true;
            }

        }

        private bool checkDiabetic()
        {
            if (TxtDurationUlcer.Text.Equals("") || TxtUndermining.Text.Equals("") ||
                TxtWoundSurface.Text.Equals("") || TxtPeriwound.Text.Equals("") ||
                TxtCallus.Text.Equals("") || TxtBrittleNail.Text.Equals("") ||
                TxtHammerToe.Text.Equals("") || TxtFissures.Text.Equals("") ||
                TxtLossHairGrowth.Text.Equals("") || TxtCyanosis.Text.Equals("") ||
                TxtPallorFoot.Text.Equals(""))
            {
                PanelError2.Visibility = Visibility.Visible;
                TxtErrorMessage2.Text = "Please complete all required fields.";
                return false;
            }
            else
            {
                PanelError2.Visibility = Visibility.Collapsed;
                return true;
            }
        }


        // *************** BUTTONS **********************

        // CANCEL
        private void BtnCancelAssessment_Click(object sender, RoutedEventArgs e)
        {
            PanelAssessment.Visibility = Visibility.Collapsed;
            MainSplitView.Visibility = Visibility.Visible;
        }

        // ADD
        private void BtnAddAssessment_Click(object sender, RoutedEventArgs e)
        {
            if (validateFields())
            {
                createVisitAssessment();
                PanelAssessment.Visibility = Visibility.Collapsed;
                MainSplitView.Visibility = Visibility.Visible;
                BtnRemoveAssessment.Visibility = Visibility.Visible;

            }

        }

        private void BtnErrorClose_Click(object sender, RoutedEventArgs e)
        {
            PanelError.Visibility = Visibility.Collapsed;
        }

        private void BtnErrorClose2_Click(object sender, RoutedEventArgs e)
        {
            PanelError2.Visibility = Visibility.Collapsed;
        }

        private void BtnRemovePhoto_Click(object sender, RoutedEventArgs e)
        {

            photosCollection.Remove((VisitDocumentInformation)ListPatientPhotos.SelectedItem);
        }

        private void BtnRemoveDoc_Click(object sender, RoutedEventArgs e)
        {

            docsCollection.Remove((VisitDocumentInformation)ListPatientDocs.SelectedItem);
        }

        private void BtnRemoveMed_Click(object sender, RoutedEventArgs e)
        {
            patientMedList.Remove((Medicine)ListPatientMeds.SelectedItem);
        }

        private void BtnRemoveService_Click(object sender, RoutedEventArgs e)
        {
            patientServiceList.Remove((Service)ListPatientServices.SelectedItem);
        }

        private void BtnRemoveAssessment_Click(object sender, RoutedEventArgs e)
        {
            thisAssessment = null;
            BtnRemoveAssessment.Visibility = Visibility.Collapsed;
            PanelAssessment.Visibility = Visibility.Collapsed;
            MainSplitView.Visibility = Visibility.Visible;
            clearAssessmentValues();
        }

        private void clearAssessmentValues()
        {
            TxtDurationUlcer.Text = string.Empty;
            TxtUndermining.Text = string.Empty;
            TxtWoundSurface.Text = string.Empty;
            TxtPeriwound.Text = string.Empty;
            TxtCallus.Text = string.Empty;
            TxtBrittleNail.Text = string.Empty;
            TxtHammerToe.Text = string.Empty;
            TxtFissures.Text = string.Empty;
            TxtLossHairGrowth.Text = string.Empty;
            TxtCyanosis.Text = string.Empty;
            TxtPallorFoot.Text = string.Empty;

            TxtCircumferential.Text = string.Empty;
            TxtCirculation.Text = string.Empty;

            TxtPurpose.Text = string.Empty;
            TxtBP.Text = string.Empty;
            TxtPulse.Text = string.Empty;
            TxtTemp.Text = string.Empty;
            TxtRBS.Text = string.Empty;
            TxtSite.Text = string.Empty;
            TxtSize.Text = string.Empty;
            TxtDepth.Text = string.Empty;
            TxtWoundExudate.Text = string.Empty;

            TxtCommentAbdominal.Text = string.Empty;
            TxtCommentAllergy.Text = string.Empty;
            TxtCommentAnhydrosis.Text = string.Empty;
            TxtCommentAsthma.Text = string.Empty;
            TxtCommentBP.Text = string.Empty;
            TxtCommentCardiovascular.Text = string.Empty;
            TxtCommentClaudication.Text = string.Empty;
            TxtCommentClubbing.Text = string.Empty;
            TxtCommentCVA.Text = string.Empty;
            TxtCommentDiabetes.Text = string.Empty;
            TxtCommentDurationUlcer.Text = string.Empty;
            TxtCommentDVT.Text = string.Empty;
            TxtCommentDysesthesia.Text = string.Empty;
            TxtCommentHyperesthesia.Text = string.Empty;
            TxtCommentHypertension.Text = string.Empty;
            TxtCommentHypesthesia.Text = string.Empty;
            TxtCommentIHD.Text = string.Empty;
            TxtCommentJaundice.Text = string.Empty;
            TxtCommentNeurological.Text = string.Empty;
            TxtCommentPainAtRest.Text = string.Empty;
            TxtCommentPallor.Text = string.Empty;
            TxtCommentParaesthesia.Text = string.Empty;
            TxtCommentPulse.Text = string.Empty;
            TxtCommentPulses.Text = string.Empty;
            TxtCommentRadPain.Text = string.Empty;
            TxtCommentRBS.Text = string.Empty;
            TxtCommentRespiratory.Text = string.Empty;
            TxtCommentSmoking.Text = string.Empty;
            TxtCommentTemperature.Text = string.Empty;
            TxtCommentThyroid.Text = string.Empty;
            TxtCommentTreatment.Text = string.Empty;

            ToggleAbdominal.IsOn = false;
            ToggleAllergy.IsOn = false;
            ToggleAnhydrosis.IsOn = false;
            ToggleAsthma.IsOn = false;
            ToggleCardiovascular.IsOn = false;
            ToggleClaudication.IsOn = false;
            ToggleClubbing.IsOn = false;
            ToggleCVA.IsOn = false;
            ToggleDiabetes.IsOn = false;
            ToggleDVT.IsOn = false;
            ToggleDysesthesia.IsOn = false;
            ToggleHyperesthesia.IsOn = false;
            ToggleHypertension.IsOn = false;
            ToggleHypesthesia.IsOn = false;
            ToggleIHD.IsOn = false;
            ToggleJaundice.IsOn = false;
            ToggleNeurological.IsOn = false;
            TogglePainAtRest.IsOn = false;
            TogglePallor.IsOn = false;
            ToggleParaesthesia.IsOn = false;
            TogglePulses.IsOn = false;
            ToggleRadPain.IsOn = false;
            ToggleRespiratory.IsOn = false;
            ToggleSmoking.IsOn = false;
            ToggleThyroid.IsOn = false;
            ToggleTreatment.IsOn = false;
        }

        private void BtnDialogOK_Click(object sender, RoutedEventArgs e)
        {
            PanelDialog.Visibility = Visibility.Collapsed;
            Overlay.Visibility = Visibility.Collapsed;
        }
    }
    #endregion
}
