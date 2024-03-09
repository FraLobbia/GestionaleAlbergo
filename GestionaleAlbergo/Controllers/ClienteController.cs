using GestionaleAlbergo.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
namespace GestionaleAlbergo.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {

        //             _____   _______   _____    ____    _   _ 
        //    /\      / ____| |__   __| |_   _|  / __ \  | \ | |
        //   /  \    | |         | |      | |   | |  | | |  \| |
        //  / /\ \   | |         | |      | |   | |  | | | . ` |
        // / ____ \  | |____     | |     _| |_  | |__| | | |\  |
        ///_/    \_\  \_____|    |_|    |_____|  \____/  |_| \_|

        // GET: Cliente
        public ActionResult Index()
        {
            // ottengo la lista dei clienti
            List<Cliente> clienti = Utility.GetClienti();
            return View(clienti);
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(Cliente formCliente)
        {
            if (!ModelState.IsValid)
            {
                return View(formCliente);
            }

            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                                "INSERT INTO Cliente (" +
                                "CodiceFiscale, " +
                                "Cognome, " +
                                "Nome, " +
                                "Citta, " +
                                "Provincia, " +
                                "Email, " +
                                "Telefono, " +
                                "Cellulare) " +

                                "VALUES (" +

                                "@CodiceFiscale, " +
                                "@Cognome, " +
                                "@Nome, " +
                                "@Citta, " +
                                "@Provincia, " +
                                "@Email, " +
                                "@Telefono, " +
                                "@Cellulare)", conn);

                    cmd.Parameters.AddWithValue("CodiceFiscale", formCliente.CodiceFiscale);
                    cmd.Parameters.AddWithValue("Cognome", formCliente.Cognome);
                    cmd.Parameters.AddWithValue("Nome", formCliente.Nome);
                    cmd.Parameters.AddWithValue("Citta", formCliente.Citta);
                    cmd.Parameters.AddWithValue("Provincia", formCliente.Provincia);
                    cmd.Parameters.AddWithValue("Email", formCliente.Email);
                    cmd.Parameters.AddWithValue("Telefono", formCliente.Telefono);
                    cmd.Parameters.AddWithValue("Cellulare", formCliente.Cellulare);

                    cmd.ExecuteNonQuery();
                    TempData["msgSuccess"] = "Cliente inserito con successo";
                }
                catch (SqlException ex)
                {
                    ViewBag.msgErrore = "Errore: " + ex.Message;
                    return View(formCliente);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int id)
        {
            // ottengo il cliente da modificare
            Cliente cliente = Utility.GetClienteById(id);
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Cliente formCliente)
        {
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Cliente SET " +
                        "CodiceFiscale = @CodiceFiscale, " +
                        "Cognome = @Cognome, " +
                        "Nome = @Nome, " +
                        "Citta = @Citta, " +
                        "Provincia = @Provincia, " +
                        "Email = @Email, " +
                        "Telefono = @Telefono, " +
                        "Cellulare = @Cellulare " +
                        "WHERE Id = @Id", conn);

                    cmd.Parameters.AddWithValue("Id", formCliente.Id);
                    cmd.Parameters.AddWithValue("CodiceFiscale", formCliente.CodiceFiscale);
                    cmd.Parameters.AddWithValue("Cognome", formCliente.Cognome);
                    cmd.Parameters.AddWithValue("Nome", formCliente.Nome);
                    cmd.Parameters.AddWithValue("Citta", formCliente.Citta);
                    cmd.Parameters.AddWithValue("Provincia", formCliente.Provincia);
                    cmd.Parameters.AddWithValue("Email", formCliente.Email);
                    cmd.Parameters.AddWithValue("Telefono", formCliente.Telefono);
                    cmd.Parameters.AddWithValue("Cellulare", formCliente.Cellulare);

                    cmd.ExecuteNonQuery();
                    TempData["msgSuccess"] = "Cliente modificato con successo";
                    return RedirectToAction("Index");
                }
                catch (SqlException ex)
                {
                    ViewBag.msgErrore = "Errore: " + ex.Message;
                    return View(formCliente);
                }
            }

        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int id)
        {
            // ottengo il cliente da cancellare
            Cliente cliente = Utility.GetClienteById(id);
            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Cliente formCliente)
        {
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Cliente WHERE Id = @Id", conn);
                    cmd.Parameters.AddWithValue("Id", id);
                    cmd.ExecuteNonQuery();
                    TempData["msgSuccess"] = "Cliente cancellato con successo";
                    return RedirectToAction("Index");
                }
                catch (SqlException ex)
                {
                    ViewBag.msgErrore = "Errore: " + ex.Message;
                    return View(formCliente);
                }
            }

        }


        //__      __             _        _____   _____               _______   _____    ____    _   _ 
        //\ \    / /     /\     | |      |_   _| |  __ \      /\     |__   __| |_   _|  / __ \  | \ | |
        // \ \  / /     /  \    | |        | |   | |  | |    /  \       | |      | |   | |  | | |  \| |
        //  \ \/ /     / /\ \   | |        | |   | |  | |   / /\ \      | |      | |   | |  | | | . ` |
        //   \  /     / ____ \  | |____   _| |_  | |__| |  / ____ \     | |     _| |_  | |__| | | |\  |
        //    \/     /_/    \_\ |______| |_____| |_____/  /_/    \_\    |_|    |_____|  \____/  |_| \_|

        // Metodo per verificare se il codice fiscale del cliente è disponibile
        // Richiede il parametro CodiceFiscale in formato stringa
        // Restituisce un valore booleano in formato JSON
        public ActionResult isCodiceFiscaleAvailable()
        {
            string codiceFiscale = Request["CodiceFiscale"];

            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Cliente WHERE CodiceFiscale = @CodiceFiscale", conn);
                    cmd.Parameters.AddWithValue("CodiceFiscale", codiceFiscale);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (SqlException e)
                {
                    return Json("Errore di connessione al database: " + e.Message, JsonRequestBehavior.AllowGet);
                }
            }

        }

        // NON USATO MA SCRITTO E LASCIATO PER FARE PRATICA
        // Metodo per verificare se il nome del cliente è disponibile
        // Richiede il parametro Nome in formato stringa
        // Restituisce un valore booleano in formato JSON 
        public ActionResult IsNomeClienteAvailable()
        {
            string nome = Request["Nome"];
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Cliente WHERE Nome = @Nome";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nome", nome);
                        int count = (int)cmd.ExecuteScalar(); // Restituisce solo la prima colonna della prima riga, ignorando le altre colonne e righe
                        if (count > 0)
                        {
                            return Json(false, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (SqlException e)
                {
                    return Json("Errore di connessione al database: " + e.Message, JsonRequestBehavior.AllowGet);
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        // NON USATO MA SCRITTO E LASCIATO PER FARE PRATICA
        // Metodo per verificare se il cognome del cliente è disponibile
        // Richiede il parametro Cognome in formato stringa
        // Restituisce un valore booleano in formato JSON
        public ActionResult IsCognomeClienteAvailable()
        {
            string cognome = Request["Cognome"];
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Cliente WHERE Cognome = @Cognome";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Cognome", cognome);
                        int count = (int)cmd.ExecuteScalar(); // Restituisce solo la prima colonna della prima riga, ignorando le altre colonne e righe
                        if (count > 0)
                        {
                            return Json(false, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (SqlException e)
                {
                    return Json("Errore di connessione al database: " + e.Message, JsonRequestBehavior.AllowGet);
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
