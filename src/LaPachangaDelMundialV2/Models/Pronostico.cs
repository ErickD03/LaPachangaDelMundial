namespace LaPachangaDelMundialV2.Models
{
    /// <summary>
    /// representa un pronóstico
    /// </summary>
    public class Pronostico
    {
        /// <summary>
        /// identificador del usuario
        /// </summary>
        public string IdUsuario { get; set; }
        /// <summary>
        /// identificador del partido
        /// </summary>
        public string IdPartido { get; set; }
        /// <summary>
        /// goles pronosticados para el equipo local
        /// </summary>
        public int GolesLocal { get; set; }
        /// <summary>
        /// goles pronosticados para el equipo visitante
        /// </summary>
        public int GolesVisitante { get; set; }
        /// <summary>
        /// puntos obtenidos por el pronóstico
        /// </summary>
        public int PuntosObtenidos { get; set; }
        /// <summary>
        /// inicializa un nuevo pronóstico
        /// </summary>
        public Pronostico() { }
    }
}