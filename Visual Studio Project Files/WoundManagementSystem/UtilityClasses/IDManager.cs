using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoundManagementSystem
{
    public static class IDManager
    {
        // CONVERTER
        public static string getNextID(string lastId)
        {
            List<string> charactersOnly = new List<string>();
            List<int> digitsOnly = new List<int>();

            for (int i = 0; i < lastId.Length; i++)
            {
                var thisChar = lastId.Substring(i, 1);
                try
                {
                    int x = int.Parse(thisChar);
                    digitsOnly.Add(x);
                }
                catch (Exception)
                {
                    charactersOnly.Add(thisChar);
                }

            }

            string letters = "";
            foreach (var item in charactersOnly)
            {
                letters = letters + item;
            }

            string digits = "";
            foreach (var item in digitsOnly)
            {
                digits = digits + item;
            }

            int newNum = int.Parse(digits) + 1;
            string newDigits = newNum.ToString();

            while (newDigits.Length < 3)
            {
                newDigits = "0" + newDigits;
            }

            return letters + newDigits;

        }

        // APPOINTMENT
        public static string getNextAppointmentID()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM appointments ORDER BY AppointmentID DESC LIMIT 1;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            string id = "";

            while (reader.Read())
            {
                id = reader["AppointmentID"].ToString();

            }

            conn.Close();

            if (id == "")
            {
                id = "APT000";
            }

            return getNextID(id);
        }

        // PATIENT APPOINTMENT
        public static string getNextPatientApptID()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM patient_appointments ORDER BY PatientAppointmentID DESC LIMIT 1;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            string id = "";

            while (reader.Read())
            {
                id = reader["PatientAppointmentID"].ToString();

            }

            conn.Close();

            if (id == "")
            {
                id = "PA000";
            }

            return getNextID(id);
        }

        // VISIT 
        public static string getNextPatientVisitId()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM patient_visits ORDER BY PatientVisitID DESC LIMIT 1;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            string id = "";

            while (reader.Read())
            {
                id = reader["PatientVisitID"].ToString();

            }

            conn.Close();

            if (id == "")
            {
                id = "PV000";
            }

            return getNextID(id);
        }

        // INCOME
        public static string getNextIncomeId()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM income ORDER BY IncomeID DESC LIMIT 1;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            string id = "";

            while (reader.Read())
            {
                id = reader["IncomeID"].ToString();

            }

            conn.Close();

            if (id == "")
            {
                id = "INC000";
            }

            return getNextID(id);
        }

        // EXPENSE
        public static string getNextExpenseId()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM expense ORDER BY ExpenseID DESC LIMIT 1;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            string id = "";

            while (reader.Read())
            {
                id = reader["ExpenseID"].ToString();

            }

            conn.Close();

            if (id == "")
            {
                id = "EX000";
            }

            return getNextID(id);
        }

        // CONTACT
        public static string getNextContactId()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM contacts ORDER BY ContactID DESC LIMIT 1;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            string id = "";

            while (reader.Read())
            {
                id = reader["ContactID"].ToString();

            }

            conn.Close();

            if (id == "")
            {
                id = "CONT000";
            }

            return getNextID(id);
        }

        // VISIT MEDICATION
        public static string getNextVisitMedId()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM patient_visit_medication ORDER BY ID DESC LIMIT 1;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            string id = "";

            while (reader.Read())
            {
                id = reader["ID"].ToString();

            }

            if (id == "")
            {
                id = "PVM000";
            }

            conn.Close();

            return getNextID(id);
        }

        // VISIT SERVICE
        public static string getNextVisitServiceId()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM patient_visit_services ORDER BY ID DESC LIMIT 1;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            string id = "";

            while (reader.Read())
            {
                id = reader["ID"].ToString();

            }

            if (id == "")
            {
                id = "PVS000";
            }

            conn.Close();

            return getNextID(id);
        }

        // BURNS VISIT
        public static string getNextBurnsVisitID()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM burns_visit ORDER BY VisitID DESC LIMIT 1;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            string id = "";

            while (reader.Read())
            {
                id = reader["VisitID"].ToString();

            }

            conn.Close();

            if (id == "")
            {
                id = "BV000";
            }

            return getNextID(id);
        }

        // CHRONIC ULCER VISIT
        public static string getNextUlcerID()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM chronic_ulcer_visit ORDER BY VisitID DESC LIMIT 1;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            string id = "";

            while (reader.Read())
            {
                id = reader["VisitID"].ToString();

            }

            if (id == "")
            {
                id = "CUV000";
            }

            conn.Close();

            return getNextID(id);
        }

        // DIABETIC FOOT VISIT
        public static string getNextDiabeticFootID()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM diabetic_foot_visit ORDER BY VisitID DESC LIMIT 1;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            string id = "";

            while (reader.Read())
            {
                id = reader["VisitID"].ToString();

            }

            if (id == "")
            {
                id = "DFV000";
            }

            conn.Close();

            return getNextID(id);
        }

        // SERVICE
        public static string getNextServiceId()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM services ORDER BY ServiceID DESC LIMIT 1;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            string id = "";

            while (reader.Read())
            {
                id = reader["ServiceID"].ToString();

            }

            conn.Close();

            if (id == "")
            {
                id = "S000";
            }

            return getNextID(id);
        }

        // MEDICATION
        public static string getNextMedId()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM stock ORDER BY MedicationID DESC LIMIT 1;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            string id = "";

            while (reader.Read())
            {
                id = reader["MedicationID"].ToString();

            }

            conn.Close();

            if (id == "")
            {
                id = "MEDST000";
            }

            return getNextID(id);
        }

        // PATIENT
        public static string getNextPatientID()
        {

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM patients ORDER BY PatientID DESC LIMIT 1;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            string id = "";

            while (reader.Read())
            {
                id = reader["PatientID"].ToString();

            }

            conn.Close();

            if (id == "")
            {
                id = "P000";
            }

            string newID = getNextID(id);

            while (existsInArchives(newID))
            {
                newID = getNextID(newID);
            }

            return newID;
        }

        private static bool existsInArchives(string id)
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"SELECT * FROM patient_archives WHERE PatientID = '{id}';";
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
                return true;
            else
                return false;
        }

        // MEDICATION SELLING LIST
        public static string getNextSellingListId()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM medication_selling_list ORDER BY MedicationID DESC LIMIT 1;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            string id = "";

            while (reader.Read())
            {
                id = reader["MedicationID"].ToString();

            }

            conn.Close();

            if (id == "")
            {
                id = "MEDSL000";
            }

            return getNextID(id);
        }

        // MEDICATION BUYING LIST
        public static string getNextBuyingListId()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM medication_buying_list ORDER BY MedicationID DESC LIMIT 1;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            string id = "";

            while (reader.Read())
            {
                id = reader["MedicationID"].ToString();

            }

            conn.Close();

            if (id == "")
            {
                id = "MEDBL000";
            }

            return getNextID(id);
        }

        // STAFF
        public static string getNextStaffID()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM staff ORDER BY StaffID DESC LIMIT 1;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            string id = "";

            while (reader.Read())
            {
                id = reader["StaffID"].ToString();

            }

            conn.Close();

            if (id == "")
            {
                id = "ST000";
            }

            return getNextID(id);
        }

        // STAFF
        public static string getNextScheduleID()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = "SELECT * FROM staff_schedule ORDER BY ShiftID DESC LIMIT 1;";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            string id = "";

            while (reader.Read())
            {
                id = reader["ShiftID"].ToString();

            }

            conn.Close();

            if (id == "")
            {
                id = "STFS000";
            }

            return getNextID(id);
        }
    }
}
