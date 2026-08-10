using LaPachangaDelMundialV2.Models;
using LaPachangaDelMundialV2.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace LaPachangaDelMundialV2.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PronosticoController
    {
        private readonly List<Pronostico> _pronosticos;

        public PronosticoController()
        {
            _pronosticos = CargarPronosticos();
        }

        private static List<Pronostico> CargarPronosticos()
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

        /// <summary>
        /// valida si algun usuario pronostico el partido
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idPartido"></param>
        /// <returns></returns>
        public bool YaPronostico(string idUsuario, string idPartido)
        {
            foreach (Pronostico partido in _pronosticos)
            {
                if (partido.IdUsuario == idUsuario &&
                    partido.IdPartido == idPartido)
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// realiza un registro de un nuevo prnostico
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idPartido"></param>
        /// <param name="golesLocal"></param>
        /// <param name="golesVisitante"></param>
        /// <param name="estadoPartido"></param>
        /// <returns></returns>
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

        /// <summary>
        /// realiza el calculo de puntos para los pronosticos de un partido
        /// </summary>
        /// <param name="partido"></param>
        public void CalcularPuntos(Partido partido)
        {
            foreach (Pronostico pronostico in _pronosticos)
            {
                if (pronostico.IdPartido != partido.Id)
                    continue;

                if (pronostico.GolesLocal == partido.GolesLocal &&
                    pronostico.GolesVisitante == partido.GolesVisitante)
                {
                    pronostico.PuntosObtenidos = 5;
                }
                else
                {
                    bool acertoGanador = false;

                    if (pronostico.GolesLocal > pronostico.GolesVisitante &&
                        partido.GolesLocal > partido.GolesVisitante)
                        acertoGanador = true;

                    if (pronostico.GolesLocal < pronostico.GolesVisitante &&
                        partido.GolesLocal < partido.GolesVisitante)
                        acertoGanador = true;

                    if (pronostico.GolesLocal == pronostico.GolesVisitante &&
                        partido.GolesLocal == partido.GolesVisitante)
                        acertoGanador = true;

                    if (acertoGanador)
                        pronostico.PuntosObtenidos = 2;
                    else
                        pronostico.PuntosObtenidos = 0;
                }
            }

            Guardar();
        }


        /// <summary>
        /// devuelve pronósticos de un usuario o todos si idUsuario es null
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public List<Pronostico> ObtenerPorUsuario(string idUsuario)
        {
            if (idUsuario == null)
                return _pronosticos;

            List<Pronostico> lista = new List<Pronostico>();

            foreach (Pronostico pronostico in _pronosticos)
            {
                if (pronostico.IdUsuario == idUsuario)
                {
                    lista.Add(pronostico);
                }
            }
            return lista;
        }


        /// <summary>
        /// devuelve ranking global de usuarios
        /// </summary>
        /// <param name="usuarios"></param>
        /// <returns></returns>
        public List<RankingItem> ObtenerRankingGlobal(List<Usuario> usuarios)
        {
            List<RankingItem> ranking = new List<RankingItem>();

            foreach (Usuario usuario in usuarios)
            {
                int puntos = usuario.Puntos;

                foreach (Pronostico pronostico in _pronosticos)
                {
                    if (pronostico.IdUsuario == usuario.Id)
                    {
                        puntos += pronostico.PuntosObtenidos;
                    }
                }

                RankingItem item = new RankingItem();
                item.IdUsuario = usuario.Id;
                item.NombreUsuario = usuario.NombreUsuario;
                item.Puntos = puntos;

                ranking.Add(item);
            }

            ranking.Sort((a, b) => b.Puntos.CompareTo(a.Puntos));

            return ranking;
        }

        /// <summary>
        /// devuelve ranking de quiniela especifia
        /// </summary>
        /// <param name="quiniela"></param>
        /// <param name="usuarios"></param>
        /// <returns></returns>
        public List<RankingItem> ObtenerRankingQuiniela(Quiniela quiniela, List<Usuario> usuarios)
        {
            List<RankingItem> ranking = new List<RankingItem>();

            foreach (Usuario usuario in usuarios)
            {
                if (!quiniela.IdsIntegrantes.Contains(usuario.Id))
                    continue;

                int puntos = usuario.Puntos;

                foreach (Pronostico partido in _pronosticos)
                {
                    if (partido.IdUsuario == usuario.Id)
                    {
                        puntos += partido.PuntosObtenidos;
                    }
                }

                RankingItem item = new RankingItem();
                item.IdUsuario = usuario.Id;
                item.NombreUsuario = usuario.NombreUsuario;
                item.Puntos = puntos;

                ranking.Add(item);
            }

            ranking.Sort((a, b) => b.Puntos.CompareTo(a.Puntos));

            return ranking;
        }
    }

    public class RankingItem
    {
        public string IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public int Puntos { get; set; }
    }
}