<%@ Page Title="" Language="C#" MasterPageFile="~/FormeEntitetaSite.Master" AutoEventWireup="true" CodeBehind="IzvjestajTimaForma.aspx.cs" Inherits="Administracija_i_izvjestavanje.IzvjestajTimaForma" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="jumbotron">
            <h1 class="display-4 text-center">
                <asp:Label ID="lblIzvjestajRadnihSatiHeader" Text="Izvještaj radnih sati tima" runat="server" meta:resourcekey="lblIzvjestajRadnihSatiHeaderResource1" /></h1>
        </div>
        <div class="form-group">
            <asp:Label Text="Tim" runat="server" meta:resourcekey="LabelResource1" />
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
        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lblPocetniDatum" Text="Početni datum" runat="server" meta:resourcekey="lblPocetniDatumResource1" />
                <asp:RequiredFieldValidator
                    ErrorMessage="*"
                    ControlToValidate="txtPocetniDatum"
                    runat="server"
                    ForeColor="Red"
                    SetFocusOnError="True"
                    CssClass="error-text" meta:resourcekey="RequiredFieldValidatorResource1" />
                <asp:TextBox ID="txtPocetniDatum" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtPocetniDatumResource1"></asp:TextBox>
                <asp:CompareValidator 
                    ID="pocetniDatumCompareValidator" 
                    runat="server"
                    ControlToValidate="txtPocetniDatum"
                    ErrorMessage="Unesite ispravni datum"
                    ForeColor="Red"
                    Operator="DataTypeCheck"
                    Type="Date" meta:resourcekey="pocetniDatumCompareValidatorResource1"/>
            </div>
            <div class="form-group col-md-6">
                <asp:Label ID="lblZavrsniDatum" Text="Završni datum" runat="server" meta:resourcekey="lblZavrsniDatumResource1" />
                <asp:RequiredFieldValidator
                    ErrorMessage="*"
                    ControlToValidate="txtZavrsniDatum"
                    runat="server"
                    SetFocusOnError="True"
                    CssClass="error-text" meta:resourcekey="RequiredFieldValidatorResource2" />
                <asp:TextBox ID="txtZavrsniDatum" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtZavrsniDatumResource1"></asp:TextBox>
                <asp:CompareValidator 
                    ID="zavrsniDatumCompareValidator" 
                    runat="server"
                    ControlToValidate="txtZavrsniDatum"
                    ErrorMessage="Unesite ispravni datum"
                    ForeColor="Red"
                    Operator="DataTypeCheck"
                    Type="Date" meta:resourcekey="zavrsniDatumCompareValidatorResource1"/>
            </div>
        </div>
        <div class="form-row">
            <div class="form-group ml-auto">
                <asp:Button ID="btnPrikazi" Text="Prikaži" CssClass="btn btn-primary" OnClick="btnPrikazi_Click" runat="server" meta:resourcekey="btnPrikaziResource1" />
            </div>
        </div>
    </div>
</asp:Content>
