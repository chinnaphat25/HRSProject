<%@ Page Title="ประวัติพนักงาน" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="empViwe.aspx.cs" Inherits="HRSProject.Profile.empViwe" EnableEventValidation="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <% if (alert != "")
        { %>
    <script type="text/javascript">
        $(function () {
            demo.showNotification('top', 'center', '<%=icon%>','<%=alertType%>', '<%=alert%>');
        });
    </script>
    <% } %>
    <div class="form-row" id="DivAdd" runat="server">
        <div class="col-md">
            <asp:LinkButton ID="btnAddEmp" runat="server" Font-Size="Medium" CssClass="btn btn-success fa btn-sm" data-toggle="modal" data-target="#ModalCheckEmp" OnClientClick="return false;" Text="&#xf067; เพิ่มพนักงานใหม่" />
            <!--<asp:LinkButton ID="btnUpdateEmp" runat="server" Text="&#xf067; พนักงานเดิม(สมัครใหม่)" Font-Size="Medium" CssClass="btn btn-info fa btn-sm" data-toggle="modal" data-target="#ModalOldEmp" OnClientClick="return false;" />-->
            <asp:LinkButton ID="btnShEmp" runat="server" Text="ค้นหาประวัติ (เข้า/ออก)" Font-Size="Medium" CssClass="btn btn-warning fa btn-sm" OnClick="btnShEmp_Click" />
            <asp:LinkButton ID="btnPrintListName" runat="server" Text="พิมพ์รายชื่อ" Font-Size="Medium" CssClass="btn btn-dark fa btn-sm" OnClick="btnPrintListName_Click" />
        </div>
        <div class="col-md">
        </div>
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="card" style="z-index: 0;" id="DivSearch" runat="server">
                <div class="card-header card-header-warning">
                    <h4 class="card-title ">ค้นหา</h4>
                </div>
                <div class="card-body">
                    <br />
                    <div class="row">
                        <div class="col-md-2 text-right">รหัสบุคคล : </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtSearchId" runat="server" CssClass="form-control" placeholder="รหัสบุคคล"></asp:TextBox>
                        </div>
                        <div class="col-md-1 text-right">ชื่อ-สกุล : </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtSearchName" runat="server" CssClass="form-control" placeholder="ชื่อ-สกุล"></asp:TextBox>
                        </div>
                        <div class="col-md-1 text-right">ด่านฯ : </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="txtSearchCpoint" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-1 text-right">ตำแหน่ง : </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="txtSearchPos" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-1 text-right">หน่วย : </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="txtSearchAffi" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-1 text-right">ประเภท : </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="txtSearchType" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md text-center">
                            <asp:Button ID="btnSearchEmp" runat="server" Text="&#xf002; ค้นหา" CssClass="fa btn btn-info" Font-Size="0.75em" OnClick="btnSearchEmp_Click" />
                            <asp:Button ID="btnSearchClear" runat="server" Text="&#xf021; ล้างค่า" CssClass="fa btn" Font-Size="0.75em" OnClick="btnSearchClear_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="card" id="DivEmp" runat="server" style="z-index: 0;">
                <div class="card-header card-header-warning">
                    <h3 class="card-title ">ประวัติส่วนพนักงาน</h3>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <asp:GridView ID="GridViewEmp" runat="server" DataKeyNames="emp_id"
                            GridLines="None"
                            CssClass="table table-hover table-condensed table-sm"
                            OnRowDataBound="GridViewEmp_RowDataBound"
                            AutoGenerateColumns="False"
                            HeaderStyle-CssClass="thead-light"
                            Font-Size="19px"
                            AllowSorting="true"
                            AllowPaging="true"
                            PageSize="100"
                            OnPageIndexChanging="GridViewEmp_PageIndexChanging"
                            PagerSettings-Mode="NumericFirstLast" OnSorting="GridViewEmp_Sorting">
                            <Columns>
                                <asp:TemplateField HeaderText="รหัสบุคคล" ItemStyle-Width="80px" SortExpression="emp_id">
                                    <ItemTemplate>
                                        <asp:Label ID="lbempID" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อ-สกุล" SortExpression="emp_name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbempName" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="สังกัดด่านฯ" SortExpression="cpoint_name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbempCpoint" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ตำแหน่ง" SortExpression="pos_name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbempPos" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="หน่วย" SortExpression="affi_name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbempAff" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ประเภทพนักงาน" SortExpression="type_emp_name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbempType" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="อายุงานแรกเข้า">
                                    <ItemTemplate>
                                        <asp:Label ID="lbempAgeWork" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="อายุงานตำแหน่งปัจจุบัน">
                                    <ItemTemplate>
                                        <asp:Label ID="lbempAgeWorkNow" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ดู/แก้ไข">
                                    <ItemTemplate>
                                        <asp:Button ID="txtEmpViwe" runat="server" Text="&#xf0c6;" Font-Size="xx-small" CssClass="btn btn-outline-warning btn-sm fa" OnCommand="txtEmpViwe_Command" />
                                    </ItemTemplate>
                                </asp:TemplateField>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- -------------------------------------------------------------------------- -->
    <div class="modal fade" id="ModalOldEmp" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">+พนักงานเดิม(สมัครใหม่)</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md">
                            <label class="col-form-label">เลขที่บัตรประจำตัวประชาชน 13 หลัก </label>
                            <asp:TextBox ID="txtIdcard" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="col-form-label">วันที่เข้าทำงาน </label>
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-10">
                            <label class="col-form-label">ตำแหน่ง </label>
                            <asp:DropDownList ID="txtPos" runat="server" CssClass="form-control datepicker"></asp:DropDownList>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-5">
                            <label class="col-form-label">หน่วย </label>
                            <asp:DropDownList ID="txtAffi" runat="server" CssClass="form-control datepicker"></asp:DropDownList>
                        </div>
                        <div class="col-md">
                            <label class="col-form-label">ด่านฯ </label>
                            <asp:DropDownList ID="txtCpoint" runat="server" CssClass="form-control datepicker"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">ยกเลิก</button>
                    <asp:Button ID="btnAddOldEmp" runat="server" Text="ถัดไป" CssClass="btn btn-warning" OnClick="btnAddOldEmp_Click" OnClientClick="return confirmAddOldEmp(this);" />
                </div>
            </div>
        </div>
    </div>
    <!-- -------------------------------------------------------------------------- -->
    <div class="modal fade" id="ModalCheckEmp" tabindex="-1" role="dialog" aria-labelledby="ModalChack" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalChack">ตรวจสอบ</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <%if (dimsgErr.Text != "")
                        { %>
                    <div class="alert alert-danger alert-dismissible fade show" role="alert">
                        <asp:Label ID="dimsgErr" runat="server" Text=""></asp:Label>
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <%} %>
                    <div class="row">
                        <div class="col-md">
                            <label class="col-form-label">รหัสบัตรประจำตัวประชาชน </label>
                            <p class="text-danger" style="font-size: medium;">***รหัสบัตรประจำตัวประชาชน 13 หลัก ไม่ต้องใส่ขีด</p>
                            <asp:TextBox ID="txtNewIDCard" runat="server" CssClass="form-control" MaxLength="13"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">ยกเลิก</button>
                    <asp:Button ID="btnCheckEmp" runat="server" Text="ยืนยัน" CssClass="btn btn-warning" OnClick="btnCheckEmp_Click" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">   
        $(function () {
            $(".datepicker").datepicker($.datepicker.regional["th"]);
            $("#<%=txtStartDate.ClientID.ToString()%>").datepicker($.datepicker.regional["th"]);
            $("#<%=txtStartDate.ClientID.ToString()%>").datepicker("setDate", new Date());
        });
    </script>
</asp:Content>
