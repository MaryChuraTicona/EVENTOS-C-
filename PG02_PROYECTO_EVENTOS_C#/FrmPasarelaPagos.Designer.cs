namespace PG02_PROYECTO_UNIDAD3_EVENTOS
{
    partial class FrmPasarelaPagos
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.gbDatosPago = new System.Windows.Forms.GroupBox();
            this.lblIdOrden = new System.Windows.Forms.Label();
            this.txtIdOrden = new System.Windows.Forms.TextBox();
            this.lblConcepto = new System.Windows.Forms.Label();
            this.txtConcepto = new System.Windows.Forms.TextBox();
            this.lblMonto = new System.Windows.Forms.Label();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.lblMetodo = new System.Windows.Forms.Label();
            this.cmbMetodoPago = new System.Windows.Forms.ComboBox();
            this.btnCargarComprobante = new System.Windows.Forms.Button();
            this.lblComprobante = new System.Windows.Forms.Label();
            this.picComprobante = new System.Windows.Forms.PictureBox();
            this.gbStripeInfo = new System.Windows.Forms.GroupBox();
            this.lblIdTransaccionExterna = new System.Windows.Forms.Label();
            this.txtIdTransaccionExterna = new System.Windows.Forms.TextBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtEstadoPasarela = new System.Windows.Forms.TextBox();
            this.lblMontoAutorizado = new System.Windows.Forms.Label();
            this.txtMontoAutorizado = new System.Windows.Forms.TextBox();
            this.lblCodigoAut = new System.Windows.Forms.Label();
            this.txtCodigoAutorizacion = new System.Windows.Forms.TextBox();
            this.lblRaw = new System.Windows.Forms.Label();
            this.txtRawResponseJSON = new System.Windows.Forms.TextBox();
            this.gbQR = new System.Windows.Forms.GroupBox();
            this.picQR = new System.Windows.Forms.PictureBox();
            this.btnGenerarPago = new System.Windows.Forms.Button();
            this.btnVerificarPago = new System.Windows.Forms.Button();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panelSuperior.SuspendLayout();
            this.panelContenedor.SuspendLayout();
            this.gbDatosPago.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picComprobante)).BeginInit();
            this.gbStripeInfo.SuspendLayout();
            this.gbQR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).BeginInit();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelSuperior.Controls.Add(this.lblTitulo);
            this.panelSuperior.Controls.Add(this.btnCerrar);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(955, 54);
            this.panelSuperior.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(218, 28);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Pasarela de Pago";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(1670, 10);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(30, 30);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // panelContenedor
            // 
            this.panelContenedor.AutoScroll = true;
            this.panelContenedor.BackColor = System.Drawing.Color.White;
            this.panelContenedor.Controls.Add(this.gbDatosPago);
            this.panelContenedor.Controls.Add(this.gbStripeInfo);
            this.panelContenedor.Controls.Add(this.gbQR);
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(0, 54);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Padding = new System.Windows.Forms.Padding(20);
            this.panelContenedor.Size = new System.Drawing.Size(955, 596);
            this.panelContenedor.TabIndex = 1;
            this.panelContenedor.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContenedor_Paint);
            // 
            // gbDatosPago
            // 
            this.gbDatosPago.Controls.Add(this.lblIdOrden);
            this.gbDatosPago.Controls.Add(this.txtIdOrden);
            this.gbDatosPago.Controls.Add(this.lblConcepto);
            this.gbDatosPago.Controls.Add(this.txtConcepto);
            this.gbDatosPago.Controls.Add(this.lblMonto);
            this.gbDatosPago.Controls.Add(this.txtMonto);
            this.gbDatosPago.Controls.Add(this.lblMetodo);
            this.gbDatosPago.Controls.Add(this.cmbMetodoPago);
            this.gbDatosPago.Controls.Add(this.btnCargarComprobante);
            this.gbDatosPago.Controls.Add(this.lblComprobante);
            this.gbDatosPago.Controls.Add(this.picComprobante);
            this.gbDatosPago.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.gbDatosPago.Location = new System.Drawing.Point(25, 6);
            this.gbDatosPago.Name = "gbDatosPago";
            this.gbDatosPago.Size = new System.Drawing.Size(854, 209);
            this.gbDatosPago.TabIndex = 0;
            this.gbDatosPago.TabStop = false;
            this.gbDatosPago.Text = "Datos del Pago";
            // 
            // lblIdOrden
            // 
            this.lblIdOrden.AutoSize = true;
            this.lblIdOrden.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.lblIdOrden.Location = new System.Drawing.Point(30, 30);
            this.lblIdOrden.Name = "lblIdOrden";
            this.lblIdOrden.Size = new System.Drawing.Size(85, 17);
            this.lblIdOrden.TabIndex = 0;
            this.lblIdOrden.Text = "Id Orden ID:";
            // 
            // txtIdOrden
            // 
            this.txtIdOrden.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.txtIdOrden.Location = new System.Drawing.Point(30, 50);
            this.txtIdOrden.Name = "txtIdOrden";
            this.txtIdOrden.Size = new System.Drawing.Size(200, 23);
            this.txtIdOrden.TabIndex = 1;
            // 
            // lblConcepto
            // 
            this.lblConcepto.AutoSize = true;
            this.lblConcepto.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.lblConcepto.Location = new System.Drawing.Point(250, 30);
            this.lblConcepto.Name = "lblConcepto";
            this.lblConcepto.Size = new System.Drawing.Size(79, 17);
            this.lblConcepto.TabIndex = 2;
            this.lblConcepto.Text = "Concepto:";
            // 
            // txtConcepto
            // 
            this.txtConcepto.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.txtConcepto.Location = new System.Drawing.Point(250, 50);
            this.txtConcepto.Name = "txtConcepto";
            this.txtConcepto.Size = new System.Drawing.Size(370, 23);
            this.txtConcepto.TabIndex = 3;
            // 
            // lblMonto
            // 
            this.lblMonto.AutoSize = true;
            this.lblMonto.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.lblMonto.Location = new System.Drawing.Point(30, 90);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(54, 17);
            this.lblMonto.TabIndex = 4;
            this.lblMonto.Text = "Monto:";
            // 
            // txtMonto
            // 
            this.txtMonto.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.txtMonto.Location = new System.Drawing.Point(30, 110);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(200, 23);
            this.txtMonto.TabIndex = 5;
            this.txtMonto.Text = "0.00";
            // 
            // lblMetodo
            // 
            this.lblMetodo.AutoSize = true;
            this.lblMetodo.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.lblMetodo.Location = new System.Drawing.Point(250, 90);
            this.lblMetodo.Name = "lblMetodo";
            this.lblMetodo.Size = new System.Drawing.Size(123, 17);
            this.lblMetodo.TabIndex = 6;
            this.lblMetodo.Text = "Método de Pago:";
            // 
            // cmbMetodoPago
            // 
            this.cmbMetodoPago.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cmbMetodoPago.FormattingEnabled = true;
            this.cmbMetodoPago.Items.AddRange(new object[] {
            "Yape",
            "Plin"});
            this.cmbMetodoPago.Location = new System.Drawing.Point(250, 110);
            this.cmbMetodoPago.Name = "cmbMetodoPago";
            this.cmbMetodoPago.Size = new System.Drawing.Size(150, 25);
            this.cmbMetodoPago.TabIndex = 7;
            // 
            // btnCargarComprobante
            // 
            this.btnCargarComprobante.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnCargarComprobante.FlatAppearance.BorderSize = 0;
            this.btnCargarComprobante.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCargarComprobante.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.btnCargarComprobante.ForeColor = System.Drawing.Color.White;
            this.btnCargarComprobante.Location = new System.Drawing.Point(650, 60);
            this.btnCargarComprobante.Name = "btnCargarComprobante";
            this.btnCargarComprobante.Size = new System.Drawing.Size(150, 30);
            this.btnCargarComprobante.TabIndex = 8;
            this.btnCargarComprobante.Text = "Cargar Comprobante";
            this.btnCargarComprobante.UseVisualStyleBackColor = false;
            this.btnCargarComprobante.Click += new System.EventHandler(this.btnCargarComprobante_Click);
            // 
            // lblComprobante
            // 
            this.lblComprobante.AutoSize = true;
            this.lblComprobante.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.lblComprobante.Location = new System.Drawing.Point(650, 30);
            this.lblComprobante.Name = "lblComprobante";
            this.lblComprobante.Size = new System.Drawing.Size(106, 17);
            this.lblComprobante.TabIndex = 9;
            this.lblComprobante.Text = "Comprobante:";
            // 
            // picComprobante
            // 
            this.picComprobante.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picComprobante.Location = new System.Drawing.Point(650, 100);
            this.picComprobante.Name = "picComprobante";
            this.picComprobante.Size = new System.Drawing.Size(150, 100);
            this.picComprobante.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picComprobante.TabIndex = 10;
            this.picComprobante.TabStop = false;
            // 
            // gbStripeInfo
            // 
            this.gbStripeInfo.Controls.Add(this.lblIdTransaccionExterna);
            this.gbStripeInfo.Controls.Add(this.txtIdTransaccionExterna);
            this.gbStripeInfo.Controls.Add(this.lblEstado);
            this.gbStripeInfo.Controls.Add(this.txtEstadoPasarela);
            this.gbStripeInfo.Controls.Add(this.lblMontoAutorizado);
            this.gbStripeInfo.Controls.Add(this.txtMontoAutorizado);
            this.gbStripeInfo.Controls.Add(this.lblCodigoAut);
            this.gbStripeInfo.Controls.Add(this.txtCodigoAutorizacion);
            this.gbStripeInfo.Controls.Add(this.lblRaw);
            this.gbStripeInfo.Controls.Add(this.txtRawResponseJSON);
            this.gbStripeInfo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.gbStripeInfo.Location = new System.Drawing.Point(31, 212);
            this.gbStripeInfo.Name = "gbStripeInfo";
            this.gbStripeInfo.Size = new System.Drawing.Size(854, 210);
            this.gbStripeInfo.TabIndex = 1;
            this.gbStripeInfo.TabStop = false;
            this.gbStripeInfo.Text = "Información de Transacción (solo lectura)";
            // 
            // lblIdTransaccionExterna
            // 
            this.lblIdTransaccionExterna.AutoSize = true;
            this.lblIdTransaccionExterna.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.lblIdTransaccionExterna.Location = new System.Drawing.Point(30, 25);
            this.lblIdTransaccionExterna.Name = "lblIdTransaccionExterna";
            this.lblIdTransaccionExterna.Size = new System.Drawing.Size(155, 17);
            this.lblIdTransaccionExterna.TabIndex = 0;
            this.lblIdTransaccionExterna.Text = "Id Transacción Externa:";
            // 
            // txtIdTransaccionExterna
            // 
            this.txtIdTransaccionExterna.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.txtIdTransaccionExterna.Location = new System.Drawing.Point(30, 45);
            this.txtIdTransaccionExterna.Name = "txtIdTransaccionExterna";
            this.txtIdTransaccionExterna.ReadOnly = true;
            this.txtIdTransaccionExterna.Size = new System.Drawing.Size(300, 23);
            this.txtIdTransaccionExterna.TabIndex = 1;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.lblEstado.Location = new System.Drawing.Point(350, 25);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(56, 17);
            this.lblEstado.TabIndex = 2;
            this.lblEstado.Text = "Estado:";
            // 
            // txtEstadoPasarela
            // 
            this.txtEstadoPasarela.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.txtEstadoPasarela.Location = new System.Drawing.Point(350, 45);
            this.txtEstadoPasarela.Name = "txtEstadoPasarela";
            this.txtEstadoPasarela.ReadOnly = true;
            this.txtEstadoPasarela.Size = new System.Drawing.Size(200, 23);
            this.txtEstadoPasarela.TabIndex = 3;
            // 
            // lblMontoAutorizado
            // 
            this.lblMontoAutorizado.AutoSize = true;
            this.lblMontoAutorizado.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.lblMontoAutorizado.Location = new System.Drawing.Point(30, 80);
            this.lblMontoAutorizado.Name = "lblMontoAutorizado";
            this.lblMontoAutorizado.Size = new System.Drawing.Size(129, 17);
            this.lblMontoAutorizado.TabIndex = 4;
            this.lblMontoAutorizado.Text = "Monto Autorizado:";
            // 
            // txtMontoAutorizado
            // 
            this.txtMontoAutorizado.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.txtMontoAutorizado.Location = new System.Drawing.Point(30, 100);
            this.txtMontoAutorizado.Name = "txtMontoAutorizado";
            this.txtMontoAutorizado.ReadOnly = true;
            this.txtMontoAutorizado.Size = new System.Drawing.Size(200, 23);
            this.txtMontoAutorizado.TabIndex = 5;
            // 
            // lblCodigoAut
            // 
            this.lblCodigoAut.AutoSize = true;
            this.lblCodigoAut.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.lblCodigoAut.Location = new System.Drawing.Point(250, 80);
            this.lblCodigoAut.Name = "lblCodigoAut";
            this.lblCodigoAut.Size = new System.Drawing.Size(147, 17);
            this.lblCodigoAut.TabIndex = 6;
            this.lblCodigoAut.Text = "Código Autorización:";
            // 
            // txtCodigoAutorizacion
            // 
            this.txtCodigoAutorizacion.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.txtCodigoAutorizacion.Location = new System.Drawing.Point(250, 100);
            this.txtCodigoAutorizacion.Name = "txtCodigoAutorizacion";
            this.txtCodigoAutorizacion.ReadOnly = true;
            this.txtCodigoAutorizacion.Size = new System.Drawing.Size(200, 23);
            this.txtCodigoAutorizacion.TabIndex = 7;
            // 
            // lblRaw
            // 
            this.lblRaw.AutoSize = true;
            this.lblRaw.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.lblRaw.Location = new System.Drawing.Point(30, 135);
            this.lblRaw.Name = "lblRaw";
            this.lblRaw.Size = new System.Drawing.Size(114, 17);
            this.lblRaw.TabIndex = 8;
            this.lblRaw.Text = "Respuesta JSON:";
            // 
            // txtRawResponseJSON
            // 
            this.txtRawResponseJSON.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.txtRawResponseJSON.Location = new System.Drawing.Point(30, 155);
            this.txtRawResponseJSON.Multiline = true;
            this.txtRawResponseJSON.Name = "txtRawResponseJSON";
            this.txtRawResponseJSON.ReadOnly = true;
            this.txtRawResponseJSON.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRawResponseJSON.Size = new System.Drawing.Size(700, 40);
            this.txtRawResponseJSON.TabIndex = 9;
            // 
            // gbQR
            // 
            this.gbQR.Controls.Add(this.picQR);
            this.gbQR.Controls.Add(this.btnGenerarPago);
            this.gbQR.Controls.Add(this.btnVerificarPago);
            this.gbQR.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.gbQR.Location = new System.Drawing.Point(31, 428);
            this.gbQR.Name = "gbQR";
            this.gbQR.Size = new System.Drawing.Size(854, 168);
            this.gbQR.TabIndex = 2;
            this.gbQR.TabStop = false;
            this.gbQR.Text = "QR de Pago / Acciones";
            // 
            // picQR
            // 
            this.picQR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picQR.Location = new System.Drawing.Point(30, 30);
            this.picQR.Name = "picQR";
            this.picQR.Size = new System.Drawing.Size(200, 200);
            this.picQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picQR.TabIndex = 0;
            this.picQR.TabStop = false;
            // 
            // btnGenerarPago
            // 
            this.btnGenerarPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnGenerarPago.FlatAppearance.BorderSize = 0;
            this.btnGenerarPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarPago.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.btnGenerarPago.ForeColor = System.Drawing.Color.White;
            this.btnGenerarPago.Location = new System.Drawing.Point(260, 40);
            this.btnGenerarPago.Name = "btnGenerarPago";
            this.btnGenerarPago.Size = new System.Drawing.Size(150, 40);
            this.btnGenerarPago.TabIndex = 1;
            this.btnGenerarPago.Text = "GENERAR QR";
            this.btnGenerarPago.UseVisualStyleBackColor = false;
            this.btnGenerarPago.Click += new System.EventHandler(this.btnGenerarPago_Click);
            // 
            // btnVerificarPago
            // 
            this.btnVerificarPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnVerificarPago.FlatAppearance.BorderSize = 0;
            this.btnVerificarPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerificarPago.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.btnVerificarPago.ForeColor = System.Drawing.Color.White;
            this.btnVerificarPago.Location = new System.Drawing.Point(260, 90);
            this.btnVerificarPago.Name = "btnVerificarPago";
            this.btnVerificarPago.Size = new System.Drawing.Size(150, 40);
            this.btnVerificarPago.TabIndex = 2;
            this.btnVerificarPago.Text = "VERIFICAR PAGO";
            this.btnVerificarPago.UseVisualStyleBackColor = false;
            this.btnVerificarPago.Click += new System.EventHandler(this.btnVerificarPago_Click);
            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelBotones.Controls.Add(this.btnNuevo);
            this.panelBotones.Controls.Add(this.btnGuardar);
            this.panelBotones.Controls.Add(this.btnCancelar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Location = new System.Drawing.Point(0, 650);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(955, 70);
            this.panelBotones.TabIndex = 2;
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(520, 15);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(120, 40);
            this.btnNuevo.TabIndex = 2;
            this.btnNuevo.Text = "NUEVO";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(650, 15);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 40);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "GUARDAR";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(790, 15);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(120, 40);
            this.btnCancelar.TabIndex = 0;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp";
            this.openFileDialog.Title = "Seleccionar comprobante";
            // 
            // FrmPasarelaPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 720);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.panelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPasarelaPago";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pasarela de Pago";
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelContenedor.ResumeLayout(false);
            this.gbDatosPago.ResumeLayout(false);
            this.gbDatosPago.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picComprobante)).EndInit();
            this.gbStripeInfo.ResumeLayout(false);
            this.gbStripeInfo.PerformLayout();
            this.gbQR.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).EndInit();
            this.panelBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.GroupBox gbDatosPago;
        private System.Windows.Forms.Label lblConcepto;
        private System.Windows.Forms.TextBox txtConcepto;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Label lblMetodo;
        private System.Windows.Forms.ComboBox cmbMetodoPago;
        private System.Windows.Forms.GroupBox gbStripeInfo;
        private System.Windows.Forms.Label lblIdTransaccionExterna;
        private System.Windows.Forms.TextBox txtIdTransaccionExterna;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.TextBox txtEstadoPasarela;
        private System.Windows.Forms.Label lblMontoAutorizado;
        private System.Windows.Forms.TextBox txtMontoAutorizado;
        private System.Windows.Forms.Label lblCodigoAut;
        private System.Windows.Forms.TextBox txtCodigoAutorizacion;
        private System.Windows.Forms.Label lblRaw;
        private System.Windows.Forms.TextBox txtRawResponseJSON;
        private System.Windows.Forms.GroupBox gbQR;
        private System.Windows.Forms.PictureBox picQR;
        private System.Windows.Forms.Button btnGenerarPago;
        private System.Windows.Forms.Button btnVerificarPago;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.PictureBox picComprobante;
        private System.Windows.Forms.Button btnCargarComprobante;
        private System.Windows.Forms.Label lblComprobante;
        private System.Windows.Forms.Label lblIdOrden;
        private System.Windows.Forms.TextBox txtIdOrden;
    }
}
