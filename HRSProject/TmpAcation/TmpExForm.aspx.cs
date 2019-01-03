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

namespace HRSProject.TmpAcation
{
    public partial class TmpExForm : System.Web.UI.Page
    {
        DBScript dBScript = new DBScript();
        public string alert = "";
        public string alertType = "";
        public string icon = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string sql = "SELECT emp_id,CONCAT(emp_name,' ',emp_lname) AS emp_name FROM tbl_emp_profile ep where emp_staus_working = '1' ORDER BY emp_name";
                dBScript.GetDownList(txtEmp, sql, "emp_name", "emp_id");
                txtEmp.Items.Insert(0, new ListItem("", ""));
                sql = "SELECT * FROM tbl_status_working WHERE status_working_id <> 1";
                dBScript.GetDownList(txtWorkingStatus, sql, "status_working_name", "status_working_id");
               
                BindData();
                BindDataHis();
            }
            if (Session["User"] != null)
            {
                if (dBScript.Notallow(new string[] { "5", "4", "3" }, Session["UserPrivilegeId"].ToString()))
                {
                    Response.Redirect("/");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Response.Write("<script>alert('" + txtEmp.SelectedValue + "');</script>");
            //Response.Write("<script>demo.showNotification('top','center');</script>");
            if (txtEmp.SelectedValue != "")
            {
                if (txtDateSchedule.Text.Length == 10)
                {
                    string sql = "INSERT INTO tbl_tmp_ex ( tmp_ex_emp, tmp_ex_status, tmp_ex_date, tmp_ex_note,tmp_ex_working_status ) VALUES ( '" + txtEmp.SelectedValue+"', '0', '"+txtDateSchedule.Text.Trim()+"', '"+txtNote.Text+"','"+txtWorkingStatus.SelectedValue+"' )";
                    if (dBScript.actionSql(sql))
                    {
                        icon = "add_alert";
                        alertType = "success";
                        alert = "บันทึกข้อมูลสำเร็จ";
                        ClearData();
                    }
                    else
                    {
                        icon = "error";
                        alertType = "danger";
                        alert = "Error : บันทึกล้มเหลว!!";
                    }
                }
                else
                {
                    icon = "warning";
                    alertType = "danger";
                    alert = "รูปแบบวันที่ไม่ถูกต้อง วัน-เดือน-ปี ตัวอย่างรูปแบบวันที่ " + DateTime.Now.ToString("dd-MM") + "-" + (DateTime.Now.Year + 543);
                }
            }
            else
            {
                icon = "warning";
                alertType = "danger";
                alert = "กรุณากรอกชื่อ-สกุล ให้ถูกต้อง";
            }

            BindData();
        }

        protected void TmpExGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lbempName = (Label)(e.Row.FindControl("lbempName"));
            if (lbempName != null)
            {
                lbempName.Text = (string)DataBinder.Eval(e.Row.DataItem, "profix_name") + (string)DataBinder.Eval(e.Row.DataItem, "emp_name") + "  " + (string)DataBinder.Eval(e.Row.DataItem, "emp_lname");
            }

            Label lbWorkingStatus = (Label)(e.Row.FindControl("lbWorkingStatus"));
            if (lbWorkingStatus != null)
            {
                lbWorkingStatus.Text = (string)DataBinder.Eval(e.Row.DataItem, "status_working_name");
            }

            Label lbempChengDate = (Label)(e.Row.FindControl("lbempChengDate"));
            if (lbempChengDate != null)
            {
                lbempChengDate.Text = dBScript.convertDatelongThai((string)DataBinder.Eval(e.Row.DataItem, "tmp_ex_date"));
            }

            // เหลือเวลา
            Label lbempAgeWork = (Label)(e.Row.FindControl("lbCountdown"));
            if (lbempAgeWork != null)
            {
                string[] data = DataBinder.Eval(e.Row.DataItem, "tmp_ex_date").ToString().Split('-');
                DateTime dateStart = DateTime.ParseExact(data[0] + "-" + data[1] + "-" + (int.Parse(data[2]) - 543), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateDifference dDiff = new DateDifference(dateStart);
                lbempAgeWork.Text = dDiff.ToString();

            }

            Button btnEdit = (Button)(e.Row.FindControl("btnEdit"));
            if (btnEdit != null)
            {
                btnEdit.CommandArgument = (string)DataBinder.Eval(e.Row.DataItem, "tmp_ex_id");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    ((LinkButton)e.Row.Cells[4].Controls[0]).OnClientClick = "return confirmDelete(this);";
                }
                catch { }
            }
        }

        protected void TmpExGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TmpExGridView.PageIndex = e.NewPageIndex;
            //getSeclctEmp("emp_name", "ASC");
        }

        protected void TmpExGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            alert = "";

            string sql = "DELETE FROM tbl_tmp_ex WHERE tmp_ex_id = '" + TmpExGridView.DataKeys[e.RowIndex].Value + "'";
            if (dBScript.actionSql(sql))
            {
                icon = "add_alert";
                alertType = "success";
                alert = "ลบข้อมูลสำเร็จ<br/>";
                //msgSuccess.Text = "ลบหน่วยสำเร็จ<br/>";
            }
            else
            {
                icon = "error";
                alertType = "danger";
                alert = "Error : ลบข้อมูลล้มเหลว<br/>";
                //msgErr.Text = "ลบหน่วยล้มเหลว<br/>";
            }
            TmpExGridView.EditIndex = -1;
            BindData();
        }

        void BindData()
        {
            string sql = "SELECT * FROM tbl_tmp_ex ex JOIN tbl_emp_profile ep ON ep.emp_id = ex.tmp_ex_emp JOIN tbl_profix px ON px.profix_id = ep.emp_profix_id JOIN tbl_status_working sw ON ex.tmp_ex_working_status = sw.status_working_id WHERE tmp_ex_status = 0";
            MySqlDataAdapter da = dBScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            TmpExGridView.DataSource = ds.Tables[0];
            TmpExGridView.DataBind();
            LaGridViewData.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
        }

        void BindDataHis()
        {
            string sql = "SELECT * FROM tbl_tmp_ex ex LEFT JOIN tbl_emp_profile ep ON ep.emp_id = ex.tmp_ex_emp LEFT JOIN tbl_profix px ON px.profix_id = ep.emp_profix_id JOIN tbl_status_working sw ON ex.tmp_ex_working_status = sw.status_working_id WHERE tmp_ex_status = 1 LIMIT 0,20";
            MySqlDataAdapter da = dBScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            HisTmpExGridViewGridView.DataSource = ds.Tables[0];
            HisTmpExGridViewGridView.DataBind();
            LabelHis.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
        }

        void ClearData()
        {
            txtEmp.SelectedIndex = 0;
            txtDateSchedule.Text = "";
            txtNote.Text = "";
        }

        protected void HisTmpExGridViewGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lbempName = (Label)(e.Row.FindControl("lbempName"));
            if (lbempName != null)
            {
                if (!DataBinder.Eval(e.Row.DataItem, "profix_name").Equals(DBNull.Value)) {
                    lbempName.Text = (string)DataBinder.Eval(e.Row.DataItem, "profix_name") + (string)DataBinder.Eval(e.Row.DataItem, "emp_name") + "  " + (string)DataBinder.Eval(e.Row.DataItem, "emp_lname");
                }
                else
                {
                    lbempName.Text = "ไม่พบชื่อในระบบ";
                }
            }

            Label lbWorkingStatus = (Label)(e.Row.FindControl("lbWorkingStatus"));
            if (lbWorkingStatus != null)
            {
                lbWorkingStatus.Text = (string)DataBinder.Eval(e.Row.DataItem, "status_working_name");
            }

            Label lbempChengDate = (Label)(e.Row.FindControl("lbempChengDate"));
            if (lbempChengDate != null)
            {
                lbempChengDate.Text = dBScript.convertDatelongThai((string)DataBinder.Eval(e.Row.DataItem, "tmp_ex_date"));
            }

            Label lbempAgeWork = (Label)(e.Row.FindControl("lbCountdown"));
            if (lbempAgeWork != null)
            {
                string[] data = DataBinder.Eval(e.Row.DataItem, "tmp_ex_date").ToString().Split('-');
                DateTime dateStart = DateTime.ParseExact(data[0] + "-" + data[1] + "-" + (int.Parse(data[2]) - 543), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateDifference dDiff = new DateDifference(dateStart);
                lbempAgeWork.Text = dDiff.ToStringNew();

            }
        }
    }
}