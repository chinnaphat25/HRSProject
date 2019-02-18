<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reportView.aspx.cs" Inherits="HRSProject.Report.reportView" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/crystalreportviewers13/js/crviewer/crv.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <CR:CrystalReportViewer ID="resultReportLeave" runat="server"
                    EnableParameterPrompt="False" 
                    ToolPanelView="None"  HasCrystalLogo="False" HasToggleGroupTreeButton="False" 
                PrintMode="Pdf"/>
        </div>
    </form>
</body>
</html>
