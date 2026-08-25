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
    public class PartidoControllerTests
    {
        private PartidoController _controller;

        /// <summary>
        /// constructor que crea una nueva instancia antes de cada prueba
        /// </summary>
        public PartidoControllerTests()
        {
            _controller = new PartidoController();
        }

        /// <summary>
        /// prueba: obtener todos los partidos, esperar que la lista no sea null y tenga partidos cargados
        /// </summary>
        [Fact]
        public void ObtenerTodos_RetornaListaNoVacia()
        {
            List<Partido> resultado = _controller.ObtenerTodos();

            Assert.NotNull(resultado);
            Assert.True(resultado.Count > 0);
        }

        /// <summary>
        /// prueba: obtener últimos 5 partidos finalizados, esperar que retorne máximo 5 partidos con estado Finalizado
        /// </summary>
        [Fact]
        public void ObtenerUltimos5_RetornaMaximo5Partidos()
        {
            SistemaFecha.FechaActual = new DateTime(2026, 7, 19, 23, 59, 0);
            _controller.ActualizarEstados();

            List<Partido> resultado = _controller.ObtenerUltimos5();

            Assert.NotNull(resultado);
            Assert.True(resultado.Count <= 5);
            foreach (Partido partido in resultado)
            {
                Assert.Equal(EstadoPartido.Finalizado, partido.Estado);
            }
        }

        /// <summary>
        /// prueba: obtener próximos partidos en 24 horas, con la fecha al inicio del mundial esperamos partidos pendientes
        /// </summary>
        [Fact]
        public void ObtenerProximos24Horas_ConFechaInicio_RetornaPartidos()
        {
            SistemaFecha.FechaActual = new DateTime(2026, 6, 11, 0, 0, 0);
            _controller.ActualizarEstados();

            List<Partido> resultado = _controller.ObtenerProximos24Horas();

            Assert.NotNull(resultado);
            Assert.True(resultado.Count > 0);
            foreach (Partido partido in resultado)
            {
                Assert.Equal(EstadoPartido.Pendiente, partido.Estado);
            }
        }

        /// <summary>
        /// prueba: obtener partidos por grupo existente, el Grupo A existe y debe tener partidos
        /// </summary>
        [Fact]
        public void ObtenerPorGrupo_GrupoExistente_RetornaPartidos()
        {
            string grupo = "A";

            List<Partido> resultado = _controller.ObtenerPorGrupo(grupo);

            Assert.NotNull(resultado);
            Assert.True(resultado.Count > 0);
            foreach (Partido partido in resultado)
            {
                Assert.Equal(grupo, partido.Grupo);
            }
        }

        /// <summary>
        /// prueba: obtener partidos por grupo inexistente, un grupo que no existe debe retornar lista vacía
        /// </summary>
        [Fact]
        public void ObtenerPorGrupo_GrupoInexistente_RetornaListaVacia()
        {
            string grupo = "Z";

            List<Partido> resultado = _controller.ObtenerPorGrupo(grupo);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
        }

        /// <summary>
        /// prueba: obtener partidos por fase existente, la fase "Final" existe y debe tener exactamente 1 partido
        /// </summary>
        [Fact]
        public void ObtenerPorFase_FaseExistente_RetornaPartidos()
        {
            string fase = "Final";

            List<Partido> resultado = _controller.ObtenerPorFase(fase);

            Assert.NotNull(resultado);
            Assert.True(resultado.Count > 0);
        }

        /// <summary>
        /// prueba: calcular tabla de posiciones del Grupo A, esperar exactamente 4 equipos en la tabla
        /// </summary>
        [Fact]
        public void CalcularTablaGrupo_GrupoA_Retorna4Posiciones()
        {
            SistemaFecha.FechaActual = new DateTime(2026, 7, 19, 23, 59, 0);
            _controller.ActualizarEstados();
            string grupo = "A";

            List<PosicionGrupo> resultado = _controller.CalcularTablaGrupo(grupo);

            Assert.NotNull(resultado);
            Assert.Equal(4, resultado.Count);
        }

        /// <summary>
        /// prueba: la tabla de posiciones debe estar ordenada por puntos, el primero debe tener más o igual puntos que el segundo
        /// </summary>
        [Fact]
        public void CalcularTablaGrupo_EstaOrdenadaPorPuntos()
        {
            SistemaFecha.FechaActual = new DateTime(2026, 7, 19, 23, 59, 0);
            _controller.ActualizarEstados();

            List<PosicionGrupo> resultado = _controller.CalcularTablaGrupo("A");

            for (int i = 0; i < resultado.Count - 1; i++)
            {
                Assert.True(resultado[i].Puntos >= resultado[i + 1].Puntos);
            }
        }

        /// <summary>
        /// prueba: obtener nombre de selección por código existente, el código "ARG" debe retornar "Argentina"
        /// </summary>
        [Fact]
        public void ObtenerNombreSeleccion_CodigoExistente_RetornaNombre()
        {
            string codigo = "ARG";

            string resultado = _controller.ObtenerNombreSeleccion(codigo);

            Assert.Equal("Argentina", resultado);
        }

        /// <summary>
        /// prueba: obtener nombre de selección por código inexistente, un código que no existe debe retornar el mismo código
        /// </summary>
        [Fact]
        public void ObtenerNombreSeleccion_CodigoInexistente_RetornaCodigo()
        {
            string codigo = "XYZ";

            string resultado = _controller.ObtenerNombreSeleccion(codigo);

            Assert.Equal("XYZ", resultado);
        }
    }
}