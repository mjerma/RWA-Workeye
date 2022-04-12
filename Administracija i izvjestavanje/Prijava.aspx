<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Prijava.aspx.cs" Inherits="Administracija_i_izvjestavanje.Prijava" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Prijava</title>
    <link href="~/Content/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <link href="~/Content/Prijava.css" rel="stylesheet" />
</head>
<body>
    <div class="global-container">
        <div class="card login-form">
            <div class="card-body">
                <h3 class="card-title text-center">Prijava korisnika</h3>
                <div class="card-text">
                    <form id="form1" runat="server">
                        <div>
                            <asp:Label Text="Email" runat="server" />
                            <asp:TextBox ID="tbEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="emailReqValidator"
                                ErrorMessage="Unesite email adresu"
                                ControlToValidate="tbEmail"
                                runat="server"
                                ForeColor="Red"
                                SetFocusOnError="True" />
                        </div>
                        <div>
                            <asp:Label Text="Zaporka" runat="server" />
                            <asp:TextBox ID="tbZaporka" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="zaporkaReqValidator"
                                ErrorMessage="Unesite zaporku"
                                ControlToValidate="tbZaporka"
                                runat="server"
                                ForeColor="Red"
                                SetFocusOnError="True" />
                            
                        </div>
                        <asp:Button ID="btnPrijava" Text="Prijava" CssClass="btn btn-primary btn-block" OnClick="btnPrijava_Click" runat="server" />
                        <asp:Label ID="lblPrijavaError" Text="" ForeColor="Red" runat="server" />
                    </form>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
