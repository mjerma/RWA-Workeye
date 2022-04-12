<%@ Page Title="" Language="C#" MasterPageFile="~/FormeEntitetaSite.Master" AutoEventWireup="true" CodeBehind="DodajDjelatnika.aspx.cs" Inherits="Administracija_i_izvjestavanje.DodajDjelatnika" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="jumbotron">
            <h1 class="display-4 text-center">
                <asp:Label ID="lblDodajDjelatnikaHeader" Text="Dodaj djelatnika" runat="server" meta:resourcekey="lblDodajDjelatnikaHeaderResource1" /></h1>
        </div>
        <div class="form-djelatnik-content">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label ID="lblIme" Text="Ime" runat="server" meta:resourcekey="lblImeResource1" />
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
                    <asp:Label ID="lblPrezime" Text="Prezime" runat="server" meta:resourcekey="lblPrezimeResource1" />
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
                    <asp:Label ID="lblEmail" Text="Email" runat="server" meta:resourcekey="lblEmailResource1" />
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
                    <asp:Label ID="lblZaporka" Text="Zaporka" runat="server" meta:resourcekey="lblZaporkaResource1" />
                    <asp:RequiredFieldValidator
                        ID="zaporkaReqValidator"
                        ErrorMessage="*"
                        ControlToValidate="txtZaporka"
                        runat="server"
                        ForeColor="Red"
                        SetFocusOnError="True" meta:resourcekey="zaporkaReqValidatorResource1" />
                    <asp:TextBox ID="txtZaporka" runat="server" CssClass="form-control" TextMode="Password" meta:resourcekey="txtZaporkaResource1"></asp:TextBox>
                    <asp:RegularExpressionValidator
                        ID="zaporkaRegexValidator"
                        runat="server"
                        ErrorMessage="Zaporka ne može sadržavati posebne znakove i mora biti duljine između 3 i 20 znakova"
                        ForeColor="Red"
                        SetFocusOnError="True"
                        Display="Dynamic"
                        ControlToValidate="txtZaporka"
                        ValidationExpression="[a-zA-Z0-9]{3,20}" meta:resourcekey="zaporkaRegexValidatorResource1" />
                </div>
            </div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label ID="lblTipDjelatnika" Text="Tip djelatnika" runat="server" meta:resourcekey="lblTipDjelatnikaResource1" />
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
            </div>
            <div class="form-group">
                <asp:Label ID="lblProjekti" Text="Projekti" runat="server" meta:resourcekey="lblProjektiResource1" />
                <asp:DropDownList runat="server" ID="ddlProjekti" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlProjekti_SelectedIndexChanged" meta:resourcekey="ddlProjektiResource1" />
                <asp:ListBox ID="lbProjekti" runat="server" SelectionMode="Multiple" CssClass="form-control mt-2" meta:resourcekey="lbProjektiResource1" />
                <div class="form-djelatnik-btns mt-2">
                    <asp:Button ID="btnOdustani" Text="Odustani" CssClass="btn btn-outline-primary" runat="server" OnClick="btnOdustani_Click" CausesValidation="False" meta:resourcekey="btnOdustaniResource1" />
                    <asp:Button ID="btnSpremi" Text="Spremi" CssClass="btn btn-primary" OnClick="btnSpremi_Click" runat="server" meta:resourcekey="btnSpremiResource1" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

