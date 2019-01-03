using CrystalDecisions.CrystalReports.Engine;
using HRSProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRSProject.LeaveEmp
{
    public partial class empLeaveForm : System.Web.UI.Page
    {
        DBScript dbScript = new DBScript();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                resultCard.Visible = false;

                string sql_year = "SELECT * FROM tbl_year";
                dbScript.GetDownList(txtYear, sql_year, "year", "year");

                string sql_cpoint = "SELECT * FROM tbl_cpoint";
                dbScript.GetDownList(txtCpoint, sql_cpoint, "cpoint_name", "cpoint_id");
                dbScript.GetDownList(txtSearchCpoint, sql_cpoint, "cpoint_name", "cpoint_id");
                txtSearchCpoint.Items.Insert(0, new ListItem("เลือก", ""));

                string sql_pos = "SELECT * FROM tbl_pos";
                dbScript.GetDownList(txtSearchPos, sql_pos, "pos_name", "pos_id");
                txtSearchPos.Items.Insert(0, new ListItem("เลือก", ""));

                string sql_affi = "SELECT * FROM tbl_affiliation";
                dbScript.GetDownList(txtSearchAffi, sql_affi, "affi_name", "affi_id");
                txtSearchAffi.Items.Insert(0, new ListItem("เลือก", ""));
            }

            if (Session["User"] != null)
            {
                if (Session["UserPrivilegeId"].ToString() == "5" && Session["emp_login_id"].ToString() != null)
                {
                    Response.Redirect("/LeaveEmp/empLeaveHistoryForm?empID=" + dbScript.getMd5Hash(Session["emp_login_id"].ToString()));
                }
            }
        }

        protected void btnSearchEmp_Click(object sender, EventArgs e)
        {
            BindData("emp_name", "ASC");
        }

        public void BindData(string sortDate, string sortType)
        {
            string strSeclctEmp = "";
            if (txtSearchId.Text != "") { strSeclctEmp += "emp_id LIKE '%" + txtSearchId.Text.Trim() + "%' "; }
            if (txtSearchName.Text != "") { if (strSeclctEmp != "") { strSeclctEmp += " AND "; } strSeclctEmp += "(profix_name LIKE '%" + txtSearchName.Text.Trim() + "%' OR emp_name LIKE '%" + txtSearchName.Text.Trim() + "%' OR emp_lname LIKE '%" + txtSearchName.Text.Trim() + "%')"; }
            if (txtSearchCpoint.SelectedValue != "") { if (strSeclctEmp != "") { strSeclctEmp += " AND "; } strSeclctEmp += "cpoint_name LIKE '%" + txtSearchCpoint.SelectedItem + "%' "; }
            if (txtSearchPos.SelectedValue != "") { if (strSeclctEmp != "") { strSeclctEmp += " AND "; } strSeclctEmp += "pos_name LIKE '%" + txtSearchPos.SelectedItem + "%' "; }
            if (txtSearchAffi.SelectedValue != "") { if (strSeclctEmp != "") { strSeclctEmp += " AND "; } strSeclctEmp += "affi_name LIKE '%" + txtSearchAffi.SelectedItem + "%' "; }

            if (strSeclctEmp != "")
            {
                strSeclctEmp = "(" + strSeclctEmp + ") AND ";
            }

            string sql = "SELECT * FROM tbl_emp_profile JOIN tbl_profix ON emp_profix_id = profix_id JOIN tbl_cpoint ON emp_cpoint_id = cpoint_id JOIN tbl_pos ON emp_pos_id = pos_id JOIN tbl_affiliation ON affi_id = emp_affi_id JOIN tbl_type_emp ON type_emp_id = emp_type_emp_id JOIN tbl_type_add ON type_add_id = emp_add_type WHERE " + strSeclctEmp + " emp_staus_working = '1' ORDER BY " + sortDate + " " + sortType + " LIMIT 0,20";
            Session["sqlEmp"] = sql;
            MySqlDataAdapter da = dbScript.getDataSelect(sql);
            DataTable ds = new DataTable();
            da.Fill(ds);
            GridViewEmp.DataSource = ds;
            GridViewEmp.DataBind();
            LaGridViewData.Text = "พบข้อมูลจำนวน " + ds.Rows.Count + " แถว";

            resultCard.Visible = true;
            dbScript.CloseConnection();
        }

        protected void GridViewEmp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string date_start = (string)DataBinder.Eval(e.Row.DataItem, "emp_start_working");
            if (dbScript.getEmpData("type_emp_id", (string)DataBinder.Eval(e.Row.DataItem, "emp_id")) == "5")
            {
                string sql = "SELECT exp_moterway_start FROM tbl_exp_moterway WHERE exp_moterway_emp_id = '" + (string)DataBinder.Eval(e.Row.DataItem, "emp_id") + "' AND exp_moterway_end = '00-00-0000'";
                MySqlDataReader rs = dbScript.selectSQL(sql);
                if (rs.Read())
                {
                    date_start = rs.GetString("exp_moterway_start");
                }
                else
                {
                    date_start = DateTime.Now.ToString("dd-MM-") + (DateTime.Now.Year + 543);
                }
                rs.Close();
                dbScript.CloseConnection();
            }
            if (date_start == null) { date_start = DateTime.Now.ToString("dd-MM-") + (DateTime.Now.Year + 543); }

            Leave leave = new Leave(date_start);
            Leave leaveUser = new Leave((string)DataBinder.Eval(e.Row.DataItem, "emp_id"), int.Parse(dbScript.getBudgetYear()));

            Label lbSick = (Label)(e.Row.FindControl("lbSick"));
            if (lbSick != null)
            {
                if (leaveUser.UserSick <= leave.Sick)
                {
                    lbSick.Text = leaveUser.UserSick + " / " + leave.Sick;
                }
                else
                {
                    lbSick.CssClass = "text-danger";
                    lbSick.Text = leaveUser.UserSick + " / " + leave.Sick;
                }
            }

            Label lbRelax = (Label)(e.Row.FindControl("lbRelax"));
            if (lbRelax != null)
            {
                if (leaveUser.UserRelax <= leave.Relax)
                {
                    lbRelax.Text = leaveUser.UserRelax + " / " + leave.Relax;
                }
                else
                {
                    lbRelax.CssClass = "text-danger";
                    lbRelax.Text = leaveUser.UserRelax + " / " + leave.Relax;
                }
            }

            Label lbMaternity = (Label)(e.Row.FindControl("lbMaternity"));
            if (lbMaternity != null)
            {
                if (leaveUser.UserMaternity <= leave.Maternity)
                {
                    lbMaternity.Text = leaveUser.UserMaternity + " / " + leave.Maternity;
                }
                else
                {
                    lbMaternity.CssClass = "text-danger";
                    lbMaternity.Text = leaveUser.UserMaternity + " / " + leave.Maternity;
                }
            }

            Label lbOrdain = (Label)(e.Row.FindControl("lbOrdain"));
            if (lbOrdain != null)
            {
                if (leave.Ordain > 0)
                {
                    lbOrdain.CssClass = "text-success";
                    lbOrdain.Text = leaveUser.UserOrdain + " / มีสิทธิลา";
                }
                else
                {
                    lbOrdain.CssClass = "text-danger";
                    lbOrdain.Text = leaveUser.UserOrdain + " / ไม่มีสิทธิลา";
                }
            }

            Label lbMilitary = (Label)(e.Row.FindControl("lbMilitary"));
            if (lbMilitary != null)
            {
                if (leaveUser.UserMilitary <= leave.Military)
                {
                    lbMilitary.Text = leaveUser.UserMilitary + " / " + leave.Military;
                }
                else
                {
                    lbMilitary.CssClass = "text-danger";
                    lbMilitary.Text = leaveUser.UserMilitary + " / " + leave.Military;
                }
            }

            Label lbStartDate = (Label)(e.Row.FindControl("lbStartDate"));
            if (lbStartDate != null)
            {
                string[] data = date_start.Split('-');
                DateTime dateStart = DateTime.ParseExact(data[0] + "-" + data[1] + "-" + (int.Parse(data[2]) - 543), "dd-MM-yyyy", CultureInfo.InvariantCulture);

                if (dateStart.Date <= DateTime.Now.Date)
                {
                    if (dateStart.Date.ToString("dd-MM-yyyy") == DateTime.Now.Date.ToString("dd-MM-yyyy"))
                    {
                        lbStartDate.CssClass = "text-danger";
                        lbStartDate.Text = leave.Date.ToString() + " / " + new DateDifference(dateStart).ToString();
                    }
                    else
                    {
                        lbStartDate.Text = leave.Date.ToString() + " / " + new DateDifference(dateStart).ToString();
                    }
                }
                else
                {
                    lbStartDate.Text = leave.Date.ToString() + " / <span class='badge badge-pill badge-danger'>ข้อมูลวันเริ่มงานผิดพลาด</span>";
                }
            }

            Label lbYear = (Label)(e.Row.FindControl("lbYear"));
            if (lbYear != null)
            {
                lbYear.Text = leave.BudgetYear.ToString();
            }

            Label lbEmpType = (Label)(e.Row.FindControl("lbEmpType"));
            if (lbEmpType != null)
            {
                lbEmpType.Text = (string)DataBinder.Eval(e.Row.DataItem, "type_emp_name");
            }

            LinkButton btnView = (LinkButton)(e.Row.FindControl("btnView"));
            if (btnView != null)
            {
                btnView.CommandArgument = (string)DataBinder.Eval(e.Row.DataItem, "emp_id");
            }

            dbScript.CloseConnection();
        }

        protected void btnView_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect("/LeaveEmp/empLeaveHistoryForm?empID=" + dbScript.getMd5Hash(e.CommandArgument.ToString()));
            dbScript.CloseConnection();
        }

        public void GetDownList(DropDownList list, string sql, string text, string value)
        {
            MySqlDataReader rs = dbScript.selectSQL(sql);
            using (var reader = dbScript.selectSQL(sql))
            {
                if (reader.HasRows)
                {
                    list.DataSource = reader;
                    list.DataValueField = value;
                    list.DataTextField = text;
                    list.DataBind();
                }
            }
            dbScript.CloseConnection();
        }

        protected void btnShowReport_Click(object sender, EventArgs e)
        {
            ReportDocument rpt = new ReportDocument();
            rpt.Load(MapPath("/Report/reportLeaveEmp.rpt"));

            rpt.SetParameterValue("cpoint_name", txtCpoint.SelectedItem.Text);
            rpt.SetParameterValue("txt_year", txtYear.Text);
            rpt.SetParameterValue("date_between", "");
            Session["Report"] = rpt;
            Session["ReportTitle"] = "สรุปทะเบียนวันลาของพนักงาน";
            Response.Write("<script>");
            Response.Write("window.open('/Report/reportView','_blank')");
            Response.Write("</script>");
        }

        protected void btnSearchClear_Click(object sender, EventArgs e)
        {
            txtSearchId.Text = "";
            txtSearchName.Text = "";
            txtSearchCpoint.SelectedIndex = 0;
            txtSearchPos.SelectedIndex = 0;
            txtSearchAffi.SelectedIndex = 0;
            GridViewEmp.DataSource = null;
            GridViewEmp.DataBind();
            resultCard.Visible = false;
        }
    }
}