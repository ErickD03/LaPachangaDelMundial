using LaPachangaDelMundial.Controllers;
using LaPachangaDelMundial.Models;
using LaPachangaDelMundial.Utils;
using LaPachangaDelMundial.Views;
using LaPachangaDelMundial.Vistas;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LaPachangaDelMundial.Utils;

namespace LaPachangaDelMundial
{
    public partial class MainForm : Form
    {
        private Usuario _usuarioActivo;
        private UsuarioController _usuarioController;
        private QuinielaController _quinielaController;
        private PartidoController _partidoController;
        private PronosticoController _pronosticoController;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _usuarioController = new UsuarioController();
            _quinielaController = new QuinielaController();
            _partidoController = new PartidoController();
            _pronosticoController = new PronosticoController();

            LoginForm login = new LoginForm(_usuarioController);

            if (login.ShowDialog() == DialogResult.OK)
            {
                _usuarioActivo = login.UsuarioActivo;
                lblBienvenida.Text = $"Bienvenido, {_usuarioActivo.NombreUsuario}!";
            }
            else
            {
                this.Close();
            }
        }

        private void btnVerPerfil_Click(object sender, EventArgs e)
        {
            PerfilForm perfil = new PerfilForm(_usuarioActivo);
            perfil.ShowDialog();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                "¿Seguro que quieres cerrar sesión?",
                "Cerrar sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                _usuarioActivo = null;
                MainForm_Load(sender, e);
            }
        
        }

        private void btnQuinielas_Click(object sender, EventArgs e)
        {
            QuinielaForm quinielas = new QuinielaForm(_usuarioActivo, _quinielaController);
            quinielas.ShowDialog();
        }
        private void btnPartidos_Click(object sender, EventArgs e)
        {
            PartidosForm partidos = new PartidosForm(_partidoController);
            partidos.ShowDialog();
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            List<Pronostico> pronosticos = _pronosticoController.ObtenerPorUsuario(null)
                ?? new List<Pronostico>();

            EstadisticasForm estadisticas = new EstadisticasForm(
                _partidoController.ObtenerTodos(),
                _usuarioController.ObtenerTodos(),
                pronosticos,
                JsonLoader.CargarSelecciones());

            estadisticas.ShowDialog();
        }

        private void btnPronosticos_Click(object sender, EventArgs e)
        {
            PronosticoForm pronosticos = new PronosticoForm(
                _usuarioActivo,
                _pronosticoController,
                _partidoController,
                _usuarioController,
                _quinielaController);
            pronosticos.ShowDialog();
        }
    }
}