using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class clsConexion
    {
        private readonly string cadenaConexion;

        public clsConexion()
        {
        
            cadenaConexion = "Server=DESKTOP-H2DTJ40;" +
                             "Database=EVENTOSC#;" +
                             "Integrated Security=True;" +
                             "TrustServerCertificate=True;";
        }

       
        public SqlConnection mtdAbrirConexion()
        {
            SqlConnection con = new SqlConnection(cadenaConexion);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            return con;
        }

        
        public void mtdCerrarConexion(SqlConnection con)
        {
            if (con != null && con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
