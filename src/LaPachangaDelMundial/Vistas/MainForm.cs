using LaPachangaDelMundial.Controllers;
using LaPachangaDelMundial.Models;
using LaPachangaDelMundial.Views;
using LaPachangaDelMundial.Vistas;
using System;
using System.Windows.Forms;

namespace LaPachangaDelMundial
{
    public partial class MainForm : Form
    {
        private Usuario _usuarioActivo;
        private UsuarioController _usuarioController;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _usuarioController = new UsuarioController();

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
                "¿Seguro que querés cerrar sesión?",
                "Cerrar sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                _usuarioActivo = null;
                MainForm_Load(sender, e);
            }
        }
    }
}