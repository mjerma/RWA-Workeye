<%@ Page Title="" Language="C#" MasterPageFile="~/FormeEntitetaSite.master" AutoEventWireup="true" CodeBehind="UrediDjelatnika.aspx.cs" Inherits="Administracija_i_izvjestavanje.UrediDjelatnika" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="jumbotron">
            <h1 class="display-4 text-center">
                <asp:Label ID="lblUrediDjelatnikaHeader" Text="Uredi djelatnika" runat="server" meta:resourcekey="lblUrediDjelatnikaHeaderResource1" /></h1>
        </div>
        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lblIme" Text="Ime" runat="server" meta:resourcekey="lblImeResource1"/>
                <asp:RequiredFieldValidator
                    ID="imeReqValidator"
                    ErrorMessage="*"
                    ControlToValidate="txtIme"
                    runat="server"
                    ForeColor="Red"
                    SetFocusOnError="True" meta:resourcekey="imeReqValidatorResource1" />
                <asp:TextBox ID="txtIme" runat="server" CssClass="form-control" meta:resourcekey="txtImeResource1"></asp:TextBox>
            </div>
            <div class="form-group col-md-6">
                <asp:Label ID="lblPrezime" Text="Prezime" runat="server" meta:resourcekey="lblPrezimeResource1"/>
                <asp:RequiredFieldValidator
                    ID="prezimeReqValidator"
                    ErrorMessage="*"
                    ControlToValidate="txtPrezime"
                    runat="server"
                    ForeColor="Red"
                    SetFocusOnError="True" meta:resourcekey="prezimeReqValidatorResource1" />
                <asp:TextBox ID="txtPrezime" runat="server" CssClass="form-control" meta:resourcekey="txtPrezimeResource1"></asp:TextBox>
            </div>
        </div>
        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lblEmail" Text="Email" runat="server" meta:resourcekey="lblEmailResource1"/>
                <asp:RequiredFieldValidator
                    ID="emailReqValidator"
                    ErrorMessage="*"
                    ControlToValidate="txtEmail"
                    runat="server"
                    ForeColor="Red"
                    SetFocusOnError="True" meta:resourcekey="emailReqValidatorResource1" />
                <asp:TextBox ID="txtEmail" TextMode="Email" runat="server" CssClass="form-control" meta:resourcekey="txtEmailResource1"></asp:TextBox>
            </div>
            <div class="form-group col-md-6">
                <asp:Label ID="lblTipDjelatnika" Text="Tip djelatika" runat="server" meta:resourcekey="lblTipDjelatnikaResource1" />
                <asp:CompareValidator
                    ID="tipDjelatnikaCompareValidator"
                    runat="server"
                    ControlToValidate="ddlTipDjelatnika"
                    ErrorMessage="*"
                    Operator="NotEqual"
                    ValueToCompare="NA"
                    ForeColor="Red"
                    SetFocusOnError="True" meta:resourcekey="tipDjelatnikaCompareValidatorResource1" />
                <asp:DropDownList runat="server" ID="ddlTipDjelatnika" CssClass="form-control" meta:resourcekey="ddlTipDjelatnikaResource1" />
            </div>
        </div>
        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lblTim" Text="Tim" runat="server" meta:resourcekey="lblTimResource1" />
                <asp:CompareValidator
                    ID="timCompareValidator"
                    runat="server"
                    ControlToValidate="ddlTim"
                    ErrorMessage="*"
                    Operator="NotEqual"
                    ValueToCompare="NA"
                    ForeColor="Red"
                    SetFocusOnError="True" meta:resourcekey="timCompareValidatorResource1" />
                <asp:DropDownList runat="server" ID="ddlTim" CssClass="form-control" meta:resourcekey="ddlTimResource1" />
            </div>
            <div class="form-group col-md-6">
                <asp:Label ID="lblIzborProjekata" Text="Izbor projekata" runat="server" meta:resourcekey="lblIzborProjekataResource1" />
                <asp:DropDownList runat="server" ID="ddlProjekti" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlProjekti_SelectedIndexChanged" meta:resourcekey="ddlProjektiResource1" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="lblProjektiDjelatnika" Text="Projekti djelatnika" runat="server" meta:resourcekey="lblProjektiDjelatnikaResource1" />
            <asp:ListBox ID="lbProjekti" runat="server" SelectionMode="Multiple" CssClass="form-control mt-2" meta:resourcekey="lbProjektiResource1" />
        </div>
        <div class="form-row">
            <div class="form-group ml-auto">
                <asp:Button ID="btnOdustani" Text="Odustani" CssClass="btn btn-outline-primary" runat="server" OnClick="btnOdustani_Click" CausesValidation="False" meta:resourcekey="btnOdustaniResource1" />
                <asp:Button ID="btnPromjeniZaporku" Text="Promjeni zaporku" CssClass="btn btn-outline-primary" OnClick="btnPromjeniZaporku_Click" runat="server" meta:resourcekey="btnPromjeniZaporkuResource1" />
                <asp:Button ID="btnSpremi" Text="Spremi" CssClass="btn btn-primary" OnClick="btnSpremi_Click" runat="server" meta:resourcekey="btnSpremiResource1" />
            </div>
        </div>
        <asp:Panel ID="formPromjeniZaporku" runat="server" Visible="False" meta:resourcekey="formPromjeniZaporkuResource1">
            <div class="jumbotron">
                <h1 class="headerZaporka text-center">
                    <asp:Label ID="lblPromjenaZaporke" Text="Promjena zaporke" runat="server" meta:resourcekey="lblPromjenaZaporkeResource1" /></h1>
            </div>
            <div class="form-group">
                <asp:Label ID="lblNovaZaporka" Text="Nova zaporka" runat="server" meta:resourcekey="lblNovaZaporkaResource1" />
                <asp:TextBox ID="tbNovaZaporka" runat="server" CssClass="form-control" TextMode="Password" meta:resourcekey="tbNovaZaporkaResource1"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblPonoviNovuZaporku" Text="Ponovite novu zaporku" runat="server" meta:resourcekey="lblPonoviNovuZaporkuResource1" />
                <asp:TextBox ID="tbPonoviNovuZaporku" runat="server" CssClass="form-control" TextMode="Password" meta:resourcekey="tbPonoviNovuZaporkuResource1"></asp:TextBox>
                <asp:CompareValidator ID="zaporkaValidator" runat="server" ErrorMessage="Zaporke nisu jednake" ControlToCompare="tbNovaZaporka" ControlToValidate="tbPonoviNovuZaporku" ForeColor="Red" meta:resourcekey="zaporkaValidatorResource1"></asp:CompareValidator>
            </div>
            <div class="form-group">
                <asp:Button ID="btnSpremiZaporku" Text="Spremi" CssClass="btn btn-primary float-right" OnClick="btnSpremiZaporku_Click" runat="server" meta:resourcekey="btnSpremiZaporkuResource1" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
