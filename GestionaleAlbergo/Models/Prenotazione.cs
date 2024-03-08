using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionaleAlbergo.Models
{
    public class Prenotazione
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data Prenotazione")]
        [Required]
        public DateTime DataPrenotazione { get; set; } = DateTime.Now;

        [Display(Name = "N. Fattura")]
        public int NumeroProgressivoAnno { get; set; }
        public int Anno { get; set; } = DateTime.Now.Year;

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [Display(Name = "A partire dal")]
        [DataType(DataType.Date)]
        public DateTime PeriodoDal { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [Display(Name = "Fino al")]
        public DateTime PeriodoAl { get; set; } = DateTime.Now.AddDays(1);

        [Required(ErrorMessage = "Il campo Caparra è obbligatorio.")]
        [Display(Name = "Caparra Confirmatoria")]
        public decimal CaparraConfirmatoria { get; set; }

        [Required(ErrorMessage = "Il campo Tariffa è obbligatorio.")]
        [Display(Name = "Tariffa Applicata per notte")]
        public decimal TariffaApplicata { get; set; } = 80;

        [ScaffoldColumn(false)]
        [Display(Name = "Numero Notti")]
        public int NumeroNotti
        {
            get
            {
                return (PeriodoAl - PeriodoDal).Days;
            }
        }
        [ScaffoldColumn(false)]
        public decimal ImportoDovuto
        {
            get
            {
                return TariffaApplicata * NumeroNotti - CaparraConfirmatoria + ImportoServiziAggiuntivi;
            }
        }

        [Required(ErrorMessage = "Il campo Tipo Soggiorno è obbligatorio.")]
        [Display(Name = "Tipo Soggiorno")]
        public string TipoSoggiorno { get; set; }

        [Required(ErrorMessage = "Il campo Cliente è obbligatorio.")]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        [ScaffoldColumn(false)]
        public Cliente Cliente
        {
            get
            {
                return Utility.GetClienteById(ClienteId);
            }
        }

        [Required(ErrorMessage = "Il campo Camera è obbligatorio.")]
        public int CameraId { get; set; }
        [ScaffoldColumn(false)]
        public Camera Camera
        {
            get
            {
                return Utility.GetCameraById(CameraId);
            }
        }
        [ScaffoldColumn(false)]
        public List<ServizioAggiuntivo> ServiziAggiuntivi
        {
            get
            {
                return Utility.GetServiziAggiuntiviByPrenotazioneId(Id);
            }

        }

        [ScaffoldColumn(false)]
        public decimal ImportoServiziAggiuntivi
        {
            get
            {
                decimal importo = 0;
                foreach (var servizio in ServiziAggiuntivi)
                {
                    importo += servizio.Prezzo * servizio.Quantita;
                }
                return importo;
            }
        }

        [ScaffoldColumn(false)]
        public string Messaggio { get; set; }
    }


}