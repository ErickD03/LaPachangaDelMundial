using System;
using System.Collections.Generic;
using System.Linq;
using LaPachangaDelMundial.Models;
using LaPachangaDelMundial.Utils;

namespace LaPachangaDelMundial.Controllers
{
    public class PartidoController
    {
        private List<Partido> _partidos;
        private List<Seleccion> _selecciones;

        public PartidoController()
        {
            _partidos = JsonLoader.CargarPartidos();
            _selecciones = JsonLoader.CargarSelecciones();
        }

        // devuelve todos los partidos
        public List<Partido> ObtenerTodos()
        {
            return _partidos;
        }

        // devuelve el name de una seleccion por codigo
        public string ObtenerNombreSeleccion(string codigo)
        {
            Seleccion sel = _selecciones.FirstOrDefault(s => s.Codigo == codigo);
            return sel != null ? sel.Nombre : codigo;
        }

        // actualiza estado de partidos según fecha que se simule
        public void ActualizarEstados()
        {
            DateTime fechaActual = SistemaFecha.FechaActual;

            foreach (Partido p in _partidos)
            {
                if (p.Estado == EstadoPartido.Finalizado)
                    continue;

                if (p.FechaHora <= fechaActual)
                    p.Estado = EstadoPartido.EnCurso;
                else
                    p.Estado = EstadoPartido.Pendiente;
            }
        }

        // devuelve 5 ultimos patidos terminados
        public List<Partido> ObtenerUltimos5()
        {
            return _partidos
                .Where(p => p.Estado == EstadoPartido.Finalizado)
                .OrderByDescending(p => p.FechaHora)
                .Take(5)
                .ToList();
        }

        // devuele partidos en 24h proximas
        public List<Partido> ObtenerProximos24Horas()
        {
            DateTime fechaActual = SistemaFecha.FechaActual;
            DateTime limite = fechaActual.AddHours(24);

            return _partidos
                .Where(p => p.Estado == EstadoPartido.Pendiente &&
                            p.FechaHora >= fechaActual &&
                            p.FechaHora <= limite)
                .OrderBy(p => p.FechaHora)
                .ToList();
        }

        // devuelve partidos de grpo especifico
        public List<Partido> ObtenerPorGrupo(string grupo)
        {
            return _partidos
                .Where(p => p.Grupo == grupo)
                .OrderBy(p => p.FechaHora)
                .ToList();
        }

        // devuelve partidos fase especifica 
        public List<Partido> ObtenerPorFase(string fase)
        {
            return _partidos
                .Where(p => p.Fase == fase)
                .OrderBy(p => p.FechaHora)
                .ToList();
        }

        // calcula tabla posociones en un grupo
        public List<PosicionGrupo> CalcularTablaGrupo(string grupo)
        {
            List<Partido> partidos = ObtenerPorGrupo(grupo)
                .Where(p => p.Estado == EstadoPartido.Finalizado)
                .ToList();

            List<Seleccion> selecciones = _selecciones
                .Where(s => s.Grupo == grupo)
                .ToList();

            List<PosicionGrupo> tabla = selecciones.Select(s => new PosicionGrupo
            {
                Codigo = s.Codigo,
                Nombre = s.Nombre
            }).ToList();

            foreach (Partido p in partidos)
            {
                PosicionGrupo local = tabla.FirstOrDefault(t => t.Codigo == p.CodigoLocal);
                PosicionGrupo visitante = tabla.FirstOrDefault(t => t.Codigo == p.CodigoVisitante);

                if (local == null || visitante == null) continue;

                local.GolesFavor += p.GolesLocal;
                local.GolesContra += p.GolesVisitante;
                visitante.GolesFavor += p.GolesVisitante;
                visitante.GolesContra += p.GolesLocal;
                local.PartidosJugados++;
                visitante.PartidosJugados++;

                if (p.GolesLocal > p.GolesVisitante)
                {
                    local.Puntos += 3;
                    local.Ganados++;
                    visitante.Perdidos++;
                }
                else if (p.GolesLocal < p.GolesVisitante)
                {
                    visitante.Puntos += 3;
                    visitante.Ganados++;
                    local.Perdidos++;
                }
                else
                {
                    local.Puntos++;
                    visitante.Puntos++;
                    local.Empatados++;
                    visitante.Empatados++;
                }
            }

            return tabla.OrderByDescending(t => t.Puntos)
                        .ThenByDescending(t => t.DiferenciaGoles)
                        .ThenByDescending(t => t.GolesFavor)
                        .ToList();
        }
    }

    // clase apoyo para tablas posiciones
    public class PosicionGrupo
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int PartidosJugados { get; set; }
        public int Ganados { get; set; }
        public int Empatados { get; set; }
        public int Perdidos { get; set; }
        public int GolesFavor { get; set; }
        public int GolesContra { get; set; }
        public int Puntos { get; set; }
        public int DiferenciaGoles => GolesFavor - GolesContra;
    }
}