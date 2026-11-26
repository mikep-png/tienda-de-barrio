
using System;
using System.Windows.Forms;
using Datos;

namespace Presentacion;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        try
        {
            InicializadorBaseDatos.Inicializar();

            Application.Run(new FormPrincipal());
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "No fue posible iniciar la aplicación.\n\n" + ex.Message,
                "Error de inicio",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
