using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GestionaleAlbergo.Models
{
    public class Camera
    {
        //=================================================================//
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        //=================================================================//
        [Required(ErrorMessage = "Il campo Numero è obbligatorio.")]
        [Display(Name = "Numero Camera")]
        [Remote("isCameraAlreadyExist", "Camera", ErrorMessage = "Numero Camera già esistente.")]
        public int Numero { get; set; }
        //=================================================================//
        [Required]
        public string Descrizione { get; set; }
        //=================================================================//
        [Required]
        public string Tipologia { get; set; }
        //=================================================================//
        [ScaffoldColumn(false)]
        public string Messaggio { get; set; }
        //=================================================================//
        [Display(Name = "Nome Completo della stanza")]
        public string NomeCompleto
        {
            get
            {
                return "Camera " + Numero + " - " + Descrizione;
            }
        }
        //=================================================================//
    }
}