using HRSProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRSProject.SearchHistory
{
    public partial class historyForm : System.Web.UI.Page
    {
        DBScript dbScript = new DBScript();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                resultCard.Visible = false;
            }
        }

        public void BindData()
        {
            string sql = "SELECT * FROM tbl_emp_profile JOIN tbl_history ON id = history_emp_id JOIN tbl_profix ON profix_id=emp_profix_id JOIN tbl_status_working ON status_working_id = history_status_id WHERE emp_id_card = '" + txtSearchIDCard.Text.Trim() + "' ORDER BY history_id DESC";
            MySqlDataAdapter da = dbScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridViewEmp.DataSource = ds.Tables[0];
            GridViewEmp.DataBind();
            if (ds.Tables[0].Rows.Count != 0)
            {
                LaGridViewData.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
            }
            else
            {
                LaGridViewData.Text = "ไม่พบข้อมูล";
            }

            resultCard.Visible = true;
        }

        protected void btnSearchEmp_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void GridViewEmp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lbempStatusDate = (Label)(e.Row.FindControl("lbempStatusDate"));
            if (lbempStatusDate != null)
            {
                lbempStatusDate.Text = dbScript.convertDateShortThai((string)DataBinder.Eval(e.Row.DataItem, "history_date"));
            }

            LinkButton btnView = (LinkButton)(e.Row.FindControl("btnView"));
            if (btnView != null)
            {
                btnView.CommandArgument = (string)DataBinder.Eval(e.Row.DataItem, "emp_id");
            }
        }

        protected void btnView_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect("/Profile/empForm?empID=" + dbScript.getMd5Hash(e.CommandArgument.ToString()) + "&view=true");
        }
    }
}