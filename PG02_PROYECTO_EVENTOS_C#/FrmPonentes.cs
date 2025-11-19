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
using System.IO;


namespace PG02_PROYECTO_EVENTOS_C_
{
    public partial class FrmPonentes : Form
    {
        private clsPonente_CN objPonente_CN = new clsPonente_CN();

        private int idPonenteSeleccionado = 0;
        private string rutaFotoActual = null;
        public FrmPonentes()
        {
            InitializeComponent();
        }

        private void gbDatosPersonales_Enter(object sender, EventArgs e)
        {

        }

        private void FrmPonentes_Load(object sender, EventArgs e)
        {
            try
            {
                dgvPonentes.AutoGenerateColumns = false;
                mtdListarPonentes();
                mtdEstadoInicial();

                btnCargarFoto.Click += btnCargarFoto_Click;
                btnGuardar.Click += btnGuardar_Click;
                btnCancelar.Click += btnCancelar_Click;
                btnEliminar.Click += btnEliminar_Click;
                dgvPonentes.CellClick += dgvPonentes_CellClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Ponentes:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mtdEstadoInicial()
        {
            idPonenteSeleccionado = 0;
            mtdLimpiarCampos();
            mtdHabilitarCampos(true);
        }
        private void mtdHabilitarCampos(bool estado)
        {
            txtNombre.Enabled = estado;
            txtApellido.Enabled = estado;
            txtEspecialidad.Enabled = estado;
            txtEmail.Enabled = estado;
            txtTelefono.Enabled = estado;
            rtbBiografia.Enabled = estado;
            btnCargarFoto.Enabled = estado;
            btnGuardar.Enabled = estado;
        }

        private void mtdLimpiarCampos()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEspecialidad.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            rtbBiografia.Text = "";
            pbFoto.Image = null;
            rutaFotoActual = null;
        }

        private bool mtdValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre del ponente.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("Ingrese el apellido del ponente.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return false;
            }

            return true;
        }
        private void mtdListarPonentes()
        {
            DataTable dt = objPonente_CN.mtdListar();
            dgvPonentes.DataSource = dt;
        }

        private void btnCargarFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Seleccionar fotografía del ponente";
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!mtdValidarCampos())
                return;

            clsPonente_CE obj = new clsPonente_CE
            {
                IdPonente = idPonenteSeleccionado,
                Nombres = txtNombre.Text.Trim(),
                Apellidos = txtApellido.Text.Trim(),
                Especialidad = string.IsNullOrWhiteSpace(txtEspecialidad.Text)
                                ? null
                                : txtEspecialidad.Text.Trim(),
                Correo = string.IsNullOrWhiteSpace(txtEmail.Text)
                                ? null
                                : txtEmail.Text.Trim(),
                Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text)
                                ? null
                                : txtTelefono.Text.Trim(),
                Biografia = string.IsNullOrWhiteSpace(rtbBiografia.Text)
                                ? null
                                : rtbBiografia.Text.Trim(),
                FotoURL = rutaFotoActual,
                Estado = true
            };

            string mensaje;

            if (idPonenteSeleccionado == 0)
            {
                
                int idGenerado = objPonente_CN.mtdInsertar(obj, out mensaje);
                if (idGenerado > 0)
                {
                    MessageBox.Show("Ponente registrado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtdListarPonentes();
                    mtdEstadoInicial();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el ponente.\n" + mensaje,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
             
                bool ok = objPonente_CN.mtdActualizar(obj, out mensaje);
                if (ok)
                {
                    MessageBox.Show("Ponente actualizado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtdListarPonentes();
                    mtdEstadoInicial();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el ponente.\n" + mensaje,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea limpiar los campos?", "Confirmación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                mtdEstadoInicial();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idPonenteSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un ponente de la lista.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Desea dar de baja este ponente?", "Confirmación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string mensaje;
                bool ok = objPonente_CN.mtdEliminar(idPonenteSeleccionado, out mensaje);

                if (ok)
                {
                    MessageBox.Show("Ponente dado de baja correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtdListarPonentes();
                    mtdEstadoInicial();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el ponente.\n" + mensaje,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvPonentes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvPonentes.CurrentRow == null)
                return;

            DataGridViewRow fila = dgvPonentes.CurrentRow;

            idPonenteSeleccionado = Convert.ToInt32(
                fila.Cells["colIdPonente"].Value);

            txtNombre.Text = fila.Cells["colNombres"].Value?.ToString();
            txtApellido.Text = fila.Cells["colApellidos"].Value?.ToString();
            txtEspecialidad.Text = fila.Cells["colEspecialidad"].Value?.ToString();
            txtEmail.Text = fila.Cells["colCorreo"].Value?.ToString();
            txtTelefono.Text = fila.Cells["colTelefono"].Value?.ToString();
            rtbBiografia.Text = fila.Cells["Biografia"] != null
                                && fila.Cells["Biografia"].Value != null
                                ? fila.Cells["Biografia"].Value.ToString()
                                : "";

          
            rutaFotoActual = fila.Cells["colFotoURL"].Value?.ToString();
            pbFoto.Image = null;

            if (!string.IsNullOrWhiteSpace(rutaFotoActual) && File.Exists(rutaFotoActual))
            {
                try
                {
                    pbFoto.Image = Image.FromFile(rutaFotoActual);
                }
                catch
                {
                   
                    pbFoto.Image = null;
                }
            }
        }
    }
}
