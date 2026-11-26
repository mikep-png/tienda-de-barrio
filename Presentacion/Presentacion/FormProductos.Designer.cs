
namespace Presentacion
{
    partial class FormProductos
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtNombre;
        private TextBox txtPrecio;
        private TextBox txtStock;
        private ComboBox cmbCategoria;
        private TextBox txtBuscar;

        private Button btnRegistrar;
        private Button btnModificar;
        private Button btnEliminar;
        private Button btnLimpiar;
        private Button btnNuevaCategoria;

        private DataGridView dgvProductos;

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;

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
            txtNombre = new TextBox();
            txtPrecio = new TextBox();
            txtStock = new TextBox();
            cmbCategoria = new ComboBox();
            txtBuscar = new TextBox();

            btnRegistrar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            btnLimpiar = new Button();
            btnNuevaCategoria = new Button();

            dgvProductos = new DataGridView();

            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();

            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();

            SuspendLayout();

            // Etiquetas
            label1.AutoSize = true;
            label1.Location = new Point(20, 22);
            label1.Text = "Nombre:";

            label2.AutoSize = true;
            label2.Location = new Point(20, 62);
            label2.Text = "Precio:";

            label3.AutoSize = true;
            label3.Location = new Point(20, 102);
            label3.Text = "Stock:";

            label4.AutoSize = true;
            label4.Location = new Point(20, 142);
            label4.Text = "Categoría:";

            label5.AutoSize = true;
            label5.Location = new Point(330, 20);
            label5.Text = "Buscar producto:";

            // Entradas
            txtNombre.Location = new Point(100, 18);
            txtNombre.Size = new Size(190, 23);

            txtPrecio.Location = new Point(100, 58);
            txtPrecio.Size = new Size(190, 23);

            txtStock.Location = new Point(100, 98);
            txtStock.Size = new Size(190, 23);

            cmbCategoria.Location = new Point(100, 138);
            cmbCategoria.Size = new Size(145, 23);
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;

            txtBuscar.Location = new Point(440, 17);
            txtBuscar.Size = new Size(300, 23);
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            // Nueva categoría
            btnNuevaCategoria.Location = new Point(250, 137);
            btnNuevaCategoria.Size = new Size(40, 25);
            btnNuevaCategoria.Text = "+";
            btnNuevaCategoria.Click += btnNuevaCategoria_Click;

            // Botones
            btnRegistrar.Location = new Point(20, 195);
            btnRegistrar.Size = new Size(125, 35);
            btnRegistrar.Text = "Registrar";
            btnRegistrar.Click += btnRegistrar_Click;

            btnModificar.Location = new Point(155, 195);
            btnModificar.Size = new Size(125, 35);
            btnModificar.Text = "Modificar";
            btnModificar.Click += btnModificar_Click;

            btnEliminar.Location = new Point(20, 240);
            btnEliminar.Size = new Size(125, 35);
            btnEliminar.Text = "Eliminar";
            btnEliminar.Click += btnEliminar_Click;

            btnLimpiar.Location = new Point(155, 240);
            btnLimpiar.Size = new Size(125, 35);
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.Click += btnLimpiar_Click;

            // Tabla
            dgvProductos.Location = new Point(330, 55);
            dgvProductos.Size = new Size(410, 270);
            dgvProductos.ReadOnly = true;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
            dgvProductos.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.CellClick += dgvProductos_CellClick;

            // Formulario
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;

            ClientSize = new Size(770, 350);

            Controls.AddRange(new Control[]
            {
                label1, label2, label3, label4, label5,
                txtNombre, txtPrecio, txtStock,
                cmbCategoria, txtBuscar,
                btnRegistrar, btnModificar,
                btnEliminar, btnLimpiar,
                btnNuevaCategoria, dgvProductos
            });

            Name = "FormProductos";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Gestión de productos";

            Load += FormProductos_Load;

            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
