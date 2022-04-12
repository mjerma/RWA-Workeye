<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Djelatnici.aspx.cs" Inherits="Administracija_i_izvjestavanje.Djelatnici" culture="auto" meta:resourcekey="PageResource1" uiculture="auto"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="jumbotron">
            <h1 class="display-4 text-center">
                <asp:Label ID="lblDjelatniciHeader" Text="Djelatnici" runat="server" meta:resourcekey="lblDjelatniciHeaderResource1" /></h1>
        </div>
        <div class="form-group">
            <asp:LinkButton ID="btnDodajDjelatnika" CssClass="btn btn-outline-primary float-right mb-1" OnClick="btnDodajDjelatnika_Click" runat="server" meta:resourcekey="btnDodajDjelatnikaResource1">
                Dodaj djelatnika
                <i class="fas fa-plus"></i>
            </asp:LinkButton>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvDjelatnici" CssClass="table table-bordered table-striped" OnRowDataBound="gvDjelatnici_RowDataBound" runat="server"
                AllowPaging="True" AutoGenerateColumns="False" GridLines="None" OnPageIndexChanging="gvDjelatnici_PageIndexChanging" DataKeyNames="IDDjelatnik" meta:resourcekey="gvDjelatniciResource1">
                <Columns>
                    <asp:BoundField DataField="Ime" HeaderText="Ime" meta:resourcekey="BoundFieldResource1"></asp:BoundField>
                    <asp:BoundField DataField="Prezime" HeaderText="Prezime" meta:resourcekey="BoundFieldResource2"/>
                    <asp:BoundField DataField="E-mail" HeaderText="E-mail" meta:resourcekey="BoundFieldResource3" />
                    <asp:BoundField DataField="Datum zaposlenja" HeaderText="Datum zaposlenja" meta:resourcekey="BoundFieldResource4"/>
                    <asp:BoundField DataField="Tip djelatnika" HeaderText="Tip djelatnika" meta:resourcekey="BoundFieldResource5"/>
                    <asp:BoundField DataField="Tim" HeaderText="Tim" meta:resourcekey="BoundFieldResource6"/>
                    <asp:BoundField meta:resourcekey="BoundFieldResource7"/>
                </Columns>
                <HeaderStyle CssClass="thead-dark" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
