using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System;
using LaPachangaDelMundialV2.Models;
using LaPachangaDelMundialV2.Utils;

namespace LaPachangaDelMundialV2.Controllers
{
    /// <summary>
    /// controlador de quinielas
    /// </summary>
    public class QuinielaController
    {
        private readonly List<Quiniela> _quinielas;

        /// <summary>
        /// inicializa el controlador y carga las quinielas
        /// </summary>
        public QuinielaController()
        {
            _quinielas = JsonLoader.CargarQuinielas();
        }

        /// <summary>
        /// retorna las todas las quinielas
        /// </summary>
        /// <returns></returns>
        public List<Quiniela> ObtenerTodas()
        {
            return _quinielas;
        }

        /// <summary>
        /// retorna las quinielas de un usuario en particular
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public List<Quiniela> ObtenerPorUsuario(string idUsuario)
        {
            List<Quiniela> lista = new List<Quiniela>();

            foreach (Quiniela quiniela in _quinielas)
            {
                if (quiniela.IdsIntegrantes.Contains(idUsuario))
                {
                    lista.Add(quiniela);
                }
            }

            return lista;
        }

        /// <summary>
        /// devuelve quienielas publicas
        /// </summary>
        /// <returns></returns>
        public List<Quiniela> ObtenerPublicas()
        {
            List<Quiniela> lista = new List<Quiniela>();

            foreach (Quiniela quiniela in _quinielas)
            {
                if (quiniela.Tipo == TipoQuiniela.Publica)
                {
                    lista.Add(quiniela);
                }
            }
            return lista;
        }

        /// <summary>
        /// crea nueva quinuela
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="tipo"></param>
        /// <param name="idCreador"></param>
        /// <returns></returns>
        public bool Crear(string nombre, TipoQuiniela tipo, string idCreador)
        {
            foreach (Quiniela quiniela in _quinielas)
            {
                if (quiniela.Nombre == nombre)
                {
                    return false;
                }
            }

            Quiniela nueva = new Quiniela
            {
                Id = $"Q{_quinielas.Count + 1}",
                Nombre = nombre,
                Tipo = tipo,
                IdCreador = idCreador,
                IdsIntegrantes = new List<string> { idCreador },
                Notificaciones = new List<string>
                {
                    $"Quiniela creada por {idCreador}"
                }
            };

            _quinielas.Add(nueva);
            Guardar();
            return true;
        }

        /// <summary>
        /// agrega usuario a una quiniela que ya exista
        /// </summary>
        /// <param name="idQuiniela"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public bool UnirseAQuiniela(string idQuiniela, string idUsuario)
        {
            Quiniela quiniela = null;

            foreach (Quiniela q in _quinielas)
            {
                if (q.Id == idQuiniela)
                {
                    quiniela = q;
                    break;
                }
            }

            if (quiniela == null)
            {
                return false;
            }

            if (quiniela.IdsIntegrantes.Contains(idUsuario))
                return false;

            quiniela.IdsIntegrantes.Add(idUsuario);
            quiniela.Notificaciones.Add($"{idUsuario} se unió a la quiniela");
            Guardar();
            return true;
        }

        /// <summary>
        /// agrega notificacion al timeline de la quiniela
        /// </summary>
        /// <param name="idQuiniela"></param>
        /// <param name="mensaje"></param>
        public void AgregarNotificacion(string idQuiniela, string mensaje)
        {
            Quiniela quiniela = null;

            foreach (Quiniela q in _quinielas)
            {
                if (q.Id == idQuiniela)
                {
                    quiniela = q;
                    break;
                }
            }

            if (quiniela == null)
            {
                return;
            }

            quiniela.Notificaciones.Add(mensaje);
            Guardar();
        }

        private void Guardar()
        {
            string ruta = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "Datos", "quinielas.json");

            string contenido = JsonConvert.SerializeObject(
                _quinielas, Formatting.Indented);

            File.WriteAllText(ruta, contenido);
        }
    }
}