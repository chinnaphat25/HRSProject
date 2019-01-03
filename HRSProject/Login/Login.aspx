<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HRSProject.Login.Login" %>

<!DOCTYPE html>
<html lang="th">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="favicon.ico">

    <title>Login Human Resources System ระบบทรัพยากรบุคคล</title>

    <!-- Bootstrap core CSS -->
    <link href="/Content/bootstrap.css" rel="stylesheet">
    <link href="/Content/font-awesome.css" rel="stylesheet" />
    <!-- Custom styles for this template -->
    <link href="/Content/signin.css" rel="stylesheet">
    <link href="/Content/material-dashboard.css" rel="stylesheet" />
</head>
<body class="text-center">
    <div class="container text-center">
        <div class="card form-signin">
            <div class="card-header card-header-warning card-header-icon" style="color: black;">
                <div class="card-icon">
                    <img class="mb-4" src="/Content/Images/j4.png" alt="" width="130" height="130">
                </div>
                <p class="card-category">
                    <h4>Human Resources System</h4>
                </p>
                <h2 class="h3 mb-3 font-weight-normal">ระบบทรัพยากรบุคคล</h2>
            </div>
            <div class="card-body table-responsive">

                <form class="formLogin" runat="server">
                    <asp:ScriptManager runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col">
                                    <asp:Label ID="msgBox" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text fa fa-user-circle-o" style="font-size: x-large; width: 50px;"></span>
                                        </div>
                                        <asp:TextBox ID="txtUser" runat="server" CssClass="form-control" placeholder="Username" MaxLength="20"></asp:TextBox>
                                    </div>
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text fa fa-unlock-alt" style="font-size: x-large; width: 50px;"></span>
                                        </div>
                                        <asp:TextBox TextMode="Password" ID="txtPass" runat="server" CssClass="form-control" placeholder="Password" MaxLength="20"></asp:TextBox><br />
                                    </div>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Login" CssClass="btn btn-warning col-6" />
                                </div>
                            </div>

                            <button type="button" class="btn btn-primary col-6" data-toggle="modal" data-target="#ModalLogin">
                                ใช้รหัสบัตรประชาชน
                            </button>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <!-- //////////////////////////////////////////////////////////////////////////////////////-->
                    <div class="modal fade" id="ModalLogin" tabindex="-1" role="dialog" aria-labelledby="ModalLogin" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">เข้าสู่ระบบ</h4>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md">
                                            <label class="col-form-label">รหัสบัตรประจำตัวประชาชน </label>
                                            <p class="text-danger" style="font-size: medium;">***รหัสบัตรประจำตัวประชาชน 13 หลัก ไม่ต้องใส่ขีด</p>
                                            <asp:TextBox ID="txtLoginIDCard" runat="server" CssClass="form-control" MaxLength="13"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">ยกเลิก</button>
                                    <asp:Button ID="btnLoginEmp" runat="server" Text="เข้าสู่ระบบ" CssClass="btn btn-warning" OnClick="btnLoginEmp_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
            <div class="card-footer">
                <div class="stats">
                    <p style="font-size: 18px; text-align: center;">&copy; <%=DateTime.Now.Year%> - ฝ่ายบริหารการจัดเก็บเงินค่าธรรมเนียม กองทางหลวงพิเศษระหว่าเมือง กรมทางหลวง </p>
                </div>
            </div>
        </div>
    </div>
    <script src="/Scripts/jquery-3.2.1.min.js"></script>
    <script src="/Scripts/popper.min.js"></script>
    <script src="/Scripts/bootstrap-material-design.min.js"></script>
</body>
</html>
