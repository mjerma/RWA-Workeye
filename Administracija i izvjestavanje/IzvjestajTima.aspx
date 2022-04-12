<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="IzvjestajTima.aspx.cs" Inherits="Administracija_i_izvjestavanje.IzvjestajTima" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/Izvjestaj.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div ID="izvjestaj-tima-content" class="container">
        <div class="jumbotron">
            <h1 id="timHeader" class="display-4 text-center" runat ="server"></h1>
            <h4 id="izvjestajPeriod" class="text-center" runat ="server"></h4>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvIzvjestaj" CssClass="table table-bordered table-striped" runat="server"
                AllowPaging="True" AutoGenerateColumns="False" GridLines="None" meta:resourcekey="gvIzvjestajResource1" >
                <Columns>
                    <asp:BoundField DataField="Ime" HeaderText="Ime" meta:resourcekey="BoundFieldResource1"></asp:BoundField>
                    <asp:BoundField DataField="Prezime" HeaderText="Prezime" meta:resourcekey="BoundFieldResource2" />
                    <asp:BoundField DataField="Tip djelatnika" HeaderText="Tip djelatnika" meta:resourcekey="BoundFieldResource3" />
                    <asp:BoundField DataField="Redovni sati" HeaderText="Radni sati" meta:resourcekey="BoundFieldResource4" />
                    <asp:BoundField DataField="Prekovremeni sati" HeaderText="Prekovremeni sati" meta:resourcekey="BoundFieldResource5" />
                    <asp:BoundField DataField="Ukupno" HeaderText="Ukupno" meta:resourcekey="BoundFieldResource6" />
                </Columns>
                <HeaderStyle CssClass="thead-dark" />
            </asp:GridView>
            <div class="izvoz-div">
                <asp:Button ID="btnIzvoz" Text="Izvoz" CssClass="btn btn-secondary" OnClick="btnIzvoz_Click" runat="server" meta:resourcekey="btnIzvozResource1" />
            </div>
        </div>
    </div>
</asp:Content>
