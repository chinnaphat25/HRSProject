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
    public partial class affForm : Page
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
            string sql = "SELECT * FROM tbl_affiliation ORDER BY affi_name";
            MySqlDataAdapter da = dbScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            AffGridView.DataSource = ds.Tables[0];
            AffGridView.DataBind();
            lbAffNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
        }

        protected void btnAffAdd_Click(object sender, EventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            if (txtAff.Text != "")
            {
                string sql = "INSERT INTO tbl_affiliation (affi_name) VALUES ('" + txtAff.Text + "')";
                if (dbScript.actionSql(sql))
                {
                    txtAff.Text = "";
                    msgSuccess.Text = "เพิ่มหน่วยสำเร็จ<br/>";
                }
                else
                {
                    msgErr.Text = "เพิ่มหน่วยล้มเหลว<br/>";
                }
                BindData();
            }
            else
            {
                msgErr.Text = "เพิ่มหน่วยล้มเหลว<br/>- กรุณาใส่หน่วย";
            }
        }

        protected void AffGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void AffGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            AffGridView.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void AffGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            AffGridView.EditIndex = -1;
            BindData();
        }

        protected void AffGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            TextBox txtAff = (TextBox)AffGridView.Rows[e.RowIndex].FindControl("txtAff");

            string sql = "UPDATE tbl_affiliation SET affi_name='" + txtAff.Text + "' WHERE affi_id = '" + AffGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "แก้ไขหน่วยสำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "แก้ไขหน่วยล้มเหลว<br/>";
            }
            AffGridView.EditIndex = -1;
            BindData();
        }

        protected void AffGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";

            string sql = "DELETE FROM tbl_affiliation WHERE affi_id = '" + AffGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "ลบหน่วยสำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "ลบหน่วยล้มเหลว<br/>";
            }
            AffGridView.EditIndex = -1;
            BindData();
        }
    }
}