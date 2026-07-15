using System;
using System.Windows.Forms;
using LaPachangaDelMundial.Controllers;
using LaPachangaDelMundial.Models;

namespace LaPachangaDelMundial.Vistas
{
    public partial class LoginForm : Form
    {
        private UsuarioController _usuarioController;

        public Usuario UsuarioActivo { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            _usuarioController = new UsuarioController();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor completá todos los campos.",
                    "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Usuario usuario = _usuarioController.Login(nombreUsuario, contrasena);

            if (usuario == null)
            {
                MessageBox.Show("Usuario o contraseña incorrectos.",
                    "Error de acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UsuarioActivo = usuario;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            // Por ahora mostramos un mensaje — aquí irá el RegistroForm
            MessageBox.Show("Función de registro próximamente.",
                "En construcción", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
