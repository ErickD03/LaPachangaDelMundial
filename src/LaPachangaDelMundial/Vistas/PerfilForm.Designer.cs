namespace LaPachangaDelMundial.Views
{
    partial class PerfilForm
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblUsuarioVal = new System.Windows.Forms.Label();
            this.lblPaisVal = new System.Windows.Forms.Label();
            this.lblPuntosVal = new System.Windows.Forms.Label();
            this.lblInsigniasVal = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblPais = new System.Windows.Forms.Label();
            this.lblPuntos = new System.Windows.Forms.Label();
            this.lblInsignias = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblTitulo
            this.lblTitulo.Text = "Mi Perfil";
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(180, 20);
            this.lblTitulo.Size = new System.Drawing.Size(150, 30);

            // Labels estáticos
            this.lblUsuario.Text = "Usuario:";
            this.lblUsuario.Location = new System.Drawing.Point(50, 80);
            this.lblUsuario.Size = new System.Drawing.Size(120, 20);

            this.lblPais.Text = "País preferido:";
            this.lblPais.Location = new System.Drawing.Point(50, 120);
            this.lblPais.Size = new System.Drawing.Size(120, 20);

            this.lblPuntos.Text = "Puntos:";
            this.lblPuntos.Location = new System.Drawing.Point(50, 160);
            this.lblPuntos.Size = new System.Drawing.Size(120, 20);

            this.lblInsignias.Text = "Insignias:";
            this.lblInsignias.Location = new System.Drawing.Point(50, 200);
            this.lblInsignias.Size = new System.Drawing.Size(120, 20);

            // Labels dinámicos (muestran los datos del usuario)
            this.lblUsuarioVal.Text = "";
            this.lblUsuarioVal.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblUsuarioVal.Location = new System.Drawing.Point(200, 80);
            this.lblUsuarioVal.Size = new System.Drawing.Size(250, 20);

            this.lblPaisVal.Text = "";
            this.lblPaisVal.Location = new System.Drawing.Point(200, 120);
            this.lblPaisVal.Size = new System.Drawing.Size(250, 20);

            this.lblPuntosVal.Text = "";
            this.lblPuntosVal.Location = new System.Drawing.Point(200, 160);
            this.lblPuntosVal.Size = new System.Drawing.Size(250, 20);

            this.lblInsigniasVal.Text = "";
            this.lblInsigniasVal.Location = new System.Drawing.Point(200, 200);
            this.lblInsigniasVal.Size = new System.Drawing.Size(250, 20);

            // btnCerrar
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.Location = new System.Drawing.Point(200, 260);
            this.btnCerrar.Size = new System.Drawing.Size(100, 30);
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);

            // PerfilForm
            this.ClientSize = new System.Drawing.Size(500, 330);
            this.Name = "PerfilForm";
            this.Text = "Mi Perfil";
            this.Load += new System.EventHandler(this.PerfilForm_Load);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.lblPais);
            this.Controls.Add(this.lblPuntos);
            this.Controls.Add(this.lblInsignias);
            this.Controls.Add(this.lblUsuarioVal);
            this.Controls.Add(this.lblPaisVal);
            this.Controls.Add(this.lblPuntosVal);
            this.Controls.Add(this.lblInsigniasVal);
            this.Controls.Add(this.btnCerrar);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblPais;
        private System.Windows.Forms.Label lblPuntos;
        private System.Windows.Forms.Label lblInsignias;
        private System.Windows.Forms.Label lblUsuarioVal;
        private System.Windows.Forms.Label lblPaisVal;
        private System.Windows.Forms.Label lblPuntosVal;
        private System.Windows.Forms.Label lblInsigniasVal;
        private System.Windows.Forms.Button btnCerrar;
    }
}