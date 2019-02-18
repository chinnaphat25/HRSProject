<%@ Page Title="รายการพนักงานใหม่" Language="C#" MasterPageFile="/Site.Master" AutoEventWireup="true" CodeBehind="GuestNewListForm.aspx.cs" Inherits="HRSProject.Guest.GuestNewListForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card" style="z-index: 0;">
        <div class="card-header card-header-warning">
            <h3 class="card-title">รายการพนักงานใหม่</h3>
        </div>
        <div class="card-body table-responsive">
            <asp:HiddenField ID="txtGuest_id" runat="server" />
            <div class="row">
                <div class="col-md-2 text-right">
                    <asp:Label ID="Label1" runat="server" Text="เรื่อง : "></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtTitle" CssClass="form-control" runat="server">ส่งรายชื่อเจ้าหน้าที่ที่มารายงานตัว</asp:TextBox>
                </div>
                <div class="col-md-2 text-right">
                    <asp:Label ID="Label5" runat="server" Text="เรียน : "></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtTo" CssClass="form-control" runat="server">ผอท. ผ่าน รอท.2</asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-2 text-right">
                    <asp:Label ID="Label2" runat="server" Text="อ้างถึงบันทึก : "></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtRefer" CssClass="form-control" runat="server" ToolTip="เช่น กท./ส./1"></asp:TextBox>
                </div>
                <div class="col-md-2 text-right">
                    <asp:Label ID="Label4" runat="server" Text="ลงวันที่ : "></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtReferDate" CssClass="form-control datepicker" runat="server"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-2 text-right">
                    <asp:Label ID="Label12" runat="server" Text="บันทึกอ้างอิงเรื่อง : "></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtReferTitle" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2 text-right">
                    <asp:Label ID="Label3" runat="server" Text="วันที่รายงานตัว : "></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtOfferDate" CssClass="form-control datepicker" runat="server"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-2">&nbsp</div>
                <div class="col-md">
                    <asp:LinkButton ID="btnAddTitleGuest" CssClass="fa btn btn-success btn-sm" Font-Size="Medium" runat="server" OnClick="btnAddTitleGuest_Click">&#xf0c7; บันทึก</asp:LinkButton>
                    <asp:LinkButton ID="btnReport" CssClass="fa btn btn-info btn-sm" Font-Size="Medium" runat="server" OnClick="btnReport_Click">&#xf02f; บันทึกข้อความ</asp:LinkButton>
                    <asp:LinkButton ID="btnReportCopy" CssClass="fa btn btn-danger btn-sm" Font-Size="Medium" runat="server" OnClick="btnReportCopy_Click">&#xf02f; บันทึกข้อความ(สำเนา)</asp:LinkButton>
                </div>
            </div>
            <div class="row" runat="server" id="DivAddGuest">
                <div class="col-md-2 text-right">
                    <h3>เพิ่มรายชื่อ : </h3>
                </div>
                <div class="card border border-info col-md-7">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-2 text-right">
                                <asp:Label ID="Label6" runat="server" Text="สรรพนาม : "></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="txtProfix" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-2 text-right">
                                <asp:Label ID="Label7" runat="server" Text="ชื่อ : "></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2 text-right">
                                <asp:Label ID="Label8" runat="server" Text="สกุล : "></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtLname" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-2 text-right">
                                <asp:Label ID="Label9" runat="server" Text="ตำแหน่ง : "></asp:Label>
                            </div>
                            <div class="col-md-5">
                                <asp:DropDownList ID="txtPos" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-md-2 text-right">
                                <asp:Label ID="Label10" runat="server" Text="อัตราค่าจ้าง : "></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtSalary" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-2 text-right">
                                <asp:Label ID="Label11" runat="server" Text="ปฏิบัติงานที่ : "></asp:Label>
                            </div>
                            <div class="col-md-5">
                                <asp:DropDownList ID="txtCpoint" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-4 text-right">
                                <asp:LinkButton ID="btnAddGuest" runat="server" CssClass="fa btn btn-success btn-sm" Font-Size="Medium" OnClick="btnAddGuest_Click">&#xf0fe; เพิ่ม</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <asp:GridView ID="GridViewGuestList" runat="server" DataKeyNames="guest_list_id"
                GridLines="None"
                CssClass="table table-hover table-sm"
                OnRowDataBound="GridViewGuestList_RowDataBound"
                AutoGenerateColumns="False"
                HeaderStyle-CssClass="thead-light"
                Font-Size="19px" OnRowCommand="GridViewGuestList_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="ลำดับ" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lbGuestNum" runat="server" Text=" <%# Container.DataItemIndex + 1 %> "></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ชื่อ-สกุล">
                        <ItemTemplate>
                            <asp:Label ID="lbGueatName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ตำแหน่ง">
                        <ItemTemplate>
                            <asp:Label ID="lbGuestPos" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ปฏิบัติงานที่" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lbGuestCpoint" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="รับรายงานตัว/เพิ่มประวัติ" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnAddEmp" runat="server" Font-Size="Medium" CssClass="btn btn-warning fa btn-sm" Text="&#xf067;" />
                            <asp:HyperLink Target="_blank" ID="LabelAddEmp" runat="server" CssClass="badge badge-success" Font-Size="Large" Text=""></asp:HyperLink>
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
    <!-- -------------------------------------------------------------------------- -->
    <div class="modal fade" id="ModalCheckEmp" tabindex="-1" role="dialog" aria-labelledby="ModalChack" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalChack">รับรายงานตัวพนักงานใหม่</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4">
                            <label class="col-form-label">ชื่อ-สกุล </label>
                            <asp:Label ID="lbName" CssClass="form-control" runat="server"></asp:Label>
                            <asp:HiddenField ID="hdProfix" runat="server" />
                            <asp:HiddenField ID="hdName" runat="server" />
                            <asp:HiddenField ID="hdLname" runat="server" />
                            <asp:HiddenField ID="hdPos" runat="server" />
                            <asp:HiddenField ID="hdCpoint" runat="server" />
                            <asp:HiddenField ID="hdGuest_list_id" runat="server" />
                        </div>
                        <div class="col-md">
                            <label class="col-form-label">รหัสบัตรประจำตัวประชาชน </label>
                            <p class="text-danger" style="font-size: medium;">***รหัสบัตรประจำตัวประชาชน 13 หลัก ไม่ต้องใส่ขีด</p>
                            <asp:TextBox ID="txtNewIDCard" runat="server" CssClass="form-control" MaxLength="13"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="col-form-label">วันเดือนปีเกิด </label>
                            <asp:TextBox ID="txtBirdthDay" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <label class="col-form-label">สัญชาติ </label>
                            <asp:TextBox ID="txtNationality" runat="server" Text="ไทย" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="col-form-label">เชื้อชาติ </label>
                            <asp:TextBox ID="txtRace" runat="server" Text="ไทย" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="col-form-label">ศาสนา </label>
                            <asp:TextBox ID="txtReligion" runat="server" Text="พุทธ" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="col-form-label">เลขที่บัญชีธนาคารกรุงไทย </label>
                            <asp:TextBox ID="txtBookBank" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            <label class="col-form-label">สถานะภาพ </label>
                            <div class="radio radio-warning">
                                <asp:RadioButtonList ID="txtStatusRd" CssClass="radioButtonList" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="โสด" Value="โสด" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="สมรส" Value="สมรส"></asp:ListItem>
                                    <asp:ListItem Text="สมรส (ไม่จดทะเบียน)" Value="สมรส (ไม่จดทะเบียน)"></asp:ListItem>
                                    <asp:ListItem Text="หย่า" Value="หย่า"></asp:ListItem>
                                    <asp:ListItem Text="หม้าย" Value="หม้าย"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="col-form-label">ตำแหน่ง </label>
                            <asp:Label ID="lbPos" CssClass="form-control" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <label class="col-form-label">หน่วย </label>
                            <asp:DropDownList ID="txtAff" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label class="col-form-label">สถานที่ปฏิบัติงาน </label>
                            <asp:Label ID="lbCpoint" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">ยกเลิก</button>
                    <asp:Button ID="btnCheckEmp" runat="server" Text="ยืนยัน" OnClick="btnCheckEmp_Click" CssClass="btn btn-warning" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">   
        $(function () {
            $(".datepicker").datepicker($.datepicker.regional["th"]);
            if ($("#<%=txtBirdthDay.ClientID%>").val() == "") {
                $("#<%=txtBirdthDay.ClientID%>").datepicker("setDate", new Date());
            }
            //$(".datepicker").datepicker("setDate", new Date());
        });

        $(function () {
        <%
        if (guest_to_emp > 0)
        {
        %>
            $("#ModalCheckEmp").modal('show');
        <% } %>
        });
    </script>
</asp:Content>
