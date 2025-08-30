using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using miminal_api.Dominio.Entidades;
using miminal_api.Dominio.Interfaces;
using miminal_api.Infraestrutura.Db;

namespace miminal_api.Dominio.Servicos
{
    public class AdministradorServico : IAdministradorServico
    {
        private readonly DbContexto _contexto;
        public AdministradorServico(DbContexto db)
        {
            _contexto = db;
        }

        public Administrador? Login(LoginDTO loginDTO)
        {
            var Adm = _contexto.Administradores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha).FirstOrDefault();
            return Adm;
        }
    }
}