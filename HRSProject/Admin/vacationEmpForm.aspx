<%@ Page Title="วันลาพนักงาน" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vacationEmpForm.aspx.cs" Inherits="HRSProject.Admin.vacationEmpForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col">
            <%if (msgErr.Text != "")
                { %>
            <div class="alert alert-danger alert-dismissible fade show" role="alert">
                <asp:Label ID="msgErr" runat="server" Text=""></asp:Label>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <%} %>
            <%if (msgSuccess.Text != "")
                { %>
            <div class="alert alert-success alert-dismissible fade show" role="alert">
                <asp:Label ID="msgSuccess" runat="server" Text=""></asp:Label>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <%} %>
            <%if (msgAlert.Text != "")
                { %>
            <div class="alert alert-warning alert-dismissible fade show" role="alert">
                <asp:Label ID="msgAlert" runat="server" Text=""></asp:Label>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <%} %>
        </div>
    </div>
    <div class="form-row">
        <div class="card">
            <div class="card-header card-header-warning">
                <h3 class="card-title">วันลาพนักงาน</h3>
            </div>
            <div class="card-body table-responsive">
                <div class="row">
                    <div class="col-md-2">เริ่มงานเดือน ต.ค</div>
                    <div class="col-md-2">ปีงบประมาณที่ 1</div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
