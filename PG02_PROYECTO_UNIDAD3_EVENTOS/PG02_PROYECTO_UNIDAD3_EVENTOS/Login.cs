using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PG02_PROYECTO_UNIDAD3_EVENTOS
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            MessageBox.Show("Login cargado correctamente"); // ← Línea de prueba
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Validación básica
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(usuario))
            {
                MessageBox.Show("Por favor ingrese un usuario.",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor ingrese una contraseña.",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // Aquí implementarías tu lógica de autenticación
            // Ejemplo básico:
            if (usuario == "admin" && password == "1234")
            {
                MessageBox.Show("¡Inicio de sesión exitoso!",
                    "Bienvenido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Aquí abrirías tu formulario principal
                // Form principal = new FormPrincipal();
                // principal.Show();
                // this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.",
                    "Error de autenticación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panelDerecho_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}