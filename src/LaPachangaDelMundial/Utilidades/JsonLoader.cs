using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using LaPachangaDelMundial.Models;

namespace LaPachangaDelMundial.Utils
{
    public static class JsonLoader
    {
        private static readonly string RutaBase =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Datos");

        public static List<Seleccion> CargarSelecciones()
        {
            return CargarArchivo<List<Seleccion>>("selecciones.json");
        }

        public static List<Partido> CargarPartidos()
        {
            return CargarArchivo<List<Partido>>("partidos.json");
        }

        public static List<Usuario> CargarUsuarios()
        {
            return CargarArchivo<List<Usuario>>("usuarios.json");
        }

        public static List<Quiniela> CargarQuinielas()
        {
            return CargarArchivo<List<Quiniela>>("quinielas.json");
        }

        private static T CargarArchivo<T>(string nombreArchivo)
        {
            try
            {
                string rutaCompleta = Path.Combine(RutaBase, nombreArchivo);

                if (!File.Exists(rutaCompleta))
                {
                    MessageBox.Show(
                        $"Archivo no encontrado:\n{rutaCompleta}",
                        "Error de carga");
                    return default;
                }

                string contenido = File.ReadAllText(rutaCompleta);
                return JsonConvert.DeserializeObject<T>(contenido);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error en {nombreArchivo}:\n{ex.Message}",
                    "Error de carga");
                return default;
            }
        }
    }
}


