using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MantenimientoDll_v2_wpf
{
    class LecturaRuta
    {
        private readonly string directorioBase;
        private readonly string nombreArchivo;
        private readonly string rutaCompleta;
        private string contenido;
        public LecturaRuta()
        {
            // Obtener la ruta del directorio de la aplicación
            directorioBase = AppDomain.CurrentDomain.BaseDirectory;

            // Nombre del archivo que deseas leer
            nombreArchivo = "Ruta.txt";

            rutaCompleta = Path.Combine(directorioBase, nombreArchivo);

            SetRuta();
        }

        public string GetRuta()
        {
            return contenido;
        }

        public void SetRuta()
        {
            // Verificar si el archivo existe
            if (File.Exists(rutaCompleta))
            {
                try
                {
                    // Leer el contenido del archivo
                    contenido = File.ReadAllText(rutaCompleta);

                }
                catch (Exception ex)
                {
                    _ = MessageBox.Show("Error al leer el archivo: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                _ = MessageBox.Show("El archivo no existe en la ruta especificada.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
