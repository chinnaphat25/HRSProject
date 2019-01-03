using HRSProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace HRSProject.Manpower
{
    public partial class editManpower : System.Web.UI.Page
    {
        DBScript dBScript = new DBScript();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string sql_cpoint = "SELECT * FROM tbl_cpoint";
                GetDownList(txtCpoint, sql_cpoint, "cpoint_name", "cpoint_id");

                string sql_year = "SELECT * FROM tbl_year";
                GetDownList(txtYear, sql_year, "year", "year");

                string sql_aff = "SELECT * FROM tbl_affiliation ORDER BY affi_name";
                GetDownList(txtAffAdd, sql_aff, "affi_name", "affi_id");

                diAdd.Visible = false;
                //BindData();
            }
        }

        void BindData()
        {
            string sql = "SELECT * FROM tbl_manpower LEFT JOIN tbl_cpoint ON manpower_cpoint_id = cpoint_id LEFT JOIN tbl_affiliation ON affi_id = manpower_pos_id WHERE manpower_year = '" + txtYear.SelectedValue + "' AND manpower_cpoint_id = '" + txtCpoint.SelectedValue + "'";
            MySqlDataAdapter da = dBScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ManPowerGridView.DataSource = ds.Tables[0];
            ManPowerGridView.DataBind();
            if (ds.Tables[0].Rows.Count>0)
            {
                lbManPowerNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
            }
            else
            {
                lbManPowerNull.Text = "ไม่พบข้อมูล";
            }
        }

        public void GetDownList(DropDownList list, string sql, string text, string value)
        {
            MySqlDataReader rs = dBScript.selectSQL(sql);
            using (var reader = dBScript.selectSQL(sql))
            {
                if (reader.HasRows)
                {
                    list.DataSource = reader;
                    list.DataValueField = value;
                    list.DataTextField = text;
                    list.DataBind();
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            txtHeard.Text = "อัตรากำลัง " + dBScript.getCpointData("cpoint_name", txtCpoint.SelectedValue) + " ปีงบประมาณ " + txtYear.SelectedValue;
            BindData();
            diAdd.Visible = true;
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
        }

        protected void ManPowerGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void btnManPowerAdd_Click(object sender, EventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            string sql_chk = "select * from tbl_manpower where manpower_pos_id = '"+txtAffAdd.SelectedValue+ "' and manpower_cpoint_id = '"+txtCpoint.SelectedValue+"'";
            MySqlDataReader rs = dBScript.selectSQL(sql_chk);
            if (!rs.Read()) {
                string sql = "INSERT INTO tbl_manpower (manpower_pos_id,manpower_full,manpower_cpoint_id,manpower_year) VALUES ('" + txtAffAdd.SelectedValue + "','" + txtFullAdd.Text + "','" + txtCpoint.SelectedValue + "','" + txtYear.SelectedValue + "')";
                if (dBScript.actionSql(sql))
                {
                    msgSuccess.Text = "เพิ่มอัตรากำลัง " + txtCpoint.SelectedItem + " สำเร็จ<br/>";
                }
                else
                {
                    msgErr.Text = "เพิ่มอัตรากำลัง " + txtCpoint.SelectedItem + " ล้มเหลว<br/>";
                }
                BindData();
            }
            else
            {
                msgErr.Text = "เพิ่มอัตรากำลัง " + txtCpoint.SelectedItem + " ล้มเหลว<br/> - "+ txtCpoint.SelectedItem + " มีข้อมูลอัตตรากำลัง "+txtAffAdd.SelectedItem+" อยู่แล้ว<br/>";
            }
        }

        protected void ManPowerGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ManPowerGridView.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void ManPowerGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ManPowerGridView.EditIndex = -1;
            BindData();
        }

        protected void ManPowerGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";

            string sql = "DELETE FROM tbl_manpower WHERE manpower_id = '" + ManPowerGridView.DataKeys[e.RowIndex].Value + "'";
            if (dBScript.actionSql(sql))
            {
                msgSuccess.Text = "ลบอัตรากำลัง " + txtCpoint.SelectedItem + " สำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "ลบอัตรากำลัง " + txtCpoint.SelectedItem + " ล้มเหลว<br/>";
            }
            ManPowerGridView.EditIndex = -1;
            BindData();
        }

        protected void ManPowerGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            TextBox txtTypeAdd = (TextBox)ManPowerGridView.Rows[e.RowIndex].FindControl("txtTypeAdd");

            string sql = "UPDATE tbl_manpower SET manpower_full='" + txtFullAdd.Text + "' WHERE manpower_id = '" + ManPowerGridView.DataKeys[e.RowIndex].Value + "'";
            if (dBScript.actionSql(sql))
            {
                msgSuccess.Text = "แก้ไขอัตรากำลัง " + txtCpoint.SelectedItem + " สำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "แก้ไขอัตรากำลัง " + txtCpoint.SelectedItem + " ล้มเหลว<br/>";
            }
            ManPowerGridView.EditIndex = -1;
            BindData();
        }
    }

}