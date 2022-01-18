using System;
using System.ComponentModel.DataAnnotations;

namespace MundoDoCinema.WebApi.Models
{
    public class NewSessaoInputModel
    {
        [Required]
        public Guid IdDoFilme { get; set; }

        [Required]
        public DateTime Dia { get; set; }

        [Required]
        public double Preco { get; set; }

        [Required]
        public int InicioHora { get; set; }

        [Required]
        public int InicioMinuto { get; set; }

        [Required]
        public int QuantidadeLugares { get; set; }
        
    }
}