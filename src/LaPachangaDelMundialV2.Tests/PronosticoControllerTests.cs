using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaPachangaDelMundialV2.Controllers;
using LaPachangaDelMundialV2.Models;
using LaPachangaDelMundialV2.Utils;

namespace LaPachangaDelMundialV2.Tests
{
    public class PronosticoControllerTests
    {
        private PronosticoController _controller;
        private PartidoController _partidoController;
        private UsuarioController _usuarioController;

        /// <summary>
        /// constructor que crea nuevas instancias antes de cada prueba
        /// </summary>
        public PronosticoControllerTests()
        {
            _controller = new PronosticoController();
            _partidoController = new PartidoController();
            _usuarioController = new UsuarioController();
        }

        /// <summary>
        /// prueba: registrar un pronóstico válido sobre un partido pendiente, esperar que retorne true porque el partido está pendiente
        /// </summary>
        [Fact]
        public void Registrar_PartidoPendiente_RetornaTrue()
        {
            SistemaFecha.FechaActual = new DateTime(2026, 7, 5, 12, 0, 0);
            _partidoController.ActualizarEstados();

            Partido partidoPendiente = null;
            foreach (Partido partido in _partidoController.ObtenerTodos())
            {
                if (partido.Estado == EstadoPartido.Pendiente)
                {
                    partidoPendiente = partido;
                    break;
                }
            }

            if (partidoPendiente == null) return;

            string idUsuario = "TEST_PRONOSTICO_" + DateTime.Now.Ticks.ToString();

            bool resultado = _controller.Registrar(
                idUsuario,
                partidoPendiente.Id,
                1, 0,
                EstadoPartido.Pendiente);

            Assert.True(resultado);
        }

        /// <summary>
        /// prueba: no se puede pronosticar un partido finalizado, esperar que retorne false porque el partido ya terminó
        /// </summary>
        [Fact]
        public void Registrar_PartidoFinalizado_RetornaFalse()
        {
            SistemaFecha.FechaActual = new DateTime(2026, 7, 19, 23, 59, 0);
            _partidoController.ActualizarEstados();

            Partido partidoFinalizado = null;
            foreach (Partido partido in _partidoController.ObtenerTodos())
            {
                if (partido.Estado == EstadoPartido.Finalizado)
                {
                    partidoFinalizado = partido;
                    break;
                }
            }

            if (partidoFinalizado == null) return;

            string idUsuario = "TEST_" + DateTime.Now.Ticks.ToString();

            bool resultado = _controller.Registrar(
                idUsuario,
                partidoFinalizado.Id,
                1, 0,
                EstadoPartido.Finalizado);

            Assert.False(resultado);
        }

        /// <summary>
        /// prueba: no se puede pronosticar dos veces el mismo partido, esperar que el segundo intento retorne false
        /// </summary>
        [Fact]
        public void Registrar_PronosticoDuplicado_RetornaFalse()
        {
            SistemaFecha.FechaActual = new DateTime(2026, 7, 5, 12, 0, 0);
            _partidoController.ActualizarEstados();

            Partido partidoPendiente = null;
            foreach (Partido partido in _partidoController.ObtenerTodos())
            {
                if (partido.Estado == EstadoPartido.Pendiente)
                {
                    partidoPendiente = partido;
                    break;
                }
            }

            if (partidoPendiente == null) return;

            string idUsuario = "TEST_DUP_" + DateTime.Now.Ticks.ToString();

            _controller.Registrar(idUsuario, partidoPendiente.Id,
                1, 0, EstadoPartido.Pendiente);

            bool resultado = _controller.Registrar(idUsuario, partidoPendiente.Id,
                2, 1, EstadoPartido.Pendiente);

            Assert.False(resultado);
        }

        /// <summary>
        /// prueba: calcular puntos con marcador exacto, si el pronóstico es igual al resultado real, esperamos 5 puntos
        /// </summary>
        [Fact]
        public void CalcularPuntos_MarcadorExacto_Retorna5Puntos()
        {
            Partido partido = new Partido
            {
                Id = "TEST_PARTIDO",
                GolesLocal = 2,
                GolesVisitante = 0,
                Estado = EstadoPartido.Finalizado
            };

            string idUsuario = "TEST_PUNTOS_" + DateTime.Now.Ticks.ToString();
            _controller.Registrar(idUsuario, "TEST_PARTIDO", 2, 0,
                EstadoPartido.Pendiente);

            _controller.CalcularPuntos(partido);

            List<Pronostico> pronosticos = _controller.ObtenerPorUsuario(idUsuario);

            Assert.True(pronosticos.Count > 0);
            Assert.Equal(5, pronosticos[0].PuntosObtenidos);
        }

        /// <summary>
        /// prueba: calcular puntos acertando solo el resultado, si acertamos quién gana pero no el marcador exacto, esperamos 2 puntos
        /// </summary>
        [Fact]
        public void CalcularPuntos_ResultadoCorrecto_Retorna2Puntos()
        {
            Partido partido = new Partido
            {
                Id = "TEST_PARTIDO2",
                GolesLocal = 2,
                GolesVisitante = 0,
                Estado = EstadoPartido.Finalizado
            };

            string idUsuario = "TEST_2PTS_" + DateTime.Now.Ticks.ToString();
            _controller.Registrar(idUsuario, "TEST_PARTIDO2", 1, 0,
                EstadoPartido.Pendiente);

            _controller.CalcularPuntos(partido);

            List<Pronostico> pronosticos = _controller.ObtenerPorUsuario(idUsuario);

            Assert.True(pronosticos.Count > 0);
            Assert.Equal(2, pronosticos[0].PuntosObtenidos);
        }

        /// <summary>
        /// prueba: calcular puntos con pronóstico incorrecto, si el pronóstico es completamente incorrecto, esperamos 0 puntos
        /// </summary>
        [Fact]
        public void CalcularPuntos_ResultadoIncorrecto_Retorna0Puntos()
        {
            Partido partido = new Partido
            {
                Id = "TEST_PARTIDO3",
                GolesLocal = 2,
                GolesVisitante = 0,
                Estado = EstadoPartido.Finalizado
            };

            string idUsuario = "TEST_0PTS_" + DateTime.Now.Ticks.ToString();
            _controller.Registrar(idUsuario, "TEST_PARTIDO3", 0, 1,
                EstadoPartido.Pendiente);

            _controller.CalcularPuntos(partido);

            List<Pronostico> pronosticos = _controller.ObtenerPorUsuario(idUsuario);

            Assert.True(pronosticos.Count > 0);
            Assert.Equal(0, pronosticos[0].PuntosObtenidos);
        }

        /// <summary>
        /// prueba: obtener ranking global con lista de usuarios, esperar que el ranking no sea null y tenga usuarios
        /// </summary>
        [Fact]
        public void ObtenerRankingGlobal_RetornaListaOrdenada()
        {
            List<Usuario> usuarios = _usuarioController.ObtenerTodos();

            List<RankingItem> resultado = _controller.ObtenerRankingGlobal(usuarios);

            Assert.NotNull(resultado);
            Assert.True(resultado.Count > 0);

            for (int i = 0; i < resultado.Count - 1; i++)
            {
                Assert.True(resultado[i].Puntos >= resultado[i + 1].Puntos);
            }
        }

        /// <summary>
        /// prueba: obtener pronósticos de un usuario existente, esperar que retorne la lista de pronósticos de ese usuario
        /// </summary>
        [Fact]
        public void ObtenerPorUsuario_UsuarioExistente_RetornaPronosticos()
        {
            string idUsuario = "U02";

            List<Pronostico> resultado = _controller.ObtenerPorUsuario(idUsuario);

            Assert.NotNull(resultado);
            Assert.True(resultado.Count > 0);
            foreach (Pronostico partido in resultado)
            {
                Assert.Equal(idUsuario, partido.IdUsuario);
            }
        }

        /// <summary>
        /// prueba: obtener pronósticos de un usuario sin pronósticos, esperar que retorne una lista vacía
        /// </summary>
        [Fact]
        public void ObtenerPorUsuario_UsuarioSinPronosticos_RetornaListaVacia()
        {
            string idUsuario = "USUARIO_SIN_PRONOSTICOS";

            List<Pronostico> resultado = _controller.ObtenerPorUsuario(idUsuario);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
        }
    }
}