using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class clsPonente_CE
    {
        public int IdPonente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Biografia { get; set; }
        public string Especialidad { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string FotoURL { get; set; }   
        public bool Estado { get; set; }      
    }
}
