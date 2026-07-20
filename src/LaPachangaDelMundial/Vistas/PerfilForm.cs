using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LaPachangaDelMundial.Models;
using LaPachangaDelMundial.Utils;

namespace LaPachangaDelMundial.Views
{
    public partial class PerfilForm : Form
    {
        private Usuario _usuario;
        private List<Seleccion> _selecciones;

        public PerfilForm(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            _selecciones = JsonLoader.CargarSelecciones();
        }

        private void PerfilForm_Load(object sender, EventArgs e)
        {
            // se encarga de buscar el nombre del país prefereido
            Seleccion pais = _selecciones.Find(
                s => s.Codigo == _usuario.CodigoPaisPreferido);

            string nombrePais = pais != null ? pais.Nombre : _usuario.CodigoPaisPreferido;

            // muestra los dartos del ususario
            lblUsuarioVal.Text = _usuario.NombreUsuario;
            lblPaisVal.Text = nombrePais;
            lblPuntosVal.Text = _usuario.Puntos.ToString();

            if (_usuario.Insignias.Count > 0)
                lblInsigniasVal.Text = string.Join(", ", _usuario.Insignias);
            else
                lblInsigniasVal.Text = "Sin insignias aún";
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}