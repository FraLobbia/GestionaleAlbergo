using GestionaleAlbergo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Security;

namespace GestionaleAlbergo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Dipendente u)
        {
            string role = "";
            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Dipendenti WHERE Username = @Username AND Password = @Password", conn);
                    cmd.Parameters.AddWithValue("Username", u.Username);
                    cmd.Parameters.AddWithValue("Password", u.Password);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        FormsAuthentication.SetAuthCookie(u.Username, false);
                        while (reader.Read())
                        {
                            role = reader.GetString(3);
                        }
                        TempData["msgSuccess"] = "Login come " + role + " effettuato con successo";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        conn.Close();
                        ViewBag.AuthError = "Autenticazione non riuscita";
                        return View();

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.AuthError = ex.Message;
                }
                finally { conn.Close(); }

            return RedirectToAction("Index", "Home");


        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            TempData["msgSuccess"] = "Logout effettuato con successo";
            return RedirectToAction("Index", "Home");
        }
        //   __  __   ______   _______    ____    _____    _____                 _____  __     __  _   _    _____ 
        //  |  \/  | |  ____| |__   __|  / __ \  |  __ \  |_   _|       /\      / ____| \ \   / / | \ | |  / ____|
        //  | \  / | | |__       | |    | |  | | | |  | |   | |        /  \    | (___    \ \_/ /  |  \| | | |     
        //  | |\/| | |  __|      | |    | |  | | | |  | |   | |       / /\ \    \___ \    \   /   | . ` | | |     
        //  | |  | | | |____     | |    | |__| | | |__| |  _| |_     / ____ \   ____) |    | |    | |\  | | |____ 
        //  |_|  |_| |______|    |_|     \____/  |_____/  |_____|   /_/    \_\ |_____/     |_|    |_| \_|  \_____|

        // Metodo per ottenere la lista delle prenotazioni in base al codice fiscale del cliente
        // Riceve una stringa che rappresenta il codice fiscale del cliente tramite l'attributo id
        // Restituisce un json con la lista delle prenotazioni
        public JsonResult GetPrenotazioniByCodiceFiscale(string id)
        {
            // ottengo la lista delle prenotazioni in base al codice fiscale del cliente
            List<Prenotazione> prenotazioni = Utility.GetPrenotazioniByCodiceFiscale(id);
            return Json(prenotazioni, JsonRequestBehavior.AllowGet);
        }

        // Metodo per ottenere la lista delle prenotazioni di tipo "Pensione completa"
        // Non riceve parametri
        // Restituisce un json con la lista delle prenotazioni
        public JsonResult GetPrenotazioniPensioneCompleta()
        {
            // ottengo la lista delle prenotazioni di tipo "Pensione completa"
            List<Prenotazione> prenotazioni = Utility.GetPrenotazioniPensioneCompleta();
            return Json(prenotazioni, JsonRequestBehavior.AllowGet);
        }


    }
}
