<%@ Page Title="รายงานตัวพนักงานใหม่" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GuestDefault.aspx.cs" Inherits="HRSProject.Guest.GuestDefault" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="card" style="z-index: 0;">
        <div class="card-header card-header-warning">
            <h3 class="card-title">รายงานตัวพนักงานใหม่</h3>
        </div>
        <div class="card-body table-responsive">
            <asp:LinkButton ID="btnAddGuest" CssClass="btn btn-success btn-sm fa" Font-Size="Large" OnClick="btnAddGuest_Click" runat="server">&#xf067; เพิ่มรายการใหม่</asp:LinkButton>
            <br />
            <asp:GridView ID="GridViewGuest" runat="server" DataKeyNames="guest_id"
                GridLines="None"
                CssClass="table table-hover table-sm"
                OnRowDataBound="GridViewGuest_RowDataBound"
                AutoGenerateColumns="False"
                HeaderStyle-CssClass="thead-light"
                Font-Size="19px" OnRowCommand="GridViewGuest_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="เรื่อง">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbGuestTitle" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.guest_title")%>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="อ้างถึงบันทึก" >
                        <ItemTemplate>
                            <asp:LinkButton ID="lbGueatRefer" runat="server"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="วันที่รายงานตัว" >
                        <ItemTemplate>
                            <asp:LinkButton ID="lbGuestDate" runat="server"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="จำนวนพนักงาน" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lbGuestAmount" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>
        <div class="card-footer">
            <div class="stats">
                <asp:Label ID="LaGridViewData" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
