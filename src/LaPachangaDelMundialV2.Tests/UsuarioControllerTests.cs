using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaPachangaDelMundialV2.Controllers;
using LaPachangaDelMundialV2.Models;

namespace LaPachangaDelMundialV2.Tests
{
    public class UsuarioControllerTests
    {
        private UsuarioController _controller;

        /// <summary>
        /// constructor que se ejecuta antes de cada prueba, crea una nueva instancia del controlador con los datos del JSON
        /// </summary>
        public UsuarioControllerTests()
        {
            _controller = new UsuarioController();
        }

        /// <summary>
        /// prueba: login con usuario y contraseña correctos, esperar que retorne el objeto Usuario (no null)
        /// </summary>
        [Fact]
        public void Login_ConCredencialesCorrectas_RetornaUsuario()
        {
            string nombreUsuario = "erick_araya";
            string contrasena = "pass123";

            Usuario resultado = _controller.Login(nombreUsuario, contrasena);

            Assert.NotNull(resultado);
            Assert.Equal("erick_araya", resultado.NombreUsuario);
        }

        /// <summary>
        /// prueba: login con contraseña incorrecta, esperar que retorne null porque la contraseña no coincide
        /// </summary>
        [Fact]
        public void Login_ConCredencialesIncorrectas_RetornaNull()
        {
            string nombreUsuario = "erick_araya";
            string contrasena = "contrasenaMal";

            Usuario resultado = _controller.Login(nombreUsuario, contrasena);

            Assert.Null(resultado);
        }

        /// <summary>
        /// prueba: login con usuario que no existe en el sistema, esperar que retorne null porque el usuario no existe
        /// </summary>
        [Fact]
        public void Login_ConUsuarioInexistente_RetornaNull()
        {
            string nombreUsuario = "usuarioQueNoExiste";
            string contrasena = "pass123";

            Usuario resultado = _controller.Login(nombreUsuario, contrasena);

            Assert.Null(resultado);
        }

        /// <summary>
        /// prueba: verificar si un usuario existente es encontrado, esperar que retorne true porque erick_araya sí existe
        /// </summary>
        [Fact]
        public void ExisteUsuario_ConUsuarioExistente_RetornaTrue()
        {
            string nombreUsuario = "erick_araya";

            bool resultado = _controller.ExisteUsuario(nombreUsuario);

            Assert.True(resultado);
        }

        /// <summary>
        /// prueba: verificar si un usuario inexistente es encontrado, esperar que retorne false porque ese usuario no existe
        /// </summary>
        [Fact]
        public void ExisteUsuario_ConUsuarioInexistente_RetornaFalse()
        {
            string nombreUsuario = "usuarioQueNoExiste";

            bool resultado = _controller.ExisteUsuario(nombreUsuario);

            Assert.False(resultado);
        }

        /// <summary>
        /// prueba: registrar un usuario completamente nuevo, usamos un nombre único basado en la hora para evitar duplicados
        /// </summary>
        [Fact]
        public void Registrar_NuevoUsuario_RetornaTrue()
        {
            string nombreUsuario = "testUser" + DateTime.Now.Ticks.ToString();
            string contrasena = "pass123";
            string codigoPais = "CRC";

            bool resultado = _controller.Registrar(nombreUsuario, contrasena, codigoPais);

            Assert.True(resultado);
        }

        /// <summary>
        /// prueba: intentar registrar un usuario que ya existe, esperar que retorne false porque erick_araya ya está registrado
        /// </summary>
        [Fact]
        public void Registrar_UsuarioExistente_RetornaFalse()
        {
            string nombreUsuario = "erick_araya";
            string contrasena = "pass123";
            string codigoPais = "ARG";

            bool resultado = _controller.Registrar(nombreUsuario, contrasena, codigoPais);

            Assert.False(resultado);
        }

        /// <summary>
        /// prueba: obtener la lista completa de usuarios, esperar que la lista no sea null y tenga al menos un usuario
        /// </summary>
        [Fact]
        public void ObtenerTodos_RetornaListaNoVacia()
        {
            List<Usuario> resultado = _controller.ObtenerTodos();

            Assert.NotNull(resultado);
            Assert.True(resultado.Count > 0);
        }
    }
}