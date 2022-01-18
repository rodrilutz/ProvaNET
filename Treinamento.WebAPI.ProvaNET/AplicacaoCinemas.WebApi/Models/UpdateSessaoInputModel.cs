using System;
using System.ComponentModel.DataAnnotations;

namespace MundoDoCinema.WebApi.Models
{
    public sealed class UpdateSessaoInputModel
    {
        public Guid IdDoFilme { get; set; }

        public DateTime Dia { get; set; }

        public float Preco { get; set; }

        public int InicioHora { get; set; }
        
        public int InicioMinuto { get; set; }

        public int QuantidadeLugares { get; set; }       
        

    }
}