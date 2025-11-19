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
    public class clsPonente_CD
    {
        private clsConexion cn = new clsConexion();

        public int mtdInsertar(clsPonente_CE obj, out string mensaje)
        {
            mensaje = string.Empty;
            int idGenerado = 0;
            SqlConnection con = null;

            try
            {
                con = cn.mtdAbrirConexion();

                using (SqlCommand cmd = new SqlCommand("usp_I_Ponente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("@Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("@Biografia",
                        (object)obj.Biografia ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Especialidad",
                        (object)obj.Especialidad ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Correo",
                        (object)obj.Correo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Telefono",
                        (object)obj.Telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FotoURL",
                        (object)obj.FotoURL ?? DBNull.Value);

                    SqlParameter pId = new SqlParameter("@IdGenerado", SqlDbType.Int);
                    pId.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pId);

                    SqlParameter pMsg = new SqlParameter("@Mensaje", SqlDbType.VarChar, 300);
                    pMsg.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pMsg);

                    cmd.ExecuteNonQuery();

                    idGenerado = (pId.Value != DBNull.Value) ? Convert.ToInt32(pId.Value) : 0;
                    mensaje = pMsg.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idGenerado = 0;
                mensaje = "Error en mtdInsertar Ponente: " + ex.Message;
            }
            finally
            {
                cn.mtdCerrarConexion(con);
            }

            return idGenerado;
        }

        public DataTable mtdListar()
        {
            DataTable dt = new DataTable();
            SqlConnection con = null;

            try
            {
                con = cn.mtdAbrirConexion();
                using (SqlCommand cmd = new SqlCommand("usp_S_Ponente_Listar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch
            {
                dt = null;
            }
            finally
            {
                cn.mtdCerrarConexion(con);
            }

            return dt;
        }

        public bool mtdActualizar(clsPonente_CE obj, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;
            SqlConnection con = null;

            try
            {
                con = cn.mtdAbrirConexion();

                using (SqlCommand cmd = new SqlCommand("usp_U_Ponente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdPonente", obj.IdPonente);
                    cmd.Parameters.AddWithValue("@Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("@Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("@Biografia",
                        (object)obj.Biografia ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Especialidad",
                        (object)obj.Especialidad ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Correo",
                        (object)obj.Correo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Telefono",
                        (object)obj.Telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FotoURL",
                        (object)obj.FotoURL ?? DBNull.Value);

                    SqlParameter pMsg = new SqlParameter("@Mensaje", SqlDbType.VarChar, 300);
                    pMsg.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pMsg);

                    int filas = cmd.ExecuteNonQuery();
                    resultado = filas > 0;
                    mensaje = pMsg.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = "Error en mtdActualizar Ponente: " + ex.Message;
            }
            finally
            {
                cn.mtdCerrarConexion(con);
            }

            return resultado;
        }

        public bool mtdEliminar(int idPonente, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;
            SqlConnection con = null;

            try
            {
                con = cn.mtdAbrirConexion();

                using (SqlCommand cmd = new SqlCommand("usp_D_Ponente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdPonente", idPonente);

                    SqlParameter pMsg = new SqlParameter("@Mensaje", SqlDbType.VarChar, 300);
                    pMsg.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pMsg);

                    int filas = cmd.ExecuteNonQuery();
                    resultado = filas > 0;
                    mensaje = pMsg.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = "Error en mtdEliminar Ponente: " + ex.Message;
            }
            finally
            {
                cn.mtdCerrarConexion(con);
            }

            return resultado;
        }



    }
}
