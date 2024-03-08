using System.ComponentModel.DataAnnotations;

namespace GestionaleAlbergo.Models
{
    public class Camera
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo Numero è obbligatorio.")]
        [Display(Name = "Numero Camera")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Il campo Descrizione è obbligatorio.")]
        public string Descrizione { get; set; }

        [Required(ErrorMessage = "Il campo Tipologia è obbligatorio.")]
        public string Tipologia { get; set; }

        [ScaffoldColumn(false)]
        public string Messaggio { get; set; }

        [Display(Name = "Nome Completo della stanza")]
        public string NomeCompleto
        {
            get
            {
                return "Camera " + Numero + " - " + Descrizione;
            }
        }
    }
}