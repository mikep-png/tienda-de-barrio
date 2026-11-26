
namespace Presentacion
{
    partial class FormReportes
    {
        private System.ComponentModel.IContainer components = null;

        private ComboBox cmbProductos;
        private NumericUpDown nudCantidad;

        private Button btnRegistrarVenta;
        private Button btnAgregarStock;

        private NumericUpDown nudStockMinimo;

        private Button btnFiltrarStockBajo;
        private Button btnVerTodo;
        private Button btnReporteCategorias;

        private DataGridView dgvReportes;

        private Label label1;
        private Label label2;
        private Label label3;

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
            cmbProductos = new ComboBox();
            nudCantidad = new NumericUpDown();

            btnRegistrarVenta = new Button();
            btnAgregarStock = new Button();

            nudStockMinimo = new NumericUpDown();

            btnFiltrarStockBajo = new Button();
            btnVerTodo = new Button();
            btnReporteCategorias = new Button();

            dgvReportes = new DataGridView();

            label1 = new Label();
            label2 = new Label();
            label3 = new Label();

            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStockMinimo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvReportes).BeginInit();

            SuspendLayout();

            // Producto
            label1.AutoSize = true;
            label1.Location = new Point(20, 22);
            label1.Text = "Producto:";

            cmbProductos.Location = new Point(90, 18);
            cmbProductos.Size = new Size(200, 23);
            cmbProductos.DropDownStyle = ComboBoxStyle.DropDownList;

            // Cantidad
            label2.AutoSize = true;
            label2.Location = new Point(310, 22);
            label2.Text = "Cantidad:";

            nudCantidad.Location = new Point(380, 18);
            nudCantidad.Size = new Size(75, 23);
            nudCantidad.Minimum = 1;
            nudCantidad.Maximum = 10000;
            nudCantidad.Value = 1;

            // Venta
            btnRegistrarVenta.Location = new Point(470, 15);
            btnRegistrarVenta.Size = new Size(120, 30);
            btnRegistrarVenta.Text = "Registrar venta";
            btnRegistrarVenta.Click += btnRegistrarVenta_Click;

            // Agregar stock
            btnAgregarStock.Location = new Point(600, 15);
            btnAgregarStock.Size = new Size(145, 30);
            btnAgregarStock.Text = "Agregar existencias";
            btnAgregarStock.Click += btnAgregarStock_Click;

            // Stock mínimo
            label3.AutoSize = true;
            label3.Location = new Point(20, 68);
            label3.Text = "Stock mínimo:";

            nudStockMinimo.Location = new Point(115, 64);
            nudStockMinimo.Size = new Size(75, 23);
            nudStockMinimo.Minimum = 0;
            nudStockMinimo.Maximum = 10000;
            nudStockMinimo.Value = 5;

            // Filtrar stock bajo
            btnFiltrarStockBajo.Location = new Point(205, 61);
            btnFiltrarStockBajo.Size = new Size(155, 30);
            btnFiltrarStockBajo.Text = "Consultar stock bajo";
            btnFiltrarStockBajo.Click += btnFiltrarStockBajo_Click;

            // Ver todo
            btnVerTodo.Location = new Point(370, 61);
            btnVerTodo.Size = new Size(155, 30);
            btnVerTodo.Text = "Ver todo el inventario";
            btnVerTodo.Click += btnVerTodo_Click;

            // Reporte por categorías
            btnReporteCategorias.Location = new Point(535, 61);
            btnReporteCategorias.Size = new Size(210, 30);
            btnReporteCategorias.Text = "Reporte por categorías";
            btnReporteCategorias.Click += btnReporteCategorias_Click;

            // Tabla
            dgvReportes.Location = new Point(20, 110);
            dgvReportes.Size = new Size(725, 280);
            dgvReportes.ReadOnly = true;
            dgvReportes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReportes.MultiSelect = false;
            dgvReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReportes.AllowUserToAddRows = false;
            dgvReportes.RowHeadersVisible = false;

            // Formulario
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;

            ClientSize = new Size(765, 415);

            Controls.AddRange(new Control[]
            {
                label1,
                cmbProductos,
                label2,
                nudCantidad,
                btnRegistrarVenta,
                btnAgregarStock,
                label3,
                nudStockMinimo,
                btnFiltrarStockBajo,
                btnVerTodo,
                btnReporteCategorias,
                dgvReportes
            });

            Name = "FormReportes";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Movimientos y reportes de inventario";

            Load += FormReportes_Load;

            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStockMinimo).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvReportes).EndInit();

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
