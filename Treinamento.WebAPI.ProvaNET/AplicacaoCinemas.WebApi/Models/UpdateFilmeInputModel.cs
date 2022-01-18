using System;
using System.ComponentModel.DataAnnotations;

namespace MundoDoCinema.WebApi.Models
{
    public sealed class UpdateFilmeInputModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "Tamanho inválido")]
        public string Titulo { get; set; }

        public int Duracao { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Tamanho inválido")]
        public string Sinopse { get; set; }        

    }
}