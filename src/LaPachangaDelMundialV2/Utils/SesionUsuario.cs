using LaPachangaDelMundialV2.Models;

namespace LaPachangaDelMundialV2.Utils
{
    /// <summary>
    /// gestiona la sesión del usuario
    /// </summary>
    public class SesionUsuario
    {
        /// <summary>
        /// usuario actualmente activo
        /// </summary>
        public Usuario UsuarioActivo { get; set; }
        /// <summary>
        /// indica si hay una sesión activa
        /// </summary>
        public bool EstaLogueado => UsuarioActivo != null;
        /// <summary>
        /// indica si el usuario es administrador
        /// </summary>
        public bool EsAdministrador => UsuarioActivo?.NombreUsuario == "admin";

        /// <summary>
        /// inicia sesión con un usuario
        /// </summary>
        /// <param name="usuario"></param>
        public void IniciarSesion(Usuario usuario)
        {
            UsuarioActivo = usuario;
        }

        /// <summary>
        /// cierra la sesión actual
        /// </summary>
        public void CerrarSesion()
        {
            UsuarioActivo = null;
        }
    }
}