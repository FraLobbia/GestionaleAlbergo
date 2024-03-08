using System;
using System.ComponentModel.DataAnnotations;

namespace GestionaleAlbergo.Models
{
    public class ServizioAggiuntivo
    {
        public int Id { get; set; }
        public string Descrizione { get; set; }
        public DateTime Data { get; set; }
        public int Quantita { get; set; }
        public decimal Prezzo { get; set; }
        [ScaffoldColumn(false)]
        public int PrenotazioneId { get; set; }
        [ScaffoldColumn(false)]
        public Prenotazione Prenotazione { get; set; }

        [ScaffoldColumn(false)]
        public string Messaggio { get; set; }

    }

}