using miminal_api.Dominio.Entidades;
using miminal_api.Dominio.Interfaces;
using miminal_api.DTOs;
using miminal_api.Infraestrutura.Db;
using Microsoft.EntityFrameworkCore;

public class VeiculoServico : IVeiculoServico
{
    public List<veiculo> Todos(int? pagina)
    {
        return Todos(pagina, null, null);
    }
    private readonly DbContexto _contexto;

    public VeiculoServico(DbContexto db)
    {
        _contexto = db;
    }
    public veiculo? BuscarPorId(int id)
    {
        return _contexto.Veiculos.Find(id);
    }

    public List<veiculo> Todos(int? pagina, string? nome = null, string? marca = null)
    {
        var query = _contexto.Veiculos.AsQueryable();

        if (!string.IsNullOrEmpty(nome))
        {
            query = query.Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{nome.ToLower()}%"));
        }

        if (!string.IsNullOrEmpty(marca))
        {
            query = query.Where(v => EF.Functions.Like(v.Marca.ToLower(), $"%{marca.ToLower()}%"));
        }

        int itensPorPagina = 10;
        if (pagina.HasValue && pagina.Value > 0)
        {
            query = query.Skip((pagina.Value - 1) * itensPorPagina).Take(itensPorPagina);
        }

        return query.ToList();
    }

    // Implementa inclusão via LoginDTO (se realmente necessário)
    public veiculo? Incluir(LoginDTO loginDTO)
    {
        // Aqui você deve mapear loginDTO para um Veiculo, se fizer sentido
        return null; // apenas exemplo
    }

    // Implementa inclusão direta de Veiculo
    public void Incluir(veiculo veiculos)
    {
        _contexto.Veiculos.Add(veiculos);
        _contexto.SaveChanges();
    }
    public void Atualizar(veiculo veiculo)
    {
        _contexto.Veiculos.Update(veiculo);
        _contexto.SaveChanges();

    }
      public void Apagar(veiculo veiculo)
    {
        _contexto.Veiculos.Remove(veiculo);
        _contexto.SaveChanges();
    }
}        