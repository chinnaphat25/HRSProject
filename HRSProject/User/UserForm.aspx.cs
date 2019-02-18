using HRSProject.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRSProject.User
{
    public partial class UserForm : System.Web.UI.Page
    {
        DBScript dbScript = new DBScript();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("/");
            }
        }

        protected void btnConfirmPass_Click(object sender, EventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            if (txtNewPass.Text.Trim() == txtConfirmNewPass.Text.Trim()&& txtNewPass.Text.Trim() != "" && txtConfirmNewPass.Text.Trim() != "")
            {
                string sql = "UPDATE tbl_emp_user SET emp_user_pass = '"+txtNewPass.Text.Trim()+ "' WHERE emp_user_name='"+ Session["User"].ToString() + "'";
                if (dbScript.actionSql(sql))
                {
                    txtNewPass.Text = "";
                    txtConfirmNewPass.Text = "";
                    msgSuccess.Text = "เปลี่ยนรหัสผ่านสำเร็จสำเร็จ<br/>";
                }
                else
                {
                    msgErr.Text = "เปลี่ยนรหัสผ่านสำเร็จล้มเหลว<br/>";
                }
            }
            else
            {
                msgErr.Text ="ใส่ข้อมูลไม่ครบถ้วน หรือ รหัสผ่านใหม่ไม่ตรงกัน";
            }
            dbScript.CloseConnection();
        }
    }
}