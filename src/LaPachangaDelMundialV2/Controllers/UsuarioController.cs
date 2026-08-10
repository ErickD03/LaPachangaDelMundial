using LaPachangaDelMundialV2.Models;
using LaPachangaDelMundialV2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaPachangaDelMundialV2.Controllers
{
    public class UsuarioController
    {
        private readonly List<Usuario> _usuarios;

        public UsuarioController()
        {
            _usuarios = JsonLoader.CargarUsuarios();
        }

        // revisa si el usuario y contraseña existen //
        public Usuario Login(string nombreUsuario, string contrasena)
        {
            foreach (Usuario usuario in _usuarios)
            {
                if (usuario.NombreUsuario == nombreUsuario && usuario.Contrasena == contrasena)
                {
                    return usuario;
                }
            }
            return null;
        }

        // este revisa si un nombre de usuario ya está en uso //
        public bool ExisteUsuario(string nombreUsuario)
        {
            foreach (Usuario usuario in _usuarios)
            {
                if (usuario.NombreUsuario == nombreUsuario)
                {
                    return true;
                }
            }
            return false;
        }

        // retorna un usuario por su ID //
        public Usuario ObtenerPorId(string id)
        {
            foreach (Usuario usuario in _usuarios)
            {
                if (usuario.Id == id)
                {
                    return usuario;
                }
            }
            return null;
        }

        // retorna la lista completa de usuarios
        public List<Usuario> ObtenerTodos()
        {
            return _usuarios;
        }
        // este registra un nuevo usuario //
        public bool Registrar(string nombreUsuario, string contrasena, string codigoPais)
        {
            if (ExisteUsuario(nombreUsuario))
                return false;

            Usuario nuevo = new Usuario();

            nuevo.Id = "U" + (_usuarios.Count + 1).ToString("D2");
            nuevo.NombreUsuario = nombreUsuario;
            nuevo.Contrasena = contrasena;
            nuevo.CodigoPaisPreferido = codigoPais;
            nuevo.Puntos = 0;
            _usuarios.Add(nuevo);
            GuardarUsuarios();

            return true;
        }
        private void GuardarUsuarios()
        {
            string ruta = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Datos",
                "usuarios.json");

            string contenido = Newtonsoft.Json.JsonConvert.SerializeObject(
                _usuarios,
                Newtonsoft.Json.Formatting.Indented);

            System.IO.File.WriteAllText(ruta, contenido);
        }
    }
}