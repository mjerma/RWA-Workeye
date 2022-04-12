<%@ Page Title="" Language="C#" MasterPageFile="~/FormeEntitetaSite.Master" AutoEventWireup="true" CodeBehind="DodajProjekt.aspx.cs" Inherits="Administracija_i_izvjestavanje.DodajProjekt" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="jumbotron">
            <h1 class="display-4 text-center">
                <asp:Label ID="lblDodajProjektHeader" Text="Dodaj projekt" runat="server" meta:resourcekey="lblDodajProjektHeaderResource1" /></h1>
        </div>
        <div class="form-projekt">
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
                    ValidationExpression="^[0-9a-zA-Z ]+$" meta:resourcekey="nazivRegexValidatorResource1" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblKlijent" Text="Klijent" runat="server" meta:resourcekey="lblKlijentResource1" />
                <asp:CompareValidator
                    ID="klijentCompareValidator"
                    runat="server"
                    ControlToValidate="ddlKlijenti"
                    ErrorMessage="*"
                    Operator="NotEqual"
                    ValueToCompare="NA"
                    ForeColor="Red"
                    SetFocusOnError="True" meta:resourcekey="klijentCompareValidatorResource1" />
                <asp:DropDownList runat="server" ID="ddlKlijenti" CssClass="form-control" AutoPostBack="True" meta:resourcekey="ddlKlijentiResource1" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblVoditeljProjekta" Text="Voditelj projekta" runat="server" meta:resourcekey="lblVoditeljProjektaResource1" />
                <asp:CompareValidator
                    ID="voditeljProjektaCompareValidator"
                    runat="server"
                    ControlToValidate="ddlVoditelji"
                    ErrorMessage="*"
                    Operator="NotEqual"
                    ValueToCompare="NA"
                    ForeColor="Red"
                    SetFocusOnError="True" meta:resourcekey="voditeljProjektaCompareValidatorResource1" />
                <asp:DropDownList runat="server" ID="ddlVoditelji" CssClass="form-control" AutoPostBack="True" meta:resourcekey="ddlVoditeljiResource1" />
            </div>
            <div class="form-row">
                <div class="form-group ml-auto">
                    <asp:Button ID="btnOdustani" Text="Odustani" CssClass="btn btn-outline-primary" runat="server" OnClick="btnOdustani_Click" CausesValidation="False" meta:resourcekey="btnOdustaniResource1" />
                    <asp:Button ID="btnSpremi" Text="Spremi" CssClass="btn btn-primary" OnClick="btnSpremi_Click" runat="server" meta:resourcekey="btnSpremiResource1" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
