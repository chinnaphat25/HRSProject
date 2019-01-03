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
    public partial class yearFrom : System.Web.UI.Page
    {
        DBScript dbScript = new DBScript();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                BindData();
            }
        }
        void BindData()
        {
            string sql = "SELECT * FROM tbl_year";
            MySqlDataAdapter da = dbScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            YearGridView.DataSource = ds.Tables[0];
            YearGridView.DataBind();
            lbYearNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
        }

        protected void YearGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    ((LinkButton)e.Row.Cells[1].Controls[0]).OnClientClick = "return confirmDelete(this);";
                }
                catch { }
            }
        }

        protected void YearGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";

            string sql = "DELETE FROM tbl_year WHERE year = '" + YearGridView.DataKeys[e.RowIndex].Value + "'";
            if (dbScript.actionSql(sql))
            {
                msgSuccess.Text = "ลบปีงบประมาณสำเร็จ<br/>";
            }
            else
            {
                msgErr.Text = "ลบปีงบประมาณล้มเหลว<br/>";
            }
            YearGridView.EditIndex = -1;
            BindData();
        }

        protected void btnYearAdd_Click(object sender, EventArgs e)
        {
            msgSuccess.Text = "";
            msgErr.Text = "";
            msgAlert.Text = "";
            if (txtYear.Text != "")
            {
                string sql = "INSERT INTO tbl_year (year) VALUES ('" + txtYear.Text + "')";
                if (dbScript.actionSql(sql))
                {
                    txtYear.Text = "";
                    msgSuccess.Text = "เพิ่มปีงบประมาณสำเร็จ<br/>";
                }
                else
                {
                    msgErr.Text = "เพิ่มปีงบประมาณล้มเหลว<br/>";
                }
                BindData();
            }
            else
            {
                msgErr.Text = "เพิ่มปีงบประมาณล้มเหลว<br/>- กรุณาใส่ปีงบประมาณ";
            }
        }
    }
}