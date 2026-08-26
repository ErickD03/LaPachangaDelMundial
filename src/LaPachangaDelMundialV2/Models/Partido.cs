using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LaPachangaDelMundialV2.Models
{
    /// <summary>
    /// estados posibles de un partido
    /// </summary>
    public enum EstadoPartido
    {
        Pendiente,
        EnCurso,
        Finalizado
    }

    /// <summary>
    /// representa un partido
    /// </summary>
    public class Partido
    {
        /// <summary>
        /// identificador del partido
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// código del equipo local
        /// </summary>
        public string CodigoLocal { get; set; }
        /// <summary>
        /// código del equipo visitante
        /// </summary>
        public string CodigoVisitante { get; set; }
        /// <summary>
        /// goles del equipo local
        /// </summary>
        public int GolesLocal { get; set; }
        /// <summary>
        /// goles del equipo visitante
        /// </summary>
        public int GolesVisitante { get; set; }
        /// <summary>
        /// fase como grupos, octavos, cuartos
        /// </summary>
        public string Fase { get; set; }
        /// <summary>
        /// solo fase grupos
        /// </summary>
        public string Grupo { get; set; }
        /// <summary>
        /// fecha y hora del partido
        /// </summary>
        public DateTime FechaHora { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        /// <summary>
        /// estado actual del partido
        /// </summary>
        public EstadoPartido Estado { get; set; }
        /// <summary>
        /// inicializa un nuevo partido
        /// </summary>
        public Partido() { }
    }
}