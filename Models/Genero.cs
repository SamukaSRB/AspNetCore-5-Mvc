using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace FilmsJust.Models
{

    public class Genero
    {      
        public int GeneroId { get; set; }
        [Display(Name = "Nome do Genero")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public String GeneroNome { get; set; }
        public virtual ICollection<Filme> Filmes { get; set; }
    }
}