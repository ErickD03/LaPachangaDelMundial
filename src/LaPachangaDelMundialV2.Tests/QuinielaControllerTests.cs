using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaPachangaDelMundialV2.Controllers;
using LaPachangaDelMundialV2.Models;

namespace LaPachangaDelMundialV2.Tests
{
    public class QuinielaControllerTests
    {
        private QuinielaController _controller;

        /// <summary>
        /// constructor que crea una nueva instancia antes de cada prueba
        /// </summary>
        public QuinielaControllerTests()
        {
            _controller = new QuinielaController();
        }

        /// <summary>
        /// prueba: obtener todas las quinielas, esperar que la lista no sea null y tenga quinielas cargadas
        /// </summary>
        [Fact]
        public void ObtenerTodas_RetornaListaNoVacia()
        {
            List<Quiniela> resultado = _controller.ObtenerTodas();

            Assert.NotNull(resultado);
            Assert.True(resultado.Count > 0);
        }

        /// <summary>
        /// prueba: obtener quinielas de un usuario que pertenece a varias, ususario 02 pertenece a Q1 y Q2
        /// </summary>
        [Fact]
        public void ObtenerPorUsuario_UsuarioConQuinielas_RetornaLista()
        {
            string idUsuario = "U02";

            List<Quiniela> resultado = _controller.ObtenerPorUsuario(idUsuario);

            Assert.NotNull(resultado);
            Assert.True(resultado.Count > 0);
            foreach (Quiniela q in resultado)
            {
                Assert.Contains(idUsuario, q.IdsIntegrantes);
            }
        }

        /// <summary>
        /// prueba: obtener quinielas de un usuario que no existe, esperar una lista vacía
        /// </summary>
        [Fact]
        public void ObtenerPorUsuario_UsuarioSinQuinielas_RetornaListaVacia()
        {
            string idUsuario = "USUARIO_INEXISTENTE";

            List<Quiniela> resultado = _controller.ObtenerPorUsuario(idUsuario);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
        }

        /// <summary>
        /// prueba: crear una quiniela nueva con nombre único, esperar que retorne true porque el nombre no existe
        /// </summary>
        [Fact]
        public void Crear_QuinielaNueva_RetornaTrue()
        {
            string nombre = "QuinielaTest_" + DateTime.Now.Ticks.ToString();
            string idCreador = "U01";

            bool resultado = _controller.Crear(nombre, TipoQuiniela.Privada, idCreador);

            Assert.True(resultado);
        }

        /// <summary>
        /// prueba: crear una quiniela con nombre ya existente, esperar que retorne false porque el nombre ya existe
        /// </summary>
        [Fact]
        public void Crear_QuinielaExistente_RetornaFalse()
        {
            string nombre = "Los Clavados del Pronostico";
            string idCreador = "U01";

            bool resultado = _controller.Crear(nombre, TipoQuiniela.Privada, idCreador);

            Assert.False(resultado);
        }

        /// <summary>
        /// prueba: unirse a una quiniela existente, esperar que retorne true porque el usuario no está en esa quiniela
        /// </summary>
        [Fact]
        public void UnirseAQuiniela_UsuarioNuevo_RetornaTrue()
        {
            string nombreQuiniela = "QuinielaUnirse_" + DateTime.Now.Ticks.ToString();
            _controller.Crear(nombreQuiniela, TipoQuiniela.Publica, "U01");

            Quiniela quinielaCreada = null;
            foreach (Quiniela quiniela in _controller.ObtenerTodas())
            {
                if (quiniela.Nombre == nombreQuiniela)
                {
                    quinielaCreada = quiniela;
                    break;
                }
            }

            if (quinielaCreada == null) return;

            bool resultado = _controller.UnirseAQuiniela(quinielaCreada.Id, "U05");

            Assert.True(resultado);
        }

        /// <summary>
        /// prueba: unirse a una quiniela donde ya se está, esperar que retorne false porque el usuario ya es integrante
        /// </summary>
        [Fact]
        public void UnirseAQuiniela_UsuarioYaIntegrante_RetornaFalse()
        {
            string idQuiniela = "Q1";
            string idUsuario = "U02";

            bool resultado = _controller.UnirseAQuiniela(idQuiniela, idUsuario);

            Assert.False(resultado);
        }

        /// <summary>
        /// prueba: unirse a una quiniela que no existe, esperar que retorne false porque el ID no existe
        /// </summary>
        [Fact]
        public void UnirseAQuiniela_QuinielaInexistente_RetornaFalse()
        {
            string idQuiniela = "Q_INEXISTENTE";
            string idUsuario = "U01";

            bool resultado = _controller.UnirseAQuiniela(idQuiniela, idUsuario);

            Assert.False(resultado);
        }

        /// <summary>
        /// prueba: obtener quinielas públicas, esperar que todas las retornadas sean de tipo Pública
        /// </summary>
        [Fact]
        public void ObtenerPublicas_RetornaSoloPublicas()
        {
            List<Quiniela> resultado = _controller.ObtenerPublicas();

            Assert.NotNull(resultado);
            foreach (Quiniela quiniela in resultado)
            {
                Assert.Equal(TipoQuiniela.Publica, quiniela.Tipo);
            }
        }
    }
}