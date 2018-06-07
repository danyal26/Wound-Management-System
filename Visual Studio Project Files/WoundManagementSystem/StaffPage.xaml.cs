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
    public sealed partial class StaffPage : Page
    {
        private ObservableCollection<Staff> staffList = new ObservableCollection<Staff>();
        private ObservableCollection<Staff> filteredList = new ObservableCollection<Staff>();

        private ObservableCollection<string> statusList = new ObservableCollection<string>();
        private ObservableCollection<string> titleList = new ObservableCollection<string>();

        private ObservableCollection<string> occurrenceList = new ObservableCollection<string>();
        private ObservableCollection<string> rightsList = new ObservableCollection<string>();

        private ObservableCollection<ScheduleItem> scheduleList = new ObservableCollection<ScheduleItem>();

        public StaffPage()
        {
            this.InitializeComponent();

            titleList.Add("Mr");
            titleList.Add("Mrs");
            titleList.Add("Ms");

            statusList.Add("GP");
            statusList.Add("Specialist");
            statusList.Add("Receptionist");
            statusList.Add("Wound Manager");
            statusList.Add("Accountant");
            statusList.Add("Cleaner");
            statusList.Add("HCA");
            statusList.Add("Nurse");

            occurrenceList.Add("per hour");
            occurrenceList.Add("per day");
            occurrenceList.Add("per week");
            occurrenceList.Add("per month");

            rightsList.Add("Normal");
            rightsList.Add("Administrative");

            retrieveStaffFromDB();

            NoSelectionPanel.Visibility = Visibility.Visible;
            PanelDetails.Visibility = Visibility.Collapsed;
            PanelAddModify.Visibility = Visibility.Collapsed;

            String todayDate = DateTime.Now.ToString("dd-MM-yyyy");

            getSchedule(todayDate);

            BtnModifySchedule.IsEnabled = false;
            BtnRemoveSchedule.IsEnabled = false;
        }

        public void retrieveStaffFromDB()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT* FROM staff";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

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

                staffList.Add(staff);
            }

            conn.Close();
        }

        public bool validateFields()
        {
            bool contactValid = true;
            bool rateValid = true;

            if (!long.TryParse(TxtContact.Text, out long contact))
            {
                contactValid = false;
            }

            if (!int.TryParse(TxtRate.Text, out int rate))
            {
                rateValid = false;
            }

            if (CmbStatus.SelectedIndex == -1)
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please select a status.";
                return false;
            }
            else if (CmbTitle.SelectedValue == null || CmbTitle.SelectedItem == null || CmbTitle.SelectedValue.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please select a title.";
                return false;
            }
            else if (TxtName.Text.Equals(""))
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
            else if (!(bool)RadioSexM.IsChecked && !(bool)RadioSexF.IsChecked)
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please select a sex.";
                return false;
            }
            else if (TxtAddress.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please enter an address.";
                return false;
            }
            else if (TxtContact.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please enter a contact.";
                return false;
            }
            else if (contactValid == false)
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Contact can only contain numbers.";
                return false;
            }
            else if (TxtEmail.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please enter an email address.";
                return false;
            }
            else if (TxtRate.Text.Equals(""))
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please enter a salary/wage.";
                return false;
            }
            else if (rateValid == false)
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Salary/Wage can only contain numbers.";
                return false;
            }
            else if (CmbOccurrence.SelectedIndex == -1)
            {
                PanelError.Visibility = Visibility.Visible;
                TxtErrorMessage.Text = "Please select a salary/wage occurrence.";
                return false;
            }
            else
            {
                PanelError.Visibility = Visibility.Collapsed;
                return true;
            }
        }

        private Staff createStaff()
        {
            string sex = "";
            string password = "";
            string rights = "";

            string date = DateTimeManager.convertDate(DpStaffDOB.Date.ToString());

            if ((bool)RadioSexM.IsChecked)
            {
                sex = "Male";
            }
            if ((bool)RadioSexF.IsChecked)
            {
                sex = "Female";
            }

            if (TxtPassword.Password.Equals(""))
            {
                password = "none";
            }
            else
            {
                password = TxtPassword.Password;
            }

            if (CmbRights.SelectedIndex == -1)
            {
                rights = "none";
            }
            else
            {
                rights = CmbRights.SelectedValue.ToString();
            }


            Staff staff = new Staff(IDManager.getNextStaffID(), CmbStatus.SelectedValue.ToString(),
                CmbTitle.SelectedValue.ToString(), TxtName.Text, TxtSurname.Text, date, sex,
                TxtAddress.Text, TxtContact.Text, TxtRate.Text, TxtEmail.Text,
                CmbOccurrence.SelectedValue.ToString(), password,
                rights);

            return staff;
        }

        private void insertStaff(Staff staff)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = "INSERT INTO staff VALUES('" + staff.staffID + "','" +
                            staff.status + "','" + staff.title + "','" + staff.name + "','" +
                            staff.surname + "','"
                            + staff.dob + "','" + staff.sex + "','" + staff.address + "','" +
                            staff.contact + "','" + staff.email + "','" + staff.rate + "','" +
                            staff.occurence + "', '" + staff.password + "', '" + staff.rights + "')";

            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void clearFormValues()
        {
            CmbStatus.SelectedIndex = -1;
            CmbTitle.SelectedIndex = -1;
            CmbRights.SelectedIndex = -1;
            CmbOccurrence.SelectedIndex = -1;

            TxtName.Text = string.Empty;
            TxtSurname.Text = string.Empty;
            TxtAddress.Text = string.Empty;
            TxtAddress.Text = string.Empty;
            TxtContact.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            TxtPassword.Password = string.Empty;
            TxtRate.Text = string.Empty;
        }

        public void removeStaff(Staff staff)
        {
            staffList.Remove(staff);

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"DELETE FROM staff WHERE StaffID = '{staff.staffID}';";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void modifyStaff(Staff staff)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = $"Update staff SET Status='{staff.status}', Title='{staff.title}', " +
                $"Name='{staff.name}'," +
                $"Surname='{staff.surname}', DateOfBirth='{staff.dob}', Sex='{staff.sex}'," +
                $"Address='{staff.address}', TelNo='{staff.contact}', Email='{staff.email}'," +
                $"Rate='{staff.rate}', Occurrence='{staff.occurence}', Password='{staff.password}', " +
                $"Rights='{staff.rights}' WHERE StaffID='{staff.staffID}';";


            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            int index = -1;

            foreach (var st in staffList)
            {
                if (st.staffID.Equals(staff.staffID))
                {
                    index = staffList.IndexOf(st);
                }
            }

            if (index >= 0)
            {
                staffList[index] = staff;
            }
        }

        // BUTTONS

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            PanelDetails.Visibility = Visibility.Visible;
            NoSelectionPanel.Visibility = Visibility.Collapsed;

            Staff staff = (Staff)e.ClickedItem;

            LblID.Text = staff.staffID;
            LblName.Text = staff.title + ". " + staff.name + " " + staff.surname;
            LblDob.Text = DateTimeManager.DateToShortYear(staff.dob);
            LblAddress.Text = staff.address;
            LblEmail.Text = staff.email;
            LblRate.Text = "Rs" + staff.rate + " " + staff.occurence;
            LblSex.Text = staff.sex;
            LblStatus.Text = staff.status;
            LblContact.Text = staff.contact;
            LblRights.Text = staff.rights;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Dashboard));
        }

        private void BtnToggleNav_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
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
            removeStaff((Staff)ListViewStaff.SelectedItem);

            PanelDialog2.Visibility = Visibility.Collapsed;
            Overlay.Visibility = Visibility.Collapsed;

            if (ListViewStaff.SelectedItem != null)
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

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Add Staff";

            PanelDetails.Visibility = Visibility.Collapsed;
            PanelAddModify.Visibility = Visibility.Visible;
            BtnCancel.Visibility = Visibility.Collapsed;
            NoSelectionPanel.Visibility = Visibility.Collapsed;
            BtnModifyThisStaff.Visibility = Visibility.Collapsed;
            ListViewStaff.IsEnabled = false;
            BtnAdd.IsEnabled = false;
            TxtSearch.IsEnabled = false;
            BtnAddThisStaff.Visibility = Visibility.Visible;
        }

        private void BtnAddThisStaff_Click(object sender, RoutedEventArgs e)
        {
            if (validateFields())
            {

                Staff staff = createStaff();

                insertStaff(staff);

                clearFormValues();

                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "A new staff has been added.";
                Overlay.Visibility = Visibility.Visible;

                PanelAddModify.Visibility = Visibility.Collapsed;
                BtnCancel.Visibility = Visibility.Visible;

                ListViewStaff.IsEnabled = true;
                BtnAdd.IsEnabled = true;
                TxtSearch.IsEnabled = true;

                staffList.Add(staff);

                LblTitle.Text = "Staff";

                if (ListViewStaff.SelectedItem != null)
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

        private void BtnModifyThisStaff_Click(object sender, RoutedEventArgs e)
        {
            Staff staff = (Staff)ListViewStaff.SelectedItem;

            if (validateFields())
            {
                staff.status = CmbStatus.SelectedValue.ToString();
                staff.title = CmbTitle.SelectedValue.ToString();
                staff.name = TxtName.Text;
                staff.surname = TxtSurname.Text;
                staff.dob = DateTimeManager.convertDate(DpStaffDOB.Date.ToString());

                if ((bool)RadioSexM.IsChecked)
                {
                    staff.sex = "Male";
                }
                if ((bool)RadioSexF.IsChecked)
                {
                    staff.sex = "Female";
                }

                if (TxtPassword.Password.Equals(""))
                {
                    staff.password = "none";
                }
                else
                {
                    staff.password = TxtPassword.Password;
                }

                if (CmbRights.SelectedIndex == -1)
                {
                    staff.rights = "none";
                }
                else
                {
                    staff.rights = CmbRights.SelectedValue.ToString();
                }

                staff.address = TxtAddress.Text;
                staff.contact = TxtContact.Text;
                staff.email = TxtEmail.Text;
                staff.rate = TxtRate.Text;
                staff.occurence = CmbOccurrence.SelectedValue.ToString();
                staff.password = TxtPassword.Password;

                modifyStaff(staff);

                clearFormValues();

                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "Staff has been modified.";
                Overlay.Visibility = Visibility.Visible;

                PanelDetails.Visibility = Visibility.Collapsed;
                PanelAddModify.Visibility = Visibility.Collapsed;
                NoSelectionPanel.Visibility = Visibility.Visible;
                BtnCancel.Visibility = Visibility.Visible;

                ListViewStaff.IsEnabled = true;
                BtnAdd.IsEnabled = true;
                TxtSearch.IsEnabled = true;

                LblTitle.Text = "Staff";
            }
        }

        private void BtnAddModifyCancel_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Staff";

            if (ListViewStaff.SelectedItem != null)
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

            ListViewStaff.IsEnabled = true;
            BtnAdd.IsEnabled = true;
            TxtSearch.IsEnabled = true;
            PanelError.Visibility = Visibility.Collapsed;
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            // Dialog 2
            PanelDialog2.Visibility = Visibility.Visible;
            TxtDialogMessage2.Text = "Are you sure you want to remove this staff?";
            Overlay.Visibility = Visibility.Visible;
        }

        private void BtnModify_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Modify Staff";

            string password = "";

            PanelDetails.Visibility = Visibility.Collapsed;
            PanelAddModify.Visibility = Visibility.Visible;
            BtnCancel.Visibility = Visibility.Collapsed;
            NoSelectionPanel.Visibility = Visibility.Collapsed;

            BtnAddThisStaff.Visibility = Visibility.Collapsed;
            BtnModifyThisStaff.Visibility = Visibility.Visible;

            Staff staff = (Staff)ListViewStaff.SelectedItem;

            CmbStatus.SelectedValue = staff.status;
            CmbTitle.SelectedValue = staff.title;
            TxtName.Text = staff.name;
            TxtSurname.Text = staff.surname;

            DpStaffDOB.Date = DateTimeManager.convertToDate(staff.dob);

            if (staff.sex.Equals("Male"))
            {
                RadioSexM.IsChecked = true;
            }
            else
            {
                RadioSexF.IsChecked = true;
            }

            if (staff.password.Equals("") || staff.password.Equals("none"))
            {
                password = "";
            }
            else
            {
                TxtPassword.Password = staff.password;
            }

            TxtAddress.Text = staff.address;
            TxtContact.Text = staff.contact;
            TxtEmail.Text = staff.email;
            TxtRate.Text = staff.rate;
            CmbOccurrence.SelectedValue = staff.occurence;
            TxtPassword.Password = password;
            CmbRights.SelectedValue = staff.rights;

            ListViewStaff.IsEnabled = false;
            BtnAdd.IsEnabled = false;
            TxtSearch.IsEnabled = false;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            filteredList.Clear();
            string searchText = TxtSearch.Text;

            if (searchText != "")
            {
                foreach (var staff in staffList)
                {
                    if (staff.name.ToLower().StartsWith(searchText.ToLower()))
                    {
                        filteredList.Add(staff);
                    }
                    else if (staff.surname.ToLower().StartsWith(searchText.ToLower()))
                    {
                        filteredList.Add(staff);
                    }
                }

                ListViewStaff.ItemsSource = filteredList;

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
                ListViewStaff.ItemsSource = staffList;
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

        // ********************************** SCHEDULE *************************************

        private void CalendarView_CalendarViewDayItemChanging(CalendarView sender,
            CalendarViewDayItemChangingEventArgs args)
        {
            if (args.Item.Date.Date.Equals(DateTime.Now.Date))
                args.Item.Background = ColorManager.GetSolidColorBrush("#CC009fe3");
        }

        private void getSchedule(string date)
        {
            scheduleList.Clear();

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"SELECT staff_schedule.*, staff.StaffID, staff.Name, staff.Surname, staff.TelNo " +
                $"FROM staff_schedule LEFT JOIN staff " +
                $"ON staff.StaffID = staff_schedule.StaffID " +
                $"WHERE `Date` = '{date}'; ";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            int counter = 0;

            while (reader.Read())
            {
                string shiftId = reader["ShiftID"].ToString();
                string startTime = reader["StartTime"].ToString();
                string endTime = reader["EndTime"].ToString();
                string staffId = reader["StaffID"].ToString();
                string name = reader["Name"].ToString();
                string surname = reader["Surname"].ToString();
                string contact = reader["TelNo"].ToString();

                ScheduleItem schedule = new ScheduleItem(shiftId, date, startTime, endTime,
                    staffId, name, surname, contact);

                scheduleList.Add(schedule);
                counter++;
            }

            conn.Close();

            if (counter > 0)
            {
                NoScheduleSelection.Visibility = Visibility.Collapsed;
                ListViewSchedule.Visibility = Visibility.Visible;
            }
            else
            {
                NoScheduleSelection.Visibility = Visibility.Visible;
                ListViewSchedule.Visibility = Visibility.Collapsed;
            }
        }

        private void CalSchedule_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {
            if (args.Item.Date.Date.Equals(DateTime.Now.Date))
                args.Item.Background = ColorManager.GetSolidColorBrush("#CC009fe3");
        }

        private void CalSchedule_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            if (sender.SelectedDates.Count == 1)
            {
                string selectedDate = CalSchedule.SelectedDates[0].ToString();
                selectedDate = selectedDate.Substring(0, 10);

                getSchedule(DateTimeManager.convertDate(selectedDate));
            }
        }

        private void BtnSchedule_Click(object sender, RoutedEventArgs e)
        {
            PanelSchedule.Visibility = Visibility.Visible;
            LeftOverlay.Visibility = Visibility.Visible;
        }

        private void BtnCloseSchedule_Click(object sender, RoutedEventArgs e)
        {
            PanelSchedule.Visibility = Visibility.Collapsed;
            LeftOverlay.Visibility = Visibility.Collapsed;
        }

        // MODIFY SCHEDULE

        private void BtnModifySchedule_Click(object sender, RoutedEventArgs e)
        {
            PanelAddModifySchedule.Visibility = Visibility.Visible;
            BtnAddThisSchedule.Visibility = Visibility.Collapsed;
            BtnModifyThisSchedule.Visibility = Visibility.Visible;
            RightOverlay.Visibility = Visibility.Visible;

            ScheduleItem item = (ScheduleItem)ListViewSchedule.SelectedItem;

            DpScheduleDate.Date = DateTimeManager.convertToDate(item.date);
            TPStart.Time = TimeSpan.Parse(item.startTime);
            TPEnd.Time = TimeSpan.Parse(item.endTime);

            Staff selectedStaff = null;

            foreach (var staff in staffList)
            {
                if (staff.staffID.Equals(item.staffID))
                {
                    selectedStaff = staff;
                }
            }
            CmbChooseStaff.SelectedItem = selectedStaff;
        }

        private void BtnModifyThisSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (validateSchedule())
            {
                ScheduleItem item = (ScheduleItem)ListViewSchedule.SelectedItem;

                Staff staff = (Staff)CmbChooseStaff.SelectedItem;

                item.staffID = staff.staffID;
                item.date = DateTimeManager.convertDate(DpScheduleDate.Date.ToString());
                item.startTime = TPStart.Time.ToString();
                item.endTime = TPEnd.Time.ToString();

                modifySchedule(item);

                PanelAddModifySchedule.Visibility = Visibility.Collapsed;
                RightOverlay.Visibility = Visibility.Collapsed;

                getSchedule(item.date);

                if (ListViewSchedule.SelectedItem == null)
                {
                    BtnModifySchedule.IsEnabled = false;
                    BtnRemoveSchedule.IsEnabled = false;
                }
            }
        }

        private void modifySchedule(ScheduleItem item)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = $"Update staff_schedule SET Date='{item.date}', StartTime='{item.startTime}', " +
                $"EndTime='{item.endTime}', StaffID='{item.staffID}' " +
                $"WHERE ShiftID='{item.id}';";


            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            // Dialog
            PanelDialog.Visibility = Visibility.Visible;
            TxtDialogMessage.Text = "Staff schedule has been modified";

            PanelError2.Visibility = Visibility.Collapsed;

            clearScheduleValues();
        }

        // REMOVE SCHEDULE

        private void BtnRemoveSchedule_Click(object sender, RoutedEventArgs e)
        {
            PanelDialog3.Visibility = Visibility.Visible;
            TxtDialogMessage3.Text = "Are you sure you want to delete this schedule item?";
            RightOverlay.Visibility = Visibility.Visible;
        }

        private void deleteScheduleItem(ScheduleItem item)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"DELETE FROM staff_schedule WHERE ShiftID = '{item.id}'";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            scheduleList.Remove(item);
        }

        private void BtnDialogOK3_Click(object sender, RoutedEventArgs e)
        {
            ScheduleItem item = (ScheduleItem)ListViewSchedule.SelectedItem;
            deleteScheduleItem(item);

            PanelDialog3.Visibility = Visibility.Collapsed;
            RightOverlay.Visibility = Visibility.Collapsed;

            getSchedule(item.date);

            if (ListViewSchedule.SelectedItem == null)
            {
                BtnModifySchedule.IsEnabled = false;
                BtnRemoveSchedule.IsEnabled = false;
            }
        }

        private void BtnDialogCancel3_Click(object sender, RoutedEventArgs e)
        {
            PanelDialog3.Visibility = Visibility.Collapsed;
            RightOverlay.Visibility = Visibility.Collapsed;
        }

        // ADD SCHEDULE

        private void BtnAddSchedule_Click(object sender, RoutedEventArgs e)
        {
            PanelAddModifySchedule.Visibility = Visibility.Visible;
            BtnAddThisSchedule.Visibility = Visibility.Visible;
            BtnModifyThisSchedule.Visibility = Visibility.Collapsed;
            RightOverlay.Visibility = Visibility.Visible;

            if (CalSchedule.SelectedDates.Count == 1)
            {
                DpScheduleDate.Date = CalSchedule.SelectedDates[0];

            }
            else
            {
                DpScheduleDate.Date = DateTime.Now;
            }

            clearScheduleValues();
        }

        private void addSchedule(ScheduleItem item)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = $"INSERT INTO staff_schedule VALUES('{item.id}','{item.date}'," +
                $"'{item.startTime}','{item.endTime}','{item.staffID}');";

            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            PanelError2.Visibility = Visibility.Collapsed;

            clearScheduleValues();
        }

        private void BtnAddThisSchedule_Click(object sender, RoutedEventArgs e)
        {

            if (validateSchedule())
            {
                Staff staff = (Staff)CmbChooseStaff.SelectedItem;
                string date = DateTimeManager.convertDate(DpScheduleDate.Date.ToString());

                string startTime = TPStart.Time.ToString();
                string endTime = TPEnd.Time.ToString();

                if (startTime.Length > 5)
                    startTime = startTime.Substring(0, 5);

                if (endTime.Length > 5)
                    endTime = endTime.Substring(0, 5);

                if (checkStaffTodaySchedule(staff))
                {
                    ScheduleItem item = new ScheduleItem(IDManager.getNextScheduleID(), date,
                        startTime, endTime, staff.staffID, staff.name, staff.surname, staff.contact);

                    addSchedule(item);
                    scheduleList.Add(item);

                    PanelAddModifySchedule.Visibility = Visibility.Collapsed;
                    RightOverlay.Visibility = Visibility.Collapsed;

                    clearScheduleValues();

                    PanelDialog.Visibility = Visibility.Visible;
                    TxtDialogMessage.Text = "Staff has been added to the schedule.";

                    NoScheduleSelection.Visibility = Visibility.Collapsed;
                    ListViewSchedule.Visibility = Visibility.Visible;
                }
            }
        }

        // VALIDATE
        private bool validateSchedule()
        {

            if (!DpScheduleDate.Date.HasValue)
            {
                PanelError2.Visibility = Visibility.Visible;
                TxtErrorMessage2.Text = "Please select a date.";
                return false;
            }
            else if (CmbChooseStaff.SelectedIndex == -1)
            {
                PanelError2.Visibility = Visibility.Visible;
                TxtErrorMessage2.Text = "Please select a staff member.";
                return false;
            }
            else
            {
                PanelError.Visibility = Visibility.Collapsed;
                return true;
            }
        }

        private bool checkStaffTodaySchedule(Staff staff)
        {
            string date = DateTimeManager.convertDate(DpScheduleDate.Date.ToString());

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"SELECT * FROM staff_schedule WHERE StaffID='{staff.staffID}' " +
                $"AND Date='{date}';";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            int counter = 0;

            while (reader.Read())
            {
                counter++;
            }

            conn.Close();

            if (counter > 0)
            {
                PanelError2.Visibility = Visibility.Visible;
                TxtErrorMessage2.Text = "This staff member already has a shift on the selected date.";
                return false;
            }
            else
            {
                return true;
            }
        }

        private void clearScheduleValues()
        {
            CmbChooseStaff.SelectedIndex = -1;
            TPStart.Time = new TimeSpan(8, 0, 0);
            TPEnd.Time = new TimeSpan(16, 0, 0);
        }

        private void BtnCancelSchedule_Click(object sender, RoutedEventArgs e)
        {
            PanelAddModifySchedule.Visibility = Visibility.Collapsed;
            RightOverlay.Visibility = Visibility.Collapsed;

            clearScheduleValues();
        }

        private void BtnErrorClose2_Click(object sender, RoutedEventArgs e)
        {
            PanelError2.Visibility = Visibility.Collapsed;
        }

        private void ListViewSchedule_ItemClick(object sender, ItemClickEventArgs e)
        {
            BtnModifySchedule.IsEnabled = true;
            BtnRemoveSchedule.IsEnabled = true;
        }

        private void NavLogout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }
    }
}
