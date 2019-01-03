<%@ Page Title="ข้อมูลวันลาพนักงาน" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="empLeaveForm.aspx.cs" Inherits="HRSProject.LeaveEmp.empLeaveForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card" style="z-index: 0;">
        <div class="card-header card-header-warning">
            <h3 class="card-title">รายงานการลางานของพนักงาน</h3>
        </div>
        <div class="card-body table-responsive">
            <div class="row">
                <div class="col-md-2 text-right">
                    <asp:Label ID="Label1" runat="server">ด่านฯ : </asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="txtCpoint" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 text-right">
                    <asp:Label ID="Label3" runat="server" Text="Label">ปีงบประมาณ : </asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="txtYear" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 text-right">
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:LinkButton ID="btnShowReport" runat="server" Text="&#xf201; แสดงรายงาน" Font-Size="Medium" CssClass="btn btn-info fa btn-sm" OnClick="btnShowReport_Click" />
                </div>
            </div>

        </div>
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="card" style="z-index: 0;">
                <div class="card-header card-header-warning">
                    <h3 class="card-title">ค้นหา</h3>
                </div>
                <div class="card-body table-responsive">
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
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-1 text-right">ด่านฯ : </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="txtSearchCpoint" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-1 text-right">ตำแหน่ง : </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="txtSearchPos" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-1 text-right">หน่วย : </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="txtSearchAffi" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md text-center">
                            <asp:Button ID="btnSearchEmp" runat="server" Text="&#xf002; ค้นหา" CssClass="fa btn btn-info" Font-Size="0.75em" OnClick="btnSearchEmp_Click" />
                            <asp:Button ID="btnSearchClear" runat="server" Text="&#xf021; ล้างค่า" CssClass="fa btn" Font-Size="0.75em" OnClick="btnSearchClear_Click" />
                        </div>
                    </div>
                    <br />
                    <p class="text-danger">***ผลลัพธ์การค้นหาจะแสดงสูงสุดแค่ 20 แถว</p>
                    <div class="card" runat="server" id="resultCard" aria-hidden="True" style="z-index: 0;">
                        <div class="card-header card-header-warning">
                            <h3 class="card-title ">ผลลัพธ์การค้นหา</h3>
                        </div>
                        <div class="card-body">
                            <asp:GridView ID="GridViewEmp" runat="server" DataKeyNames="emp_id"
                                GridLines="None"
                                CssClass="table table-hover table-sm"
                                OnRowDataBound="GridViewEmp_RowDataBound"
                                AutoGenerateColumns="False"
                                HeaderStyle-CssClass="thead-light"
                                Font-Size="19px">
                                <Columns>
                                    <asp:TemplateField HeaderText="รหัสบุคคล" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbempID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.emp_id")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ชื่อ-สกุล" HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbempName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.profix_name")+" "+DataBinder.Eval(Container, "DataItem.emp_name")+"  "+DataBinder.Eval(Container, "DataItem.emp_lname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ลาป่วย" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbSick" runat="server" Text='0/0'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ลาพักผ่อน" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbRelax" runat="server" Text='0/0'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ลาคลอดบุตร" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbMaternity" runat="server" Text='0/0'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ลาอุปสมบท/พิธีฮัจย์" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbOrdain" runat="server" Text='0/0'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ตรวจเลือก/เตรียมพล" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbMilitary" runat="server" Text='0/0'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="วันเริ่มงาน / อายุงาน" HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbStartDate" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ปีงบประาณที่เข้า" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbYear" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ประเภทพนักงาน" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbEmpType" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ประวัติการลา" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
