using System;
using System.Windows.Forms;
using LaPachangaDelMundial.Utils;

namespace LaPachangaDelMundial
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(MainForm_Load);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var selecciones = JsonLoader.CargarSelecciones();
            var partidos = JsonLoader.CargarPartidos();
            var usuarios = JsonLoader.CargarUsuarios();
            var quinielas = JsonLoader.CargarQuinielas();
        }
    }
}