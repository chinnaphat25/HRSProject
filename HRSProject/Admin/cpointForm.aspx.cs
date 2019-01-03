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
    public partial class cpointForm : Page
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
            string sql = "SELECT * FROM tbl_cpoint";
            MySqlDataAdapter da = dbScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            CpointGridView.DataSource = ds.Tables[0];
            CpointGridView.DataBind();
            lbCpointNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
        }

        protected void CpointGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    ((LinkButton)e.Row.Cells[3].Controls[0]).OnClientClick = "return confirmDelete(this);";
                }
                catch { }
            }
        }

        protected void CpointGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CpointGridView.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void CpointGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            CpointGridView.EditIndex = -1;
            BindData();
        }

        protected void CpointGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            TextBox txtCpointId = (TextBox)CpointGridView.Rows[e.RowIndex].FindControl("txtCpointId");
            TextBox txtCpoint = (TextBox)CpointGridView.Rows[e.RowIndex].FindControl("txtCpoint");

            string sql = "UPDATE tbl_cpoint SET cpoint_id='"+ txtCpointId.Text+ "', cpoint_name='" + txtCpoint.Text + "' WHERE cpoint_id = '" + CpointGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "แก้ไขด่านฯ สำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "แก้ไขด่านฯ ล้มเหลว<br/>";
            }
            CpointGridView.EditIndex = -1;
            BindData();
        }

        protected void CpointGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";

            string sql = "DELETE FROM tbl_cpoint WHERE cpoint_id = '" + CpointGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "ลบด่านฯ สำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "ลบด่านฯ ล้มเหลว<br/>";
            }
            CpointGridView.EditIndex = -1;
            BindData();
        }

        protected void btnCpointAdd_Click(object sender, EventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            if (txtCpoint.Text != "")
            {
                string sql = "INSERT INTO tbl_cpoint (cpoint_id,cpoint_name) VALUES ('"+txtCpointId.Text+"','" + txtCpoint.Text + "')";
                if (dbScript.actionSql(sql))
                {
                    txtCpoint.Text = "";
                    msgSuccess.Text = "เพิ่มด่านฯ สำเร็จ<br/>";
                }
                else
                {
                    msgErr.Text = "เพิ่มด่านฯ ล้มเหลว<br/>";
                }
                BindData();
            }
            else
            {
                msgErr.Text = "เพิ่มด่านฯ ล้มเหลว<br/>- กรุณาใส่ด่านฯ";
            }
        }
    }
}