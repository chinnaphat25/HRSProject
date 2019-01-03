using System;
using System.Web;
using HRSProject.Config;
using MySql.Data.MySqlClient;

namespace HRSProject.Login
{
    public partial class Login : System.Web.UI.Page
    {
        DBScript dBScript = new DBScript();
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSubmit.Click += new EventHandler(this.btnSubmit_Click);
            if (Session["User"] != null || Request.Cookies["cookiesLogin"] != null)
            {
                Response.Redirect("/");
            }
        }

        private void btnSubmit_Click(Object sender, EventArgs e)
        {
            string mess = "";
            if (txtUser.Text.Trim() == "")
            {
                mess += "- กรุณาป้อน Username<br/>";
            }

            if (txtPass.Text.Trim() == "")
            {
                mess += "- กรุณาป้อน Password<br/>";
            }

            if (mess == "")
            {
                string sql = "SELECT * FROM tbl_emp_user join tbl_privilege on privilege_id = emp_user_privilege WHERE emp_user_name ='" + txtUser.Text.Trim() + "' AND emp_user_pass = '" + txtPass.Text.Trim() + "'";
                MySqlDataReader rs = dBScript.selectSQL(sql);
                if (rs.Read())
                {

                    if (!rs.IsDBNull(0))
                    {
                        if (rs.GetString("emp_status_login") != "1")
                        {
                            // Storee Session
                            Session.Add("User", txtUser.Text);
                            Session.Add("UserName", rs.GetString("emp_name"));
                            Session.Add("UserPrivilegeId", rs.GetString("privilege_id"));
                            Session.Add("UserPrivilege", rs.GetString("privilege_name"));
                            if (rs.GetString("privilege_id") == "5")
                            {
                                Session.Add("emp_login_id", rs.GetString("emp_user_id"));
                            }
                            else
                            {
                                Session.Add("emp_login_id", null);
                            }
                            Session.Timeout = 60;

                            // now Storing Cookies & config.
                            Response.Cookies["cookiesLogin"]["User"] = txtUser.Text;
                            Response.Cookies["cookiesLogin"]["UserName"] = rs.GetString("emp_name");
                            Response.Cookies["cookiesLogin"]["UserPrivilegeId"] = rs.GetString("privilege_id");
                            Response.Cookies["cookiesLogin"]["UserPrivilege"] = rs.GetString("privilege_name");
                            if (rs.GetString("privilege_id") == "5")
                            {
                                Response.Cookies["cookiesLogin"]["emp_login_id"] = rs.GetString("emp_user_id");
                            }
                            else
                            {
                                Response.Cookies["cookiesLogin"]["emp_login_id"] = null;
                            }
                            Response.Cookies["cookiesLogin"].Expires = DateTime.Now.AddDays(1);

                            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('dd')</script>");
                            dBScript.userLoginUpdate(txtUser.Text);
                            Response.Redirect("/");
                        }
                        else
                        {
                            mess += "- ***ไม่อนุญาติให้มีการเข้าใช้งานด้วยรหัสเดียวกันซ้ำ";
                        }

                    }
                    else
                    {
                        mess += "- Username หรือ Password ไม่ถูกต้อง";
                    }
                }
                else
                {
                    mess += "- Username หรือ Password ไม่ถูกต้อง";
                }
            }

            if (mess != "")
            {
                MsgBox(mess);
            }
            else
            {
                msgBox.Text = "";
            }
        }

        private void MsgBox(string message)
        {
            msgBox.Text = "<div class='alert alert-danger' style='font-size:large;'><strong>ผิดพลาด! </strong><br/>" + message + "</div>";
        }

        protected void btnLoginEmp_Click(object sender, EventArgs e)
        {
            MsgBox("");
            string sql = "SELECT * FROM tbl_emp_profile WHERE emp_id_card ='" + txtLoginIDCard.Text.Trim() + "'";
            MySqlDataReader rs = dBScript.selectSQL(sql);
            if (rs.Read())
            {

                if (!rs.IsDBNull(0))
                {
                    // Storee Session
                    Session.Add("User", txtUser.Text);
                    Session.Add("UserName", rs.GetString("emp_name")+ " " +rs.GetString("emp_lname"));
                    Session.Add("UserPrivilegeId", "5");
                    Session.Add("UserPrivilege", "Employee");
                    Session.Add("emp_login_id", rs.GetString("emp_id"));
                    Session.Timeout = 60;

                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('dd')</script>");
                    //dBScript.userLoginUpdate(txtUser.Text);
                    Response.Redirect("/Profile/empForm?empID="+dBScript.getMd5Hash(rs.GetString("emp_id")));

                }
                else
                {
                    MsgBox("ไม่พบข้อมูลในระบบ ลองใหม่อีกครั้ง");
                }
            }
            else
            {
                MsgBox("ไม่พบข้อมูลในระบบ ลองใหม่อีกครั้ง");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);
        }
    }
}