using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System;
using LaPachangaDelMundial.Models;
using LaPachangaDelMundial.Utils;

namespace LaPachangaDelMundial.Controllers
{
    public class QuinielaController
    {
        private List<Quiniela> _quinielas;

        public QuinielaController()
        {
            _quinielas = JsonLoader.CargarQuinielas();
        }

        // retorna las todas las quinielas
        public List<Quiniela> ObtenerTodas()
        {
            return _quinielas;
        }

        // retorna las quinielas de un usuario en particular
        public List<Quiniela> ObtenerPorUsuario(string idUsuario)
        {
            return _quinielas
                .Where(q => q.IdsIntegrantes.Contains(idUsuario))
                .ToList();
        }

        // devuelve quienielas publicas
        public List<Quiniela> ObtenerPublicas()
        {
            return _quinielas
                .Where(q => q.Tipo == TipoQuiniela.Publica)
                .ToList();
        }

        // crea nueva quinuela
        public bool Crear(string nombre, TipoQuiniela tipo, string idCreador)
        {
            if (_quinielas.Any(q => q.Nombre == nombre))
                return false;

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

        // agrega usuario a una quiniela que ya exista
        public bool UnirseAQuiniela(string idQuiniela, string idUsuario)
        {
            Quiniela quiniela = _quinielas.FirstOrDefault(q => q.Id == idQuiniela);

            if (quiniela == null)
                return false;

            if (quiniela.IdsIntegrantes.Contains(idUsuario))
                return false;

            quiniela.IdsIntegrantes.Add(idUsuario);
            quiniela.Notificaciones.Add($"{idUsuario} se unió a la quiniela");
            Guardar();
            return true;
        }

        // agrega notificacion al timeline de la quiniela
        public void AgregarNotificacion(string idQuiniela, string mensaje)
        {
            Quiniela quiniela = _quinielas.FirstOrDefault(q => q.Id == idQuiniela);
            if (quiniela == null) return;

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