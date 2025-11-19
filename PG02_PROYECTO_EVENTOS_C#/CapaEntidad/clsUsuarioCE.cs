using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class clsUsuario_CE
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public string NombreRol { get; set; }

        public int? IdTipoProcedencia { get; set; }
        public string NombreTipoProcedencia { get; set; }

        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }   

        public string DNI { get; set; }
        public string CodigoUPT { get; set; }
        public string Telefono { get; set; }

        public bool EsVerificado { get; set; }
        public bool Estado { get; set; }
    }

}
