<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TmpCpointForm.aspx.cs" Inherits="HRSProject.TmpAcation.TmpCpointForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% if (alert != "")
        { %>
    <script type="text/javascript">
        $(function () {
            demo.showNotification('top', 'center', '<%=icon%>','<%=alertType%>', '<%=alert%>');
        });
    </script>
    <% } %>
    <div class="card" style="z-index: 0">
        <div class="card-header card-header-warning">
            <h3 class="card-title ">เพิ่มพนักงานที่ย้ายด่านฯ</h3>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-2 text-right">
                    <asp:Label ID="Label1" runat="server" Text="ชื่อ-สกุล : "></asp:Label>
                </div>
                <div class="col-3">
                    <asp:DropDownList ID="txtEmp" runat="server" CssClass="combobox form-control"></asp:DropDownList>
                </div>
                <div class="col-2 text-right">
                    <asp:Label ID="Label2" runat="server" Text="ย้ายไปที่ด่านฯ : "></asp:Label>
                </div>
                <div class="col-4">
                    <asp:DropDownList ID="txtCpoint" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-2 text-right">
                    <asp:Label ID="Label3" runat="server" Text="วันที่ต้องย้านด่านฯ : "></asp:Label>
                </div>
                <div class="col-3">
                    <asp:TextBox ID="txtDateSchedule" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row text-center">
                <div class="col">
                    <asp:Button ID="btnSave" runat="server" Text="บันทึก" CssClass="btn btn-success" OnClick="btnSave_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="card" style="z-index: 0">
        <div class="card-header card-header-warning">
            <h3 class="card-title ">รายการพนักงานที่รอย้ายด่านฯ</h3>
        </div>
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView ID="TmpCopintGridView" runat="server" DataKeyNames="tmp_cpoint_id"
                    GridLines="None"
                    CssClass="table table-hover table-condensed table-sm"
                    OnRowDataBound="TmpCopintGridView_RowDataBound"
                    OnRowDeleting="TmpCopintGridView_RowDeleting"
                    AutoGenerateColumns="False"
                    HeaderStyle-CssClass="thead-light"
                    Font-Size="19px"
                    AllowSorting="true"
                    AllowPaging="true"
                    PageSize="50"
                    OnPageIndexChanging="TmpCopintGridView_PageIndexChanging"
                    PagerSettings-Mode="NumericFirstLast">
                    <Columns>
                        <asp:TemplateField HeaderText="รหัสบุคคล" ItemStyle-Width="80px">
                            <ItemTemplate>
                                <asp:Label ID="lbempID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.tmp_cpoint_emp_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ชื่อ-สกุล">
                            <ItemTemplate>
                                <asp:Label ID="lbempName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ด่านฯ ปัจจุบัน">
                            <ItemTemplate>
                                <asp:Label ID="lbempPos" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cpoint_name2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ด่านฯ ที่รอย้าย">
                            <ItemTemplate>
                                <asp:Label ID="lbempPosNew" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cpoint_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="วันที่ย้ายด่านฯ">
                            <ItemTemplate>
                                <asp:Label ID="lbempChengDate" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="เหลืออีก/วัน">
                            <ItemTemplate>
                                <asp:Label ID="lbCountdown" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" HeaderText="ลบ" DeleteText="&#xf014; ลบ" ControlStyle-CssClass="btn btn-outline-danger btn-sm fa" ControlStyle-Font-Size="Small" />
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                </asp:GridView>
            </div>
        </div>
        <div class="card-footer">
            <div class="stats">
                <asp:Label ID="LaGridViewData" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <div class="card" style="z-index: 0">
        <div class="card-header card-header-success">
            <h3 class="card-title ">ประวัติรายการพนักงานที่ย้ายด่านฯ (ล่าสุด)</h3>
        </div>
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView ID="TmpCopintHisGridView" runat="server" DataKeyNames="tmp_cpoint_id"
                    GridLines="None"
                    CssClass="table table-hover table-condensed table-sm"
                    OnRowDataBound="TmpCopintGridView_RowDataBound"
                    OnRowDeleting="TmpCopintGridView_RowDeleting"
                    AutoGenerateColumns="False"
                    HeaderStyle-CssClass="thead-light"
                    Font-Size="19px"
                    AllowSorting="true"
                    AllowPaging="true"
                    PageSize="21"
                    OnPageIndexChanging="TmpCopintGridView_PageIndexChanging"
                    PagerSettings-Mode="NumericFirstLast">
                    <Columns>
                        <asp:TemplateField HeaderText="รหัสบุคคล" ItemStyle-Width="80px">
                            <ItemTemplate>
                                <asp:Label ID="lbempID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.tmp_cpoint_emp_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ชื่อ-สกุล">
                            <ItemTemplate>
                                <asp:Label ID="lbempName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ด่านฯ เดิม">
                            <ItemTemplate>
                                <asp:Label ID="lbempPos" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cpoint_name2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ด่านฯ ย้ายไป">
                            <ItemTemplate>
                                <asp:Label ID="lbempPosNew" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cpoint_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="วันที่ย้ายด่านฯ">
                            <ItemTemplate>
                                <asp:Label ID="lbempChengDate" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ผ่ามาแล้ว/วัน">
                            <ItemTemplate>
                                <asp:Label ID="lbCountdown" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                </asp:GridView>
            </div>
        </div>
        <div class="card-footer">
            <div class="stats">
                <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
