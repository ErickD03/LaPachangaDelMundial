using System;

namespace LaPachangaDelMundialV2.Utils
{
    /// <summary>
    /// gestiona la fecha del sistema
    /// </summary>
    public static class SistemaFecha
    {
        /// <summary>
        /// fecha simulada
        /// </summary>
        public static DateTime FechaActual { get; set; } =
            new DateTime(2026, 7, 10, 23, 59, 0, DateTimeKind.Local);
    }
}