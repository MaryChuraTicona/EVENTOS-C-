using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class clsUsuario_CD
    {
        private clsConexion cn = new clsConexion();

        public clsUsuario_CE mtdLogin(string correo, string contrasena)
        {
            clsUsuario_CE objUsuario = null;
            SqlConnection con = null;

            try
            {
                con = cn.mtdAbrirConexion();

                SqlCommand cmd = new SqlCommand("dbo.usp_S_Usuario_Login", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 150).Value = correo;
                cmd.Parameters.Add("@Contrasena", SqlDbType.VarChar, 200).Value = contrasena;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    objUsuario = new clsUsuario_CE
                    {
                        IdUsuario = dr.GetInt32(dr.GetOrdinal("IdUsuario")),
                        Nombres = dr["Nombres"].ToString(),
                        Apellidos = dr["Apellidos"].ToString(),
                        Correo = dr["Correo"].ToString(),
                        IdRol = Convert.ToInt32(dr["IdRol"]),
                        NombreRol = dr["NombreRol"].ToString(),
                        Contrasena = contrasena
                    };
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en mtdLogin (CapaDatos): " + ex.Message);
            }
            finally
            {
                cn.mtdCerrarConexion(con);
            }

            return objUsuario;
        }

        public DataTable mtdListar()
        {
            DataTable dt = new DataTable();
            SqlConnection con = null;

            try
            {
                con = cn.mtdAbrirConexion();
                SqlCommand cmd = new SqlCommand("dbo.usp_S_Usuario_Listar", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en mtdListar (Usuarios): " + ex.Message);
            }
            finally
            {
                cn.mtdCerrarConexion(con);
            }

            return dt;
        }

        
        public int mtdInsertar(clsUsuario_CE obj, out string mensaje)
        {
            int idGenerado = 0;
            mensaje = string.Empty;
            SqlConnection con = null;

            try
            {
                con = cn.mtdAbrirConexion();
                SqlCommand cmd = new SqlCommand("dbo.usp_I_Usuario", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdRol", obj.IdRol);
                cmd.Parameters.AddWithValue("@IdTipoProcedencia", (object)obj.IdTipoProcedencia ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Nombres", obj.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", obj.Apellidos);
                cmd.Parameters.AddWithValue("@Correo", obj.Correo);
                cmd.Parameters.AddWithValue("@Contrasena", obj.Contrasena);
                cmd.Parameters.AddWithValue("@DNI", (object)obj.DNI ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CodigoUPT", (object)obj.CodigoUPT ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Telefono", (object)obj.Telefono ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@EsVerificado", obj.EsVerificado);
                cmd.Parameters.AddWithValue("@Estado", obj.Estado);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    idGenerado = Convert.ToInt32(result);
                }
            }
            catch (SqlException ex)
            {
                
                mensaje = "Error SQL al insertar usuario: " + ex.Message;
            }
            catch (Exception ex)
            {
                mensaje = "Error al insertar usuario: " + ex.Message;
            }
            finally
            {
                cn.mtdCerrarConexion(con);
            }

            return idGenerado;
        }

      
        public bool mtdActualizar(clsUsuario_CE obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            SqlConnection con = null;

            try
            {
                con = cn.mtdAbrirConexion();
                SqlCommand cmd = new SqlCommand("dbo.usp_U_Usuario", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                cmd.Parameters.AddWithValue("@IdRol", obj.IdRol);
                cmd.Parameters.AddWithValue("@IdTipoProcedencia", (object)obj.IdTipoProcedencia ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Nombres", obj.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", obj.Apellidos);
                cmd.Parameters.AddWithValue("@Correo", obj.Correo);
                cmd.Parameters.AddWithValue("@Contrasena", obj.Contrasena);
                cmd.Parameters.AddWithValue("@DNI", (object)obj.DNI ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CodigoUPT", (object)obj.CodigoUPT ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Telefono", (object)obj.Telefono ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@EsVerificado", obj.EsVerificado);
                cmd.Parameters.AddWithValue("@Estado", obj.Estado);

                int filas = cmd.ExecuteNonQuery();
                resultado = filas > 0;
            }
            catch (SqlException ex)
            {
                mensaje = "Error SQL al actualizar usuario: " + ex.Message;
            }
            catch (Exception ex)
            {
                mensaje = "Error al actualizar usuario: " + ex.Message;
            }
            finally
            {
                cn.mtdCerrarConexion(con);
            }

            return resultado;
        }

      
        public bool mtdEliminar(int idUsuario, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            SqlConnection con = null;

            try
            {
                con = cn.mtdAbrirConexion();
                SqlCommand cmd = new SqlCommand("dbo.usp_D_Usuario", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                int filas = cmd.ExecuteNonQuery();
                resultado = filas > 0;
            }
            catch (Exception ex)
            {
                mensaje = "Error al eliminar usuario: " + ex.Message;
            }
            finally
            {
                cn.mtdCerrarConexion(con);
            }

            return resultado;
        }

        public DataTable mtdListarRoles()
        {
            DataTable dt = new DataTable();
            SqlConnection con = null;

            try
            {
                con = cn.mtdAbrirConexion();
                SqlCommand cmd = new SqlCommand("dbo.usp_S_Rol_Activos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            finally
            {
                cn.mtdCerrarConexion(con);
            }

            return dt;
        }

        
        public DataTable mtdListarTipoProcedencia()
        {
            DataTable dt = new DataTable();
            SqlConnection con = null;

            try
            {
                con = cn.mtdAbrirConexion();
                SqlCommand cmd = new SqlCommand("dbo.usp_S_TipoProcedencia_Activos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            finally
            {
                cn.mtdCerrarConexion(con);
            }

            return dt;
        }
    }
}

