<%@ Page Title="ตั้งค่าพื้นฐาน" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="settingData.aspx.cs" Inherits="HRSProject.Admin.settingData" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header card-header-warning">
            <h3 class="card-title">ตั้งค่าพื้นฐาน</h3>
        </div>
        <div class="card-body table-responsive">
            <div class="row">
                <div class="col-sm-2">
                    <nav class="nav flex-column">
                        <a class="nav-link" href="/Admin/profixForm"><i class="fa fa-address-book" style="color: #FF7E00;"></i>&nbsp สรรพนาม</a>
                        <a class="nav-link" href="/Admin/addTypeForm"><i class="fa fa-home" style="color: #FF7E00;"></i>&nbsp ประเภทที่อยู่</a>
                        <a class="nav-link" href="/Admin/levelEduForm"><i class="fa fa-mortar-board" style="color: #FF7E00;"></i>&nbsp ระดับการศึกษา</a>
                        <a class="nav-link" href="/Admin/posForm"><i class="fa fa-star" style="color: #FF7E00;"></i>&nbsp ตำแหน่งงาน</a>
                        <a class="nav-link" href="/Admin/affForm"><i class="fa fa-group" style="color: #FF7E00;"></i>&nbsp หน่วยย่อย</a>
                    </nav>
                </div>
                <div class="col-sm-2">
                    <nav class="nav flex-column">
                        <a class="nav-link" href="/Admin/cpointForm"><i class="fa fa-thumb-tack" style="color: #FF7E00;"></i>&nbsp ด่านฯ</a>
                        <a class="nav-link" href="/Admin/provinceForm"><i class="fa fa-street-view" style="color: #FF7E00;"></i>&nbsp จังหวัด</a>
                        <a class="nav-link" href="/Admin/statusForm"><i class="fa fa-info-circle" style="color: #FF7E00;"></i>&nbsp สถานะ</a>
                        <a class="nav-link" href="/Admin/docTypeForm"><i class="fa fa-id-card-o" style="color: #FF7E00;"></i>&nbsp ประเภทเอกสาร</a>
                        <a class="nav-link" href="/Admin/empTypeForm"><i class="fa fa-user-circle-o" style="color: #FF7E00;"></i>&nbsp ประเภทพนักงาน</a>
                    </nav>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="card">
        <div class="card-header card-header-warning">
            <h3 class="card-title">ค่าคงที่</h3>
        </div>
        <div class="card-body table-responsive">
            <div class="row">
                <div class="col-sm">
                    <nav class="nav flex-column">
                        <a class="nav-link" href="/Admin/vacationEmpForm"><i class="fa fa-bed" style="color: #FF7E00;"></i>&nbsp วันลา</a>
                    </nav>
                </div>
                <div class="col-sm">
                </div>
                <div class="col-sm">
                </div>
                <div class="col-sm">
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="card">
        <div class="card-header card-header-warning">
            <h3 class="card-title">ตั้งค่าผู้ใช้งาน</h3>
        </div>
        <div class="card-body table-responsive">
            <div class="row">
                <div class="col-sm">
                    <nav class="nav flex-column">
                        <a class="nav-link" href="/Admin/userForm"><i class="fa fa-user-plus" style="color: #FF7E00;"></i>&nbsp ผู้ใช้งาน</a>
                    </nav>
                </div>
                <div class="col-sm">
                </div>
                <div class="col-sm">
                </div>
                <div class="col-sm">
                </div>
            </div>
        </div>
    </div>


</asp:Content>
