using HRSProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRSProject
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["User"] == null)
                {
                    if (Request.Cookies["cookiesLogin"] != null)
                    {
                        Session["User"] = Request.Cookies["cookiesLogin"]["User"];
                        Session["UserName"] = Request.Cookies["cookiesLogin"]["UserName"];
                        Session["UserPrivilege"] = Request.Cookies["cookiesLogin"]["UserPrivilege"];
                        Session["UserPrivilegeId"] = Request.Cookies["cookiesLogin"]["UserPrivilegeId"];
                        if (Response.Cookies["cookiesLogin"]["emp_login_id"] != null)
                        {
                            Session["emp_login_id"] = Response.Cookies["cookiesLogin"]["emp_login_id"];
                        }
                        else
                        {
                            Session["emp_login_id"] = null;
                        }
                    }
                    else
                    {
                        Response.Redirect("/Login/Login");
                        /*Session["User"] = "";
                        Session["UserName"] = "";
                        Session["UserPrivilege"] = "";
                        Session["UserPrivilegeId"] = "";*/
                    }
                    new DBScript().userLoginUpdate(Session["User"].ToString());
                }

                if (Session["User"] != null)
                {
                    lbUser.Text = "ยินดีต้อนรับ : คุณ" + Session["UserName"].ToString() + " : " + Session["UserPrivilege"];
                    MenuShow();
                }
            }

            if (Session["User"] != null)
            {
                new DBScript().userLoginUpdate(Session["User"].ToString());
            }
        }

        private void activeNav(System.Web.UI.HtmlControls.HtmlGenericControl nav)
        {

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            new DBScript().userLogOutUpdate(Session["User"].ToString());
            Session.Abandon();
            Session.Clear();
            Session.Contents.RemoveAll();
            Session.RemoveAll();
            if (Request.Cookies["cookiesLogin"] != null)
            {
                Response.Cookies["cookiesLogin"].Expires = DateTime.Now.AddDays(-1);
                // its trick it will expire yesterday next time when will login it will be expired..
                // because date was before 1 day from today..
            }
            Response.Redirect("/");
        }

        private void MenuShow()
        {
            switch (Session["UserPrivilegeId"].ToString())
            {
                case "0":
                    nav1.Visible = true;
                    nav3.Visible = true;
                    Li2.Visible = true;
                    Admin.Visible = true;
                    SUAdmin.Visible = true;
                    break;
                case "1":
                    nav1.Visible = true;
                    nav3.Visible = true;
                    Li2.Visible = true;
                    Admin.Visible = true;
                    SUAdmin.Visible = true;
                    break;
                case "2":
                    nav1.Visible = true;
                    nav3.Visible = true;
                    Li2.Visible = true;
                    Admin.Visible = true;
                    SUAdmin.Visible = false;
                    break;
                case "3":
                    nav1.Visible = true;
                    nav3.Visible = true;
                    Li2.Visible = true;
                    Admin.Visible = false;
                    SUAdmin.Visible = false;
                    break;
                case "4":
                    nav1.Visible = true;
                    nav3.Visible = true;
                    Li2.Visible = true;
                    Admin.Visible = true;
                    SUAdmin.Visible = false;
                    break;
                case "5":
                    nav1.Visible = false;
                    nav3.Visible = true;
                    Li2.Visible = true;
                    Admin.Visible = false;
                    SUAdmin.Visible = false;
                    break;
                default:
                    nav1.Visible = false;
                    nav3.Visible = false;
                    Li2.Visible = false;
                    Admin.Visible = false;
                    SUAdmin.Visible = false;
                    break;
            }
        }
    }
}
