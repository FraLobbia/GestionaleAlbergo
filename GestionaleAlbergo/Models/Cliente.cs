using System.ComponentModel.DataAnnotations;

namespace GestionaleAlbergo.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo Codice Fiscale è obbligatorio.")]
        [StringLength(16)]
        public string CodiceFiscale { get; set; }

        [Required(ErrorMessage = "Il campo Cognome è obbligatorio.")]
        public string Cognome { get; set; }
        [Required(ErrorMessage = "Il campo Nome è obbligatorio.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Il campo Città è obbligatorio.")]
        public string Citta { get; set; }

        [Required(ErrorMessage = "Il campo Provincia è obbligatorio.")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "Il campo Email è obbligatorio.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Il campo Telefono è obbligatorio.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Il campo Cellulare è obbligatorio.")]
        public string Cellulare { get; set; }

        [ScaffoldColumn(false)]
        public string Messaggio { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto
        {
            get
            {
                return Cognome + " " + Nome;
            }
        }
    }

}