<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Klijenti.aspx.cs" Inherits="Administracija_i_izvjestavanje.Klijenti" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="klijenti-content" class="container">
        <div class="jumbotron">
            <h1 class="display-4 text-center">
                <asp:Label ID="lblKlijentiHeader" Text="Klijenti" runat="server" meta:resourcekey="lblKlijentiHeaderResource1" /></h1>
        </div>
        <div class="form-group">
            <asp:LinkButton ID="btnDodajKlijenta" CssClass="btn btn-outline-primary float-right mb-1" OnClick="btnDodajKlijenta_Click" runat="server" meta:resourcekey="btnDodajKlijentaResource1">
                Dodaj klijenta
                <i class="fas fa-plus"></i>
            </asp:LinkButton>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvKlijenti" CssClass="table table-bordered table-striped" runat="server" OnRowDataBound="gvKlijenti_RowDataBound"
                AllowPaging="True" AutoGenerateColumns="False" GridLines="None" DataKeyNames="IDKlijent" OnPageIndexChanging="gvKlijenti_PageIndexChanging" meta:resourcekey="gvKlijentiResource1" >
                <Columns>
                    <asp:BoundField DataField="Naziv" HeaderText="Naziv" meta:resourcekey="BoundFieldResource1"></asp:BoundField>
                    <asp:BoundField DataField="Telefon" HeaderText="Telefon" meta:resourcekey="BoundFieldResource2" />
                    <asp:BoundField DataField="Email" HeaderText="Email" meta:resourcekey="BoundFieldResource3" />
                    <asp:BoundField meta:resourcekey="BoundFieldResource4" />
                </Columns>
                <HeaderStyle CssClass="thead-dark" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
