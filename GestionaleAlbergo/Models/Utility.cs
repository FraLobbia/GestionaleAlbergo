using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GestionaleAlbergo.Models
{
    public static class Utility
    {


        // _____    _____    ______   _   _    ____    _______              ______  _____    ____    _   _   _____ 
        //|  __ \  |  __ \  |  ____| | \ | |  / __ \  |__   __|     /\     |___  / |_   _|  / __ \  | \ | | |_   _|
        //| |__) | | |__) | | |__    |  \| | | |  | |    | |       /  \       / /    | |   | |  | | |  \| |   | |  
        //|  ___/  |  _  /  |  __|   | . ` | | |  | |    | |      / /\ \     / /     | |   | |  | | | . ` |   | |  
        //| |      | | \ \  | |____  | |\  | | |__| |    | |     / ____ \   / /__   _| |_  | |__| | | |\  |  _| |_ 
        //|_|      |_|  \_\ |______| |_| \_|  \____/     |_|    /_/    \_\ /_____| |_____|  \____/  |_| \_| |_____|

        // Metodo per ottenere la lista delle prenotazioni
        // Non riceve nulla
        // Restituisce una lista di oggetti di tipo Prenotazione
        public static List<Prenotazione> GetPrenotazioni()
        {
            // crea una lista di prenotazioni 
            List<Prenotazione> prenotazioni = new List<Prenotazione>();

            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Prenotazione", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // crea un oggetto Prenotazione
                        Prenotazione p = new Prenotazione();

                        // popola p con i dati del record corrente
                        p.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                        p.DataPrenotazione = reader.GetDateTime(reader.GetOrdinal("DataPrenotazione"));
                        p.NumeroProgressivoAnno = reader.GetInt32(reader.GetOrdinal("NumeroProgressivoAnno"));
                        p.Anno = reader.GetInt32(reader.GetOrdinal("Anno"));
                        p.PeriodoDal = reader.GetDateTime(reader.GetOrdinal("PeriodoDal"));
                        p.PeriodoAl = reader.GetDateTime(reader.GetOrdinal("PeriodoAl"));
                        p.CaparraConfirmatoria = reader.GetDecimal(reader.GetOrdinal("CaparraConfirmatoria"));
                        p.TariffaApplicata = reader.GetDecimal(reader.GetOrdinal("TariffaApplicata"));
                        p.TipoSoggiorno = reader.GetString(reader.GetOrdinal("TipoSoggiorno"));
                        p.ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId"));
                        p.Camera.Id = reader.GetInt32(reader.GetOrdinal("CameraId"));

                        // aggiungi p alla lista prenotazioni
                        prenotazioni.Add(p);

                    }
                    return prenotazioni;
                }
                catch (Exception ex)
                {
                    Prenotazione msgErrore = new Prenotazione();
                    msgErrore.Messaggio = "Errore: " + ex.Message;
                    prenotazioni.Add(msgErrore);
                }
            }

            return prenotazioni;
        }

        // metodo per ottenere una prenotazione dall'id
        // riceve un intero che rappresenta l'id della prenotazione
        // restituisce un oggetto di tipo Prenotazione
        public static Prenotazione GetPrenotazioneById(int id)
        {
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Prenotazione WHERE ID = @ID", conn);
                    cmd.Parameters.AddWithValue("ID", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Prenotazione p = new Prenotazione();
                    while (reader.Read())
                    {
                        p.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                        p.DataPrenotazione = reader.GetDateTime(reader.GetOrdinal("DataPrenotazione"));
                        p.NumeroProgressivoAnno = reader.GetInt32(reader.GetOrdinal("NumeroProgressivoAnno"));
                        p.Anno = reader.GetInt32(reader.GetOrdinal("Anno"));
                        p.PeriodoDal = reader.GetDateTime(reader.GetOrdinal("PeriodoDal"));
                        p.PeriodoAl = reader.GetDateTime(reader.GetOrdinal("PeriodoAl"));
                        p.CaparraConfirmatoria = reader.GetDecimal(reader.GetOrdinal("CaparraConfirmatoria"));
                        p.TariffaApplicata = reader.GetDecimal(reader.GetOrdinal("TariffaApplicata"));
                        p.TipoSoggiorno = reader.GetString(reader.GetOrdinal("TipoSoggiorno"));
                        p.ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId"));
                        p.Camera.Id = reader.GetInt32(reader.GetOrdinal("CameraId"));
                    }
                    return p;
                }
                catch (Exception ex)
                {
                    Prenotazione msgErrore = new Prenotazione();
                    msgErrore.Messaggio = "Errore: " + ex.Message;
                    return msgErrore;
                }
            }
        }

        // Metodo per controllare che in questo anno non ci siano già prenotazioni con lo stesso numero progressivo
        // Riceve un intero che rappresenta il numero progressivo
        // Restituisce un booleano
        public static bool CheckNumeroProgressivoAnno(int numeroProgressivo)
        {
            // ottieni anno corrente
            int annoCorrente = DateTime.Now.Year;

            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT * FROM Prenotazioni " +
                        "WHERE Anno = @Anno " +
                        "AND " +
                        "NumeroProgressivoAnno = @NumeroProgressivoAnno", conn);
                    // aggiungi i parametri
                    cmd.Parameters.AddWithValue("Anno", annoCorrente);
                    cmd.Parameters.AddWithValue("NumeroProgressivoAnno", numeroProgressivo);

                    // esegui la query
                    SqlDataReader reader = cmd.ExecuteReader();

                    // se ci sono record restituisci false altrimenti true
                    if (reader.HasRows)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }


        }

        // Metodo per ottenere la lista delle prenotazioni in base al codice fiscale del cliente
        // Riceve una stringa che rappresenta il codice fiscale del cliente
        // Restituisce una lista di oggetti di tipo Prenotazione
        public static List<Prenotazione> GetPrenotazioniByCodiceFiscale(string codiceFiscale)
        {
            // crea una lista di prenotazioni 
            List<Prenotazione> prenotazioni = new List<Prenotazione>();

            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Prenotazione " +
                "WHERE ClienteId = (SELECT ID FROM Cliente WHERE CodiceFiscale = @CodiceFiscale)", conn);
                    cmd.Parameters.AddWithValue("CodiceFiscale", codiceFiscale);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // crea un oggetto Prenotazione
                        Prenotazione p = new Prenotazione();

                        // popola p con i dati del record corrente
                        p.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                        p.DataPrenotazione = reader.GetDateTime(reader.GetOrdinal("DataPrenotazione"));
                        p.NumeroProgressivoAnno = reader.GetInt32(reader.GetOrdinal("NumeroProgressivoAnno"));
                        p.Anno = reader.GetInt32(reader.GetOrdinal("Anno"));
                        p.PeriodoDal = reader.GetDateTime(reader.GetOrdinal("PeriodoDal"));
                        p.PeriodoAl = reader.GetDateTime(reader.GetOrdinal("PeriodoAl"));
                        p.CaparraConfirmatoria = reader.GetDecimal(reader.GetOrdinal("CaparraConfirmatoria"));
                        p.TariffaApplicata = reader.GetDecimal(reader.GetOrdinal("TariffaApplicata"));
                        p.TipoSoggiorno = reader.GetString(reader.GetOrdinal("TipoSoggiorno"));
                        p.ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId"));
                        p.Camera.Id = reader.GetInt32(reader.GetOrdinal("CameraId"));

                        // aggiungi p alla lista prenotazioni
                        prenotazioni.Add(p);

                    }
                    return prenotazioni;
                }
                catch (Exception ex)
                {
                    Prenotazione msgErrore = new Prenotazione();
                    msgErrore.Messaggio = "Errore: " + ex.Message;
                    prenotazioni.Add(msgErrore);
                }
            }

            return prenotazioni;
        }

        // metodo per ottenere la prenotazioni di tipo "Pensione completa"
        // Non riceve nulla
        // Restituisce una lista di oggetti di tipo Prenotazione
        public static List<Prenotazione> GetPrenotazioniPensioneCompleta()
        {
            // crea una lista di prenotazioni 
            List<Prenotazione> prenotazioni = new List<Prenotazione>();

            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT * FROM Prenotazione " +
                        "WHERE TipoSoggiorno = 'Pensione completa'", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // crea un oggetto Prenotazione
                        Prenotazione p = new Prenotazione();

                        // popola p con i dati del record corrente
                        p.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                        p.DataPrenotazione = reader.GetDateTime(reader.GetOrdinal("DataPrenotazione"));
                        p.NumeroProgressivoAnno = reader.GetInt32(reader.GetOrdinal("NumeroProgressivoAnno"));
                        p.Anno = reader.GetInt32(reader.GetOrdinal("Anno"));
                        p.PeriodoDal = reader.GetDateTime(reader.GetOrdinal("PeriodoDal"));
                        p.PeriodoAl = reader.GetDateTime(reader.GetOrdinal("PeriodoAl"));
                        p.CaparraConfirmatoria = reader.GetDecimal(reader.GetOrdinal("CaparraConfirmatoria"));
                        p.TariffaApplicata = reader.GetDecimal(reader.GetOrdinal("TariffaApplicata"));
                        p.TipoSoggiorno = reader.GetString(reader.GetOrdinal("TipoSoggiorno"));
                        p.ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId"));
                        p.Camera.Id = reader.GetInt32(reader.GetOrdinal("CameraId"));

                        // aggiungi p alla lista prenotazioni
                        prenotazioni.Add(p);

                    }
                    return prenotazioni;
                }
                catch (Exception ex)
                {
                    Prenotazione msgErrore = new Prenotazione();
                    msgErrore.Messaggio = "Errore: " + ex.Message;
                    prenotazioni.Add(msgErrore);
                }
            }

            return prenotazioni;
        }

        //  _____   _        _____   ______   _   _   _______   _____ 
        // / ____| | |      |_   _| |  ____| | \ | | |__   __| |_   _|
        //| |      | |        | |   | |__    |  \| |    | |      | |  
        //| |      | |        | |   |  __|   | . ` |    | |      | |  
        //| |____  | |____   _| |_  | |____  | |\  |    | |     _| |_ 
        // \_____| |______| |_____| |______| |_| \_|    |_|    |_____|

        // Metodo per ottenere la lista dei clienti
        // Non riceve nulla
        // Restituisce una lista di oggetti di tipo Cliente
        public static List<Cliente> GetClienti()
        {
            // crea una lista di clienti 
            List<Cliente> clienti = new List<Cliente>();

            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Cliente", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // crea un oggetto Cliente
                        Cliente c = new Cliente();

                        // popola c con i dati del record corrente
                        c.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                        c.CodiceFiscale = reader.GetString(reader.GetOrdinal("CodiceFiscale"));
                        c.Cognome = reader.GetString(reader.GetOrdinal("Cognome"));
                        c.Nome = reader.GetString(reader.GetOrdinal("Nome"));
                        c.Citta = reader.GetString(reader.GetOrdinal("Citta"));
                        c.Provincia = reader.GetString(reader.GetOrdinal("Provincia"));
                        c.Email = reader.GetString(reader.GetOrdinal("Email"));
                        c.Telefono = reader.GetString(reader.GetOrdinal("Telefono"));
                        c.Cellulare = reader.GetString(reader.GetOrdinal("Cellulare"));

                        // aggiungi c alla lista clienti
                        clienti.Add(c);

                    }
                    return clienti;
                }
                catch (Exception ex)
                {
                    Cliente msgErrore = new Cliente();
                    msgErrore.Messaggio = "Errore: " + ex.Message;
                    clienti.Add(msgErrore);
                }
            }

            return clienti;
        }



        // metodo per ottenere un cliente dall'id
        // riceve un intero che rappresenta l'id del cliente
        // restituisce un oggetto di tipo Cliente
        public static Cliente GetClienteById(int id)
        {
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Cliente WHERE ID = @ID", conn);
                    cmd.Parameters.AddWithValue("ID", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Cliente c = new Cliente();
                    while (reader.Read())
                    {
                        c.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                        c.CodiceFiscale = reader.GetString(reader.GetOrdinal("CodiceFiscale"));
                        c.Cognome = reader.GetString(reader.GetOrdinal("Cognome"));
                        c.Nome = reader.GetString(reader.GetOrdinal("Nome"));
                        c.Citta = reader.GetString(reader.GetOrdinal("Citta"));
                        c.Provincia = reader.GetString(reader.GetOrdinal("Provincia"));
                        c.Email = reader.GetString(reader.GetOrdinal("Email"));
                        c.Telefono = reader.GetString(reader.GetOrdinal("Telefono"));
                        c.Cellulare = reader.GetString(reader.GetOrdinal("Cellulare"));
                    }
                    return c;
                }
                catch (Exception ex)
                {
                    Cliente msgErrore = new Cliente();
                    msgErrore.Messaggio = "Errore: " + ex.Message;
                    return msgErrore;
                }
            }
        }


        //  _____              __  __   ______   _____    ______ 
        // / ____|     /\     |  \/  | |  ____| |  __ \  |  ____|
        //| |         /  \    | \  / | | |__    | |__) | | |__   
        //| |        / /\ \   | |\/| | |  __|   |  _  /  |  __|  
        //| |____   / ____ \  | |  | | | |____  | | \ \  | |____ 
        // \_____| /_/    \_\ |_|  |_| |______| |_|  \_\ |______|

        // Metodo per ottenere la lista delle camere
        // Non riceve nulla
        // Restituisce una lista di oggetti di tipo Camera
        public static List<Camera> GetCamere()
        {
            // crea una lista di camere 
            List<Camera> camere = new List<Camera>();

            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Camera", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // crea un oggetto Camera
                        Camera c = new Camera();

                        // popola c con i dati del record corrente
                        c.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                        c.Numero = reader.GetInt32(reader.GetOrdinal("Numero"));
                        c.Descrizione = reader.GetString(reader.GetOrdinal("Descrizione"));
                        c.Tipologia = reader.GetString(reader.GetOrdinal("Tipologia"));

                        // aggiungi c alla lista camere
                        camere.Add(c);

                    }
                    return camere;
                }
                catch (Exception ex)
                {
                    Camera msgErrore = new Camera();
                    msgErrore.Messaggio = "Errore: " + ex.Message;
                    camere.Add(msgErrore);
                }
            }

            return camere;
        }

        // metodo per ottenere la camera dall'id
        // riceve un intero che rappresenta l'id della camera
        // restituisce un oggetto di tipo Camera
        public static Camera GetCameraById(int id)
        {
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Camera WHERE ID = @ID", conn);
                    cmd.Parameters.AddWithValue("ID", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Camera c = new Camera();
                    while (reader.Read())
                    {
                        c.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                        c.Numero = reader.GetInt32(reader.GetOrdinal("Numero"));
                        c.Descrizione = reader.GetString(reader.GetOrdinal("Descrizione"));
                        c.Tipologia = reader.GetString(reader.GetOrdinal("Tipologia"));
                    }
                    return c;
                }
                catch (Exception ex)
                {
                    Camera msgErrore = new Camera();
                    msgErrore.Messaggio = "Errore: " + ex.Message;
                    return msgErrore;
                }
            }
        }


        //  _____   ______   _____   __      __  _____   ______  _____    ____                  _____    _____   _____   _    _   _   _   _______   _____  __      __   ____  
        // / ____| |  ____| |  __ \  \ \    / / |_   _| |___  / |_   _|  / __ \        /\      / ____|  / ____| |_   _| | |  | | | \ | | |__   __| |_   _| \ \    / /  / __ \ 
        //| (___   | |__    | |__) |  \ \  / /    | |      / /    | |   | |  | |      /  \    | |  __  | |  __    | |   | |  | | |  \| |    | |      | |    \ \  / /  | |  | |
        // \___ \  |  __|   |  _  /    \ \/ /     | |     / /     | |   | |  | |     / /\ \   | | |_ | | | |_ |   | |   | |  | | | . ` |    | |      | |     \ \/ /   | |  | |
        // ____) | | |____  | | \ \     \  /     _| |_   / /__   _| |_  | |__| |    / ____ \  | |__| | | |__| |  _| |_  | |__| | | |\  |    | |     _| |_     \  /    | |__| |
        //|_____/  |______| |_|  \_\     \/     |_____| /_____| |_____|  \____/    /_/    \_\  \_____|  \_____| |_____|  \____/  |_| \_|    |_|    |_____|     \/      \____/ 

        // metodo per ottenere i servizi aggiuntivi in base ad un id prenotazione
        // riceve un id di tipo intero che rappresenta l'id della prenotazione
        // restituisce una lista di oggetti di tipo ServizioAggiuntivoPrenotazione
        public static List<ServizioAggiuntivoPrenotazione> GetServiziAggiuntiviByPrenotazioneId(int prenotazioneId)
        {
            // crea una lista di servizi aggiuntivi
            List<ServizioAggiuntivoPrenotazione> serviziAggiuntivi = new List<ServizioAggiuntivoPrenotazione>();

            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT * FROM PrenotazioneServizioAggiuntivo " +
                        "WHERE PrenotazioneId = @PrenotazioneId", conn);
                    cmd.Parameters.AddWithValue("PrenotazioneId", prenotazioneId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // crea un oggetto ServizioAggiuntivo
                        ServizioAggiuntivoPrenotazione s = new ServizioAggiuntivoPrenotazione();

                        // popola s con i dati del record corrente
                        s.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                        s.ServizioAggiuntivoID = reader.GetInt32(reader.GetOrdinal("ServizioAggiuntivoId"));
                        s.Data = reader.GetDateTime(reader.GetOrdinal("Data"));
                        s.Quantita = reader.GetInt32(reader.GetOrdinal("Quantita"));
                        s.PrenotazioneId = reader.GetInt32(reader.GetOrdinal("PrenotazioneId"));

                        // aggiungi s alla lista serviziAggiuntivi
                        serviziAggiuntivi.Add(s);

                    }
                    return serviziAggiuntivi;
                }
                catch (Exception ex)
                {
                    ServizioAggiuntivoPrenotazione msgErrore = new ServizioAggiuntivoPrenotazione();
                    msgErrore.ServizioAggiuntivo.Messaggio = "Errore: " + ex.Message;
                    serviziAggiuntivi.Add(msgErrore);
                }
            }

            return serviziAggiuntivi;
        }

        // Metodo per ottenere un servizio aggiuntivo in base all'id
        // Riceve un intero che rappresenta l'id del servizio aggiuntivo
        // Restituisce un oggetto di tipo ServizioAggiuntivoPrenotazione
        public static ServizioAggiuntivoPrenotazione GetServizioAggiuntivoPrenotazioneById(int id)
        {
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                                               "SELECT * FROM PrenotazioneServizioAggiuntivo " +
                                                                      "WHERE ID = @ID", conn);
                    cmd.Parameters.AddWithValue("ID", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    ServizioAggiuntivoPrenotazione s = new ServizioAggiuntivoPrenotazione();
                    while (reader.Read())
                    {
                        s.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                        s.ServizioAggiuntivoID = reader.GetInt32(reader.GetOrdinal("ServizioAggiuntivoId"));
                        s.Data = reader.GetDateTime(reader.GetOrdinal("Data"));
                        s.Quantita = reader.GetInt32(reader.GetOrdinal("Quantita"));
                        s.PrenotazioneId = reader.GetInt32(reader.GetOrdinal("PrenotazioneId"));
                    }
                    return s;
                }
                catch (Exception ex)
                {
                    ServizioAggiuntivoPrenotazione msgErrore = new ServizioAggiuntivoPrenotazione();
                    msgErrore.ServizioAggiuntivo.Messaggio = "Errore: " + ex.Message;
                    return msgErrore;
                }
            }
        }

        // metodo per ottenere la lista dei servizi aggiuntivi
        // Non riceve nulla
        // Restituisce una lista di oggetti di tipo ServizioAggiuntivo
        public static List<ServizioAggiuntivo> GetServiziAggiuntivi()
        {
            // crea una lista di servizi aggiuntivi
            List<ServizioAggiuntivo> serviziAggiuntivi = new List<ServizioAggiuntivo>();

            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM ServiziAggiuntiviDisponibili", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // crea un oggetto ServizioAggiuntivo
                        ServizioAggiuntivo s = new ServizioAggiuntivo();

                        // popola s con i dati del record corrente
                        s.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                        s.Descrizione = reader.GetString(reader.GetOrdinal("Descrizione"));
                        s.Prezzo = reader.GetDecimal(reader.GetOrdinal("Prezzo"));

                        // aggiungi s alla lista serviziAggiuntivi
                        serviziAggiuntivi.Add(s);

                    }
                    return serviziAggiuntivi;
                }
                catch (Exception ex)
                {
                    ServizioAggiuntivo msgErrore = new ServizioAggiuntivo();
                    msgErrore.Messaggio = "Errore: " + ex.Message;
                    serviziAggiuntivi.Add(msgErrore);
                }
            }

            return serviziAggiuntivi;
        }

        // metodo per ottenere un servizio aggiuntivo dall'id
        // riceve un intero che rappresenta l'id del servizio aggiuntivo
        // restituisce un oggetto di tipo ServizioAggiuntivo
        public static ServizioAggiuntivo GetServizioAggiuntivoById(int id)
        {
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM ServiziAggiuntiviDisponibili WHERE ID = @ID", conn);
                    cmd.Parameters.AddWithValue("ID", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    ServizioAggiuntivo s = new ServizioAggiuntivo();
                    while (reader.Read())
                    {
                        s.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                        s.Descrizione = reader.GetString(reader.GetOrdinal("Descrizione"));
                        s.Prezzo = reader.GetDecimal(reader.GetOrdinal("Prezzo"));
                    }
                    return s;
                }
                catch (Exception ex)
                {
                    ServizioAggiuntivo msgErrore = new ServizioAggiuntivo();
                    msgErrore.Messaggio = "Errore: " + ex.Message;
                    return msgErrore;
                }
            }
        }

    }
}