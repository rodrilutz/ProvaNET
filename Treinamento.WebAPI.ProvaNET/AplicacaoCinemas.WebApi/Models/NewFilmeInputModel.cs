using System;
using System.ComponentModel.DataAnnotations;

namespace MundoDoCinema.WebApi.Models
{
    public class NewFilmeInputModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "Tamanho inválido")]
        public string Titulo { get; set; }
        
        [Required] 
        public int Duracao { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Tamanho inválido")]
        public string Sinopse { get; set; }
    }
}