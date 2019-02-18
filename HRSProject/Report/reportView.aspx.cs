using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using HRSProject.Guest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRSProject.Report
{
    public partial class reportView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                

                if (Session["ReportTitle"] != null)
                {
                    Title = Session["ReportTitle"].ToString();
                    //resultReportLeave.ParameterFieldInfo = (ParameterFields)Session["para"];
                    resultReportLeave.ReportSource = ShowReport();
                    resultReportLeave.Visible = true;
                }
                else
                {
                    Response.Redirect("/");
                }
            }
        }

        public ReportDocument ShowReport()
        {
            ReportDocument cryRpt = (ReportDocument)Session["Report"];
            try
            {
                //cryRpt.SetDatabaseLogon("adminhrs", "admin25", "MySql DSN HR", "hrsystem");
            }
            catch (Exception ex)
            {
                throw ex;
                //ShowReport(@"default_report.rpt");
            }
            //CleareParameter();
            return cryRpt;
        }
    }
}