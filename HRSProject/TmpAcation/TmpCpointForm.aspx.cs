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
    public partial class TmpCpointForm : System.Web.UI.Page
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
                BindData();
                BindDataHis();

                string sql_cpoint = "SELECT * FROM tbl_cpoint";
                dBScript.GetDownList(txtCpoint, sql_cpoint, "cpoint_name", "cpoint_id");
            }
            if (Session["User"] != null)
            {
                if (dBScript.Notallow(new string[] { "5","4","3" }, Session["UserPrivilegeId"].ToString()))
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
                    string sql = "INSERT INTO tbl_tmp_cpoint ( tmp_cpoint_emp_id, tmp_cpoint_cpoint_id, tmp_cpoint_date, tmp_cpoint_status,tmp_cpoint_emp_pos,tmp_cpoint_emp_aff,tmp_cpoint_cpoint_old_id ) VALUES ( '" + txtEmp.SelectedValue+"', '"+txtCpoint.SelectedValue+"', '"+txtDateSchedule.Text.Trim()+"', '0','"+dBScript.getEmpData("emp_pos_id", txtEmp.SelectedValue) + "','" + dBScript.getEmpData("emp_affi_id", txtEmp.SelectedValue) + "','" + dBScript.getEmpData("emp_cpoint_id", txtEmp.SelectedValue) + "' )";
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

        protected void TmpCopintGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lbempName = (Label)(e.Row.FindControl("lbempName"));
            if (lbempName != null)
            {
                lbempName.Text = (string)DataBinder.Eval(e.Row.DataItem, "profix_name") + (string)DataBinder.Eval(e.Row.DataItem, "emp_name") + "  " + (string)DataBinder.Eval(e.Row.DataItem, "emp_lname");
            }

            Label lbempChengDate = (Label)(e.Row.FindControl("lbempChengDate"));
            if (lbempChengDate != null)
            {
                lbempChengDate.Text = dBScript.convertDatelongThai((string)DataBinder.Eval(e.Row.DataItem, "tmp_cpoint_date"));
            }

            // เหลือเวลา
            Label lbempAgeWork = (Label)(e.Row.FindControl("lbCountdown"));
            if (lbempAgeWork != null)
            {
                string[] data = DataBinder.Eval(e.Row.DataItem, "tmp_cpoint_date").ToString().Split('-');
                DateTime dateStart = DateTime.ParseExact(data[0] + "-" + data[1] + "-" + (int.Parse(data[2]) - 543), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateDifference dDiff = new DateDifference(dateStart);
                lbempAgeWork.Text = dDiff.ToString();

            }

            Button btnEdit = (Button)(e.Row.FindControl("btnEdit"));
            if (btnEdit != null)
            {
                btnEdit.CommandArgument = (string)DataBinder.Eval(e.Row.DataItem, "tmp_cpoint_id");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    ((LinkButton)e.Row.Cells[6].Controls[0]).OnClientClick = "return confirmDelete(this);";
                }
                catch { }
            }
        }

        protected void TmpCopintGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TmpCopintGridView.PageIndex = e.NewPageIndex;
            //getSeclctEmp("emp_name", "ASC");
        }

        protected void TmpCopintGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            alert = "";

            string sql = "DELETE FROM tbl_tmp_cpoint WHERE tmp_cpoint_id = '" + TmpCopintGridView.DataKeys[e.RowIndex].Value + "'";
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
            TmpCopintGridView.EditIndex = -1;
            BindData();
        }

        void BindData()
        {
            string sql = "SELECT tmp_cpoint_id, profix_name, tmp_cpoint_emp_id, ep.emp_name, ep.emp_lname, cp.tmp_cpoint_date, c.cpoint_name, co.cpoint_name AS cpoint_name2 FROM tbl_tmp_cpoint cp JOIN tbl_emp_profile ep ON cp.tmp_cpoint_emp_id = ep.emp_id JOIN tbl_profix px ON px.profix_id = ep.emp_profix_id JOIN tbl_cpoint c ON c.cpoint_id = cp.tmp_cpoint_cpoint_id JOIN tbl_cpoint co ON co.cpoint_id = ep.emp_cpoint_id WHERE cp.tmp_cpoint_status = 0";
            MySqlDataAdapter da = dBScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            TmpCopintGridView.DataSource = ds.Tables[0];
            TmpCopintGridView.DataBind();
            LaGridViewData.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
        }

        void BindDataHis()
        {
            string sql = "SELECT tmp_cpoint_id, profix_name, tmp_cpoint_emp_id, ep.emp_name, ep.emp_lname, cp.tmp_cpoint_date, c.cpoint_name, co.cpoint_name AS cpoint_name2 FROM tbl_tmp_cpoint cp JOIN tbl_emp_profile ep ON cp.tmp_cpoint_emp_id = ep.emp_id JOIN tbl_profix px ON px.profix_id = ep.emp_profix_id JOIN tbl_cpoint c ON c.cpoint_id = cp.tmp_cpoint_cpoint_id JOIN tbl_cpoint co ON co.cpoint_id = cp.tmp_cpoint_cpoint_old_id WHERE cp.tmp_cpoint_status = 1";
            MySqlDataAdapter da = dBScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            TmpCopintHisGridView.DataSource = ds.Tables[0];
            TmpCopintHisGridView.DataBind();
            Label4.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
        }

        void ClearData()
        {
            txtEmp.SelectedIndex = 0;
            txtCpoint.SelectedIndex = 0;
            txtDateSchedule.Text = "";
        }
    }
}