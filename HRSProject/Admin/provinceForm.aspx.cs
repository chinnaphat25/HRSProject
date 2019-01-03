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
    public partial class provinceForm : System.Web.UI.Page
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
            string sql = "SELECT * FROM tbl_province";
            MySqlDataAdapter da = dbScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ProvinceGridView.DataSource = ds.Tables[0];
            ProvinceGridView.DataBind();
            lbProvinceNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
        }

        protected void btnProvinceAdd_Click(object sender, EventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            if (txtProvince.Text != "")
            {
                string sql = "INSERT INTO tbl_province (province_name) VALUES ('" + txtProvince.Text + "')";
                if (dbScript.actionSql(sql))
                {
                    txtProvince.Text = "";
                    msgSuccess.Text = "เพิ่มจังหวัดสำเร็จ<br/>";
                }
                else
                {
                    msgErr.Text = "เพิ่มจังหวัดล้มเหลว<br/>";
                }
                BindData();
            }
            else
            {
                msgErr.Text = "เพิ่มจังหวัดล้มเหลว<br/>- กรุณาใส่จังหวัด";
            }
        }

        protected void ProvinceGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void ProvinceGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ProvinceGridView.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void ProvinceGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ProvinceGridView.EditIndex = -1;
            BindData();
        }

        protected void ProvinceGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            TextBox txtProvince = (TextBox)ProvinceGridView.Rows[e.RowIndex].FindControl("txtProvince");

            string sql = "UPDATE tbl_province SET province_name='" + txtProvince.Text + "' WHERE province_id = '" + ProvinceGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "แก้ไขจังหวัดสำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "แก้ไขจังหวัดล้มเหลว<br/>";
            }
            ProvinceGridView.EditIndex = -1;
            BindData();
        }

        protected void ProvinceGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";

            string sql = "DELETE FROM tbl_province WHERE province_id = '" + ProvinceGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "ลบจังหวัดสำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "ลบจังหวัดล้มเหลว<br/>";
            }
            ProvinceGridView.EditIndex = -1;
            BindData();
        }
    }
}