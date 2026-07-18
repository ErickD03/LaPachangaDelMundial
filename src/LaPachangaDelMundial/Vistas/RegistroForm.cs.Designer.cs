namespace LaPachangaDelMundial.Views
{
    partial class RegistroForm
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
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.lblPais = new System.Windows.Forms.Label();
            this.cboPais = new System.Windows.Forms.ComboBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblTitulo
            this.lblTitulo.Text = "Nuevo Usuario";
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(180, 20);
            this.lblTitulo.Size = new System.Drawing.Size(220, 30);

            // lblUsuario
            this.lblUsuario.Text = "Nombre de usuario:";
            this.lblUsuario.Location = new System.Drawing.Point(50, 80);
            this.lblUsuario.Size = new System.Drawing.Size(140, 20);

            // txtUsuario
            this.txtUsuario.Location = new System.Drawing.Point(200, 77);
            this.txtUsuario.Size = new System.Drawing.Size(200, 20);

            // lblContrasena
            this.lblContrasena.Text = "Contraseña:";
            this.lblContrasena.Location = new System.Drawing.Point(50, 130);
            this.lblContrasena.Size = new System.Drawing.Size(140, 20);

            // txtContrasena
            this.txtContrasena.Location = new System.Drawing.Point(200, 127);
            this.txtContrasena.Size = new System.Drawing.Size(200, 20);
            this.txtContrasena.PasswordChar = '*';

            // lblPais
            this.lblPais.Text = "País preferido:";
            this.lblPais.Location = new System.Drawing.Point(50, 180);
            this.lblPais.Size = new System.Drawing.Size(140, 20);

            // cboPais
            this.cboPais.Location = new System.Drawing.Point(200, 177);
            this.cboPais.Size = new System.Drawing.Size(200, 21);
            this.cboPais.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // btnRegistrar
            this.btnRegistrar.Text = "Registrarse";
            this.btnRegistrar.Location = new System.Drawing.Point(150, 240);
            this.btnRegistrar.Size = new System.Drawing.Size(100, 30);
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);

            // btnCancelar
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Location = new System.Drawing.Point(270, 240);
            this.btnCancelar.Size = new System.Drawing.Size(100, 30);
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // RegistroForm
            this.ClientSize = new System.Drawing.Size(500, 320);
            this.Name = "RegistroForm";
            this.Text = "Registro de Usuario";
            this.Load += new System.EventHandler(this.RegistroForm_Load);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.lblContrasena);
            this.Controls.Add(this.txtContrasena);
            this.Controls.Add(this.lblPais);
            this.Controls.Add(this.cboPais);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.btnCancelar);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblContrasena;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.Label lblPais;
        private System.Windows.Forms.ComboBox cboPais;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Button btnCancelar;
    }
}