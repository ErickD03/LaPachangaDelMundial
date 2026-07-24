using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LaPachangaDelMundial.Controllers;
using LaPachangaDelMundial.Models;
using LaPachangaDelMundial.Utils;

namespace LaPachangaDelMundial.Views
{
    public partial class RegistroForm : Form
    {
        private UsuarioController _usuarioController;
        private List<Seleccion> _selecciones;

        public RegistroForm(UsuarioController usuarioController)
        {
            InitializeComponent();
            _usuarioController = usuarioController;
            _selecciones = JsonLoader.CargarSelecciones();
        }

        private void RegistroForm_Load(object sender, EventArgs e)
        {
            // se llena con las selecciones
            cboPais.DisplayMember = "Nombre";
            cboPais.ValueMember = "Codigo";
            cboPais.DataSource = _selecciones;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();
            string codigoPais = cboPais.SelectedValue?.ToString();

            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor completar todos los campos.",
                    "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nombreUsuario.Length < 4)
            {
                MessageBox.Show("El nombre de usuario debe tener al menos 4 caracteres.",
                    "Usuario inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (contrasena.Length < 6)
            {
                MessageBox.Show("La contraseña debe tener al menos 6 caracteres.",
                    "Contraseña inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool registrado = _usuarioController.Registrar(
                nombreUsuario, contrasena, codigoPais);

            if (!registrado)
            {
                MessageBox.Show("Nombre de usuario se encuentra en uso. Elegir otro.",
                    "Usuario existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show($"¡Registro exitoso! Ya podés ingresar con tu usuario.",
                "Registro completado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}