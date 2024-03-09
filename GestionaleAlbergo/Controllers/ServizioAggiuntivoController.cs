using GestionaleAlbergo.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
namespace GestionaleAlbergo.Controllers
{
    public class ServizioAggiuntivoController : Controller
    {
        // GET: ServizioAggiuntivo/5
        public ActionResult Index(int id)
        {
            // ottieni la lista dei servizi aggiuntivi
            List<ServizioAggiuntivoPrenotazione> serviziAggiuntivi = Utility.GetServiziAggiuntiviByPrenotazioneId(id);
            ViewBag.PrenotazioneId = id;
            return View(serviziAggiuntivi);
        }

        // GET: ServizioAggiuntivo/Create/5
        public ActionResult Create(int id)
        {
            int prenotazioneId = id;
            ViewBag.PrenotazioneId = prenotazioneId;
            ViewBag.ServiziDisponibili = Utility.GetServiziAggiuntivi();
            return View();
        }

        // POST: ServizioAggiuntivo/Create
        [HttpPost]
        public ActionResult Create(int id, ServizioAggiuntivoPrenotazione formServizioAggiuntivo)
        {
            if (!ModelState.IsValid)
            {
                return View(formServizioAggiuntivo);
            }

            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                            "INSERT INTO PrenotazioneServizioAggiuntivo (" +
                            "ServizioAggiuntivoId, " +
                            "Data, " +
                            "Quantita, " +
                            "PrenotazioneId) " +

                            "VALUES (" +

                            "@ServizioAggiuntivoId, " +
                            "@Data, " +
                            "@Quantita, " +
                            "@PrenotazioneId)", conn);

                    cmd.Parameters.AddWithValue("ServizioAggiuntivoId", formServizioAggiuntivo.ServizioAggiuntivoID);
                    cmd.Parameters.AddWithValue("Data", formServizioAggiuntivo.Data);
                    cmd.Parameters.AddWithValue("Quantita", formServizioAggiuntivo.Quantita);
                    cmd.Parameters.AddWithValue("PrenotazioneId", id);

                    cmd.ExecuteNonQuery();

                    TempData["msgSuccess"] = "Servizio aggiuntivo inserito con successo";
                    return RedirectToAction("Index", new { id = id });
                }
                catch (SqlException ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }
            return RedirectToAction("Index", new { id = id });

        }

        // GET: ServizioAggiuntivo/Edit/5
        public ActionResult Edit(int id)
        {
            // ottieni il servizio aggiuntivo da modificare
            ServizioAggiuntivoPrenotazione servizioAggiuntivo = Utility.GetServizioAggiuntivoPrenotazioneById(id);
            return View();
        }

        // POST: ServizioAggiuntivo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ServizioAggiuntivo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ServizioAggiuntivo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
