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
    public partial class docTypeForm : System.Web.UI.Page
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
            string sql = "SELECT * FROM tbl_type_doc";
            MySqlDataAdapter da = dbScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DocTypeGridView.DataSource = ds.Tables[0];
            DocTypeGridView.DataBind();
            lbDocTypeNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
        }

        protected void btnDocTypeAdd_Click(object sender, EventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            if (txtDocType.Text != "")
            {
                string sql = "INSERT INTO tbl_type_doc (type_doc_name) VALUES ('" + txtDocType.Text + "')";
                if (dbScript.actionSql(sql))
                {
                    txtDocType.Text = "";
                    msgSuccess.Text = "เพิ่มประเภทเอกสารสำเร็จ<br/>";
                }
                else
                {
                    msgErr.Text = "เพิ่มประเภทเอกสารล้มเหลว<br/>";
                }
                BindData();
            }
            else
            {
                msgErr.Text = "เพิ่มประเภทเอกสารล้มเหลว<br/>- กรุณาใส่ประเภทเอกสาร";
            }
        }

        protected void DocTypeGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void DocTypeGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DocTypeGridView.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void DocTypeGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            DocTypeGridView.EditIndex = -1;
            BindData();
        }

        protected void DocTypeGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            TextBox txtDocType = (TextBox)DocTypeGridView.Rows[e.RowIndex].FindControl("txtDocType");

            string sql = "UPDATE tbl_type_doc SET type_doc_name='" + txtDocType.Text + "' WHERE type_doc_id = '" + DocTypeGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "แก้ไขประเภทเอกสารสำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "แก้ไขประเภทเอกสารล้มเหลว<br/>";
            }
            DocTypeGridView.EditIndex = -1;
            BindData();
        }

        protected void DocTypeGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";

            string sql = "DELETE FROM tbl_type_doc WHERE type_doc_id = '" + DocTypeGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "ลบประเภทเอกสารสำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "ลบประเภทเอกสารล้มเหลว<br/>";
            }
            DocTypeGridView.EditIndex = -1;
            BindData();
        }
    }
}