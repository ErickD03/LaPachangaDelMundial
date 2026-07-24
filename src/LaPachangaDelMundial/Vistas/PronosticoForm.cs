using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LaPachangaDelMundial.Controllers;
using LaPachangaDelMundial.Models;

namespace LaPachangaDelMundial.Views
{
    public partial class PronosticoForm : Form
    {
        private Usuario _usuarioActivo;
        private PronosticoController _pronosticoController;
        private PartidoController _partidoController;
        private UsuarioController _usuarioController;
        private QuinielaController _quinielaController;
        private List<Partido> _partidosPendientes;
        private Partido _partidoSeleccionado;

        public PronosticoForm(
            Usuario usuarioActivo,
            PronosticoController pronosticoController,
            PartidoController partidoController,
            UsuarioController usuarioController,
            QuinielaController quinielaController)
        {
            InitializeComponent();
            _usuarioActivo = usuarioActivo;
            _pronosticoController = pronosticoController;
            _partidoController = partidoController;
            _usuarioController = usuarioController;
            _quinielaController = quinielaController;
        }

        private void PronosticoForm_Load(object sender, EventArgs e)
        {
            CargarPartidosPendientes();
            CargarQuinielasRanking();
        }

        private void CargarPartidosPendientes()
        {
            lstPartidosPendientes.Items.Clear();
            _partidosPendientes = _partidoController
                .ObtenerTodos()
                .FindAll(p => p.Estado == EstadoPartido.Pendiente);

            foreach (Partido p in _partidosPendientes)
            {
                string local = _partidoController.ObtenerNombreSeleccion(p.CodigoLocal);
                string visitante = _partidoController.ObtenerNombreSeleccion(p.CodigoVisitante);
                string yaPronostico = _pronosticoController.YaPronostico(
                    _usuarioActivo.Id, p.Id) ? " ✓" : "";

                lstPartidosPendientes.Items.Add(
                    $"{p.FechaHora:dd/MM HH:mm} | {local} vs {visitante} [{p.Fase}]{yaPronostico}");
            }
        }

        private void lstPartidosPendientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lstPartidosPendientes.SelectedIndex;
            if (index < 0) return;

            _partidoSeleccionado = _partidosPendientes[index];
            string local = _partidoController.ObtenerNombreSeleccion(
                _partidoSeleccionado.CodigoLocal);
            string visitante = _partidoController.ObtenerNombreSeleccion(
                _partidoSeleccionado.CodigoVisitante);

            lblLocal.Text = local;
            lblVisitante.Text = visitante;
            numGolesLocal.Value = 0;
            numGolesVisitante.Value = 0;

            bool yaPronostico = _pronosticoController.YaPronostico(
                _usuarioActivo.Id, _partidoSeleccionado.Id);

            btnPronosticar.Enabled = !yaPronostico;

            if (yaPronostico)
                btnPronosticar.Text = "Ya pronosticaste este partido";
            else
                btnPronosticar.Text = "Guardar pronóstico";
        }

        private void btnPronosticar_Click(object sender, EventArgs e)
        {
            if (_partidoSeleccionado == null) return;

            bool registrado = _pronosticoController.Registrar(
                _usuarioActivo.Id,
                _partidoSeleccionado.Id,
                (int)numGolesLocal.Value,
                (int)numGolesVisitante.Value,
                _partidoSeleccionado.Estado);

            if (registrado)
            {
                string local = _partidoController.ObtenerNombreSeleccion(
                    _partidoSeleccionado.CodigoLocal);
                string visitante = _partidoController.ObtenerNombreSeleccion(
                    _partidoSeleccionado.CodigoVisitante);

                MessageBox.Show(
                    $"Pronóstico guardado:\n{local} {(int)numGolesLocal.Value} - {(int)numGolesVisitante.Value} {visitante}",
                    "Pronóstico registrado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CargarPartidosPendientes();
                btnPronosticar.Enabled = false;
                btnPronosticar.Text = "Ya pronosticaste este partido";
            }
            else
            {
                MessageBox.Show(
                    "No se pudo registrar el pronóstico.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void CargarHistorial()
        {
            dgvHistorial.Rows.Clear();
            dgvHistorial.Columns.Clear();

            dgvHistorial.Columns.Add("Partido", "Partido");
            dgvHistorial.Columns.Add("Pronostico", "Tu pronóstico");
            dgvHistorial.Columns.Add("Resultado", "Resultado real");
            dgvHistorial.Columns.Add("Puntos", "Puntos");

            List<Pronostico> historial = _pronosticoController
                .ObtenerPorUsuario(_usuarioActivo.Id);

            foreach (Pronostico p in historial)
            {
                Partido partido = _partidoController.ObtenerTodos()
                    .Find(pa => pa.Id == p.IdPartido);

                if (partido == null) continue;

                string local = _partidoController.ObtenerNombreSeleccion(partido.CodigoLocal);
                string visitante = _partidoController.ObtenerNombreSeleccion(partido.CodigoVisitante);
                string resultado = partido.Estado == EstadoPartido.Finalizado
                    ? $"{partido.GolesLocal} - {partido.GolesVisitante}"
                    : "Pendiente";

                dgvHistorial.Rows.Add(
                    $"{local} vs {visitante}",
                    $"{p.GolesLocal} - {p.GolesVisitante}",
                    resultado,
                    partido.Estado == EstadoPartido.Finalizado
                        ? p.PuntosObtenidos.ToString()
                        : "-"
                );
            }
        }

        private void CargarQuinielasRanking()
        {
            cboQuiniela.Items.Clear();
            cboQuiniela.Items.Add("Ranking Global");

            List<Quiniela> misQuinielas = _quinielaController
                .ObtenerPorUsuario(_usuarioActivo.Id);

            foreach (Quiniela q in misQuinielas)
                cboQuiniela.Items.Add(q.Nombre);

            cboQuiniela.SelectedIndex = 0;
        }

        private void CargarRanking(int indice)
        {
            dgvRanking.Rows.Clear();
            dgvRanking.Columns.Clear();

            dgvRanking.Columns.Add("Pos", "#");
            dgvRanking.Columns.Add("Usuario", "Usuario");
            dgvRanking.Columns.Add("Puntos", "Puntos");

            List<Usuario> todosUsuarios = _usuarioController.ObtenerTodos();
            List<RankingItem> ranking;

            if (indice == 0)
            {
                ranking = _pronosticoController.ObtenerRankingGlobal(todosUsuarios);
            }
            else
            {
                List<Quiniela> misQuinielas = _quinielaController
                    .ObtenerPorUsuario(_usuarioActivo.Id);
                Quiniela quiniela = misQuinielas[indice - 1];
                ranking = _pronosticoController.ObtenerRankingQuiniela(
                    quiniela, todosUsuarios);
            }

            for (int i = 0; i < ranking.Count; i++)
                dgvRanking.Rows.Add(i + 1, ranking[i].NombreUsuario, ranking[i].Puntos);
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0: CargarPartidosPendientes(); break;
                case 1: CargarHistorial(); break;
                case 2: CargarRanking(cboQuiniela.SelectedIndex); break;
            }
        }

        private void cboQuiniela_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarRanking(cboQuiniela.SelectedIndex);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}