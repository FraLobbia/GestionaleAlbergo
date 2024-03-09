using System;
using System.ComponentModel.DataAnnotations;

namespace GestionaleAlbergo.Models
{
    public class ServizioAggiuntivoPrenotazione
    {
        //=================================================================//
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        //=================================================================//
        [Required]
        public int ServizioAggiuntivoID { get; set; }
        //=================================================================//
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime Data { get; set; }
        //=================================================================//
        [Required]
        public int Quantita { get; set; } = 1; // 1 è il valore di default
        //=================================================================//

        [ScaffoldColumn(false)]
        public int PrenotazioneId { get; set; } // Foreign Key necessaria per ottenere la prenotazione
        //=================================================================//
        [ScaffoldColumn(false)]
        public Prenotazione Prenotazione
        {
            get
            {
                return Utility.GetPrenotazioneById(PrenotazioneId);
            }

        } // ottenuta grazie a PrenotazioneId
        //=================================================================//

        [ScaffoldColumn(false)]
        public ServizioAggiuntivo ServizioAggiuntivo
        {
            get
            {
                return Utility.GetServizioAggiuntivoById(ServizioAggiuntivoID);
            }
        }
    }

}