
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
            try
            {
                ActualizarDatos();
            }
            catch (Exception ex)
            {
                MostrarError("No fue posible cargar los reportes.", ex);
            }
        }

        private void ActualizarDatos()
        {
            var productos = _servicio.ConsultarInventario();

            cmbProductos.DataSource = null;
            cmbProductos.DisplayMember = "Nombre";
            cmbProductos.ValueMember = "Id";
            cmbProductos.DataSource = productos;

            MostrarProductosEnTabla(productos);
        }

        private void MostrarProductosEnTabla(object datos)
        {
            dgvReportes.DataSource = null;
            dgvReportes.DataSource = datos;

            if (dgvReportes.Columns["Id"] != null)
                dgvReportes.Columns["Id"].HeaderText = "Código";

            if (dgvReportes.Columns["Nombre"] != null)
                dgvReportes.Columns["Nombre"].HeaderText = "Producto";

            if (dgvReportes.Columns["Precio"] != null)
            {
                dgvReportes.Columns["Precio"].HeaderText = "Precio";
                dgvReportes.Columns["Precio"].DefaultCellStyle.Format = "C2";
            }

            if (dgvReportes.Columns["Stock"] != null)
                dgvReportes.Columns["Stock"].HeaderText = "Existencias";

            if (dgvReportes.Columns["IdCategoria"] != null)
                dgvReportes.Columns["IdCategoria"].HeaderText = "Código categoría";

            if (dgvReportes.Columns["Categoria"] != null &&
                dgvReportes.Columns["Categoria"].ValueType != typeof(string))
            {
                dgvReportes.Columns["Categoria"].Visible = false;
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
                MostrarError("Error al registrar la venta.", ex);
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
                MostrarError("Error al agregar existencias.", ex);
            }
        }

        private void btnFiltrarStockBajo_Click(object sender, EventArgs e)
        {
            try
            {
                int stockMinimo = Convert.ToInt32(nudStockMinimo.Value);

                var productos = _servicio.ConsultarProductosStockBajo(stockMinimo);

                MostrarProductosEnTabla(productos);

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
                MostrarError("Error en la consulta de stock bajo.", ex);
            }
        }

        private void btnVerTodo_Click(object sender, EventArgs e)
        {
            try
            {
                ActualizarDatos();
            }
            catch (Exception ex)
            {
                MostrarError("No fue posible mostrar el inventario.", ex);
            }
        }

        private void btnReporteCategorias_Click(object sender, EventArgs e)
        {
            try
            {
                var reporte = _servicio.ConsultarInventarioPorCategoria();

                dgvReportes.DataSource = null;
                dgvReportes.DataSource = reporte;

                if (dgvReportes.Columns["Categoria"] != null)
                    dgvReportes.Columns["Categoria"].HeaderText = "Categoría";

                if (dgvReportes.Columns["CantidadProductos"] != null)
                    dgvReportes.Columns["CantidadProductos"].HeaderText = "Cantidad de productos";

                if (dgvReportes.Columns["UnidadesDisponibles"] != null)
                    dgvReportes.Columns["UnidadesDisponibles"].HeaderText = "Unidades disponibles";

                if (dgvReportes.Columns["ValorInventario"] != null)
                {
                    dgvReportes.Columns["ValorInventario"].HeaderText = "Valor del inventario";
                    dgvReportes.Columns["ValorInventario"].DefaultCellStyle.Format = "C2";
                }

                if (reporte.Count == 0)
                {
                    MessageBox.Show(
                        "No hay productos registrados para generar el reporte.",
                        "Reporte por categorías",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MostrarError("No fue posible generar el reporte por categorías.", ex);
            }
        }

        private void MostrarError(string mensaje, Exception ex)
        {
            MessageBox.Show(
                mensaje + "\n\nDetalle: " + ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }
    }
}
