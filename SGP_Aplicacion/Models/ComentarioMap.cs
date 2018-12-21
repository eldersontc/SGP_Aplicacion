using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGP_Aplicacion.Models
{
    public class ComentarioMap : ClassMap<Comentario>
    {
        public ComentarioMap()
        {
            Id(x => x.Id);
            Map(x => x.FechaHora);
            Map(x => x.Descripcion);
            Map(x => x.Calificacion);
            Map(x => x.Fabricante);
            Map(x => x.Modelo);
            Map(x => x.Plataforma);
            Map(x => x.Version);
            Map(x => x.Serial);
            Map(x => x.UUID);
        }
    }
}
