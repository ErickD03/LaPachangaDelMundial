using LaPachangaDelMundial.Vistas;
using System;
using System.Windows.Forms;

namespace LaPachangaDelMundial
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();

            if (login.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(
                    $"Bienvenido, {login.UsuarioActivo.NombreUsuario}!",
                    "Acceso exitoso");
            }
            else
            {
                // Si cierra el login sin ingresar, cerramos la app
                this.Close();
            }
        }
    }
}