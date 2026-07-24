using LaPachangaDelMundial.Models;
using LaPachangaDelMundial.Utils;
using Microsoft.VisualBasic.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LaPachangaDelMundial.Controllers
{
    public class PronosticoController
    {
        private List<Pronostico> _pronosticos;

        public PronosticoController()
        {
            _pronosticos = CargarPronosticos();
        }

        private List<Pronostico> CargarPronosticos()
        {
            string ruta = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "Datos", "pronosticos.json");

            if (!File.Exists(ruta))
                return new List<Pronostico>();

            string contenido = File.ReadAllText(ruta);
            return JsonConvert.DeserializeObject<List<Pronostico>>(contenido)
                   ?? new List<Pronostico>();
        }

        private void Guardar()
        {
            string ruta = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "Datos", "pronosticos.json");

            string contenido = JsonConvert.SerializeObject(
                _pronosticos, Formatting.Indented);

            File.WriteAllText(ruta, contenido);
        }

        // valida si algun usuario pronostico el partido
        public bool YaPronostico(string idUsuario, string idPartido)
        {
            return _pronosticos.Any(p =>
                p.IdUsuario == idUsuario &&
                p.IdPartido == idPartido);
        }

        // realiza un registro de un nuevo prnostico
        public bool Registrar(string idUsuario, string idPartido,
                              int golesLocal, int golesVisitante,
                              EstadoPartido estadoPartido)
        {
            if (estadoPartido != EstadoPartido.Pendiente)
                return false;

            if (YaPronostico(idUsuario, idPartido))
                return false;

            _pronosticos.Add(new Pronostico
            {
                IdUsuario = idUsuario,
                IdPartido = idPartido,
                GolesLocal = golesLocal,
                GolesVisitante = golesVisitante,
                PuntosObtenidos = 0
            });

            Guardar();
            return true;
        }

        // realiza el calculo de puntos para los pronosticos de un partido
        public void CalcularPuntos(Partido partido)
        {
            List<Pronostico> pronosticos = _pronosticos
                .Where(p => p.IdPartido == partido.Id)
                .ToList();

            foreach (Pronostico p in pronosticos)
            {
                if (p.GolesLocal == partido.GolesLocal &&
                    p.GolesVisitante == partido.GolesVisitante)
                {
                    p.PuntosObtenidos = 5; // si es marcador exacto
                }
                else
                {
                    bool acertoGanador =
                        (p.GolesLocal > p.GolesVisitante &&
                         partido.GolesLocal > partido.GolesVisitante) ||
                        (p.GolesLocal < p.GolesVisitante &&
                         partido.GolesLocal < partido.GolesVisitante) ||
                        (p.GolesLocal == p.GolesVisitante &&
                         partido.GolesLocal == partido.GolesVisitante);

                    p.PuntosObtenidos = acertoGanador ? 2 : 0;
                }
            }

            Guardar();
        }

        // Devuelve el historial de pronósticos de un usuario
        public List<Pronostico> ObtenerPorUsuario(string idUsuario)
        {
            return _pronosticos
                .Where(p => p.IdUsuario == idUsuario)
                .ToList();
        }

        // Devuelve el ranking global de usuarios
        public List<RankingItem> ObtenerRankingGlobal(List<Usuario> usuarios)
        {
            return usuarios
                .Select(u => new RankingItem
                {
                    IdUsuario = u.Id,
                    NombreUsuario = u.NombreUsuario,
                    Puntos = u.Puntos +
                        _pronosticos
                            .Where(p => p.IdUsuario == u.Id)
                            .Sum(p => p.PuntosObtenidos)
                })
                .OrderByDescending(r => r.Puntos)
                .ToList();
        }

        // devuelve ranking de quiniela especifia
        public List<RankingItem> ObtenerRankingQuiniela(
            Quiniela quiniela, List<Usuario> usuarios)
        {
            return usuarios
                .Where(u => quiniela.IdsIntegrantes.Contains(u.Id))
                .Select(u => new RankingItem
                {
                    IdUsuario = u.Id,
                    NombreUsuario = u.NombreUsuario,
                    Puntos = u.Puntos +
                        _pronosticos
                            .Where(p => p.IdUsuario == u.Id)
                            .Sum(p => p.PuntosObtenidos)
                })
                .OrderByDescending(r => r.Puntos)
                .ToList();
        }
    }

    public class RankingItem
    {
        public string IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public int Puntos { get; set; }
    }
}