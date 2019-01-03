<%@ Page Title="ปรับปรุงข้อมูลอัตรากำลัง" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="editManpower.aspx.cs" Inherits="HRSProject.Manpower.editManpower" %>

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
        <div class="col-md-1">
            <asp:DropDownList ID="txtYear" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-md-2">
            <asp:DropDownList ID="txtCpoint" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-md-1">
            <asp:Button ID="btnSearch" runat="server" Text="&#xf002; ค้นหา" Font-Size="Medium" CssClass="btn btn-success btn-sm align-items-end fa" OnClick="btnSearch_Click" />
        </div>
    </div>
    <hr />

    <div class="form-row" runat="server" id="showData">
        <div class="col-md-5">
            <div class="card">
                <div class="card-header card-header-warning">
                    <asp:Label ID="txtHeard" runat="server" Text="" Font-Size="XX-Large"></asp:Label>
                </div>
                <div class="card-body table-responsive">
                    <asp:GridView ID="ManPowerGridView" runat="server"
                        DataKeyNames="manpower_id"
                        AutoGenerateColumns="False"
                        CssClass="table table-hover table-sm"
                        OnRowDataBound="ManPowerGridView_RowDataBound"
                        OnRowEditing="ManPowerGridView_RowEditing"
                        OnRowCancelingEdit="ManPowerGridView_RowCancelingEdit"
                        OnRowUpdating="ManPowerGridView_RowUpdating"
                        OnRowDeleting="ManPowerGridView_RowDeleting"
                        GridLines="None">
                        <Columns>
                            <asp:TemplateField HeaderText="ตำแหน่ง">
                                <ItemTemplate>
                                    <asp:Label ID="lbAff" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.affi_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="อัตราเต็ม">
                                <ItemTemplate>
                                    <asp:Label ID="lbFull" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.manpower_full") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFullEdit" size="10" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.manpower_full") %>' CssClass="form-control"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" CancelText="ยกเลิก" EditText="&#xf040; แก้ไข" UpdateText="แก้ไข" HeaderText="ปรับปรุง" ControlStyle-Font-Size="Small" ControlStyle-CssClass="btn btn-outline-warning btn-sm fa" />
                            <asp:CommandField ShowDeleteButton="True" HeaderText="ลบ" DeleteText="&#xf014; ลบ" ControlStyle-CssClass="btn btn-outline-danger btn-sm fa" ControlStyle-Font-Size="Small" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="card-footer">
                    <div class="stats">
                        <asp:Label ID="lbManPowerNull" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-5 card" id="diAdd" runat="server">
            <div class="form-row card-body">
                <div class="col-md-5">
                    หน่วย
                    <asp:DropDownList ID="txtAffAdd" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:DropDownList>
                </div>
                <div class="col-md-3">
                    อัตราเต็ม
                    <asp:TextBox ID="txtFullAdd" size="10" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <br />
                    <asp:Button ID="btnManPowerAdd" runat="server" Text="&#xf067; เพิ่ม" Font-Size="Medium" CssClass="btn btn-success btn-sm align-items-end fa" OnClick="btnManPowerAdd_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
