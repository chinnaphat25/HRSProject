<%@ Page Title="ปีงบประมาณ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="yearFrom.aspx.cs" Inherits="HRSProject.Admin.yearFrom" %>

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
        <div class="col-md-2">
            ปีงบประมาณ 
            <asp:TextBox ID="txtYear" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-1">
            <br />
            <asp:Button ID="btnYearAdd" runat="server" Text="&#xf067; เพิ่ม" Font-Size="Medium" CssClass="btn btn-success btn-sm align-items-end fa" OnClick="btnYearAdd_Click" />
        </div>
    </div>
    <hr />
    <div class="col-md-3 align-content">
        <div class="card">
            <div class="card-header card-header-warning">
                <h3 class="card-title">ปีงบประมาณ</h3>
            </div>
            <div class="card-body table-responsive">
                <div class="form-row">
                    <asp:GridView ID="YearGridView" runat="server"
                        DataKeyNames="year"
                        GridLines="None"
                        OnRowDataBound="YearGridView_RowDataBound"
                        AutoGenerateColumns="False"
                        CssClass="table table-hover table-sm"
                        OnRowDeleting="YearGridView_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="ปีงบประมาณ">
                                <ItemTemplate>
                                    <asp:Label ID="lbYear" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.year") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" HeaderText="ลบ" DeleteText="&#xf014; ลบ" ControlStyle-CssClass="btn btn-outline-danger btn-sm fa" ControlStyle-Font-Size="Small" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="card-footer">
                <div class="stats">
                    <asp:Label ID="lbYearNull" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
