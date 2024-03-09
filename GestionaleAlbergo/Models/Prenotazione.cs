using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionaleAlbergo.Models
{
    public class Prenotazione
    {
        //=================================================================//
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        //=================================================================//
        [DataType(DataType.DateTime)]
        [Display(Name = "Data Prenotazione")]
        [Required]
        public DateTime DataPrenotazione { get; set; } = DateTime.Now;
        //=================================================================//
        [Display(Name = "N. Fattura")]
        public int NumeroProgressivoAnno { get; set; }
        //=================================================================//
        [Required]
        public int Anno { get; set; } = DateTime.Now.Year;
        //=================================================================//
        [Required]
        [Display(Name = "A partire dal")]
        [DataType(DataType.DateTime)]
        public DateTime PeriodoDal { get; set; } = DateTime.Now;
        //=================================================================//
        [DataType(DataType.DateTime)]
        [Required]
        [Display(Name = "Fino al")]
        public DateTime PeriodoAl { get; set; } = DateTime.Now.AddDays(1);
        //=================================================================//
        [Required]
        [Display(Name = "Caparra Confirmatoria")]
        [DataType(DataType.Currency)]
        public decimal CaparraConfirmatoria { get; set; }
        //=================================================================//
        [Required]
        [Display(Name = "Tariffa Applicata per notte")]
        [DataType(DataType.Currency)]
        public decimal TariffaApplicata { get; set; } = 80;
        //=================================================================//
        [ScaffoldColumn(false)]
        [Display(Name = "Numero Notti")]
        public int NumeroNotti
        {
            get
            {
                return (PeriodoAl - PeriodoDal).Days;
            }
        }
        //=================================================================//
        [ScaffoldColumn(false)]
        [DataType(DataType.Currency)]
        public decimal ImportoDovuto
        {
            get
            {
                return TariffaApplicata * NumeroNotti - CaparraConfirmatoria + ImportoServiziAggiuntivi;
            }
        }
        //=================================================================//
        [Required]
        [Display(Name = "Tipo Soggiorno")]
        public string TipoSoggiorno { get; set; }
        //=================================================================//
        [Required]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; } // foreign key NECESSARIA per ottenere il cliente interamente
        //=================================================================//
        [ScaffoldColumn(false)]
        public Cliente Cliente
        {
            get
            {
                return Utility.GetClienteById(ClienteId);
            }
        }
        //=================================================================//
        [Required]
        public int CameraId { get; set; } // foreign key NECESSARIA per ottenere la camera interamente
        //=================================================================//
        [ScaffoldColumn(false)]
        public Camera Camera // ottenuta grazie a CameraId
        {
            get
            {
                return Utility.GetCameraById(CameraId);
            }
        }
        //=================================================================//
        [ScaffoldColumn(false)]
        public List<ServizioAggiuntivoPrenotazione> ServiziAggiuntivi
        {
            get
            {
                return Utility.GetServiziAggiuntiviByPrenotazioneId(Id);
            }

        }
        //=================================================================//
        [ScaffoldColumn(false)]
        [DataType(DataType.Currency)]
        public decimal ImportoServiziAggiuntivi
        {
            get
            {
                decimal importo = 0;
                foreach (var servizio in ServiziAggiuntivi)
                {
                    importo += servizio.ServizioAggiuntivo.Prezzo * servizio.Quantita;
                }
                return importo;
            }
        }
        //=================================================================//
        [ScaffoldColumn(false)]
        public string Messaggio { get; set; }
        //=================================================================//
    }


}