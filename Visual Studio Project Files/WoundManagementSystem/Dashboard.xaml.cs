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
using Microsoft.Toolkit.Uwp.UI.Controls;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WoundManagementSystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Dashboard : Page
    {

        //private List<Appointment> appointmentList = new List<Appointment>();
        private ObservableCollection<Appointment> appointmentList = new ObservableCollection<Appointment>();
        private ObservableCollection<Patient> patientList = new ObservableCollection<Patient>();

        public Dashboard()
        {
            this.InitializeComponent();

            getPatientList();

            String todayDate = DateTime.Now.ToString("dd-MM-yyyy");

            retrieveAppointmentsFromDB(todayDate);

            refreshPanelsForAppointments();

            BtnCancelSelect.Visibility = Visibility.Collapsed;
        }

        // NAVIGATION BUTTONS
        private void NavPatients_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PatientPage));
        }

        private void NavContacts_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ContactsPage));
        }

        private void NavLogout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }

        private void NavStaff_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StaffPage));
        }

        private void NavServices_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ServicesPage));
        }

        private void NavPriceLists_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PriceLists));
        }

        private void NavForms_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FormPage));
        }

        private void NavFinances_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Finances));
        }

        private void NavStock_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Stock));
        }

        // MY METHODS

        private void getPatientList()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM patients;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();
            patientList.Clear();

            while (reader.Read())
            {
                string id = reader["PatientID"].ToString();
                string title = reader["Title"].ToString();
                string name = reader["Name"].ToString();
                string surname = reader["Surname"].ToString();
                string dob = reader["DateOfBirth"].ToString();
                string sex = reader["Sex"].ToString();
                string address = reader["Address"].ToString();
                string contact = reader["Contact"].ToString();
                string email = reader["Email"].ToString();
                string occupation = reader["Occupation"].ToString();

                Patient patient = new Patient(id, title, name, surname, dob, sex, address, contact, email, occupation);


                patientList.Add(patient);

            }

            conn.Close();
        }

        public void retrieveAppointmentsFromDB(string newDate)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"SELECT appointments.*, IFNULL(patient_appointments.PatientID,0) AS PatientID " +
                $"FROM appointments LEFT JOIN patient_appointments " +
                $"ON patient_appointments.AppointmentID = appointments.AppointmentID " +
                $"WHERE Date = '{newDate}' ORDER BY Time ASC;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string appointmentId = reader["AppointmentID"].ToString();
                string name = reader["Name"].ToString();
                string surname = reader["Surname"].ToString();
                string contact = reader["Contact"].ToString();
                string date = reader["Date"].ToString();
                string time = reader["Time"].ToString();
                string type = reader["Type"].ToString();
                string notes = reader["Notes"].ToString();
                string patientId = reader["PatientID"].ToString();

                Appointment appointment = new Appointment(appointmentId, name, surname, contact, date, time, type, notes, patientId);

                appointmentList.Add(appointment);
            }

            conn.Close();
        }

        private void showAppointment(Appointment appt)
        {
            BtnDeleteAppt.Tag = appt;

            GridNoApptSelection.Visibility = Visibility.Collapsed;
            NextApptPanel.Visibility = Visibility.Visible;

            LblName.Text = appt.name + " " + appt.surname;
            LblDate.Text = DateTimeManager.DateToShort(appt.date);
            LblTime.Text = appt.time;
            LblContactText.Text = appt.contact;
            LblNoteText.Text = appt.notes;

            string patientId = appt.patientId;

            if (apptIsExisitingPatient(patientId))
            {

                if (visitExists(patientId, appt.date, appt.time))
                {
                    BtnNewVisit.Visibility = Visibility.Collapsed;
                    BtnViewVisit.Visibility = Visibility.Visible;
                    BtnDeleteAppt.Visibility = Visibility.Collapsed;
                    BtnModifyAppointment.Visibility = Visibility.Collapsed;
                }
                else
                {
                    BtnDeleteAppt.Visibility = Visibility.Visible;
                    BtnModifyAppointment.Visibility = Visibility.Visible;
                    BtnNewVisit.Visibility = Visibility.Visible;
                    BtnViewVisit.Visibility = Visibility.Collapsed;
                }
                patientId = " - " + patientId;
            }
            else
            {
                patientId = "";
                BtnViewVisit.Visibility = Visibility.Collapsed;
                BtnDeleteAppt.Visibility = Visibility.Visible;
                BtnModifyAppointment.Visibility = Visibility.Visible;
            }

            LblPatientID.Text = patientId;

            BtnNewVisit.Tag = appt;
        }

        private bool visitExists(string patientId, string date, string time)
        {

            bool exists = false;

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"SELECT DISTINCT patient_visits.*, patient_appointments.PatientID FROM patient_visits " +
                $"JOIN patient_appointments ON patient_visits.PatientID = patient_appointments.PatientID " +
                $"WHERE patient_visits.PatientID = '{patientId}' AND Date = '{date}' AND time = '{time}';";

            //LblNoteText.Text = qry;

            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            int counter = 0;

            while (reader.Read())
            {
                string id = reader["PatientVisitID"].ToString();
                string pId = reader["PatientID"].ToString();
                string notes = reader["Notes"].ToString();
                string visitDate = reader["Date"].ToString();
                string visitTime = reader["Time"].ToString();
                string incomeId = reader["IncomeID"].ToString();
                string afterOpening = reader["AfterOpening"].ToString();
                string actions = reader["ActionsPerformed"].ToString();
                string beforeClosing = reader["BeforeClosing"].ToString();
                string type = reader["Type"].ToString();

                PatientVisit visit = new PatientVisit(id, pId, visitDate, visitTime, notes,
                    incomeId, afterOpening, actions, beforeClosing, type);

                BtnViewVisit.Tag = visit;

                counter++;
            }

            conn.Close();

            if (counter >= 1)
                exists = true;

            return exists;

        }

        private void getSelectedDateAppointments()
        {
            //clear list
            appointmentList.Clear();
            //Get date selection
            string selectedDate;

            if (CalUpcomingAppt.SelectedDates.Count == 1)
            {
                selectedDate = CalUpcomingAppt.SelectedDates[0].ToString();
            }
            else
            {
                selectedDate = DateTime.Now.ToString();
            }

            selectedDate = selectedDate.Substring(0, 10);

            retrieveAppointmentsFromDB(DateTimeManager.convertDate(selectedDate));

            NextApptPanel.Visibility = Visibility.Collapsed;
            GridNoApptSelection.Visibility = Visibility.Visible;

            refreshPanelsForAppointments();
        }

        private void refreshPanelsForAppointments()
        {
            PanelAddAppointment.Visibility = Visibility.Collapsed;
            MainPanel.Visibility = Visibility.Visible;

            if (appointmentList.Count == 0)
            {
                GridNoApptSelection.Visibility = Visibility.Visible;
                NextApptPanel.Visibility = Visibility.Collapsed;

                PanelAppointmentList.Visibility = Visibility.Collapsed;
                PanelListEmpty.Visibility = Visibility.Visible;
                PanelAddAppointment.Visibility = Visibility.Collapsed;
            }
            else
            {
                PanelListEmpty.Visibility = Visibility.Collapsed;
                PanelAppointmentList.Visibility = Visibility.Visible;
                NextApptPanel.Visibility = Visibility.Collapsed;
            }
        }

        private bool apptIsExisitingPatient(String pId)
        {

            if (pId == "0")
            {
                BtnNewVisit.Visibility = Visibility.Collapsed;
                BtnAddPatient.Visibility = Visibility.Visible;
                return false;
            }
            else
            {

                BtnNewVisit.Visibility = Visibility.Visible;
                BtnAddPatient.Visibility = Visibility.Collapsed;

                return true;
            }
        }

        private Patient getPatientForViewing(string patientId)
        {
            Patient patient = null;

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM patients WHERE PatientID = '" + patientId + "';";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                string id = reader["PatientID"].ToString();
                string title = reader["Title"].ToString();
                string name = reader["Name"].ToString();
                string surname = reader["Surname"].ToString();
                string dateOfBirth = reader["DateOfBirth"].ToString();
                string sex = reader["Sex"].ToString();
                string address = reader["Address"].ToString();
                string contact = reader["Contact"].ToString();
                string email = reader["Email"].ToString();
                string occupation = reader["Occupation"].ToString();

                patient = new Patient(id, title, name, surname, dateOfBirth,
                    sex, address, contact, email, occupation);

            }

            return patient;

        }


        // --------------------- ADD APPOINTMENT METHODS ----------------------------------


        // INSERT INTO APPOINTMENTS
        private void addAppointment(Appointment appt)
        {

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "INSERT INTO appointments VALUES('" + appt.appointmentId + "','" +
                appt.date + "','" + appt.time + "','" + appt.name + "','" + appt.surname + "','"
                + appt.contact + "','" + appt.type + "','" + appt.notes + "')";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // INSERT INTO PATIENT APPOINTMENTS

        private void addPatientAppt(string patientId, string appointmentId)
        {

            string patientApptId = IDManager.getNextPatientApptID();

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "INSERT INTO patient_appointments VALUES('" + patientApptId + "', '" + appointmentId +
                "', '" + patientId + "')";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private bool fieldsAreValid()
        {
            bool radioValidity = false;
            bool numberValidity = true;

            if (!long.TryParse(TxtContact.Text, out long contact))
            {
                numberValidity = false;
            }

            if ((bool)RadioBurn.IsChecked || (bool)RadioDiabeticFoot.IsChecked ||
                (bool)RadioUlcer.IsChecked)
            {
                radioValidity = true;

            }

            if (TxtName.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please enter a name.";
                return false;
            }
            else if (TxtSurname.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please enter a surname.";
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
            else if (radioValidity == false)
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please select a Type.";
                return false;
            }
            else
            {
                PanelError.Visibility = Visibility.Collapsed;
                return true;
            }
        }

        private void clearFormValues()
        {
            TxtID.Text = "Select patient from list";
            TxtName.Text = String.Empty;
            TxtSurname.Text = String.Empty;
            TxtContact.Text = String.Empty;
            TxtNotes.Text = String.Empty;
            RadioBurn.IsChecked = false;
            RadioDiabeticFoot.IsChecked = false;
            RadioUlcer.IsChecked = false;
        }

        private void deleteAppointment(Appointment appt)
        {

            if (appt.patientId != "" && appt.patientId != null)
            {
                deletePatientAppointment(appt);
            }

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"DELETE FROM appointments WHERE AppointmentID = '{appt.appointmentId}'";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void deletePatientAppointment(Appointment appt)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"DELETE FROM patient_appointments WHERE AppointmentID = '{appt.appointmentId}' AND PatientID = '{appt.patientId}'";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // DATE SELECTION ON CALENDAR

        private void CalUpcomingAppt_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            if (sender.SelectedDates.Count == 1)
            {
                getSelectedDateAppointments();
            }
            if (sender.SelectedDates.Count == 0)
            {
                PanelAppointmentList.Visibility = Visibility.Collapsed;
                PanelListEmpty.Visibility = Visibility.Visible;
            }

        }


        // ---------------------------------- BUTTONS -----------------------------------------

        // APPOINTMENT SELECTION FROM LIST
        private void ListViewAppointment_ItemClick(object sender, ItemClickEventArgs e)
        {
            var appointment = (Appointment)e.ClickedItem;

            showAppointment(appointment);
        }

        //Add New Appointment
        private void BtnNewAppt_Click(object sender, RoutedEventArgs e)
        {
            PanelAppointmentList.Visibility = Visibility.Collapsed;
            PanelListEmpty.Visibility = Visibility.Collapsed;
            MainPanel.Visibility = Visibility.Collapsed;
            PanelAddAppointment.Visibility = Visibility.Visible;
            BtnAdd.Visibility = Visibility.Visible;
            BtnModify.Visibility = Visibility.Collapsed;

            if (CalUpcomingAppt.SelectedDates.Count == 1)
            {
                DatePickerAddAppt.Date = CalUpcomingAppt.SelectedDates[0];

            }
            else
            {
                DatePickerAddAppt.Date = DateTime.Now;
            }

        }

        //Add this Appointment
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            if (fieldsAreValid())
            {
                string appointmentID = IDManager.getNextAppointmentID();
                string name = TxtName.Text;
                string surname = TxtSurname.Text;
                string date = DateTimeManager.convertDate(DatePickerAddAppt.Date.ToString());
                string time = TimePickerAddAppt.Time.ToString();
                string contact = TxtContact.Text;
                string notes = TxtNotes.Text;
                string type = "";
                string patientId = TxtID.Text;

                if (patientId.Equals("Select patient from list"))
                {
                    patientId = "";
                }

                time = time.Substring(0, 5);

                if ((bool)RadioBurn.IsChecked)
                    type = "Burn";
                if ((bool)RadioUlcer.IsChecked)
                    type = "Chronic Ulcer";
                if ((bool)RadioDiabeticFoot.IsChecked)
                    type = "Diabetic Foot";

                Appointment newAppointment = new Appointment(appointmentID, name, surname, contact, date, time, type, notes, patientId);

                addAppointment(newAppointment);

                if (!patientId.Equals(""))
                {
                    addPatientAppt(patientId, appointmentID);
                }

                clearFormValues();

                // Dialog
                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "Appointment has been added";
                Overlay.Visibility = Visibility.Visible;

                getSelectedDateAppointments();

                PanelAddAppointment.Visibility = Visibility.Collapsed;
                MainPanel.Visibility = Visibility.Visible;

                refreshPanelsForAppointments();

            }


        }

        //Cancel - Add appointment
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            PanelAddAppointment.Visibility = Visibility.Collapsed;
            MainPanel.Visibility = Visibility.Visible;

            clearFormValues();

            getSelectedDateAppointments();
        }

        //Add Visit Details
        private void BtnNewVisit_Click(object sender, RoutedEventArgs e)
        {
            Appointment appt = (Appointment)BtnNewVisit.Tag;
            this.Frame.Navigate(typeof(AddVisitDetails), appt);
        }

        private void CalUpcomingAppt_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {
            if (args.Item.Date.Date.Equals(DateTime.Now.Date))
                args.Item.Background = ColorManager.GetSolidColorBrush("#CC009fe3");
        }

        private void BtnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            Appointment appt = (Appointment)ListViewAppointment.SelectedItem;
            this.Frame.Navigate(typeof(PatientPage), appt);
        }

        private void BtnViewVisit_Click(object sender, RoutedEventArgs e)
        {
            PatientVisit visit = (PatientVisit)BtnViewVisit.Tag;
            this.Frame.Navigate(typeof(ViewPatientVisit), visit);
        }

        private void TxtPatientId_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {

                sender.ItemsSource = patientList;
            }
        }

        private void TxtPatientId_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            Patient patient = (Patient)args.SelectedItem;

            sender.Text = patient.patientId;

            TxtName.Text = patient.name;
            TxtSurname.Text = patient.surname;
            TxtContact.Text = patient.contact;

        }

        private void BtnDeleteAppt_Click(object sender, RoutedEventArgs e)
        {
            // Dialog 2
            PanelDialog2.Visibility = Visibility.Visible;
            TxtDialogMessage2.Text = "Are you sure you want to delete this appointment?";
            Overlay.Visibility = Visibility.Visible;

        }

        private void PatientListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            BtnChoosePatient.Visibility = Visibility.Collapsed;
            BtnCancelSelect.Visibility = Visibility.Visible;

            Patient patient = (Patient)e.ClickedItem;

            TxtID.Text = patient.patientId;

            TxtName.Text = patient.name;
            TxtSurname.Text = patient.surname;
            TxtContact.Text = patient.contact;

            BtnChoosePatient.Flyout.Hide();
        }

        private void BtnCancelSelect_Click(object sender, RoutedEventArgs e)
        {
            BtnChoosePatient.Visibility = Visibility.Visible;
            BtnCancelSelect.Visibility = Visibility.Collapsed;
            clearFormValues();
        }

        private void BtnErrorClose_Click(object sender, RoutedEventArgs e)
        {
            PanelError.Visibility = Visibility.Collapsed;
        }

        private void BtnModifyAppointment_Click(object sender, RoutedEventArgs e)
        {
            LblAddModifyTitle.Text = "Modify Appointment";

            Appointment appointment = (Appointment)ListViewAppointment.SelectedItem;

            MainPanel.Visibility = Visibility.Collapsed;
            PanelAddAppointment.Visibility = Visibility.Visible;
            BtnAdd.Visibility = Visibility.Collapsed;
            BtnModify.Visibility = Visibility.Visible;

            BtnChoosePatient.Visibility = Visibility.Collapsed;
            BtnCancelSelect.Visibility = Visibility.Collapsed;

            TxtName.Text = appointment.name;
            TxtSurname.Text = appointment.surname;

            DatePickerAddAppt.Date = DateTimeManager.convertToDate(appointment.date);
            TimePickerAddAppt.Time = TimeSpan.Parse(appointment.time);

            TxtContact.Text = appointment.contact;
            TxtNotes.Text = appointment.notes;

            if (appointment.patientId == "0")
            {
                TxtID.Text = "";
            }
            else
            {
                TxtID.Text = appointment.patientId;
            }

            string type = appointment.type;

            if (type.Equals("Burn"))
            {
                RadioBurn.IsChecked = true;
            }
            else if (type.Equals("Chronic Ulcer"))
            {
                RadioUlcer.IsChecked = true;
            }
            else if (type.Equals("Diabetic Foot"))
            {
                RadioDiabeticFoot.IsChecked = true;
            }

        }

        public void modifyAppointment(Appointment appointment)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = $"Update appointments SET Date='{appointment.date}', Time='{appointment.time}', " +
                $"Name='{appointment.name}', Surname='{appointment.surname}', Contact='{appointment.contact}', " +
                $"Type='{appointment.type}' WHERE AppointmentID='{appointment.appointmentId}';";


            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            clearFormValues();

            // Dialog
            PanelDialog.Visibility = Visibility.Visible;
            TxtDialogMessage.Text = "Appointment has been modified";
            Overlay.Visibility = Visibility.Visible;

            getSelectedDateAppointments();

            PanelAddAppointment.Visibility = Visibility.Collapsed;
            MainPanel.Visibility = Visibility.Visible;

            refreshPanelsForAppointments();

        }

        private void BtnModify_Click(object sender, RoutedEventArgs e)
        {

            Appointment appointment = (Appointment)ListViewAppointment.SelectedItem;

            if (fieldsAreValid())
            {
                appointment.name = TxtName.Text;
                appointment.surname = TxtName.Text;
                appointment.date = DateTimeManager.convertDate(DatePickerAddAppt.Date.ToString());
                appointment.time = TimePickerAddAppt.Time.ToString();
                appointment.contact = TxtName.Text;
                appointment.type = TxtName.Text;
                appointment.notes = TxtNotes.Text;

                if ((bool)RadioBurn.IsChecked)
                    appointment.type = "Burn";
                if ((bool)RadioUlcer.IsChecked)
                    appointment.type = "Chronic Ulcer";
                if ((bool)RadioDiabeticFoot.IsChecked)
                    appointment.type = "Diabetic Foot";

                modifyAppointment(appointment);
            }
        }

        private void BtnDialogOK_Click(object sender, RoutedEventArgs e)
        {
            PanelDialog.Visibility = Visibility.Collapsed;
            Overlay.Visibility = Visibility.Collapsed;
        }

        private void BtnDialogOK2_Click(object sender, RoutedEventArgs e)
        {
            Appointment appt = (Appointment)BtnDeleteAppt.Tag;
            deleteAppointment(appt);
            getSelectedDateAppointments();

            PanelDialog2.Visibility = Visibility.Collapsed;
            Overlay.Visibility = Visibility.Collapsed;
        }

        private void BtnDialogCancel2_Click(object sender, RoutedEventArgs e)
        {
            PanelDialog2.Visibility = Visibility.Collapsed;
            Overlay.Visibility = Visibility.Collapsed;
        }
    }
}
