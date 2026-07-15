using System.Collections.Generic;
using System.Linq;
using LaPachangaDelMundial.Models;
using LaPachangaDelMundial.Utils;

namespace LaPachangaDelMundial.Controllers
{
    public class UsuarioController
    {
        private List<Usuario> _usuarios;

        public UsuarioController()
        {
            _usuarios = JsonLoader.CargarUsuarios();
        }

        // revisa si el usuario y contraseña existen
        public Usuario Login(string nombreUsuario, string contrasena)
        {
            return _usuarios.FirstOrDefault(u =>
                u.NombreUsuario == nombreUsuario &&
                u.Contrasena == contrasena);
        }

        // este revisa si un nombre de usuario ya está en uso
        public bool ExisteUsuario(string nombreUsuario)
        {
            return _usuarios.Any(u => u.NombreUsuario == nombreUsuario);
        }

        // retorna un usuario por su ID
        public Usuario ObtenerPorId(string id)
        {
            return _usuarios.FirstOrDefault(u => u.Id == id);
        }

        // retorna la lista completa de usuarios
        public List<Usuario> ObtenerTodos()
        {
            return _usuarios;
        }
    }
}