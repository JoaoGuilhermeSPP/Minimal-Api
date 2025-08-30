using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace miminal_api.DTOs
{
    public record VeiculoDTO
    {
   
    public string Marca { get; set; } = default!;

    public int Ano { get; set; } = default!;
      
       public string Nome { get; set; } = default!;
    }
}