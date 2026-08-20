using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LaPachangaDelMundialV2.Models
{
    public enum EstadoPartido
    {
        Pendiente,
        EnCurso,
        Finalizado
    }

    public class Partido
    {
        public string Id { get; set; }
        public string CodigoLocal { get; set; }
        public string CodigoVisitante { get; set; }
        public int GolesLocal { get; set; }
        public int GolesVisitante { get; set; }
        /// <summary>
        /// fase como grupos, octavos, cuartos
        /// </summary>
        public string Fase { get; set; }
        /// <summary>
        /// solo fase grupos
        /// </summary>
        public string Grupo { get; set; }
        public DateTime FechaHora { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public EstadoPartido Estado { get; set; }

        public Partido() { }
    }
}