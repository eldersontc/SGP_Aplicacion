using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGP_Aplicacion.Models
{
    public class PalabraMap : ClassMap<Palabra>
    {
        public PalabraMap()
        {
            Id(x => x.Id);
            Map(x => x.IdComentario);
            Map(x => x.Texto);
        }
    }
}
