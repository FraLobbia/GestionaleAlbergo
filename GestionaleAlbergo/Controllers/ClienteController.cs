using GestionaleAlbergo.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
namespace GestionaleAlbergo.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
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
                                "@Nome, " +
                                "@Cognome, " +
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
    }
}
