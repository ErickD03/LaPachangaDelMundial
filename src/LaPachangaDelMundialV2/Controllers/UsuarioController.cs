using LaPachangaDelMundialV2.Models;
using LaPachangaDelMundialV2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaPachangaDelMundialV2.Controllers
{
    /// <summary>
    /// controlador de usuarios
    /// </summary>
    public class UsuarioController
    {
        private readonly List<Usuario> _usuarios;

        /// <summary>
        /// inicializa el controlador y carga los usuarios
        /// </summary>
        public UsuarioController()
        {
            _usuarios = JsonLoader.CargarUsuarios();
        }

        /// <summary>
        /// revisa si el usuario y contraseña existen
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <param name="contrasena"></param>
        /// <returns></returns>
        public Usuario Login(string nombreUsuario, string contrasena)
        {
            foreach (Usuario usuario in _usuarios)
            {
                if (usuario.NombreUsuario == nombreUsuario &&
                    usuario.Contrasena == contrasena &&
                    usuario.Activo)
                {
                    return usuario;
                }
            }
            return null;
        }

        /// <summary>
        /// este revisa si un nombre de usuario ya está en uso
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <returns></returns>
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

        /// <summary>
        /// retorna un usuario por su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// retorna la lista completa de usuarios
        /// </summary>
        /// <returns></returns>
        public List<Usuario> ObtenerTodos()
        {
            return _usuarios;
        }
        /// <summary>
        /// este registra un nuevo usuario
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <param name="contrasena"></param>
        /// <param name="codigoPais"></param>
        /// <returns></returns>
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

        /// <summary>
        /// restablece la contraseña de un usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="nuevaContrasena"></param>
        public void ResetearContrasena(string idUsuario, string nuevaContrasena)
        {
            foreach (Usuario usuario in _usuarios)
            {
                if (usuario.Id == idUsuario)
                {
                    usuario.Contrasena = nuevaContrasena;
                    break;
                }
            }
            GuardarUsuarios();
        }

        /// <summary>
        /// desactiva un usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        public void DesactivarUsuario(string idUsuario)
        {
            foreach (Usuario usuario in _usuarios)
            {
                if (usuario.Id == idUsuario)
                {
                    usuario.Activo = false;
                    break;
                }
            }
            GuardarUsuarios();
        }

        /// <summary>
        /// activa un usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        public void ActivarUsuario(string idUsuario)
        {
            foreach (Usuario usuario in _usuarios)
            {
                if (usuario.Id == idUsuario)
                {
                    usuario.Activo = true;
                    break;
                }
            }
            GuardarUsuarios();
        }

        /// <summary>
        /// obtiene un usuario desactivado por su nombre
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <returns></returns>
        public Usuario ObtenerDesactivado(string nombreUsuario)
        {
            foreach (Usuario usuario in _usuarios)
            {
                if (usuario.NombreUsuario == nombreUsuario && !usuario.Activo)
                    return usuario;
            }
            return null;
        }
    }
}