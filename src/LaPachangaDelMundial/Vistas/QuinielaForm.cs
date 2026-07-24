using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LaPachangaDelMundial.Controllers;
using LaPachangaDelMundial.Models;

namespace LaPachangaDelMundial.Views
{
    public partial class QuinielaForm : Form
    {
        private Usuario _usuarioActivo;
        private QuinielaController _quinielaController;
        private List<Quiniela> _misQuinielas;

        public QuinielaForm(Usuario usuarioActivo, QuinielaController quinielaController)
        {
            InitializeComponent();
            _usuarioActivo = usuarioActivo;
            _quinielaController = quinielaController;
        }

        private void QuinielaForm_Load(object sender, EventArgs e)
        {
            CargarMisQuinielas();
        }

        private void CargarMisQuinielas()
        {
            _misQuinielas = _quinielaController.ObtenerPorUsuario(_usuarioActivo.Id);
            lstMisQuinielas.Items.Clear();

            foreach (var q in _misQuinielas)
            {
                string tipo = q.Tipo == TipoQuiniela.Privada ? "[Privada]" : "[Pública]";
                lstMisQuinielas.Items.Add($"{tipo} {q.Nombre} ({q.IdsIntegrantes.Count} integrantes)");
            }

            lstTimeline.Items.Clear();
        }

        private void lstMisQuinielas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lstMisQuinielas.SelectedIndex;
            if (index < 0) return;

            Quiniela seleccionada = _misQuinielas[index];
            lstTimeline.Items.Clear();

            foreach (string notif in seleccionada.Notificaciones)
                lstTimeline.Items.Add(notif);
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            string nombre = Microsoft.VisualBasic.Interaction.InputBox(
                "Nombre de la quiniela:", "Crear quiniela", "");

            if (string.IsNullOrEmpty(nombre)) return;

            DialogResult tipo = MessageBox.Show(
                "¿Querés que sea privada?\n(No = Pública)",
                "Tipo de quiniela",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            TipoQuiniela tipoQuiniela = tipo == DialogResult.Yes
                ? TipoQuiniela.Privada
                : TipoQuiniela.Publica;

            bool creada = _quinielaController.Crear(
                nombre, tipoQuiniela, _usuarioActivo.Id);

            if (creada)
            {
                MessageBox.Show($"Quiniela '{nombre}' creada exitosamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarMisQuinielas();
            }
            else
            {
                MessageBox.Show("Ya existe una quiniela con ese nombre.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUnirse_Click(object sender, EventArgs e)
        {
            var todasLasQuinielas = _quinielaController.ObtenerTodas();
            var disponibles = todasLasQuinielas.FindAll(
                q => !q.IdsIntegrantes.Contains(_usuarioActivo.Id));

            if (disponibles.Count == 0)
            {
                MessageBox.Show("Ya estás en todas las quinielas disponibles.",
                    "Sin quinielas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Mostramos las quinielas disponibles en un selector simple
            string[] opciones = new string[disponibles.Count];
            for (int i = 0; i < disponibles.Count; i++)
            {
                string tipo = disponibles[i].Tipo == TipoQuiniela.Privada
                    ? "[Privada]" : "[Pública]";
                opciones[i] = $"{tipo} {disponibles[i].Nombre}";
            }

            string seleccion = Microsoft.VisualBasic.Interaction.InputBox(
                "Quinielas disponibles:\n" + string.Join("\n", opciones) +
                "\n\nEscribí el nombre exacto de la quiniela:",
                "Unirse a quiniela", "");

            if (string.IsNullOrEmpty(seleccion)) return;

            Quiniela encontrada = disponibles.Find(q => q.Nombre == seleccion);

            if (encontrada == null)
            {
                MessageBox.Show("No se encontró esa quiniela.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool unido = _quinielaController.UnirseAQuiniela(
                encontrada.Id, _usuarioActivo.Id);

            if (unido)
            {
                MessageBox.Show($"Te uniste a '{encontrada.Nombre}' exitosamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarMisQuinielas();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}