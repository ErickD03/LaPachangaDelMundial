using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace LaPachangaDelMundialV2.Models
{
    /// <summary>
    /// tipos de quiniela
    /// </summary>
    public enum TipoQuiniela
    {
        Publica,
        Privada
    }

    /// <summary>
    /// representa una quiniela
    /// </summary>
    public class Quiniela
    {
        /// <summary>
        /// identificador de la quiniela
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// nombre de la quiniela
        /// </summary>
        public string Nombre { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        /// <summary>
        /// tipo de quiniela
        /// </summary>
        public TipoQuiniela Tipo { get; set; }
        /// <summary>
        /// identificador del creador
        /// </summary>
        public string IdCreador { get; set; }
        /// <summary>
        /// lista de integrantes
        /// </summary>
        public List<string> IdsIntegrantes { get; set; }
        /// <summary>
        /// lista de notificaciones
        /// </summary>
        public List<string> Notificaciones { get; set; }

        /// <summary>
        /// inicializa una nueva quiniela
        /// </summary>
        public Quiniela()
        {
            IdsIntegrantes = new List<string>();
            Notificaciones = new List<string>();
        }
    }
}