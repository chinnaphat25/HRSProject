using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace HRSProject.Config
{
    public class Leave
    {
        DateTime dateS;

        string _date;
        string _date6Month;
        string _date1Year;

        string _budgetYear;
        int _sick = 0;
        int _relax = 0;
        int _ordain = 0;
        int _Maternity = 0;
        int _Military = 0;
        int _UserSick = 0;
        int _UserRelax = 0;
        int _UserOrdain = 0;
        int _UserMaternity = 0;
        int _UserMilitary = 0;
        string _empID = "";


        public string Date { get => _date; set => _date = value; }
        public string Date6Month { get => _date6Month; set => _date6Month = value; }
        public string Date1Year { get => _date1Year; set => _date1Year = value; }
        public string BudgetYear { get => _budgetYear; set => _budgetYear = value; }
        public int Sick { get => _sick; set => _sick = value; }
        public int Relax { get => _relax; set => _relax = value; }
        public int Ordain { get => _ordain; set => _ordain = value; }
        public int Maternity { get => _Maternity; set => _Maternity = value; }
        public int Military { get => _Military; set => _Military = value; }
        public int UserSick { get => _UserSick; set => _UserSick = value; }
        public int UserRelax { get => _UserRelax; set => _UserRelax = value; }
        public int UserOrdain { get => _UserOrdain; set => _UserOrdain = value; }
        public int UserMaternity { get => _UserMaternity; set => _UserMaternity = value; }
        public int UserMilitary { get => _UserMilitary; set => _UserMilitary = value; }
        public string EmpID { get => _empID; set => _empID = value; }

        public Leave(string empID, int yearBudget)
        {
            DBScript bScript = new DBScript();
            string sqlGetLeave = "SELECT IF( SUM(emp_leave_sick) IS NOT NULL, SUM(emp_leave_sick), 0 ) AS sick, IF( SUM(emp_leave_relax) IS NOT NULL, SUM(emp_leave_relax), 0 ) AS relax, IF( SUM(emp_leave_ordain) IS NOT NULL, SUM(emp_leave_ordain), 0 ) AS ordain, IF( SUM(emp_leave_maternity) IS NOT NULL, SUM(emp_leave_maternity), 0 ) AS maternity, IF( SUM(emp_leave_military) IS NOT NULL, SUM(emp_leave_military), 0 ) AS military FROM tbl_emp_leave WHERE emp_leave_emp_id = '" + empID + "' AND emp_leave_year = '" + yearBudget + "'";
            MySqlDataReader rs = bScript.selectSQL(sqlGetLeave);
            if (rs.Read())
            {
                UserSick = int.Parse(rs.GetString("sick"));
                UserRelax = int.Parse(rs.GetString("relax"));
                UserOrdain = int.Parse(rs.GetString("ordain"));
                UserMaternity = int.Parse(rs.GetString("maternity"));
                UserMilitary = int.Parse(rs.GetString("military"));
                EmpID = empID;
            }
            rs.Close();
            bScript.CloseConnection();
        }

        public Leave(string date)
        {
            string[] dateSub = date.Split('-');
            dateS = DateTime.ParseExact(dateSub[0] + "-" + dateSub[1] + "-" + (int.Parse(dateSub[2]) - 543), "dd-MM-yyyy", CultureInfo.InvariantCulture);

            Date = dateToString(dateS);
            Date6Month = dateToString(dateS.AddMonths(6));
            Date1Year = dateToString(dateS.AddYears(1));
            getBudgetYear(dateS);
            leaveDay(dateS);
        }

        private string dateToString(DateTime date)
        {
            string dateString = date.ToString("dd-MM-") + (date.Year + 543);
            try
            {
                string[] subDate = dateString.Split('-');
                switch (subDate[1])
                {
                    case "01":
                        subDate[1] = "มกราคม";
                        break;
                    case "02":
                        subDate[1] = "กุมภาพันธ์";
                        break;
                    case "03":
                        subDate[1] = "มีนาคม";
                        break;
                    case "04":
                        subDate[1] = "เมษายน";
                        break;
                    case "05":
                        subDate[1] = "พฤษภาคม";
                        break;
                    case "06":
                        subDate[1] = "มิถุนายน";
                        break;
                    case "07":
                        subDate[1] = "กรกฎาคม";
                        break;
                    case "08":
                        subDate[1] = "สิงหาคม";
                        break;
                    case "09":
                        subDate[1] = "กันยายน";
                        break;
                    case "10":
                        subDate[1] = "ตุลาคม";
                        break;
                    case "11":
                        subDate[1] = "พฤศจิกายน";
                        break;
                    case "12":
                        subDate[1] = "ธันวาคม";
                        break;
                }
                return subDate[0] + " " + subDate[1] + " " + subDate[2];
            }
            catch
            {
                return "";
            }
        }

        private void getBudgetYear(DateTime date)
        {
            if (date.Month >= 10 && date.Month <= 12)
            {
                BudgetYear = (date.Year + 1 + 543).ToString();
            }
            else
            {
                BudgetYear = (date.Year + 543).ToString();
            }
        }

        private void leaveDay(DateTime date)
        {
            try
            {
                TimeSpan diffDate = DateTime.Now.Date - date.Date;
                DateTime Age = DateTime.MinValue.AddDays(diffDate.Days);
                int Day = 0;
                int Month = 0;
                int Year = 0;

                Day = Age.Day - 1;
                Month = Age.Month - 1;
                Year = Age.Year - 1;
                if (Year > 1)
                {
                    if (new DBScript().getEmpData("emp_type_emp_id", EmpID) == "5")
                    {
                        Sick = 30;
                        Relax = 10;
                    }
                    else
                    {
                        Sick = 15;
                        Relax = 10;
                    }
                    Maternity = 90;
                    Military = 1;
                    Ordain = 1;
                }
                else if (Year < 1 && Month >= 6)
                {
                    if (new DBScript().getEmpData("emp_type_emp_id", EmpID) == "5")
                    {
                        Sick = 8;
                        Relax = 10;
                    }
                    else
                    {
                        switch (date.Month)
                        {
                            case 10:
                                Sick = 15;
                                Relax = 10;
                                break;
                            case 11:
                                Sick = 8;
                                Relax = 9;
                                break;
                            case 12:
                                Sick = 8;
                                Relax = 8;
                                break;
                            case 1:
                                Sick = 8;
                                Relax = 7;
                                break;
                            case 2:
                                Sick = 8;
                                Relax = 6;
                                break;
                            case 3:
                                Sick = 8;
                                Relax = 5;
                                break;
                            case 4:
                                Sick = 8;
                                Relax = 4;
                                break;
                            case 5:
                                Sick = 8;
                                Relax = 3;
                                break;
                            case 6:
                                Sick = 8;
                                Relax = 2;
                                break;
                            case 7:
                                Sick = 8;
                                Relax = 1;
                                break;
                            case 8:
                                Sick = 8;
                                Relax = 0;
                                break;
                            case 9:
                                Sick = 8;
                                Relax = 0;
                                break;
                        }
                        Maternity = 90;
                        Military = 1;
                        Ordain = 1;
                    }
                }
                /*else
                {
                    if (Month <= 6)
                    {
                        switch (date.Month)
                        {
                            case 10:
                                Sick = 8;
                                break;
                            case 11:
                                Sick = 7;
                                break;
                            case 12:
                                Sick = 6;
                                break;
                            case 1:
                                Sick = 5;
                                break;
                            case 2:
                                Sick = 4;
                                break;
                            case 3:
                                Sick = 3;
                                break;
                        }
                        Maternity = 90;
                        Military = 1;
                        Ordain = 1;
                    }
                }*/
            }
            catch { }
        }
    }
}