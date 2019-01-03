using HRSProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRSProject.Manpower
{
    public partial class index : System.Web.UI.Page
    {
        DBScript dBScript = new DBScript();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string sql_year = "SELECT * FROM tbl_year";
                GetDownList(txtYear, sql_year, "year", "year");
                getMampowerData(dBScript.getBudgetYear());
                getMampowerFullData(dBScript.getBudgetYear());
                Title = "อัตรากำลัง ปีงบประมาณ " + dBScript.getBudgetYear();
                //rowShow.InnerHtml += "<div class='col-md'>55555</ div > ";
            }
        }
        public void GetDownList(DropDownList list, string sql, string text, string value)
        {
            MySqlDataReader rs = dBScript.selectSQL(sql);
            using (var reader = dBScript.selectSQL(sql))
            {
                if (reader.HasRows)
                {
                    list.DataSource = reader;
                    list.DataValueField = value;
                    list.DataTextField = text;
                    list.DataBind();
                }
            }
        }

        public string[] getAffManpower(string year)
        {
            string sql = "SELECT manpower_pos_id,affi_name FROM tbl_manpower LEFT JOIN tbl_emp_profile ON manpower_cpoint_id = emp_cpoint_id AND emp_staus_working = 1 LEFT JOIN tbl_cpoint ON manpower_cpoint_id = cpoint_id LEFT JOIN tbl_affiliation ON affi_id = manpower_pos_id WHERE manpower_year = '" + year + "' GROUP BY manpower_pos_id ORDER BY affi_name";
            int count = 0;
            string[] data = new string[100];
            MySqlDataReader rs = dBScript.selectSQL(sql);
            while (rs.Read())
            {
                data[count] = rs.GetString("manpower_pos_id");
                count++;
            }
            rs.Close();
            return data;
        }

        public void getMampowerData(string year)
        {
            string[] aff = getAffManpower(year);
            rowShow.InnerHtml = "";

            for (int i = 0; i < aff.Length; i++)
            {
                string sql = "SELECT affi_name, cpoint_name, manpower_full, COUNT(emp_id) AS num, (manpower_full - COUNT(emp_id)) AS dif FROM tbl_manpower LEFT JOIN tbl_emp_profile ON manpower_cpoint_id = emp_cpoint_id AND manpower_pos_id = emp_affi_id AND emp_staus_working = 1 LEFT JOIN tbl_cpoint ON manpower_cpoint_id = cpoint_id LEFT JOIN tbl_affiliation ON affi_id = manpower_pos_id WHERE manpower_pos_id = '" + aff[i] + "' AND manpower_year = '" + year + "' GROUP BY manpower_pos_id,manpower_cpoint_id ORDER BY manpower_cpoint_id DESC";
                MySqlDataReader rs = dBScript.selectSQL(sql);
                int start = 0;
                int[] sum = { 0, 0, 0 };
                while (rs.Read())
                {
                    /*rowShow.InnerHtml += rs.GetString("affi_name");
                    rowShow.InnerHtml += rs.GetString("cpoint_name");
                    rowShow.InnerHtml += rs.GetString("manpower_full");
                    rowShow.InnerHtml += rs.GetString("num");
                    rowShow.InnerHtml += rs.GetString("dif");*/
                    if (start == 0)
                    {
                        rowShow.InnerHtml += "<div class='col-md-5'>";
                        rowShow.InnerHtml += "<div class='card'>";
                        rowShow.InnerHtml += "<div class='card-header card-header-primary'><h3 class='card-title' style='font-weight: bold;'>อัตรากำลัง " + rs.GetString("affi_name") + "</h3><p class='card-category' style='font-weight: bold;'>อัตรากำลัง ปีงบประมาณ " + txtYear.SelectedValue + " </p></div>";
                        rowShow.InnerHtml += "<div class='card-body table-responsive table-sm'>";
                        rowShow.InnerHtml += "<table class='table table-hover' width='100%'>";
                        rowShow.InnerHtml += "<thead class='thead-light'>";
                        rowShow.InnerHtml += "<th>ด่านฯ</th>";
                        rowShow.InnerHtml += "<th>อัตราเต็ม</th>";
                        rowShow.InnerHtml += "<th>อัตราปัจจุบัน</th>";
                        rowShow.InnerHtml += "<th>อัตราขาด</th>";
                        rowShow.InnerHtml += "</thead>";
                        rowShow.InnerHtml += "<tbody>";
                    }

                    rowShow.InnerHtml += "<tr class='thead-light'>";
                    rowShow.InnerHtml += "<td>" + rs.GetString("cpoint_name") + "</td>";
                    rowShow.InnerHtml += "<td>" + rs.GetString("manpower_full") + "</td>";
                    rowShow.InnerHtml += "<td>" + rs.GetString("num") + "</td>";
                    rowShow.InnerHtml += getColorNum(rs.GetString("dif"));
                    rowShow.InnerHtml += "</tr>";
                    sum[0] += rs.GetInt32("manpower_full");
                    sum[1] += rs.GetInt32("num");
                    sum[2] += rs.GetInt32("dif");
                    start++;
                }

                if (sum[0] != 0)
                {
                    rowShow.InnerHtml += "<tr class='bg-light' style='text-decoration: underline;'>";
                    rowShow.InnerHtml += "<td><b>รวม</b></td>";
                    rowShow.InnerHtml += "<td><b>" + sum[0] + "</b></td>";
                    rowShow.InnerHtml += "<td><b>" + sum[1] + "</b></td>";
                    rowShow.InnerHtml += getColorNum(sum[2].ToString());
                    rowShow.InnerHtml += "</tr>";
                    rowShow.InnerHtml += "</tbody>";
                    rowShow.InnerHtml += "</table>";
                    rowShow.InnerHtml += "</div>";
                    rowShow.InnerHtml += "</div>";
                    rowShow.InnerHtml += "</div><br/>";
                }
                rs.Close();
            }
            if (rowShow.InnerText == "")
            {
                rowShow.InnerHtml = "ไม่พบข้อมูล";
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            getMampowerData(txtYear.SelectedValue);
            getMampowerFullData(txtYear.SelectedValue);
            Title = "อัตรากำลัง ปีงบประมาณ " + txtYear.SelectedValue;
        }

        public void getMampowerFullData(string year)
        {
            string sql = "SELECT T1.affi_name,T1.manpower_full,IF(T2.num IS NULL,0,T2.num) AS num,(T1.manpower_full-IF(T2.num IS NULL,0,T2.num)) AS dif FROM (SELECT affi_id,affi_name, SUM(manpower_full) AS manpower_full FROM tbl_manpower JOIN tbl_affiliation ON affi_id = manpower_pos_id WHERE manpower_year = '" + year+"' GROUP BY affi_id ) T1 LEFT JOIN ( SELECT affi_id,affi_name,COUNT(emp_id) AS num FROM tbl_emp_profile JOIN tbl_affiliation ON affi_id = emp_affi_id WHERE emp_staus_working = 1 GROUP BY emp_affi_id ) T2 ON T1.affi_id = T2.affi_id ORDER BY T1.affi_name";
            MySqlDataReader rs = dBScript.selectSQL(sql);
            rowHead.InnerHtml = "";
            int[] sum = { 0, 0, 0 };
            int start = 0;
            while (rs.Read())
            {

                if (start == 0)
                {
                    rowHead.InnerHtml += "<div class='col-md-10'>";
                    rowHead.InnerHtml += "<div class='card'>";
                    rowHead.InnerHtml += "<div class='card-header card-header-primary' style='color: #3C4858;font-weight: bold;'><h3 class='card-title'><b>อัตรากำลัง ปีงบประมาณ " + year + " ทั้งหมด</b></h3><p class='card-category' style='font-weight: bold;'>อัตรากำลัง ปีงบประมาณ " + txtYear.SelectedValue + " </p></div>";
                    rowHead.InnerHtml += "<div class='card-body table-responsive'>";
                    rowHead.InnerHtml += "<table class='table table-hover table-condensed table-sm' width='100%'>";
                    rowHead.InnerHtml += "<thead  class='thead-light'>";
                    rowHead.InnerHtml += "<th>หน่วย</th>";
                    rowHead.InnerHtml += "<th>อัตราเต็ม</th>";
                    rowHead.InnerHtml += "<th>อัตราปัจจุบัน</th>";
                    rowHead.InnerHtml += "<th>อัตราขาด</th>";
                    rowHead.InnerHtml += "</thead>";
                    rowHead.InnerHtml += "<tbody>";
                }

                rowHead.InnerHtml += "<tr class='thead-light'>";
                rowHead.InnerHtml += "<td>" + rs.GetString("affi_name") + "</td>";
                rowHead.InnerHtml += "<td>" + rs.GetString("manpower_full") + "</td>";
                rowHead.InnerHtml += "<td>" + rs.GetString("num") + "</td>";
                rowHead.InnerHtml += getColorNum(rs.GetString("dif")) ;
                rowHead.InnerHtml += "</tr>";
                sum[0] += rs.GetInt32("manpower_full");
                sum[1] += rs.GetInt32("num");
                sum[2] += rs.GetInt32("dif");
                start++;
            }

            if (sum[0] != 0)
            {
                rowHead.InnerHtml += "<tr class='bg-light' style='text-decoration: underline;'>";
                rowHead.InnerHtml += "<td><b>รวม</b></td>";
                rowHead.InnerHtml += "<td><b>" + sum[0] + "</b></td>";
                rowHead.InnerHtml += "<td><b>" + sum[1] + "</b></td>";
                rowHead.InnerHtml += getColorNum(sum[2].ToString());
                rowHead.InnerHtml += "</tr>";
                rowHead.InnerHtml += "</tbody>";
                rowHead.InnerHtml += "</table>";
                rowHead.InnerHtml += "</div>";
                rowHead.InnerHtml += "</div>";
                rowHead.InnerHtml += "</div><br/>";
            }
            rs.Close();

            if (rowHead.InnerText == "")
            {
                rowShow.InnerHtml = "ไม่พบข้อมูล";
            }
        }

        public string getColorNum(string num)
        {
            int number = int.Parse(num);
            if (number < 0) { return "<td class='badge badge-warning'>&nbsp" + num+ "&nbsp</td>"; }
            if (number > 0) { return "<td class='badge badge-danger'>&nbsp" + num + "&nbsp</td>"; }
            return "<td class='badge badge-success'>&nbsp" + num + "&nbsp</td>"; 
        }
    }
}