<%@ Page Title="ค้นหาประวัติ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="historyForm.aspx.cs" Inherits="HRSProject.SearchHistory.historyForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-row">
        <div class="col-md-3 input-group-lg">
            <asp:TextBox ID="txtSearchIDCard" runat="server" CssClass="form-control" MaxLength="13" placeholder="ใส่รหัสบัตรประจำตัวประชาชน 13 หลัก"></asp:TextBox>
            <div class="input-group-btn">
                <asp:Button ID="btnSearchEmp" runat="server" Text="&#xf002;" CssClass="fa btn" OnClick="btnSearchEmp_Click" />
            </div>
        </div>
    </div>
    <br />

    <div class="card" runat="server" id="resultCard" aria-hidden="True">
        <div class="card-header card-header-warning">
            <h3 class="card-title ">ผลลัพธ์การค้นหา</h3>
        </div>
        <div class="card-body">
            <asp:GridView ID="GridViewEmp" runat="server" DataKeyNames="history_emp_id"
                GridLines="None"
                CssClass="table table-hover table-sm"
                OnRowDataBound="GridViewEmp_RowDataBound"
                AutoGenerateColumns="False"
                HeaderStyle-CssClass="thead-light"
                Font-Size="19px">
                <Columns>
                    <asp:TemplateField HeaderText="สรรพนาม" ItemStyle-Width="50px">
                        <ItemTemplate>
                            <asp:Label ID="lbempProfix" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.profix_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ชื่อ-สกุล">
                        <ItemTemplate>
                            <asp:Label ID="lbempName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.emp_name")+"  "+DataBinder.Eval(Container, "DataItem.emp_lname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="สถานะ">
                        <ItemTemplate>
                            <asp:Label ID="lbempStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.status_working_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="วันที่">
                        <ItemTemplate>
                            <asp:Label ID="lbempStatusDate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="หมายเหตุ">
                        <ItemTemplate>
                            <asp:Label ID="lbempStatusNote" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.history_note") %>'>></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ดูประวัติ">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnView" runat="server" OnCommand="btnView_Command"><i style="font-size:24px" class="fa">&#xf1da;</i></asp:LinkButton>
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
