<%@ Page Title="ประวัติการลา" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="empLeaveHistoryForm.aspx.cs" Inherits="HRSProject.LeaveEmp.empLeaveHistoryForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card" aria-hidden="True">
        <div class="card-header card-header-warning">
            <h3 class="card-title ">สิทธิวันลาพนักงาน</h3>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-md-2 text-right">
                    ประเภทพนักงาน : 
                </div>
                <div class="col-md">
                    <asp:Label ID="lbTypeEmp" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 text-right">
                    พนักงาน : 
                </div>
                <div class="col-md">
                    <asp:Label ID="lbEmpName" CssClass="text-left" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-md-2 text-right">
                    ครบ 6 เดือน : 
                </div>
                <div class="col-md">
                    <asp:Label ID="lbExp6Month" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 text-right">
                    ตำแหน่ง : 
                </div>
                <div class="col-md">
                    <asp:Label ID="lbPos" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-md-2 text-right">
                    ครบ 1 ปี : 
                </div>
                <div class="col-md">
                    <asp:Label ID="lbExp1Year" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 text-right">
                    วันเริ่มงาน : 
                </div>
                <div class="col-md">
                    <asp:Label ID="lbStartDate" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-md-2 text-right">
                    ปีงบประมาณที่เข้าทำงาน : 
                </div>
                <div class="col-md">
                    <asp:Label ID="lbYear" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md text-left">
                    <h3>สิทธิวันลาพนักงาน</h3>
                </div>
            </div>
            <div class="row">
                <div class="col-md text-right">
                    ลาป่วย : 
                </div>
                <div class="col-md">
                    <asp:Label ID="lbSick" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-md-2 text-right">
                    ลาพักผ่อน : 
                </div>
                <div class="col-md">
                    <asp:Label ID="lbRelax" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-md-2 text-right">
                </div>
                <div class="col-md">
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 text-right">
                    ลาคลอดบุตร : 
                </div>
                <div class="col-md">
                    <asp:Label ID="lbMaternity" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-md-2 text-right">
                    ลาอุปสมบท/พิธีฮัจย์ : 
                </div>
                <div class="col-md">
                    <asp:Label ID="lbOrdain" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-md-2 text-right">
                    ตรวจเลือก/เตรียมพล : 
                </div>
                <div class="col-md">
                    <asp:Label ID="lbMilitary" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="card-footer">
                <div class="stats">
                    <asp:Label ID="LaGridViewData" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="Button1" runat="server" Text="&#xf067; เพิ่มประวัติการลางาน" Font-Size="Medium" CssClass="btn btn-success fa btn-sm" data-toggle="modal" data-target="#ModalAddLeave" OnClientClick="return false;" />
    <div class="card" aria-hidden="True">
        <div class="card-header card-header-warning">
            <h3 class="card-title ">ประวัติการลางาน</h3>
        </div>
        <div class="card-body">
            <asp:GridView ID="GridLeaveEmp" runat="server" DataKeyNames="emp_leave_id"
                GridLines="None"
                CssClass="table table-hover table-sm"
                OnRowDataBound="GridLeaveEmp_RowDataBound"
                AutoGenerateColumns="False"
                HeaderStyle-CssClass="thead-light"
                Font-Size="19px">
                <Columns>
                    <asp:TemplateField HeaderText="ครั้งที่" ItemStyle-Width="50px">
                        <ItemTemplate>
                            <asp:Label ID="lbLeaveRow" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="เริ่มวันที่">
                        <ItemTemplate>
                            <asp:Label ID="lbLeaveStart" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ถึงวันที่">
                        <ItemTemplate>
                            <asp:Label ID="lbLeaveEnd" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ป่วย (วัน)" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lbempName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.emp_leave_sick")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="พักผ่อน (วัน)" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lbempStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.emp_leave_relax") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="คลอดบุตร (วัน)" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lbempStatusDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.emp_leave_maternity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ใบรับรองแพทย์" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lbLeaveMedicalCertificate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ตัดค่าจ้าง" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lbLeaveDeductionWages" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.emp_leave_deduction_wages") %>'>></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="หมายเหตุ">
                        <ItemTemplate>
                            <asp:Label ID="lbempStatusNote1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.emp_leave_note") %>'>></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>
        <div class="card-footer">
            <div class="stats">
                <asp:Label ID="Labeltxt" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <!-- -------------------------------------------------------------------------- -->
    <div class="modal fade" id="ModalAddLeave" tabindex="-1" role="dialog" aria-labelledby="ModalAddLeave" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalChack">เพิ่มประวัติการลางาน</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md">
                            <label class="col-form-label">ประเภทการลา </label>
                            <div class="radio radio-warning">
                                <asp:RadioButtonList ID="rdTypeLeave" CssClass="radioButtonList" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="ลาป่วย" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="ลาพักผ่อน" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="ลาคลอดบุตร" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="ลาตรวจเลือก/เตรียมพล" Value="4"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="checkbox checkbox-warning">
                                <asp:CheckBox ID="ckTypeLeave" Text="ลาอุปสมบท/พิธีฮัจย์" runat="server" />
                            </div>
                        </div>
                    </div>
                    <label class="col-form-label">ตั้งแต่-ถึง </label>
                    <div class="form-row">
                        <asp:TextBox ID="txtStartLeave" runat="server" CssClass="form-control"></asp:TextBox>&nbsp ถึง &nbsp
                            <asp:TextBox ID="txtEndLeave" runat="server" CssClass="form-control"></asp:TextBox>
                        <label class="col-form-label">รวม : </label>
                        <asp:TextBox ID="txtTotalDay" runat="server" CssClass="form-control text-center" Width="50px"></asp:TextBox>
                        <label class="col-form-label">วัน</label>
                    </div>
                    <div class="form-row">
                        <div class="col-md">
                            <div class="form-row">
                                <label class="col-form-label">หมายเหตุ : </label>
                                <asp:TextBox ID="txtNote" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            <label class="col-form-label">ตัดค่าจ้าง </label>
                            <div class="radio radio-warning">
                                <asp:RadioButtonList ID="rbDeductionWages" runat="server">
                                    <asp:ListItem Text="ไม่ตัดค่าจ้าง" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="ตัดค่าจ้างตามจำนวนวันที่ลา" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="ตัดค่าจ้างระบุจำนวนวัน" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                                <div class="form-row">
                                    ระบุจำนวนวัน &nbsp<asp:TextBox ID="txtDeductionWages" runat="server" CssClass="form-control text-center" Width="50px"></asp:TextBox>&nbsp วัน
                                </div>
                            </div>
                        </div>
                        <div class="col-md">
                            <label class="col-form-label">ใบรับรองแพทย์</label>
                            <div class="form-row">
                                <div class="radio radio-warning">
                                    <asp:RadioButtonList ID="rbMedicalCertificate" runat="server">
                                        <asp:ListItem Text="มี" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="ไม่มี" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal">ยกเลิก</button>
                <asp:Button ID="btnSaveLeave" runat="server" Text="ยืนยัน" CssClass="btn btn-warning" OnClick="btnSaveLeave_Click" />
            </div>
        </div>
    </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $(".datepicker").datepicker($.datepicker.regional["th"]);
            //$(".hasDatepicker").attr('readonly', true);
            //$(".datepicker").datepicker("setDate", new Date());

            $("#<%=txtStartLeave.ClientID.ToString()%>").datepicker($.datepicker.regional["th"]); // Set ภาษาที่เรานิยามไว้ด้านบน
            $("#<%=txtEndLeave.ClientID.ToString()%>").datepicker($.datepicker.regional["th"]);

            $("#<%=txtStartLeave.ClientID.ToString()%>").datepicker("setDate", new Date());
            $("#<%=txtEndLeave.ClientID.ToString()%>").datepicker("setDate", new Date());

            //คำนวนจำนวนวันลา
            $("#<%=txtTotalDay.ClientID.ToString()%>").val(getDateDiff($("#<%=txtStartLeave.ClientID.ToString()%>").val(), $("#<%=txtEndLeave.ClientID.ToString()%>").val()));

            $("#<%=txtStartLeave.ClientID.ToString()%>").on("change", function () {
                $("#<%=txtTotalDay.ClientID.ToString()%>").val(getDateDiff($("#<%=txtStartLeave.ClientID.ToString()%>").val(), $("#<%=txtEndLeave.ClientID.ToString()%>").val()));
            });
            $("#<%=txtEndLeave.ClientID.ToString()%>").on("change", function () {
                $("#<%=txtTotalDay.ClientID.ToString()%>").val(getDateDiff($("#<%=txtStartLeave.ClientID.ToString()%>").val(), $("#<%=txtEndLeave.ClientID.ToString()%>").val()));
            });
        });

        /*function saveLeave() {
            
        }*/
    </script>
    <script type="text/javascript">
        window.onload = function () {
            var rbl = document.getElementById("<%=rbDeductionWages.ClientID %>");
            var radio = rbl.getElementsByTagName("INPUT");
            for (var i = 0; i < radio.length; i++) {
                radio[i].onchange = function () {
                    var radio = this;
                    var label = radio.parentNode.getElementsByTagName("LABEL")[0];
                    if (radio.value == 2) {
                        $("#<%=txtDeductionWages.ClientID.ToString()%>").removeAttr("ReadOnly");
                        $("#<%=txtDeductionWages.ClientID.ToString()%>").val($("#<%=txtTotalDay.ClientID.ToString()%>").val());
                    }
                    else {
                        $("#<%=txtDeductionWages.ClientID.ToString()%>").attr("ReadOnly", "true");
                        //$("#<%=txtDeductionWages.ClientID.ToString()%>").val("");
                    }
                };
            }
        };
    </script>
</asp:Content>
