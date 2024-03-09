using GestionaleAlbergo.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace GestionaleAlbergo.Controllers
{
    [Authorize]
    public class PrenotazioneController : Controller
    {
        // GET: Prenotazione
        public ActionResult Index()
        {
            // ottengo la lista delle prenotazioni
            List<Prenotazione> prenotazioni = Utility.GetPrenotazioni();
            return View(prenotazioni);
        }


        // GET: Prenotazione/Create
        public ActionResult Create()
        {
            // ottengo la lista dei clienti
            List<Cliente> clienti = Utility.GetClienti();
            ViewBag.Clienti = clienti;
            return View();
        }

        // POST: Prenotazione/Create
        [HttpPost]
        public ActionResult Create(Prenotazione formPrenotazione)
        {
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Prenotazione (" +

                        "DataPrenotazione, " +
                        "NumeroProgressivoAnno, " +
                        "Anno, " +
                        "PeriodoDal, " +
                        "PeriodoAl, " +
                        "CaparraConfirmatoria, " +
                        "TariffaApplicata, " +
                        "TipoSoggiorno, " +
                        "ClienteId, " +
                        "CameraId) " +

                        "VALUES (" +

                        "@DataPrenotazione, " +
                        "@NumeroProgressivoAnno, " +
                        "@Anno, " +
                        "@PeriodoDal, " +
                        "@PeriodoAl, " +
                        "@CaparraConfirmatoria, " +
                        "@TariffaApplicata, " +
                        "@TipoSoggiorno, " +
                        "@ClienteId, " +
                        "@CameraId)", conn);


                    cmd.Parameters.AddWithValue("DataPrenotazione", formPrenotazione.DataPrenotazione);

                    // definisco il numero progressivo dell'anno a partire da 1
                    int numeroProgressivoAnno = 1;

                    // controllo se il numero progressivo dell'anno è già presente altrimenti lo incremento
                    while (Utility.CheckNumeroProgressivoAnno(numeroProgressivoAnno))
                    {
                        numeroProgressivoAnno++;
                    }

                    // una volta trovato un numero progressivo libero lo assegno
                    cmd.Parameters.AddWithValue("NumeroProgressivoAnno", numeroProgressivoAnno);


                    cmd.Parameters.AddWithValue("Anno", formPrenotazione.Anno);
                    cmd.Parameters.AddWithValue("PeriodoDal", formPrenotazione.PeriodoDal);
                    cmd.Parameters.AddWithValue("PeriodoAl", formPrenotazione.PeriodoAl);
                    cmd.Parameters.AddWithValue("CaparraConfirmatoria", formPrenotazione.CaparraConfirmatoria);
                    cmd.Parameters.AddWithValue("TariffaApplicata", formPrenotazione.TariffaApplicata);
                    cmd.Parameters.AddWithValue("TipoSoggiorno", formPrenotazione.TipoSoggiorno);
                    cmd.Parameters.AddWithValue("ClienteId", formPrenotazione.ClienteId);
                    cmd.Parameters.AddWithValue("CameraId", formPrenotazione.Camera.Id);

                    cmd.ExecuteNonQuery();

                    TempData["msgSuccess"] = "Prenotazione inserita con successo";
                    return RedirectToAction("Index", "Prenotazione");
                }
                catch (SqlException ex)
                {
                    ViewBag.msgErrore = "Errore: " + ex.Message;
                    return View();
                }
            }
        }

        // GET: Prenotazione/Edit/5
        public ActionResult Edit(int id)
        {
            // ottengo la prenotazione da modificare
            Prenotazione prenotazione = Utility.GetPrenotazioneById(id);
            return View(prenotazione);
        }

        // POST: Prenotazione/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Prenotazione formPrenotazione)
        {
            if (!ModelState.IsValid)
            {
                return View(formPrenotazione);
            }

            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Prenotazione SET " +
                        "DataPrenotazione = @DataPrenotazione, " +
                        "NumeroProgressivoAnno = @NumeroProgressivoAnno, " +
                        "Anno = @Anno, " +
                        "PeriodoDal = @PeriodoDal, " +
                        "PeriodoAl = @PeriodoAl, " +
                        "CaparraConfirmatoria = @CaparraConfirmatoria, " +
                        "TariffaApplicata = @TariffaApplicata, " +
                        "TipoSoggiorno = @TipoSoggiorno, " +
                        "ClienteId = @ClienteId, " +
                        "CameraId = @CameraId " +
                        "WHERE Id = @Id", conn);

                    cmd.Parameters.AddWithValue("Id", id);
                    cmd.Parameters.AddWithValue("DataPrenotazione", formPrenotazione.DataPrenotazione);
                    cmd.Parameters.AddWithValue("NumeroProgressivoAnno", formPrenotazione.NumeroProgressivoAnno);
                    cmd.Parameters.AddWithValue("Anno", formPrenotazione.Anno);
                    cmd.Parameters.AddWithValue("PeriodoDal", formPrenotazione.PeriodoDal);
                    cmd.Parameters.AddWithValue("PeriodoAl", formPrenotazione.PeriodoAl);
                    cmd.Parameters.AddWithValue("CaparraConfirmatoria", formPrenotazione.CaparraConfirmatoria);
                    cmd.Parameters.AddWithValue("TariffaApplicata", formPrenotazione.TariffaApplicata);
                    cmd.Parameters.AddWithValue("TipoSoggiorno", formPrenotazione.TipoSoggiorno);
                    cmd.Parameters.AddWithValue("ClienteId", formPrenotazione.ClienteId);
                    cmd.Parameters.AddWithValue("CameraId", formPrenotazione.Camera.Id);

                    cmd.ExecuteNonQuery();

                    TempData["msgSuccess"] = "Prenotazione modificata con successo";
                    return RedirectToAction("Index", "Prenotazione");
                }
                catch (SqlException ex)
                {
                    ViewBag.msgErrore = "Errore: " + ex.Message;
                    return View();
                }
            }
        }

        // GET: Prenotazione/Delete/5
        public ActionResult Delete(int id)
        {
            // ottengo la prenotazione da eliminare
            Prenotazione prenotazione = Utility.GetPrenotazioneById(id);
            return View(prenotazione);
        }

        // POST: Prenotazione/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Prenotazione formPrenotazione)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Prenotazione WHERE Id = @Id", conn);
                    cmd.Parameters.AddWithValue("Id", id);
                    cmd.ExecuteNonQuery();
                    TempData["msgSuccess"] = "Prenotazione eliminata con successo";
                    return RedirectToAction("Index", "Prenotazione");
                }
                catch (SqlException ex)
                {
                    ViewBag.msgErrore = "Errore: " + ex.Message;
                    return View();
                }
            }
        }

        // GET: Prenotazione/CheckOut/5
        public ActionResult CheckOut(int id)
        {
            // ottieni la prenotazione di cui fare il check-out
            Prenotazione prenotazione = Utility.GetPrenotazioneById(id);
            return View(prenotazione);
        }


    }
}
