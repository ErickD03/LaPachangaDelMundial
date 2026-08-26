using System;
using System.Collections.Generic;
using LaPachangaDelMundialV2.Models;
using LaPachangaDelMundialV2.Utils;

namespace LaPachangaDelMundialV2.Controllers
{
    /// <summary>
    /// controlador encargado de gestionar los partidos
    /// </summary>
    public class PartidoController
    {
        private List<Partido> _partidos;
        private readonly List<Seleccion> _selecciones;

        /// <summary>
        /// inicializa el controlador y carga los datos
        /// </summary>
        public PartidoController()
        {
            _partidos = JsonLoader.CargarPartidos();
            _selecciones = JsonLoader.CargarSelecciones();
            ActualizarEstados();
        }

        /// <summary>
        /// este devuelve todos los partidos
        /// </summary>
        /// <returns></returns>
        public List<Partido> ObtenerTodos()
        {
            return _partidos;
        }

        /// <summary>
        /// este devuelve el nombre completo de una selección dado su código
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public string ObtenerNombreSeleccion(string codigo)
        {
            foreach (Seleccion seleccion in _selecciones)
            {
                if (seleccion.Codigo == codigo)
                    return seleccion.Nombre;
            }
            return codigo;
        }

        /// <summary>
        /// realiza la actualización el estado de cada partido según la fecha simulada
        /// </summary>
        public void ActualizarEstados()
        {
            _partidos = JsonLoader.CargarPartidos();

            DateTime fechaActual = SistemaFecha.FechaActual;

            foreach (Partido p in _partidos)
            {
                if (p.FechaHora <= fechaActual)
                    p.Estado = EstadoPartido.Finalizado;
                else
                    p.Estado = EstadoPartido.Pendiente;
            }
        }

        /// <summary>
        /// devuelve los últimos 5 partidos finalizados
        /// </summary>
        /// <returns></returns>
        public List<Partido> ObtenerUltimos5()
        {
            List<Partido> finalizados = new List<Partido>();
            foreach (Partido partido in _partidos)
            {
                if (partido.Estado == EstadoPartido.Finalizado)
                    finalizados.Add(partido);
            }

            for (int i = 0; i < finalizados.Count - 1; i++)
            {
                for (int j = 0; j < finalizados.Count - i - 1; j++)
                {
                    if (finalizados[j].FechaHora < finalizados[j + 1].FechaHora)
                    {
                        Partido temp = finalizados[j];
                        finalizados[j] = finalizados[j + 1];
                        finalizados[j + 1] = temp;
                    }
                }
            }

            List<Partido> ultimos = new List<Partido>();
            for (int i = 0; i < finalizados.Count && i < 5; i++)
                ultimos.Add(finalizados[i]);

            return ultimos;
        }

        /// <summary>
        /// devuelve partidos pendientes en las próximas 24 horas
        /// </summary>
        /// <returns></returns>
        public List<Partido> ObtenerProximos24Horas()
        {
            DateTime fechaActual = SistemaFecha.FechaActual;
            DateTime limite = fechaActual.AddHours(24);

            List<Partido> proximos = new List<Partido>();

            foreach (Partido partido in _partidos)
            {
                if (partido.Estado == EstadoPartido.Pendiente &&
                    partido.FechaHora >= fechaActual &&
                    partido.FechaHora <= limite)
                {
                    proximos.Add(partido);
                }
            }

            for (int i = 0; i < proximos.Count - 1; i++)
            {
                for (int j = 0; j < proximos.Count - i - 1; j++)
                {
                    if (proximos[j].FechaHora > proximos[j + 1].FechaHora)
                    {
                        Partido temp = proximos[j];
                        proximos[j] = proximos[j + 1];
                        proximos[j + 1] = temp;
                    }
                }
            }

            return proximos;
        }

        /// <summary>
        /// returna los partidos de un grupo específico
        /// </summary>
        /// <param name="grupo"></param>
        /// <returns></returns>
        public List<Partido> ObtenerPorGrupo(string grupo)
        {
            List<Partido> resultado = new List<Partido>();

            foreach (Partido partidos in _partidos)
            {
                if (partidos.Grupo == grupo)
                    resultado.Add(partidos);
            }

            return resultado;
        }

        /// <summary>
        /// devuelve los partidos de una fase específica
        /// </summary>
        /// <param name="fase"></param>
        /// <returns></returns>
        public List<Partido> ObtenerPorFase(string fase)
        {
            List<Partido> resultado = new List<Partido>();

            foreach (Partido partido in _partidos)
            {
                if (partido.Fase == fase)
                    resultado.Add(partido);
            }

            return resultado;
        }

        /// <summary>
        /// realiza un calculo en la tabla de posiciones de un grupo
        /// </summary>
        /// <param name="grupo"></param>
        /// <returns></returns>
        public List<PosicionGrupo> CalcularTablaGrupo(string grupo)
        {
            List<Partido> partidos = new List<Partido>();
            foreach (Partido partido in ObtenerPorGrupo(grupo))
            {
                if (partido.Estado == EstadoPartido.Finalizado)
                    partidos.Add(partido);
            }

            List<PosicionGrupo> tabla = new List<PosicionGrupo>();
            foreach (Seleccion seleccion in _selecciones)
            {
                if (seleccion.Grupo == grupo)
                {
                    tabla.Add(new PosicionGrupo
                    {
                        Codigo = seleccion.Codigo,
                        Nombre = seleccion.Nombre
                    });
                }
            }

            foreach (Partido partido in partidos)
            {
                PosicionGrupo local = null;
                PosicionGrupo visitante = null;

                foreach (PosicionGrupo pos in tabla)
                {
                    if (pos.Codigo == partido.CodigoLocal) local = pos;
                    if (pos.Codigo == partido.CodigoVisitante) visitante = pos;
                }

                if (local == null || visitante == null) continue;

                local.PartidosJugados++;
                visitante.PartidosJugados++;
                local.GolesFavor += partido.GolesLocal;
                local.GolesContra += partido.GolesVisitante;
                visitante.GolesFavor += partido.GolesVisitante;
                visitante.GolesContra += partido.GolesLocal;

                if (partido.GolesLocal > partido.GolesVisitante)
                {
                    local.Puntos += 3;
                    local.Ganados++;
                    visitante.Perdidos++;
                }
                else if (partido.GolesLocal < partido.GolesVisitante)
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

            for (int i = 0; i < tabla.Count - 1; i++)
            {
                for (int j = 0; j < tabla.Count - i - 1; j++)
                {
                    bool debeSwap = tabla[j].Puntos < tabla[j + 1].Puntos ||
                        (tabla[j].Puntos == tabla[j + 1].Puntos &&
                         tabla[j].DiferenciaGoles < tabla[j + 1].DiferenciaGoles);

                    if (debeSwap)
                    {
                        PosicionGrupo temp = tabla[j];
                        tabla[j] = tabla[j + 1];
                        tabla[j + 1] = temp;
                    }
                }
            }

            return tabla;
        }
    }

    /// <summary>
    /// trabaja como clase auxiliar para la tabla de posiciones
    /// </summary>
    public class PosicionGrupo
    {
        /// <summary>
        /// código del equipo
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// nombre del equipo
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// partidos jugados
        /// </summary>
        public int PartidosJugados { get; set; }
        /// <summary>
        /// partidos ganados
        /// </summary>
        public int Ganados { get; set; }
        /// <summary>
        /// partidos empatados
        /// </summary>
        public int Empatados { get; set; }
        /// <summary>
        /// partidos perdidos
        /// </summary>
        public int Perdidos { get; set; }
        /// <summary>
        /// goles a favor
        /// </summary>
        public int GolesFavor { get; set; }
        /// <summary>
        /// goles en contra
        /// </summary>
        public int GolesContra { get; set; }
        /// <summary>
        /// puntos obtenidos
        /// </summary>
        public int Puntos { get; set; }
        /// <summary>
        /// diferencia entre goles a favor y en contra
        /// </summary>
        public int DiferenciaGoles => GolesFavor - GolesContra;
    }
}