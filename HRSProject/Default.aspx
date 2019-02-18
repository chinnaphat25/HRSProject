<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HRSProject._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-3 col-md-6 col-sm-6" runat="server" id="boxUserSystem">
                    <div class="card card-stats">
                        <div class="card-header card-header-success card-header-icon">
                            <div class="card-icon">
                                <i class="fa fa-user-circle-o"></i>
                            </div>
                            <h4 class="card-category">กำลังใช้งานระบบ</h4>
                            <h5 class="card-title">
                                <asp:Label ID="lbUserOnline" runat="server" Text="Label"></asp:Label>
                            </h5>
                        </div>
                        <div class="card-footer">
                            <div class="stats">
                                <i class="fa fa-clock-o"></i>&nbsp อัปเดท
                                <asp:Label ID="ticTime" runat="server" Text=""></asp:Label>
                                วินาทีที่ผ่านมา
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 col-sm-6" runat="server" id="boxGuest">
                    <div class="card card-stats">
                        <div class="card-header card-header-info card-header-icon">
                            <div class="card-icon">
                                <i class="	fa fa-address-book-o"></i>
                            </div>
                            <h4 class="card-category">รายงานตัวพนักงานใหม่</h4>
                            <h5 class="card-title">
                                <asp:Label ID="LabelGuest" runat="server" Font-Size="Large"></asp:Label>
                            </h5>
                        </div>
                        <div class="card-footer">
                            <div class="stats">
                                <a class="card-link text-secondary" href="/Guest/GuestDefault"><i class="	fa fa-address-book-o"></i>&nbsp รายงานตัวพนักงานใหม่</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 col-sm-6" runat="server" id="boxDataEmp">
                    <div class="card card-stats">
                        <div class="card-header card-header-warning card-header-icon">
                            <div class="card-icon">
                                <a class="card-link text-light" href="/Profile/empViwe"><i class="fa fa-vcard-o"></i></a>
                            </div>
                            <h4 class="card-category">บุคลากรในระบบ</h4>
                            <h5 class="card-title">
                                <asp:Label ID="lbCountEmp" runat="server"></asp:Label>
                            </h5>
                        </div>
                        <div class="card-footer">
                            <div class="stats">
                                <a class="card-link text-secondary" href="/Profile/empViwe"><i class="fa fa-address-card-o "></i>&nbsp ข้อมูลบุคคล</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 col-sm-6" runat="server" id="boxMigrateEmp">
                    <div class="card card-stats">
                        <div class="card-header card-header-info card-header-icon">
                            <div class="card-icon">
                                <a class="card-link text-light" href="TmpAcation/TmpCpointForm"><i class="fa fa-refresh"></i></a>
                            </div>
                            <h4 class="card-category">การโยกย้ายพนักงาน</h4>
                            <h5 class="card-title">
                                <asp:Label ID="Label1" runat="server">&nbsp</asp:Label>
                            </h5>
                        </div>
                        <div class="card-footer">
                            <div class="stats">
                                <a class="card-link text-secondary" href="TmpAcation/TmpCpointForm"><i class="fa fa-refresh"></i>&nbsp จัดการข้อมูลการโยกย้าย</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 col-sm-6" runat="server" id="boxChangPos">
                    <div class="card card-stats">
                        <div class="card-header card-header-success card-header-icon">
                            <div class="card-icon">
                                <a class="card-link text-light" href="/TmpAcation/TmpPosForm"><i class="fa fa-line-chart"></i></a>
                            </div>
                            <h4 class="card-category">ปรับตำแหน่งพนักงาน</h4>
                            <h5 class="card-title">
                                <asp:Label ID="Label3" runat="server">&nbsp</asp:Label>
                            </h5>
                        </div>
                        <div class="card-footer">
                            <div class="stats">
                                <a class="card-link text-secondary" href="/TmpAcation/TmpPosForm"><i class="fa fa-line-chart"></i>&nbsp จัดการข้อมูลการปรับตำแหน่ง</a>
                            </div>
                        </div>
                    </div>
                </div>
                <!--<div class="col-lg-3 col-md-6 col-sm-6" runat="server" id="boxSaralyEmp">
            <div class="card card-stats">
                <div class="card-header card-header-success card-header-icon">
                    <div class="card-icon">
                        <i class="fa fa-money"></i>
                    </div>
                    <h4 class="card-category">ข้อมูลเงินเดือน</h4>
                    <h5 class="card-title">
                        <asp:Label ID="Label6" runat="server">coming soon...</asp:Label>
                    </h5>
                </div>
                <div class="card-footer">
                    <div class="stats">
                        <a class="card-link text-secondary" href="#"><i class="fa fa-money"></i>&nbsp จัดการข้อมูลเงินเดือนพนักงาน</a>
                    </div>
                </div>
            </div>
        </div>-->
                <div class="col-lg-3 col-md-6 col-sm-6" runat="server" id="boxLeaveEmp">
                    <div class="card card-stats">
                        <div class="card-header card-header-warning card-header-icon">
                            <div class="card-icon">
                                <a class="card-link text-light" href="/LeaveEmp/empLeaveForm"><i class="fa fa-hotel"></i></a>
                            </div>
                            <h4 class="card-category">ข้อมูลการลางาน</h4>
                            <h5 class="card-title">
                                <asp:Label ID="Label5" runat="server">&nbsp</asp:Label>
                            </h5>
                        </div>
                        <div class="card-footer">
                            <div class="stats">
                                <a class="card-link text-secondary" href="/LeaveEmp/empLeaveForm"><i class="fa fa-hotel"></i>&nbsp จัดการข้อมูลการลางาน</a>
                            </div>
                        </div>
                    </div>
                </div>
                <!--
        <div class="col-lg-3 col-md-6 col-sm-6" runat="server" id="boxScheduleEmp">
            <div class="card card-stats">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="fa fa-table"></i>
                    </div>
                    <h4 class="card-category">ตารางเวร</h4>
                    <h5 class="card-title">
                        <asp:Label ID="Label2" runat="server">coming soon...</asp:Label>
                    </h5>
                </div>
                <div class="card-footer">
                    <div class="stats">
                        <a class="card-link text-secondary" href="#"><i class="fa fa-table"></i>&nbsp จัดการตารางเวร</a>
                    </div>
                </div>
            </div>
        </div>-->
                <div class="col-lg-3 col-md-6 col-sm-6" runat="server" id="boxResignEmp">
                    <div class="card card-stats">
                        <div class="card-header card-header-danger card-header-icon">
                            <div class="card-icon">
                                <a class="card-link text-light" href="TmpAcation/TmpExForm"><i class="fa fa-share"></i></a>
                            </div>
                            <h4 class="card-category">ลาออก</h4>
                            <h5 class="card-title">
                                <asp:Label ID="Label4" runat="server">&nbsp</asp:Label>
                            </h5>
                        </div>
                        <div class="card-footer">
                            <div class="stats">
                                <a class="card-link text-secondary" href="TmpAcation/TmpExForm"><i class="fa fa-share"></i>&nbsp จัดการลาออก</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-8 col-md-7 col-sm-7">
                    <div class="card card-stats">
                        <div class="card-header card-header-danger card-header-icon">
                            <div class="card-icon">
                                <i class="fa fa-hotel"></i>
                            </div>
                            <h4 class="card-category">สถิตการลาสูงสุด 10 อันดับ</h4>
                            <h4 class="card-title">ปีงบประมาณ
                                <asp:Label ID="lbYear" runat="server" Text=""></asp:Label></h4>
                            <h5 class="card-title">
                                <asp:Label ID="lbLeaveStatisticsNull" runat="server" Text=""></asp:Label>
                            </h5>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-1">
                            </div>
                            <div class="col-md">
                                <asp:GridView ID="LeaveStatisticsGridView" runat="server"
                                    DataKeyNames="emp_leave_emp_id"
                                    GridLines="None"
                                    AutoGenerateColumns="False"
                                    CssClass="table table-hover table-sm">
                                    <Columns>
                                        <asp:TemplateField HeaderText="รหัสบุคคล">
                                            <ItemTemplate>
                                                <asp:Label ID="lbEmpID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.emp_leave_emp_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ชื่อ-สกุล">
                                            <ItemTemplate>
                                                <asp:Label ID="lbEmpName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.emp_name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนที่ลา (ครั้ง)" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbSick" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.total") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ป่วย (วัน)" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbSick" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.sick") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="พักผ่อน (วัน)" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbSick" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.relax") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                        <div class="card-footer">
                            <div class="stats">
                                <i class="fa fa-clock-o"></i>&nbsp อัปเดท 30 วินาทีที่ผ่านมา
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-8 col-md-6 col-sm-6" runat="server" id="Div1">
                    <div class="card card-stats">
                        <div class="card-header card-header-secondary card-header-icon">
                            <div class="card-icon">
                                <a class="card-link text-light" href="#"><i class='fa fa-user-secret'></i></a>
                            </div>
                            <h4 class="card-category">รายชื่อผู้เกษียณ</h4>
                            <h4 class="card-title">ปีงบประมาณ
                                <asp:Label ID="txtYear" runat="server" Text=""></asp:Label></h4>
                            <h5 class="card-title">
                                <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                            </h5>
                            <br />
                            <div class="row">
                                <div class="col-md-1">
                                </div>
                                <div class="col-md">
                                    <asp:GridView ID="RetireGridView" runat="server"
                                        DataKeyNames="emp_id"
                                        GridLines="None" HeaderStyle-HorizontalAlign="Left"
                                        AutoGenerateColumns="False"
                                        CssClass="table table-hover table-sm" CellPadding="4" ForeColor="#333333">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="emp_id" HeaderText="รหัสบุคคล">
                                                <HeaderStyle />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="name" HeaderText="ชื่อ-สกุล">
                                                <HeaderStyle />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cpoint_name" HeaderText="ด่านฯ">
                                                <HeaderStyle />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="pos_name" HeaderText="ตำแหน่ง">
                                                <HeaderStyle />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="type_emp_name" HeaderText="ประเภท">
                                                <HeaderStyle />
                                            </asp:BoundField>
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                </div>
                                <div class="col-md-1">
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <div class="stats">
                                <i class="fa fa-clock-o"></i>&nbsp อัปเดท 30 วินาทีที่ผ่านมา
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Timer ID="Timer1" runat="server" Interval="30000" OnTick="Timer1_Tick"></asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
