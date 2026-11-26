
using System;
using System.Windows.Forms;
using Datos;
using Servicios;

namespace Presentacion
{
    public partial class FormReportes : Form
    {
        private readonly ServicioInventario _servicio;

        public FormReportes()
        {
            InitializeComponent();

            _servicio = new ServicioInventario(
                new RepositorioProductos(),
                new RepositorioCategorias()
            );
        }

        private void FormReportes_Load(object sender, EventArgs e)
        {
            ActualizarDatos();
        }

        private void ActualizarDatos()
        {
            var productos = _servicio.ConsultarInventario();

            cmbProductos.DataSource = null;
            cmbProductos.DataSource = productos;
            cmbProductos.DisplayMember = "Nombre";
            cmbProductos.ValueMember = "Id";

            dgvReportes.DataSource = null;
            dgvReportes.DataSource = productos;

            if (dgvReportes.Columns["Id"] != null)
            {
                dgvReportes.Columns["Id"].HeaderText = "Código";
            }

            if (dgvReportes.Columns["Nombre"] != null)
            {
                dgvReportes.Columns["Nombre"].HeaderText = "Producto";
            }

            if (dgvReportes.Columns["Precio"] != null)
            {
                dgvReportes.Columns["Precio"].HeaderText = "Precio";
                dgvReportes.Columns["Precio"].DefaultCellStyle.Format = "C2";
            }

            if (dgvReportes.Columns["Stock"] != null)
            {
                dgvReportes.Columns["Stock"].HeaderText = "Existencias";
            }
        }

        private bool ValidarMovimiento()
        {
            if (cmbProductos.SelectedValue == null)
            {
                MessageBox.Show(
                    "Seleccione un producto.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            if (nudCantidad.Value <= 0)
            {
                MessageBox.Show(
                    "La cantidad debe ser mayor que cero.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            return true;
        }

        private void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarMovimiento())
                    return;

                int idProducto = Convert.ToInt32(cmbProductos.SelectedValue);
                int cantidad = Convert.ToInt32(nudCantidad.Value);

                _servicio.RegistrarVenta(idProducto, cantidad);

                MessageBox.Show(
                    "Venta registrada correctamente. El inventario fue actualizado.",
                    "Venta exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                ActualizarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error al registrar venta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnAgregarStock_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarMovimiento())
                    return;

                int idProducto = Convert.ToInt32(cmbProductos.SelectedValue);
                int cantidad = Convert.ToInt32(nudCantidad.Value);

                _servicio.AgregarExistencias(idProducto, cantidad);

                MessageBox.Show(
                    "Existencias agregadas correctamente.",
                    "Inventario actualizado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                ActualizarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error al agregar existencias",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnFiltrarStockBajo_Click(object sender, EventArgs e)
        {
            try
            {
                int stockMinimo = Convert.ToInt32(nudStockMinimo.Value);

                var productos =
                    _servicio.ConsultarProductosStockBajo(stockMinimo);

                dgvReportes.DataSource = null;
                dgvReportes.DataSource = productos;

                if (productos.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontraron productos con stock bajo.",
                        "Consulta finalizada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error en consulta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnVerTodo_Click(object sender, EventArgs e)
        {
            ActualizarDatos();
        }
    }
}
