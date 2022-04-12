<%@ Page Title="" Language="C#" MasterPageFile="~/FormeEntitetaSite.Master" AutoEventWireup="true" CodeBehind="IzvjestajZaKlijentaForma.aspx.cs" Inherits="Administracija_i_izvjestavanje.IzvjestajZaKlijentaForma" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="jumbotron">
            <h1 class="display-4 text-center">
                <asp:Label ID="lblIzvjestajKlijentHeader" Text="Izvještaj za klijenta" runat="server" meta:resourcekey="lblIzvjestajKlijentHeaderResource1" /></h1>
        </div>
        <div class="form-group">
            <asp:label ID="lblKlijent" text="Klijent" runat="server" meta:resourcekey="lblKlijentResource1" />
            <asp:CompareValidator 
                ID="klijentCompareValidator" 
                runat="server" 
                ControlToValidate="ddlKlijenti"
                ErrorMessage="*" 
                Operator="NotEqual" 
                ValueToCompare="NA" 
                SetFocusOnError="True"
                ForeColor="Red" meta:resourcekey="klijentCompareValidatorResource1"/>
            <asp:DropDownList runat="server" ID="ddlKlijenti" CssClass="form-control" meta:resourcekey="ddlKlijentiResource1" />
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
                    ForeColor="Red"
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
                    Type="Date" meta:resourcekey="zavrsniDatumCompareValidatorResource1" />
            </div>
        </div>
        <div class="form-row">
            <div class="form-group ml-auto">
                <asp:Button ID="btnPrikazi" Text="Prikaži" CssClass="btn btn-primary" OnClick="btnPrikazi_Click" runat="server" meta:resourcekey="btnPrikaziResource1" />
            </div>
        </div>
    </div>
</asp:Content>
