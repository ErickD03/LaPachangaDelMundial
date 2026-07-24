using LaPachangaDelMundial.Controllers;
using LaPachangaDelMundial.Models;
using LaPachangaDelMundial.Views;
using System;
using System.Windows.Forms;

namespace LaPachangaDelMundial.Vistas
{
    public partial class LoginForm : Form
    {
        private UsuarioController _usuarioController;

        public Usuario UsuarioActivo { get; private set; }

        public LoginForm(UsuarioController usuarioController)
        {
            InitializeComponent();
            _usuarioController = usuarioController;
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
            RegistroForm registro = new RegistroForm(_usuarioController);

            if (registro.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Ahora podés ingresar con tu nuevo usuario.",
                    "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
