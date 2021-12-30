using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



namespace FilmsJust.Models
{
 
    public class Filme
    {
        [Key]
        public int FilmeId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }

        public DateTime DataLancamento { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Informe o preço do produto", AllowEmptyStrings = false)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]     
        public decimal Preco { get; set; }

        [DataType(DataType.Upload)]
        public string Imagem { get; set; }
        public int GeneroId {get; set;}
        public Genero Genero { get; set; }

         public virtual ICollection<Locacao> Locacacoes { get; set; } 
        
    }
}