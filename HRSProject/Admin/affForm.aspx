﻿<%@ Page Title="ข้อมูลหน่วย" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="affForm.aspx.cs" Inherits="HRSProject.Admin.affForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
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
        <div class="col-md-2">
            หน่วย 
            <asp:TextBox ID="txtAff" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-1">
            <br />
            <asp:Button ID="btnAffAdd" runat="server" Text="&#xf067; เพิ่ม" Font-Size="Medium" CssClass="btn btn-success btn-sm align-items-end fa" OnClick="btnAffAdd_Click" />
        </div>
    </div>
    <hr />
    <div class="form-row">
        <asp:GridView ID="AffGridView" runat="server"
            DataKeyNames="affi_id"
            GridLines="None"
            OnRowDataBound="AffGridView_RowDataBound"
            AutoGenerateColumns="False"
            CssClass="table table-hover table-sm"
            OnRowEditing="AffGridView_RowEditing"
            OnRowCancelingEdit="AffGridView_RowCancelingEdit"
            OnRowUpdating="AffGridView_RowUpdating"
            OnRowDeleting="AffGridView_RowDeleting">
            <Columns>
                <asp:TemplateField HeaderText="หน่วย">
                    <ItemTemplate>
                        <asp:Label ID="lbAff" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.affi_name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtAff" size="20" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.affi_name") %>' CssClass="form-control"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" CancelText="ยกเลิก" EditText="&#xf040; แก้ไข" UpdateText="แก้ไข" HeaderText="ปรับปรุง" ControlStyle-Font-Size="Small" ControlStyle-CssClass="btn btn-outline-warning btn-sm fa" />
                <asp:CommandField ShowDeleteButton="True" HeaderText="ลบ" DeleteText="&#xf014; ลบ" ControlStyle-CssClass="btn btn-outline-danger btn-sm fa" ControlStyle-Font-Size="Small" />
            </Columns>
        </asp:GridView>
    </div>
    <asp:Label ID="lbAffNull" runat="server" Text="Label"></asp:Label>
</asp:Content>