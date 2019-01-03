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
    public partial class statusForm : System.Web.UI.Page
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
            string sql = "SELECT * FROM tbl_status_working";
            MySqlDataAdapter da = dbScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            StatusGridView.DataSource = ds.Tables[0];
            StatusGridView.DataBind();
            lbStatusNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
        }

        protected void btnStatusAdd_Click(object sender, EventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            if (txtStatus.Text != "")
            {
                string sql = "INSERT INTO tbl_status_working (status_working_name) VALUES ('" + txtStatus.Text + "')";
                if (dbScript.actionSql(sql))
                {
                    txtStatus.Text = "";
                    msgSuccess.Text = "เพิ่มสถานะสำเร็จ<br/>";
                }
                else
                {
                    msgErr.Text = "เพิ่มสถานะล้มเหลว<br/>";
                }
                BindData();
            }
            else
            {
                msgErr.Text = "เพิ่มสถานะล้มเหลว<br/>- กรุณาใส่สถานะ";
            }
        }

        protected void StatusGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void StatusGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            StatusGridView.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void StatusGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            StatusGridView.EditIndex = -1;
            BindData();
        }

        protected void StatusGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            TextBox txtStatus = (TextBox)StatusGridView.Rows[e.RowIndex].FindControl("txtStatus");

            string sql = "UPDATE tbl_status_working SET status_working_name='" + txtStatus.Text + "' WHERE status_working_id = '" + StatusGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "แก้ไขสถานะสำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "แก้ไขสถานะล้มเหลว<br/>";
            }
            StatusGridView.EditIndex = -1;
            BindData();
        }

        protected void StatusGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";

            string sql = "DELETE FROM tbl_status_working WHERE status_working_id = '" + StatusGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "ลบสถานะสำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "ลบสถานะล้มเหลว<br/>";
            }
            StatusGridView.EditIndex = -1;
            BindData();
        }
    }
}