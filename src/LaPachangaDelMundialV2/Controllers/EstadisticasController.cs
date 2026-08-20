using System;
using System.Collections.Generic;
using LaPachangaDelMundialV2.Models;

namespace LaPachangaDelMundialV2.Controllers
{
    public class EstadisticasController
    {
        private readonly List<Partido> _partidos;
        private readonly List<Usuario> _usuarios;
        private readonly List<Pronostico> _pronosticos;
        private readonly List<Seleccion> _selecciones;

        public EstadisticasController(
            List<Partido> partidos,
            List<Usuario> usuarios,
            List<Pronostico> pronosticos,
            List<Seleccion> selecciones)
        {
            _partidos = partidos;
            _usuarios = usuarios;
            _pronosticos = pronosticos;
            _selecciones = selecciones;
        }

        private string ObtenerNombre(string codigo)
        {
            foreach (Seleccion seleccion in _selecciones)
            {
                if (seleccion.Codigo == codigo)
                {
                    return seleccion.Nombre;
                }
            }

            return codigo;
        }

        private List<Partido> ObtenerPartidosFinalizados(DateTime desde, DateTime hasta)
        {
            List<Partido> resultado = new List<Partido>();
            foreach (Partido partido in _partidos)
            {
                if (partido.Estado == EstadoPartido.Finalizado &&
                    partido.FechaHora >= desde && partido.FechaHora <= hasta)
                {
                    resultado.Add(partido);
                }
            }
            return resultado;
        }

        private List<Pronostico> ObtenerPronosticosEnRango(DateTime desde, DateTime hasta)
        {
            List<string> idsPartidos = new List<string>();
            foreach (Partido partido in _partidos)
            {
                if (partido.FechaHora >= desde && partido.FechaHora <= hasta)
                    idsPartidos.Add(partido.Id);
            }

            List<Pronostico> resultado = new List<Pronostico>();
            foreach (Pronostico pronostico in _pronosticos)
            {
                if (idsPartidos.Contains(pronostico.IdPartido))
                    resultado.Add(pronostico);
            }
            return resultado;
        }

        /// <summary>
        /// equipo más apostado como ganador
        /// </summary>
        /// <param name="desde"></param>
        /// <param name="hasta"></param>
        /// <returns></returns>
        public string EquipoMasApostado(DateTime desde, DateTime hasta)
        {
            List<Pronostico> pronosticos = ObtenerPronosticosEnRango(desde, hasta);

            string codigoGanador = "";
            int maxVotos = 0;

            Dictionary<string, int> conteo = new Dictionary<string, int>();

            foreach (Pronostico pronostico in pronosticos)
            {
                Partido partido = null;
                foreach (Partido p in _partidos)
                {
                    if (p.Id == pronostico.IdPartido) { partido = p; break; }
                }
                if (partido == null) continue;

                string apostado = "";
                if (pronostico.GolesLocal > pronostico.GolesVisitante)
                    apostado = partido.CodigoLocal;
                else if (pronostico.GolesVisitante > pronostico.GolesLocal)
                    apostado = partido.CodigoVisitante;
                else
                    apostado = "Empate";

                if (apostado == "Empate") continue;

                if (!conteo.ContainsKey(apostado))
                    conteo[apostado] = 0;
                conteo[apostado]++;

                if (conteo[apostado] > maxVotos)
                {
                    maxVotos = conteo[apostado];
                    codigoGanador = apostado;
                }
            }

            if (codigoGanador == "") return "Sin datos";
            return ObtenerNombre(codigoGanador);
        }

        /// <summary>
        /// resultado más repetido en partidos finalizados
        /// </summary>
        /// <param name="desde"></param>
        /// <param name="hasta"></param>
        /// <returns></returns>
        public string ResultadoMasRepetido(DateTime desde, DateTime hasta)
        {
            Dictionary<string, int> resultados = new Dictionary<string, int>();

            foreach (Partido partido in _partidos)
            {
                if (partido.Estado == EstadoPartido.Finalizado &&
                    partido.FechaHora >= desde &&
                    partido.FechaHora <= hasta)
                {
                    string marcador = partido.GolesLocal + "-" + partido.GolesVisitante;

                    if (resultados.ContainsKey(marcador))
                        resultados[marcador]++;
                    else
                        resultados.Add(marcador, 1);
                }
            }

            string mejor = "";
            int mayor = 0;

            foreach (KeyValuePair<string, int> dato in resultados)
            {
                if (dato.Value > mayor)
                {
                    mayor = dato.Value;
                    mejor = dato.Key;
                }
            }

            if (mejor == "")
                return "Sin datos";

            return mejor + " (" + mayor + " veces)";
        }

        /// <summary>
        /// partido con más aciertos de marcador exacto
        /// </summary>
        /// <param name="desde"></param>
        /// <param name="hasta"></param>
        /// <returns></returns>
        public string PartidoConMasAciertos(DateTime desde, DateTime hasta)
        {
            List<Partido> partidos = ObtenerPartidosFinalizados(desde, hasta);

            Partido mejorPartido = null;
            int maxAciertos = 0;

            foreach (Partido partido in partidos)
            {
                int aciertos = 0;
                foreach (Pronostico pronostico in _pronosticos)
                {
                    if (pronostico.IdPartido == partido.Id &&
                        pronostico.GolesLocal == partido.GolesLocal &&
                        pronostico.GolesVisitante == partido.GolesVisitante)
                    {
                        aciertos++;
                    }
                }

                if (aciertos > maxAciertos)
                {
                    maxAciertos = aciertos;
                    mejorPartido = partido;
                }
            }

            if (mejorPartido == null) return "Sin datos";

            string local = ObtenerNombre(mejorPartido.CodigoLocal);
            string visitante = ObtenerNombre(mejorPartido.CodigoVisitante);
            return $"{local} vs {visitante} ({maxAciertos} aciertos)";
        }

        /// <summary>
        /// usuario con más aciertos exactos en el rango
        /// </summary>
        /// <param name="desde"></param>
        /// <param name="hasta"></param>
        /// <returns></returns>
        public string UsuarioConMasAciertos(DateTime desde, DateTime hasta)
        {
            List<Partido> partidos = ObtenerPartidosFinalizados(desde, hasta);

            List<string> idsPartidos = new List<string>();
            foreach (Partido partido in partidos)
                idsPartidos.Add(partido.Id);

            Dictionary<string, int> aciertos = new Dictionary<string, int>();

            foreach (Pronostico pronostico in _pronosticos)
            {
                if (!idsPartidos.Contains(pronostico.IdPartido)) continue;
                if (pronostico.PuntosObtenidos != 5) continue;

                Usuario usuario = null;
                foreach (Usuario us in _usuarios)
                {
                    if (us.Id == pronostico.IdUsuario)
                    {
                        usuario = us;
                        break;
                    }
                }

                if (usuario == null) continue;
                if (usuario.EsAdministrador) continue;

                if (!aciertos.ContainsKey(pronostico.IdUsuario))
                    aciertos[pronostico.IdUsuario] = 0;
                aciertos[pronostico.IdUsuario]++;
            }

            string mejorUsuario = "";
            int maxAciertos = 0;

            foreach (KeyValuePair<string, int> item in aciertos)
            {
                if (item.Value > maxAciertos)
                {
                    maxAciertos = item.Value;
                    mejorUsuario = item.Key;
                }
            }

            if (mejorUsuario == "") return "Sin datos";

            foreach (Usuario usuario in _usuarios)
            {
                if (usuario.Id == mejorUsuario)
                    return $"{usuario.NombreUsuario} ({maxAciertos} aciertos exactos)";
            }

            return "Sin datos";
        }

        /// <summary>
        /// partido con más pronósticos registrados
        /// </summary>
        /// <param name="desde"></param>
        /// <param name="hasta"></param>
        /// <returns></returns>
        public string PartidoConMasPronosticos(DateTime desde, DateTime hasta)
        {
            List<Pronostico> pronosticos = ObtenerPronosticosEnRango(desde, hasta);

            Dictionary<string, int> conteo = new Dictionary<string, int>();

            foreach (Pronostico pronostico in pronosticos)
            {
                if (!conteo.ContainsKey(pronostico.IdPartido))
                    conteo[pronostico.IdPartido] = 0;
                conteo[pronostico.IdPartido]++;
            }

            string mejorId = "";
            int maxPronosticos = 0;

            foreach (KeyValuePair<string, int> item in conteo)
            {
                if (item.Value > maxPronosticos)
                {
                    maxPronosticos = item.Value;
                    mejorId = item.Key;
                }
            }

            if (mejorId == "") return "Sin datos";

            foreach (Partido partido in _partidos)
            {
                if (partido.Id == mejorId)
                {
                    string local = ObtenerNombre(partido.CodigoLocal);
                    string visitante = ObtenerNombre(partido.CodigoVisitante);
                    return $"{local} vs {visitante} ({maxPronosticos} pronósticos)";
                }
            }

            return "Sin datos";
        }

        /// <summary>
        /// promedio de goles por partido en el rango
        /// </summary>
        /// <param name="desde"></param>
        /// <param name="hasta"></param>
        /// <returns></returns>
        public string PromedioGoles(DateTime desde, DateTime hasta)
        {
            int totalGoles = 0;
            int cantidad = 0;

            foreach (Partido partido in _partidos)
            {
                if (partido.Estado == EstadoPartido.Finalizado &&
                    partido.FechaHora >= desde &&
                    partido.FechaHora <= hasta)
                {
                    totalGoles += partido.GolesLocal + partido.GolesVisitante;
                    cantidad++;
                }
            }

            if (cantidad == 0)
                return "Sin datos";

            double promedio = (double)totalGoles / cantidad;

            return promedio.ToString("F2") + " goles por partido";
        }

        /// <summary>
        /// mejor rendimiento entre no favoritos
        /// </summary>
        /// <param name="desde"></param>
        /// <param name="hasta"></param>
        /// <returns></returns>
        public string EquipoSorpresa(DateTime desde, DateTime hasta)
        {
            List<Partido> partidos = ObtenerPartidosFinalizados(desde, hasta);

            List<string> favoritos = new List<string> { "BRA", "ARG", "FRA", "ESP", "ENG", "GER", "POR" };

            Dictionary<string, int> diferencias = new Dictionary<string, int>();

            foreach (Partido partido in partidos)
            {
                if (!favoritos.Contains(partido.CodigoLocal))
                {
                    if (!diferencias.ContainsKey(partido.CodigoLocal))
                        diferencias[partido.CodigoLocal] = 0;
                    diferencias[partido.CodigoLocal] += partido.GolesLocal - partido.GolesVisitante;
                }

                if (!favoritos.Contains(partido.CodigoVisitante))
                {
                    if (!diferencias.ContainsKey(partido.CodigoVisitante))
                        diferencias[partido.CodigoVisitante] = 0;
                    diferencias[partido.CodigoVisitante] += partido.GolesVisitante - partido.GolesLocal;
                }
            }

            string mejorEquipo = "";
            int mejorDiferencia = int.MinValue;

            foreach (KeyValuePair<string, int> item in diferencias)
            {
                if (item.Value > mejorDiferencia)
                {
                    mejorDiferencia = item.Value;
                    mejorEquipo = item.Key;
                }
            }

            if (mejorEquipo == "") return "Sin datos";
            return ObtenerNombre(mejorEquipo);
        }
    }
}