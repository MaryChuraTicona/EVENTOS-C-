using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QRCoder; // Necesario: QRCoder NuGet

namespace PG02_PROYECTO_UNIDAD3_EVENTOS
{
    public partial class FrmPasarelaPago : Form
    {
        // ---- CONFIGURA AQUÍ tu cadena de conexión ----
        // Reemplaza por tu servidor / base de datos
        private string connectionString = @"Server=TU_SERVIDOR;Database=TU_BASE;Trusted_Connection=True;";
        // ------------------------------------------------

        // Ruta donde se guardarán comprobantes (ajusta)
        private string rutaComprobantes = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Comprobantes");

        public FrmPasarelaPago()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            // Inicializaciones varias
            if (!Directory.Exists(rutaComprobantes))
                Directory.CreateDirectory(rutaComprobantes);

            txtMonto.Text = "0.00";
            txtEstadoPasarela.Text = "Pendiente";
            txtIdTransaccionExterna.Text = "";
            txtMontoAutorizado.Text = "";
            txtCodigoAutorizacion.Text = "";
            txtRawResponseJSON.Text = "";
            cmbMetodoPago.SelectedIndex = 0; // Yape por defecto
            picQR.Image = null;
            picComprobante.Image = null;
        }

        // Cerrar
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Nuevo: limpia
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarFormulario();
            txtConcepto.Focus();
        }

        // Cancelar = cerrar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Cargar comprobante de pago (imagen)
        private void btnCargarComprobante_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string archivo = openFileDialog.FileName;
                try
                {
                    picComprobante.Image = Image.FromFile(archivo);
                    // opcional: guardar copia temporal o dejar para guardar al presionar Guardar
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error cargando imagen: " + ex.Message);
                }
            }
        }

        // Generar QR: crea un texto/URI para abrir Yape/Plin (básico) y genera imagen QR
        private void btnGenerarPago_Click(object sender, EventArgs e)
        {
            // Validaciones básicas
            if (!decimal.TryParse(txtMonto.Text.Trim(), out decimal monto) || monto <= 0)
            {
                MessageBox.Show("Ingrese un monto válido (> 0).");
                return;
            }

            string metodo = cmbMetodoPago.SelectedItem?.ToString() ?? "Yape";

            // Generamos un payload simple; puedes adaptar al formato que desees
            // Ejemplo: yape://pay?phone=999999999&amount=20
            // Para el proyecto universitario un URI simple está bien:
            string telefonoDestino = "999999999"; // <- reemplazar por tu número Yape/Plin si quieres
            string uri = "";

            if (metodo == "Yape")
            {
                // Formato simplificado; muchas apps abren URI custom
                uri = $"yape://pay?phone={telefonoDestino}&amount={monto:F2}";
            }
            else // Plin
            {
                uri = $"plin://pay?phone={telefonoDestino}&amount={monto:F2}";
            }

            // También incluimos un URI interno de tu sistema para referencia
            // por ejemplo para registrar la TransaccionInterna antes de pagar:
            string referenciaInterna = $"ORD-{txtIdOrden.Text.Trim()}-{DateTime.Now:yyyyMMddHHmmss}";
            string contenidoQR = $"{uri}|ref={referenciaInterna}|concepto={txtConcepto.Text.Trim()}";

            // Guardamos el id transaccion externa provisional como la referencia interna
            txtIdTransaccionExterna.Text = referenciaInterna;
            txtEstadoPasarela.Text = "QR GENERADO";

            // Generar QR con QRCoder
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

        // Verificar Pago (manual / simulado)
        // En un sistema real, aquí el admin validaría el comprobante y se pondría "Validado"
        private void btnVerificarPago_Click(object sender, EventArgs e)
        {
            // Simulación: si hay imagen de comprobante, marcamos "Validado"
            if (picComprobante.Image == null)
            {
                MessageBox.Show("No se ha cargado comprobante. El usuario debe subir su comprobante para validar.");
                return;
            }

            // En una versión simple se pide al admin confirmar
            var res = MessageBox.Show("¿Confirmas que el pago es correcto y deseas marcarlo como VALIDADO?", "Verificar pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                txtEstadoPasarela.Text = "Validado";
                txtMontoAutorizado.Text = txtMonto.Text.Trim();
                txtCodigoAutorizacion.Text = "AUT-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                txtRawResponseJSON.Text = "{ \"metodo\":\"" + cmbMetodoPago.SelectedItem + "\", \"verificado_por\":\"admin\" }";
                MessageBox.Show("Pago marcado como VALIDADO.");
            }
        }

        // Guardar la transacción en la base de datos (TransaccionPasarela)
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (!int.TryParse(txtIdOrden.Text.Trim(), out int idOrden))
            {
                MessageBox.Show("Ingrese un IdOrden válido (número entero).");
                return;
            }

            if (!decimal.TryParse(txtMonto.Text.Trim(), out decimal monto))
            {
                MessageBox.Show("Ingrese un monto válido.");
                return;
            }

            // Determinar IdPasarelaPago según método (1=Yape, 2=Plin)
            int idPasarela = (cmbMetodoPago.SelectedItem?.ToString() == "Plin") ? 2 : 1;

            // Guardar comprobante (si existe) en carpeta y obtener nombre de archivo
            string nombreArchivoComprobante = null;
            if (picComprobante.Image != null)
            {
                try
                {
                    nombreArchivoComprobante = $"CMP_{idOrden}_{DateTime.Now:yyyyMMddHHmmss}.png";
                    string rutaCompleta = Path.Combine(rutaComprobantes, nombreArchivoComprobante);
                    picComprobante.Image.Save(rutaCompleta); // formato PNG por defecto
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error guardando comprobante: " + ex.Message);
                    return;
                }
            }

            // Preparar valores para insertar
            string idTransaccionExterna = txtIdTransaccionExterna.Text.Trim();
            string estado = txtEstadoPasarela.Text.Trim();
            decimal? montoAutorizado = null;
            if (decimal.TryParse(txtMontoAutorizado.Text.Trim(), out decimal mAuto)) montoAutorizado = mAuto;
            string codigoAut = txtCodigoAutorizacion.Text.Trim();
            string rawJson = txtRawResponseJSON.Text.Trim();

            // Insert en TransaccionPasarela
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

                            // Opcional: actualizar un campo de OrdenPago como 'EstadoPago' si tienes esa columna
                            // También puedes guardar el nombre del comprobante asociado en otra tabla o columna
                        }
                        else
                        {
                            MessageBox.Show("No se pudo obtener el Id insertado.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la transacción: " + ex.Message);
            }
        }
    }
}
