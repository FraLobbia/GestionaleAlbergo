
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GestionaleAlbergo.Models
{
    public class Cliente
    {
        //=================================================================//
        public int Id { get; set; }
        //=================================================================//

        [Required]
        [Display(Name = "Codice Fiscale")]
        [Remote("isCodiceFiscaleAvailable", "Cliente", ErrorMessage = "Codice Fiscale già esistente, inserirne un altro.")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Il Codice Fiscale deve essere lungo 16 caratteri.")]
        public string CodiceFiscale { get; set; }
        //=================================================================//
        [Required]
        public string Cognome { get; set; }
        //=================================================================//
        [Required]
        public string Nome { get; set; }
        //=================================================================//
        [Required]
        public string Citta { get; set; }
        //=================================================================//
        [Required]
        public string Provincia { get; set; }
        //=================================================================//
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        //=================================================================//
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }
        //=================================================================//
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Cellulare { get; set; }
        //=================================================================//
        [ScaffoldColumn(false)]
        public string Messaggio { get; set; }
        //=================================================================//
        [ScaffoldColumn(false)]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto
        {
            get
            {
                return Cognome + " " + Nome;
            }
        }
        //=================================================================//
    }

}