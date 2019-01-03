using HRSProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace HRSProject.Admin
{
    public partial class levelEduForm : System.Web.UI.Page
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
            string sql = "SELECT * FROM tbl_level_edu";
            MySqlDataAdapter da = dbScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            LevelEduGridView.DataSource = ds.Tables[0];
            LevelEduGridView.DataBind();
            lbLevelEduNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
        }

        protected void btnLevelEdu_Click(object sender, EventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            if (txtLevelEdu.Text != "")
            {
                string sql = "INSERT INTO tbl_level_edu (level_edu_name) VALUES ('" + txtLevelEdu.Text + "')";
                if (dbScript.actionSql(sql))
                {
                    txtLevelEdu.Text = "";
                    msgSuccess.Text = "เพิ่มระดับการศึกษาสำเร็จ<br/>";
                }
                else
                {
                    msgErr.Text = "เพิ่มระดับการศึกษาล้มเหลว<br/>";
                }
                BindData();
            }
            else
            {
                msgErr.Text = "เพิ่มระดับการศึกษาล้มเหลว<br/>- กรุณาใส่ระดับการศึกษา";
            }
        }

        protected void LevelEduGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void LevelEduGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            LevelEduGridView.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void LevelEduGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            LevelEduGridView.EditIndex = -1;
            BindData();
        }

        protected void LevelEduGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            TextBox txtLevelEdu = (TextBox)LevelEduGridView.Rows[e.RowIndex].FindControl("txtLevelEdu");

            string sql = "UPDATE tbl_level_edu SET level_edu_name='" + txtLevelEdu.Text + "' WHERE level_edu_id = '" + LevelEduGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "แก้ไขระดับการศึกษาสำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "แก้ไขระดับการศึกษาล้มเหลว<br/>";
            }
            LevelEduGridView.EditIndex = -1;
            BindData();
        }

        protected void LevelEduGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";

            string sql = "DELETE FROM tbl_level_edu WHERE level_edu_id = '" + LevelEduGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "ลบระดับการศึกษาสำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "ลบระดับการศึกษาล้มเหลว<br/>";
            }
            LevelEduGridView.EditIndex = -1;
            BindData();
        }
    }
}