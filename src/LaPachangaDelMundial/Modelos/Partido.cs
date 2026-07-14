using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LaPachangaDelMundial.Models
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
        public string Fase { get; set; }        // Fase = grupos, octavos, cuartos
        public string Grupo { get; set; }       // Válido solo fase grupos
        public DateTime FechaHora { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public EstadoPartido Estado { get; set; }

        public Partido() { }
    }
}