<%@ Page Title="ข้อมูลประเภทเอกสาร" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="docTypeForm.aspx.cs" Inherits="HRSProject.Admin.docTypeForm" %>

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
            ประเภทเอกสาร 
            <asp:TextBox ID="txtDocType" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-1">
            <br />
            <asp:Button ID="btnDocTypeAdd" runat="server" Text="&#xf067; เพิ่ม" Font-Size="Medium" CssClass="btn btn-success btn-sm align-items-end fa" OnClick="btnDocTypeAdd_Click" />
        </div>
    </div>
    <hr />
    <div class="form-row">
        <asp:GridView ID="DocTypeGridView" runat="server"
            DataKeyNames="type_doc_id"
            GridLines="None"
            OnRowDataBound="DocTypeGridView_RowDataBound"
            AutoGenerateColumns="False"
            CssClass="table table-hover table-sm"
            OnRowEditing="DocTypeGridView_RowEditing"
            OnRowCancelingEdit="DocTypeGridView_RowCancelingEdit"
            OnRowUpdating="DocTypeGridView_RowUpdating"
            OnRowDeleting="DocTypeGridView_RowDeleting">
            <Columns>
                <asp:TemplateField HeaderText="ประเภทเอกสาร">
                    <ItemTemplate>
                        <asp:Label ID="lbDocType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.type_doc_name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDocType" size="20" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.type_doc_name") %>' CssClass="form-control"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" CancelText="ยกเลิก" EditText="&#xf040; แก้ไข" UpdateText="แก้ไข" HeaderText="ปรับปรุง" ControlStyle-Font-Size="Small" ControlStyle-CssClass="btn btn-outline-warning btn-sm fa" />
                <asp:CommandField ShowDeleteButton="True" HeaderText="ลบ" DeleteText="&#xf014; ลบ" ControlStyle-CssClass="btn btn-outline-danger btn-sm fa" ControlStyle-Font-Size="Small" />
            </Columns>
        </asp:GridView>
    </div>
    <asp:Label ID="lbDocTypeNull" runat="server" Text="Label"></asp:Label>
</asp:Content>
