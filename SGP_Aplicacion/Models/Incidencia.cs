using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGP_Aplicacion.Models
{
    public class Incidencia: Base
    {
        public DateTime FechaHora { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Base64 { get; set; }
        public bool Foto { get; set; }
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
        public string Plataforma { get; set; }
        public string Version { get; set; }
        public string Serial { get; set; }
        public string UUID { get; set; }
        public string SIM { get; set; }
    }
}
