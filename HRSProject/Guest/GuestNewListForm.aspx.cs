using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using HRSProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRSProject.Guest
{
    public partial class GuestNewListForm : System.Web.UI.Page
    {
        public decimal guest_to_emp = 0;

        DBScript dBScript = new DBScript();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Params["id"]))
            {
                txtGuest_id.Value = Request.Params["id"].ToString();
            }
            if (Session["User"] != null)
            {
                if (int.Parse(Session["UserPrivilegeId"].ToString()) > 1)
                {
                    DivAddGuest.Visible = false;
                    btnAddTitleGuest.Visible = false;
                    txtOfferDate.Enabled = false;
                    txtReferTitle.Enabled = false;
                    txtReferDate.Enabled = false;
                    txtRefer.Enabled = false;
                    txtTo.Enabled = false;
                    txtTitle.Enabled = false;
                }
            }
            else
            {

            }

            if (!this.IsPostBack)
            {
                BindData();
                dBScript.GetDownList(txtProfix, "select * from tbl_profix", "profix_name", "profix_id");
                dBScript.GetDownList(txtPos, "select * from tbl_pos", "pos_name", "pos_id");
                dBScript.GetDownList(txtCpoint, "select * from tbl_cpoint", "cpoint_name", "cpoint_id");
                dBScript.GetDownList(txtAff, "select * from tbl_affiliation", "affi_name", "affi_id");
                txtAff.Items.Insert(0, new ListItem("เลือก", ""));

                if (txtGuest_id.Value.ToString() == "")
                {
                    DivAddGuest.Visible = false;
                    btnReport.Visible = false;
                    btnReportCopy.Visible = false;
                }
                else
                {
                    string sql_select_guest = "SELECT * FROM tbl_guest guest WHERE guest_id = '" + txtGuest_id.Value + "'";
                    MySqlDataReader rs = dBScript.selectSQL(sql_select_guest);
                    if (rs.Read())
                    {
                        txtTitle.Text = rs.GetString("guest_title");
                        txtTo.Text = rs.GetString("guest_title_to");
                        txtRefer.Text = rs.GetString("guest_refer");
                        txtReferDate.Text = rs.GetString("guest_refer_date");
                        txtReferTitle.Text = rs.GetString("guest_refer_title");
                        txtOfferDate.Text = rs.GetString("guest_offer_date");
                    }
                    rs.Close();
                    dBScript.CloseConnection();
                }
            }
        }

        protected void GridViewGuestList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            Label lbGueatName = (Label)(e.Row.FindControl("lbGueatName"));
            if (lbGueatName != null)
            {
                lbGueatName.Text = DataBinder.Eval(e.Row.DataItem, "profix_name").ToString() + DataBinder.Eval(e.Row.DataItem, "guest_list_name").ToString() + " " + DataBinder.Eval(e.Row.DataItem, "guest_list_lname").ToString();
            }

            Label lbGuestPos = (Label)(e.Row.FindControl("lbGuestPos"));
            if (lbGuestPos != null)
            {
                lbGuestPos.Text = DataBinder.Eval(e.Row.DataItem, "pos_name").ToString();
            }

            Label lbGuestCpoint = (Label)(e.Row.FindControl("lbGuestCpoint"));
            if (lbGuestCpoint != null)
            {
                lbGuestCpoint.Text = DataBinder.Eval(e.Row.DataItem, "cpoint_name").ToString();
            }

            HyperLink LabelAddEmp = (HyperLink)(e.Row.FindControl("LabelAddEmp"));
            if (LabelAddEmp != null)
            {
                LabelAddEmp.Text = "รายงานตัวเรียบร้อยแล้ว";
            }

            LinkButton btnAddEmp = (LinkButton)(e.Row.FindControl("btnAddEmp"));
            if (btnAddEmp != null)
            {
                btnAddEmp.CommandName = DataBinder.Eval(e.Row.DataItem, "guest_list_id").ToString();
                if (DataBinder.Eval(e.Row.DataItem, "guest_list_idcard").ToString() != "")
                {
                    btnAddEmp.Visible = false;
                    LabelAddEmp.NavigateUrl = "/Profile/empForm?empID=" + dBScript.getMd5Hash(dBScript.getEmpDataIDCard("emp_id", DataBinder.Eval(e.Row.DataItem, "guest_list_idcard").ToString()));
                }
                else
                {
                    DateTime date;
                    date = dBScript.DateCalculationK(DataBinder.Eval(e.Row.DataItem, "guest_offer_date").ToString(), 5);

                    if (DateTime.Now.Date <= date.Date.AddDays(5))
                    {
                        LabelAddEmp.Visible = false;
                        btnAddEmp.Visible = true;
                    }
                    else
                    {
                        if (int.Parse(Session["UserPrivilegeId"].ToString()) > 1)
                        {
                            LabelAddEmp.Visible = true;
                            LabelAddEmp.Text = "ไม่มารายงานตัว";
                            LabelAddEmp.CssClass = "badge badge-danger";
                            btnAddEmp.Visible = false;
                        }
                        else
                        {
                            //LabelAddEmp.Visible = false;
                            LabelAddEmp.Visible = true;
                            LabelAddEmp.Text = "เกินกำหนด";
                            LabelAddEmp.CssClass = "badge badge-danger";
                        }
                    }

                }
            }
        }

        void BindData()
        {
            string sql = "SELECT * FROM tbl_guest guest LEFT JOIN tbl_guest_list li ON guest.guest_id = li.guest_id LEFT JOIN tbl_profix profix ON li.guest_list_profix = profix.profix_id LEFT JOIN tbl_pos pos ON pos.pos_id = li.guest_list_pos LEFT JOIN tbl_cpoint cp ON cp.cpoint_id = li.guest_list_cpoint WHERE li.guest_id = '" + txtGuest_id.Value + "' ORDER BY STR_TO_DATE( guest.guest_offer_date, '%d-%m-%Y' ) DESC";
            MySqlDataAdapter da = dBScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridViewGuestList.DataSource = ds.Tables[0];
            GridViewGuestList.DataBind();
            LaGridViewData.Text = "พนักงานทั้งหมด " + ds.Tables[0].Rows.Count + " คน";
        }

        protected void btnAddGuest_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtLname.Text != "" && txtSalary.Text != ""&& txtAff.SelectedValue != "")
            {
                string sql = "INSERT INTO tbl_guest_list ( guest_id, guest_list_profix, guest_list_name, guest_list_lname, guest_list_pos, guest_list_salary, guest_list_cpoint, guest_list_status, guest_list_idcard ) VALUES ('" + txtGuest_id.Value.ToString() + "', '" + txtProfix.SelectedValue + "', '" + txtName.Text.Trim() + "', '" + txtLname.Text.Trim() + "', '" + txtPos.Text + "', '" + double.Parse(txtSalary.Text) + "', '" + txtCpoint.Text + "', '0', '')";
                string guest_pk = dBScript.InsertQueryPK(sql);
                if (guest_pk != "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('บันทึกสำเร็จ')", true);
                    BindData();
                    clearDataGreat();
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Error : ล้มเหลว')", true);
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('กรุณากรอกข้อมูลให้ครบถ้วน')", true);
            }
        }

        protected void btnAddTitleGuest_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text != "" && txtRefer.Text != "")
            {
                if (txtGuest_id.Value.ToString() == "")
                {
                    string sql = "INSERT INTO tbl_guest (guest_title,guest_title_to,guest_refer,guest_refer_date,guest_refer_title,guest_offer_date,guest_status) VALUES ('" + txtTitle.Text + "','" + txtTo.Text + "','" + txtRefer.Text + "','" + txtReferDate.Text + "','" + txtReferTitle.Text + "','" + txtOfferDate.Text + "','0')";
                    string guest_pk = dBScript.InsertQueryPK(sql);
                    if (guest_pk != "")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('บันทึกสำเร็จ')", true);
                        txtGuest_id.Value = guest_pk;
                        DivAddGuest.Visible = true;
                        BindData();
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Error : ล้มเหลว')", true);
                    }
                }
                else
                {
                    string sql = "UPDATE tbl_guest SET guest_title = '" + txtTitle.Text.Trim() + "',guest_title_to='" + txtTo.Text.Trim() + "',guest_refer = '" + txtRefer.Text.Trim() + "',guest_refer_date = '" + txtReferDate.Text + "',guest_refer_title='" + txtReferTitle.Text.Trim() + "',guest_offer_date='" + txtOfferDate.Text + "' WHERE guest_id = '" + txtGuest_id.Value + "'";
                    if (dBScript.actionSql(sql))
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('บันทึกสำเร็จ')", true);
                        BindData();
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Error : ล้มเหลว')", true);
                    }
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('กรุณากรอกเรื่อง และกรอกข้อมูลอ้างถึงบันทึกของกองฯ ด้วย')", true);
            }
        }

        void clearDataGreat()
        {
            txtProfix.SelectedIndex = 0;
            txtName.Text = "";
            txtLname.Text = "";
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            ShowRport(false);
        }

        private void ShowRport(bool copy)
        {
            //noteOffer reportNote = new noteOffer();
            ReportDocument reportNote = new ReportDocument();
            reportNote.Load(Server.MapPath("/Guest/noteOffer.rpt"));
            Session["Report"] = "";
            //reportNote.DataSourceConnections.Clear();
            //reportNote.Database.Tables.Reset();
            //CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            //rpt.Load(Server.MapPath("FinalReport.rpt"));
            //reportNote.SetDatabaseLogon("adminhrs", "admin25", "MySql DSN HR", "hrsystem");
            string pos_list = "ตำแหน่งพนักงานจัดเก็บและตำแหน่งพนักงานจัดการจราจร";
            reportNote.SetParameterValue("guest_id", txtGuest_id.Value.ToString());
            reportNote.SetParameterValue("txt_footer1", "เบื้องต้นฝ่ายบริหารการจัดเก็บเงินค่าธรรมเนียม ดำเนินการแนะนำกฎระเบียบที่เกี่ยวข้องกับงานราชการและการปฏิบัติงานใน" + pos_list + " ดังกล่าวข้างต้นและส่งตัวพนักงานไปปฏิบัติหน้าที่ตั้งแต่วันที่ " + dBScript.convertDatelongThai(txtOfferDate.Text.Trim()) + " เป็นต้นไป");
            reportNote.SetParameterValue("txt_footer2", "อนึ่ง ฝ่ายบริหารการจัดเก็บเงินค่าธรรมเนียม ได้แจ้งให้พนักงานทุกคนรับทราบ ในกรณีที่พนักงานฯ ปฏิบัติงานไม่ถึง 10 ผลัด จะไม่ขอรับเงินเดือน เรียบร้อยแล้ว");
            reportNote.SetParameterValue("txt_footer3", "จึงเรียนมาเพื่อโปรดทราบและดำเนินการต่อไป");
            reportNote.SetParameterValue("copy", copy);
            reportNote.SetParameterValue("user", Session["UserName"].ToString().Split(' ')[0]);

            Session["Report"] = reportNote;
            Session["ReportTitle"] = "บันทึกข้อความ";
            Response.Write("<script>");
            Response.Write("window.open('/Report/reportView','_blank')");
            Response.Write("</script>");
            //resultListEmp.LogOnInfo = crTableLogonInfos;
        }



        protected void btnReportCopy_Click(object sender, EventArgs e)
        {
            ShowRport(true);
        }

        protected void GridViewGuestList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            guest_to_emp = int.Parse(e.CommandName.ToString());
            string sql_guest = "SELECT * FROM tbl_guest_list li JOIN tbl_profix px ON px.profix_id = li.guest_list_profix JOIN tbl_pos pos ON pos.pos_id = li.guest_list_pos JOIN tbl_cpoint cp ON cp.cpoint_id = li.guest_list_cpoint WHERE guest_list_id = '" + guest_to_emp + "'";
            MySqlDataReader rs = dBScript.selectSQL(sql_guest);
            if (rs.Read())
            {
                lbName.Text = rs.GetString("profix_name") + rs.GetString("guest_list_name") + " " + rs.GetString("guest_list_lname");
                lbPos.Text = rs.GetString("pos_name");
                lbCpoint.Text = rs.GetString("cpoint_name");
                hdProfix.Value = rs.GetString("profix_id");
                hdName.Value = rs.GetString("guest_list_name");
                hdLname.Value = rs.GetString("guest_list_lname");
                hdPos.Value = rs.GetString("pos_id");
                hdCpoint.Value = rs.GetString("cpoint_id");
                hdGuest_list_id.Value = e.CommandName.ToString();
            }
            rs.Close();
            dBScript.CloseConnection();
        }

        protected void btnCheckEmp_Click(object sender, EventArgs e)
        {
            if (dBScript.checkIDCard(txtNewIDCard.Text.Trim()))
            {
                if (dBScript.checkDupicalIDCard(txtNewIDCard.Text.Trim()))
                {
                    if (txtNationality.Text != "" && txtRace.Text != "" && txtReligion.Text != "" && txtBookBank.Text != "" && txtStatusRd.SelectedValue != "" && txtAff.SelectedValue != "")
                    {
                        string emp_id = dBScript.createEmpId(txtOfferDate);
                        string text = "emp_id, emp_profix_id, emp_name, emp_lname, emp_pos_id, emp_affi_id, emp_cpoint_id, emp_type_emp_id, emp_start_working, emp_birth_date, emp_origin, emp_nationality, emp_religion, emp_id_card, emp_status, emp_book_bank_no,emp_img_profile";
                        string value = "'" + emp_id + "', '" + hdProfix.Value + "', '" + hdName.Value + "', '" + hdLname.Value + "', '" + hdPos.Value + "', '" + txtAff.SelectedValue + "', '" + hdCpoint.Value + "', '2', '" + txtOfferDate.Text + "', '" + txtBirdthDay.Text + "', '" + txtRace.Text + "', '" + txtNationality.Text + "', '" + txtReligion.Text + "', '" + txtNewIDCard.Text + "', '" + txtStatusRd.SelectedValue + "', '" + txtBookBank.Text + "',''";
                        string sql = "INSERT INTO tbl_emp_profile (" + text + ") VALUES (" + value + ")";
                        if (dBScript.actionSql("UPDATE tbl_guest_list SET guest_list_idcard = '" + txtNewIDCard.Text.Trim() + "' WHERE guest_list_id = '" + hdGuest_list_id.Value + "'"))
                        {
                            if (dBScript.actionSql(sql))
                            {
                                string insert_history_text = "history_status_id,history_date,history_note,history_emp_id";
                                string insert_history_value = "'1','00-00-0000','', '" + dBScript.getEmpData("id", emp_id) + "'";
                                string insert_history = "INSERT INTO tbl_history (" + insert_history_text + ") VALUES (" + insert_history_value + ")";
                                dBScript.actionSql(insert_history);
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('บันทึก ประวัติส่วนตัว สำเร็จ')", true);
                                BindData();
                            }
                            else
                            {
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Error : บันทึก ประวัติส่วนตัว ล้มเหลว')", true);
                                BindData();
                            }
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Error : ล้มเหลว')", true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('กรุณาใส่ข้อมูลให้ครบถ้วน')", true);
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('เลขบัตรประจำตัวประชาชน ซ้ำกับพนักงานที่ทำงานอยู่ในปัจจุบัน กรุณณาตรวจสอบ')", true);
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('เลขบัตรประจำตัวประชาชนไม่ถูกต้องกรุณณาตรวจสอบ')", true);
            }
        }
    }
}