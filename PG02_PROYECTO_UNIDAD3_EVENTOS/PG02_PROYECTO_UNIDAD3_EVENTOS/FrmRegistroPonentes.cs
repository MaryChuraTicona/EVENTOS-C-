using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PG02_PROYECTO_UNIDAD3_EVENTOS
{
    public partial class frmRegistroPonentes : Form
    {
        public frmRegistroPonentes()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCargarFoto_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    // Cargar la imagen
                    Image imagen = Image.FromFile(filePath);

                    // Redimensionar si es muy grande (opcional)
                    if (imagen.Width > 300 || imagen.Height > 300)
                    {
                        imagen = RedimensionarImagen(imagen, 150, 150);
                    }

                    pbFoto.Image = imagen;

                    MessageBox.Show("Fotografía cargada correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la imagen: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private Image RedimensionarImagen(Image imagen, int nuevoAncho, int nuevoAlto)
        {
            Bitmap nuevaImagen = new Bitmap(nuevoAncho, nuevoAlto);
            using (Graphics graphics = Graphics.FromImage(nuevaImagen))
            {
                graphics.DrawImage(imagen, 0, 0, nuevoAncho, nuevoAlto);
            }
            return nuevaImagen;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos())
                return;

            try
            {
                // Aquí iría la lógica para guardar en la base de datos
                GuardarPonente();

                MessageBox.Show("Ponente registrado correctamente.",
                    "Registro Exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El apellido es obligatorio.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtApellido.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("El email es obligatorio.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Por favor ingrese un email válido.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTituloCharla.Text))
            {
                MessageBox.Show("El título de la charla es obligatorio.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtTituloCharla.Focus();
                return false;
            }

            return true;
        }

        private void GuardarPonente()
        {
            // Aquí implementarías la lógica para guardar en tu base de datos
            // Por ahora solo mostraremos los datos en un MessageBox
            string datos = $"Nombre: {txtNombre.Text} {txtApellido.Text}\n" +
                          $"Email: {txtEmail.Text}\n" +
                          $"Especialidad: {txtEspecialidad.Text}\n" +
                          $"Teléfono: {txtTelefono.Text}\n" +
                          $"Tipo Evento: {cbTipoEvento.Text}\n" +
                          $"Título Charla: {txtTituloCharla.Text}\n" +
                          $"Duración: {txtDuracion.Text} minutos\n" +
                          $"Biografía: {rtbBiografia.Text}";

            Console.WriteLine(datos); // Para ver en la consola
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtEspecialidad.Clear();
            txtTelefono.Clear();
            txtTituloCharla.Clear();
            txtDuracion.Text = "60";
            cbTipoEvento.SelectedIndex = -1;
            rtbBiografia.Clear();
            pbFoto.Image = null;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "¿Está seguro de que desea cancelar? Se perderán todos los datos no guardados.",
                "Confirmar Cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void frmRegistroPonentes_Load(object sender, EventArgs e)
        {
            // Configuración inicial
            txtDuracion.Text = "60";
        }
    }
}