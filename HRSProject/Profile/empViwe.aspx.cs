using System;
using HRSProject.Config;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;

namespace HRSProject.Profile
{
    public partial class empViwe : System.Web.UI.Page
    {
        DBScript dbScript = new DBScript();
        DataTable ds;
        public string alert = "";
        public string alertType = "";
        public string icon = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Params["add"]))
            {
                //Response.Write("<script>$('#ModalCheckEmp').modal('show');</script>");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalCheckEmp", "$('#ModalCheckEmp').modal('show');", true);
            }

            if (!this.IsPostBack)
            {
                //getSeclctEmp("emp_name", "ASC");
                DivEmp.Visible = false;

                string sql_cpoint = "SELECT * FROM tbl_cpoint";
                dbScript.GetDownList(txtCpoint, sql_cpoint, "cpoint_name", "cpoint_id");
                dbScript.GetDownList(txtSearchCpoint, sql_cpoint, "cpoint_name", "cpoint_id");
                txtSearchCpoint.Items.Insert(0, new ListItem("เลือก", ""));

                string sql_pos = "SELECT * FROM tbl_pos";
                dbScript.GetDownList(txtPos, sql_pos, "pos_name", "pos_id");
                dbScript.GetDownList(txtSearchPos, sql_pos, "pos_name", "pos_id");
                txtSearchPos.Items.Insert(0, new ListItem("เลือก", ""));

                string sql_affi = "SELECT * FROM tbl_affiliation";
                dbScript.GetDownList(txtAffi, sql_affi, "affi_name", "affi_id");
                dbScript.GetDownList(txtSearchAffi, sql_affi, "affi_name", "affi_id");
                txtSearchAffi.Items.Insert(0, new ListItem("เลือก", ""));
            }
            dbScript.CloseConnection();

            if (Session["UserPrivilegeId"].ToString() == "5" && Session["emp_login_id"].ToString()!= null)
            {
                Response.Redirect("/Profile/empForm?empID=" + dbScript.getMd5Hash(Session["emp_login_id"].ToString()));
            }
        }

        protected void GridViewEmp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //รหัสบุคคล
            Label lbempID = (Label)(e.Row.FindControl("lbempID"));
            if (lbempID != null)
            {
                lbempID.Text = (string)DataBinder.Eval(e.Row.DataItem, "emp_id");
            }


            // ชื่อ - สกุล
            Label lbempName = (Label)(e.Row.FindControl("lbempName"));
            if (lbempName != null)
            {
                lbempName.Text = (string)DataBinder.Eval(e.Row.DataItem, "profix_name") + (string)DataBinder.Eval(e.Row.DataItem, "emp_name") + "  " + (string)DataBinder.Eval(e.Row.DataItem, "emp_lname");
            }

            // สังกัดด่านฯ
            Label lbempCpoint = (Label)(e.Row.FindControl("lbempCpoint"));
            if (lbempCpoint != null)
            {
                lbempCpoint.Text = (string)DataBinder.Eval(e.Row.DataItem, "cpoint_name");
            }

            // ตำแหน่ง
            Label lbempPos = (Label)(e.Row.FindControl("lbempPos"));
            if (lbempPos != null)
            {
                lbempPos.Text = (string)DataBinder.Eval(e.Row.DataItem, "pos_name");
            }

            // หน่วย
            Label lbempAff = (Label)(e.Row.FindControl("lbempAff"));
            if (lbempAff != null)
            {
                lbempAff.Text = (string)DataBinder.Eval(e.Row.DataItem, "affi_name");
            }

            // ประเภทพนักงาน
            Label lbempType = (Label)(e.Row.FindControl("lbempType"));
            if (lbempType != null)
            {
                lbempType.Text = (string)DataBinder.Eval(e.Row.DataItem, "type_emp_name");
            }

            // อายุงาน
            Label lbempAgeWork = (Label)(e.Row.FindControl("lbempAgeWork"));
            if (lbempAgeWork != null)
            {
                string[] data = DataBinder.Eval(e.Row.DataItem, "emp_start_working").ToString().Split('-');
                DateTime dateStart = DateTime.ParseExact(data[0] + "-" + data[1] + "-" + (int.Parse(data[2]) - 543), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                if (dateStart.Date <= DateTime.Now.Date)
                {
                    DateDifference dDiff = new DateDifference(dateStart);
                    lbempAgeWork.Text = dDiff.ToString();
                }
                else
                {
                    lbempAgeWork.Text = "<span class='badge badge-pill badge-danger'>ข้อมูลวันเริ่มงานผิดพลาด</span>";
                }
            }

            // อายุงาน
            Label lbempAgeWorkNow = (Label)(e.Row.FindControl("lbempAgeWorkNow"));
            if (lbempAgeWorkNow != null)
            {
                try
                {
                    string sql = "SELECT * FROM tbl_exp_moterway WHERE exp_moterway_emp_id = '" + (string)DataBinder.Eval(e.Row.DataItem, "emp_id") + "' AND exp_moterway_end = '00-00-0000'";
                    MySqlDataReader rs = dbScript.selectSQL(sql);
                    if (rs.Read())
                    {
                        string[] data = rs.GetString("exp_moterway_start").ToString().Split('-');
                        DateTime dateStart = DateTime.ParseExact(data[0] + "-" + data[1] + "-" + (int.Parse(data[2]) - 543), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        if (dateStart.Date <= DateTime.Now.Date)
                        {
                            DateDifference dDiff = new DateDifference(dateStart);
                            lbempAgeWorkNow.Text = dDiff.ToString();
                        }
                        else
                        {
                            lbempAgeWorkNow.Text = "<span class='badge badge-pill badge-danger'>ข้อมูลวันเริ่มงานตำแหน่งปัจจุบันผิดพลาด</span>";
                        }
                        rs.Close();
                    }
                    else
                    {
                        lbempAgeWorkNow.Text = "-";
                    }
                }
                catch { lbempAgeWorkNow.Text = "มีข้อผิดพลาด"; }
            }

            Button txtEmpViwe = (Button)(e.Row.FindControl("txtEmpViwe"));
            if (txtEmpViwe != null)
            {
                txtEmpViwe.CommandArgument = (string)DataBinder.Eval(e.Row.DataItem, "emp_id");
            }
            dbScript.CloseConnection();
        }

        public void getSeclctEmp(string sortDate, string sortType)
        {
            System.Threading.Thread.Sleep(5000);
            //DivLoad.Visible = true;
            

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

            string sql = "SELECT * FROM tbl_emp_profile JOIN tbl_profix ON emp_profix_id = profix_id JOIN tbl_cpoint ON emp_cpoint_id = cpoint_id JOIN tbl_pos ON emp_pos_id = pos_id JOIN tbl_affiliation ON affi_id = emp_affi_id JOIN tbl_type_emp ON type_emp_id = emp_type_emp_id JOIN tbl_type_add ON type_add_id = emp_add_type  WHERE " + strSeclctEmp + " emp_staus_working = '1' ORDER BY " + sortDate + " " + sortType;
            Session["sqlEmp"] = sql;
            MySqlDataAdapter da = dbScript.getDataSelect(sql);
            ds = new DataTable();
            da.Fill(ds);
            GridViewEmp.DataSource = ds;
            GridViewEmp.DataBind();
            LaGridViewData.Text = "พบข้อมูลจำนวน " + ds.Rows.Count + " แถว";


            if (GridViewEmp.DataSource != null)
            {
                DivEmp.Visible = true;
            }
            dbScript.CloseConnection();
            //DivLoad.Visible = false;
        }

        protected void btnSearchEmp_Click(object sender, EventArgs e)
        {
            getSeclctEmp("emp_name", "ASC");
        }
        protected void txtEmpViwe_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect("/Profile/empForm?empID=" + dbScript.getMd5Hash(e.CommandArgument.ToString()));
            dbScript.CloseConnection();
        }

        protected void GridViewEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewEmp.PageIndex = e.NewPageIndex;
            getSeclctEmp("emp_name", "ASC");
        }

        protected void btnAddOldEmp_Click(object sender, EventArgs e)
        {
            alert = "";
            string empId_new = "";
            string empId_old = dbScript.getEmpDataIDCard("emp_id", txtIdcard.Text.Trim());
            string sql = "SELECT * FROM tbl_emp_profile WHERE emp_id_card = '" + txtIdcard.Text.Trim() + "' AND emp_staus_working <> 1";
            MySqlDataReader rs = dbScript.selectSQL(sql);
            if (rs.Read())
            {
                rs.Close();
                empId_new = dbScript.createEmpId(txtStartDate);

                if (dbScript.actionSql("UPDATE tbl_emp_profile SET emp_id = '" + empId_new + "',emp_staus_working='1',emp_pos_id='" + txtPos.SelectedValue + "',emp_affi_id='" + txtAffi.SelectedValue + "',emp_cpoint_id='" + txtCpoint.SelectedValue + "' WHERE id='" + dbScript.getEmpDataIDCard("id", txtIdcard.Text.Trim()) + "'"))
                {
                    dbScript.actionSql("DELETE FROM tbl_exp_moterway WHERE emp_id='" + empId_old + "'");
                    dbScript.actionSql("DELETE FROM tbl_work_history WHERE emp_id='" + empId_old + "'");


                    string insert_history_text = "history_status_id,history_date,history_note,history_emp_id";
                    string insert_history_value = "'1','00-00-0000','', '" + dbScript.getEmpDataIDCard("id", txtIdcard.Text.Trim()) + "'";
                    string insert_history = "INSERT INTO tbl_history (" + insert_history_text + ") VALUES (" + insert_history_value + ")";
                    dbScript.actionSql(insert_history);

                    Response.Redirect("/Profile/empForm?empID=" + dbScript.getMd5Hash(empId_new));
                }
                else
                {
                    alert += "บันทึกข้อมูลล้มเหลว ลองใหม่อีกครั้ง<br/>"; alertType = "danger"; icon = "error";
                    //msgErr.Text = "บันทึกข้อมูลล้มเหลว ลองใหม่อีกครั้ง";
                }
            }
            else
            {
                alert += "ผิดพลาดไม่พบข้อมูล รหัสบัตรประจำตัวประชาชน : " + txtIdcard.Text + "< br/>"; alertType = "danger"; icon = "error";
                //msgErr.Text = "ผิดพลาดไม่พบข้อมูล รหัสบัตรประจำตัวประชาชน : " + txtIdcard.Text;
            }
            dbScript.CloseConnection();
        }

        protected void btnPrintCard_Command(object sender, CommandEventArgs e)
        {
            //Response.Redirect("/Profile/printCardForm?id="+ dbScript.getMd5Hash(e.CommandArgument.ToString()));
        }

        protected void btnCheckEmp_Click(object sender, EventArgs e)
        {
            alert = "";
            string sql = "SELECT * FROM tbl_emp_profile WHERE emp_id_card='" + txtNewIDCard.Text.Trim() + "'";
            MySqlDataReader rs = dbScript.selectSQL(sql);
            if (!rs.Read())
            {
                if (!dbScript.checkIDCard(txtNewIDCard.Text.Trim())) { dimsgErr.Text += "- เลขบัตรประจำตัวประชาชนไม่ถูกต้องกรุณณาตรวจสอบ<br/>"; alert += "เลขบัตรประจำตัวประชาชนไม่ถูกต้องกรุณณาตรวจสอบ<br/>"; alertType = "danger"; icon = "error"; }
                else { Response.Redirect("/Profile/empForm?ic=" + txtNewIDCard.Text.Trim()); }

            }
            else
            {
                alert += "มีข้อมูลในระบบอยู่แล้ว กรุณาตรวจสอบ<br/>"; alertType = "danger"; icon = "error";
                //msgErr.Text = "มีข้อมูลในระบบอยู่แล้ว กรุณาตรวจสอบ";
            }
            dbScript.CloseConnection();
        }

        protected void GridViewEmp_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            ViewState["z_sortexpresion"] = e.SortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, "ASC");
            }
        }
        public string SortExpression
        {
            get
            {
                if (ViewState["z_sortexpresion"] == null)
                    ViewState["z_sortexpresion"] = this.GridViewEmp.DataKeyNames[0].ToString();
                return ViewState["z_sortexpresion"].ToString();
            }
            set
            {
                ViewState["z_sortexpresion"] = value;
            }
        }

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set
            {
                ViewState["sortDirection"] = value;
            }
        }

        private void SortGridView(string sortExpression, string direction)
        {
            MySqlDataAdapter da = dbScript.getDataSelect(Session["sqlEmp"].ToString());
            ds = new DataTable();
            da.Fill(ds);

            DataTable dt = ds;
            DataView dv = new DataView(dt);
            //dv.Sort = "age asc";
            dv.Sort = sortExpression + " " + direction;
            this.GridViewEmp.DataSource = dv;
            GridViewEmp.DataBind();
            dbScript.CloseConnection();
        }

        protected void btnShEmp_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SearchHistory/historyForm");
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
            DivEmp.Visible = false;
        }
    }
}