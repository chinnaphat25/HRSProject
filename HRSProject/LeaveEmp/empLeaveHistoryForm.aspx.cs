using HRSProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRSProject.LeaveEmp
{

    public partial class empLeaveHistoryForm : System.Web.UI.Page
    {
        DBScript dBScript = new DBScript();
        Leave leave;
        string msgError = "";
        Leave leaveUser; 
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request.Params["empID"]))
            {
                if (dBScript.CheckPrivilege(Session["UserPrivilegeId"].ToString(), "Assistant"))
                {
                    Button1.Visible = true;
                }
                else
                {
                    Button1.Visible = false;
                }

                if (dBScript.getEmpIDMD5("type_emp_id", Request.Params["empID"]) == "5")
                {
                    string sql = "SELECT exp_moterway_start FROM tbl_exp_moterway WHERE exp_moterway_emp_id = '" + dBScript.getEmpIDMD5("emp_id", Request.Params["empID"]) + "' AND exp_moterway_end = '00-00-0000'";
                    MySqlDataReader rs = dBScript.selectSQL(sql);
                    if (rs.Read())
                    {
                        leave = new Leave(rs.GetString("exp_moterway_start"),true);
                    }
                    else
                    {
                        leave = new Leave(DateTime.Now.ToString("dd-MM-") + (DateTime.Now.Year + 543),true);
                    }
                    rs.Close();
                    dBScript.CloseConnection();
                }
                else
                {
                    leave = new Leave(dBScript.getEmpIDMD5("emp_start_working", Request.Params["empID"]),false);
                }
                //empId.Text = Request.Params["empID"].ToString().Trim();pos_name
                leaveUser = new Leave(dBScript.getEmpIDMD5("emp_id", Request.Params["empID"]), int.Parse(dBScript.getBudgetYear()));

                lbTypeEmp.Text = dBScript.getEmpIDMD5("type_emp_name", Request.Params["empID"]);
                lbEmpName.Text = dBScript.getEmpIDMD5("emp_id", Request.Params["empID"]) + " " + dBScript.getEmpIDMD5("profix_name", Request.Params["empID"]) + dBScript.getEmpIDMD5("emp_name", Request.Params["empID"]) + " " + dBScript.getEmpIDMD5("emp_lname", Request.Params["empID"]);
                lbPos.Text = dBScript.getEmpIDMD5("pos_name", Request.Params["empID"])+"        / ด่านฯ : "+ dBScript.getEmpIDMD5("cpoint_name", Request.Params["empID"]);
                lbExp6Month.Text = leave.Date6Month;
                lbExp1Year.Text = leave.Date1Year;
                lbStartDate.Text = leave.Date;
                lbYear.Text = leave.BudgetYear;

                lbSick.Text = leaveUser.UserSick +" / "+ leave.Sick.ToString("0 วัน");
                lbSick.CssClass = leaveUser.UserSick > leave.Sick? "text-danger" : "";

                lbRelax.Text = leaveUser.UserRelax + " / " + leave.Relax.ToString("0 วัน");
                lbRelax.CssClass = leaveUser.UserRelax > leave.Relax ? "text-danger" : "";

                lbOrdain.Text = leave.Ordain > 0 ? "มีสิทธิลา" : "ไม่มีสิทธิลา";

                lbMaternity.Text = leaveUser.UserMaternity + " / " + leave.Maternity.ToString("0 วัน");
                lbMaternity.CssClass = leaveUser.UserMaternity > leave.Maternity ? "text-danger" : "";

                lbMilitary.Text = leaveUser.UserMilitary + " / " + leave.Military.ToString("0 วัน");
                lbMilitary.CssClass = leaveUser.UserMilitary > leave.Military ? "text-danger" : "";


                dBScript.CloseConnection();
            }
            else
            {
                Response.Redirect("/LeaveEmp/empLeaveForm");
            }

            if (!this.IsPostBack)
            {

                BindData();
            }
        }

        protected void btnSaveLeave_Click(object sender, EventArgs e)
        {
            string emp_leave_sick = "";
            string emp_leave_relax = "";
            string emp_leave_ordain = "";
            string emp_leave_maternity = "";
            string emp_leave_military = "";
            string emp_leave_deduction_wages = "";

            string emp_medical_certificate = rbMedicalCertificate.SelectedValue;
            string emp_leave_date_start = txtStartLeave.Text.Trim();
            string emp_leave_date_end = txtEndLeave.Text.Trim();
            string emp_leave_emp_id = dBScript.getEmpIDMD5("emp_id", Request.Params["empID"]);
            string emp_leave_note = txtNote.Text.Trim();

            switch (rdTypeLeave.SelectedValue)
            {
                case "1":
                    emp_leave_sick = txtTotalDay.Text.Trim();
                    break;
                case "2": emp_leave_relax = txtTotalDay.Text.Trim(); ;
                    break;
                case "3": emp_leave_maternity = txtTotalDay.Text.Trim();
                    break;
                case "4": emp_leave_military = txtTotalDay.Text.Trim();
                    break;
            }

            if (ckTypeLeave.Checked)
            {
                emp_leave_ordain = "true";
            }
            else
            {
                emp_leave_ordain = "false";
            }

            switch (rbDeductionWages.SelectedValue)
            {
                case "0":
                    emp_leave_deduction_wages = "0";
                    break;
                case "1":
                    emp_leave_deduction_wages = txtTotalDay.Text.Trim();
                    break;
                case "2":
                    emp_leave_deduction_wages = txtDeductionWages.Text.Trim();
                    break;
            }

            string sql = "INSERT INTO tbl_emp_leave ( emp_leave_sick, emp_leave_relax, emp_leave_ordain, emp_leave_maternity, emp_leave_military, emp_leave_deduction_wages, emp_medical_certificate, emp_leave_date_start, emp_leave_date_end, emp_leave_emp_id, emp_leave_year, emp_leave_note ) VALUES ( '"+ emp_leave_sick + "', '"+ emp_leave_relax + "', '"+emp_leave_ordain+"', '"+emp_leave_maternity+"', '"+emp_leave_military+"', '"+emp_leave_deduction_wages+"', '"+emp_medical_certificate+"', '"+emp_leave_date_start+"', '"+emp_leave_date_end+"', '"+emp_leave_emp_id+"', '"+dBScript.getBudgetYear(emp_leave_date_start) +"', '"+emp_leave_note+"' )";
            if (dBScript.actionSql(sql))
            {
                msgError = "บันทึกข้อมูลสำเร็จ";
            }
            else
            {
                msgError = "ERROR : บันทึกข้อมูลลมเหลว";
            }

            string script = "alert('" + msgError + "');";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
            BindData();
        }

        protected void GridLeaveEmp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //วันเริ่มต้น
            Label lbLeaveStart = (Label)(e.Row.FindControl("lbLeaveStart"));
            if (lbLeaveStart != null)
            {
                lbLeaveStart.Text = dBScript.convertDatelongThai((string)DataBinder.Eval(e.Row.DataItem, "emp_leave_date_start"));
            }

            //วันเริ่มสิ้นสุด
            Label lbLeaveEnd = (Label)(e.Row.FindControl("lbLeaveEnd"));
            if (lbLeaveEnd != null)
            {
                lbLeaveEnd.Text = dBScript.convertDatelongThai((string)DataBinder.Eval(e.Row.DataItem, "emp_leave_date_end"));
            }

            //ใบรับรองแพทย์
            Label lbLeaveMedicalCertificate = (Label)(e.Row.FindControl("lbLeaveMedicalCertificate"));
            if (lbLeaveMedicalCertificate != null)
            {
                if ((bool)DataBinder.Eval(e.Row.DataItem, "emp_medical_certificate"))
                {
                    lbLeaveMedicalCertificate.Text = "<i class='fa fa-check-circle-o text-success'></i>";
                }
                else
                {
                    lbLeaveMedicalCertificate.Text = "<i class='fa fa-times-circle-o text-danger'></i>";
                }
                
            }

            //ตัดค่าจ้าง
            Label lbLeaveDeductionWages = (Label)(e.Row.FindControl("lbLeaveDeductionWages"));
            if (lbLeaveDeductionWages != null)
            {
                if ((int)DataBinder.Eval(e.Row.DataItem, "emp_leave_deduction_wages")>0)
                {
                    int sum = (int)DataBinder.Eval(e.Row.DataItem, "emp_leave_sick") + (int)DataBinder.Eval(e.Row.DataItem, "emp_leave_relax") + (int)DataBinder.Eval(e.Row.DataItem, "emp_leave_maternity");
                    if ((int)DataBinder.Eval(e.Row.DataItem, "emp_leave_deduction_wages") == sum)
                    {
                        lbLeaveDeductionWages.CssClass = "text-danger";
                        lbLeaveDeductionWages.Text = "ตัดค่าจ้างเต็ม";
                    }
                    else
                    {
                        lbLeaveDeductionWages.CssClass = "text-danger";
                        lbLeaveDeductionWages.Text = "ตัดค่าจ้าง "+ (int)DataBinder.Eval(e.Row.DataItem, "emp_leave_deduction_wages") + " วัน";
                    }
                    
                }
                else
                {
                    lbLeaveDeductionWages.CssClass = "text-success";
                    lbLeaveDeductionWages.Text = "ไม่ตัดค่าจ้าง";
                }

            }
        }

        public void BindData()
        {
            string sql = "SELECT  emp_leave_id,emp_leave_date_start,  emp_leave_date_end,  emp_leave_sick,  emp_leave_relax,  emp_leave_maternity,  emp_medical_certificate,  emp_leave_deduction_wages,  emp_leave_note FROM  tbl_emp_leave  WHERE emp_leave_emp_id = '"+dBScript.getEmpIDMD5("emp_id", Request.Params["empID"]) +"' AND emp_leave_year ='"+dBScript.getBudgetYear()+"' ORDER BY emp_leave_id ASC";
            MySqlDataAdapter da = dBScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridLeaveEmp.DataSource = ds.Tables[0];
            GridLeaveEmp.DataBind();
            if (ds.Tables[0].Rows.Count != 0)
            {
                Labeltxt.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
            }
            else
            {
                Labeltxt.Text = "ไม่พบข้อมูล";
            }
        }
    }
}