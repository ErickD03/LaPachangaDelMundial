using System.Collections.Generic;

namespace LaPachangaDelMundialV2.Models
{
    /// <summary>
    /// representa un usuario
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// identificador del usuario
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// nombre de usuario
        /// </summary>
        public string NombreUsuario { get; set; }
        /// <summary>
        /// contraseña del usuario
        /// </summary>
        public string Contrasena { get; set; }
        /// <summary>
        /// código del país preferido
        /// </summary>
        public string CodigoPaisPreferido { get; set; }
        /// <summary>
        /// puntos del usuario
        /// </summary>
        public int Puntos { get; set; }
        /// <summary>
        /// lista de insignias
        /// </summary>
        public List<string> Insignias { get; set; }
        /// <summary>
        /// lista de quinielas
        /// </summary>
        public List<string> IdsQuinielas { get; set; }
        /// <summary>
        /// indica si es administrador
        /// </summary>
        public bool EsAdministrador { get; set; }
        /// <summary>
        /// indica si el usuario está activo
        /// </summary>
        public bool Activo { get; set; } = true;

        /// <summary>
        /// inicializa un nuevo usuario
        /// </summary>
        public Usuario()
        {
            Insignias = new List<string>();
            IdsQuinielas = new List<string>();
        }
    }
}