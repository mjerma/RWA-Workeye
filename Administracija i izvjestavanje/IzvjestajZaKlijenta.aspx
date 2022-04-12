<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="IzvjestajZaKlijenta.aspx.cs" Inherits="Administracija_i_izvjestavanje.IzvjestajZaKlijenta" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/Izvjestaj.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div ID="izvjestaj-klijenta-content" class="container">
        <div class="jumbotron">
            <h1 id="klijentHeader" class="display-4 text-center" runat ="server"></h1>
            <h4 id="izvjestajPeriod" class="text-center" runat ="server"></h4>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvIzvjestaj" CssClass="table table-bordered table-striped" runat="server"
                AllowPaging="True" AutoGenerateColumns="False" GridLines="None" meta:resourcekey="gvIzvjestajResource1" >
                <Columns>
                    <asp:BoundField DataField="Naziv projekta" HeaderText="Naziv projekta" meta:resourcekey="BoundFieldResource1"></asp:BoundField>
                    <asp:BoundField DataField="Sati" HeaderText="Sati" meta:resourcekey="BoundFieldResource2" />
                </Columns>
                <HeaderStyle CssClass="thead-dark" />
            </asp:GridView>
            <div class="izvoz-div">
                <asp:Button ID="btnIzvoz" Text="Izvoz" CssClass="btn btn-secondary" OnClick="btnIzvoz_Click" runat="server" meta:resourcekey="btnIzvozResource1" />
            </div>
        </div>
    </div>
</asp:Content>
