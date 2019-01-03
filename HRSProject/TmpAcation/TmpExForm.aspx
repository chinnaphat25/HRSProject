<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TmpExForm.aspx.cs" Inherits="HRSProject.TmpAcation.TmpExForm" %>

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
            <h4 class="card-title ">เพิ่มพนักงานที่ลาออก</h4>
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
                    <asp:Label ID="Label2" runat="server" Text="วันที่มีผล : "></asp:Label>
                </div>
                <div class="col-3">
                    <asp:TextBox ID="txtDateSchedule" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-2 text-right">
                    <asp:Label ID="Label4" runat="server" Text="สถานภาพ : "></asp:Label>
                </div>
                <div class="col-2">
                    <asp:DropDownList ID="txtWorkingStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-3 text-right">
                    <asp:Label ID="Label3" runat="server" Text="เนื่องจาก : "></asp:Label>
                </div>
                <div class="col-3">
                    <asp:TextBox ID="txtNote" runat="server" CssClass="form-control"></asp:TextBox>
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
            <h5 class="card-title ">รายการพนักงานที่ลาออก (ยังไม่มีผล)</h5>
        </div>
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView ID="TmpExGridView" runat="server" DataKeyNames="tmp_ex_id"
                    GridLines="None"
                    CssClass="table table-hover table-condensed table-sm"
                    OnRowDataBound="TmpExGridView_RowDataBound"
                    OnRowDeleting="TmpExGridView_RowDeleting"
                    AutoGenerateColumns="False"
                    HeaderStyle-CssClass="thead-light"
                    Font-Size="19px"
                    AllowSorting="true"
                    AllowPaging="true"
                    PageSize="50"
                    OnPageIndexChanging="TmpExGridView_PageIndexChanging"
                    PagerSettings-Mode="NumericFirstLast">
                    <Columns>
                        <asp:TemplateField HeaderText="รหัสบุคคล" ItemStyle-Width="80px">
                            <ItemTemplate>
                                <asp:Label ID="lbempID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.tmp_ex_emp") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ชื่อ-สกุล">
                            <ItemTemplate>
                                <asp:Label ID="lbempName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="สถานภาพ">
                            <ItemTemplate>
                                <asp:Label ID="lbWorkingStatus" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="เนื่องจาก" ItemStyle-Width="80px">
                            <ItemTemplate>
                                <asp:Label ID="lbNote" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.tmp_ex_note") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="วันที่มีผล">
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
            <h5 class="card-title ">ประวัติรายการพนักงานที่ลาออกล่าสุด</h5>
        </div>
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView ID="HisTmpExGridViewGridView" runat="server" DataKeyNames="tmp_ex_id"
                    GridLines="None"
                    CssClass="table table-hover table-condensed table-sm"
                    OnRowDataBound="HisTmpExGridViewGridView_RowDataBound"
                    AutoGenerateColumns="False"
                    HeaderStyle-CssClass="thead-light"
                    Font-Size="19px">
                    <Columns>
                        <asp:TemplateField HeaderText="รหัสบุคคล" ItemStyle-Width="80px">
                            <ItemTemplate>
                                <asp:Label ID="lbempID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.tmp_ex_emp") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ชื่อ-สกุล">
                            <ItemTemplate>
                                <asp:Label ID="lbempName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="สถานภาพ">
                            <ItemTemplate>
                                <asp:Label ID="lbWorkingStatus" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="เนื่องจาก" ItemStyle-Width="80px">
                            <ItemTemplate>
                                <asp:Label ID="lbNote" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.tmp_ex_note") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="วันที่มีผล">
                            <ItemTemplate>
                                <asp:Label ID="lbempChengDate" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ลาออกไปแล้ว/วัน">
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
                <asp:Label ID="LabelHis" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
