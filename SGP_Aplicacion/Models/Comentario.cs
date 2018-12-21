using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGP_Aplicacion.Models
{
    public class Comentario: Base
    {
        public DateTime FechaHora { get; set; }
        public string Descripcion { get; set; }
        public int Calificacion { get; set; }
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
        public string Plataforma { get; set; }
        public string Version { get; set; }
        public string Serial { get; set; }
        public string UUID { get; set; }
    }
}
