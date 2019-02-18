using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using HRSProject.Config;
using HRSProject.Models;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace HRSProject.Profile
{
    public partial class ReportListEmp : System.Web.UI.Page
    {
        DBScript dBScript = new DBScript();
        //ReportDocument rpt = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("/Login/Login");
            }

            if (!this.IsPostBack)
            {
                dBScript.GetDownList(pos_id, "SELECT * FROM tbl_pos ORDER BY pos_name ASC", "pos_name", "pos_id");
                pos_id.Items.Insert(0, new ListItem("เลือก", ""));

                dBScript.GetDownList(cpoint_id, "SELECT IF(cpoint_id LIKE '60%','60',cpoint_id) AS cpoint_id1, IF(cpoint_id LIKE '60%','ฝ่ายฯ ',cpoint_name) AS cpoint_name FROM tbl_cpoint GROUP BY cpoint_id1 ORDER BY cpoint_id ASC", "cpoint_name", "cpoint_id1");
                cpoint_id.Items.Insert(0, new ListItem("เลือก", ""));

                GetReport("", "");
            }
        }

        private void GetReport(string pos, string cpoint)
        {
            //resultListEmp.RefreshReport();
            //string sql = "SELECT p.profix_name as profix, ep.emp_name as name, ep.emp_lname as lname, IF( cp.cpoint_id LIKE '60%', 'ฝ่ายฯ ', cp.cpoint_name ) AS cpoint_name, pos.pos_name, ep.emp_id, '' AS note, NOW() AS dateNote, CASE WHEN pos.pos_id = '1' THEN CONCAT( SUBSTRING_INDEX(pos.pos_name, ' ', 1), ' (รองฯ)' ) WHEN pos.pos_id = '2' THEN CONCAT( SUBSTRING_INDEX(pos.pos_name, ' ', 1), ' (คุมระบบฯ)' ) ELSE pos.pos_name END AS pos_row, REPLACE( REPLACE( REPLACE( REPLACE( cp.cpoint_name, 'ฝ่ายฯ ', '' ), '(', '' ), ')', '' ), 'ด่านฯ ', '' ) AS cpoint_row FROM tbl_emp_profile ep JOIN tbl_profix p ON p.profix_id = ep.emp_profix_id JOIN tbl_cpoint cp ON ep.emp_cpoint_id = cp.cpoint_id JOIN tbl_pos pos ON pos.pos_id = ep.emp_pos_id WHERE ep.emp_staus_working = '1' AND pos.pos_id = '" + pos + "' AND cp.cpoint_id LIKE '" + cpoint + "%' ORDER BY name,lname";
            ReportProfile reportProfile = new ReportProfile();


            //CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            //rpt.Load(Server.MapPath("FinalReport.rpt"));
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            crConnectionInfo.ServerName = "MySql DSN HR"; //Database server or ODBC
            crConnectionInfo.DatabaseName = "hrsystem"; // Database name
            crConnectionInfo.UserID = "adminhrs"; // username
            crConnectionInfo.Password = "admin25"; // password

            TableLogOnInfos crTableLogonInfos = new TableLogOnInfos();
            TableLogOnInfo crTableLogonInfo = new TableLogOnInfo();
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in reportProfile.Database.Tables)
            {
                crTableLogonInfo.TableName = table.Name;
                crTableLogonInfo.ConnectionInfo = crConnectionInfo;
                crTableLogonInfos.Add(crTableLogonInfo);
                table.ApplyLogOnInfo(crTableLogonInfo);
            }
            
            resultListEmp.LogOnInfo = crTableLogonInfos;
            //reportProfile.SetParameterValue("cpoint",cpoint_id.SelectedValue);
            //reportProfile.SetParameterValue("pos",pos_id.SelectedValue);
            //resultListEmp.ReportSource = reportProfile;

            
            //rpt.SetParameterValue("LAB_ID_SQL", 7);
            resultListEmp.ReportSource = reportProfile;

        }

        protected void cpoint_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pos_id.SelectedValue != "" && cpoint_id.SelectedValue != "")
            {
                GetReport(pos_id.SelectedValue, cpoint_id.SelectedValue);
            }
        }
    }
}