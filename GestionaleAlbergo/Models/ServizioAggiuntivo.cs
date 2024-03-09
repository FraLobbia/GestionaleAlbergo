using System;
using System.ComponentModel.DataAnnotations;

namespace GestionaleAlbergo.Models
{
    public class ServizioAggiuntivo
    {
        //=================================================================//
        public int Id { get; set; }
        //=================================================================//
        [Required]
        public string Descrizione { get; set; }
        //=================================================================//
        [Required]
        public decimal Prezzo { get; set; }
        //=================================================================//
        [ScaffoldColumn(false)]
        public string Messaggio { get; set; }
        //=================================================================//

        [ScaffoldColumn(false)]
        public string NomeCompleto
        {
            get
            {
                int prezzo = Convert.ToInt32(Prezzo);

                return Descrizione + " - " + prezzo + "€";
            }
        }

    }
}