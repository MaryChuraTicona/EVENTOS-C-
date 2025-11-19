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
    public partial class FrmLogin : Form
    {
        private clsUsuario_CN objUsuario_CN = new clsUsuario_CN();

        private string _captchaTexto;
        private Random _random = new Random();

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            GenerarCaptcha();
            btnRefrescar.Click += btnRefrescar_Click;
            btnCrearCuenta.Click += btnCrearCuenta_Click;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string correo = txtUsuario.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(correo))
            {
                MessageBox.Show("Ingrese su correo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Ingrese su contraseña.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCaptcha.Text))
            {
                MessageBox.Show("Ingrese el texto de verificación (CAPTCHA).", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCaptcha.Focus();
                return;
            }

            if (!string.Equals(txtCaptcha.Text.Trim(), _captchaTexto,
                               StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("El texto de verificación no coincide. Inténtelo nuevamente.",
                    "CAPTCHA incorrecto",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtCaptcha.Clear();
                GenerarCaptcha();
                txtCaptcha.Focus();
                return;
            }


            try
            {
                clsUsuario_CE usuario = objUsuario_CN.mtdLogin(correo, password);

                if (usuario != null)
                {
                   
                    FrmPrincipalMDI frm = new FrmPrincipalMDI(usuario);
                    frm.Show();

                    this.Hide(); 
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas o usuario inactivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCaptcha.Clear();
                    GenerarCaptcha();
                    txtCaptcha.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar iniciar sesión:\n" + ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void GenerarCaptcha()
        {
           
            _captchaTexto = GenerarTextoCaptcha(5);

            if (picCaptcha.Width <= 0 || picCaptcha.Height <= 0)
                return;

            
            Bitmap bmp = new Bitmap(picCaptcha.Width, picCaptcha.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                using (Font font = new Font("Arial", 18, FontStyle.Bold))
                {
                    
                    Color colorTexto = Color.FromArgb(
                        _random.Next(0, 150),
                        _random.Next(0, 150),
                        _random.Next(0, 150));

                    using (Brush brush = new SolidBrush(colorTexto))
                    {
                        var size = g.MeasureString(_captchaTexto, font);
                        float x = (bmp.Width - size.Width) / 2;
                        float y = (bmp.Height - size.Height) / 2;

                        g.DrawString(_captchaTexto, font, brush, x, y);
                    }
                }

               
                using (Pen pen = new Pen(Color.LightGray, 1))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int x1 = _random.Next(0, bmp.Width);
                        int y1 = _random.Next(0, bmp.Height);
                        int x2 = _random.Next(0, bmp.Width);
                        int y2 = _random.Next(0, bmp.Height);
                        g.DrawLine(pen, x1, y1, x2, y2);
                    }
                }
            }

            
            picCaptcha.Image = bmp;
        }

        private string GenerarTextoCaptcha(int length)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            char[] buffer = new char[length];

            for (int i = 0; i < length; i++)
            {
                buffer[i] = chars[_random.Next(chars.Length)];
            }

            return new string(buffer);
        }
        private void panelDerecho_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            txtCaptcha.Clear();
            GenerarCaptcha();
        }

        private void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            using (FrmCrearCuenta frm = new FrmCrearCuenta())
            {
                frm.ShowDialog();
            }
        }
    }

}
