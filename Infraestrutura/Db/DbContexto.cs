using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using miminal_api.Dominio.Entidades;

namespace miminal_api.Infraestrutura.Db;

public class DbContexto : DbContext
{
    
    private readonly IConfiguration _ConfiguracaoAppSettings;
    public DbContexto(IConfiguration _configuracaoAppSettings)
    {
        _ConfiguracaoAppSettings = _configuracaoAppSettings;
    }
    public DbSet<Administrador> Administradores { get; set; } = default!;
      public DbSet<veiculo> Veiculos { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder) //Adicionando um Adm
    {
        modelBuilder.Entity<Administrador>().HasData(
            new Administrador
            {
                Id = 1,
                Email = "administrador@teste.com",
                Senha = "123456",
                Perfil = "Adm"
            }
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var stringConexao = _ConfiguracaoAppSettings.GetConnectionString("mysql")?.ToString(); //Conexão com o mysql, appSettings.json
            if (!string.IsNullOrEmpty(stringConexao))
            {

                optionsBuilder.UseMySql(stringConexao,
     ServerVersion.AutoDetect(stringConexao));
                //Configuração Basica para criar a tabela
            }
        }
    } 
}