using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using miminal_api.Infraestrutura.Db;
using miminal_api.DTOs;
using miminal_api.Dominio.Interfaces;
using miminal_api.Dominio.Entidades;
using miminal_api.Dominio.Servicos;
using miminal_api.Dominio.ModelViews;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Serviços
        builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();
        builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<DbContexto>(options =>
            options.UseMySql(
                builder.Configuration.GetConnectionString("mysql"),
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
            )
        );

        var app = builder.Build();

        // Home
        app.MapGet("/", () => Results.Json(new Home()));

        // Administradores
        app.MapPost("/Administradores/login", ([FromBody] LoginDTO loginDTO, IAdministradorServico administradorServico) =>
        {
            var resultado = administradorServico.Login(loginDTO);
            return resultado != null ? Results.Ok("Login com sucesso") : Results.Unauthorized();
        });
         ErrosDeValidacao validacaoDTO(VeiculoDTO veiculoDTO)
        {
            var validacao = new ErrosDeValidacao
            {
                Mensagens = new List<string>()
            };
            if (string.IsNullOrEmpty(veiculoDTO.Nome))
            {
                validacao.Mensagens.Add("O nome não pode ser vazio");

            }
            if (string.IsNullOrEmpty(veiculoDTO.Marca))
            {
                validacao.Mensagens.Add("A marca não pode ser vazio");
            }
            if (veiculoDTO.Ano < 1886)
            {
                validacao.Mensagens.Add("O ano do veículo deve ser um ano válido.");
            }
            return validacao;
        }
        // Veículos
        app.MapPost("/veiculos", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
        {
             var validacao = validacaoDTO(veiculoDTO);

            if (validacao.Mensagens.Count > 0)
            {
                return Results.BadRequest(validacao);
            }
            var veiculo = new veiculo
            {
                Nome = veiculoDTO.Nome,
                Marca = veiculoDTO.Marca,
                Ano = veiculoDTO.Ano
            };
            veiculoServico.Incluir(veiculo);
            return Results.Created($"/veiculo/{veiculo.Id}", veiculo);
        });

        app.MapGet("/veiculos", ([FromQuery] int? pagina, IVeiculoServico veiculoServico) =>
        {
            var veiculos = veiculoServico.Todos(pagina);
            return Results.Ok(veiculos);
        });
           app.MapGet("/veiculos/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
        {
            var veiculo = veiculoServico.BuscarPorId(id);
            if (veiculo == null) return Results.NotFound();
            return Results.Ok(veiculo);
        });
            app.MapPut("/veiculos/{id}", ([FromRoute] int id,VeiculoDTO veiculoDTOP , IVeiculoServico veiculoServico) =>
        {
            var validacao = validacaoDTO(veiculoDTOP);

            if (validacao.Mensagens.Count > 0)
            {
                return Results.BadRequest(validacao);
            }

            var veiculo = veiculoServico.BuscarPorId(id);
            if (veiculo == null) return Results.NotFound();
            veiculo.Nome = veiculoDTOP.Nome;
            veiculo.Marca = veiculoDTOP.Marca;
            veiculo.Ano = veiculoDTOP.Ano;

            veiculoServico.Atualizar(veiculo);

            return Results.Ok(veiculo);
        });
           app.MapDelete("/veiculos/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
        {
          var veiculo = veiculoServico.BuscarPorId(id);

            veiculoServico.Apagar(veiculo);

            return Results.NoContent();
        });

        // Swagger
        app.UseSwagger();
        app.UseSwaggerUI();

        app.Run();
    }
}

// DTO para login
public class LoginDTO
{
    public string Email { get; set; } = default!;
    public string Senha { get; set; } = default!;
}