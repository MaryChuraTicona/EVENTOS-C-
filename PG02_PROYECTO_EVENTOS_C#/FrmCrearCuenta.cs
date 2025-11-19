using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;



namespace PG02_PROYECTO_EVENTOS_C_
{
    public partial class FrmCrearCuenta : Form
    {
        private clsUsuario_CN objUsuario_CN = new clsUsuario_CN();
        public FrmCrearCuenta()
        {
            InitializeComponent();
        }

        private void FrmCrearCuenta_Load(object sender, EventArgs e)
        {
        }
        private async Task<clsPersonaReniec> ConsultarDni(string dni)
        {
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6ImZyYW5jb21wMDI4QGdtYWlsLmNvbSJ9.JiCSOMkmlg_tbxXM1Hv_wgdsLsq2hhKM6NZsKb7iHU4";
            string url = $"https://dniruc.apisperu.com/api/v1/dni/{dni}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return null;

                clsPersonaReniec persona = JsonConvert.DeserializeObject<clsPersonaReniec>(json);
                return persona;
            }
        }

        private async void btnBuscarDni_Click(object sender, EventArgs e)
        {
            string dni = txtDNI.Text.Trim();

            if (dni.Length != 8)
            {
                MessageBox.Show("Ingrese un DNI válido (8 dígitos).", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var persona = await ConsultarDni(dni);

                if (persona == null)
                {
                    MessageBox.Show("DNI no encontrado.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                
                txtNombres.Text = persona.nombres;
                txtApellido.Text = persona.apellidoPaterno + " " + persona.apellidoMaterno;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar RENIEC:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDNI.Text) || txtDNI.Text.Length != 8)
            {
                MessageBox.Show("Ingrese un DNI válido.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDNI.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombres.Text))
            {
                MessageBox.Show("Ingrese los nombres.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombres.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombres.Text))
            {
                MessageBox.Show("Ingrese los apellidos.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombres.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("Ingrese un correo.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Ingrese una contraseña.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (txtPassword.Text != txtConfirmarPassword.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmarPassword.Focus();
                return;
            }

         
            int idRolEstudiante = ObtenerIdRolEstudiante();
            if (idRolEstudiante == 0)
            {
                MessageBox.Show("No se encontró el rol 'ESTUDIANTE' en la base de datos.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

          
            clsUsuario_CE obj = new clsUsuario_CE
            {
                IdUsuario = 0,
                IdRol = idRolEstudiante,
                IdTipoProcedencia = null,
                Nombres = txtNombres.Text.Trim(),
                Apellidos = txtNombres.Text.Trim(),
                DNI = txtDNI.Text.Trim(),
                Correo = txtCorreo.Text.Trim(),
                Contrasena = txtPassword.Text.Trim(),
                CodigoUPT = string.IsNullOrWhiteSpace(txtCodigoUPT.Text)
                            ? null
                            : txtCodigoUPT.Text.Trim(),
                Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text)
                            ? null
                            : txtTelefono.Text.Trim(),
                EsVerificado = false,
                Estado = true
            };

        
            try
            {
                string mensaje;
                int idGenerado = objUsuario_CN.mtdInsertar(obj, out mensaje);

                if (idGenerado > 0)
                {
                    MessageBox.Show("Cuenta creada correctamente. Ahora puede iniciar sesión.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo crear la cuenta.\n" + mensaje,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar la cuenta:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int ObtenerIdRolEstudiante()
        {
            DataTable dtRoles = objUsuario_CN.mtdListarRoles();
            foreach (DataRow row in dtRoles.Rows)
            {
                string nombreRol = row["Nombre"].ToString();
                if (nombreRol.Equals("ESTUDIANTE", StringComparison.OrdinalIgnoreCase))
                {
                    return Convert.ToInt32(row["IdRol"]);
                }
            }
            return 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
