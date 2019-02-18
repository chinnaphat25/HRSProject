<%@ Page Title="พิมพ์รายชื่อพนักงาน" Language="C#" AutoEventWireup="true" CodeBehind="ReportListEmp.aspx.cs" Inherits="HRSProject.Profile.ReportListEmp" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/crystalreportviewers13/js/crviewer/crv.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--<div class="row">
            <asp:Label ID="Label1" runat="server" Text="เลือกข้อมูลที่ต้องการ : "></asp:Label>
            <asp:DropDownList ID="cpoint_id" runat="server" OnSelectedIndexChanged="cpoint_id_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <asp:DropDownList ID="pos_id" runat="server" OnSelectedIndexChanged="cpoint_id_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        </div>
        <hr />-->
        <div>
            <CR:CrystalReportViewer ID="resultListEmp" runat="server"
                EnableParameterPrompt="False"
                ToolPanelView="None" GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False" PrintMode="ActiveX" AutoDataBind="false" EnableDatabaseLogonPrompt="false" />
        </div>
    </form>
</body>
</html>
