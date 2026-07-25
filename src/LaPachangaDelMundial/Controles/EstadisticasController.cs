using System;
using System.Collections.Generic;
using System.Linq;
using LaPachangaDelMundial.Models;

namespace LaPachangaDelMundial.Controllers
{
    public class EstadisticasController
    {
        private List<Partido> _partidos;
        private List<Usuario> _usuarios;
        private List<Pronostico> _pronosticos;
        private List<Seleccion> _selecciones;

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

        // equipo con mayores probabilidades de ganar en un periodo específico //
        public string EquipoMasApostado(DateTime desde, DateTime hasta)
        {
            var partidosRango = _partidos
                .Where(p => p.FechaHora >= desde && p.FechaHora <= hasta)
                .Select(p => p.Id)
                .ToList();

            var apuestas = _pronosticos
                .Where(p => partidosRango.Contains(p.IdPartido))
                .GroupBy(p =>
                {
                    Partido partido = _partidos.FirstOrDefault(pa => pa.Id == p.IdPartido);
                    if (partido == null) return "";
                    return p.GolesLocal > p.GolesVisitante
                        ? partido.CodigoLocal
                        : p.GolesVisitante > p.GolesLocal
                            ? partido.CodigoVisitante
                            : "Empate";
                })
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            if (apuestas == null) return "Sin datos";

            Seleccion sel = _selecciones.FirstOrDefault(s => s.Codigo == apuestas.Key);
            return sel != null ? sel.Nombre : apuestas.Key;
        }

        // resultado más repetido en rango de fechas //
        public string ResultadoMasRepetido(DateTime desde, DateTime hasta)
        {
            var resultado = _partidos
                .Where(p => p.Estado == EstadoPartido.Finalizado &&
                            p.FechaHora >= desde && p.FechaHora <= hasta)
                .GroupBy(p => $"{p.GolesLocal}-{p.GolesVisitante}")
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            return resultado != null
                ? $"{resultado.Key} ({resultado.Count()} veces)"
                : "Sin datos";
        }

        // partido más aciertos marcador exacto //
        public string PartidoConMasAciertos(DateTime desde, DateTime hasta)
        {
            var partidosRango = _partidos
                .Where(p => p.Estado == EstadoPartido.Finalizado &&
                            p.FechaHora >= desde && p.FechaHora <= hasta)
                .ToList();

            Partido mejor = null;
            int maxAciertos = 0;

            foreach (Partido p in partidosRango)
            {
                int aciertos = _pronosticos.Count(pr =>
                    pr.IdPartido == p.Id &&
                    pr.GolesLocal == p.GolesLocal &&
                    pr.GolesVisitante == p.GolesVisitante);

                if (aciertos > maxAciertos)
                {
                    maxAciertos = aciertos;
                    mejor = p;
                }
            }

            if (mejor == null) return "Sin datos";

            Seleccion local = _selecciones.FirstOrDefault(s => s.Codigo == mejor.CodigoLocal);
            Seleccion visitante = _selecciones.FirstOrDefault(s => s.Codigo == mejor.CodigoVisitante);

            string nombreLocal = local != null ? local.Nombre : mejor.CodigoLocal;
            string nombreVisitante = visitante != null ? visitante.Nombre : mejor.CodigoVisitante;

            return $"{nombreLocal} vs {nombreVisitante} ({maxAciertos} aciertos)";
        }

        // ususario más aciertos en rango de fecha //
        public string UsuarioConMasAciertos(DateTime desde, DateTime hasta)
        {
            var partidosRango = _partidos
                .Where(p => p.Estado == EstadoPartido.Finalizado &&
                            p.FechaHora >= desde && p.FechaHora <= hasta)
                .Select(p => p.Id)
                .ToList();

            var mejor = _pronosticos
                .Where(p => partidosRango.Contains(p.IdPartido) &&
                            p.PuntosObtenidos == 5)
                .GroupBy(p => p.IdUsuario)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            if (mejor == null) return "Sin datos";

            Usuario usuario = _usuarios.FirstOrDefault(u => u.Id == mejor.Key);
            return usuario != null
                ? $"{usuario.NombreUsuario} ({mejor.Count()} aciertos exactos)"
                : mejor.Key;
        }

        // partido más pronostico registrados //
        public string PartidoConMasPronosticos(DateTime desde, DateTime hasta)
        {
            var partidosRango = _partidos
                .Where(p => p.FechaHora >= desde && p.FechaHora <= hasta)
                .Select(p => p.Id)
                .ToList();

            var mejor = _pronosticos
                .Where(p => partidosRango.Contains(p.IdPartido))
                .GroupBy(p => p.IdPartido)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            if (mejor == null) return "Sin datos";

            Partido partido = _partidos.FirstOrDefault(p => p.Id == mejor.Key);
            if (partido == null) return "Sin datos";

            Seleccion local = _selecciones.FirstOrDefault(s => s.Codigo == partido.CodigoLocal);
            Seleccion visitante = _selecciones.FirstOrDefault(s => s.Codigo == partido.CodigoVisitante);

            string nombreLocal = local != null ? local.Nombre : partido.CodigoLocal;
            string nombreVisitante = visitante != null ? visitante.Nombre : partido.CodigoVisitante;

            return $"{nombreLocal} vs {nombreVisitante} ({mejor.Count()} pronósticos)";
        }

        // promedio goles partido rango de fechas //
        public string PromedioGoles(DateTime desde, DateTime hasta)
        {
            var partidos = _partidos
                .Where(p => p.Estado == EstadoPartido.Finalizado &&
                            p.FechaHora >= desde && p.FechaHora <= hasta)
                .ToList();

            if (partidos.Count == 0) return "Sin datos";

            double promedio = partidos
                .Average(p => p.GolesLocal + p.GolesVisitante);

            return $"{promedio:F2} goles por partido";
        }

        // equipo sorpressa según diferencia de goles entre los no favoritos //
        public string EquipoSorpresa(DateTime desde, DateTime hasta)
        {
            string[] favoritos = { "BRA", "ARG", "FRA", "ESP", "ENG", "GER", "POR" };

            var sorpresa = _partidos
                .Where(p => p.Estado == EstadoPartido.Finalizado &&
                            p.FechaHora >= desde && p.FechaHora <= hasta)
                .SelectMany(p => new[]
                {
                    new { Codigo = p.CodigoLocal,
                          Goles = p.GolesLocal - p.GolesVisitante },
                    new { Codigo = p.CodigoVisitante,
                          Goles = p.GolesVisitante - p.GolesLocal }
                })
                .Where(x => !favoritos.Contains(x.Codigo))
                .GroupBy(x => x.Codigo)
                .Select(g => new { Codigo = g.Key, Total = g.Sum(x => x.Goles) })
                .OrderByDescending(x => x.Total)
                .FirstOrDefault();

            if (sorpresa == null) return "Sin datos";

            Seleccion sel = _selecciones.FirstOrDefault(s => s.Codigo == sorpresa.Codigo);
            return sel != null ? sel.Nombre : sorpresa.Codigo;
        }
    }
}