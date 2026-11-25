
namespace Presentacion
{
    partial class FormMovimientos
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.ComboBox cmbProductos;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Button btnEntrada;
        private System.Windows.Forms.Button btnVenta;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new System.Windows.Forms.Label();
            lblProducto = new System.Windows.Forms.Label();
            lblCantidad = new System.Windows.Forms.Label();
            lblStock = new System.Windows.Forms.Label();
            lblResultado = new System.Windows.Forms.Label();
            cmbProductos = new System.Windows.Forms.ComboBox();
            nudCantidad = new System.Windows.Forms.NumericUpDown();
            btnEntrada = new System.Windows.Forms.Button();
            btnVenta = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            SuspendLayout();

            // lblTitulo
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            lblTitulo.Location = new System.Drawing.Point(30, 25);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new System.Drawing.Size(340, 26);
            lblTitulo.Text = "REGISTRO DE MOVIMIENTOS";

            // lblProducto
            lblProducto.AutoSize = true;
            lblProducto.Location = new System.Drawing.Point(35, 90);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new System.Drawing.Size(115, 15);
            lblProducto.Text = "Seleccionar producto:";

            // cmbProductos
            cmbProductos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbProductos.Location = new System.Drawing.Point(190, 85);
            cmbProductos.Name = "cmbProductos";
            cmbProductos.Size = new System.Drawing.Size(300, 23);
            cmbProductos.SelectedIndexChanged += CmbProductos_SelectedIndexChanged;

            // lblCantidad
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new System.Drawing.Point(35, 145);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new System.Drawing.Size(58, 15);
            lblCantidad.Text = "Cantidad:";

            // nudCantidad
            nudCantidad.Location = new System.Drawing.Point(190, 140);
            nudCantidad.Maximum = 100000;
            nudCantidad.Minimum = 1;
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new System.Drawing.Size(100, 23);
            nudCantidad.Value = 1;

            // lblStock
            lblStock.AutoSize = true;
            lblStock.Location = new System.Drawing.Point(35, 190);
            lblStock.Name = "lblStock";
            lblStock.Size = new System.Drawing.Size(130, 15);
            lblStock.Text = "Stock disponible: -";

            // btnEntrada
            btnEntrada.Location = new System.Drawing.Point(35, 240);
            btnEntrada.Name = "btnEntrada";
            btnEntrada.Size = new System.Drawing.Size(180, 40);
            btnEntrada.Text = "Registrar entrada";
            btnEntrada.UseVisualStyleBackColor = true;
            btnEntrada.Click += BtnEntrada_Click;

            // btnVenta
            btnVenta.Location = new System.Drawing.Point(240, 240);
            btnVenta.Name = "btnVenta";
            btnVenta.Size = new System.Drawing.Size(180, 40);
            btnVenta.Text = "Registrar venta";
            btnVenta.UseVisualStyleBackColor = true;
            btnVenta.Click += BtnVenta_Click;

            // lblResultado
            lblResultado.AutoSize = true;
            lblResultado.Location = new System.Drawing.Point(35, 310);
            lblResultado.Name = "lblResultado";
            lblResultado.Size = new System.Drawing.Size(230, 15);
            lblResultado.Text = "Seleccione un producto para comenzar.";

            // FormMovimientos
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(600, 400);
            Controls.Add(lblTitulo);
            Controls.Add(lblProducto);
            Controls.Add(cmbProductos);
            Controls.Add(lblCantidad);
            Controls.Add(nudCantidad);
            Controls.Add(lblStock);
            Controls.Add(btnEntrada);
            Controls.Add(btnVenta);
            Controls.Add(lblResultado);
            Name = "FormMovimientos";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Movimientos de inventario";

            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
