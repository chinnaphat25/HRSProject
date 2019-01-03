using HRSProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace HRSProject.Admin
{
    public partial class profixForm : System.Web.UI.Page
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
            string sql = "SELECT * FROM tbl_profix";
            MySqlDataAdapter da = dbScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ProfixGridView.DataSource = ds.Tables[0];
            ProfixGridView.DataBind();
            lbProfixNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
        }

        protected void btnProfixAdd_Click(object sender, EventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            if (txtProfix.Text != "")
            {
                string sql = "INSERT INTO tbl_profix (profix_name) VALUES ('" + txtProfix.Text + "')";
                if (dbScript.actionSql(sql))
                {
                    txtProfix.Text = "";
                    msgSuccess.Text = "เพิ่มสรรพนามสำเร็จ<br/>";
                }
                else
                {
                    msgErr.Text = "เพิ่มสรรพนามล้มเหลว<br/>";
                }
                BindData();
            }
            else
            {
                msgErr.Text = "เพิ่มสรรพนามล้มเหลว<br/>- กรุณาใส่สรรพนาม";
            }
        }

        protected void ProfixGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ProfixGridView.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void ProfixGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ProfixGridView.EditIndex = -1;
            BindData();
        }

        protected void ProfixGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            TextBox txtProfix = (TextBox)ProfixGridView.Rows[e.RowIndex].FindControl("txtProfix");

            string sql = "UPDATE tbl_profix SET profix_name='" + txtProfix.Text + "' WHERE profix_id = '" + ProfixGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "แก้ไขสรรพนามสำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "แก้ไขสรรพนามล้มเหลว<br/>";
            }
            ProfixGridView.EditIndex = -1;
            BindData();
        }

        protected void ProfixGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";

            string sql = "DELETE FROM tbl_profix WHERE profix_id = '" + ProfixGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "ลบสรรพนามสำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "ลบสรรพนามล้มเหลว<br/>";
            }
            ProfixGridView.EditIndex = -1;
            BindData();
        }

        protected void ProfixGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
    }
}