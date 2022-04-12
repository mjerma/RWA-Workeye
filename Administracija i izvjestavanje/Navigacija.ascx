<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navigacija.ascx.cs" Inherits="Administracija_i_izvjestavanje.Navigacija" %>

<nav class="navbar navbar-expand-lg navbar-dark bg-dark">
  <a class="navbar-brand pr-4 py-3" href="#">Workeye</a>
  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
    <span class="navbar-toggler-icon"></span>
  </button>
  <div class="collapse navbar-collapse" id="navbarNavDropdown">
    <ul class="navbar-nav">
      <li class="nav-item dropdown px-2">
        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdownMenuLink" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            <i class="fas fa-user-plus"></i>
            <asp:label ID="lblUnosAzuriranjeDropdown" text="Unos i ažuriranje" runat="server" meta:resourcekey="lblUnosAzuriranjeResource1" />
        </a>
        <div class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
          <a class="dropdown-item" href="Djelatnici.aspx"><asp:label ID="lblDjelatnikDropdown" text="Djelatnik" runat="server" meta:resourcekey="lblDjelatnikDropdownResource1" /></a>
          <a class="dropdown-item" href="Timovi.aspx"><asp:label ID="lblTimDropdown" text="Tim" runat="server" meta:resourcekey="lblTimDropdownResource1" /></a>
          <a class="dropdown-item" href="Projekti.aspx"><asp:label ID="lblProjektDropdown" text="Projekt" runat="server" meta:resourcekey="lblProjektDropdownResource1" /></a>
          <a class="dropdown-item" href="Klijenti.aspx"><asp:label ID="lblKlijentDropdown" text="Klijent" runat="server" meta:resourcekey="lblKlijentDropdownResource1" /></a>
        </div>
      </li>
      <li class="nav-item dropdown px-2">
        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdownMenuLink" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            <i class="fas fa-file-alt"></i>
            <asp:label ID="lblIzvjestavanjeDropdown" text="Izvještavanje" runat="server" meta:resourcekey="lblIzvjestavanjeDropdownResource1" />
        </a>
        <div class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
          <a class="dropdown-item" href="IzvjestajZaKlijentaForma.aspx"><asp:label ID="lblIzvjestajKlijentDropdown" text="Izvještaj za klijenta" runat="server" meta:resourcekey="lblIzvjestajKlijentDropdownResource1" /></a>
          <a class="dropdown-item" href="IzvjestajTimaForma.aspx"><asp:label ID="lblIzvjestajTimDropdown" text="Izvještaj o radnim satima tima" runat="server" meta:resourcekey="lblIzvjestajTimDropdownResource1" /></a>
        </div>
      </li>
    </ul>
    <ul class="navbar-nav ml-auto">
        <li class="nav-item dropdown px-2">
        <a class="nav-link dropdown-toggle" href="#"  role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
             <asp:label ID="lblJezikDropdown" text="Jezik" runat="server" meta:resourcekey="lblJezikDropdownResource1" />
        </a>
        <div class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
          <asp:LinkButton ID="btnHR" class="dropdown-item" OnClick="btnHR_Click" Text="Hrvatski" CausesValidation="false" runat="server" meta:resourcekey="btnHRResource1"/>
          <asp:LinkButton ID="btnEN" class="dropdown-item" OnClick="btnEN_Click" Text="Engleski" CausesValidation="false" runat="server" meta:resourcekey="btnENResource1" />
        </div>
      </li>
        <li class="nav-item px-2">
            <a class="nav-link" href="Prijava.aspx"><asp:label ID="lblOdjava" text="Odjava" runat="server" meta:resourcekey="lblOdjavaResource1" /></a>
        </li>
    </ul>
  </div>
</nav>
