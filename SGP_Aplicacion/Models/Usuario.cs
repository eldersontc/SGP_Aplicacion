using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGP_Aplicacion.Models
{
    public class Usuario: Base
    {
        public string Alias { get; set; }
        public string Password { get; set; }
        public string Perfil { get; set; }
        public bool Eliminado { get; set; }
    }
}
