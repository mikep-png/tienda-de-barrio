
using System;
using System.Globalization;
using System.Windows.Forms;
using Datos;
using Modelos;
using Servicios;
using Microsoft.VisualBasic;

namespace Presentacion
{
    public partial class FormProductos : Form
    {
        private readonly ServicioInventario _servicio;
        private readonly RepositorioCategorias _repoCategorias;

        private int _idSeleccionado = 0;

        public FormProductos()
        {
            InitializeComponent();

            _repoCategorias = new RepositorioCategorias();

            _servicio = new ServicioInventario(
                new RepositorioProductos(),
                _repoCategorias
            );
        }

        private void FormProductos_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            CargarProductos();
        }

        private void CargarCategorias()
        {
            var categorias = _repoCategorias.Listar();

            if (categorias.Count == 0)
            {
                _repoCategorias.Registrar(
                    new Categoria
                    {
                        Nombre = "General",
                        Descripcion = "Categoría general"
                    }
                );

                categorias = _repoCategorias.Listar();
            }

            cmbCategoria.DataSource = null;
            cmbCategoria.DataSource = categorias;
            cmbCategoria.DisplayMember = "Nombre";
            cmbCategoria.ValueMember = "Id";

            if (cmbCategoria.Items.Count > 0)
            {
                cmbCategoria.SelectedIndex = 0;
            }
        }

        private void CargarProductos()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = _servicio.ConsultarInventario();

            if (dgvProductos.Columns["Id"] != null)
            {
                dgvProductos.Columns["Id"].HeaderText = "Código";
            }

            if (dgvProductos.Columns["Nombre"] != null)
            {
                dgvProductos.Columns["Nombre"].HeaderText = "Producto";
            }

            if (dgvProductos.Columns["Precio"] != null)
            {
                dgvProductos.Columns["Precio"].HeaderText = "Precio";
                dgvProductos.Columns["Precio"].DefaultCellStyle.Format = "C2";
            }

            if (dgvProductos.Columns["Stock"] != null)
            {
                dgvProductos.Columns["Stock"].HeaderText = "Existencias";
            }
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show(
                    "Ingrese el nombre del producto.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtNombre.Focus();
                return false;
            }

            if (!decimal.TryParse(
                txtPrecio.Text,
                NumberStyles.Number,
                CultureInfo.CurrentCulture,
                out decimal precio))
            {
                MessageBox.Show(
                    "Ingrese un precio numérico válido.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtPrecio.Focus();
                return false;
            }

            if (precio <= 0)
            {
                MessageBox.Show(
                    "El precio debe ser mayor que cero.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                MessageBox.Show(
                    "El stock debe ser un número entero igual o mayor que cero.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtStock.Focus();
                return false;
            }

            if (cmbCategoria.SelectedValue == null)
            {
                MessageBox.Show(
                    "Seleccione una categoría.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            return true;
        }

        private Producto ObtenerProducto()
        {
            return new Producto
            {
                Id = _idSeleccionado,
                Nombre = txtNombre.Text.Trim(),
                Precio = decimal.Parse(
                    txtPrecio.Text,
                    CultureInfo.CurrentCulture
                ),
                Stock = int.Parse(txtStock.Text),
                IdCategoria = Convert.ToInt32(cmbCategoria.SelectedValue)
            };
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarFormulario())
                    return;

                Producto producto = ObtenerProducto();

                _servicio.RegistrarProducto(producto);

                MessageBox.Show(
                    "Producto registrado correctamente.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Limpiar();
                CargarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error al registrar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idSeleccionado == 0)
                {
                    MessageBox.Show(
                        "Seleccione un producto de la tabla.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                if (!ValidarFormulario())
                    return;

                Producto producto = ObtenerProducto();

                _servicio.ModificarProducto(producto);

                MessageBox.Show(
                    "Producto modificado correctamente.",
                    "Modificación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Limpiar();
                CargarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error al modificar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idSeleccionado == 0)
                {
                    MessageBox.Show(
                        "Seleccione un producto de la tabla.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro de eliminar este producto?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (respuesta == DialogResult.Yes)
                {
                    _servicio.EliminarProducto(_idSeleccionado);

                    MessageBox.Show(
                        "Producto eliminado correctamente.",
                        "Eliminación exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    Limpiar();
                    CargarProductos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error al eliminar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void dgvProductos_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvProductos.Rows[e.RowIndex].DataBoundItem is not Producto producto)
                return;

            _idSeleccionado = producto.Id;

            txtNombre.Text = producto.Nombre;
            txtPrecio.Text = producto.Precio.ToString(
                CultureInfo.CurrentCulture
            );
            txtStock.Text = producto.Stock.ToString();

            cmbCategoria.SelectedValue = producto.IdCategoria;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvProductos.DataSource = null;

            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                CargarProductos();
            }
            else
            {
                dgvProductos.DataSource =
                    _servicio.BuscarProductosPorNombre(txtBuscar.Text.Trim());
            }
        }

        private void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            string nombreCategoria = Interaction.InputBox(
                "Ingrese el nombre de la nueva categoría:",
                "Nueva categoría",
                ""
            );

            if (string.IsNullOrWhiteSpace(nombreCategoria))
                return;

            try
            {
                _repoCategorias.Registrar(
                    new Categoria
                    {
                        Nombre = nombreCategoria.Trim(),
                        Descripcion = "Categoría registrada desde la aplicación"
                    }
                );

                CargarCategorias();

                MessageBox.Show(
                    "Categoría registrada correctamente.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error al registrar categoría",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            _idSeleccionado = 0;

            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Clear();

            if (cmbCategoria.Items.Count > 0)
            {
                cmbCategoria.SelectedIndex = 0;
            }

            txtNombre.Focus();
        }
    }
}
