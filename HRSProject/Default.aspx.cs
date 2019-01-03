using HRSProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRSProject
{
    public partial class _Default : Page
    {
        DBScript dBScript = new DBScript();

        protected void Page_Load(object sender, EventArgs e)
        {
            lbUserOnline.Text = int.Parse(Application["TotalOnlineUsers"].ToString()).ToString(" 0 คน");

            if (!this.IsPostBack)
            {
                string sql = "SELECT COUNT('id') AS EmpSum FROM tbl_emp_profile WHERE emp_staus_working = '1'";
                MySqlDataReader rs = dBScript.selectSQL(sql);
                if (rs.Read())
                {
                    lbCountEmp.Text = "ทั้งหมด "+rs.GetString("EmpSum") +" คน";
                }
                rs.Close();
                BindDataLeave();
                dBScript.CloseConnection();

                lbYear.Text = dBScript.getBudgetYear();
            }
            if (Session["User"] != null)
            {
                if (dBScript.Notallow(new string[] { "5" }, Session["UserPrivilegeId"].ToString()))
                {
                    Response.Redirect("/Profile/empViwe");
                }

                if (dBScript.Notallow(new string[] { "5","4","3" }, Session["UserPrivilegeId"].ToString()))
                {
                    boxChangPos.Visible = false;
                    boxMigrateEmp.Visible = false;
                    boxResignEmp.Visible = false;
                }
            }
        }

        void BindDataLeave()
        {
            string sql = "SELECT emp_leave_emp_id,CONCAT(p.profix_name,' ',e.emp_name,' ',e.emp_lname) AS emp_name, COUNT(emp_leave_emp_id) AS total,SUM(emp_leave_sick) AS sick,SUM(emp_leave_relax) AS relax FROM tbl_emp_leave l JOIN tbl_emp_profile e ON l.emp_leave_emp_id = e.emp_id JOIN tbl_profix p ON p.profix_id = e.emp_profix_id WHERE emp_leave_year ='" + dBScript.getBudgetYear() + "' GROUP BY emp_leave_emp_id ORDER BY total,sick DESC LIMIT 0,10";
            MySqlDataAdapter da = dBScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            LeaveStatisticsGridView.DataSource = ds.Tables[0];
            LeaveStatisticsGridView.DataBind();
            if (ds.Tables[0].Rows.Count <= 0)
            {
                lbLeaveStatisticsNull.Text = "ไม่พบข้อมูลการลาของพนักงาน";
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Page_Load(null, null);
        }
    }
}