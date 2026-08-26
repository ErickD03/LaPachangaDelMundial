using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using LaPachangaDelMundialV2.Models;

namespace LaPachangaDelMundialV2.Utils
{
    /// <summary>
    /// clase encargada de cargar datos desde archivos JSON
    /// </summary>
    public static class JsonLoader
    {
        private static readonly string RutaBase =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Datos");
        /// <summary>
        /// carga las selecciones
        /// </summary>
        /// <returns></returns>
        public static List<Seleccion> CargarSelecciones()
        {
            return CargarArchivo<List<Seleccion>>("selecciones.json");
        }
        /// <summary>
        /// carga los partidos
        /// </summary>
        /// <returns></returns>
        public static List<Partido> CargarPartidos()
        {
            return CargarArchivo<List<Partido>>("partidos.json");
        }
        /// <summary>
        /// carga los usuarios
        /// </summary>
        /// <returns></returns>
        public static List<Usuario> CargarUsuarios()
        {
            return CargarArchivo<List<Usuario>>("usuarios.json");
        }
        /// <summary>
        /// carga las quinielas
        /// </summary>
        /// <returns></returns>
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
                    Console.WriteLine($"Archivo no encontrado: {rutaCompleta}");
                    return default;
                }

                string contenido = File.ReadAllText(rutaCompleta);
                return JsonConvert.DeserializeObject<T>(contenido);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en {nombreArchivo}: {ex.Message}");
                return default;
            }
        }
        /// <summary>
        /// carga los pronósticos
        /// </summary>
        /// <returns></returns>
        public static List<Pronostico> CargarPronosticos()
        {
            return CargarArchivo<List<Pronostico>>("pronosticos.json");
        }
    }
}


