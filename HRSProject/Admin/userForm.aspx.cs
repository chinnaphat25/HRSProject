using HRSProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRSProject.Admin
{
    public partial class userForm : System.Web.UI.Page
    {
        DBScript dBScript = new DBScript();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserPrivilegeId"] == null) { Response.Redirect("/"); }
            if (Session["User"] != null)
            {
                if (dBScript.Notallow(new string[] { "5", "4", "3", "2" }, Session["UserPrivilegeId"].ToString()))
                {
                    Response.Redirect("/");
                }
            }

            if (!this.IsPostBack)
            {
                string sql_privilege = "SELECT * FROM tbl_privilege ORDER BY privilege_name";
                dBScript.GetDownList(txtPrivilege, sql_privilege, "privilege_name", "privilege_id");

                BindData();
            }
        }

        void BindData()
        {
            string sql = "SELECT * FROM tbl_emp_user JOIN tbl_privilege ON emp_user_privilege=privilege_id";
            MySqlDataAdapter da = dBScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            UserGridView.DataSource = ds.Tables[0];
            UserGridView.DataBind();
            lbUserNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
        }

        protected void UserGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //*** Edit ***'
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList txtEPrivilege = (DropDownList)e.Row.FindControl("txtEPrivilege");
                if ((txtEPrivilege != null))
                {
                    string sql_status_working = "SELECT * FROM tbl_privilege";
                    dBScript.GetDownList(txtEPrivilege, sql_status_working, "privilege_name", "privilege_id");
                    txtEPrivilege.SelectedIndex = txtEPrivilege.Items.IndexOf(txtEPrivilege.Items.FindByValue((string)DataBinder.Eval(e.Row.DataItem, "emp_user_privilege").ToString()));
                }
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

        protected void UserGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            UserGridView.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void UserGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            UserGridView.EditIndex = -1;
            BindData();
        }

        protected void UserGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            TextBox txtEName = (TextBox)UserGridView.Rows[e.RowIndex].FindControl("txtEName");
            DropDownList txtEPrivilege = (DropDownList)UserGridView.Rows[e.RowIndex].FindControl("txtEPrivilege");

            string sql = "UPDATE tbl_emp_user SET emp_name='" + txtEName.Text + "',emp_user_privilege='" + txtEPrivilege.SelectedValue + "' WHERE emp_user_name = '" + UserGridView.DataKeys[e.RowIndex].Value + "'";
            if (dBScript.actionSql(sql))
            {
                msgSuccess.Text = "แก้ไขสำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "แก้ไขล้มเหลว<br/>";
            }
            UserGridView.EditIndex = -1;
            BindData();

        }

        protected void UserGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";

            string sql = "DELETE FROM tbl_emp_user WHERE emp_user_name = '" + UserGridView.DataKeys[e.RowIndex].Value + "'";
            if (dBScript.actionSql(sql))
            {
                msgSuccess.Text = "ลบสำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "ลบล้มเหลว<br/>";
            }
            UserGridView.EditIndex = -1;
            BindData();
        }

        protected void btnUserAdd_Click(object sender, EventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            if (txtPass.Text == txtPass.Text)
            {
                string sql = "INSERT INTO tbl_emp_user (emp_user_name,emp_user_pass,emp_name,emp_user_privilege,emp_status_login) VALUES ('" + txtUser.Text.Trim() + "','" + txtPass.Text.Trim() + "','" + txtName.Text + "','" + txtPrivilege.SelectedValue + "','0')";
                if (dBScript.actionSql(sql))
                {
                    txtName.Text = "";
                    txtPass.Text = "";
                    txtCPass.Text = "";
                    txtPrivilege.SelectedIndex = 0;
                    txtUser.Text = "";
                    msgSuccess.Text = "เพิ่มสำเร็จ<br/>";
                }
                else
                {
                    msgErr.Text = "เพิ่มล้มเหลว<br/>";
                }
                UserGridView.EditIndex = -1;
                BindData();
            }
            else
            {
                msgErr.Text = "เพิ่มล้มเหลว<br/> - รหัสผ่านไม่ตรงกัน";
            }
        }
    }
}