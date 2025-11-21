using CapaDatos;
using CapaEntidad;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace CapaNegocios
{
    public class clsUsuario_CN
    {
        private clsUsuario_CD objUsuario_CD = new clsUsuario_CD();

        // ===== MÉTODO PARA ENCRIPTAR =====
        private string HashSHA256(string texto)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(texto);
                byte[] hash = sha.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        public clsUsuario_CE mtdLogin(string correo, string contrasena)
        {
            // Convertir contraseña en hash ANTES de enviar a SQL
            string claveHash = HashSHA256(contrasena);

            return objUsuario_CD.mtdLogin(correo, contrasena);
        }

        public DataTable mtdListar()
        {
            return objUsuario_CD.mtdListar();
        }

        public int mtdInsertar(clsUsuario_CE obj, out string mensaje)
        {
            // Hash a la contraseña ANTES de mandar a SQL
            obj.Contrasena = HashSHA256(obj.Contrasena);

            return objUsuario_CD.mtdInsertar(obj, out mensaje);
        }

        public bool mtdActualizar(clsUsuario_CE obj, out string mensaje)
        {
            // Hash a la contraseña si el usuario envía una nueva
            obj.Contrasena = HashSHA256(obj.Contrasena);

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

        public DataTable mtdListarTipoProcedencia()
        {
            return objUsuario_CD.mtdListarTipoProcedencia();
        }
    }
}
