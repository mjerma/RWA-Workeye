<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Projekti.aspx.cs" Inherits="Administracija_i_izvjestavanje.Projekti" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="jumbotron">
            <h1 class="display-4 text-center">
                <asp:Label ID="lblProjektiHeader" Text="Projekti" runat="server" meta:resourcekey="lblProjektiHeaderResource1" /></h1>
        </div>
        <div class="form-group">
            <asp:LinkButton ID="btnDodajProjekt" CssClass="btn btn-outline-primary float-right mb-1" OnClick="btnDodajProjekt_Click" runat="server" meta:resourcekey="btnDodajProjektResource1">
                Dodaj projekt
                <i class="fas fa-plus"></i>
            </asp:LinkButton>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvProjekti" CssClass="table table-bordered table-striped" runat="server" OnRowDataBound="gvProjekti_RowDataBound"
                AllowPaging="True" AutoGenerateColumns="False" GridLines="None" DataKeyNames="IDProjekt" OnPageIndexChanging="gvProjekti_PageIndexChanging" meta:resourcekey="gvProjektiResource1" >
                <Columns>
                    <asp:BoundField DataField="Naziv" HeaderText="Naziv" meta:resourcekey="BoundFieldResource1"></asp:BoundField>
                    <asp:BoundField DataField="Voditelj" HeaderText="Voditelj" meta:resourcekey="BoundFieldResource2" />
                    <asp:BoundField DataField="Klijent" HeaderText="Klijent" meta:resourcekey="BoundFieldResource3" />
                    <asp:BoundField DataField="DatumKreiranja" HeaderText="Datum kreiranja" meta:resourcekey="BoundFieldResource4" />
                    <asp:BoundField meta:resourcekey="BoundFieldResource5" />
                </Columns>
                <HeaderStyle CssClass="thead-dark" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
