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

            // MainForm
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Name = "MainForm";
            this.Text = "La Pachanga del Mundial";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Controls.Add(this.lblBienvenida);
            this.Controls.Add(this.btnVerPerfil);
            this.Controls.Add(this.btnCerrarSesion);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Button btnVerPerfil;
        private System.Windows.Forms.Button btnCerrarSesion;
    }
}