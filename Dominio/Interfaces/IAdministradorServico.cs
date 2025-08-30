using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using miminal_api.Dominio.Entidades;

namespace miminal_api.Dominio.Interfaces
{
    public interface IAdministradorServico
    {
        Administrador? Login(LoginDTO loginDTO);
    }
}