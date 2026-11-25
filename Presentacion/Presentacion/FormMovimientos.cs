
using System;
using System.Windows.Forms;
using Servicios;
using Modelos;

namespace Presentacion
{
    public partial class FormMovimientos : Form
    {
        private readonly ServicioInventario _servicio;

        public FormMovimientos()
            : this(new ServicioInventario())
        {
        }

        public FormMovimientos(ServicioInventario servicio)
        {
            InitializeComponent();

            _servicio = servicio ??
                throw new ArgumentNullException(nameof(servicio));

            CargarProductos();
        }

        private void CargarProductos()
        {
            try
            {
                cmbProductos.DataSource = null;
                cmbProductos.DataSource =
                    _servicio.ConsultarInventario();

                cmbProductos.DisplayMember = "Nombre";
                cmbProductos.ValueMember = "Id";

                ActualizarStock();
            }
            catch (Exception ex)
            {
                MostrarResultado(
                    "Error al cargar productos: " + ex.Message, true);
            }
        }

        private Producto ObtenerProductoSeleccionado()
        {
            if (cmbProductos.SelectedItem is not Producto producto)
            {
                throw new InvalidOperationException(
                    "Debe seleccionar un producto.");
            }

            return producto;
        }

        private void ActualizarStock()
        {
            if (cmbProductos.SelectedItem is Producto producto)
            {
                lblStock.Text = "Stock disponible: " + producto.Stock;
            }
            else
            {
                lblStock.Text = "Stock disponible: -";
            }
        }

        private void CmbProductos_SelectedIndexChanged(
            object? sender, EventArgs e)
        {
            ActualizarStock();
        }

        private void BtnEntrada_Click(object? sender, EventArgs e)
        {
            try
            {
                Producto producto = ObtenerProductoSeleccionado();

                _servicio.AgregarExistencias(
                    producto.Id, (int)nudCantidad.Value);

                MostrarResultado(
                    "Entrada registrada correctamente.", false);

                CargarProductos();
            }
            catch (Exception ex)
            {
                MostrarResultado(ex.Message, true);
            }
        }

        private void BtnVenta_Click(object? sender, EventArgs e)
        {
            try
            {
                Producto producto = ObtenerProductoSeleccionado();

                _servicio.RegistrarVenta(
                    producto.Id, (int)nudCantidad.Value);

                MostrarResultado(
                    "Venta registrada correctamente.", false);

                CargarProductos();
            }
            catch (Exception ex)
            {
                MostrarResultado(ex.Message, true);
            }
        }

        private void MostrarResultado(string mensaje, bool error)
        {
            lblResultado.Text = mensaje;

            MessageBox.Show(
                mensaje,
                error ? "Validación" : "Movimiento registrado",
                MessageBoxButtons.OK,
                error ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        }
    }
}
