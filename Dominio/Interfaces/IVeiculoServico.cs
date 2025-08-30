using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using miminal_api.Dominio.Entidades;

namespace miminal_api.Dominio.Interfaces
{
    public interface IVeiculoServico
    {
        List<veiculo> Todos(int? pagina);
        List<veiculo> Todos(int? pagina, string? nome = null, string? marca = null);
        void Incluir(veiculo veiculo);
        veiculo? Incluir(LoginDTO loginDTO);
        veiculo? BuscarPorId(int id);
        void Atualizar(veiculo veiculo);
        void Apagar(veiculo veiculo);
    }
    
}