
namespace Presentacion
{
    partial class FormPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        private Button btnProductos;
        private Button btnReportes;
        private Label lblTitulo;
        private Label lblSubtitulo;

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
            btnProductos = new Button();
            btnReportes = new Button();
            lblTitulo = new Label();
            lblSubtitulo = new Label();

            SuspendLayout();

            // Título
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblTitulo.Location = new Point(65, 35);
            lblTitulo.Text = "TIENDA DE BARRIO";

            // Subtítulo
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI", 10F);
            lblSubtitulo.Location = new Point(95, 80);
            lblSubtitulo.Text = "Sistema de gestión de inventario";

            // Botón productos
            btnProductos.Font = new Font("Segoe UI", 11F);
            btnProductos.Location = new Point(85, 130);
            btnProductos.Size = new Size(250, 50);
            btnProductos.Text = "Administrar productos";
            btnProductos.UseVisualStyleBackColor = true;
            btnProductos.Click += btnProductos_Click;

            // Botón reportes
            btnReportes.Font = new Font("Segoe UI", 11F);
            btnReportes.Location = new Point(85, 200);
            btnReportes.Size = new Size(250, 50);
            btnReportes.Text = "Movimientos y reportes";
            btnReportes.UseVisualStyleBackColor = true;
            btnReportes.Click += btnReportes_Click;

            // Formulario
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 300);
            Controls.Add(lblTitulo);
            Controls.Add(lblSubtitulo);
            Controls.Add(btnProductos);
            Controls.Add(btnReportes);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tienda de Barrio";

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
