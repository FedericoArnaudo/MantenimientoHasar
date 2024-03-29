using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace MantenimientoDll_v2_wpf
{
    class Mantenimiento
    {
        private readonly List<string> borrados;

        public Mantenimiento()
        {
            borrados = new List<string>();
        }


        /*
         * El metodo "Mantener", recorre todas las carpetas que hay en la ruta, luego las almacena en una lista llamada carpetas.
         * Una vez que este proceso fue realizado correctamente, recorre la lista, borrando el archivo especificado
         */

        public void Mantener(string rutaDirectorio, string nombreArchivo)
        {
            try
            {
                // Verifica si el directorio existe
                if (Directory.Exists(rutaDirectorio))
                {
                    // Obtén la lista de carpetas en el directorio
                    List<string> carpetas = ObtenerCarpetas(rutaDirectorio);

                    // Borra el Dll
                    foreach (string carpeta in carpetas)
                    {
                        BorrarDll(carpeta, nombreArchivo);
                    }

                }
                else
                {
                    _ = MessageBox.Show($"El directorio {rutaDirectorio} no existe.");
                }
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show($"Error al obtener las carpetas: {ex.Message}");
            }
        }

        private List<string> ObtenerCarpetas(string rutaDirectorio)
        {
            // Utiliza la clase Directory para obtener la lista de carpetas
            string[] carpetasArray = Directory.GetDirectories(rutaDirectorio);

            // Convierte el array en una lista
            List<string> carpetas = new List<string>(carpetasArray);

            return carpetas;
        }

        private void BorrarDll(string carpeta, string nombreArchivo)
        {
            // Obtener todos los archivos en el directorio
            string[] archivos = Directory.GetFiles(carpeta);

            foreach(string archivo in archivos)
            {
                // Verifica si el archivo existe
                if (Path.GetFileName(archivo).StartsWith(nombreArchivo.Substring(0, 8).ToLower()) || Path.GetFileName(archivo).Equals(nombreArchivo))
                {
                    borrados.Add(carpeta);

                    // Borra el archivo
                    File.Delete(archivo);
                }
            }
        }

        public string Info()
        {
            string info = "Archivos borrados: \n";
            foreach (string borrado in borrados)
            {
                info += borrado + "\n";
            }
            borrados.Clear();
            return info;
        }
    }
}
