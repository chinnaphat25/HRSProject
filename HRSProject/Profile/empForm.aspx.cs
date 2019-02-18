using System;
using HRSProject.Config;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Globalization;

namespace HRSProject.Profile
{
    public partial class empForm : System.Web.UI.Page
    {
        DBScript dBScript = new DBScript();
        public string alert = "";
        public string alertType = "";
        public string icon = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //txtPos.SelectedIndexChanged += new EventHandler(this.txtPos_SelectedIndexChanged);
            if (!string.IsNullOrEmpty(Request.Params["empID"]))
            {
                //empId.Text = Request.Params["empID"].ToString().Trim();
                empId.Text = dBScript.getEmpIDMD5("emp_id", Request.Params["empID"]);
                if (dBScript.CheckPrivilege(Session["UserPrivilegeId"].ToString(), "HR"))
                {
                    GroupProfile(true);
                }
                else
                {
                    GroupProfile(false);
                }
            }
            else
            {

                if (!string.IsNullOrEmpty(Request.Params["ic"]))
                {
                    txtIdcard.Text = Request.Params["ic"].ToString();
                }
                else
                {
                    if (string.IsNullOrEmpty(Request.Params["empID"]))
                    {
                        Response.Redirect("/Profile/empViwe");
                    }
                }
            }

            if (empId.Text.Trim() == "")
            {
                Title = "เพิ่มพนักงาน";
            }
            else
            {
                Title = "ข้อมูลพนักงาน";
            }

            lbTxtPos.Text = dBScript.getEmpData("pos_name", empId.Text);
            lbTxtID.Text = empId.Text;
            lbTxtName.Text = dBScript.getEmpData("profix_name", empId.Text) + dBScript.getEmpData("emp_name", empId.Text) + "  " + dBScript.getEmpData("emp_lname", empId.Text);

            if (!this.IsPostBack)
            {

                string sql_profix = "SELECT * FROM tbl_profix";
                dBScript.GetDownList(txtProfix, sql_profix, "profix_name", "profix_id");

                string sql_pos = "SELECT * FROM tbl_pos";
                dBScript.GetDownList(txtPos, sql_pos, "pos_name", "pos_id");

                string sql_cpoint = "SELECT * FROM tbl_cpoint";
                dBScript.GetDownList(txtCpoint, sql_cpoint, "cpoint_name", "cpoint_id");
                dBScript.GetDownList(txtExpCpoint, sql_cpoint, "cpoint_name", "cpoint_id");

                string sql_province = "SELECT * FROM tbl_province";
                dBScript.GetDownList(txtProvince, sql_province, "province_name", "province_id");

                string sql_Education = "SELECT * FROM tbl_level_edu";
                dBScript.GetDownList(txtEducation, sql_Education, "level_edu_name", "level_edu_id");

                string sql_type_doc = "SELECT * FROM tbl_type_doc";
                dBScript.GetDownList(txtDocType, sql_type_doc, "type_doc_name", "type_doc_id");

                string sql_EmpType = "SELECT * FROM tbl_type_emp";
                dBScript.GetDownList(txtEmpType, sql_EmpType, "type_emp_name", "type_emp_id");

                string sql_TypeAdd = "SELECT * FROM tbl_type_add";
                dBScript.GetDownList(txtTypeAdd, sql_TypeAdd, "type_add_name", "type_add_id");

                string sql_affi = "SELECT * FROM tbl_affiliation ORDER BY affi_name";
                dBScript.GetDownList(txtAffi, sql_affi, "affi_name", "affi_id");
                dBScript.GetDownList(txtExpCpointAff, sql_affi, "affi_name", "affi_id");

                string sql_province_idcard = "SELECT * FROM tbl_province";
                dBScript.GetDownList(txtProvince_idcard, sql_province_idcard, "province_name", "province_id");

                sql_pos = "SELECT * FROM tbl_pos";
                dBScript.GetDownList(txtExpMoterwayPos, sql_pos, "pos_name", "pos_id");

                string sql_status_working = "SELECT * FROM tbl_status_working";
                dBScript.GetDownList(txtHistoryStatus, sql_status_working, "status_working_name", "status_working_id");

                if (empId.Text != "")
                {
                    showData();
                }
            }
            dBScript.CloseConnection();

        }

        protected void reSet_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Profile/EmpForm");
        }
        private string GetSelectedRadio(params RadioButton[] radioButtonGroup)
        {
            for (int i = 0; i < radioButtonGroup.Length; i++)
            {
                if (radioButtonGroup[i].Checked)
                {
                    // return it
                    return radioButtonGroup[i].Text;
                }
            }
            return null;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //GetSelectedRadio(txtStatus1,txtStatus2,txtStatus3,txtStatus4,txtStatus5);
            alert = "";

            bool chk = true;
            if (txtIdcard.Text == "") { alert += "- กรุณาใส่เลขบัตรประชาชน<br/>"; chk = false; alertType = "danger"; icon = "error"; }
            if (txtIdcard.Text.Length < 12) { alert += "- กรุณาใส่เลขบัตรประชาชนให้ครบ 13 หลัก<br/>"; chk = false; alertType = "danger"; icon = "error"; }

            string[] data = txtDateStart.Text.Trim().Split('-');
            DateTime dateStart = DateTime.ParseExact(data[0] + "-" + data[1] + "-" + (int.Parse(data[2]) - 543), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            if (dateStart.Date > DateTime.Now.Date) { alert += "- เลือกวันที่เริ่มงานไม่ถูกต้อง<br/>"; chk = false; alertType = "danger"; icon = "error"; }
            if (!dBScript.checkIDCard(txtIdcard.Text.Trim())) { alert += "- เลขบัตรประจำตัวประชาชนไม่ถูกต้องกรุณณาตรวจสอบ<br/>"; chk = false; alertType = "danger"; icon = "error"; }

            if (chk)
            {
                if (empId.Text == "")
                {
                    string sql_chk_idcard = "SELECT * FROM tbl_emp_profile WHERE emp_id_card = '" + txtIdcard.Text + "'";
                    MySqlDataReader rs_chk_idcard = dBScript.selectSQL(sql_chk_idcard);
                    if (!rs_chk_idcard.Read())
                    {
                        rs_chk_idcard.Close();

                        ///////////////////////////////// INSERT
                        String NewFileName = "";
                        string[] cdate = txtDateStart.Text.Split('-');
                        if (cdate[0].Length < 2) { cdate[0] = "0" + cdate[0]; }
                        if (cdate[1].Length < 2) { cdate[1] = "0" + cdate[1]; }
                        txtDateStart.Text = cdate[0] + "-" + cdate[1] + "-" + cdate[2];

                        empId.Text = dBScript.createEmpId(txtDateStart);
                        if (imgUpload.HasFile)
                        {
                            NewFileName = empId.Text + "_profile";
                            NewFileName = "/Upload/images/empProfile/" + dBScript.getMd5Hash(NewFileName) + "." + imgUpload.FileName.Split('.')[imgUpload.FileName.Split('.').Length - 1];
                            imgUpload.SaveAs(Server.MapPath(NewFileName.ToString()));
                        }

                        string sql_text = "emp_id,emp_profix_id,emp_name,emp_lname,emp_pos_id,emp_affi_id,emp_cpoint_id,emp_type_emp_id,emp_start_working,emp_birth_date,emp_weight,emp_height,emp_origin,emp_nationality,emp_religion,emp_id_card,emp_brethren_num,emp_brethren,emp_status,emp_add_idcard_num,emp_add_idcard_moo,emp_add_idcard_soi,emp_add_idcard_rd,emp_add_idcard_sub_dis,emp_add_idcard_dis,emp_add_idcard_province_id,emp_add_idcard_zipcode,emp_add_type,emp_add_num,emp_add_moo,emp_add_soi,emp_add_rd,emp_add_sub_dis,emp_add_dis,emp_add_province_id,emp_add_zipcode,emp_home_tel,emp_mobile_tel,emp_sso_blood_group,emp_sso_hospital,emp_book_bank_no,emp_img_profile,emp_staus_working,emp_pos_num";
                        string sql_value = "'" + empId.Text + "','" + txtProfix.SelectedValue + "','" + txtName.Text + "','" + txtLname.Text + "','" + txtPos.SelectedValue + "','" + txtAffi.SelectedValue + "','" + txtCpoint.SelectedValue + "','" + txtEmpType.SelectedValue + "','" + txtDateStart.Text + "','" + txtBrithDate.Text + "','" + txtWeight.Text + "','" + txtHeight.Text + "','" + txtOrigin.Text + "','" + txtNationality.Text + "','" + txtReligion.Text + "','" + txtIdcard.Text + "','" + txtBrethren_num.Text + "','" + txtBrethren.Text + "','" + txtStatusRd.SelectedValue + "','" + txtIdcardAddNum.Text + "','" + txtIdcardMoo.Text + "','" + txtIdcardSoi.Text + "','" + txtIdcardRd.Text + "','" + txtIdcardSubDis.Text + "','" + txtIdcardDis.Text + "','" + txtProvince_idcard.SelectedValue + "','" + txtIdcardZipCode.Text + "','" + txtTypeAdd.SelectedValue + "','" + txtAddNum.Text + "','" + txtAddMoo.Text + "','" + txtAddSoi.Text + "','" + txtAddRd.Text + "','" + txtAddSubDis.Text + "','" + txtAddDis.Text + "','" + txtProvince.SelectedValue + "','" + txtAddZipcode.Text + "','" + txtPhone.Text + "','" + txtMobile.Text + "','" + txtBlood.SelectedItem + "','" + txtHospital.Text + "','" + txtBookBank.Text + "','" + NewFileName.ToString() + "','1','"+ txtPosNum.Text.Trim() + "'";
                        string sql = "INSERT INTO tbl_emp_profile (" + sql_text + ") VALUES (" + sql_value + ")";

                        if (dBScript.actionSql(sql))
                        {
                            sql = "SELECT * FROM tbl_status_working WHERE status_working_name Like '%ทำงาน%'";
                            MySqlDataReader rs = dBScript.selectSQL(sql);
                            if (rs.Read())
                            {
                                string insert_history_text = "history_status_id,history_date,history_note,history_emp_id";
                                string insert_history_value = "'" + rs.GetString("status_working_id") + "','00-00-0000','', '" + dBScript.getEmpData("id", empId.Text) + "'";
                                string insert_history = "INSERT INTO tbl_history (" + insert_history_text + ") VALUES (" + insert_history_value + ")";
                                dBScript.actionSql(insert_history);
                            }
                            rs.Close();
                            alert += "บันทึก ประวัติส่วนตัว สำเร็จ <br/>"; alertType = "success"; icon = "add_alert";
                            showData();
                        }
                        else
                        {
                            alert += "Error : บันทึก ประวัติส่วนตัว ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                        }
                        ///////////////////////////////// END INSERT
                    }
                    else
                    {
                        alert += "Error : บันทึกล้มเหลว<br/>- มีข้อมูลพนักงานในระบบอยู่แล้ว"; alertType = "danger"; icon = "error";
                    }
                }
                else
                {
                    ///////////////////////////////// Start Update
                    string NewFileName = "";
                    if (imgUpload.HasFile)
                    {
                        NewFileName = empId.Text + "_profile";
                        NewFileName = "/Upload/images/empProfile/" + dBScript.getMd5Hash(NewFileName) + "." + imgUpload.FileName.Split('.')[imgUpload.FileName.Split('.').Length - 1];
                        imgUpload.SaveAs(Server.MapPath(NewFileName.ToString()));
                    }

                    string sql_value = "";
                    sql_value += "emp_profix_id='" + txtProfix.SelectedItem.Value + "',";
                    sql_value += "emp_name='" + txtName.Text + "',";
                    sql_value += "emp_lname='" + txtLname.Text + "',";
                    sql_value += "emp_pos_id='" + txtPos.SelectedValue + "',";
                    sql_value += "emp_pos_num='" + txtPosNum.Text.Trim() + "',";
                    sql_value += "emp_affi_id='" + txtAffi.SelectedValue + "',";
                    sql_value += "emp_cpoint_id='" + txtCpoint.SelectedValue + "',";
                    sql_value += "emp_type_emp_id='" + txtEmpType.SelectedValue + "',";
                    sql_value += "emp_start_working='" + txtDateStart.Text + "',";
                    sql_value += "emp_birth_date='" + txtBrithDate.Text + "',";
                    sql_value += "emp_weight='" + txtWeight.Text + "',";
                    sql_value += "emp_height='" + txtHeight.Text + "',";
                    sql_value += "emp_origin='" + txtOrigin.Text + "',";
                    sql_value += "emp_nationality='" + txtNationality.Text + "',";
                    sql_value += "emp_religion='" + txtReligion.Text + "',";
                    sql_value += "emp_id_card='" + txtIdcard.Text + "',";
                    sql_value += "emp_brethren_num='" + txtBrethren_num.Text + "',";
                    sql_value += "emp_brethren='" + txtBrethren.Text + "',";
                    sql_value += "emp_status='" + txtStatusRd.SelectedValue + "',";
                    sql_value += "emp_add_idcard_num='" + txtIdcardAddNum.Text + "',";
                    sql_value += "emp_add_idcard_moo='" + txtIdcardMoo.Text + "',";
                    sql_value += "emp_add_idcard_soi='" + txtIdcardSoi.Text + "',";
                    sql_value += "emp_add_idcard_rd='" + txtIdcardRd.Text + "',";
                    sql_value += "emp_add_idcard_sub_dis='" + txtIdcardSubDis.Text + "',";
                    sql_value += "emp_add_idcard_dis='" + txtIdcardDis.Text + "',";
                    sql_value += "emp_add_idcard_province_id='" + txtProvince_idcard.SelectedValue + "',";
                    sql_value += "emp_add_idcard_zipcode='" + txtIdcardZipCode.Text + "',";
                    sql_value += "emp_add_type='" + txtTypeAdd.SelectedValue + "',";
                    sql_value += "emp_add_num='" + txtAddNum.Text + "',";
                    sql_value += "emp_add_moo='" + txtAddMoo.Text + "',";
                    sql_value += "emp_add_soi='" + txtAddSoi.Text + "',";
                    sql_value += "emp_add_rd='" + txtAddRd.Text + "',";
                    sql_value += "emp_add_sub_dis='" + txtAddSubDis.Text + "',";
                    sql_value += "emp_add_dis='" + txtAddDis.Text + "',";
                    sql_value += "emp_add_province_id='" + txtProvince.SelectedValue + "',";
                    sql_value += "emp_add_zipcode='" + txtAddZipcode.Text + "',";
                    sql_value += "emp_home_tel='" + txtPhone.Text + "',";
                    sql_value += "emp_mobile_tel='" + txtMobile.Text + "',";
                    sql_value += "emp_sso_blood_group='" + txtBlood.SelectedItem + "',";
                    sql_value += "emp_sso_hospital='" + txtHospital.Text + "',";
                    sql_value += "emp_book_bank_no='" + txtBookBank.Text + "',";
                    if (imgUpload.HasFile) { sql_value += "emp_img_profile='" + NewFileName + "',"; }
                    sql_value += "emp_staus_working='1'";
                    string sql = "UPDATE tbl_emp_profile SET " + sql_value + " WHERE emp_id='" + empId.Text + "'";
                    if (dBScript.actionSql(sql))
                    {
                        alert = "บันทึก ประวัติส่วนตัว สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                        showData();
                    }
                    else
                    {
                        alert = "Error : บันทึก ประวัติส่วนตัว ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                    }
                }
                TabClick.Value = "home";

            }
            dBScript.CloseConnection();
        }


        public void showData()
        {
            string sql = "SELECT * FROM tbl_emp_profile WHERE emp_id = '" + empId.Text + "'";
            MySqlDataReader result = dBScript.selectSQL(sql);
            if (result.Read())
            {
                empId.Text = result.GetString("emp_id");
                txtProfix.SelectedValue = result.GetString("emp_profix_id");
                txtName.Text = result.GetString("emp_name");
                txtLname.Text = result.GetString("emp_lname");
                txtPos.SelectedValue = result.GetString("emp_pos_id");
                if (!result.IsDBNull(6)) { txtPosNum.Text = result.GetString("emp_pos_num"); }
                txtAffi.SelectedValue = result.GetString("emp_affi_id");
                txtCpoint.SelectedValue = result.GetString("emp_cpoint_id");
                txtEmpType.SelectedValue = result.GetString("emp_type_emp_id");
                txtDateStart.Text = result.GetString("emp_start_working");
                txtBrithDate.Text = result.GetString("emp_birth_date");
                txtWeight.Text = result.GetString("emp_weight");
                txtHeight.Text = result.GetString("emp_height");
                txtOrigin.Text = result.GetString("emp_origin");
                txtNationality.Text = result.GetString("emp_nationality");
                txtReligion.Text = result.GetString("emp_religion");
                txtIdcard.Text = result.GetString("emp_id_card");
                txtBrethren_num.Text = result.GetString("emp_brethren_num");
                txtBrethren.Text = result.GetString("emp_brethren");
                txtStatusRd.SelectedValue = result.GetString("emp_status");
                txtIdcardAddNum.Text = result.GetString("emp_add_idcard_num");
                txtIdcardMoo.Text = result.GetString("emp_add_idcard_moo");
                txtIdcardSoi.Text = result.GetString("emp_add_idcard_soi");
                txtIdcardRd.Text = result.GetString("emp_add_idcard_rd");
                txtIdcardSubDis.Text = result.GetString("emp_add_idcard_sub_dis");
                txtIdcardDis.Text = result.GetString("emp_add_idcard_dis");
                txtProvince_idcard.SelectedValue = result.GetString("emp_add_idcard_province_id");
                txtIdcardZipCode.Text = result.GetString("emp_add_idcard_zipcode");
                txtTypeAdd.SelectedValue = result.GetString("emp_add_type");
                txtAddNum.Text = result.GetString("emp_add_num");
                txtAddMoo.Text = result.GetString("emp_add_moo");
                txtAddSoi.Text = result.GetString("emp_add_soi");
                txtAddRd.Text = result.GetString("emp_add_rd");
                txtAddSubDis.Text = result.GetString("emp_add_sub_dis");
                txtAddDis.Text = result.GetString("emp_add_dis");
                txtProvince.SelectedValue = result.GetString("emp_add_province_id");
                txtAddZipcode.Text = result.GetString("emp_add_zipcode");
                txtPhone.Text = result.GetString("emp_home_tel");
                txtMobile.Text = result.GetString("emp_mobile_tel");
                txtBlood.SelectedValue = result.GetString("emp_sso_blood_group");
                txtHospital.Text = result.GetString("emp_sso_hospital");
                txtBookBank.Text = result.GetString("emp_book_bank_no");
            }
            result.Close();

            string sql_mate = "SELECT * FROM tbl_mate WHERE mate_emp_id = '" + empId.Text + "'";
            result = dBScript.selectSQL(sql_mate);
            if (result.Read())
            {
                txtMateName.Text = result.GetString("mate_name");
                txtMateMetier.Text = result.GetString("mate_metier");
                txtMateTel.Text = result.GetString("mate_tel");
                txtMateSon_num.Text = result.GetString("mate_son_num");
                txtMateDaughter_num.Text = result.GetString("mate_daughter_num");
                txtMateWorkAdd.Text = result.GetString("mate_work_add");
            }
            result.Close();

            string sql_family = "SELECT * FROM tbl_family WHERE family_emp_id = '" + empId.Text + "' AND family_relationship ='1'";
            result = dBScript.selectSQL(sql_family);
            if (result.Read())
            {
                txtFaterName.Text = result.GetString("family_name");
                txtFaterMetier.Text = result.GetString("family_vocation");
                txtFaterTel.Text = result.GetString("family_tel");
                txtFaterAdd.Text = result.GetString("family_add");
            }
            result.Close();

            sql_family = "SELECT * FROM tbl_family WHERE family_emp_id = '" + empId.Text + "' AND family_relationship ='2'";
            result = dBScript.selectSQL(sql_family);
            if (result.Read())
            {
                txtMomName.Text = result.GetString("family_name");
                txtMomMetier.Text = result.GetString("family_vocation");
                txtMomTel.Text = result.GetString("family_tel");
                txtMomAdd.Text = result.GetString("family_add");
            }
            result.Close();

            getSeclctEmpEdu();
            getSeclctEmpExp();
            getSeclctEmpExpMoterway();

            string sqlSelectGuarantor = "SELECT * FROM tbl_guarantor WHERE guarantor_emp_id = '" + empId.Text + "' ORDER BY guarantor_id DESC";
            result = dBScript.selectSQL(sqlSelectGuarantor);
            if (result.Read())
            {
                txtGuarantorName.Text = result.GetString("guarantor_name");
                txtGuarantorPos.Text = result.GetString("guarantor_pos");
                txtGuarantorAff.Text = result.GetString("guarantor_extraction");
                txtGuarantorAdd.Text = result.GetString("guarantor_add");
                txtGuarantorTel.Text = result.GetString("guarantor_tel");
            }
            result.Close();

            string sqlSelectEcon = "SELECT * FROM tbl_emergency_contact WHERE emergency_contact_emp_id = '" + empId.Text + "' ORDER BY emergency_contact_id DESC";
            result = dBScript.selectSQL(sqlSelectEcon);
            if (result.Read())
            {
                txtEConName.Text = result.GetString("emergency_contact_name");
                txtEConRelationShip.Text = result.GetString("emergency_contact_relationship");
                txtEConAdd.Text = result.GetString("emergency_contact_add");
                txtEConTel.Text = result.GetString("emergency_contact_tel");
            }
            result.Close();

            getSeclctDoc();
            getSeclctExpCpoint();
            getSeclctHistory();
            dBScript.CloseConnection();
        }

        public void getSeclctEmpEdu()
        {
            string sql = "SELECT * FROM tbl_profile_education JOIN tbl_level_edu ON level_edu_id=pro_edu_level_id WHERE pro_edu_emp_id='" + empId.Text + "' ORDER BY pro_edu_level_id";
            MySqlDataAdapter da = dBScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            EduGridView.DataSource = ds.Tables[0];
            EduGridView.DataBind();
            if (ds.Tables[0].Rows.Count == 0)
            {
                lbNullEdu.Text = "ไม่พบข้อมูลการศึกษา";
            }
            else
            {
                lbNullEdu.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
            }
            dBScript.CloseConnection();
        }

        public void getSeclctEmpExp()
        {
            string sql = "SELECT * FROM tbl_exp WHERE exp_emp_id='" + empId.Text + "' ORDER BY DATE_ADD(STR_TO_DATE(exp_start, '%d-%m-%Y'),INTERVAL -543 YEAR) DESC";
            MySqlDataAdapter da = dBScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ExpGridView.DataSource = ds.Tables[0];
            ExpGridView.DataBind();
            if (ds.Tables[0].Rows.Count == 0)
            {
                lbExpNull.Text = "ไม่พบข้อมูลประวัติการทำงาน";
            }
            else
            {
                lbExpNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
            }
            dBScript.CloseConnection();
        }

        public void getSeclctEmpExpMoterway()
        {
            string sql = "SELECT * FROM tbl_exp_moterway JOIN tbl_pos ON pos_id=exp_moterway_pos WHERE exp_moterway_emp_id='" + empId.Text + "' ORDER BY DATE_ADD(STR_TO_DATE(exp_moterway_start, '%d-%m-%Y'),INTERVAL -543 YEAR) DESC";
            MySqlDataAdapter da = dBScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ExpMoterwayGridView.DataSource = ds.Tables[0];
            ExpMoterwayGridView.DataBind();
            if (ds.Tables[0].Rows.Count == 0)
            {
                lbExpMoterwayNull.Text = "ไม่พบข้อมูลประวัติการทำงาน (ภายในฝ่ายจัดเก็บฯ)";
            }
            else
            {
                lbExpMoterwayNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
            }
            dBScript.CloseConnection();
        }

        public void getSeclctDoc()
        {
            string sql = "SELECT * FROM tbl_doc JOIN tbl_type_doc ON doc_type_doc_id = type_doc_id WHERE doc_emp_id = '" + empId.Text + "' ORDER BY type_doc_id";
            MySqlDataAdapter da = dBScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DocGridView.DataSource = ds.Tables[0];
            DocGridView.DataBind();
            if (ds.Tables[0].Rows.Count == 0)
            {
                lbDocNull.Text = "ไม่พบข้อมูลเอกสาร";
            }
            else
            {
                lbDocNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
            }
            dBScript.CloseConnection();
        }

        public void getSeclctExpCpoint()
        {
            string sql = "SELECT * FROM tbl_work_history JOIN tbl_cpoint ON cpoint_id=work_history_cpoint JOIN tbl_pos ON pos_id = work_history_pos JOIN tbl_affiliation ON affi_id=work_history_aff WHERE work_history_emp_id = '" + empId.Text + "' ORDER BY DATE_ADD(STR_TO_DATE(work_history_in, '%d-%m-%Y'),INTERVAL -543 YEAR) DESC ";
            MySqlDataAdapter da = dBScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ExpCpointGridView.DataSource = ds.Tables[0];
            ExpCpointGridView.DataBind();
            if (ds.Tables[0].Rows.Count == 0)
            {
                lbExpCpointNull.Text = "ไม่พบข้อมูลประวัติการทำงาน/ด่านฯ";
            }
            else
            {
                lbExpCpointNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
            }
            dBScript.CloseConnection();
        }

        public void getSeclctHistory()
        {
            string sql = "SELECT * FROM tbl_history JOIN tbl_status_working ON status_working_id = history_status_id WHERE history_emp_id = '" + dBScript.getEmpData("id", empId.Text) + "' ORDER BY history_id DESC ";
            MySqlDataAdapter da = dBScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            InOutHistoryGridView.DataSource = ds.Tables[0];
            InOutHistoryGridView.DataBind();
            if (ds.Tables[0].Rows.Count == 0)
            {
                lbHistoryNull.Text = "ไม่พบข้อมูลประวัติการเข้า-ออก/สถานะภาพ";
            }
            else
            {
                lbHistoryNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
            }
            dBScript.CloseConnection();
        }
        protected void btnFamily_Click(object sender, EventArgs e)
        {
            alert = "";
            if (empId.Text != "")
            {
                /////////// Mate
                if (txtMateName.Text.Trim() != "")
                {
                    string sql_chk_mate = "SELECT * FROM tbl_mate WHERE mate_emp_id = '" + empId.Text + "'";
                    MySqlDataReader rs = dBScript.selectSQL(sql_chk_mate);
                    if (!rs.Read())
                    {
                        string mate_insert_text = "mate_name,mate_metier,mate_work_add,mate_tel,mate_son_num,mate_daughter_num,mate_emp_id";
                        string mate_insert_value = "'" + txtMateName.Text + "','" + txtMateMetier.Text + "','" + txtMateWorkAdd.Text + "','" + txtMateTel.Text + "','" + txtMateSon_num.Text + "','" + txtMateDaughter_num.Text + "','" + empId.Text + "'";
                        string mate_insert = "INSERT INTO tbl_mate (" + mate_insert_text + ") VALUES (" + mate_insert_value + ")";
                        if (dBScript.actionSql(mate_insert))
                        {
                            alert += "บันทึกข้อมูล คู่สมรส สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                        }
                        else
                        {
                            alert += "Error : บันทึกข้อมูล คู่สมรส ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                        }
                    }
                    else
                    {
                        string mate_updat_value = "mate_name='" + txtMateName.Text + "',";
                        mate_updat_value += "mate_metier='" + txtMateMetier.Text + "',";
                        mate_updat_value += "mate_work_add='" + txtMateWorkAdd.Text + "',";
                        mate_updat_value += "mate_tel='" + txtMateTel.Text + "',";
                        mate_updat_value += "mate_son_num='" + txtMateSon_num.Text + "',";
                        mate_updat_value += "mate_daughter_num='" + txtMateDaughter_num.Text + "'";
                        if (dBScript.actionSql("UPDATE tbl_mate SET " + mate_updat_value + " WHERE mate_emp_id = '" + empId.Text + "'"))
                        {
                            alert += "บันทึกข้อมูล คู่สมรส สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                        }
                        else
                        {
                            alert += "Error : บันทึกข้อมูล คู่สมรส ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                        }
                    }
                    rs.Close();
                }
                else
                {
                    alert += "Error : บันทึกข้อมูลล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                }

                //////family fater
                if (txtFaterName.Text != "")
                {
                    string sql_chk_family = "SELECT * FROM tbl_family WHERE family_emp_id = '" + empId.Text + "' AND family_relationship ='1'";
                    MySqlDataReader rs = dBScript.selectSQL(sql_chk_family);
                    if (!rs.Read())
                    {
                        string family_insert_text = "family_name,family_relationship,family_add,family_vocation,family_tel,family_emp_id";
                        string family_insert_value = "'" + txtFaterName.Text + "','1','" + txtFaterAdd.Text + "','" + txtFaterMetier.Text + "','" + txtFaterTel.Text + "','" + empId.Text + "'";
                        string family_insert = "INSERT INTO tbl_family (" + family_insert_text + ") VALUES (" + family_insert_value + ")";
                        if (dBScript.actionSql(family_insert))
                        {
                            alert += "บันทึกข้อมูล บิดา สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                        }
                        else
                        {
                            alert += "Error : บันทึกข้อมูล บิดา ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                        }
                    }
                    else
                    {
                        string family_updat_value = "family_name='" + txtFaterName.Text + "',";
                        family_updat_value += "family_add='" + txtFaterAdd.Text + "',";
                        family_updat_value += "family_vocation='" + txtFaterMetier.Text + "',";
                        family_updat_value += "family_tel='" + txtFaterTel.Text + "'";
                        if (dBScript.actionSql("UPDATE tbl_family SET " + family_updat_value + " WHERE family_emp_id = '" + empId.Text + "' AND family_relationship ='1'"))
                        {
                            alert += "บันทึกข้อมูล บิดา สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                        }
                        else
                        {
                            alert += "Error : บันทึกข้อมูล บิดา ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                        }
                    }
                    rs.Close();
                }
                //////family mom
                if (txtMomName.Text.Trim() != "")
                {
                    string sql_chk_family = "SELECT * FROM tbl_family WHERE family_emp_id = '" + empId.Text + "' AND family_relationship ='2'";
                    MySqlDataReader rs = dBScript.selectSQL(sql_chk_family);
                    if (!rs.Read())
                    {
                        string family_insert_text = "family_name,family_relationship,family_add,family_vocation,family_tel,family_emp_id";
                        string family_insert_value = "'" + txtMomName.Text + "','2','" + txtMomAdd.Text + "','" + txtMomMetier.Text + "','" + txtMomTel.Text + "','" + empId.Text + "'";
                        string family_insert = "INSERT INTO tbl_family (" + family_insert_text + ") VALUES (" + family_insert_value + ")";
                        if (dBScript.actionSql(family_insert))
                        {
                            alert += "บันทึกข้อมูล มารดา สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                        }
                        else
                        {
                            alert += "Error : บันทึกข้อมูล มารดา ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                        }
                    }
                    else
                    {
                        string family_updat_value = "family_name='" + txtMomName.Text + "',";
                        family_updat_value += "family_add='" + txtMomAdd.Text + "',";
                        family_updat_value += "family_vocation='" + txtMomMetier.Text + "',";
                        family_updat_value += "family_tel='" + txtMomTel.Text + "'";
                        if (dBScript.actionSql("UPDATE tbl_family SET " + family_updat_value + " WHERE family_emp_id = '" + empId.Text + "' AND family_relationship ='2'"))
                        {
                            alert += "บันทึกข้อมูล มารดา สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                        }
                        else
                        {
                            alert += "Error : บันทึกข้อมูล มารดา ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                        }
                    }
                    rs.Close();
                }
            }
            showData();
            TabClick.Value = "menu1";
            dBScript.CloseConnection();
        }

        protected void btnEducation_Click(object sender, EventArgs e)
        {
            if (empId.Text != "")
            {
                alert = "";

                string insert_edu_text = "pro_edu_level_id, pro_edu_name, pro_edu_gpa, pro_edu_branch, pro_edu_graduated_year, pro_edu_emp_id";
                string insert_edu_value = "'" + txtEducation.SelectedValue + "', '" + txtEduName.Text + "', '" + txtEduGPA.Text + "', '" + txtEduBranch.Text + "', '" + txtEduYear.Text + "', '" + empId.Text + "'";
                string insert_edu = "INSERT INTO tbl_profile_education (" + insert_edu_text + ") VALUES (" + insert_edu_value + ")";
                if (dBScript.actionSql(insert_edu))
                {
                    alert = "บันทึกข้อมูล ประวัติการศึกษา สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                    showData();
                    txtEducation.SelectedIndex = 0;
                    txtEduName.Text = "";
                    txtEduGPA.Text = "";
                    txtEduBranch.Text = "";
                    txtEduYear.Text = "";
                }

                else
                {
                    alert = "Error : บันทึกข้อมูล ประวัติการศึกษา ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                }
            }
            TabClick.Value = "menu2";
            dBScript.CloseConnection();
        }

        protected void EduGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //ระดับการศึกษา
            Label lbEducation = (Label)(e.Row.FindControl("lbEducation"));
            if (lbEducation != null)
            {
                lbEducation.Text = (string)DataBinder.Eval(e.Row.DataItem, "level_edu_name");
            }

            //ชื่อสถานศึกษา
            Label lbEduName = (Label)(e.Row.FindControl("lbEduName"));
            if (lbEduName != null)
            {
                lbEduName.Text = (string)DataBinder.Eval(e.Row.DataItem, "pro_edu_name");
            }

            //สาขาวิชา
            Label lbEduBranch = (Label)(e.Row.FindControl("lbEduBranch"));
            if (lbEduBranch != null)
            {
                lbEduBranch.Text = (string)DataBinder.Eval(e.Row.DataItem, "pro_edu_branch");
            }

            //เกรดเฉลี่ย
            Label lbEduGPA = (Label)(e.Row.FindControl("lbEduGPA"));
            if (lbEduGPA != null)
            {
                lbEduGPA.Text = ((double)DataBinder.Eval(e.Row.DataItem, "pro_edu_gpa")).ToString("#.00");
            }

            //ปีที่สำเร็จการศึกษา
            Label lbEduYear = (Label)(e.Row.FindControl("lbEduYear"));
            if (lbEduYear != null)
            {
                lbEduYear.Text = (string)DataBinder.Eval(e.Row.DataItem, "pro_edu_graduated_year");
            }


            //ปุ่มลบ
            Button btnEduDelete = (Button)(e.Row.FindControl("btnEduDelete"));
            if (btnEduDelete != null)
            {
                btnEduDelete.CommandArgument = ((int)DataBinder.Eval(e.Row.DataItem, "pro_edu_id")).ToString();
            }

        }

        protected void btnEduDelete_Command(object sender, CommandEventArgs e)
        {
            alert = "";
            string sql = "DELETE FROM tbl_profile_education WHERE pro_edu_id = '" + e.CommandArgument.ToString() + "'";
            if (dBScript.actionSql(sql))
            {
                alert = "ลบ ประวัติการศึกษา สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
            }
            else
            {
                alert = "Error : ลบ ประวัติการศึกษา ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
            }
            TabClick.Value = "menu2";
            dBScript.CloseConnection();
        }

        protected void btnExpAdd_Click(object sender, EventArgs e)
        {
            if (empId.Text != "")
            {
                alert = "";

                string insert_exp_text = "exp_start,exp_end,exp_pos,exp_job_detail,exp_saraly,exp_emp_id";
                string insert_exp_value = "'" + txtDateExpStart.Text + "','" + txtDateExpEnd.Text + "','" + txtExpPos.Text + "','" + txtExpDetail.Text + "','" + txtExpSaraly.Text + "', '" + empId.Text + "'";
                string insert_exp = "INSERT INTO tbl_exp (" + insert_exp_text + ") VALUES (" + insert_exp_value + ")";
                if (dBScript.actionSql(insert_exp))
                {
                    alert = "บันทึกข้อมูล ประวัติการทำงาน สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                    txtDateExpStart.Text = "";
                    txtDateExpEnd.Text = "";
                    txtExpPos.Text = "";
                    txtExpDetail.Text = "";
                    txtExpSaraly.Text = "";
                    showData();
                }
                else
                {
                    alert = "Error : บันทึกข้อมูล ประวัติการทำงาน ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                }
            }
            TabClick.Value = "menu3";
            dBScript.CloseConnection();
        }

        protected void ExpGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //ตั้งแต่-ถึง
            Label lbExpDate = (Label)(e.Row.FindControl("lbExpDate"));
            if (lbExpDate != null)
            {
                lbExpDate.Text = dBScript.convertDateShortThai((string)DataBinder.Eval(e.Row.DataItem, "exp_start")) + " - " + dBScript.convertDateShortThai((string)DataBinder.Eval(e.Row.DataItem, "exp_end"));
            }

            //ตำแหน่ง
            Label lbExpPos = (Label)(e.Row.FindControl("lbExpPos"));
            if (lbExpPos != null)
            {
                lbExpPos.Text = (string)DataBinder.Eval(e.Row.DataItem, "exp_pos");
            }

            //ลักษณะงาน
            Label lbExpDetail = (Label)(e.Row.FindControl("lbExpDetail"));
            if (lbExpDetail != null)
            {
                lbExpDetail.Text = (string)DataBinder.Eval(e.Row.DataItem, "exp_job_detail");
            }

            //เงินเดือน
            Label lbExpSaraly = (Label)(e.Row.FindControl("lbExpSaraly"));
            if (lbExpSaraly != null)
            {
                lbExpSaraly.Text = ((double)DataBinder.Eval(e.Row.DataItem, "exp_saraly")).ToString("#,##0.00") + " บาท";
            }

            //ปุ่มลบ
            Button btnExpDelete = (Button)(e.Row.FindControl("btnExpDelete"));
            if (btnExpDelete != null)
            {
                btnExpDelete.CommandArgument = ((int)DataBinder.Eval(e.Row.DataItem, "exp_id")).ToString();
            }
        }

        protected void btnExpDelete_Command(object sender, CommandEventArgs e)
        {
            alert = "";
            string sql = "DELETE FROM tbl_exp WHERE exp_id = '" + e.CommandArgument.ToString() + "'";
            if (dBScript.actionSql(sql))
            {
                alert = "ลบข้อมูล ประวัติการการทำงาน สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                showData();
            }
            else
            {
                alert = "Error : ลบข้อมูล ประวัติการการทำงาน ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
            }
            TabClick.Value = "menu3";
            dBScript.CloseConnection();
        }

        protected void ExpMoterwayGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //ตั้งแต่-ถึง
            Label lbExpMoterwayDate = (Label)(e.Row.FindControl("lbExpMoterwayDate"));
            if (lbExpMoterwayDate != null)
            {
                lbExpMoterwayDate.Text = dBScript.convertDateShortThai((string)DataBinder.Eval(e.Row.DataItem, "exp_moterway_start")) + " - " + dBScript.convertDateShortThai((string)DataBinder.Eval(e.Row.DataItem, "exp_moterway_end"));
            }

            //ตำแหน่ง
            Label lbExpMoterwayPos = (Label)(e.Row.FindControl("lbExpMoterwayPos"));
            if (lbExpMoterwayPos != null)
            {
                lbExpMoterwayPos.Text = (string)DataBinder.Eval(e.Row.DataItem, "pos_name");
            }

            //ลักษณะงาน
            Label lbExpMoterwayAff = (Label)(e.Row.FindControl("lbExpMoterwayAff"));
            if (lbExpMoterwayAff != null)
            {
                lbExpMoterwayAff.Text = (string)DataBinder.Eval(e.Row.DataItem, "affi_name");
            }

            //เงินเดือน
            Label lbExpMoterwaySaraly = (Label)(e.Row.FindControl("lbExpMoterwaySaraly"));
            if (lbExpMoterwaySaraly != null)
            {
                lbExpMoterwaySaraly.Text = ((double)DataBinder.Eval(e.Row.DataItem, "exp_moterway_saraly")).ToString("#,##0.00") + " บาท";
            }

            //Confirm ปุ่ม ลบ
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    ((LinkButton)e.Row.Cells[5].Controls[0]).OnClientClick = "return confirmDelete(this);";
                }
                catch { dBScript.CloseConnection(); }
            }
            dBScript.CloseConnection();
        }

        protected void btnExpMoterwayAdd_Click(object sender, EventArgs e)
        {
            if (empId.Text != "")
            {
                alert = "";
                bool chkExpMoterwayEnd = true;


                string sql = "SELECT * FROM tbl_exp_moterway WHERE exp_moterway_emp_id = '" + empId.Text + "' AND exp_moterway_end = '00-00-0000'";
                MySqlDataReader rs = dBScript.selectSQL(sql);
                if (rs.Read())
                {
                    if (txtExpMoterwayEnd.Text.Trim() == "00-00-0000")
                    {
                        chkExpMoterwayEnd = false;
                        alert = "Error : บันทึกข้อมูล ประวัติการทำงาน(ภายในฝ่ายจัดเก็บฯ) ล้มเหลว<br/> - มีประวัติการทำงานถึงปัจจุบันก่อนหน้านี้แล้ว<br/>"; alertType = "danger"; icon = "error";
                    }
                    rs.Close();
                }

                if (chkExpMoterwayEnd)
                {
                    string insert_exp_text = "exp_moterway_start,exp_moterway_end,exp_moterway_pos,exp_moterway_emp_id";
                    string insert_exp_value = "'" + txtExpMoterwayStart.Text + "','" + txtExpMoterwayEnd.Text + "','" + txtExpMoterwayPos.SelectedValue + "', '" + empId.Text + "'";
                    string insert_exp = "INSERT INTO tbl_exp_moterway (" + insert_exp_text + ") VALUES (" + insert_exp_value + ")";
                    if (dBScript.actionSql(insert_exp))
                    {
                        if (txtExpMoterwayEnd.Text.Trim() == "00-00-0000")
                        {
                            string sqlChkPos = "SELECT emp_pos_id FROM tbl_emp_profile WHERE emp_pos_id ='" + txtExpMoterwayPos.SelectedValue + "' AND emp_id = '" + empId.Text + "' ";
                            rs = dBScript.selectSQL(sqlChkPos);
                            if (!rs.Read())
                            {
                                dBScript.actionSql("UPDATE tbl_emp_profile SET emp_pos_id = '" + txtExpMoterwayPos.SelectedValue + "' WHERE  emp_id='" + empId.Text + "'");
                            }
                            rs.Close();
                        }
                        alert = "บันทึกข้อมูล ประวัติการทำงาน(ภายในฝ่ายจัดเก็บฯ) สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                        txtExpMoterwayStart.Text = "";
                        txtExpMoterwayEnd.Text = "";
                        txtExpMoterwayPos.SelectedIndex = 0;
                        //txtExpMoterwaySaraly.Text = "";
                        showData();
                    }
                    else
                    {
                        alert = "Error : บันทึกข้อมูล ประวัติการทำงาน(ภายในฝ่ายจัดเก็บฯ) ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                    }
                }
            }
            TabClick.Value = "menu3";
            dBScript.CloseConnection();
        }

        protected void btnContactSave_Click(object sender, EventArgs e)
        {
            if (empId.Text != "")
            {
                alert = "";
                if (txtGuarantorName.Text != "")
                {
                    string sql_text = "guarantor_name,guarantor_pos,guarantor_extraction,guarantor_add,guarantor_tel,guarantor_emp_id";
                    string sql_value = "'" + txtGuarantorName.Text + "','" + txtGuarantorPos.Text + "','" + txtGuarantorAff.Text + "','" + txtGuarantorAdd.Text + "','" + txtGuarantorTel.Text + "','" + empId.Text + "'";
                    string sql_insert = "INSERT INTO tbl_guarantor (" + sql_text + ") VALUES (" + sql_value + ")";
                    if (dBScript.actionSql(sql_insert))
                    {
                        alert = "บันทึกข้อมูล บุคคลรับรองหรือค้ำประกัน สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                    }
                    else
                    {
                        alert = "Error : บันทึกข้อมูล บุคคลรับรองหรือค้ำประกัน ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                    }
                }

                if (txtEConName.Text != "")
                {
                    string sql_text = "emergency_contact_name,emergency_contact_relationship,emergency_contact_add,emergency_contact_tel,emergency_contact_emp_id";
                    string sql_value = "'" + txtEConName.Text + "','" + txtEConRelationShip.Text + "','" + txtEConAdd.Text + "','" + txtEConTel.Text + "','" + empId.Text + "'";
                    string sql_insert = "INSERT INTO tbl_emergency_contact (" + sql_text + ") VALUES (" + sql_value + ")";
                    if (dBScript.actionSql(sql_insert))
                    {
                        alert = "บันทึกข้อมูล บุคคลที่สามารถติดต่อได้ในกรณีฉุกเฉิน สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                    }
                    else
                    {
                        alert = "Error : บันทึกข้อมูล บุคคลที่สามารถติดต่อได้ในกรณีฉุกเฉิน ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                    }
                }
            }
            else
            {
                alert = "";

                if (txtGuarantorName.Text != "")
                {
                    string updat_value = "guarantor_name='" + txtGuarantorName.Text + "',";
                    updat_value += "guarantor_pos='" + txtGuarantorPos.Text + "',";
                    updat_value += "guarantor_extraction='" + txtGuarantorAff.Text + "',";
                    updat_value += "guarantor_add='" + txtGuarantorTel.Text + "'";
                    updat_value += "guarantor_tel='" + txtGuarantorTel.Text + "'";
                    if (dBScript.actionSql("UPDATE tbl_guarantor SET " + updat_value + " WHERE guarantor_emp_id = '" + empId.Text + "'"))
                    {
                        alert = "บันทึกข้อมูล บุคคลรับรองหรือค้ำประกัน สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                    }
                    else
                    {
                        alert = "Error : บันทึกข้อมูล บุคคลรับรองหรือค้ำประกัน ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                    }
                }

                if (txtEConName.Text != "")
                {
                    string updat_value = "emergency_contact_name='" + txtEConName.Text + "',";
                    updat_value += "emergency_contact_relationship='" + txtEConName.Text + "',";
                    updat_value += "emergency_contact_add='" + txtEConAdd.Text + "',";
                    updat_value += "emergency_contact_tel='" + txtEConTel.Text + "'";
                    if (dBScript.actionSql("UPDATE tbl_emergency_contact SET " + updat_value + " WHERE emergency_contact_emp_id = '" + empId.Text + "'"))
                    {
                        alert = "บันทึกข้อมูล บุคคลที่สามารถติดต่อได้ในกรณีฉุกเฉิน สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                        showData();
                    }
                    else
                    {
                        alert = "Error : บันทึกข้อมูล บุคคลที่สามารถติดต่อได้ในกรณีฉุกเฉิน ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                    }
                }

            }
            showData();
            TabClick.Value = "menu4";
            dBScript.CloseConnection();
        }

        protected void btnDocAdd_Click(object sender, EventArgs e)
        {
            alert = "";

            String NewFileDocName = "";
            if (txtFileDoc.HasFile)
            {
                string typeFile = txtFileDoc.FileName.Split('.')[txtFileDoc.FileName.Split('.').Length - 1];
                if (typeFile == "jpg" || typeFile == "jpeg" || typeFile == "png")
                {
                    NewFileDocName = empId.Text + txtDocType.SelectedItem;
                    NewFileDocName = "/Upload/doc/" + dBScript.getMd5Hash(NewFileDocName) + "." + txtFileDoc.FileName.Split('.')[txtFileDoc.FileName.Split('.').Length - 1];
                    txtFileDoc.SaveAs(Server.MapPath(NewFileDocName.ToString()));

                    string sql_text = "doc_images,doc_type_doc_id,doc_emp_id";
                    string sql_value = "'" + NewFileDocName + "','" + txtDocType.SelectedValue + "','" + empId.Text + "'";
                    string sql_insert = "INSERT INTO tbl_doc (" + sql_text + ") VALUES (" + sql_value + ")";
                    if (dBScript.actionSql(sql_insert))
                    {
                        alert = "บันทึกข้อมูล เอกสาร" + txtDocType.SelectedItem + " สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                    }
                    else
                    {
                        alert = "Error : บันทึกข้อมูล เอกสาร" + txtDocType.SelectedItem + " ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                    }
                }
                else
                {
                    alert = "Error : บันทึกข้อมูล เอกสาร" + txtDocType.SelectedItem + " ล้มเหลว<br/> ไฟล์เอกสารต้องเป็น *.jpg *.jpge *.png เท่านั้น<br/>"; alertType = "danger"; icon = "error";
                    //msgErrMenu5.Text += "Error : บันทึกข้อมูล เอกสาร" + txtDocType.SelectedItem + " ล้มเหลว<br/> ไฟล์เอกสารต้องเป็น *.jpg *.jpge *.png เท่านั้น<br/>";
                }
            }
            else
            {
                alert = "Error : บันทึกข้อมูล เอกสาร" + txtDocType.SelectedItem + " ล้มเหลว<br/> ไม่พบไฟล์เอกสาร<br/>"; alertType = "danger"; icon = "error";
                //msgErrMenu5.Text += "Error : บันทึกข้อมูล เอกสาร" + txtDocType.SelectedItem + " ล้มเหลว<br/> ไม่พบไฟล์เอกสาร<br/>";
            }
            showData();
            TabClick.Value = "menu5";
            dBScript.CloseConnection();
        }
        protected void DocGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //เอกสาร
            Label lbDocName = (Label)(e.Row.FindControl("lbDocName"));
            if (lbDocName != null)
            {
                lbDocName.Text = (string)DataBinder.Eval(e.Row.DataItem, "type_doc_name");
            }

            //ปุ่มดู
            LinkButton btnDocViwe = (LinkButton)(e.Row.FindControl("btnDocViwe"));
            if (btnDocViwe != null)
            {
                btnDocViwe.Attributes["href"] = (string)DataBinder.Eval(e.Row.DataItem, "doc_images");
            }

            //ปุ่มดาวโหลด
            Button btnDocDowload = (Button)(e.Row.FindControl("btnDocDowload"));
            if (btnDocDowload != null)
            {
                btnDocDowload.CommandArgument = ((string)DataBinder.Eval(e.Row.DataItem, "doc_images")).ToString();
            }

            //ปุ่มลบ
            Button btnDocDelete = (Button)(e.Row.FindControl("btnDocDelete"));
            if (btnDocDelete != null)
            {
                btnDocDelete.CommandArgument = ((int)DataBinder.Eval(e.Row.DataItem, "doc_id")).ToString();
                btnDocDelete.CommandName = "Delete";
            }
        }

        protected void btnDocDelete_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                alert = "";

                string partFile = "";
                string sql = "SELECT * FROM tbl_doc WHERE doc_id = '" + e.CommandArgument.ToString() + "'";
                MySqlDataReader rs = dBScript.selectSQL(sql);
                if (rs.Read())
                {
                    partFile = rs.GetString("doc_images");
                    rs.Close();

                    string sql_delete = "DELETE FROM tbl_doc WHERE doc_id = '" + e.CommandArgument.ToString() + "'";
                    if (dBScript.actionSql(sql_delete))
                    {
                        if (File.Exists(Server.MapPath(partFile)))
                        {
                            File.Delete(Server.MapPath(partFile));
                        }
                        alert = "ลบข้อมูล เอกสาร สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                        //msgSuccessMenu5.Text += "ลบข้อมูล เอกสาร สำเร็จ<br/>";
                        showData();
                    }
                    else
                    {
                        alert = "Error : ลบข้อมูล เอกสาร ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                        //msgErrMenu5.Text += "Error : ลบข้อมูล เอกสาร ล้มเหลว<br/>";
                    }
                }
            }
            TabClick.Value = "menu5";
            dBScript.CloseConnection();
        }

        protected void btnDocDowload_Command(object sender, CommandEventArgs e)
        {
            //string Filpath = Server.MapPath(e.CommandArgument.ToString());
            DownLoad(e.CommandArgument.ToString());
        }

        public void DownLoad(string FName)
        {
            try
            {
                string strURL = FName;
                string[] typeFile = FName.Split('/');
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + dBScript.getMd5Hash(empId.Text + DateTime.Now) + "." + typeFile[typeFile.Length - 1].Split('.')[1] + "\"");
                byte[] data = req.DownloadData(Server.MapPath(strURL));
                response.BinaryWrite(data);
                response.End();
                dBScript.CloseConnection();
            }
            catch
            {
                dBScript.CloseConnection();
            }
            /*string path = FName;
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name); Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
            }*/
        }

        protected void ExpMoterwayGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ExpMoterwayGridView.EditIndex = e.NewEditIndex;
            //ExpMoterwayGridView.ShowFooter = false;
            showData();
            TabClick.Value = "menu3";
        }

        protected void ExpMoterwayGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ExpMoterwayGridView.EditIndex = -1;
            //ExpMoterwayGridView.ShowFooter = true;
            showData();
            TabClick.Value = "menu3";
        }

        protected void ExpMoterwayGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //*** DateStart ***//
            TextBox txtEditDateStart = (TextBox)ExpMoterwayGridView.Rows[e.RowIndex].FindControl("txtEditDateStart");
            //*** DateEnd ***//
            TextBox txtEditDateEnd = (TextBox)ExpMoterwayGridView.Rows[e.RowIndex].FindControl("txtEditDateEnd");

            alert = "";

            string strSQL = "UPDATE tbl_exp_moterway SET exp_moterway_start = '" + txtEditDateStart.Text + "' " +
                " ,exp_moterway_end = '" + txtEditDateEnd.Text + "' " +
                " WHERE exp_moterway_id = '" + ExpMoterwayGridView.DataKeys[e.RowIndex].Value + "'";
            if (dBScript.actionSql(strSQL))
            {
                alert = "แก้ไขข้อมูล ประวัติการการทำงาน(ภายในฝ่ายจัดเก็บฯ) สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                //msgSuccessMenu3.Text += "แก้ไขข้อมูล ประวัติการการทำงาน(ภายในฝ่ายจัดเก็บฯ) สำเร็จ<br/>";
            }
            else
            {
                alert = "Error : แก้ไข้อมูล ประวัติการการทำงาน(ภายในฝ่ายจัดเก็บฯ) ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                //msgErrMenu3.Text += "Error : แก้ไข้อมูล ประวัติการการทำงาน(ภายในฝ่ายจัดเก็บฯ) ล้มเหลว<br/>";
            }

            ExpMoterwayGridView.EditIndex = -1;
            //ExpMoterwayGridView.ShowFooter = true;
            showData();
            TabClick.Value = "menu3";
            dBScript.CloseConnection();
        }

        protected void ExpMoterwayGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            alert = "";
            string sql = "DELETE FROM tbl_exp_moterway WHERE exp_moterway_id = '" + ExpMoterwayGridView.DataKeys[e.RowIndex].Value + "'";
            if (dBScript.actionSql(sql))
            {
                alert = "ลบข้อมูล ประวัติการการทำงาน(ภายในฝ่ายจัดเก็บฯ) สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                //msgSuccessMenu3.Text += "ลบข้อมูล ประวัติการการทำงาน(ภายในฝ่ายจัดเก็บฯ) สำเร็จ<br/>";
                showData();
            }
            else
            {
                alert = "Error : ลบข้อมูล ประวัติการการทำงาน(ภายในฝ่ายจัดเก็บฯ) ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                //msgErrMenu3.Text += "Error : ลบข้อมูล ประวัติการการทำงาน(ภายในฝ่ายจัดเก็บฯ) ล้มเหลว<br/>";
            }
            TabClick.Value = "menu3";
            dBScript.CloseConnection();
        }

        protected void btnExpCpointAdd_Click(object sender, EventArgs e)
        {

            if (empId.Text != "")
            {
                alert = "";

                bool chkExpMoterwayEnd = true;
                string sql = "SELECT * FROM tbl_work_history WHERE work_history_emp_id = '" + empId.Text + "' AND work_history_out = '00-00-0000'";
                MySqlDataReader rs = dBScript.selectSQL(sql);
                if (rs.Read())
                {
                    if (txtExpCpointEnd.Text.Trim() == "00-00-0000")
                    {
                        chkExpMoterwayEnd = false;
                        alert = "Error : บันทึกข้อมูล ประวัติการทำงาน/ด่านฯ ล้มเหลว<br/> - มีประวัติการทำงานถึงปัจจุบันก่อนหน้านี้แล้ว<br/>"; alertType = "danger"; icon = "error";
                        //msgErrMenu6.Text += "Error : บันทึกข้อมูล ประวัติการทำงาน/ด่านฯ ล้มเหลว<br/> - มีประวัติการทำงานถึงปัจจุบันก่อนหน้านี้แล้ว<br/>";
                    }
                    rs.Close();
                }

                if (chkExpMoterwayEnd)
                {
                    string insert_work_history_text = "work_history_in,work_history_out,work_history_pos,work_history_aff,work_history_cpoint,work_history_emp_id";
                    string insert_work_history_value = "'" + txtExpCpointStart.Text + "','" + txtExpCpointEnd.Text + "','" + dBScript.getEmpData("emp_pos_id", empId.Text) + "','" + txtExpCpointAff.SelectedValue + "','" + txtExpCpoint.Text + "', '" + empId.Text + "'";
                    string insert_work_history = "INSERT INTO tbl_work_history (" + insert_work_history_text + ") VALUES (" + insert_work_history_value + ")";
                    if (dBScript.actionSql(insert_work_history))
                    {
                        if (txtExpCpointEnd.Text.Trim() == "00-00-0000")
                        {
                            string sqlChkAff = "SELECT emp_affi_id FROM tbl_emp_profile WHERE emp_affi_id ='" + txtExpCpointAff.SelectedValue + "' AND emp_id = '" + empId.Text + "' ";
                            rs = dBScript.selectSQL(sqlChkAff);
                            if (!rs.Read())
                            {
                                dBScript.actionSql("UPDATE tbl_emp_profile SET emp_affi_id = '" + txtExpCpointAff.SelectedValue + "' WHERE  emp_id='" + empId.Text + "'");
                            }
                            rs.Close();

                            string sqlChkCpoint = "SELECT emp_cpoint_id FROM tbl_emp_profile WHERE emp_cpoint_id ='" + txtExpCpoint.SelectedValue + "' AND emp_id = '" + empId.Text + "' ";
                            rs = dBScript.selectSQL(sqlChkCpoint);
                            if (!rs.Read())
                            {
                                dBScript.actionSql("UPDATE tbl_emp_profile SET emp_cpoint_id = '" + txtExpCpoint.SelectedValue + "' WHERE  emp_id='" + empId.Text + "'");
                            }
                            rs.Close();
                        }
                        alert = "บันทึกข้อมูล ประวัติการทำงาน/ด่านฯ สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                        //msgSuccessMenu6.Text += "บันทึกข้อมูล ประวัติการทำงาน/ด่านฯ สำเร็จ<br/>";
                        txtExpCpointStart.Text = "";
                        txtExpCpointEnd.Text = "";
                        txtExpCpoint.SelectedIndex = 0;
                        showData();
                    }
                    else
                    {
                        alert = "Error : บันทึกข้อมูล ประวัติการทำงาน/ด่านฯ ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                        //msgErrMenu6.Text += "Error : บันทึกข้อมูล ประวัติการทำงาน/ด่านฯ ล้มเหลว<br/>";
                    }
                }
            }
            TabClick.Value = "menu6";
            dBScript.CloseConnection();
        }

        protected void ExpCpointGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //ตั้งแต่-ถึง
            Label lbExpCpointDate = (Label)(e.Row.FindControl("lbExpCpointDate"));
            if (lbExpCpointDate != null)
            {
                lbExpCpointDate.Text = dBScript.convertDateShortThai((string)DataBinder.Eval(e.Row.DataItem, "work_history_in")) + " - " + dBScript.convertDateShortThai((string)DataBinder.Eval(e.Row.DataItem, "work_history_out"));
            }

            //ตำแหน่ง
            Label lbExpCpointPos = (Label)(e.Row.FindControl("lbExpCpointPos"));
            if (lbExpCpointPos != null)
            {
                lbExpCpointPos.Text = (string)DataBinder.Eval(e.Row.DataItem, "pos_name");
            }

            //ด่านฯ
            Label lbExpCpointName = (Label)(e.Row.FindControl("lbExpCpointName"));
            if (lbExpCpointName != null)
            {
                lbExpCpointName.Text = (string)DataBinder.Eval(e.Row.DataItem, "affi_name") + " / " + (string)DataBinder.Eval(e.Row.DataItem, "cpoint_name");
            }

            //Confirm ปุ่ม ลบ
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    ((LinkButton)e.Row.Cells[4].Controls[0]).OnClientClick = "return confirmDelete(this);";
                }
                catch { dBScript.CloseConnection(); }
            }
            dBScript.CloseConnection();
        }

        protected void ExpCpointGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ExpCpointGridView.EditIndex = e.NewEditIndex;
            //ExpMoterwayGridView.ShowFooter = false;
            showData();
            TabClick.Value = "menu6";
        }

        protected void ExpCpointGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ExpCpointGridView.EditIndex = -1;
            //ExpMoterwayGridView.ShowFooter = true;
            showData();
            TabClick.Value = "menu6";
        }

        protected void ExpCpointGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //*** DateStart ***//
            TextBox txtExpCpointDateStart = (TextBox)ExpCpointGridView.Rows[e.RowIndex].FindControl("txtExpCpointDateStart");
            //*** DateEnd ***//
            TextBox txtExpCpointDateEnd = (TextBox)ExpCpointGridView.Rows[e.RowIndex].FindControl("txtExpCpointDateEnd");

            alert = "";

            bool chkExpMoterwayEnd = true;
            string sql = "SELECT * FROM tbl_work_history WHERE work_history_emp_id = '" + empId.Text + "' AND work_history_out = '00-00-0000'";
            MySqlDataReader rs = dBScript.selectSQL(sql);
            if (rs.Read())
            {
                if (txtExpCpointDateEnd.Text.Trim() == "00-00-0000")
                {
                    chkExpMoterwayEnd = false;
                    alert = "Error : แก้ไขข้อมูล ประวัติการทำงาน/ด่านฯ ล้มเหลว<br/> - มีประวัติการทำงานถึงปัจจุบันก่อนหน้านี้แล้ว<br/>"; alertType = "danger"; icon = "error";
                    //msgErrMenu6.Text += "Error : แก้ไขข้อมูล ประวัติการทำงาน/ด่านฯ ล้มเหลว<br/> - มีประวัติการทำงานถึงปัจจุบันก่อนหน้านี้แล้ว<br/>";
                }
                rs.Close();
            }

            if (chkExpMoterwayEnd)
            {
                string strSQL = "UPDATE tbl_work_history SET work_history_in = '" + txtExpCpointDateStart.Text + "' " +
                    " ,work_history_out = '" + txtExpCpointDateEnd.Text + "' " +
                    " WHERE work_history_id = '" + ExpCpointGridView.DataKeys[e.RowIndex].Value + "'";

                if (dBScript.actionSql(strSQL))
                {
                    alert = "แก้ไขข้อมูล ประวัติการทำงาน/ด่านฯ สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                    //msgSuccessMenu6.Text += "แก้ไขข้อมูล ประวัติการทำงาน/ด่านฯ สำเร็จ<br/>";
                }
                else
                {
                    alert = "Error : แก้ไข้อมูล ประวัติการทำงาน/ด่านฯ ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                    //msgErrMenu6.Text += "Error : แก้ไข้อมูล ประวัติการทำงาน/ด่านฯ ล้มเหลว<br/>";
                }
            }
            ExpCpointGridView.EditIndex = -1;
            //ExpMoterwayGridView.ShowFooter = true;
            showData();
            TabClick.Value = "menu6";
            dBScript.CloseConnection();
        }

        protected void ExpCpointGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            alert = "";
            string sql = "DELETE FROM tbl_work_history WHERE work_history_id = '" + ExpCpointGridView.DataKeys[e.RowIndex].Value + "'";
            if (dBScript.actionSql(sql))
            {
                alert = "ลบข้อมูล ประวัติการทำงาน/ด่านฯ สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                //msgSuccessMenu6.Text += "ลบข้อมูล ประวัติการทำงาน/ด่านฯ สำเร็จ<br/>";
                showData();
            }
            else
            {
                alert = "Error : ลบข้อมูล ประวัติการทำงาน/ด่านฯ ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                //msgErrMenu6.Text += "Error : ลบข้อมูล ประวัติการทำงาน/ด่านฯ ล้มเหลว<br/>";
            }
            TabClick.Value = "menu6";
            dBScript.CloseConnection();
        }

        protected void ExpCpointGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ExpCpointGridView.PageIndex = e.NewPageIndex;
            getSeclctExpCpoint();
            TabClick.Value = "menu6";
        }

        protected void btnHistoryAdd_Click(object sender, EventArgs e)
        {
            if (empId.Text != "")
            {
                alert = "";
                bool chack = true;
                if (!checkStatus(dBScript.getEmpData("id", empId.Text)))
                {
                    if (txtHistoryStatus.SelectedValue == "1" && txtHistoryDate.Text != "00-00-0000")
                    {
                        chack = false;
                        alert = "Error : บันทึกข้อมูล ประวัติการเข้า-ออก/สถานะภาพ ล้มเหลว<br/> - สถานะทำงานอยู่ต้องเลือกวันที่เป็น 00-00-0000 เท่านั้น"; alertType = "danger"; icon = "error";
                        //msgErrMenu7.Text += "Error : บันทึกข้อมูล ประวัติการเข้า-ออก/สถานะภาพ ล้มเหลว<br/> - สถานะทำงานอยู่ต้องเลือกวันที่เป็น 00-00-0000 เท่านั้น";
                    }

                    if (chack)
                    {
                        string insert_history_text = "history_status_id,history_date,history_note,history_emp_id";
                        string insert_history_value = "'" + txtHistoryStatus.SelectedValue + "','" + txtHistoryDate.Text + "','" + txtHistoryNote.Text + "', '" + dBScript.getEmpData("id", empId.Text) + "'";
                        string insert_history = "INSERT INTO tbl_history (" + insert_history_text + ") VALUES (" + insert_history_value + ")";
                        if (dBScript.actionSql(insert_history))
                        {
                            dBScript.setEmpStatus(dBScript.getEmpData("id", empId.Text), txtHistoryStatus.SelectedValue);
                            alert = "บันทึกข้อมูล ประวัติการเข้า-ออก/สถานะภาพ สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                            //msgSuccessMenu7.Text += "บันทึกข้อมูล ประวัติการเข้า-ออก/สถานะภาพ สำเร็จ<br/>";
                            txtHistoryStatus.SelectedIndex = 0;
                            txtHistoryDate.Text = "";
                            txtHistoryNote.Text = "";
                            showData();
                        }
                        else
                        {
                            alert = "Error : บันทึกข้อมูล ประวัติการเข้า-ออก/สถานะภาพ ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                            //msgErrMenu7.Text += "Error : บันทึกข้อมูล ประวัติการเข้า-ออก/สถานะภาพ ล้มเหลว<br/>";
                        }
                    }

                }
                else
                {
                    alert = "Error : บันทึกข้อมูล ประวัติการเข้า-ออก/สถานะภาพ ล้มเหลว<br/> - มีสถานะ ทำงาน - ปัจจุบันอยู่ไม่สามารถเพิ่มสะถานะอื่นได้กรุณาแก้ไขข้อมูลก่อน"; alertType = "danger"; icon = "error";
                    //msgErrMenu7.Text += "Error : บันทึกข้อมูล ประวัติการเข้า-ออก/สถานะภาพ ล้มเหลว<br/> - มีสถานะ ทำงาน - ปัจจุบันอยู่ไม่สามารถเพิ่มสะถานะอื่นได้กรุณาแก้ไขข้อมูลก่อน";
                }
            }
            TabClick.Value = "menu7";
            dBScript.CloseConnection();
        }

        protected void InOutHistoryGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //สถานภาพ
            Label lbHistoryStatus = (Label)(e.Row.FindControl("lbHistoryStatus"));
            if (lbHistoryStatus != null)
            {
                lbHistoryStatus.Text = (string)DataBinder.Eval(e.Row.DataItem, "status_working_name");
            }

            //วันที่
            Label lbHistoryDate = (Label)(e.Row.FindControl("lbHistoryDate"));
            if (lbHistoryDate != null)
            {
                lbHistoryDate.Text = dBScript.convertDateShortThai((string)DataBinder.Eval(e.Row.DataItem, "history_date"));
            }

            //หมายเหตุ
            Label lbHistoryNote = (Label)(e.Row.FindControl("lbHistoryNote"));
            if (lbHistoryNote != null)
            {
                lbHistoryNote.Text = (string)DataBinder.Eval(e.Row.DataItem, "history_note");
            }

            //*** Edit ***'
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList txtHistoryStatusEdit = (DropDownList)e.Row.FindControl("txtHistoryStatusEdit");
                if ((txtHistoryStatusEdit != null))
                {
                    string sql_status_working = "SELECT * FROM tbl_status_working";
                    dBScript.GetDownList(txtHistoryStatusEdit, sql_status_working, "status_working_name", "status_working_id");
                    txtHistoryStatusEdit.SelectedIndex = txtHistoryStatusEdit.Items.IndexOf(txtHistoryStatusEdit.Items.FindByValue((string)DataBinder.Eval(e.Row.DataItem, "status_working_id").ToString()));
                }
            }


            //Confirm ปุ่ม ลบ
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    ((LinkButton)e.Row.Cells[4].Controls[0]).OnClientClick = "return confirmDelete(this);";
                }
                catch { dBScript.CloseConnection(); }
            }
            dBScript.CloseConnection();
        }
        protected void InOutHistoryGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            InOutHistoryGridView.EditIndex = e.NewEditIndex;
            //InOutHistoryGridView.ShowFooter = false;
            showData();
            TabClick.Value = "menu7";
        }

        protected void InOutHistoryGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            InOutHistoryGridView.EditIndex = -1;
            //InOutHistoryGridView.ShowFooter = true;
            showData();
            TabClick.Value = "menu7";
        }

        protected void InOutHistoryGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //*** Status ***//
            DropDownList txtHistoryStatusEdit = (DropDownList)InOutHistoryGridView.Rows[e.RowIndex].FindControl("txtHistoryStatusEdit");

            //*** DateEnd ***//
            TextBox txtExpCpointDateEnd = (TextBox)InOutHistoryGridView.Rows[e.RowIndex].FindControl("txtHistoryDateEdit");

            //*** DateEnd ***//
            TextBox txtHistoryNoteEdit = (TextBox)InOutHistoryGridView.Rows[e.RowIndex].FindControl("txtHistoryNoteEdit");

            alert = "";

            if (txtExpCpointDateEnd.Text != "00-00-0000")
            {
                string strSQL = "UPDATE tbl_history SET history_status_id = '" + txtHistoryStatusEdit.SelectedValue + "' " +
                " ,history_date = '" + txtExpCpointDateEnd.Text + "',history_note='" + txtHistoryNoteEdit.Text + "' " +
                " WHERE history_id = '" + InOutHistoryGridView.DataKeys[e.RowIndex].Value + "'";

                if (dBScript.actionSql(strSQL))
                {
                    if (txtHistoryStatusEdit.SelectedValue != "1")
                    {
                        dBScript.setEmpStatus(dBScript.getEmpData("id", empId.Text), txtHistoryStatusEdit.SelectedValue);
                    }
                    alert = "แก้ไขข้อมูล ประวัติการการทำงาน(ภายในฝ่ายจัดเก็บฯ) สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                    //alert = "Error : บันทึกข้อมูล ประวัติการเข้า-ออก/สถานะภาพ ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                    //msgSuccessMenu7.Text += "แก้ไขข้อมูล ประวัติการการทำงาน(ภายในฝ่ายจัดเก็บฯ) สำเร็จ<br/>";
                }
                else
                {
                    alert = "Error : แก้ไข้อมูล ประวัติการการทำงาน(ภายในฝ่ายจัดเก็บฯ) ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                    //msgErrMenu7.Text += "Error : แก้ไข้อมูล ประวัติการการทำงาน(ภายในฝ่ายจัดเก็บฯ) ล้มเหลว<br/>";
                }

            }
            else
            {
                alert = "Error : บันทึกข้อมูล ประวัติการเข้า-ออก/สถานะภาพ ล้มเหลว<br/> - ไม่อนุญาติให้แก้ไขวันที่เป็น 00-00-0000 (ปัจจุบัน)"; alertType = "danger"; icon = "error";
                //msgErrMenu7.Text += "Error : บันทึกข้อมูล ประวัติการเข้า-ออก/สถานะภาพ ล้มเหลว<br/> - ไม่อนุญาติให้แก้ไขวันที่เป็น 00-00-0000 (ปัจจุบัน)";
            }
            InOutHistoryGridView.EditIndex = -1;
            //InOutHistoryGridView.ShowFooter = true;
            showData();
            TabClick.Value = "menu7";
            dBScript.CloseConnection();
        }

        protected void InOutHistoryGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            alert = "";
            string sql = "DELETE FROM tbl_history WHERE history_id = '" + InOutHistoryGridView.DataKeys[e.RowIndex].Value.ToString() + "'";
            if (dBScript.actionSql(sql))
            {
                alert = "ลบข้อมูล ประวัติการการทำงาน(ภายในฝ่ายจัดเก็บฯ) สำเร็จ<br/>"; alertType = "success"; icon = "add_alert";
                //alert = "Error : บันทึกข้อมูล ประวัติการเข้า-ออก/สถานะภาพ ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                //msgSuccessMenu7.Text += "ลบข้อมูล ประวัติการการทำงาน(ภายในฝ่ายจัดเก็บฯ) สำเร็จ<br/>";
                showData();
            }
            else
            {
                alert = "Error : ลบข้อมูล ประวัติการการทำงาน(ภายในฝ่ายจัดเก็บฯ) ล้มเหลว<br/>"; alertType = "danger"; icon = "error";
                //msgErrMenu7.Text += "Error : ลบข้อมูล ประวัติการการทำงาน(ภายในฝ่ายจัดเก็บฯ) ล้มเหลว<br/>";
            }
            TabClick.Value = "menu7";
            dBScript.CloseConnection();
        }

        private bool checkStatus(string id)
        {
            string sql = "SELECT * FROM tbl_history WHERE history_emp_id = '" + id + "' AND history_date = '00-00-0000'";
            MySqlDataReader rs = dBScript.selectSQL(sql);
            if (rs.Read())
            {
                rs.Close();
                dBScript.CloseConnection();
                return true;
            }
            rs.Close();
            dBScript.CloseConnection();
            return false;
        }

        private void GroupProfile(bool status)
        {
            txtProfix.Enabled = status;
            txtName.Enabled = status;
            txtLname.Enabled = status;
            txtPos.Enabled = status;
            txtPosNum.Enabled = status;
            txtAffi.Enabled = status;
            txtCpoint.Enabled = status;
            txtEmpType.Enabled = status;
            txtDateStart.Enabled = status;
            //txtBrithDate.Enabled = status;
            txtAge.Enabled = status;
            txtIdcard.Enabled = status;
            txtBookBank.Enabled = status;
            txtGuarantorName.Enabled = status;
            txtGuarantorPos.Enabled = status;
            txtGuarantorAff.Enabled = status;
            txtGuarantorAdd.Enabled = status;
            txtGuarantorTel.Enabled = status;
            txtGuarantorName.Enabled = status;
            txtGuarantorName.Enabled = status;
            txtGuarantorName.Enabled = status;

            DivExpMotoway.Visible = status;
            DivExpCpoint.Visible = status;
            DivStatus.Visible = status;
            GroupHideGridview(EduGridView, 5, status);
            GroupHideGridview(ExpMoterwayGridView, 2, status);
            GroupHideGridview(ExpMoterwayGridView, 3, status);
            GroupHideGridview(DocGridView, 2, status);
            GroupHideGridview(ExpCpointGridView, 3, status);
            GroupHideGridview(ExpCpointGridView, 4, status);
            GroupHideGridview(InOutHistoryGridView, 3, status);
            GroupHideGridview(InOutHistoryGridView, 4, status);
        }

        private void GroupHideGridview(GridView grid, int columns, bool status)
        {
            grid.Columns[columns].Visible = status;
        }
    }
}