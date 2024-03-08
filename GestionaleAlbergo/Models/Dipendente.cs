using System.ComponentModel.DataAnnotations;

namespace GestionaleAlbergo.Models
{
    public class Dipendente
    {
        public int Id { get; set; }
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [ScaffoldColumn(false)]
        public string Role { get; set; }
    }

}