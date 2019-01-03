<%@ Page Title="เพิ่มพนักงาน" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="empForm.aspx.cs" Inherits="HRSProject.Profile.empForm" EnableEventValidation="false" %>

<%@ Import Namespace="HRSProject.Config" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <% if (alert != "")
        { %>
    <script type="text/javascript">
        $(function () {
            demo.showNotification('top', 'center', '<%=icon%>','<%=alertType%>', '<%=alert%>');
        });
    </script>
    <% } %>
    <!-- Nav tabs -->
    <asp:HiddenField ID="TabClick" runat="server" />
    <style type="text/css">
        @media print {
            #non-printable {
                display: none;
            }
        }
    </style>
    <div class="card" id="non-printable" style="z-index: 0;">
        <div class="card-header card-header-tabs card-header-warning" style="font-size: 100%">
            <div class="nav-tabs-navigation">
                <div class="nav-tabs-wrapper">
                    <ul class="nav nav-tabs" data-tabs="tabs" id="myTab" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link active" data-toggle="tab" id="homeMenu" href="#home" onclick="getTitle('#homeMenu');">ประวัติส่วนตัว</a>
                        </li>
                        <%if (empId.Text.Trim() != "")
                            { %>
                        <li class="nav-item">
                            <a class="nav-link" data-toggle="tab" id="menu1Menu" href="#menu1" onclick="getTitle('#menu1Menu');">ครอบครัว</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-toggle="tab" id="menu2Menu" href="#menu2" onclick="getTitle('#menu2Menu');">การศึกษา</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-toggle="tab" id="menu3Menu" href="#menu3" onclick="getTitle('#menu3Menu');">ประสบการณ์การทำงาน</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-toggle="tab" id="menu4Menu" href="#menu4" onclick="getTitle('#menu4Menu');">บุคคลรับรองหรือค้ำประกัน</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-toggle="tab" id="menu5Menu" href="#menu5" onclick="getTitle('#menu5Menu');">เอกสารหลักฐาน</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-toggle="tab" id="menu6Menu" href="#menu6" onclick="getTitle('#menu6Menu');">ประวัติการทำงาน/ด่านฯ</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-toggle="tab" id="menu7Menu" href="#menu7" onclick="getTitle('#menu7Menu');">ประวัติการเข้า-ออก/สถานะภาพ</a>
                        </li>
                        <%} %>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $.uploadPreview({
                input_field: ".audio-upload",
                preview_box: ".audio-preview",
                no_label: true
            });
        });
    </script>
    <div class="row">
        <div class="col-md-9">
            <div class="card" style="font-size: 100%; z-index: 0;">
                <div class="card-header card-header-warning">
                    <h4 class="card-title" id="txtTitle"></h4>
                    <p class="card-category" style="font-size: x-large;">
                        <% if (empId.Text != "")
                            { %>รหัสประจำตัวบุคคล : <%} %>
                        <asp:Label ID="empId" runat="server" Font-Bold="true" Text="" Font-Size="X-Large"></asp:Label>
                    </p>
                </div>
                <br />
                <div class="card-body">
                    <!-- Tab panes -->
                    <div class="tab-content" style="width: 100%">
                        <div id="home" class="container tab-pane active">
                            <div class="form-row row align-items-end">
                                <div class="col-md">
                                    รูปภาพ
                   <asp:FileUpload ID="imgUpload" runat="server" CssClass="form-control-file audio-upload"></asp:FileUpload>
                                </div>
                                <div class="col-md">
                                    <!--<asp:Image ID="imgViwe" runat="server" Width="150px" Height="150px" CssClass="audio-preview img-thumbnail shadow p-3 mb-5 bg-white rounded float-right"/>-->

                                </div>
                            </div>
                            <hr />
                            <div class="form-row">
                                <div class="col-md-2">
                                    สรรพนาม
                    <asp:DropDownList ID="txtProfix" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    ชื่อ
                    <asp:TextBox ID="txtName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    นามสกุล
                    <asp:TextBox ID="txtLname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-md-4">
                                    ตำแหน่ง
                        <asp:DropDownList ID="txtPos" runat="server" Font-Size="Large" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    หน่วย
                        <asp:DropDownList ID="txtAffi" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    สังกัดด่านฯ
                        <asp:DropDownList ID="txtCpoint" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    ประเภท
                        <asp:DropDownList ID="txtEmpType" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-md-2">
                                    วันที่เริ่มงาน
                        <asp:TextBox ID="txtDateStart" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    วันเกิด
                        <asp:TextBox ID="txtBrithDate" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    อายุ
                        <asp:TextBox ID="txtAge" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    น้ำหนัก
                        <asp:TextBox ID="txtWeight" runat="server" CssClass="form-control" MaxLength="3" Text="0" TextMode="Number"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    ส่วนสูง
                        <asp:TextBox ID="txtHeight" runat="server" CssClass="form-control" MaxLength="3" Text="0" TextMode="Number"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-md-2">
                                    เชื้อชาติ
                        <asp:TextBox ID="txtOrigin" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    สัญชาติ
                        <asp:TextBox ID="txtNationality" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    ศาสนา
                        <asp:TextBox ID="txtReligion" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md">
                                    เลขที่บัตรประจำตัวประชาชน
                        <asp:TextBox ID="txtIdcard" runat="server" CssClass="form-control" MaxLength="13" TextMode="Number"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-md-2">
                                    จำนวนพี่น้อง
                    <asp:TextBox ID="txtBrethren_num" runat="server" CssClass="form-control" Text="0" TextMode="Number"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    บุตรคนที่
                    <asp:TextBox ID="txtBrethren" runat="server" CssClass="form-control" Text="0" TextMode="Number"></asp:TextBox>
                                </div>
                                <div class="col-md">
                                    สถานะภาพ
                    <div class="radio radio-warning">
                        <asp:RadioButtonList ID="txtStatusRd" CssClass="radioButtonList" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="โสด" Value="โสด"></asp:ListItem>
                            <asp:ListItem Text="สมรส" Value="สมรส"></asp:ListItem>
                            <asp:ListItem Text="สมรส (ไม่จดทะเบียน)" Value="สมรส (ไม่จดทะเบียน)"></asp:ListItem>
                            <asp:ListItem Text="หย่า" Value="หย่า"></asp:ListItem>
                            <asp:ListItem Text="หม้าย" Value="หม้าย"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                                </div>
                            </div>
                            <hr />
                            <h4>ที่อยู่ตามบัตรประชาชน</h4>
                            <div class="form-row">
                                <div class="col-md-2">
                                    เลขที่
                    <asp:TextBox ID="txtIdcardAddNum" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    หมู่ที่
                    <asp:TextBox ID="txtIdcardMoo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md">
                                    ซอย
                    <asp:TextBox ID="txtIdcardSoi" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md">
                                    ถนน
                    <asp:TextBox ID="txtIdcardRd" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md">
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-md">
                                    แขวง/ตำบล
                    <asp:TextBox ID="txtIdcardSubDis" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    เขต/อำเภอ
                    <asp:TextBox ID="txtIdcardDis" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    จังหวัด
                    <asp:DropDownList ID="txtProvince_idcard" runat="server" CssClass="combobox form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    รหัสไปรษณีย์
                    <asp:TextBox ID="txtIdcardZipCode" runat="server" CssClass="form-control" Width="70%" MaxLength="5" TextMode="Number"></asp:TextBox>
                                </div>
                            </div>
                            <hr />
                            <h4>ที่อยู่ปัจจุบัน</h4>
                            <div class="form-row">
                                <div class="col-md-2">
                                    ประเภทที่อยู่
                    <asp:DropDownList ID="txtTypeAdd" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    เลขที่
                    <asp:TextBox ID="txtAddNum" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    หมู่ที่
                    <asp:TextBox ID="txtAddMoo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md">
                                    ซอย
                    <asp:TextBox ID="txtAddSoi" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md">
                                    ถนน
                    <asp:TextBox ID="txtAddRd" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md">
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-md">
                                    แขวง/ตำบล
                    <asp:TextBox ID="txtAddSubDis" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    เขต/อำเภอ
                    <asp:TextBox ID="txtAddDis" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    จังหวัด
                    <asp:DropDownList ID="txtProvince" runat="server" CssClass="combobox form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    รหัสไปรษณีย์
                    <asp:TextBox ID="txtAddZipcode" runat="server" CssClass="form-control" Width="70%" MaxLength="5" TextMode="Number"></asp:TextBox>
                                </div>

                            </div>
                            <div class="form-row">
                                <div class="col-md-2">
                                    โทรศัพท์บ้าน
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    มือถือ
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                            </div>
                            <hr />
                            <div class="form-row">
                                <div class="col-md-1">
                                    หมู่โลหิต
                    <asp:DropDownList ID="txtBlood" runat="server" CssClass="form-control">
                        <asp:ListItem>O</asp:ListItem>
                        <asp:ListItem>A</asp:ListItem>
                        <asp:ListItem>B</asp:ListItem>
                        <asp:ListItem>AB</asp:ListItem>
                    </asp:DropDownList>
                                </div>
                                <div class="col-md">
                                    ชื่อสถานพยาบาล
                   <asp:TextBox ID="txtHospital" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md">
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-md-3">
                                    เลขที่บัญชีออมทรัพย์ธนาคารกรุงไทย
                   <asp:TextBox ID="txtBookBank" runat="server" CssClass="form-control" MaxLength="10" TextMode="Number"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row justify-content-center">
                                <asp:Button ID="btnSubmit" Text="บันทึกข้อมูล" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-success btn"></asp:Button>&nbsp&nbsp&nbsp
                <asp:Button ID="reSet" Text="ยกเลิก" runat="server" CssClass="btn btn-danger btn" OnClick="reSet_Click"></asp:Button>
                            </div>
                        </div>

                        <!-- ------------------------------------------------------------------------------------------- -->
                        <div id="menu1" class="container tab-pane fade">

                            <div class="form-row">
                                <div class="col-md-3">
                                    ชื่อ-สกุล คู่สมรส
                    <asp:TextBox ID="txtMateName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    อาชีพ
                    <asp:TextBox ID="txtMateMetier" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    โทรศัพท์
                   <asp:TextBox ID="txtMateTel" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-md-2">
                                    บุตรชาย/คน
                    <asp:TextBox ID="txtMateSon_num" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    บุตรสาว/คน
                    <asp:TextBox ID="txtMateDaughter_num" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    สถานที่ทำงาน (คู่สมรส)
                    <asp:TextBox ID="txtMateWorkAdd" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <hr />
                            <div class="form-row">
                                <div class="col-md-3">
                                    ชื่อ-สกุล บิดา
                    <asp:TextBox ID="txtFaterName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    อาชีพ
                    <asp:TextBox ID="txtFaterMetier" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    โทรศัพท์
                   <asp:TextBox ID="txtFaterTel" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-md-7">
                                    ที่อยู่ที่สามารถติดต่อได้ (บิดา)
                    <asp:TextBox ID="txtFaterAdd" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <hr />
                            <div class="form-row">
                                <div class="col-md-3">
                                    ชื่อ-สกุล มารดา
                    <asp:TextBox ID="txtMomName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    อาชีพ
                    <asp:TextBox ID="txtMomMetier" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    โทรศัพท์
                   <asp:TextBox ID="txtMomTel" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-md-7">
                                    ที่อยู่ที่สามารถติดต่อได้ (มารดา)
                    <asp:TextBox ID="txtMomAdd" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row justify-content-center">
                                <asp:Button ID="btnFamily" Text="บันทึกข้อมูล" runat="server" CssClass="btn btn-success btn" OnClick="btnFamily_Click"></asp:Button>&nbsp&nbsp&nbsp
                <asp:Button ID="Button20" Text="ยกเลิก" runat="server" CssClass="btn btn-danger btn" OnClick="reSet_Click"></asp:Button>
                            </div>
                        </div>
                        <!-- ------------------------------------------------------------------------------------------- -->
                        <div id="menu2" class="container tab-pane fade">

                            <div class="form-row formHead">
                                <div class="col-md-3">
                                    ระดับการศึกษา
                    <asp:DropDownList ID="txtEducation" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:HiddenField ID="txtEduID" runat="server" />
                                </div>
                                <div class="col-md">
                                    ชื่อสถานศึกษา
                    <asp:TextBox ID="txtEduName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md">
                                    สาขาวิชา
                    <asp:TextBox ID="txtEduBranch" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    เกรดเฉลี่ย
                    <asp:TextBox ID="txtEduGPA" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    ปีที่สำเร็จการศึกษา
                    <asp:TextBox ID="txtEduYear" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <br />
                                    <asp:Button ID="btnEducation" runat="server" Text="&#xf067; เพิ่ม" Font-Size="Medium" CssClass="btn btn-success btn-sm align-items-end fa" OnClick="btnEducation_Click" />
                                </div>
                            </div>
                            <br />
                            <div class="form-row">
                                <div class="col-md">
                                    <asp:GridView ID="EduGridView" runat="server" DataKeyNames="pro_edu_id"
                                        GridLines="None"
                                        CssClass="table table-hover table-sm"
                                        OnRowDataBound="EduGridView_RowDataBound"
                                        AutoGenerateColumns="False"
                                        HeaderStyle-CssClass="thead-light">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ระดับการศึกษา">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbEducation" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ชื่อสถานศึกษา">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbEduName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="สาขาวิชา">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbEduBranch" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="เกรดเฉลี่ย">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbEduGPA" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ปีที่สำเร็จการศึกษา">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbEduYear" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ลบข้อมูล">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEduDelete" runat="server" Text="&#xf014; ลบ" Font-Size="Small" CssClass="btn btn-outline-danger btn-sm fa"
                                                        OnClientClick="return confirmDelete(this);" OnCommand="btnEduDelete_Command" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lbNullEdu" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <!-- ------------------------------------------------------------------------------------------- -->
                        <div id="menu3" class="container tab-pane fade">
                            <h3>ประสบการณ์การทำงาน (ภายในฝ่ายจัดเก็บฯ)</h3>
                            <div id="DivExpMotoway" runat="server">
                                <div class="form-row formHead">
                                    <div class="col-md-2">
                                        ตั้งแต่
                    <asp:TextBox ID="txtExpMoterwayStart" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        ถึง
                    <asp:TextBox ID="txtExpMoterwayEnd" runat="server" CssClass="form-control"></asp:TextBox>
                                        <p style="color: red; font-size: large;">***ปัจจุบันใส่ 00-00-0000</p>
                                    </div>
                                    <div class="col-md-4">
                                        ตำแหน่ง 
                    <asp:DropDownList ID="txtExpMoterwayPos" runat="server" Font-Size="Large" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <br />
                                        <asp:Button ID="btnExpMoterwayAdd" runat="server" Text="&#xf067; เพิ่ม" Font-Size="Medium" CssClass="btn btn-success btn-sm align-items-end fa" OnClick="btnExpMoterwayAdd_Click" />
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="form-row">
                                <div class="col-md">
                                    <asp:GridView ID="ExpMoterwayGridView" runat="server" DataKeyNames="exp_moterway_id"
                                        GridLines="None"
                                        CssClass="table table-hover table-sm"
                                        OnRowDataBound="ExpMoterwayGridView_RowDataBound"
                                        AutoGenerateColumns="False"
                                        HeaderStyle-CssClass="thead-light"
                                        OnRowEditing="ExpMoterwayGridView_RowEditing"
                                        OnRowCancelingEdit="ExpMoterwayGridView_RowCancelingEdit"
                                        OnRowUpdating="ExpMoterwayGridView_RowUpdating"
                                        OnRowDeleting="ExpMoterwayGridView_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ตั้งแต่-ถึง">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbExpMoterwayDate" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEditDateStart" size="20" runat="server" Text='<%# Eval("exp_moterway_start") %>' CssClass="form-control datepicker"></asp:TextBox>
                                                    <asp:TextBox ID="txtEditDateEnd" size="20" runat="server" Text='<%# Eval("exp_moterway_end") %>' CssClass="form-control datepicker"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ตำแหน่ง">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbExpMoterwayPos" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowEditButton="True" CancelText="ยกเลิก" EditText="&#xf040; แก้ไข" UpdateText="แก้ไข" HeaderText="ปรับปรุง" ControlStyle-Font-Size="Small" ControlStyle-CssClass="btn btn-outline-warning btn-sm fa" />
                                            <asp:CommandField ShowDeleteButton="True" HeaderText="ลบ" DeleteText="&#xf014; ลบ" ControlStyle-CssClass="btn btn-outline-danger btn-sm fa" ControlStyle-Font-Size="Small" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lbExpMoterwayNull" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <br />
                            <hr />
                            <h3>ประสบการณ์การทำงาน</h3>
                            <div class="form-row formHead">
                                <div class="col-md-2">
                                    ตั้งแต่
                    <asp:TextBox ID="txtDateExpStart" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    ถึง
                    <asp:TextBox ID="txtDateExpEnd" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md">
                                    ตำแหน่ง
                    <asp:TextBox ID="txtExpPos" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    ลักษณะงานที่รับผิดชอบ
                    <asp:TextBox ID="txtExpDetail" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    อัตราเงินเดือน
                    <asp:TextBox ID="txtExpSaraly" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <br />
                                    <asp:Button ID="btnExpAdd" runat="server" Text="&#xf067; เพิ่ม" Font-Size="Medium" CssClass="btn btn-success btn-sm align-items-end fa" OnClick="btnExpAdd_Click" />
                                </div>
                            </div>
                            <hr />
                            <div class="form-row">
                                <div class="col-md">
                                    <asp:GridView ID="ExpGridView" runat="server" DataKeyNames="exp_id"
                                        GridLines="None"
                                        CssClass="table table-hover table-sm"
                                        OnRowDataBound="ExpGridView_RowDataBound"
                                        AutoGenerateColumns="False"
                                        HeaderStyle-CssClass="thead-light">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ตั้งแต่-ถึง">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbExpDate" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ตำแหน่ง">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbExpPos" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ลักษณะงานที่รับผิดชอบ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbExpDetail" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="อัตราเงินเดือน">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbExpSaraly" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ลบข้อมูล">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnExpDelete" runat="server" Text="&#xf014; ลบ" Font-Size="Small" CssClass="btn btn-outline-danger btn-sm fa"
                                                        OnClientClick="return confirmDelete(this);" OnCommand="btnExpDelete_Command" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lbExpNull" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <!-- ------------------------------------------------------------------------------------------- -->
                        <div id="menu4" class="container tab-pane fade">

                            <div class="form-row">
                                <div class="col-md-3">
                                    ชื่อ-สกุล
                    <asp:TextBox ID="txtGuarantorName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    ตำแหน่ง
                    <asp:TextBox ID="txtGuarantorPos" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md">
                                    สังกัด
                    <asp:TextBox ID="txtGuarantorAff" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-md">
                                    ที่อยู่ที่สามารถติดต่อได้
                    <asp:TextBox ID="txtGuarantorAdd" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    โทรศัพท์
                    <asp:TextBox ID="txtGuarantorTel" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <hr />
                            <h3>บุคคลที่สามารถติดต่อได้ในกรณีฉุกเฉิน</h3>
                            <div class="form-row">
                                <div class="col-md-3">
                                    ชื่อ-สกุล
                    <asp:TextBox ID="txtEConName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    ความสัมพันธ์
                    <asp:TextBox ID="txtEConRelationShip" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-md">
                                    ที่อยู่ที่สามารถติดต่อได้
                    <asp:TextBox ID="txtEConAdd" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    โทรศัพท์
                    <asp:TextBox ID="txtEConTel" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row justify-content-center">
                                <asp:Button ID="btnContactSave" Text="บันทึกข้อมูล" runat="server" CssClass="btn btn-success btn" OnClick="btnContactSave_Click"></asp:Button>&nbsp&nbsp&nbsp
                <asp:Button ID="Button16" Text="ยกเลิก" runat="server" CssClass="btn btn-danger btn" OnClick="reSet_Click"></asp:Button>
                            </div>
                        </div>
                        <!-- ------------------------------------------------------------------------------------------- -->
                        <div id="menu5" class="container tab-pane fade">

                            <div class="form-row formHead">
                                <div class="col-md-3">
                                    เอกสาร
                <asp:DropDownList ID="txtDocType" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    เลือกไฟล์
                <asp:FileUpload ID="txtFileDoc" runat="server" CssClass="form-control-file"></asp:FileUpload>
                                </div>
                                <div class="col-md-1">
                                    <br />
                                    <asp:Button ID="btnDocAdd" runat="server" Text="&#xf067; เพิ่ม" Font-Size="Medium" CssClass="btn btn-success btn-sm align-items-end fa" OnClick="btnDocAdd_Click" />
                                </div>
                            </div>
                            <br />
                            <div class="form-row">
                                <asp:GridView ID="DocGridView" runat="server" DataKeyNames="doc_id"
                                    GridLines="None"
                                    CssClass="table table-hover table-sm"
                                    OnRowDataBound="DocGridView_RowDataBound"
                                    AutoGenerateColumns="False"
                                    HeaderStyle-CssClass="thead-light">
                                    <Columns>
                                        <asp:TemplateField HeaderText="เอกสารหลักฐาน">
                                            <ItemTemplate>
                                                <asp:Label ID="lbDocName" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เรียกดู">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDocViwe" runat="server" Text="&#xf0f6; เรียกดู" Font-Size="Small" data-lightbox="image-1" CssClass="btn btn-outline-info btn-sm fa"
                                                    OnClientClick="return false;" />
                                                <asp:Button ID="btnDocDowload" runat="server" Text="&#xf019; ดาวโหลด" Font-Size="Small" CssClass="btn btn-outline-info btn-sm fa" OnCommand="btnDocDowload_Command" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ลบ">
                                            <ItemTemplate>
                                                <asp:Button ID="btnDocDelete" runat="server" Text="&#xf014; ลบ" Font-Size="Small" CssClass="btn btn-outline-danger btn-sm fa"
                                                    OnClientClick="return confirmDelete(this);" OnCommand="btnDocDelete_Command" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <asp:Label ID="lbDocNull" runat="server" Text=""></asp:Label>
                        </div>
                        <!-- ------------------------------------------------------------------------------------------- -->
                        <div id="menu6" class="container tab-pane fade">
                            <div id="DivExpCpoint" runat="server">
                                <div class="form-row formHead">
                                    <div class="col-md-2">
                                        ตั้งแต่
                    <asp:TextBox ID="txtExpCpointStart" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        ถึง
                    <asp:TextBox ID="txtExpCpointEnd" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                        <p style="color: red; font-size: large;">***ปัจจุบันใส่ 00-00-0000</p>
                                    </div>
                                    <div class="col-md-2">
                                        หน่วย
                    <asp:DropDownList ID="txtExpCpointAff" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        ด่านฯ
                    <asp:DropDownList ID="txtExpCpoint" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <br />
                                        <asp:Button ID="btnExpCpointAdd" runat="server" Text="&#xf067; เพิ่ม" Font-Size="Medium" CssClass="btn btn-success btn-sm align-items-end fa" OnClick="btnExpCpointAdd_Click" />
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="form-row">
                                <div class="col-md">
                                    <asp:GridView ID="ExpCpointGridView" runat="server" DataKeyNames="work_history_id"
                                        GridLines="None"
                                        CssClass="table table-hover table-sm"
                                        OnRowDataBound="ExpCpointGridView_RowDataBound"
                                        AutoGenerateColumns="False"
                                        HeaderStyle-CssClass="thead-light"
                                        OnRowEditing="ExpCpointGridView_RowEditing"
                                        OnRowCancelingEdit="ExpCpointGridView_RowCancelingEdit"
                                        OnRowUpdating="ExpCpointGridView_RowUpdating"
                                        OnRowDeleting="ExpCpointGridView_RowDeleting"
                                        AllowSorting="true"
                                        AllowPaging="true"
                                        PageSize="20"
                                        OnPageIndexChanging="ExpCpointGridView_PageIndexChanging"
                                        PagerSettings-Mode="NumericFirstLast">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ตั้งแต่-ถึง">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbExpCpointDate" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtExpCpointDateStart" size="20" runat="server" Text='<%# Eval("work_history_in") %>' CssClass="form-control datepicker"></asp:TextBox>
                                                    <asp:TextBox ID="txtExpCpointDateEnd" size="20" runat="server" Text='<%# Eval("work_history_out") %>' CssClass="form-control datepicker"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ตำแหน่ง">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbExpCpointPos" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="หน่วย/ด่านฯ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbExpCpointName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowEditButton="True" CancelText="ยกเลิก" EditText="&#xf040; แก้ไข" UpdateText="แก้ไข" HeaderText="ปรับปรุง" ControlStyle-Font-Size="Small" ControlStyle-CssClass="btn btn-outline-warning btn-sm fa" />
                                            <asp:CommandField ShowDeleteButton="True" HeaderText="ลบ" DeleteText="&#xf014; ลบ" ControlStyle-CssClass="btn btn-outline-danger btn-sm fa" ControlStyle-Font-Size="Small" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                    </asp:GridView>
                                    <asp:Label ID="lbExpCpointNull" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <!-- ------------------------------------------------------------------------------------------- -->
                        <!-- ------------------------------------------------------------------------------------------- -->
                        <div id="menu7" class="container tab-pane fade">
                            <div id="DivStatus" runat="server">
                                <div class="form-row formHead">
                                    <div class="col-md-2">
                                        สถานะภาพ
                    <asp:DropDownList ID="txtHistoryStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        วันที่
                    <asp:TextBox ID="txtHistoryDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                        <p style="color: red; font-size: large;">***ปัจจุบันใส่ 00-00-0000</p>
                                    </div>
                                    <div class="col-md-3">
                                        หมายเหตุ
                    <asp:TextBox ID="txtHistoryNote" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <br />
                                        <asp:Button ID="btnHistoryAdd" runat="server" Text="&#xf067; เพิ่ม" Font-Size="Medium" CssClass="btn btn-success btn-sm align-items-end fa" OnClick="btnHistoryAdd_Click" />
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="form-row">
                                <div class="col-md">
                                    <asp:GridView ID="InOutHistoryGridView" runat="server" DataKeyNames="history_id"
                                        GridLines="None"
                                        CssClass="table table-hover table-sm"
                                        AutoGenerateColumns="False"
                                        OnRowDataBound="InOutHistoryGridView_RowDataBound"
                                        HeaderStyle-CssClass="thead-light"
                                        OnRowEditing="InOutHistoryGridView_RowEditing"
                                        OnRowCancelingEdit="InOutHistoryGridView_RowCancelingEdit"
                                        OnRowUpdating="InOutHistoryGridView_RowUpdating"
                                        OnRowDeleting="InOutHistoryGridView_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="สถานะภาพ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbHistoryStatus" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="txtHistoryStatusEdit" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="วันที่">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbHistoryDate" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtHistoryDateEdit" size="10" runat="server" Text='<%# Eval("history_date") %>' CssClass="form-control datepicker"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="หมายเหตุ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbHistoryNote" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtHistoryNoteEdit" size="10" runat="server" Text='<%# Eval("history_note") %>' CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowEditButton="True" CancelText="ยกเลิก" EditText="&#xf040; แก้ไข" UpdateText="แก้ไข" HeaderText="ปรับปรุง" ControlStyle-Font-Size="Small" ControlStyle-CssClass="btn btn-outline-warning btn-sm fa" />
                                            <asp:CommandField ShowDeleteButton="True" HeaderText="ลบ" DeleteText="&#xf014; ลบ" ControlStyle-CssClass="btn btn-outline-danger btn-sm fa" ControlStyle-Font-Size="Small" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lbHistoryNull" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <!-- ------------------------------------------------------------------------------------------- -->
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card card-profile">
                <div class="card-avatar">
                    <a href="#pablo">
                        <img class="img" <%if (empId.Text == "") { Response.Write("src='/Upload/images/no-photo.jpg'"); } else { if (new DBScript().getEmpData("emp_img_profile", empId.Text) != "") { Response.Write("src='" + new DBScript().getEmpData("emp_img_profile", empId.Text) + "'"); } else { Response.Write("src='/Upload/images/no-photo.jpg'"); } } %>>
                    </a>
                </div>
                <div class="card-body" style="font-size: large;">
                    <span class="card-title">
                        <asp:Label ID="lbTxtID" runat="server"></asp:Label></span><br />
                    <span class="card-title">
                        <asp:Label ID="lbTxtName" runat="server"></asp:Label></span><br />
                    <span class="card-category text-gray">
                        <asp:Label ID="lbTxtPos" runat="server"></asp:Label></span>
                </div>
            </div>
        </div>
    </div>
    <hr />
    <script type="text/javascript">
        $(function () {
                //
                <%if (!string.IsNullOrEmpty(Request.Params["view"]))
        {
                %>
            $('.tab-content input').attr('disabled', 'true');
            $('.tab-content select').attr('disabled', 'true');
            $('.tab-content textarea').attr('disabled', 'true');
            $('.tab-content a').removeAttr('href');
            $('.tab-content a').removeAttr('onclick');
            $('.tab-content a').hide();
            $('.tab-content input[type=submit]').hide();
            $('.formHead').hide();
                <%
        }
            %>
        });
    </script>

    <script type="text/javascript">   
        $(function () {
            $(".datepicker").datepicker($.datepicker.regional["th"]);
            //$(".hasDatepicker").attr('readonly', true);
            //$(".datepicker").datepicker("setDate", new Date());

            $("#<%=txtDateStart.ClientID.ToString()%>").datepicker($.datepicker.regional["th"]); // Set ภาษาที่เรานิยามไว้ด้านบน

            $("#<%=txtBrithDate.ClientID.ToString()%>").datepicker($.datepicker.regional["th"]);

            $("#<%=txtDateExpStart.ClientID.ToString()%>").datepicker($.datepicker.regional["th"]);

            $("#<%=txtDateExpEnd.ClientID.ToString()%>").datepicker($.datepicker.regional["th"]);

            $("#<%=txtExpMoterwayStart.ClientID.ToString()%>").datepicker($.datepicker.regional["th"]);

            $("#<%=txtExpMoterwayEnd.ClientID.ToString()%>").datepicker($.datepicker.regional["th"]);



            if ($("#<%=empId.ClientID.ToString()%>").html() == "") {
                $("#<%=txtDateStart.ClientID.ToString()%>").datepicker("setDate", new Date()); //Set ค่าวันปัจจุบัน
                $("#<%=txtBrithDate.ClientID.ToString()%>").datepicker("setDate", new Date());
            }

            $("#<%=txtDateExpStart.ClientID.ToString()%>").datepicker("setDate", new Date());
            $("#<%=txtDateExpEnd.ClientID.ToString()%>").datepicker("setDate", new Date());
            $("#<%=txtExpMoterwayStart.ClientID.ToString()%>").datepicker("setDate", new Date());
            $("#<%=txtExpMoterwayEnd.ClientID.ToString()%>").val("00-00-0000");
            $("#<%=txtExpCpointStart.ClientID.ToString()%>").datepicker("setDate", new Date());
            $("#<%=txtExpCpointEnd.ClientID.ToString()%>").val("00-00-0000");
            $("#<%=txtHistoryDate.ClientID.ToString()%>").val("00-00-0000");

            //คำนวนอายุ
            $("#<%=txtAge.ClientID.ToString()%>").val(getAge($("#<%=txtBrithDate.ClientID.ToString()%>").val()));

            $("#<%=txtBrithDate.ClientID.ToString()%>").on("change", function () {
                $("#<%=txtAge.ClientID.ToString()%>").val(getAge($("#<%=txtBrithDate.ClientID.ToString()%>").val()));
            });

            $('#myTab a[href="#<%=TabClick.Value%>"]').tab('show');
            $('#txtTitle').html('ข้อมูลพนักงาน (ประวัติส่วนตัว)');
        });

        function getTitle(menu) {
            $('#txtTitle').html('ข้อมูลพนักงาน (' + $(menu).text() + ')');
        }

        function confirmDelete(sender) {
            if ($(sender).attr("confirmed") == "true") { return true; }

            bootbox.confirm({
                size: "small",
                message: "คุณต้องการลบข้อมูล ใช่หรือไม่",
                buttons: {
                    confirm: {
                        label: 'ใช่',
                        className: 'btn btn-success'
                    },
                    cancel: {
                        label: 'ไม่ใช่',
                        className: 'btn btn-danger'
                    }
                }
                , callback: function (confirmed) {
                    if (confirmed) {
                        $(sender).attr('confirmed', confirmed);
                        sender.click();
                    }
                }
            });

            return false;
        }
    </script>
</asp:Content>
