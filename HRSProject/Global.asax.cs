using HRSProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace HRSProject
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application["TotalOnlineUsers"] = 0;
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown  
        }

        void Application_BeginRequest(object sender, EventArgs e)
        {
            DBScript dBScript = new DBScript();
            string sql = "SELECT SUM(emp_status_login) AS userOnilne FROM tbl_emp_user";
            MySqlDataReader rs = dBScript.selectSQL(sql);
            if (rs.Read())
            {
                if (!rs.IsDBNull(0))
                {
                    Application.Lock();
                    Application["TotalOnlineUsers"] = int.Parse(rs.GetString("userOnilne"));
                    Application.UnLock();
                }
            }
            rs.Close();
            dBScript.userLogOutTimeUpdate();

            //อัพเดทการลาออก
            dBScript.UpdateEmpEx();

            //อัพเดทการเปลี่ยนตำแหน่ง
            dBScript.UpdateEmpPos();

            //อัพเดทการย้ายด่านฯ
            dBScript.UpdateEmpCpoint();

            //อัพเดทพนักงานที่ไม่มีประวัติการทำงานภายในฝ่าย
            dBScript.CreateMotowayWorking();
            //อัพเดทพนักงานที่ไม่มีประวัติการทำงานที่ด่านฯ
            dBScript.CreateCpointWorking();

            dBScript.CloseConnection();
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs  
        }

        void Session_Start(object sender, EventArgs e)
        {
            //User Online Count

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends.   
            // Note: The Session_End event is raised only when the sessionstate mode  
            // is set to InProc in the Web.config file. If session mode is set to StateServer   
            // or SQLServer, the event is not raised.  
        }
    }
}