using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoundManagementSystem
{
    public static class DateTimeManager
    {
        // 10 OCT
        public static string DateToShort(string fullDate)
        {
            string month = fullDate.Substring(3,2);
            string day = fullDate.Substring(0,2);

            switch (month)
            {
                case "01":
                    month = "Jan";
                    break;
                case "02":
                    month = "Feb";
                    break;
                case "03":
                    month = "Mar";
                    break;
                case "04":
                    month = "Apr";
                    break;
                case "05":
                    month = "May";
                    break;
                case "06":
                    month = "Jun";
                    break;
                case "07":
                    month = "Jul";
                    break;
                case "08":
                    month = "Aug";
                    break;
                case "09":
                    month = "Sep";
                    break;
                case "10":
                    month = "Oct";
                    break;
                case "11":
                    month = "Nov";
                    break;
                case "12":
                    month = "Dec";
                    break;
                default:
                    break;
            }

            return day + " " + month;
        }

        // 10 OCT 2000
        public static string DateToShortYear(string fullDate)
        {
            string month = fullDate.Substring(3, 2);
            string day = fullDate.Substring(0, 2);
            string year = fullDate.Substring(6,4);

            switch (month)
            {
                case "01":
                    month = "Jan";
                    break;
                case "02":
                    month = "Feb";
                    break;
                case "03":
                    month = "Mar";
                    break;
                case "04":
                    month = "Apr";
                    break;
                case "05":
                    month = "May";
                    break;
                case "06":
                    month = "Jun";
                    break;
                case "07":
                    month = "Jul";
                    break;
                case "08":
                    month = "Aug";
                    break;
                case "09":
                    month = "Sep";
                    break;
                case "10":
                    month = "Oct";
                    break;
                case "11":
                    month = "Nov";
                    break;
                case "12":
                    month = "Dec";
                    break;
                default:
                    break;
            }

            return day + " " + month + " " + year;
        }

        public static string convertDate(String date)
        {
            DateTime newDate = Convert.ToDateTime(date);
            string stringDate = newDate.ToString("dd-MM-yyyy");
            return stringDate;
        }

        public static string convertDate(DateTime date)
        {
            string stringDate = date.ToString("dd-MM-yyyy");
            return stringDate;
        }

        public static DateTime convertToDate(string date)
        {
            string day = date.Substring(0,2);
            string month = date.Substring(3,2);
            string year = date.Substring(6,4);

            return Convert.ToDateTime(month + "-" + day + "-" + year);


        }

    }
}
