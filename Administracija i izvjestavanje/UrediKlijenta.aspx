<%@ Page Title="" Language="C#" MasterPageFile="~/FormeEntitetaSite.Master" AutoEventWireup="true" CodeBehind="UrediKlijenta.aspx.cs" Inherits="Administracija_i_izvjestavanje.UrediKlijenta" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="jumbotron">
            <h1 class="display-4 text-center">
                <asp:Label ID="lblUrediKlijentaHeader" Text="Uredi klijenta" runat="server" meta:resourcekey="lblUrediKlijentaHeaderResource1" /></h1>
        </div>
        <div class="form-klijent">
            <div class="form-group">
                <asp:Label ID="lblNaziv" Text="Naziv" runat="server" meta:resourcekey="lblNazivResource1" />
                <asp:RequiredFieldValidator
                    ID="nazivReqValidator"
                    ErrorMessage="*"
                    ControlToValidate="txtNaziv"
                    runat="server"
                    ForeColor="Red"
                    SetFocusOnError="True" meta:resourcekey="nazivReqValidatorResource1" />
                <asp:TextBox ID="txtNaziv" runat="server" CssClass="form-control" meta:resourcekey="txtNazivResource1" />
                <asp:RegularExpressionValidator
                    ID="nazivRegexValidator"
                    runat="server"
                    ErrorMessage="Naziv ne može sadržavati posebne znakove"
                    ForeColor="Red"
                    SetFocusOnError="True"
                    Display="Dynamic"
                    ControlToValidate="txtNaziv"
                    ValidationExpression="^[0-9a-zA-Z .]+$" meta:resourcekey="nazivRegexValidatorResource1" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblTelefon" Text="Telefon" runat="server" meta:resourcekey="lblTelefonResource1" />
                <asp:RequiredFieldValidator
                    ID="telefonReqValidator"
                    ErrorMessage="*"
                    ControlToValidate="txtTelefon"
                    runat="server"
                    ForeColor="Red"
                    SetFocusOnError="True" meta:resourcekey="telefonReqValidatorResource1" />
                <asp:TextBox ID="txtTelefon" runat="server" CssClass="form-control" meta:resourcekey="txtTelefonResource1" />
                <asp:RegularExpressionValidator
                    ID="telefonRegexValidator"
                    runat="server"
                    ErrorMessage="Telefon ne može sadržavati posebne znakove ili slova"
                    ForeColor="Red"
                    SetFocusOnError="True"
                    Display="Dynamic"
                    ControlToValidate="txtTelefon"
                    ValidationExpression="^\d+$" meta:resourcekey="telefonRegexValidatorResource1" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblEmail" Text="Email" runat="server" meta:resourcekey="lblEmailResource1" />
                <asp:RequiredFieldValidator
                    ID="emailReqValidator"
                    ErrorMessage="*"
                    ControlToValidate="txtEmail"
                    runat="server"
                    ForeColor="Red"
                    SetFocusOnError="True" meta:resourcekey="emailReqValidatorResource1" />
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" meta:resourcekey="txtEmailResource1"></asp:TextBox>
            </div>
            <div class="form-row">
                <div class="form-group ml-auto">
                    <asp:Button ID="btnOdustani" Text="Odustani" CssClass="btn btn-outline-primary" OnClick="btnOdustani_Click" runat="server" CausesValidation="False" meta:resourcekey="btnOdustaniResource1" />
                    <asp:Button ID="btnSpremi" Text="Spremi" CssClass="btn btn-primary" OnClick="btnSpremi_Click" runat="server" meta:resourcekey="btnSpremiResource1" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
