using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PG02_PROYECTO_UNIDAD3_EVENTOS
{
    public partial class FrmRegistroEstudiante : Form
    {
        public FrmRegistroEstudiante()
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

                    // Redimensionar si es muy grande
                    if (imagen.Width > 300 || imagen.Height > 300)
                    {
                        imagen = RedimensionarImagen(imagen, 150, 150);
                    }

                    pbFotoEstudiante.Image = imagen;

                    MessageBox.Show("Fotografía del estudiante cargada correctamente.",
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
                GuardarEstudiante();

                MessageBox.Show("Estudiante registrado correctamente.",
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
            // Validar nombre
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            // Validar apellido
            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El apellido es obligatorio.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtApellido.Focus();
                return false;
            }

            // Validar email
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

            // Validar matrícula
            if (string.IsNullOrWhiteSpace(txtMatricula.Text))
            {
                MessageBox.Show("La matrícula es obligatoria.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtMatricula.Focus();
                return false;
            }

            // Validar carrera
            if (string.IsNullOrWhiteSpace(txtCarrera.Text))
            {
                MessageBox.Show("La carrera es obligatoria.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtCarrera.Focus();
                return false;
            }

            // Validar fecha de nacimiento (no puede ser mayor a hoy)
            if (dtpFechaNacimiento.Value > DateTime.Now)
            {
                MessageBox.Show("La fecha de nacimiento no puede ser futura.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                dtpFechaNacimiento.Focus();
                return false;
            }

            // Validar que sea mayor de 15 años
            int edad = DateTime.Now.Year - dtpFechaNacimiento.Value.Year;
            if (DateTime.Now < dtpFechaNacimiento.Value.AddYears(edad))
                edad--;

            if (edad < 15)
            {
                MessageBox.Show("El estudiante debe tener al menos 15 años.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                dtpFechaNacimiento.Focus();
                return false;
            }

            return true;
        }

        private void GuardarEstudiante()
        {
            // Obtener intereses seleccionados
            string intereses = "";
            foreach (string item in clbIntereses.CheckedItems)
            {
                intereses += item + ", ";
            }
            if (intereses.Length > 2)
                intereses = intereses.Substring(0, intereses.Length - 2);

            // Aquí implementarías la lógica para guardar en tu base de datos
            // Por ahora solo mostraremos los datos
            string datos = $"Nombre: {txtNombre.Text} {txtApellido.Text}\n" +
                          $"Email: {txtEmail.Text}\n" +
                          $"Teléfono: {txtTelefono.Text}\n" +
                          $"Fecha Nacimiento: {dtpFechaNacimiento.Value.ToShortDateString()}\n" +
                          $"Matrícula: {txtMatricula.Text}\n" +
                          $"Carrera: {txtCarrera.Text}\n" +
                          $"Semestre: {nudSemestre.Value}\n" +
                          $"Intereses: {intereses}";

            Console.WriteLine("=== DATOS DEL ESTUDIANTE ===");
            Console.WriteLine(datos);

            // Aquí normalmente guardarías en la base de datos
            // Ejemplo:
            // Estudiante estudiante = new Estudiante();
            // estudiante.Nombre = txtNombre.Text;
            // estudiante.Apellido = txtApellido.Text;
            // ... etc.
            // db.Estudiantes.Add(estudiante);
            // db.SaveChanges();
        }

        private void LimpiarFormulario()
        {
            // Limpiar datos personales
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            dtpFechaNacimiento.Value = DateTime.Now.AddYears(-18);

            // Limpiar información académica
            txtMatricula.Clear();
            txtCarrera.Clear();
            nudSemestre.Value = 1;

            // Limpiar intereses
            for (int i = 0; i < clbIntereses.Items.Count; i++)
            {
                clbIntereses.SetItemChecked(i, false);
            }

            // Limpiar foto
            pbFotoEstudiante.Image = null;

            txtNombre.Focus();
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

        private void FrmRegistroEstudiante_Load(object sender, EventArgs e)
        {
            // Configurar fecha de nacimiento por defecto (18 años atrás)
            dtpFechaNacimiento.Value = DateTime.Now.AddYears(-18);

            // Configurar valores por defecto
            nudSemestre.Value = 1;

            // Mensaje de bienvenida opcional
            // MessageBox.Show("Complete todos los campos obligatorios para registrar al estudiante.", 
            //     "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Método adicional para validar solo números en teléfono
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '+' && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // Permitir solo un signo + al inicio
            if (e.KeyChar == '+' && ((TextBox)sender).Text.Contains("+"))
            {
                e.Handled = true;
            }
        }

        // Método para validar matrícula (solo números y letras)
        private void txtMatricula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}