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
    public class EstadisticasControllerTests
    {
        private EstadisticasController _controller;
        private List<Partido> _partidos;
        private List<Usuario> _usuarios;
        private List<Pronostico> _pronosticos;
        private List<Seleccion> _selecciones;

        /// <summary>
        /// constructor que carga los datos y crea el controlador antes de cada prueba
        /// </summary>
        public EstadisticasControllerTests()
        {
            _partidos = JsonLoader.CargarPartidos();
            _usuarios = JsonLoader.CargarUsuarios();
            _pronosticos = JsonLoader.CargarPronosticos();
            _selecciones = JsonLoader.CargarSelecciones();

            _controller = new EstadisticasController(
                _partidos, _usuarios, _pronosticos, _selecciones);
        }

        /// <summary>
        /// prueba: Equipo más apostado en rango completo del mundial, esperar un resultado distinto de "Sin datos"
        /// </summary>
        [Fact]
        public void EquipoMasApostado_RangoCompleto_RetornaResultado()
        {
            DateTime desde = new DateTime(2026, 6, 11);
            DateTime hasta = new DateTime(2026, 7, 19, 23, 59, 59);

            string resultado = _controller.EquipoMasApostado(desde, hasta);

            Assert.NotNull(resultado);
            Assert.NotEqual("Sin datos", resultado);
        }

        /// <summary>
        /// prueba: resultado más repetido en rango completo, esperar un resultado con formato "X-Y (Z veces)"
        /// </summary>
        [Fact]
        public void ResultadoMasRepetido_RangoCompleto_RetornaResultado()
        {
            DateTime desde = new DateTime(2026, 6, 11);
            DateTime hasta = new DateTime(2026, 7, 19, 23, 59, 59);

            string resultado = _controller.ResultadoMasRepetido(desde, hasta);

            Assert.NotNull(resultado);
            Assert.NotEqual("Sin datos", resultado);
            Assert.Contains("veces", resultado);
        }

        /// <summary>
        /// prueba: promedio de goles en rango completo, esperar un resultado con "goles por partido
        /// </summary>
        [Fact]
        public void PromedioGoles_RangoCompleto_RetornaResultado()
        {
            DateTime desde = new DateTime(2026, 6, 11);
            DateTime hasta = new DateTime(2026, 7, 19, 23, 59, 59);

            string resultado = _controller.PromedioGoles(desde, hasta);

            Assert.NotNull(resultado);
            Assert.Contains("goles por partido", resultado);
        }

        /// <summary>
        /// prueba: Promedio de goles con rango sin partidos, esperar un "Sin datos" porque no hay partidos en ese rango
        /// </summary>
        [Fact]
        public void PromedioGoles_RangoSinPartidos_RetornaSinDatos()
        {
            DateTime desde = new DateTime(2025, 1, 1);
            DateTime hasta = new DateTime(2025, 12, 31);

            string resultado = _controller.PromedioGoles(desde, hasta);

            Assert.Equal("Sin datos", resultado);
        }

        /// <summary>
        /// prueba: partido con más aciertos en rango completo, esperar un resultado distinto de "Sin datos"
        /// </summary>
        [Fact]
        public void PartidoConMasAciertos_RangoCompleto_RetornaResultado()
        {
            DateTime desde = new DateTime(2026, 6, 11);
            DateTime hasta = new DateTime(2026, 7, 19, 23, 59, 59);

            string resultado = _controller.PartidoConMasAciertos(desde, hasta);

            Assert.NotNull(resultado);
            Assert.NotEqual("Sin datos", resultado);
        }

        /// <summary>
        /// prueba: usuario con más aciertos en rango completo, esperar un resultado distinto de "Sin datos"
        /// </summary>
        [Fact]
        public void UsuarioConMasAciertos_RangoCompleto_RetornaResultado()
        {
            DateTime desde = new DateTime(2026, 6, 11);
            DateTime hasta = new DateTime(2026, 7, 19, 23, 59, 59);

            string resultado = _controller.UsuarioConMasAciertos(desde, hasta);

            Assert.NotNull(resultado);
            Assert.NotEqual("Sin datos", resultado);
        }

        /// <summary>
        /// prueba: partido con más pronósticos en rango completo, esperar un resultado distinto de "Sin datos"
        /// </summary>
        [Fact]
        public void PartidoConMasPronosticos_RangoCompleto_RetornaResultado()
        {
            DateTime desde = new DateTime(2026, 6, 11);
            DateTime hasta = new DateTime(2026, 7, 19, 23, 59, 59);

            string resultado = _controller.PartidoConMasPronosticos(desde, hasta);

            Assert.NotNull(resultado);
            Assert.NotEqual("Sin datos", resultado);
        }

        /// <summary>
        /// prueba: equipo sorpresa en rango completo, esperar un resultado distinto de "Sin datos"
        /// </summary>
        [Fact]
        public void EquipoSorpresa_RangoCompleto_RetornaResultado()
        {
            DateTime desde = new DateTime(2026, 6, 11);
            DateTime hasta = new DateTime(2026, 7, 19, 23, 59, 59);

            string resultado = _controller.EquipoSorpresa(desde, hasta);

            Assert.NotNull(resultado);
            Assert.NotEqual("Sin datos", resultado);
        }
    }
}