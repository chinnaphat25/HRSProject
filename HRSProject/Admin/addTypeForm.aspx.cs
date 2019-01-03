using HRSProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace HRSProject.Admin
{
    public partial class addTypeForm : System.Web.UI.Page
    {
        DBScript dbScript = new DBScript();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] != null)
            {
                if (dbScript.Notallow(new string[] { "5", "4", "3","2" }, Session["UserPrivilegeId"].ToString()))
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
            string sql = "SELECT * FROM tbl_type_add";
            MySqlDataAdapter da = dbScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            TypeAddGridView.DataSource = ds.Tables[0];
            TypeAddGridView.DataBind();
            lbTypeAddNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
        }

        protected void btnProfixAdd_Click(object sender, EventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            if (txtTypeAdd.Text != "")
            {
                string sql = "INSERT INTO tbl_type_add (type_add_name) VALUES ('" + txtTypeAdd.Text + "')";
                if (dbScript.actionSql(sql))
                {
                    txtTypeAdd.Text = "";
                    msgSuccess.Text = "เพิ่มประเภทที่อยู่สำเร็จ<br/>";
                }
                else
                {
                    msgErr.Text = "เพิ่มประเภทที่อยู่ล้มเหลว<br/>";
                }
                BindData();
            }
            else
            {
                msgErr.Text = "เพิ่มประเภทที่อยู่ล้มเหลว<br/>- กรุณาใส่ประเภทที่อยู่";
            }
        }

        protected void TypeAddGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void TypeAddGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            TypeAddGridView.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void TypeAddGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            TypeAddGridView.EditIndex = -1;
            BindData();
        }

        protected void TypeAddGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            TextBox txtTypeAdd = (TextBox)TypeAddGridView.Rows[e.RowIndex].FindControl("txtTypeAdd");

            string sql = "UPDATE tbl_type_add SET type_add_name='" + txtTypeAdd.Text + "' WHERE type_add_id = '" + TypeAddGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "แก้ไขประเภทที่อยู่สำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "แก้ไขประเภทที่อยู่ล้มเหลว<br/>";
            }
            TypeAddGridView.EditIndex = -1;
            BindData();
        }

        protected void TypeAddGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";

            string sql = "DELETE FROM tbl_type_add WHERE type_add_id = '" + TypeAddGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "ลบประเภทที่อยู่สำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "ลบประเภทที่อยู่ล้มเหลว<br/>";
            }
            TypeAddGridView.EditIndex = -1;
            BindData();
        }
    }
}