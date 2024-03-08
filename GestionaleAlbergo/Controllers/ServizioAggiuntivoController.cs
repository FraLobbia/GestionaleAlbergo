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
            List<ServizioAggiuntivo> serviziAggiuntivi = Utility.GetServiziAggiuntiviByPrenotazioneId(id);
            ViewBag.PrenotazioneId = id;
            return View(serviziAggiuntivi);
        }

        // GET: ServizioAggiuntivo/Create/5
        public ActionResult Create(int id)
        {
            int prenotazioneId = id;
            ViewBag.PrenotazioneId = prenotazioneId;
            return View();
        }

        // POST: ServizioAggiuntivo/Create
        [HttpPost]
        public ActionResult Create(ServizioAggiuntivo formServizioAggiuntivo)
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
                        "INSERT INTO ServizioAggiuntivo (" +
                        "Descrizione, " +
                        "Data, " +
                        "Quantita, " +
                        "Prezzo, " +
                        "PrenotazioneId) " +
                        "VALUES (" +
                        "@Descrizione, " +
                        "@Data, " +
                        "@Quantita, " +
                        "@Prezzo, " +
                        "@PrenotazioneId)", conn);

                    cmd.Parameters.AddWithValue("@Descrizione", formServizioAggiuntivo.Descrizione);
                    cmd.Parameters.AddWithValue("@Data", formServizioAggiuntivo.Data);
                    cmd.Parameters.AddWithValue("@Quantita", formServizioAggiuntivo.Quantita);
                    cmd.Parameters.AddWithValue("@Prezzo", formServizioAggiuntivo.Prezzo);
                    cmd.Parameters.AddWithValue("@PrenotazioneId", formServizioAggiuntivo.PrenotazioneId);
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    formServizioAggiuntivo.Messaggio = e.Message;
                    return View(formServizioAggiuntivo);
                }
            }
            return RedirectToAction("Index", new { id = formServizioAggiuntivo.PrenotazioneId });

        }

        // GET: ServizioAggiuntivo/Edit/5
        public ActionResult Edit(int id)
        {
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
