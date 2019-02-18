<%@ Page Title="อัตรากำลัง" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="HRSProject.Manpower.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md">
            <div class="row">
                <div class="col-md-4">
                    <asp:DropDownList ID="txtYear" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <asp:Button ID="btnSearch" runat="server" Text="&#xf002; ค้นหา" Font-Size="Medium" CssClass="btn btn-success btn-sm align-items-end fa" OnClick="btnSearch_Click" />
            </div>
        </div>
        <div class="col-md">
        </div>
        <div class="col-md-4">
            <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="/Manpower/yearFrom" Text="&#xf067; เพิ่มปีงบประมาณ" Font-Size="Medium" CssClass="btn btn-success btn-sm fa"></asp:LinkButton>&nbsp&nbsp&nbsp
        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="/Manpower/editManpower" Text="&#xf044; ปรับปรุงอัตรากำลัง" Font-Size="Medium" CssClass="btn btn-warning btn-sm fa"></asp:LinkButton>
        </div>
    </div>
    <hr />
    <h2><%:Title %> รวมทั้งหมด</h2>
    <hr />
    <!--<div id="rowHead" class="row"  runat="server"></div>-->
    <asp:GridView ID="ManpowerSumGridView" runat="server" ShowFooter="True" Font-Size="19px" CssClass="table-sm" HeaderStyle-HorizontalAlign="Center"
        AutoGenerateColumns="False" OnRowDataBound="ManpowerSumGridView_RowDataBound" FooterStyle-Font-Bold="true" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
        <Columns>
            <asp:BoundField DataField="pos_name" HeaderText="ตำแหน่ง" HeaderStyle-Width="400px" />
            <asp:BoundField DataField="manpower_full" HeaderText="อัตราเต็ม" DataFormatString="{0}" HeaderStyle-Width="100px" />
            <asp:BoundField DataField="num" HeaderText="อัตราปัจจุบัน" DataFormatString="{0}" HeaderStyle-Width="100px" />
            <asp:BoundField DataField="dif" HeaderText="ส่วนต่าง" DataFormatString="{0}" HeaderStyle-Width="100px" />
        </Columns>

        <FooterStyle Font-Bold="True" BackColor="#990000" ForeColor="White"></FooterStyle>
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
    </asp:GridView>
    <hr />
    <h2><%:Title %> ตำแหน่ง/ด่านฯ</h2>
    <hr />
    <asp:GridView ID="ManpowerSubGridView" runat="server" ShowFooter="True" Font-Size="19px" CssClass="table table-sm" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center"
        AutoGenerateColumns="False" OnRowDataBound="ManpowerSubGridView_RowDataBound" FooterStyle-Font-Bold="true" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
        <Columns>
            <asp:BoundField DataField="pos_name" HeaderText="ตำแหน่ง" HeaderStyle-Width="300px" />
            <asp:TemplateField HeaderText="ลาดกระบัง" ItemStyle-Width="60px">
                <ItemTemplate>
                    <asp:LinkButton ID="lableLB" runat="server" OnCommand="lableLB_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="บางบ่อ" ItemStyle-Width="60px">
                <ItemTemplate>
                    <asp:LinkButton ID="lableBB" runat="server" OnCommand="lableLB_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="บางปะกง" ItemStyle-Width="60px">
                <ItemTemplate>
                    <asp:LinkButton ID="lableBK" runat="server" OnCommand="lableLB_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="พนัสนิคม" ItemStyle-Width="60px">
                <ItemTemplate>
                    <asp:LinkButton ID="lablePN" runat="server" OnCommand="lableLB_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="บ้านบึง" ItemStyle-Width="60px">
                <ItemTemplate>
                    <asp:LinkButton ID="lableBG" runat="server" OnCommand="lableLB_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="บางพระ(คีรี)" ItemStyle-Width="60px">
                <ItemTemplate>
                    <asp:LinkButton ID="lableBP" runat="server" OnCommand="lableLB_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="หนองขาม" ItemStyle-Width="60px">
                <ItemTemplate>
                    <asp:LinkButton ID="lableNK" runat="server" OnCommand="lableLB_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="โป่ง" ItemStyle-Width="60px">
                <ItemTemplate>
                    <asp:LinkButton ID="lablePO" runat="server" OnCommand="lableLB_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="พัทยา" ItemStyle-Width="60px">
                <ItemTemplate>
                    <asp:LinkButton ID="lablePY" runat="server" OnCommand="lableLB_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ทับช้าง 1" ItemStyle-Width="60px">
                <ItemTemplate>
                    <asp:LinkButton ID="lableTC1" runat="server" OnCommand="lableLB_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ทับช้าง 2" ItemStyle-Width="60px">
                <ItemTemplate>
                    <asp:LinkButton ID="lableTC2" runat="server" OnCommand="lableLB_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ธัญบุรี 1" ItemStyle-Width="60px" >
                <ItemTemplate>
                    <asp:LinkButton ID="lableTY1" runat="server" OnCommand="lableLB_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ธัญบุรี 2" ItemStyle-Width="60px">
                <ItemTemplate>
                    <asp:LinkButton ID="lableTY2" runat="server" OnCommand="lableLB_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ฝ่ายฯ" ItemStyle-Width="60px">
                <ItemTemplate>
                    <asp:LinkButton ID="lableCenter" runat="server" OnCommand="lableLB_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รวม" HeaderStyle-BackColor="#990000" ItemStyle-Width="60px">
                <ItemTemplate>
                    <asp:Label ID="lableTotalAll" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

        <FooterStyle Font-Bold="True" BackColor="#990000" ForeColor="White"></FooterStyle>
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
    </asp:GridView>
    <div runat="server" id="GridViewDetail" class="row" style="width: 100%"></div>
    <script type="text/javascript">


        function printDiv(div_print) {
            $(div_print).print();
        }
    </script>
</asp:Content>
