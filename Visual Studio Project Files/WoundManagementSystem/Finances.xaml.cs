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
    public sealed partial class Finances : Page
    {
        private ObservableCollection<Income> incomeList = new ObservableCollection<Income>();
        private ObservableCollection<Expense> expenseList = new ObservableCollection<Expense>();

        private string rights = MyGlobals.userRights;

        public Finances()
        {
            this.InitializeComponent();

            getIncomeList();
            getExpenseList();

            if (rights.Equals("Normal"))
            {
                MainPanelIncome.Visibility = Visibility.Collapsed;
                MainPanelExpenses.Visibility = Visibility.Visible;

                NoSelectionPanelExpense.Visibility = Visibility.Visible;
                PanelDetailsExpense.Visibility = Visibility.Collapsed;

                TabExpenses.Template = App.Current.Resources["WhiteButtonTemplate"] as ControlTemplate;
                TabIncome.Template = App.Current.Resources["GrayButtonTemplate"] as ControlTemplate;

                TabIncome.IsEnabled = false;
            }

            PanelAddModifyIncome.Visibility = Visibility.Collapsed;
            PanelDetailsIncome.Visibility = Visibility.Visible;
            BtnCancel.Visibility = Visibility.Visible;
            NoSelectionPanelIncome.Visibility = Visibility.Visible;
        }

        // INCOME

        private void getIncomeList()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM income";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string id = reader["IncomeID"].ToString();
                string type = reader["IncomeType"].ToString();
                string strAmount = reader["Amount"].ToString();
                string notes = reader["Notes"].ToString();

                double amount = Double.Parse(strAmount);

                Income income = new Income(id, type, amount, notes);

                incomeList.Add(income);
            }

            conn.Close();
        }

        private bool validateFields()
        {
            bool amountValid = true;

            if (!double.TryParse(TxtAmountIncome.Text, out double amount))
            {
                amountValid = false;
            }

            if (TxtTypeIncome.Text.Equals(""))
            {
                PanelErrorIncome.Visibility = Visibility.Visible;
                TxtErrorMessageIncome.Text = "Please enter a income type.";
                return false;
            }
            else if (TxtAmountIncome.Text.Equals(""))
            {
                PanelErrorIncome.Visibility = Visibility.Visible;
                TxtErrorMessageIncome.Text = "Please enter income amount.";
                return false;
            }
            else if (amountValid == false)
            {
                PanelErrorIncome.Visibility = Visibility.Visible;
                TxtErrorMessageIncome.Text = "Amount can only contain numbers.";
                return false;
            }
            else
            {
                PanelErrorIncome.Visibility = Visibility.Collapsed;
                return true;
            }
        }

        private void clearFormValues()
        {
            TxtNotesIncome.Text = String.Empty;
            TxtAmountIncome.Text = String.Empty;
            TxtTypeIncome.Text = String.Empty;
        }

        private void removeIncome(Income income)
        {
            incomeList.Remove(income);

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"DELETE FROM income WHERE IncomeID = '{income.id}';";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void insertIncome(Income income)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"INSERT INTO income VALUES('{income.id}','{income.type}','{income.amount}','{income.notes}')";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void modifyIncome(Income income)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = $"Update income SET IncomeType='{income.type}', Amount='{income.amount}'," +
                $"Notes='{income.notes}' WHERE IncomeID = '{income.id}';";


            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            int index = -1;

            foreach (var inc in incomeList)
            {
                if (inc.id.Equals(income.id))
                {
                    index = incomeList.IndexOf(inc);
                }
            }

            if (index >= 0)
            {
                incomeList[index] = income;
            }


        }

        // EXPENSES

        private void getExpenseList()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM expense";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string id = reader["ExpenseID"].ToString();
                string type = reader["ExpenseType"].ToString();
                string strAmount = reader["Amount"].ToString();
                string notes = reader["Notes"].ToString();

                double amount = Double.Parse(strAmount);

                Expense expense = new Expense(id, type, amount, notes);

                expenseList.Add(expense);
            }

            conn.Close();
        }

        private void clearExpenseFormValues()
        {
            TxtNotesExpense.Text = String.Empty;
            TxtAmountExpense.Text = String.Empty;
            TxtTypeExpense.Text = String.Empty;
        }

        private void insertExpense(Expense expense)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"INSERT INTO expense VALUES('{expense.id}','{expense.type}','{expense.amount}','{expense.notes}')";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private bool validateExpenseFields()
        {
            bool amountValid = true;

            if (!double.TryParse(TxtAmountExpense.Text, out double amount))
            {
                amountValid = false;
            }

            if (TxtTypeExpense.Text.Equals(""))
            {
                PanelErrorExpenses.Visibility = Visibility.Visible;
                TxtErrorMessageExpenses.Text = "Please enter an expense type.";
                return false;
            }
            else if (TxtAmountExpense.Text.Equals(""))
            {
                PanelErrorExpenses.Visibility = Visibility.Visible;
                TxtErrorMessageExpenses.Text = "Please enter expense amount.";
                return false;
            }
            else if (amountValid == false)
            {
                PanelErrorExpenses.Visibility = Visibility.Visible;
                TxtErrorMessageExpenses.Text = "Amount can only contain numbers.";
                return false;
            }
            else
            {
                PanelErrorExpenses.Visibility = Visibility.Collapsed;
                return true;
            }
        }

        private void removeExpense(Expense expense)
        {
            expenseList.Remove(expense);

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"DELETE FROM expense WHERE ExpenseID = '{expense.id}';";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void modifyExpense(Expense expense)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());

            String qry = $"Update expense SET ExpenseType='{expense.type}', Amount='{expense.amount}'," +
                $"Notes='{expense.notes}' WHERE ExpenseID = '{expense.id}';";


            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            int index = -1;

            foreach (var exp in expenseList)
            {
                if (exp.id.Equals(expense.id))
                {
                    index = expenseList.IndexOf(exp);
                }
            }

            if (index >= 0)
            {
                expenseList[index] = expense;
            }


        }

        // ------------------------------------- BUTTONS---------------------------------------

        private void BtnToggleNav_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Dashboard));
        }

        private void TabExpenses_Click(object sender, RoutedEventArgs e)
        {
            MainPanelIncome.Visibility = Visibility.Collapsed;
            MainPanelExpenses.Visibility = Visibility.Visible;

            if (ExpenseListView.SelectedItem == null)
            {
                NoSelectionPanelExpense.Visibility = Visibility.Visible;
                PanelDetailsExpense.Visibility = Visibility.Collapsed;
            }


            TabExpenses.Template = App.Current.Resources["WhiteButtonTemplate"] as ControlTemplate;
            TabIncome.Template = App.Current.Resources["GrayButtonTemplate"] as ControlTemplate;
        }

        private void TabIncome_Click(object sender, RoutedEventArgs e)
        {
            MainPanelIncome.Visibility = Visibility.Visible;
            MainPanelExpenses.Visibility = Visibility.Collapsed;

            if (IncomeListView.SelectedItem == null)
            {
                NoSelectionPanelIncome.Visibility = Visibility.Visible;
                PanelDetailsIncome.Visibility = Visibility.Collapsed;
            }

            TabIncome.Template = App.Current.Resources["WhiteButtonTemplate"] as ControlTemplate;
            TabExpenses.Template = App.Current.Resources["GrayButtonTemplate"] as ControlTemplate;
        }

        private void TabReport_Click(object sender, RoutedEventArgs e)
        {

        }

        // INCOME

        private void BtnAddIncome_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Add Income";

            PanelDetailsIncome.Visibility = Visibility.Collapsed;
            PanelAddModifyIncome.Visibility = Visibility.Visible;
            BtnCancel.Visibility = Visibility.Collapsed;
            NoSelectionPanelIncome.Visibility = Visibility.Collapsed;
            BtnModifyThisIncome.Visibility = Visibility.Collapsed;
            IncomeListView.IsEnabled = false;
            BtnAddIncome.IsEnabled = false;
            BtnAddThisIncome.Visibility = Visibility.Visible;

            TabExpenses.IsEnabled = false;
            TabIncome.IsEnabled = false;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Income income = (Income)e.ClickedItem;

            LblNotesIncome.Text = income.notes;
            LblIDIncome.Text = income.id;
            LblAmountIncome.Text = "Rs" + income.amount.ToString();
            LblTypeIncome.Text = income.type;

            PanelDetailsIncome.Visibility = Visibility.Visible;
            PanelAddModifyIncome.Visibility = Visibility.Collapsed;
            BtnCancel.Visibility = Visibility.Visible;
            NoSelectionPanelIncome.Visibility = Visibility.Collapsed;
        }

        private void BtnModifyIncome_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Modify Income";

            TabExpenses.IsEnabled = false;
            TabIncome.IsEnabled = false;

            PanelDetailsIncome.Visibility = Visibility.Collapsed;
            PanelAddModifyIncome.Visibility = Visibility.Visible;
            BtnCancel.Visibility = Visibility.Collapsed;
            NoSelectionPanelIncome.Visibility = Visibility.Collapsed;

            BtnAddThisIncome.Visibility = Visibility.Collapsed;
            BtnModifyThisIncome.Visibility = Visibility.Visible;

            Income income = (Income)IncomeListView.SelectedItem;

            TxtNotesIncome.Text = income.notes;
            TxtAmountIncome.Text = income.amount + "";
            TxtTypeIncome.Text = income.type;

            IncomeListView.IsEnabled = false;
            BtnAddIncome.IsEnabled = false;
        }

        private void BtnRemoveIncome_Click(object sender, RoutedEventArgs e)
        {
            // Dialog 2
            BtnDialogOK.Tag = "income";
            PanelDialog2.Visibility = Visibility.Visible;
            TxtDialogMessage2.Text = "Are you sure you want to remove income?";
            Overlay.Visibility = Visibility.Visible;

            //String message = "Are you sure you want to remove the income?";
            //CustomTwoButtonDialog removeConfirmDialog = new CustomTwoButtonDialog("Remove Income?", "No", "Yes", message);

            //ContentDialogResult result = await removeConfirmDialog.ShowAsync();

            //if (result == ContentDialogResult.Primary)
            //{
            //    removeIncome((Income)IncomeListView.SelectedItem);
            //}

            //if (IncomeListView.SelectedItem != null)
            //{
            //    PanelDetailsIncome.Visibility = Visibility.Visible;
            //    NoSelectionPanelIncome.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    PanelDetailsIncome.Visibility = Visibility.Collapsed;
            //    NoSelectionPanelIncome.Visibility = Visibility.Visible;
            //}
        }

        private void BtnAddThisIncome_Click(object sender, RoutedEventArgs e)
        {
            if (validateFields())
            {
                double amount = Double.Parse(TxtAmountIncome.Text);
                Income income = new Income(IDManager.getNextIncomeId(), TxtTypeIncome.Text, amount, TxtNotesIncome.Text);
                insertIncome(income);
                TxtNotesIncome.Text = "";
                TxtAmountIncome.Text = "";
                TxtTypeIncome.Text = "";
                //CustomMessageDialog.createDialog("Income Added", "OK", "A new income has been added.");

                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "A new income has been added.";
                Overlay.Visibility = Visibility.Visible;

                PanelAddModifyIncome.Visibility = Visibility.Collapsed;
                BtnCancel.Visibility = Visibility.Visible;

                TabExpenses.IsEnabled = true;

                if (!rights.Equals("Normal"))
                {
                    TabIncome.IsEnabled = true;
                }

                IncomeListView.IsEnabled = true;
                BtnAddIncome.IsEnabled = true;

                incomeList.Add(income);

                LblTitle.Text = "Finances";

                if (IncomeListView.SelectedItem != null)
                {
                    PanelDetailsIncome.Visibility = Visibility.Visible;
                    NoSelectionPanelIncome.Visibility = Visibility.Collapsed;
                }
                else
                {
                    PanelDetailsIncome.Visibility = Visibility.Collapsed;
                    NoSelectionPanelIncome.Visibility = Visibility.Visible;
                }
            }
        }

        private void BtnModifyThisIncome_Click(object sender, RoutedEventArgs e)
        {
            Income income = (Income)IncomeListView.SelectedItem;

            if (validateFields())
            {
                income.amount = Double.Parse(TxtAmountIncome.Text);
                income.type = TxtTypeIncome.Text;
                income.notes = TxtNotesIncome.Text;

                modifyIncome(income);

                clearFormValues();

                //CustomMessageDialog.createDialog("Income Modified", "OK", "Income has been modified.");

                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "Income has been modified.";
                Overlay.Visibility = Visibility.Visible;

                PanelDetailsIncome.Visibility = Visibility.Collapsed;
                PanelAddModifyIncome.Visibility = Visibility.Collapsed;
                NoSelectionPanelIncome.Visibility = Visibility.Visible;
                BtnCancel.Visibility = Visibility.Visible;

                TabExpenses.IsEnabled = true;

                if (!rights.Equals("Normal"))
                {
                    TabIncome.IsEnabled = true;
                }

                IncomeListView.IsEnabled = true;
                BtnAddIncome.IsEnabled = true;

                LblTitle.Text = "Finances";
            }
        }

        private void BtnAddModifyCancel_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Finances";

            if (IncomeListView.SelectedItem != null)
            {
                PanelDetailsIncome.Visibility = Visibility.Visible;
                NoSelectionPanelIncome.Visibility = Visibility.Collapsed;
            }
            else
            {
                PanelDetailsIncome.Visibility = Visibility.Collapsed;
                NoSelectionPanelIncome.Visibility = Visibility.Visible;
            }


            PanelAddModifyIncome.Visibility = Visibility.Collapsed;

            BtnCancel.Visibility = Visibility.Visible;

            clearFormValues();

            IncomeListView.IsEnabled = true;
            BtnAddIncome.IsEnabled = true;

            PanelErrorIncome.Visibility = Visibility.Collapsed;

            TabExpenses.IsEnabled = true;

            if (!rights.Equals("Normal"))
            {
                TabIncome.IsEnabled = true;
            }
        }

        private void BtnErrorCloseIncome_Click(object sender, RoutedEventArgs e)
        {
            PanelErrorIncome.Visibility = Visibility.Collapsed;
        }

        // EXPENSES

        private void BtnAddExpense_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Add Expense";

            PanelDetailsExpense.Visibility = Visibility.Collapsed;
            PanelAddModifyExpense.Visibility = Visibility.Visible;
            BtnCancel.Visibility = Visibility.Collapsed;
            NoSelectionPanelExpense.Visibility = Visibility.Collapsed;
            BtnModifyThisExpense.Visibility = Visibility.Collapsed;
            ExpenseListView.IsEnabled = false;
            BtnAddExpense.IsEnabled = false;
            BtnAddThisExpense.Visibility = Visibility.Visible;

            TabExpenses.IsEnabled = false;
            TabIncome.IsEnabled = false;
        }

        private void ExpenseListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Expense expense = (Expense)e.ClickedItem;

            LblNotesExpense.Text = expense.notes;
            LblIDExpense.Text = expense.id;
            LblAmountExpense.Text = "Rs" + expense.amount.ToString();
            LblTypeExpense.Text = expense.type;

            PanelDetailsExpense.Visibility = Visibility.Visible;
            PanelAddModifyExpense.Visibility = Visibility.Collapsed;
            BtnCancel.Visibility = Visibility.Visible;
            NoSelectionPanelExpense.Visibility = Visibility.Collapsed;
        }

        private void BtnRemoveExpense_Click(object sender, RoutedEventArgs e)
        {
            // Dialog 2
            BtnDialogOK.Tag = "expense";
            PanelDialog2.Visibility = Visibility.Visible;
            TxtDialogMessage2.Text = "Are you sure you want to remove expense?";
            Overlay.Visibility = Visibility.Visible;

            //String message = "Are you sure you want to remove the expense?";
            //CustomTwoButtonDialog removeConfirmDialog = new CustomTwoButtonDialog("Remove Expense?", "No", "Yes", message);

            //ContentDialogResult result = await removeConfirmDialog.ShowAsync();

            //if (result == ContentDialogResult.Primary)
            //{
            //    removeExpense((Expense)ExpenseListView.SelectedItem);
            //}

            //if (ExpenseListView.SelectedItem != null)
            //{
            //    PanelDetailsExpense.Visibility = Visibility.Visible;
            //    NoSelectionPanelExpense.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    PanelDetailsExpense.Visibility = Visibility.Collapsed;
            //    NoSelectionPanelExpense.Visibility = Visibility.Visible;
            //}
        }

        private void BtnModifyExpense_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Modify Expense";

            PanelDetailsExpense.Visibility = Visibility.Collapsed;
            PanelAddModifyExpense.Visibility = Visibility.Visible;
            BtnCancel.Visibility = Visibility.Collapsed;
            NoSelectionPanelExpense.Visibility = Visibility.Collapsed;

            BtnAddThisExpense.Visibility = Visibility.Collapsed;
            BtnModifyThisExpense.Visibility = Visibility.Visible;

            Expense expense = (Expense)ExpenseListView.SelectedItem;

            TxtNotesExpense.Text = expense.notes;
            TxtAmountExpense.Text = expense.amount + "";
            TxtTypeExpense.Text = expense.type;

            ExpenseListView.IsEnabled = false;
            BtnAddExpense.IsEnabled = false;

            TabExpenses.IsEnabled = false;
            TabIncome.IsEnabled = false;
        }

        private void BtnAddThisExpense_Click(object sender, RoutedEventArgs e)
        {
            if (validateExpenseFields())
            {
                double amount = Double.Parse(TxtAmountExpense.Text);
                Expense expense = new Expense(IDManager.getNextExpenseId(), TxtTypeExpense.Text, amount, TxtNotesExpense.Text);
                insertExpense(expense);
                clearExpenseFormValues();
                //CustomMessageDialog.createDialog("Expense Added", "OK", "A new expense has been added.");

                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "A new expense has been added.";
                Overlay.Visibility = Visibility.Visible;

                PanelAddModifyExpense.Visibility = Visibility.Collapsed;
                BtnCancel.Visibility = Visibility.Visible;

                TabExpenses.IsEnabled = true;
                if (!rights.Equals("Normal"))
                {
                    TabIncome.IsEnabled = true;
                }

                ExpenseListView.IsEnabled = true;
                BtnAddExpense.IsEnabled = true;

                expenseList.Add(expense);

                LblTitle.Text = "Finances";

                if (ExpenseListView.SelectedItem != null)
                {
                    PanelDetailsExpense.Visibility = Visibility.Visible;
                    NoSelectionPanelExpense.Visibility = Visibility.Collapsed;
                }
                else
                {
                    PanelDetailsExpense.Visibility = Visibility.Collapsed;
                    NoSelectionPanelExpense.Visibility = Visibility.Visible;
                }
            }
        }

        private void BtnAddModifyExpenseCancel_Click(object sender, RoutedEventArgs e)
        {
            LblTitle.Text = "Finances";

            if (ExpenseListView.SelectedItem != null)
            {
                PanelDetailsExpense.Visibility = Visibility.Visible;
                NoSelectionPanelExpense.Visibility = Visibility.Collapsed;
            }
            else
            {
                PanelDetailsExpense.Visibility = Visibility.Collapsed;
                NoSelectionPanelExpense.Visibility = Visibility.Visible;
            }


            PanelAddModifyExpense.Visibility = Visibility.Collapsed;

            BtnCancel.Visibility = Visibility.Visible;

            clearExpenseFormValues();

            ExpenseListView.IsEnabled = true;
            BtnAddExpense.IsEnabled = true;

            PanelErrorExpenses.Visibility = Visibility.Collapsed;

            TabExpenses.IsEnabled = true;
            if (!rights.Equals("Normal"))
            {
                TabIncome.IsEnabled = true;
            }
        }

        private void BtnModifyThisExpense_Click(object sender, RoutedEventArgs e)
        {
            Expense expense = (Expense)ExpenseListView.SelectedItem;

            if (validateExpenseFields())
            {
                expense.amount = Double.Parse(TxtAmountExpense.Text);
                expense.type = TxtTypeExpense.Text;
                expense.notes = TxtNotesExpense.Text;

                modifyExpense(expense);

                clearExpenseFormValues();

                PanelDialog.Visibility = Visibility.Visible;
                TxtDialogMessage.Text = "Expense has been modified.";
                Overlay.Visibility = Visibility.Visible;

                TabExpenses.IsEnabled = true;
                if (!rights.Equals("Normal"))
                {
                    TabIncome.IsEnabled = true;
                }

                PanelDetailsExpense.Visibility = Visibility.Collapsed;
                PanelAddModifyExpense.Visibility = Visibility.Collapsed;
                NoSelectionPanelExpense.Visibility = Visibility.Visible;
                BtnCancel.Visibility = Visibility.Visible;

                ExpenseListView.IsEnabled = true;
                BtnAddExpense.IsEnabled = true;

                LblTitle.Text = "Finances";
            }
        }

        // DIALOGS

        private void BtnErrorCloseExpenses_Click(object sender, RoutedEventArgs e)
        {
            PanelErrorExpenses.Visibility = Visibility.Collapsed;
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
            PanelDialog2.Visibility = Visibility.Collapsed;
            Overlay.Visibility = Visibility.Collapsed;

            if (BtnDialogOK.Tag.Equals("income"))
            {
                removeIncome((Income)IncomeListView.SelectedItem);

                if (IncomeListView.SelectedItem != null)
                {
                    PanelDetailsIncome.Visibility = Visibility.Visible;
                    NoSelectionPanelIncome.Visibility = Visibility.Collapsed;
                }
                else
                {
                    PanelDetailsIncome.Visibility = Visibility.Collapsed;
                    NoSelectionPanelIncome.Visibility = Visibility.Visible;
                }
            }
            else if (BtnDialogOK.Tag.Equals("expense"))
            {
                removeExpense((Expense)ExpenseListView.SelectedItem);

                if (ExpenseListView.SelectedItem != null)
                {
                    PanelDetailsExpense.Visibility = Visibility.Visible;
                    NoSelectionPanelExpense.Visibility = Visibility.Collapsed;
                }
                else
                {
                    PanelDetailsExpense.Visibility = Visibility.Collapsed;
                    NoSelectionPanelExpense.Visibility = Visibility.Visible;
                }
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
