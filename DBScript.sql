CREATE DATABASE Workeye
GO
USE Workeye
GO

CREATE TABLE TipDjelatnika
(
	IDTipDjelatnika INT PRIMARY KEY IDENTITY NOT NULL,
	Naziv NVARCHAR(50) NOT NULL,
)
GO

CREATE TABLE Tim
(
	IDTim INT PRIMARY KEY IDENTITY NOT NULL,
	Naziv NVARCHAR(50) NOT NULL,
	DatumKreiranja DATE NOT NULL,
	JeAktivan BIT
)
GO

CREATE TABLE Djelatnik
(
	IDDjelatnik INT PRIMARY KEY IDENTITY NOT NULL,
	Ime NVARCHAR(50) NOT NULL,
	Prezime NVARCHAR(50) NOT NULL,
	Email NVARCHAR(50) NOT NULL,
	DatumZaposlenja DATETIME2 (7) NOT NULL,
	Zaporka NVARCHAR(50) NOT NULL,
	TipDjelatnikaID INT FOREIGN KEY REFERENCES TipDjelatnika(IDTipDjelatnika) NOT NULL,
	TimID INT FOREIGN KEY REFERENCES Tim(IDTim),
	JeAktivan BIT
)
GO

CREATE TABLE Klijent
(
	IDKlijent INT PRIMARY KEY IDENTITY NOT NULL,
	Naziv NVARCHAR(50) NOT NULL,
	Telefon NVARCHAR(50) NOT NULL,
	Email NVARCHAR(50),
	JeAktivan BIT
)
GO

CREATE TABLE Projekt
(
	IDProjekt INT PRIMARY KEY IDENTITY NOT NULL,
	Naziv NVARCHAR(50) NOT NULL,
	KlijentID INT FOREIGN KEY REFERENCES Klijent(IDKlijent) NOT NULL,
	DatumOtvaranja DATE NOT NULL,
	VoditeljProjektaID INT FOREIGN KEY REFERENCES Djelatnik(IDDjelatnik) NOT NULL,
	JeAktivan BIT
)
GO

CREATE TABLE ProjektDjelatnik
(
	IDProjektDjelatnik INT PRIMARY KEY IDENTITY NOT NULL,
	ProjektID INT FOREIGN KEY REFERENCES Projekt(IDProjekt) NOT NULL,
	DjelatnikID INT FOREIGN KEY REFERENCES Djelatnik(IDDjelatnik) NOT NULL,
)
GO

CREATE TABLE Satnica
(
	IDSatnica INT PRIMARY KEY IDENTITY NOT NULL,
	DatumSatnice DATETIME2 (7) NOT NULL,
	DatumSlanja  DATETIME2 (7),
	ZabiljezenoUkupno NVARCHAR(50),
	RadniSatiUkupno INT NOT NULL,
	PrekovremeniSatiUkupno INT NOT NULL,
	Komentar NVARCHAR(200),
	DjelatnikID INT FOREIGN KEY REFERENCES Djelatnik(IDDjelatnik) NOT NULL,
	VoditeljID INT FOREIGN KEY REFERENCES Djelatnik(IDDjelatnik) NOT NULL,
	JePoslano BIT NOT NULL,
	JePotvrdeno BIT NOT NULL
)
GO

CREATE TABLE ProjektSatnica
(
	IDProjektSatnica INT PRIMARY KEY IDENTITY NOT NULL,
	Zabiljezeno NVARCHAR(50),
	RadniSati INT,
	PrekovremeniSati INT,
	ProjektID INT FOREIGN KEY REFERENCES Projekt(IDProjekt) NOT NULL,
	SatnicaID INT FOREIGN KEY REFERENCES Satnica(IDSatnica) NOT NULL
)
GO

CREATE PROCEDURE GetDjelatnici
AS 
BEGIN
	SELECT *FROM Djelatnik
END
GO

CREATE PROCEDURE GetDjelatnik
	@DjelatnikID int
AS 
BEGIN
	SELECT *FROM Djelatnik
	WHERE IDDjelatnik = @DjelatnikID
END
GO

CREATE PROCEDURE GetDirektor
AS 
BEGIN
	SELECT *FROM Djelatnik
	WHERE TipDjelatnikaID = 1
END
GO

CREATE PROCEDURE GetDjelatnikProjekti
	@DjelatnikID INT
AS 
BEGIN
	SELECT Projekt.* FROM Projekt
	INNER JOIN ProjektDjelatnik
	ON IDProjekt = ProjektID
	WHERE @DjelatnikID = DjelatnikID
END
GO

CREATE PROCEDURE GetVoditeljTima
	@TimID INT
AS
BEGIN 
	select Djelatnik.*from Djelatnik
	inner join Tim 
	on IDTim = TimID
	WHERE TipDjelatnikaID = 2 and TimID = @TimID
END
GO

CREATE PROCEDURE GetTimovi
AS 
BEGIN
	SELECT *FROM Tim
END
GO

CREATE PROCEDURE GetTim
	@TimID INT
AS 
BEGIN
	SELECT *FROM Tim
	WHERE @TimID = IDTim
END
GO

CREATE PROCEDURE GetTipoviDjelatnika
AS 
BEGIN
	SELECT * FROM TipDjelatnika
END
GO

CREATE PROCEDURE GetTipDjelatnika
	@TipDjelatnikaID INT
AS 
BEGIN
	SELECT *FROM TipDjelatnika
	WHERE @TipDjelatnikaID = IDTipDjelatnika
END
GO

CREATE PROCEDURE GetProjekti
AS 
BEGIN
	SELECT *FROM Projekt
END
GO

CREATE PROCEDURE GetProjekt
	@ProjektID int
AS 
BEGIN
	SELECT *FROM Projekt
	WHERE IDProjekt = @ProjektID
END
GO

CREATE PROCEDURE GetVoditelji
AS
BEGIN
	SELECT *FROM Djelatnik
	WHERE TipDjelatnikaID = 2
END
GO

CREATE PROCEDURE GetKlijenti
AS
BEGIN
	SELECT *FROM Klijent
END
GO

CREATE PROCEDURE GetKlijent
	@KlijentID int
AS
BEGIN
	SELECT *FROM Klijent
	WHERE IDKlijent = @KlijentID
END
GO

CREATE PROCEDURE GetSatnica
	@SatnicaID int
AS
BEGIN
	SELECT *FROM Satnica
	WHERE IDSatnica = @SatnicaID
END
GO

CREATE PROCEDURE GetDjelatnikSatnica
	@Datum DATETIME2 (7),
	@DjelatnikID INT
AS
BEGIN
	SELECT *FROM Satnica
	WHERE DatumSatnice = @Datum and DjelatnikID = @DjelatnikID
END
GO

CREATE PROCEDURE GetProjektiSatnice
	@SatnicaID int
AS
BEGIN
	SELECT *FROM ProjektSatnica
	WHERE SatnicaID = @SatnicaID
END
GO

CREATE PROCEDURE GetSatniceZaPotvrdu
	@VoditeljID int
AS
BEGIN
	SELECT *FROM Satnica
	WHERE VoditeljID = @VoditeljID and JePoslano = 1 and JePotvrdeno = 0
END
GO

CREATE PROCEDURE GetIzvjestajZaKlijenta
	@KlijentID int,
	@PocetniDatum DATETIME2 (7),
	@ZavrsniDatum DATETIME2 (7)
AS
BEGIN
	SELECT p.Naziv as Naziv, sum(ps.RadniSati + ps.PrekovremeniSati) as Sati
	FROM Projekt AS p
	INNER JOIN Klijent AS k	
	ON p.KlijentID = k.IDKlijent
	INNER JOIN ProjektSatnica AS ps
	ON ps.ProjektID = p.IDProjekt
	INNER JOIN Satnica AS s
	ON s.IDSatnica = ps.SatnicaID
	WHERE IDKlijent = @KlijentID
	and s.DatumSatnice >= @PocetniDatum and s.DatumSatnice <= @ZavrsniDatum
	GROUP BY p.Naziv
END
GO

CREATE PROCEDURE GetIzvjestajTima
	@TimID int,
	@PocetniDatum DATETIME2 (7),
	@ZavrsniDatum DATETIME2 (7)
AS
BEGIN
	SELECT d.IDDjelatnik, d.TipDjelatnikaID ,sum(s.RadniSatiUkupno) as RedovniRadniSati, sum(s.PrekovremeniSatiUkupno) as PrekovremeniSati
	FROM Djelatnik AS d
	INNER JOIN Tim AS t
	ON t.IDTim = d.TimID
	INNER JOIN Satnica AS s
	ON s.DjelatnikID = d.IDDjelatnik
	WHERE IDTim = @TimID and s.DatumSatnice between @PocetniDatum and @ZavrsniDatum
	GROUP BY d.IDDjelatnik, d.TipDjelatnikaID
END
GO

CREATE PROCEDURE CreateDjelatnik
	@Ime NVARCHAR(50),
	@Prezime NVARCHAR(50),
	@Email NVARCHAR(50),
	@DatumZaposlenja DATETIME2 (7),
	@Zaporka NVARCHAR(30),
	@TipDjelatnikaID INT,
	@TimID INT,
	@JeAktivan BIT
AS 
BEGIN
	INSERT INTO Djelatnik VALUES(@Ime, @Prezime, @Email, @DatumZaposlenja,@Zaporka,@TipDjelatnikaID, @TimID, @JeAktivan)
	SELECT CAST(scope_identity() AS int)
END
GO

CREATE PROCEDURE CreateTim
	@Naziv NVARCHAR(50),
	@DatumKreiranja DATETIME2 (7),
	@JeAktivan BIT
AS 
BEGIN
	INSERT INTO Tim VALUES(@Naziv, @DatumKreiranja, @JeAktivan)
END
GO

CREATE PROCEDURE CreateProjekt
	@Naziv NVARCHAR(50),
	@KlijentID INT,
	@DatumOtvaranja DATETIME2 (7),
	@VoditeljProjektaID INT,
	@JeAktivan BIT
AS 
BEGIN
	INSERT INTO Projekt VALUES(@Naziv, @KlijentID, @DatumOtvaranja, @VoditeljProjektaID, @JeAktivan)
END
GO

CREATE PROCEDURE CreateKlijent
	@Naziv NVARCHAR(50),
	@Telefon NVARCHAR(50),
	@Email NVARCHAR(50)
AS 
BEGIN
	INSERT INTO Klijent VALUES(@Naziv, @Telefon, @Email, 1)
END
GO

CREATE PROCEDURE CreateSatnica
	@DatumSatnice DATETIME2 (7),
	@DatumSlanja DATETIME2 (7),
	@ZabiljezenoUkupno NVARCHAR(50),
	@RadniSatiUkupno INT,
	@PrekovremeniSatiUkupno INT,
	@Komentar NVARCHAR(200),
	@DjelatnikID INT,
	@VoditeljID INT,
	@JePoslano BIT, 
	@JePotvrdeno BIT
AS 
BEGIN
	INSERT INTO Satnica VALUES(@DatumSatnice, @DatumSlanja, @ZabiljezenoUkupno, @RadniSatiUkupno, @PrekovremeniSatiUkupno, 
	@Komentar, @DjelatnikID, @VoditeljID, @JePoslano, @JePotvrdeno)
	SELECT CAST(scope_identity() AS int)
END
GO

CREATE PROCEDURE CreateProjektSatnica
	@Zabiljezeno NVARCHAR(50),
	@RadniSati INT,
	@PrekovremeniSati INT,
	@ProjektID INT,
	@SatnicaID INT
AS 
BEGIN
	INSERT INTO ProjektSatnica VALUES(@Zabiljezeno, @RadniSati, @PrekovremeniSati, @ProjektID, @SatnicaID)
END
GO

CREATE PROCEDURE UpdateDjelatnik
	@DjelatnikID INT,
	@Ime NVARCHAR(50),
	@Prezime NVARCHAR(50),
	@Email NVARCHAR(50),
	@TipDjelatnikaID INT,
	@TimID INT
AS 
BEGIN
	UPDATE Djelatnik SET
		Ime = @Ime,
		Prezime = @Prezime,
		Email = @Email,
		TipDjelatnikaID = @TipDjelatnikaID,
		TimID = @TimID
	WHERE IDDjelatnik = @DjelatnikID
END
GO

CREATE PROCEDURE UpdateTim
	@TimID INT,
	@Naziv NVARCHAR(50)
AS 
BEGIN
	UPDATE Tim SET
		Naziv = @Naziv
	WHERE IDTim = @TimID
END
GO

CREATE PROCEDURE UpdateProjekt
	@ProjektID INT,
	@Naziv NVARCHAR(50),
	@KlijentID INT,
	@VoditeljProjektaID INT
AS 
BEGIN
	UPDATE Projekt SET
		Naziv = @Naziv,
		KlijentID = @KlijentID,
		VoditeljProjektaID = @VoditeljProjektaID
	WHERE IDProjekt = @ProjektID
END
GO

CREATE PROCEDURE UpdateKlijent
	@KlijentID INT,
	@Naziv NVARCHAR(50),
	@Telefon NVARCHAR(50),
	@Email NVARCHAR(50)
AS 
BEGIN
	UPDATE Klijent SET
		Naziv = @Naziv,
		Telefon = @Telefon,
		Email = @Email
	WHERE IDKlijent = @KlijentID
END
GO

CREATE PROCEDURE UpdateZaporka
	@DjelatnikID INT,
	@Zaporka NVARCHAR(30)
AS 
BEGIN
	UPDATE Djelatnik SET
		Zaporka = @Zaporka
	WHERE IDDjelatnik = @DjelatnikID
END
GO

CREATE PROCEDURE UpdateDjelatnikJeAktivan
	@DjelatnikID INT,
	@JeAktivan BIT
AS 
BEGIN
	UPDATE Djelatnik SET
		JeAktivan = @JeAktivan
	WHERE IDDjelatnik = @DjelatnikID
END
GO

CREATE PROCEDURE UpdateTimJeAktivan
	@TimID INT,
	@JeAktivan BIT
AS 
BEGIN
	UPDATE Tim SET
		JeAktivan = @JeAktivan
	WHERE IDTim = @TimID
END
GO

CREATE PROCEDURE UpdateKlijentJeAktivan
	@KlijentID INT,
	@JeAktivan BIT
AS 
BEGIN
	UPDATE Klijent SET
		JeAktivan = @JeAktivan
	WHERE IDKlijent = @KlijentID
END
GO

CREATE PROCEDURE UpdateProjektJeAktivan
	@ProjektID INT,
	@JeAktivan BIT
AS 
BEGIN
	UPDATE Projekt SET
		JeAktivan = @JeAktivan
	WHERE IDProjekt = @ProjektID
END
GO

CREATE PROCEDURE UpdateSatnica
	@SatnicaID INT,
	@DatumSlanja DATETIME2 (7),
	@ZabiljezenoUkupno NVARCHAR(50),
	@RadniSatiUkupno INT,
	@PrekovremeniSatiUkupno INT,
	@Komentar NVARCHAR(200),
	@VoditeljID INT,
	@JePoslano BIT, 
	@JePotvrdeno BIT
AS 
BEGIN
	UPDATE Satnica SET
		DatumSlanja = @DatumSlanja,
		ZabiljezenoUkupno = @ZabiljezenoUkupno,
		RadniSatiUkupno = @RadniSatiUkupno,
		PrekovremeniSatiUkupno = @PrekovremeniSatiUkupno,
		Komentar = @Komentar,
		VoditeljID = @VoditeljID,
		JePoslano = @JePoslano,
		JePotvrdeno = @JePotvrdeno
	WHERE IDSatnica = @SatnicaID
END
GO

CREATE PROCEDURE UpdateProjektSatnica
    @Zabiljezeno NVARCHAR(50),
	@RadniSati INT,
	@PrekovremeniSati INT,
	@ProjektID INT,
	@SatnicaID INT
AS 
BEGIN
	UPDATE ProjektSatnica SET
		Zabiljezeno = @Zabiljezeno,
		RadniSati = @RadniSati,
		PrekovremeniSati = @PrekovremeniSati,
		ProjektID = @ProjektID,
		SatnicaID = @SatnicaID
	WHERE SatnicaID = @SatnicaID and ProjektID = @ProjektID
END
GO

CREATE PROCEDURE UpdateSatnicaJePoslano
	@SatnicaID INT,
	@Komentar NVARCHAR(200)
AS 
BEGIN
	UPDATE Satnica SET
		Komentar = @Komentar,
		JePoslano = 0
	WHERE IDSatnica = @SatnicaID
END
GO

CREATE PROCEDURE SpremiSatnicu
	@SatnicaID INT
AS 
BEGIN
	UPDATE Satnica SET
		JePotvrdeno = 1
	WHERE IDSatnica = @SatnicaID
END
GO

CREATE PROCEDURE DjelatnikJeAktivan
	@DjelatnikID INT
AS 
BEGIN
	SELECT JeAktivan FROM Djelatnik
	WHERE IDDjelatnik = @DjelatnikID
END
GO

CREATE PROCEDURE TimJeAktivan
	@TimID INT
AS 
BEGIN
	SELECT JeAktivan FROM Tim
	WHERE IDTim = @TimID
END
GO
 
CREATE PROCEDURE AddProjektDjelatnik
	@ProjektID INT,
	@DjelatnikID INT
AS 
BEGIN
	INSERT INTO ProjektDjelatnik VALUES(@ProjektID,@DjelatnikID)
END
GO			

CREATE PROCEDURE PrijavaKorisnika
	@Email nvarchar(50),
	@Zaporka nvarchar(50)
AS 
BEGIN
	SELECT *FROM Djelatnik
	WHERE Email = @Email AND Zaporka = @Zaporka
END
GO	

INSERT TipDjelatnika VALUES 
('Direktor'),
('Voditelj tima'),
('Zaposlenik'),
('Honorarni djelatnik'),
('Student')
GO

INSERT Tim VALUES 
('Tim Bube Ingenii', CAST(N'2016-04-02' AS Date), 1),
('Tim Čelični Ingenii', CAST(N'2012-06-20' AS Date), 1)
GO

INSERT Djelatnik VALUES 
('Miro', 'Miric', 'algebra@mail.com', '10.10.2005', 'algebra', '1', NULL, 1),
('Mia', 'Horvat', N'mhorvat@ingenii.hr', CAST(N'2018-02-28T00:00:00.0000000' AS DateTime2), N'123', 2, 1, 1),
('Sara', 'Kovačević', N'skovacevic@ingenii.hr', CAST(N'2019-11-30T00:00:00.0000000' AS DateTime2), N'aaa', 3, 1, 1),
('Ivan', 'Matić', N'imatic@ingenii.hr', CAST(N'2015-08-23T00:00:00.0000000' AS DateTime2), N'OY273H0CPP', 2, 2, 1),
('Matija', 'Ivić', N'mivic@ingenii.hr', CAST(N'2014-02-13T00:00:00.0000000' AS DateTime2), N'72W5W3', 3, 2, 1)
GO

INSERT Klijent VALUES 
('Mucrosoft d.o.o.', N'34634634', N'mucrosoft@mail.com', 1),
('Oricle d.o.o.', N'14325115', N'oricle@mail.com', 1)
GO

INSERT Projekt VALUES 
('Projekt Mucrosoft Prvi', 1, CAST(N'2010-01-01' AS Date), 2, 1),
('Projekt Oricle Prvi', 2, CAST(N'2010-01-31' AS Date), 4, 1)
GO

INSERT INTO ProjektDjelatnik VALUES
(1,2),
(1,3),
(2,4),
(2,5)
