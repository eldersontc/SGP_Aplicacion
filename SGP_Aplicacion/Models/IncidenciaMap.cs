using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGP_Aplicacion.Models
{
    public class IncidenciaMap : ClassMap<Incidencia>
    {
        public IncidenciaMap()
        {
            Id(x => x.Id);
            Map(x => x.FechaHora);
            Map(x => x.Latitud);
            Map(x => x.Longitud);
            Map(x => x.Foto);
            Map(x => x.Fabricante);
            Map(x => x.Modelo);
            Map(x => x.Plataforma);
            Map(x => x.Version);
            Map(x => x.Serial);
            Map(x => x.UUID);
        }
    }
}
