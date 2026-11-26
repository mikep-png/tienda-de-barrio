
using System;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            using (FormProductos formulario = new FormProductos())
            {
                formulario.ShowDialog(this);
            }
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            using (FormReportes formulario = new FormReportes())
            {
                formulario.ShowDialog(this);
            }
        }
    }
}
