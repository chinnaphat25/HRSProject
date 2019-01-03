<%@ Page Title="อัตรากำลัง" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="HRSProject.Manpower.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md">
            <div class="row">
                <div class="col-md-4">
                    <asp:DropDownList ID="txtYear" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <asp:Button ID="btnSearch" runat="server" Text="&#xf002; ค้นหา" Font-Size="Medium" CssClass="btn btn-success btn-sm align-items-end fa" OnClick="btnSearch_Click" />
            </div>
        </div>
        <div class="col-md">
        </div>
        <div class="col-md-4">
            <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="/Manpower/yearFrom" Text="&#xf067; เพิ่มปีงบประมาณ" Font-Size="Medium" CssClass="btn btn-success btn-sm fa"></asp:LinkButton>&nbsp&nbsp&nbsp
        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="/Manpower/editManpower" Text="&#xf044; ปรับปรุงอัตรากำลัง" Font-Size="Medium" CssClass="btn btn-warning btn-sm fa"></asp:LinkButton>
        </div>
    </div>
    <hr />
    <h2><%:Title %> รวมทั้งหมด</h2>
    <hr />
    <div id="rowHead" class="row"  runat="server">
    </div>
    <hr />
    <h2><%:Title %> ตำแหน่ง/ด่านฯ</h2>
    <hr />
    <div id="rowShow" class="row"  runat="server">
    </div>
</asp:Content>
