using CapaEntidad;
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
    public partial class FrmPrincipalMDI : Form
    {
        private clsUsuario_CE usuarioActivo;

        public FrmPrincipalMDI(clsUsuario_CE usuario)
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            usuarioActivo = usuario;
            lblNombreUsuario.Text = usuario.Nombres + " " + usuario.Apellidos;
            lblRolUsuario.Text = usuario.NombreRol;

        }

        private void FrmPrincipalMDI_Load(object sender, EventArgs e)
        {

           
        }
        private void mtdAbrirFormularioHijo(Form frmHijo)
        {
           
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }

            frmHijo.MdiParent = this;
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.Show();
        }

        private void btnEventos_Click(object sender, EventArgs e)
        {
            mtdAbrirFormularioHijo(new FrmEventos());
        }

        private void btnPonentes_Click(object sender, EventArgs e)
        {
            mtdAbrirFormularioHijo(new FrmPonentes());
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            mtdAbrirFormularioHijo(new FrmUsuarios());
        }

        private void btnPasarelaPagos_Click(object sender, EventArgs e)
        {
            mtdAbrirFormularioHijo(new FrmPasarelaPagos());
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Confirmación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
