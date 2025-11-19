using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class clsUsuario_CN
    {
        private clsUsuario_CD objUsuario_CD = new clsUsuario_CD();

        public clsUsuario_CE mtdLogin(string correo, string contrasena)
        {
            return objUsuario_CD.mtdLogin(correo, contrasena);
        }

        
        public DataTable mtdListar()
        {
            return objUsuario_CD.mtdListar();
        }

        
        public int mtdInsertar(clsUsuario_CE obj, out string mensaje)
        {
            return objUsuario_CD.mtdInsertar(obj, out mensaje);
        }

      
        public bool mtdActualizar(clsUsuario_CE obj, out string mensaje)
        {
            return objUsuario_CD.mtdActualizar(obj, out mensaje);
        }

       
        public bool mtdEliminar(int idUsuario, out string mensaje)
        {
            return objUsuario_CD.mtdEliminar(idUsuario, out mensaje);
        }

        
        public DataTable mtdListarRoles()
        {
            return objUsuario_CD.mtdListarRoles();
        }

        // TIPOS PROCEDENCIA
        public DataTable mtdListarTipoProcedencia()
        {
            return objUsuario_CD.mtdListarTipoProcedencia();
        }
    }
}
