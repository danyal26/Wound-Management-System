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
using System.Text.RegularExpressions;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WoundManagementSystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PatientPage : Page
    {
        private ObservableCollection<Patient> patientList = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> filteredList = new ObservableCollection<Patient>();

        private ObservableCollection<PatientVisit> visitList = new ObservableCollection<PatientVisit>();

        private ObservableCollection<string> titleList = new ObservableCollection<string>();

        private Appointment thisAppt = null;

        public PatientPage()
        {
            this.InitializeComponent();
            getPatientList();

            titleList.Add("Mr");
            titleList.Add("Mrs");
            titleList.Add("Ms");
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Appointment)
            {
                thisAppt = (Appointment)e.Parameter;

                DetailsPanel.Visibility = Visibility.Collapsed;
                AddPanel.Visibility = Visibility.Visible;
                BtnModifyPatient.Visibility = Visibility.Collapsed;
                NoSelectionPanel.Visibility = Visibility.Collapsed;
                BtnAdd.Visibility = Visibility.Visible;
                ListViewPatient.IsEnabled = false;
                BtnAddPatient.IsEnabled = false;
                TxtSearch.IsEnabled = false;
                BtnCancelPatientDetails.Visibility = Visibility.Collapsed;

                LblMainTitle.Text = "Add Patient";

                TxtPatientName.Text = thisAppt.name;
                TxtPatientSurname.Text = thisAppt.surname;
                TxtPatientContact.Text = thisAppt.contact;

            }
            else
            {
                DetailsPanel.Visibility = Visibility.Collapsed;
                AddPanel.Visibility = Visibility.Collapsed;
                NoSelectionPanel.Visibility = Visibility.Visible;
            }
        }

        private void getPatientList()
        {
            patientList.Clear();

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM patients;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string patientId = reader["PatientID"].ToString();
                string title = reader["Title"].ToString();
                string name = reader["Name"].ToString();
                string surname = reader["Surname"].ToString();
                string dateOfBirth = reader["DateOfBirth"].ToString();
                string sex = reader["Sex"].ToString();
                string address = reader["Address"].ToString();
                string telNo = reader["Contact"].ToString();
                string email = reader["Email"].ToString();
                string occupation = reader["Occupation"].ToString();


                Patient patient = new Patient(patientId, title, name, surname, dateOfBirth, sex, address, telNo, email, occupation);

                patientList.Add(patient);
            }

            conn.Close();
        }

        private void getVisitList(Patient patient)
        {
            visitList.Clear();

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"SELECT * FROM patient_visits WHERE PatientID = '{patient.patientId}' " +
                $"ORDER BY `Date` DESC;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string id = reader["PatientVisitID"].ToString();
                string patientId = reader["PatientID"].ToString();
                string notes = reader["Notes"].ToString();
                string date = reader["Date"].ToString();
                string time = reader["Time"].ToString();
                string incomeId = reader["IncomeID"].ToString();
                string afterOpening = reader["AfterOpening"].ToString();
                string actionsPerformed = reader["ActionsPerformed"].ToString();
                string beforeClosing = reader["BeforeClosing"].ToString();
                string type = reader["Type"].ToString();

                PatientVisit visit = new PatientVisit(id, patientId, date, time, notes, incomeId, afterOpening, actionsPerformed, beforeClosing, type);
                visitList.Add(visit);
            }

            conn.Close();
        }

        // PATIENT LIST SELECT
        private void ListViewPatient_ItemClick(object sender, ItemClickEventArgs e)
        {
            DetailsPanel.Visibility = Visibility.Visible;
            AddPanel.Visibility = Visibility.Collapsed;
            NoSelectionPanel.Visibility = Visibility.Collapsed;

            Patient patient = (Patient)e.ClickedItem;

            getVisitList(patient);

            LblPatientName.Text = patient.name + " " + patient.surname;
            LblPatientID.Text = patient.patientId;
            LblPatientTitle.Text = patient.title;
            LblDOB.Text = DateTimeManager.DateToShortYear(patient.dateOfBirth);

            LblSex.Text = patient.sex;
            LblPatientAddress.Text = patient.address;
            LblPatientContact.Text = patient.contact;
            LblPatientEmail.Text = patient.email;
            LblPatientOccupation.Text = patient.occupation;
        }

        private void BtnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            DetailsPanel.Visibility = Visibility.Collapsed;
            AddPanel.Visibility = Visibility.Visible;
            BtnModifyPatient.Visibility = Visibility.Collapsed;
            NoSelectionPanel.Visibility = Visibility.Collapsed;
            BtnAdd.Visibility = Visibility.Visible;
            ListViewPatient.IsEnabled = false;
            BtnAddPatient.IsEnabled = false;
            TxtSearch.IsEnabled = false;
            BtnCancelPatientDetails.Visibility = Visibility.Collapsed;
            BtnViewArchived.IsEnabled = false;

            LblMainTitle.Text = "Add Patient";
        }

        private void BtnToggleNav_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        private void BtnModify_Click(object sender, RoutedEventArgs e)
        {

            LblMainTitle.Text = "Modify Patient";

            DetailsPanel.Visibility = Visibility.Collapsed;
            AddPanel.Visibility = Visibility.Visible;

            BtnModifyPatient.Visibility = Visibility.Visible;
            BtnAdd.Visibility = Visibility.Collapsed;

            BtnCancelPatientDetails.Visibility = Visibility.Collapsed;

            ListViewPatient.IsEnabled = false;
            BtnAddPatient.IsEnabled = false;
            TxtSearch.IsEnabled = false;
            BtnViewArchived.IsEnabled = false;

            Patient pat = (Patient)ListViewPatient.SelectedItem;

            CmbTitle.SelectedValue = pat.title;
            TxtPatientSurname.Text = pat.surname;
            TxtPatientName.Text = pat.name;

            DpPatientDOB.Date = DateTimeManager.convertToDate(pat.dateOfBirth);

            if (pat.sex.Equals("Male"))
            {
                RadioSexM.IsChecked = true;
            }
            else
            {
                RadioSexF.IsChecked = true;
            }

            TxtPatientAddress.Text = pat.address;
            TxtPatientContact.Text = pat.contact;
            TxtPatientEmail.Text = pat.email;
            TxtPatientOccupation.Text = pat.occupation;


        }

        private void BtnModifyPatient_Click(object sender, RoutedEventArgs e)
        {

            ListViewPatient.IsEnabled = false;
            BtnAddPatient.IsEnabled = false;
            TxtSearch.IsEnabled = false;

            Patient pat = (Patient)ListViewPatient.SelectedItem;

            if (validatePatient())
            {
                pat.title = CmbTitle.SelectedValue.ToString();
                pat.name = TxtPatientName.Text;
                pat.surname = TxtPatientSurname.Text;
                pat.dateOfBirth = DateTimeManager.convertDate(DpPatientDOB.Date.ToString());

                if ((bool)RadioSexM.IsChecked)
                {
                    pat.sex = "Male";
                }
                if ((bool)RadioSexF.IsChecked)
                {
                    pat.sex = "Female";
                }

                pat.address = TxtPatientAddress.Text;
                pat.contact = TxtPatientContact.Text;
                pat.email = TxtPatientEmail.Text;
                pat.occupation = TxtPatientOccupation.Text;

                modifyPatient(pat);

                clearFormValues();

                // Dialog
                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "Patient has been modified";
                Overlay.Visibility = Visibility.Visible;

                DetailsPanel.Visibility = Visibility.Collapsed;
                AddPanel.Visibility = Visibility.Collapsed;
                NoSelectionPanel.Visibility = Visibility.Visible;
                BtnCancelPatientDetails.Visibility = Visibility.Visible;
                BtnViewArchived.IsEnabled = true;

                ListViewPatient.IsEnabled = true;
                BtnAddPatient.IsEnabled = true;
                TxtSearch.IsEnabled = true;

                LblMainTitle.Text = "Patients";

            }
        }

        private void modifyPatient(Patient pat)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = $"Update patients SET Title='{pat.title}', Name='{pat.name}'," +
                $"Surname='{pat.surname}', DateOfBirth='{pat.dateOfBirth}', Sex='{pat.sex}'," +
                $"Address='{pat.address}', Contact='{pat.contact}', Email='{pat.email}'," +
                $"Occupation='{pat.occupation}' WHERE PatientId='{pat.patientId}';";


            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            int index = -1;

            foreach (var patient in patientList)
            {
                if (patient.patientId.Equals(pat.patientId))
                {
                    index = patientList.IndexOf(patient);
                }
            }

            if (index >= 0)
            {
                patientList[index] = pat;
            }
        }

        private void removePatient(Patient pat)
        {

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry2 = "DELETE FROM patients WHERE PatientID ='" + pat.patientId + "';";


            MySqlCommand cmd = new MySqlCommand(qry2, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        private void BtnArchive_Click(object sender, RoutedEventArgs e)
        {
            // Dialog 2
            PanelDialog2.Visibility = Visibility.Visible;
            TxtDialogMessage2.Text = "Are you sure you want to remove this patient?";
            Overlay.Visibility = Visibility.Visible;
        }

        private void addPatientAppointment(Patient patient)
        {
            thisAppt.patientId = patient.patientId;

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"INSERT INTO patient_appointments VALUES ('{IDManager.getNextPatientApptID()}','{thisAppt.appointmentId}','{patient.patientId}')";


            MySqlCommand cmd = new MySqlCommand(qry, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (validatePatient())
            {
                Patient patient = createPatient();
                addPatient(patient);

                if (thisAppt != null)
                {
                    addPatientAppointment(patient);
                }

                clearFormValues();

                //CustomMessageDialog.createDialog("Patient Saved","OK","Patient has been saved.");

                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "Patient has been added";
                Overlay.Visibility = Visibility.Visible;

                if (ListViewPatient.SelectedItem != null)
                {
                    DetailsPanel.Visibility = Visibility.Visible;
                    NoSelectionPanel.Visibility = Visibility.Collapsed;
                }
                else
                {
                    DetailsPanel.Visibility = Visibility.Collapsed;
                    NoSelectionPanel.Visibility = Visibility.Visible;
                }

                AddPanel.Visibility = Visibility.Collapsed;

                patientList.Add(patient);

                ListViewPatient.IsEnabled = true;
                BtnAddPatient.IsEnabled = true;
                TxtSearch.IsEnabled = true;
                BtnCancelPatientDetails.Visibility = Visibility.Visible;
                LblMainTitle.Text = "Patients";
                BtnViewArchived.IsEnabled = true;
            }

        }

        // ADD PATIENT TO DB
        private void addPatient(Patient pat)
        {

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = "INSERT INTO patients VALUES('" + pat.patientId + "','" +
                            pat.title + "','" + pat.name + "','" + pat.surname + "','" + pat.dateOfBirth + "','"

                            + pat.sex + "','" + pat.address + "','" + pat.contact + "','" + pat.email + "','" + pat.occupation + "')";

            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        // CREATE PATIENT
        private Patient createPatient()
        {
            string sex = "";
            string date = DateTimeManager.convertDate(DpPatientDOB.Date.ToString());

            if ((bool)RadioSexM.IsChecked)
            {
                sex = "Male";
            }
            if ((bool)RadioSexF.IsChecked)
            {
                sex = "Female";
            }

            Patient pat = new Patient(IDManager.getNextPatientID(), CmbTitle.SelectedValue.ToString(),
                TxtPatientName.Text, TxtPatientSurname.Text, date, sex, TxtPatientAddress.Text,
                TxtPatientContact.Text, TxtPatientEmail.Text, TxtPatientOccupation.Text);

            return pat;
        }

        // VALIDATE ADD PATIENT FIELDS

        private bool validatePatient()
        {
            bool numberValidity = true;
            bool emailValidity = Regex.IsMatch(TxtPatientEmail.Text,
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                RegexOptions.IgnoreCase);

            if (!long.TryParse(TxtPatientContact.Text, out long contact))
            {
                numberValidity = false;
            }

            if (CmbTitle.SelectedValue == null || CmbTitle.SelectedItem == null || CmbTitle.SelectedValue.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please select a title.";
                return false;
            }
            else if (TxtPatientName.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please enter a name.";
                return false;
            }
            else if (TxtPatientSurname.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please enter a surname.";
                return false;
            }
            else if (!DpPatientDOB.Date.HasValue)
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please select a date of birth.";
                return false;
            }
            else if (!(bool)RadioSexM.IsChecked && !(bool)RadioSexF.IsChecked)
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please select a sex.";
                return false;
            }
            else if (TxtPatientContact.Text.Equals(""))
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
            else if (TxtPatientAddress.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please enter an address.";
                return false;
            }
            else if (TxtPatientEmail.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please enter an email address.";
                return false;
            }
            else if (emailValidity == false)
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please enter a valid email address.";
                return false;
            }
            else if (TxtPatientOccupation.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please enter an occupation.";
                return false;
            }
            else
            {
                PanelError.Visibility = Visibility.Collapsed;
                return true;
            }
        }

        // CLEAR VALUES
        private void clearFormValues()
        {
            TxtPatientName.Text = String.Empty;
            TxtPatientSurname.Text = String.Empty;
            TxtPatientAddress.Text = String.Empty;
            TxtPatientContact.Text = String.Empty;
            TxtPatientEmail.Text = String.Empty;
            CmbTitle.SelectedIndex = -1;
            TxtPatientOccupation.Text = String.Empty;
            DpPatientDOB.Date = null;
            RadioSexF.IsChecked = false;
            RadioSexM.IsChecked = false;

        }

        private void BtnCancelAddPatient_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewPatient.SelectedItem != null)
            {
                DetailsPanel.Visibility = Visibility.Visible;
                NoSelectionPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                DetailsPanel.Visibility = Visibility.Collapsed;
                NoSelectionPanel.Visibility = Visibility.Visible;
            }


            AddPanel.Visibility = Visibility.Collapsed;

            BtnCancelPatientDetails.Visibility = Visibility.Visible;
            BtnViewArchived.IsEnabled = true;

            clearFormValues();

            LblMainTitle.Text = "Patients";

            ListViewPatient.IsEnabled = true;
            BtnAddPatient.IsEnabled = true;
            TxtSearch.IsEnabled = true;

            PanelError.Visibility = Visibility.Collapsed;
        }

        private void BtnCancelPatientDetails_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Dashboard));
        }

        private void BtnViewVisits_Click(object sender, RoutedEventArgs e)
        {
            DetailsPanel.Visibility = Visibility.Collapsed;
            VisitListPanel.Visibility = Visibility.Visible;
        }

        private void ListViewVisits_ItemClick(object sender, ItemClickEventArgs e)
        {
            PatientVisit visit = (PatientVisit)e.ClickedItem;
            Frame.Navigate(typeof(ViewPatientVisit), visit);
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

        private void BtnDialogCancel2_Click(object sender, RoutedEventArgs e)
        {
            PanelDialog2.Visibility = Visibility.Collapsed;
            Overlay.Visibility = Visibility.Collapsed;
        }

        private void BtnDialogOK2_Click(object sender, RoutedEventArgs e)
        {
            Patient pat = (Patient)ListViewPatient.SelectedItem;
            removePatientAppointment(pat);
            removePatient(pat);
            patientList.Remove(pat);

            PanelDialog2.Visibility = Visibility.Collapsed;
            Overlay.Visibility = Visibility.Collapsed;
            DetailsPanel.Visibility = Visibility.Collapsed;
            AddPanel.Visibility = Visibility.Collapsed;
            NoSelectionPanel.Visibility = Visibility.Visible;
        }

        private void removePatientAppointment(Patient patient)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry2 = "DELETE FROM patient_appointments WHERE PatientID ='" + patient.patientId + "';";


            MySqlCommand cmd = new MySqlCommand(qry2, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            filteredList.Clear();
            string searchText = TxtSearch.Text;

            if (searchText != "")
            {
                foreach (var service in patientList)
                {
                    if (service.name.ToLower().StartsWith(searchText.ToLower()))
                    {
                        filteredList.Add(service);
                    }
                    else if (service.surname.ToLower().StartsWith(searchText.ToLower()))
                    {
                        filteredList.Add(service);
                    }
                }

                ListViewPatient.ItemsSource = filteredList;

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
                ListViewPatient.ItemsSource = patientList;
                PanelNoResult.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnCloseVisitList_Click(object sender, RoutedEventArgs e)
        {
            DetailsPanel.Visibility = Visibility.Visible;
            VisitListPanel.Visibility = Visibility.Collapsed;
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

        private void CalendarView_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {
            if (args.Item.Date.Date.Equals(DateTime.Now.Date))
                args.Item.Background = ColorManager.GetSolidColorBrush("#CC009fe3");
        }

        private void BtnViewArchived_Click(object sender, RoutedEventArgs e)
        {
            if (LblMainTitle.Text.Equals("Archived Patients"))
            {
                getPatientList();
                BtnAddPatient.Visibility = Visibility.Visible;
                BtnModify.Visibility = Visibility.Visible;
                BtnViewVisits.Visibility = Visibility.Visible;
                BtnArchive.Visibility = Visibility.Visible;
                LblMainTitle.Text = "Patients";
                NoSelectionPanel.Visibility = Visibility.Visible;
                BtnArchiveLabel.Text = "View Archived Patients";

            }
            else if (LblMainTitle.Text.Equals("Patients"))
            {
                getArchivedPatients();
                BtnAddPatient.Visibility = Visibility.Collapsed;
                BtnModify.Visibility = Visibility.Collapsed;
                BtnViewVisits.Visibility = Visibility.Collapsed;
                BtnArchive.Visibility = Visibility.Collapsed;
                LblMainTitle.Text = "Archived Patients";
                NoSelectionPanel.Visibility = Visibility.Visible;
                BtnArchiveLabel.Text = "View Patients";
            }


        }

        private void getArchivedPatients()
        {
            patientList.Clear();

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM patient_archives;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string patientId = reader["PatientID"].ToString();
                string title = reader["Title"].ToString();
                string name = reader["Name"].ToString();
                string surname = reader["Surname"].ToString();
                string dateOfBirth = reader["DateOfBirth"].ToString();
                string sex = reader["Sex"].ToString();
                string address = reader["Address"].ToString();
                string telNo = reader["Contact"].ToString();
                string email = reader["Email"].ToString();
                string occupation = reader["Occupation"].ToString();


                Patient patient = new Patient(patientId, title, name, surname, dateOfBirth, sex, address, telNo, email, occupation);

                patientList.Add(patient);
            }

            conn.Close();
        }

        private void NavLogout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }
    }
}
