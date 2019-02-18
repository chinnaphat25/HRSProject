using HRSProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRSProject.Guest
{
    public partial class GuestDefault : System.Web.UI.Page
    {
        DBScript dBScript = new DBScript();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindData();
            }

            if (int.Parse(Session["UserPrivilegeId"].ToString()) > 1)
            {
                btnAddGuest.Visible = false;
            }
        }

        protected void GridViewGuest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton lbGuestTitle = (LinkButton)(e.Row.FindControl("lbGuestTitle"));
            if (lbGuestTitle != null)
            {
                lbGuestTitle.CommandName = DataBinder.Eval(e.Row.DataItem, "guest_id").ToString();
            }

            LinkButton lbGueatRefer = (LinkButton)(e.Row.FindControl("lbGueatRefer"));
            if (lbGueatRefer != null)
            {
                lbGueatRefer.Text = "ที่ "+(string)DataBinder.Eval(e.Row.DataItem, "guest_refer")+" เรื่อง "+ (string)DataBinder.Eval(e.Row.DataItem, "guest_refer_title");
                lbGueatRefer.CommandName = (string)DataBinder.Eval(e.Row.DataItem, "guest_id").ToString();
            }

            LinkButton lbGuestDate = (LinkButton)(e.Row.FindControl("lbGuestDate"));
            if (lbGuestDate != null)
            {
                lbGuestDate.Text = dBScript.convertDatelongThai((string)DataBinder.Eval(e.Row.DataItem, "guest_refer_date"));
                lbGuestDate.CommandName = (string)DataBinder.Eval(e.Row.DataItem, "guest_id").ToString();
            }

            Label lbGuestAmount = (Label)(e.Row.FindControl("lbGuestAmount"));
            if (lbGuestAmount != null)
            {
                lbGuestAmount.Text = dBScript.selectCount("tbl_guest_list", "guest_id = '" + DataBinder.Eval(e.Row.DataItem, "guest_id").ToString() + "'", "guest_id").ToString();
            }
        }

        void BindData()
        {
            string sql = "SELECT * FROM tbl_guest guest ORDER BY STR_TO_DATE( guest.guest_offer_date, '%d-%m-%Y' ) DESC";
            MySqlDataAdapter da = dBScript.getDataSelect(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridViewGuest.DataSource = ds.Tables[0];
            GridViewGuest.DataBind();
            LaGridViewData.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
        }

        protected void btnAddGuest_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Guest/GuestNewListForm");
        }

        protected void GridViewGuest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Response.Redirect("/Guest/GuestNewListForm?id="+e.CommandName);
        }
    }
}