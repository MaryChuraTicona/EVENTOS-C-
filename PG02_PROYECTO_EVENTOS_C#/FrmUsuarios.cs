using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PG02_PROYECTO_EVENTOS_C_
{
    public partial class FrmUsuarios : Form
    {
        private clsUsuario_CN objUsuario_CN = new clsUsuario_CN();
        private bool esNuevo = false;
        private bool esEditar = false;
        private string rutaFotoActual = null;

        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            dgvUsuarios.AutoGenerateColumns = false;

            try
            {
                mtdCargarCombos();
                mtdListarUsuarios();
                mtdEstadoInicial();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar el formulario Usuarios:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            
            btnNuevo.Click += btnNuevo_Click;
            btnGuardar.Click += btnGuardar_Click;
            btnCancelar.Click += btnCancelar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnCargarFoto.Click += btnCargarFoto_Click;

            dgvUsuarios.CellClick += dgvUsuarios_CellClick;
        }
        private void mtdCargarCombos()
        {
            
            DataTable dtRol = objUsuario_CN.mtdListarRoles();
            cmbRol.DataSource = dtRol;
            cmbRol.DisplayMember = "Nombre";
            cmbRol.ValueMember = "IdRol";

            
            DataTable dtTP = objUsuario_CN.mtdListarTipoProcedencia();
            cmbTipoProcedencia.DataSource = dtTP;
            cmbTipoProcedencia.DisplayMember = "Nombre";
            cmbTipoProcedencia.ValueMember = "IdTipoProcedencia";

            cmbRol.SelectedIndex = -1;
            cmbTipoProcedencia.SelectedIndex = -1;
        }
        private void mtdListarUsuarios()
        {
            
            dgvUsuarios.DataSource = objUsuario_CN.mtdListar();
        }
        private void mtdEstadoInicial()
        {
            esNuevo = false;
            esEditar = false;
            mtdHabilitarCampos(false);
            mtdLimpiarCampos();
        }
        private void mtdHabilitarCampos(bool estado)
        {
            txtNombres.Enabled = estado;
            txtApellidos.Enabled = estado;
            txtDNI.Enabled = estado;
            txtCorreo.Enabled = estado;
            txtContrasena.Enabled = estado;
            txtCodigoUPT.Enabled = estado;
            txtTelefono.Enabled = estado;
            cmbRol.Enabled = estado;
            cmbTipoProcedencia.Enabled = estado;
            chkEsVerificado.Enabled = estado;
            chkEstado.Enabled = estado;
            btnCargarFoto.Enabled = estado;
        }
        private void mtdLimpiarCampos()
        {
            txtIdUsuario.Text = "";    
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtDNI.Text = "";
            txtCorreo.Text = "";
            txtContrasena.Text = "";
            txtCodigoUPT.Text = "";
            txtTelefono.Text = "";
            cmbRol.SelectedIndex = -1;
            cmbTipoProcedencia.SelectedIndex = -1;
            chkEsVerificado.Checked = false;
            chkEstado.Checked = true;
            pbFoto.Image = null;
            rutaFotoActual = null;
        }

        private bool mtdValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombres.Text))
            {
                MessageBox.Show("Ingrese nombres.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombres.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellidos.Text))
            {
                MessageBox.Show("Ingrese apellidos.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellidos.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDNI.Text))
            {
                MessageBox.Show("Ingrese DNI.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDNI.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("Ingrese correo.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return false;
            }

            if (cmbRol.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un rol.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRol.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtContrasena.Text) && esNuevo)
            {
                MessageBox.Show("Ingrese contraseña.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContrasena.Focus();
                return false;
            }

            return true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            esEditar = false;
            mtdHabilitarCampos(true);
            mtdLimpiarCampos();
            txtNombres.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!mtdValidarCampos())
                return;

         
            int idUsuario = 0;
            int.TryParse(txtIdUsuario.Text, out idUsuario);

            clsUsuario_CE obj = new clsUsuario_CE
            {
                IdUsuario = idUsuario,
                IdRol = (int)cmbRol.SelectedValue,
                IdTipoProcedencia = cmbTipoProcedencia.SelectedIndex >= 0
                    ? (int?)cmbTipoProcedencia.SelectedValue
                    : null,
                Nombres = txtNombres.Text.Trim(),
                Apellidos = txtApellidos.Text.Trim(),
                DNI = string.IsNullOrWhiteSpace(txtDNI.Text) ? null : txtDNI.Text.Trim(),
                Correo = txtCorreo.Text.Trim(),
                Contrasena = txtContrasena.Text.Trim(), 
                CodigoUPT = string.IsNullOrWhiteSpace(txtCodigoUPT.Text) ? null : txtCodigoUPT.Text.Trim(),
                Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim(),
                EsVerificado = chkEsVerificado.Checked,
                Estado = chkEstado.Checked
            };

            string mensaje;

            if (esNuevo)
            {
                int idGenerado = objUsuario_CN.mtdInsertar(obj, out mensaje);
                if (idGenerado > 0)
                {
                    MessageBox.Show("Usuario registrado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el usuario.\n" + mensaje,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (esEditar)
            {
                bool ok = objUsuario_CN.mtdActualizar(obj, out mensaje);
                if (ok)
                {
                    MessageBox.Show("Usuario actualizado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el usuario.\n" + mensaje,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            mtdListarUsuarios();
            mtdEstadoInicial();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            mtdEstadoInicial();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdUsuario.Text))
            {
                MessageBox.Show("Seleccione un usuario de la lista.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idUsuario;
            if (!int.TryParse(txtIdUsuario.Text, out idUsuario))
            {
                MessageBox.Show("El Id de usuario no es válido.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("¿Desea dar de baja este usuario?", "Confirmación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string mensaje;
                bool ok = objUsuario_CN.mtdEliminar(idUsuario, out mensaje);

                if (ok)
                {
                    MessageBox.Show("Usuario dado de baja correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtdListarUsuarios();
                    mtdEstadoInicial();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el usuario.\n" + mensaje,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void dgvUsuarios_Click(object sender, EventArgs e)
        {
                                           
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvUsuarios.CurrentRow == null)
                return;

            DataGridViewRow fila = dgvUsuarios.CurrentRow;

           
            txtIdUsuario.Text = fila.Cells["colIdUsuario"].Value?.ToString();

            txtNombres.Text = fila.Cells["colNombres"].Value?.ToString();
            txtApellidos.Text = fila.Cells["colApellidos"].Value?.ToString();
            txtDNI.Text = fila.Cells["colDNI"].Value?.ToString();
            txtCorreo.Text = fila.Cells["colCorreo"].Value?.ToString();
            txtCodigoUPT.Text = fila.Cells["colCodigoUPT"].Value?.ToString();
            txtTelefono.Text = fila.Cells["colTelefono"].Value?.ToString();

      
            if (fila.Cells["colIdRol"].Value != null && fila.Cells["colIdRol"].Value != DBNull.Value)
            {
                int idRol = Convert.ToInt32(fila.Cells["colIdRol"].Value);
                cmbRol.SelectedValue = idRol;
            }
            else
            {
                cmbRol.SelectedIndex = -1;
            }

           
            if (fila.Cells["colIdTipoProcedencia"].Value != null &&
                fila.Cells["colIdTipoProcedencia"].Value != DBNull.Value)
            {
                int idTP = Convert.ToInt32(fila.Cells["colIdTipoProcedencia"].Value);
                cmbTipoProcedencia.SelectedValue = idTP;
            }
            else
            {
                cmbTipoProcedencia.SelectedIndex = -1;
            }

          
            if (fila.Cells["colEsVerificado"].Value != null && fila.Cells["colEsVerificado"].Value != DBNull.Value)
                chkEsVerificado.Checked = Convert.ToBoolean(fila.Cells["colEsVerificado"].Value);
            else
                chkEsVerificado.Checked = false;

            if (fila.Cells["colEstado"].Value != null && fila.Cells["colEstado"].Value != DBNull.Value)
                chkEstado.Checked = Convert.ToBoolean(fila.Cells["colEstado"].Value);
            else
                chkEstado.Checked = true;

     
            esNuevo = false;
            esEditar = true;
            mtdHabilitarCampos(true);
        }

        private void gbDatosPersonales_Enter(object sender, EventArgs e)
        {

        }

        private void btnCargarFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Seleccionar fotografía";
                ofd.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                       
                        rutaFotoActual = ofd.FileName;

                     
                        pbFoto.Image = Image.FromFile(rutaFotoActual);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo cargar la imagen:\n" + ex.Message,
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
    
}
