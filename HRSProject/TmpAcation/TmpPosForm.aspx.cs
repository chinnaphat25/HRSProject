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
    public partial class TmpPosForm : System.Web.UI.Page
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

                string sql_pos = "SELECT * FROM tbl_pos";
                dBScript.GetDownList(txtPos, sql_pos, "pos_name", "pos_id");

                string sql_aff = "SELECT * FROM tbl_affiliation";
                dBScript.GetDownList(txtAff, sql_aff, "affi_name", "affi_id");

                string sql_empType = "SELECT * FROM tbl_type_emp";
                dBScript.GetDownList(txtEmpType, sql_empType, "type_emp_name", "type_emp_id");
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
                    string sql = "INSERT INTO tbl_tmp_pos (tmp_pos_emp_id,tmp_pos_pos_old_id,tmp_pos_aff_old_id,tmp_pos_emp_type_old_id,tmp_pos_pos_id,tmp_pos_aff_id,tmp_pos_emp_type_id,tmp_pos_date,tmp_pos_status ) VALUES ( '" + txtEmp.SelectedValue + "','"+dBScript.getEmpData("emp_pos_id", txtEmp.SelectedValue) + "','" + dBScript.getEmpData("emp_affi_id", txtEmp.SelectedValue) + "','" + dBScript.getEmpData("emp_type_emp_id", txtEmp.SelectedValue) + "', '" + txtPos.SelectedValue + "','"+txtAff.SelectedValue+"', '" + txtEmpType.SelectedValue + "', '" + txtDateSchedule.Text.Trim() + "', '0' )";
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
                    alert = "รูปแบบวันที่ไม่ถูกต้อง วัน-เดือน-ปี ตัวอย่างรูปแบบวันที่ "+DateTime.Now.ToString("dd-MM")+"-"+(DateTime.Now.Year+543);
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

        protected void TmpPosGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lbempName = (Label)(e.Row.FindControl("lbempName"));
            if (lbempName != null)
            {
                lbempName.Text = (string)DataBinder.Eval(e.Row.DataItem, "profix_name") + (string)DataBinder.Eval(e.Row.DataItem, "emp_name") + "  " + (string)DataBinder.Eval(e.Row.DataItem, "emp_lname");
            }

            Label lbempChengDate = (Label)(e.Row.FindControl("lbempChengDate"));
            if (lbempChengDate != null)
            {
                lbempChengDate.Text = dBScript.convertDatelongThai((string)DataBinder.Eval(e.Row.DataItem, "tmp_pos_date"));
            }

            // เหลือเวลา
            Label lbempAgeWork = (Label)(e.Row.FindControl("lbCountdown"));
            if (lbempAgeWork != null)
            {
                string[] data = DataBinder.Eval(e.Row.DataItem, "tmp_pos_date").ToString().Split('-');
                DateTime dateStart = DateTime.ParseExact(data[0] + "-" + data[1] + "-" + (int.Parse(data[2]) - 543), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateDifference dDiff = new DateDifference(dateStart);
                lbempAgeWork.Text = dDiff.ToString();

            }

            Button btnEdit = (Button)(e.Row.FindControl("btnEdit"));
            if (btnEdit != null)
            {
                btnEdit.CommandArgument = (string)DataBinder.Eval(e.Row.DataItem, "tmp_pos_id");
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

        protected void TmpPosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TmpPosGridView.PageIndex = e.NewPageIndex;
            //getSeclctEmp("emp_name", "ASC");
        }

        protected void TmpPosGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            alert = "";

            string sql = "DELETE FROM tbl_tmp_pos WHERE tmp_pos_id = '" + TmpPosGridView.DataKeys[e.RowIndex].Value + "'";
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
            TmpPosGridView.EditIndex = -1;
            BindData();
        }

        void BindData()
        {
            string sql = "SELECT tmp_pos_id, profix_name, tmp_pos_emp_id, ep.emp_name, ep.emp_lname, tp.tmp_pos_date, CONCAT(p.pos_name, ' / ', ta.affi_name,' / ',te.type_emp_name) AS pos_name, CONCAT(po.pos_name, ' / ', taf.affi_name,' / ',tem.type_emp_name) AS pos_name2 FROM tbl_tmp_pos tp JOIN tbl_emp_profile ep ON tp.tmp_pos_emp_id = ep.emp_id JOIN tbl_profix px ON px.profix_id = ep.emp_profix_id JOIN tbl_pos p ON p.pos_id = tp.tmp_pos_pos_id JOIN tbl_pos po ON po.pos_id = tp.tmp_pos_pos_old_id JOIN tbl_affiliation ta ON ta.affi_id = tp.tmp_pos_aff_id JOIN tbl_affiliation taf ON taf.affi_id = tp.tmp_pos_aff_old_id JOIN tbl_type_emp te ON te.type_emp_id = tp.tmp_pos_emp_type_id JOIN tbl_type_emp tem ON tp.tmp_pos_emp_type_old_id = tem.type_emp_id WHERE tp.tmp_pos_status = 0";
            MySqlDataAdapter da = dBScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            TmpPosGridView.DataSource = ds.Tables[0];
            TmpPosGridView.DataBind();
            LaGridViewData.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
        }

        void BindDataHis()
        {
            string sql = "SELECT tmp_pos_id, profix_name, tmp_pos_emp_id, ep.emp_name, ep.emp_lname, tp.tmp_pos_date, CONCAT(p.pos_name, ' / ', ta.affi_name,' / ',te.type_emp_name) AS pos_name, CONCAT(po.pos_name, ' / ', taf.affi_name,' / ',tem.type_emp_name) AS pos_name2 FROM tbl_tmp_pos tp JOIN tbl_emp_profile ep ON tp.tmp_pos_emp_id = ep.emp_id JOIN tbl_profix px ON px.profix_id = ep.emp_profix_id JOIN tbl_pos p ON p.pos_id = tp.tmp_pos_pos_id JOIN tbl_pos po ON po.pos_id = tp.tmp_pos_pos_old_id JOIN tbl_affiliation ta ON ta.affi_id = tp.tmp_pos_aff_id JOIN tbl_affiliation taf ON taf.affi_id = tp.tmp_pos_aff_old_id JOIN tbl_type_emp te ON te.type_emp_id = tp.tmp_pos_emp_type_id JOIN tbl_type_emp tem ON tp.tmp_pos_emp_type_old_id = tem.type_emp_id WHERE tp.tmp_pos_status = 1 LIMIT 0, 20";
            MySqlDataAdapter da = dBScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            TmpPosHisGridView.DataSource = ds.Tables[0];
            TmpPosHisGridView.DataBind();
            Label5.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
        }

        void ClearData()
        {
            txtEmp.SelectedIndex = 0;
            txtPos.SelectedIndex = 0;
            txtEmpType.SelectedIndex = 0;
            txtDateSchedule.Text = "";
        }

    }
}