using System;

namespace LaPachangaDelMundial
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnQuinielas = new System.Windows.Forms.Button();

            // btnQuinielas
            this.btnQuinielas.Text = "Mis Quinielas";
            this.btnQuinielas.Location = new System.Drawing.Point(50, 220);
            this.btnQuinielas.Size = new System.Drawing.Size(150, 40);
            this.btnQuinielas.Click += new System.EventHandler(this.btnQuinielas_Click);

            this.lblBienvenida = new System.Windows.Forms.Label();
            this.btnVerPerfil = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblBienvenida
            this.lblBienvenida.Text = "Bienvenido";
            this.lblBienvenida.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblBienvenida.Location = new System.Drawing.Point(250, 30);
            this.lblBienvenida.Size = new System.Drawing.Size(350, 30);

            // btnVerPerfil
            this.btnVerPerfil.Text = "Ver mi perfil";
            this.btnVerPerfil.Location = new System.Drawing.Point(50, 100);
            this.btnVerPerfil.Size = new System.Drawing.Size(150, 40);
            this.btnVerPerfil.Click += new System.EventHandler(this.btnVerPerfil_Click);

            // btnCerrarSesion
            this.btnCerrarSesion.Text = "Cerrar sesión";
            this.btnCerrarSesion.Location = new System.Drawing.Point(50, 160);
            this.btnCerrarSesion.Size = new System.Drawing.Size(150, 40);
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);

            this.btnPartidos = new System.Windows.Forms.Button();

            // btnPartidos
            this.btnPartidos.Text = "Partidos";
            this.btnPartidos.Location = new System.Drawing.Point(50, 280);
            this.btnPartidos.Size = new System.Drawing.Size(150, 40);
            this.btnPartidos.Click += new System.EventHandler(this.btnPartidos_Click);

            this.Controls.Add(this.btnPartidos);

            this.btnPronosticos = new System.Windows.Forms.Button();

            // btnPronosticos
            this.btnPronosticos.Text = "Pronósticos";
            this.btnPronosticos.Location = new System.Drawing.Point(50, 340);
            this.btnPronosticos.Size = new System.Drawing.Size(150, 40);
            this.btnPronosticos.Click += new System.EventHandler(this.btnPronosticos_Click);

            this.Controls.Add(this.btnPronosticos);

            // MainForm
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Name = "MainForm";
            this.Text = "La Pachanga del Mundial";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Controls.Add(this.lblBienvenida);
            this.Controls.Add(this.btnVerPerfil);
            this.Controls.Add(this.btnCerrarSesion);
            this.Controls.Add(this.btnQuinielas);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Button btnVerPerfil;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Button btnQuinielas;
        private System.Windows.Forms.Button btnPartidos;
        private System.Windows.Forms.Button btnPronosticos;
    }
}