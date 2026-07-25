using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LaPachangaDelMundial.Controllers;
using LaPachangaDelMundial.Models;
using LaPachangaDelMundial.Utils;

namespace LaPachangaDelMundial.Views
{
    public partial class EstadisticasForm : Form
    {
        private EstadisticasController _estadisticasController;

        public EstadisticasForm(
            List<Partido> partidos,
            List<Usuario> usuarios,
            List<Pronostico> pronosticos,
            List<Seleccion> selecciones)
        {
            InitializeComponent();
            _estadisticasController = new EstadisticasController(
                partidos, usuarios, pronosticos, selecciones);
        }

        private void EstadisticasForm_Load(object sender, EventArgs e)
        {
            // calcula con el rango defecto al abrir //
            Calcular();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            Calcular();
        }

        private void Calcular()
        {
            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date.AddDays(1).AddSeconds(-1);

            if (desde > hasta)
            {
                MessageBox.Show("La fecha 'Desde' no puede ser mayor que 'Hasta'.",
                    "Fechas inválidas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblMasApostadoVal.Text = _estadisticasController.EquipoMasApostado(desde, hasta);
            lblResultadoVal.Text = _estadisticasController.ResultadoMasRepetido(desde, hasta);
            lblMasAciertosVal.Text = _estadisticasController.PartidoConMasAciertos(desde, hasta);
            lblUsuarioAciertosVal.Text = _estadisticasController.UsuarioConMasAciertos(desde, hasta);
            lblMasPronosticosVal.Text = _estadisticasController.PartidoConMasPronosticos(desde, hasta);
            lblPromedioVal.Text = _estadisticasController.PromedioGoles(desde, hasta);
            lblSorpresaVal.Text = _estadisticasController.EquipoSorpresa(desde, hasta);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}