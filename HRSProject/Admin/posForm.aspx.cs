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
    public partial class posForm : System.Web.UI.Page
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
            string sql = "SELECT * FROM tbl_pos";
            MySqlDataAdapter da = dbScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            PosGridView.DataSource = ds.Tables[0];
            PosGridView.DataBind();
            lbPosNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
        }

        protected void btnPosAdd_Click(object sender, EventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            if (txtPos.Text != "")
            {
                string sql = "INSERT INTO tbl_pos (pos_name) VALUES ('" + txtPos.Text + "')";
                if (dbScript.actionSql(sql))
                {
                    txtPos.Text = "";
                    msgSuccess.Text = "เพิ่มตำแหน่งสำเร็จ<br/>";
                }
                else
                {
                    msgErr.Text = "เพิ่มตำแหน่งล้มเหลว<br/>";
                }
                BindData();
            }
            else
            {
                msgErr.Text = "เพิ่มตำแหน่งล้มเหลว<br/>- กรุณาใส่ตำแหน่ง";
            }
        }

        protected void PosGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void PosGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            PosGridView.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void PosGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            PosGridView.EditIndex = -1;
            BindData();
        }

        protected void PosGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            TextBox txtPos = (TextBox)PosGridView.Rows[e.RowIndex].FindControl("txtPos");

            string sql = "UPDATE tbl_pos SET pos_name='" + txtPos.Text + "' WHERE pos_id = '" + PosGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "แก้ไขตำแหน่งสำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "แก้ไขตำแหน่งล้มเหลว<br/>";
            }
            PosGridView.EditIndex = -1;
            BindData();
        }

        protected void PosGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";

            string sql = "DELETE FROM tbl_pos WHERE pos_id = '" + PosGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "ลบตำแหน่งสำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "ลบตำแหน่งล้มเหลว<br/>";
            }
            PosGridView.EditIndex = -1;
            BindData();
        }
    }
}