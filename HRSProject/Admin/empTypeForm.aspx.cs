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
    public partial class empTypeForm : System.Web.UI.Page
    {
        DBScript dbScript = new DBScript();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                if (dbScript.Notallow(new string[] { "5", "4", "3", "2" }, Session["UserPrivilegeId"].ToString()))
                {
                    Response.Redirect("/");
                }
            }

            if (!this.IsPostBack)
            {
                BindData();
            }
        }

        void BindData()
        {
            string sql = "SELECT * FROM tbl_type_emp";
            MySqlDataAdapter da = dbScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            EmpTypeGridView.DataSource = ds.Tables[0];
            EmpTypeGridView.DataBind();
            lbEmpTypeNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
        }

        protected void btnEmpTypeAdd_Click(object sender, EventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            if (txtEmpType.Text != "")
            {
                string sql = "INSERT INTO tbl_type_emp (type_emp_name) VALUES ('" + txtEmpType.Text + "')";
                if (dbScript.actionSql(sql))
                {
                    txtEmpType.Text = "";
                    msgSuccess.Text = "เพิ่มประเภทพนักงานสำเร็จ<br/>";
                }
                else
                {
                    msgErr.Text = "เพิ่มประเภทพนักงานล้มเหลว<br/>";
                }
                BindData();
            }
            else
            {
                msgErr.Text = "เพิ่มประเภทพนักงานล้มเหลว<br/>- กรุณาใส่ประเภทพนักงาน";
            }
        }

        protected void EmpTypeGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    ((LinkButton)e.Row.Cells[2].Controls[0]).OnClientClick = "return confirmDelete(this);";
                }
                catch { }
            }
        }

        protected void EmpTypeGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            EmpTypeGridView.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void EmpTypeGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            EmpTypeGridView.EditIndex = -1;
            BindData();
        }

        protected void EmpTypeGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            TextBox txtEmpType = (TextBox)EmpTypeGridView.Rows[e.RowIndex].FindControl("txtEmpType");

            string sql = "UPDATE tbl_type_emp SET type_emp_name='" + txtEmpType.Text + "' WHERE type_emp_id = '" + EmpTypeGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "แก้ไขประเภทพนักงานสำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "แก้ไขประเภทพนักงานล้มเหลว<br/>";
            }
            EmpTypeGridView.EditIndex = -1;
            BindData();
        }

        protected void EmpTypeGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";

            string sql = "DELETE FROM tbl_type_emp WHERE type_emp_id = '" + EmpTypeGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "ลบประเภทพนักงานสำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "ลบประเภทพนักงานล้มเหลว<br/>";
            }
            EmpTypeGridView.EditIndex = -1;
            BindData();
        }
    }
}