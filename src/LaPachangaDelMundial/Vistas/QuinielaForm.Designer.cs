namespace LaPachangaDelMundial.Views
{
    partial class QuinielaForm
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
            this.lblMisQuinielas = new System.Windows.Forms.Label();
            this.lstMisQuinielas = new System.Windows.Forms.ListBox();
            this.lblTimeline = new System.Windows.Forms.Label();
            this.lstTimeline = new System.Windows.Forms.ListBox();
            this.btnCrear = new System.Windows.Forms.Button();
            this.btnUnirse = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblTitulo
            this.lblTitulo.Text = "Gestión de Quinielas";
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(250, 20);
            this.lblTitulo.Size = new System.Drawing.Size(300, 30);

            // lblMisQuinielas
            this.lblMisQuinielas.Text = "Mis Quinielas:";
            this.lblMisQuinielas.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblMisQuinielas.Location = new System.Drawing.Point(30, 70);
            this.lblMisQuinielas.Size = new System.Drawing.Size(150, 20);

            // lstMisQuinielas
            this.lstMisQuinielas.Location = new System.Drawing.Point(30, 95);
            this.lstMisQuinielas.Size = new System.Drawing.Size(300, 150);
            this.lstMisQuinielas.SelectedIndexChanged += new System.EventHandler(this.lstMisQuinielas_SelectedIndexChanged);

            // lblTimeline
            this.lblTimeline.Text = "Timeline de notificaciones:";
            this.lblTimeline.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblTimeline.Location = new System.Drawing.Point(370, 70);
            this.lblTimeline.Size = new System.Drawing.Size(250, 20);

            // lstTimeline
            this.lstTimeline.Location = new System.Drawing.Point(370, 95);
            this.lstTimeline.Size = new System.Drawing.Size(380, 150);

            // btnCrear
            this.btnCrear.Text = "Crear quiniela";
            this.btnCrear.Location = new System.Drawing.Point(30, 270);
            this.btnCrear.Size = new System.Drawing.Size(140, 35);
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);

            // btnUnirse
            this.btnUnirse.Text = "Unirse a quiniela";
            this.btnUnirse.Location = new System.Drawing.Point(190, 270);
            this.btnUnirse.Size = new System.Drawing.Size(140, 35);
            this.btnUnirse.Click += new System.EventHandler(this.btnUnirse_Click);

            // btnCerrar
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.Location = new System.Drawing.Point(630, 270);
            this.btnCerrar.Size = new System.Drawing.Size(120, 35);
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);

            // QuinielaForm
            this.ClientSize = new System.Drawing.Size(800, 340);
            this.Name = "QuinielaForm";
            this.Text = "Quinielas";
            this.Load += new System.EventHandler(this.QuinielaForm_Load);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblMisQuinielas);
            this.Controls.Add(this.lstMisQuinielas);
            this.Controls.Add(this.lblTimeline);
            this.Controls.Add(this.lstTimeline);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.btnUnirse);
            this.Controls.Add(this.btnCerrar);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblMisQuinielas;
        private System.Windows.Forms.ListBox lstMisQuinielas;
        private System.Windows.Forms.Label lblTimeline;
        private System.Windows.Forms.ListBox lstTimeline;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnUnirse;
        private System.Windows.Forms.Button btnCerrar;
    }
}