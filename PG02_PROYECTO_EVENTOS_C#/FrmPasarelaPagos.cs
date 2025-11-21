using Microsoft.Win32;
using QRCoder;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PG02_PROYECTO_UNIDAD3_EVENTOS
{
    public partial class FrmPasarelaPagos : Form
    {
        private string connectionString = @"Server=TU_SERVIDOR;Database=TU_BASE;Trusted_Connection=True;";

        private string rutaComprobantes = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Comprobantes");

        public FrmPasarelaPagos()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            if (!Directory.Exists(rutaComprobantes))
                Directory.CreateDirectory(rutaComprobantes);

            txtMonto.Text = "0.00";
            txtEstadoPasarela.Text = "Pendiente";
            txtIdTransaccionExterna.Text = "";
            txtMontoAutorizado.Text = "";
            txtCodigoAutorizacion.Text = "";
            txtRawResponseJSON.Text = "";
            cmbMetodoPago.SelectedIndex = 0;
            picQR.Image = null;
            picComprobante.Image = null;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarFormulario();
            txtConcepto.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCargarComprobante_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string archivo = openFileDialog.FileName;
                try
                {
                    picComprobante.Image = Image.FromFile(archivo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error cargando imagen: " + ex.Message);
                }
            }
        }

        private void btnGenerarPago_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtMonto.Text.Trim(), out decimal monto) || monto <= 0)
            {
                MessageBox.Show("Ingrese un monto válido (> 0).");
                return;
            }

            string metodo = cmbMetodoPago.SelectedItem?.ToString() ?? "Yape";

            string telefonoDestino = "999999999";
            string uri = "";

            if (metodo == "Yape")
                uri = $"yape://pay?phone={telefonoDestino}&amount={monto:F2}";
            else
                uri = $"plin://pay?phone={telefonoDestino}&amount={monto:F2}";

            string referenciaInterna = $"ORD-{txtIdOrden.Text.Trim()}-{DateTime.Now:yyyyMMddHHmmss}";
            string contenidoQR = $"{uri}|ref={referenciaInterna}|concepto={txtConcepto.Text.Trim()}";

            txtIdTransaccionExterna.Text = referenciaInterna;
            txtEstadoPasarela.Text = "QR GENERADO";

            try
            {
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(contenidoQR, QRCodeGenerator.ECCLevel.Q))
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    Bitmap qrBitmap = qrCode.GetGraphic(20);
                    picQR.Image = qrBitmap;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generando el QR: " + ex.Message);
            }
        }

        private void btnVerificarPago_Click(object sender, EventArgs e)
        {
            if (picComprobante.Image == null)
            {
                MessageBox.Show("No se ha cargado comprobante.");
                return;
            }

            var res = MessageBox.Show("¿Confirmar pago y marcar VALIDADO?", "Verificar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                txtEstadoPasarela.Text = "Validado";
                txtMontoAutorizado.Text = txtMonto.Text.Trim();
                txtCodigoAutorizacion.Text = "AUT-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                txtRawResponseJSON.Text = "{ \"metodo\":\"" + cmbMetodoPago.SelectedItem + "\", \"verificado_por\":\"admin\" }";
                MessageBox.Show("Pago VALIDADO.");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtIdOrden.Text.Trim(), out int idOrden))
            {
                MessageBox.Show("Ingrese un IdOrden válido.");
                return;
            }

            if (!decimal.TryParse(txtMonto.Text.Trim(), out decimal monto))
            {
                MessageBox.Show("Monto inválido.");
                return;
            }

            int idPasarela = (cmbMetodoPago.SelectedItem?.ToString() == "Plin") ? 2 : 1;

            string nombreArchivoComprobante = null;
            if (picComprobante.Image != null)
            {
                try
                {
                    nombreArchivoComprobante = $"CMP_{idOrden}_{DateTime.Now:yyyyMMddHHmmss}.png";
                    string rutaCompleta = Path.Combine(rutaComprobantes, nombreArchivoComprobante);
                    picComprobante.Image.Save(rutaCompleta);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar comprobante: " + ex.Message);
                    return;
                }
            }

            string idTransaccionExterna = txtIdTransaccionExterna.Text.Trim();
            string estado = txtEstadoPasarela.Text.Trim();
            decimal? montoAutorizado = null;
            if (decimal.TryParse(txtMontoAutorizado.Text.Trim(), out decimal mAuto)) montoAutorizado = mAuto;

            string codigoAut = txtCodigoAutorizacion.Text.Trim();
            string rawJson = txtRawResponseJSON.Text.Trim();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    string insert = @"
INSERT INTO TransaccionPasarela
(IdOrdenPago, IdPasarelaPago, IdTransaccionExterna, EstadoPasarela, MontoAutorizado, CodigoAutorizacion, FechaTransaccion, RawResponseJSON)
VALUES
(@IdOrdenPago, @IdPasarelaPago, @IdTransaccionExterna, @EstadoPasarela, @MontoAutorizado, @CodigoAutorizacion, SYSDATETIME(), @RawResponseJSON);
SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(insert, cn))
                    {
                        cmd.Parameters.Add("@IdOrdenPago", SqlDbType.Int).Value = idOrden;
                        cmd.Parameters.Add("@IdPasarelaPago", SqlDbType.Int).Value = idPasarela;
                        cmd.Parameters.Add("@IdTransaccionExterna", SqlDbType.VarChar, 150).Value = (object)idTransaccionExterna ?? DBNull.Value;
                        cmd.Parameters.Add("@EstadoPasarela", SqlDbType.VarChar, 30).Value = (object)estado ?? DBNull.Value;
                        cmd.Parameters.Add("@MontoAutorizado", SqlDbType.Decimal).Value = (object)montoAutorizado ?? DBNull.Value;
                        cmd.Parameters["@MontoAutorizado"].Precision = 10;
                        cmd.Parameters["@MontoAutorizado"].Scale = 2;
                        cmd.Parameters.Add("@CodigoAutorizacion", SqlDbType.VarChar, 100).Value = (object)codigoAut ?? DBNull.Value;
                        cmd.Parameters.Add("@RawResponseJSON", SqlDbType.NVarChar).Value = (object)rawJson ?? DBNull.Value;

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            int idTransaccionInsertada = Convert.ToInt32(result);
                            MessageBox.Show("Transacción guardada con ID: " + idTransaccionInsertada);
                        }
                        else
                        {
                            MessageBox.Show("Error insertando transacción.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
