using LaPachangaDelMundial.Models;
using LaPachangaDelMundial.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

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
        // este registra un nuevo usuario y lo almacena en el json
        public bool Registrar(string nombreUsuario, string contrasena, string codigoPais)
            {
                if (ExisteUsuario(nombreUsuario))
                    return false;

                Usuario nuevo = new Usuario
                {
                    Id = $"U{_usuarios.Count + 1:D2}",
                    NombreUsuario = nombreUsuario,
                    Contrasena = contrasena,
                    CodigoPaisPreferido = codigoPais,
                    Puntos = 0
                };

                _usuarios.Add(nuevo);
                GuardarUsuarios();
                return true;
            }

            private void GuardarUsuarios()
            {
                string ruta = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "Datos", "usuarios.json");

                string contenido = Newtonsoft.Json.JsonConvert.SerializeObject(
                    _usuarios, Newtonsoft.Json.Formatting.Indented);

                System.IO.File.WriteAllText(ruta, contenido);
            }
    }
}