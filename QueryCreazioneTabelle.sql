--CREATE TABLE Cliente (
--    Id INT PRIMARY KEY IDENTITY,
--    CodiceFiscale CHAR(16) NOT NULL,
--    Cognome NVARCHAR(50) NOT NULL,
--    Nome NVARCHAR(50) NOT NULL,
--    Citta NVARCHAR(100) NOT NULL,
--    Provincia NVARCHAR(50) NOT NULL,
--    Email NVARCHAR(100),
--    Telefono NVARCHAR(20),
--    Cellulare NVARCHAR(20)
--)

--CREATE TABLE Camera (
--    Id INT PRIMARY KEY IDENTITY,
--    Numero INT NOT NULL,
--    Descrizione NVARCHAR(100),
--    Tipologia NVARCHAR(50)
--)

--CREATE TABLE Prenotazione (
--    Id INT PRIMARY KEY IDENTITY,
--    DataPrenotazione DATE NOT NULL,
--    NumeroProgressivoAnno INT NOT NULL,
--    Anno INT NOT NULL,
--    PeriodoDal DATE NOT NULL,
--    PeriodoAl DATE NOT NULL,
--    CaparraConfirmatoria MONEY,
--    TariffaApplicata MONEY NOT NULL,
--    TipoSoggiorno NVARCHAR(50) NOT NULL,
--    ClienteId INT FOREIGN KEY REFERENCES Cliente(Id),
--    CameraId INT FOREIGN KEY REFERENCES Camera(Id)
--)

-- Creazione della tabella ServiziAggiuntiviDisponibili
--select * from ServiziAggiuntiviDisponibili
--CREATE TABLE ServiziAggiuntiviDisponibili (
--    Id INT PRIMARY KEY IDENTITY,
--    Descrizione NVARCHAR(50),
--    Prezzo MONEY NOT NULL
--);
--select * from ServizioAggiuntivoPrenotazione
--CREATE TABLE ServizioAggiuntivoPrenotazione (
--    Id INT PRIMARY KEY IDENTITY,
--    Data DATE NOT NULL,
--    Quantita INT NOT NULL,
--    PrenotazioneId INT FOREIGN KEY REFERENCES Prenotazione(Id),
--	ServizioAggiuntivoId INT FOREIGN KEY REFERENCES ServiziAggiuntiviDisponibili(Id)
--);

--insert into ServizioAggiuntivoPrenotazione ('Data','Quantita','PrenotazioneId','ServizioAggiuntivoId') values ('2024-12-12',1,1,1)
INSERT INTO PrenotazioneServizioAggiuntivo (PrenotazioneId, ServizioAggiuntivoId, Quantita,Data)
VALUES (1,1,1,'2024-03-08')

 
--select * from ServizioAggiuntivoPrenotazione
--ServiziAggiuntiviDisponibili as P
--inner join 
--ServizioAggiuntivoPrenotazione as D
--on P.Id = D.PrenotazioneId

-- Inserimento di 10 record di esempio
--INSERT INTO ServiziAggiuntiviDisponibili (Descrizione, Prezzo)
--VALUES
--    ('Accesso alla piscina', 15.00),
--    ('Servizio di lavanderia', 20.00),
--    ('Colazione continentale', 10.00),
--    ('Parcheggio privato', 12.00),
--    ('Wi-Fi ad alta velocità', 5.00),
--    ('Servizio in camera', 25.00),
--    ('Palestra', 10.00),
--    ('Navetta per aeroporto', 30.00),
--    ('Spa e trattamenti benessere', 50.00),
--    ('Escursioni guidate', 40.00);

--CREATE TABLE Dipendenti (
--Id INT PRIMARY KEY IDENTITY,
--username NVARCHAR(50) NOT NULL,
--password NVARCHAR(50) NOT NULL,
--role NVARCHAR(50) NOT NULL,
--)

----insert into Dipendenti (username,password,role) values ('lobbia','12345','admin')

--SELECT * FROM Prenotazione WHERE TipoSoggiorno = 'Pensione completa'
