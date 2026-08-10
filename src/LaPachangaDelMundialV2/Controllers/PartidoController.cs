using System;
using System.Collections.Generic;
using LaPachangaDelMundialV2.Models;
using LaPachangaDelMundialV2.Utils;

namespace LaPachangaDelMundialV2.Controllers
{
    public class PartidoController
    {
        private readonly List<Partido> _partidos;
        private readonly List<Seleccion> _selecciones;

        public PartidoController()
        {
            _partidos = JsonLoader.CargarPartidos();
            _selecciones = JsonLoader.CargarSelecciones();
        }

        // este devuelve todos los partidos //
        public List<Partido> ObtenerTodos()
        {
            return _partidos;
        }

        // este devuelve el nombre completo de una selección dado su código //
        public string ObtenerNombreSeleccion(string codigo)
        {
            foreach (Seleccion seleccion in _selecciones)
            {
                if (seleccion.Codigo == codigo)
                    return seleccion.Nombre;
            }
            return codigo;
        }

        // realiza la actualización el estado de cada partido según la fecha simulada //
        public void ActualizarEstados()
        {
            DateTime fechaActual = SistemaFecha.FechaActual;

            foreach (Partido partido in _partidos)
            {
                if (partido.Estado == EstadoPartido.Finalizado)
                    continue;

                if (partido.FechaHora <= fechaActual)
                    partido.Estado = EstadoPartido.EnCurso;
                else
                    partido.Estado = EstadoPartido.Pendiente;
            }
        }

        // devuelve los últimos 5 partidos finalizados //
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

        //devuelve partidos pendientes en las próximas 24 horas //
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

        // returna los partidos de un grupo específico //
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

        // devuelve los partidos de una fase específica //
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

        // realiza un calculo en la tabla de posiciones de un grupo //
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

    // trabaja como clase auxiliar para la tabla de posiciones //
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