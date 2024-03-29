using System.IO;
using System.Threading;
using System.Windows;

namespace MantenimientoDll_v2_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string rutaDirectorio;
        private string nombreArchivo;
        private readonly Mantenimiento mantenimiento = new Mantenimiento();
        private readonly LecturaRuta LecturaRuta = new LecturaRuta();
        public MainWindow()
        {
            InitializeComponent();

            Ruta.Text = LecturaRuta.GetRuta();

            // Obtener la ruta del directorio del ejecutable
            string directorioEjecutable = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            // Almacenar la ruta en Properties.Settings
            Properties.Settings.Default.DirectorioEjecutable = directorioEjecutable;
            Properties.Settings.Default.Save();

            string directorioGuardado = Properties.Settings.Default.DirectorioEjecutable + @"\automatico.txt";

            if (File.Exists(directorioGuardado))
            {
                Automatico();

                // Espera durante 3 segundos antes de cerrar la aplicación (puedes ajustar este valor)
                Thread.Sleep(3000);

                Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Verificar())
            {
                mantenimiento.Mantener(rutaDirectorio, nombreArchivo);

                // Imprime en pantalla la direccion de todas las carpetas que contenian el archivo borrado.
                _ = MessageBox.Show(mantenimiento.Info());
            }
        }

        private void Automatico()
        {
            if (Verificar())
            {
                mantenimiento.Mantener(rutaDirectorio, nombreArchivo);
            }
        }

        private bool Verificar()
        {
            // Accede al contenido del TextBox
            rutaDirectorio = Ruta.Text;

            // Accede al contenido del TextBox
            nombreArchivo = Archivo.Text;
            if (rutaDirectorio != null && rutaDirectorio != "" && nombreArchivo != null && nombreArchivo != "")
            {
                return true;
            }
            else if (rutaDirectorio == null || rutaDirectorio == "")
            {
                _ = MessageBox.Show($"Falta ingresar la ruta de ubicacion");
            }
            else if (nombreArchivo != null || nombreArchivo != "")
            {
                _ = MessageBox.Show($"Falta ingresar el nombre del arcivo");
            }
            return false;
        }
    }
}
