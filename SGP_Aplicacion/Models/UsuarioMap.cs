using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGP_Aplicacion.Models
{
    public class UsuarioMap: ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Id(x => x.Id);
            Map(x => x.Alias);
            Map(x => x.Password);
            Map(x => x.Perfil);
        }
    }
}
