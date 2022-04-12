<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Timovi.aspx.cs" Inherits="Administracija_i_izvjestavanje.Timovi" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="timovi-content" class="container">
        <div class="jumbotron">
            <h1 class="display-4 text-center">
                <asp:Label ID="lblTimoviHeader" Text="Timovi" runat="server" meta:resourcekey="lblTimoviHeaderResource1" /></h1>
        </div>
        <div class="form-group">
            <asp:LinkButton ID="btnDodajTim" CssClass="btn btn-outline-primary float-right mb-1" OnClick="btnDodajTim_Click" runat="server" meta:resourcekey="btnDodajTimResource1">
                Dodaj tim
                <i class="fas fa-plus"></i>
            </asp:LinkButton>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvTimovi" CssClass="table table-bordered table-striped" runat="server" OnRowDataBound="gvTimovi_RowDataBound"
                AllowPaging="True" AutoGenerateColumns="False" GridLines="None" DataKeyNames="IDTim" OnPageIndexChanging="gvTimovi_PageIndexChanging" meta:resourcekey="gvTimoviResource1">
                <Columns>
                    <asp:BoundField DataField="Naziv" HeaderText="Naziv" meta:resourcekey="BoundFieldResource1"></asp:BoundField>
                    <asp:BoundField DataField="Voditelj" HeaderText="Voditelj" meta:resourcekey="BoundFieldResource2" />
                    <asp:BoundField DataField="DatumKreiranja" HeaderText="Datum kreiranja" meta:resourcekey="BoundFieldResource3" />
                    <asp:BoundField meta:resourcekey="BoundFieldResource4" />
                </Columns>
                <HeaderStyle CssClass="thead-dark" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
