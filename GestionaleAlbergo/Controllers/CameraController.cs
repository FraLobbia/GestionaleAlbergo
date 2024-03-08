using GestionaleAlbergo.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace GestionaleAlbergo.Controllers
{
    [Authorize]
    public class CameraController : Controller
    {
        // GET: Camera
        public ActionResult Index()
        {
            // ottengo la lista delle camere
            List<Camera> camere = Utility.GetCamere();
            return View(camere);
        }


        // GET: Camera/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Camera/Create
        [HttpPost]
        public ActionResult Create(Camera formCamera)
        {
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                            "INSERT INTO Camera (" +
                            "Numero, " +
                            "Descrizione, " +
                            "Tipologia) " +

                            "VALUES (" +

                            "@Numero, " +
                            "@Descrizione, " +
                            "@Tipologia)", conn);

                    cmd.Parameters.AddWithValue("Numero", formCamera.Numero);
                    cmd.Parameters.AddWithValue("Descrizione", formCamera.Descrizione);
                    cmd.Parameters.AddWithValue("Tipologia", formCamera.Tipologia);

                    cmd.ExecuteNonQuery();

                    TempData["msgSuccess"] = "Camera inserita con successo";
                    return RedirectToAction("Index");
                }
                catch (SqlException ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }
            return View();
        }

        // GET: Camera/Edit/5
        public ActionResult Edit(int id)
        {
            // ottengo la camera da modificare
            Camera camera = Utility.GetCameraById(id);
            return View(camera);
        }

        // POST: Camera/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Camera formCamera)
        {
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                            "UPDATE Camera " +
                            "SET Numero = @Numero, " +
                            "Descrizione = @Descrizione, " +
                            "Tipologia = @Tipologia " +
                            "WHERE Id = @Id", conn);

                    cmd.Parameters.AddWithValue("Numero", formCamera.Numero);
                    cmd.Parameters.AddWithValue("Descrizione", formCamera.Descrizione);
                    cmd.Parameters.AddWithValue("Tipologia", formCamera.Tipologia);
                    cmd.Parameters.AddWithValue("Id", id);

                    cmd.ExecuteNonQuery();

                    TempData["msgSuccess"] = "Camera modificata con successo";
                    return RedirectToAction("Index");
                }
                catch (SqlException ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }
            return View();

        }

        // GET: Camera/Delete/5
        public ActionResult Delete(int id)
        {
            // ottengo la camera da eliminare
            Camera camera = Utility.GetCameraById(id);
            return View(camera);
        }

        // POST: Camera/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Camera formCamera)
        {
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM Camera " +
                        "WHERE Id = @Id", conn);

                    cmd.Parameters.AddWithValue("Id", id);

                    cmd.ExecuteNonQuery();

                    TempData["msgSuccess"] = "Camera eliminata con successo";
                    return RedirectToAction("Index");
                }
                catch (SqlException ex)
                {
                    ViewBag.Error = ex.Message;
                    return View(formCamera);
                }
            }
        }
    }
}
