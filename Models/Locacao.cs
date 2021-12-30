using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmsJust.Models
{

  
    public class Locacao
    {
    [Key]
    public int LocacaoId {get;set;}
    public decimal Total {get;set;}
    public int FilmeId{get; set;}
    public Filme Filme {get; set;}

    }
}