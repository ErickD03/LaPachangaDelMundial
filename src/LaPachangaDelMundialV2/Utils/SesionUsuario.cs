using LaPachangaDelMundialV2.Models;

namespace LaPachangaDelMundialV2.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class SesionUsuario
    {
        public Usuario UsuarioActivo { get; set; }
        public bool EstaLogueado => UsuarioActivo != null;
        public bool EsAdministrador => UsuarioActivo?.NombreUsuario == "admin";

        public void IniciarSesion(Usuario usuario)
        {
            UsuarioActivo = usuario;
        }

        public void CerrarSesion()
        {
            UsuarioActivo = null;
        }
    }
}