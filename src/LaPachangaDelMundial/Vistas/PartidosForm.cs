using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LaPachangaDelMundial.Controllers;
using LaPachangaDelMundial.Models;
using LaPachangaDelMundial.Utils;

namespace LaPachangaDelMundial.Views
{
    public partial class PartidosForm : Form
    {
        private PartidoController _partidoController;

        public PartidosForm(PartidoController partidoController)
        {
            InitializeComponent();
            _partidoController = partidoController;
        }

        private void PartidosForm_Load(object sender, EventArgs e)
        {
            _partidoController.ActualizarEstados();

            lblFechaSimulada.Text =
                $"Fecha simulada: {SistemaFecha.FechaActual:dd/MM/yyyy HH:mm} " +
                $"(Fase actual: Octavos de Final)";

            CargarUltimos5();
            CargarGrupos();
        }

        private void CargarUltimos5()
        {
            lstUltimos.Items.Clear();
            List<Partido> ultimos = _partidoController.ObtenerUltimos5();

            foreach (Partido p in ultimos)
            {
                string local = _partidoController.ObtenerNombreSeleccion(p.CodigoLocal);
                string visitante = _partidoController.ObtenerNombreSeleccion(p.CodigoVisitante);
                lstUltimos.Items.Add(
                    $"{p.FechaHora:dd/MM} | {local,-20} {p.GolesLocal} - {p.GolesVisitante,-2} {visitante}");
            }
        }

        private void CargarProximos24()
        {
            lstProximos.Items.Clear();
            List<Partido> proximos = _partidoController.ObtenerProximos24Horas();

            if (proximos.Count == 0)
            {
                lstProximos.Items.Add("No hay partidos en las próximas 24 horas.");
                return;
            }

            foreach (Partido p in proximos)
            {
                string local = _partidoController.ObtenerNombreSeleccion(p.CodigoLocal);
                string visitante = _partidoController.ObtenerNombreSeleccion(p.CodigoVisitante);
                lstProximos.Items.Add(
                    $"{p.FechaHora:dd/MM HH:mm} | {local} vs {visitante} [{p.Fase}]");
            }
        }

        private void CargarGrupos()
        {
            cboGrupo.Items.Clear();
            string[] grupos = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };
            foreach (string g in grupos)
                cboGrupo.Items.Add($"Grupo {g}");

            cboGrupo.SelectedIndex = 0;
        }

        private void CargarTablaGrupo(string grupo)
        {
            List<PosicionGrupo> tabla = _partidoController.CalcularTablaGrupo(grupo);
            dgvTabla.Rows.Clear();
            dgvTabla.Columns.Clear();

            dgvTabla.Columns.Add("Pos", "#");
            dgvTabla.Columns.Add("Equipo", "Equipo");
            dgvTabla.Columns.Add("PJ", "PJ");
            dgvTabla.Columns.Add("G", "G");
            dgvTabla.Columns.Add("E", "E");
            dgvTabla.Columns.Add("P", "P");
            dgvTabla.Columns.Add("GF", "GF");
            dgvTabla.Columns.Add("GC", "GC");
            dgvTabla.Columns.Add("DG", "DG");
            dgvTabla.Columns.Add("Pts", "Pts");

            for (int i = 0; i < tabla.Count; i++)
            {
                PosicionGrupo pos = tabla[i];
                dgvTabla.Rows.Add(
                    i + 1,
                    pos.Nombre,
                    pos.PartidosJugados,
                    pos.Ganados,
                    pos.Empatados,
                    pos.Perdidos,
                    pos.GolesFavor,
                    pos.GolesContra,
                    pos.DiferenciaGoles,
                    pos.Puntos
                );
            }
        }

        private void CargarEliminatoria()
        {
            lstEliminatoria.Items.Clear();

            string[] fases = { "Dieciseisavos", "Octavos" };
            foreach (string fase in fases)
            {
                List<Partido> partidos = _partidoController.ObtenerPorFase(fase);
                if (partidos.Count == 0) continue;

                lstEliminatoria.Items.Add($"=== {fase} de Final ===");
                foreach (Partido p in partidos)
                {
                    string local = _partidoController.ObtenerNombreSeleccion(p.CodigoLocal);
                    string visitante = _partidoController.ObtenerNombreSeleccion(p.CodigoVisitante);

                    if (p.Estado == EstadoPartido.Finalizado)
                        lstEliminatoria.Items.Add(
                            $"{local,-20} {p.GolesLocal} - {p.GolesVisitante,-2} {visitante}");
                    else
                        lstEliminatoria.Items.Add(
                            $"{local} vs {visitante} [{p.Estado}]");
                }
                lstEliminatoria.Items.Add("");
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0: CargarUltimos5(); break;
                case 1: CargarProximos24(); break;
                case 2:
                    if (cboGrupo.SelectedIndex >= 0)
                    {
                        string grupo = cboGrupo.SelectedItem.ToString().Replace("Grupo ", "");
                        CargarTablaGrupo(grupo);
                    }
                    break;
                case 3: CargarEliminatoria(); break;
            }
        }

        private void cboGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboGrupo.SelectedIndex < 0) return;
            string grupo = cboGrupo.SelectedItem.ToString().Replace("Grupo ", "");
            CargarTablaGrupo(grupo);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}