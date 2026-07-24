namespace LaPachangaDelMundial.Views
{
    partial class PronosticoForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPronosticar = new System.Windows.Forms.TabPage();
            this.lstPartidosPendientes = new System.Windows.Forms.ListBox();
            this.lblLocal = new System.Windows.Forms.Label();
            this.lblVs = new System.Windows.Forms.Label();
            this.lblVisitante = new System.Windows.Forms.Label();
            this.numGolesLocal = new System.Windows.Forms.NumericUpDown();
            this.numGolesVisitante = new System.Windows.Forms.NumericUpDown();
            this.btnPronosticar = new System.Windows.Forms.Button();
            this.tabHistorial = new System.Windows.Forms.TabPage();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.tabRanking = new System.Windows.Forms.TabPage();
            this.cboQuiniela = new System.Windows.Forms.ComboBox();
            this.dgvRanking = new System.Windows.Forms.DataGridView();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPronosticar.SuspendLayout();
            this.SuspendLayout();

            // tabControl
            this.tabControl.Location = new System.Drawing.Point(10, 10);
            this.tabControl.Size = new System.Drawing.Size(860, 480);
            this.tabControl.Controls.Add(this.tabPronosticar);
            this.tabControl.Controls.Add(this.tabHistorial);
            this.tabControl.Controls.Add(this.tabRanking);
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);

            // tabPronosticar
            this.tabPronosticar.Text = "Pronosticar";
            this.tabPronosticar.Controls.Add(this.lstPartidosPendientes);
            this.tabPronosticar.Controls.Add(this.lblLocal);
            this.tabPronosticar.Controls.Add(this.numGolesLocal);
            this.tabPronosticar.Controls.Add(this.lblVs);
            this.tabPronosticar.Controls.Add(this.numGolesVisitante);
            this.tabPronosticar.Controls.Add(this.lblVisitante);
            this.tabPronosticar.Controls.Add(this.btnPronosticar);

            // lstPartidosPendientes
            this.lstPartidosPendientes.Location = new System.Drawing.Point(10, 10);
            this.lstPartidosPendientes.Size = new System.Drawing.Size(500, 380);
            this.lstPartidosPendientes.Font = new System.Drawing.Font("Courier New", 9F);
            this.lstPartidosPendientes.SelectedIndexChanged += new System.EventHandler(this.lstPartidosPendientes_SelectedIndexChanged);

            // lblLocal
            this.lblLocal.Text = "-";
            this.lblLocal.Location = new System.Drawing.Point(530, 60);
            this.lblLocal.Size = new System.Drawing.Size(150, 20);
            this.lblLocal.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);

            // numGolesLocal
            this.numGolesLocal.Location = new System.Drawing.Point(530, 90);
            this.numGolesLocal.Size = new System.Drawing.Size(60, 20);
            this.numGolesLocal.Minimum = 0;
            this.numGolesLocal.Maximum = 20;

            // lblVs
            this.lblVs.Text = "VS";
            this.lblVs.Location = new System.Drawing.Point(600, 90);
            this.lblVs.Size = new System.Drawing.Size(30, 20);
            this.lblVs.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);

            // numGolesVisitante
            this.numGolesVisitante.Location = new System.Drawing.Point(640, 90);
            this.numGolesVisitante.Size = new System.Drawing.Size(60, 20);
            this.numGolesVisitante.Minimum = 0;
            this.numGolesVisitante.Maximum = 20;

            // lblVisitante
            this.lblVisitante.Text = "-";
            this.lblVisitante.Location = new System.Drawing.Point(530, 120);
            this.lblVisitante.Size = new System.Drawing.Size(150, 20);
            this.lblVisitante.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);

            // btnPronosticar
            this.btnPronosticar.Text = "Guardar pronóstico";
            this.btnPronosticar.Location = new System.Drawing.Point(530, 170);
            this.btnPronosticar.Size = new System.Drawing.Size(150, 35);
            this.btnPronosticar.Enabled = false;
            this.btnPronosticar.Click += new System.EventHandler(this.btnPronosticar_Click);

            // tabHistorial
            this.tabHistorial.Text = "Mi Historial";
            this.tabHistorial.Controls.Add(this.dgvHistorial);

            // dgvHistorial
            this.dgvHistorial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistorial.ReadOnly = true;
            this.dgvHistorial.AllowUserToAddRows = false;
            this.dgvHistorial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // tabRanking
            this.tabRanking.Text = "Ranking";
            this.tabRanking.Controls.Add(this.cboQuiniela);
            this.tabRanking.Controls.Add(this.dgvRanking);

            // cboQuiniela
            this.cboQuiniela.Location = new System.Drawing.Point(10, 10);
            this.cboQuiniela.Size = new System.Drawing.Size(300, 21);
            this.cboQuiniela.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboQuiniela.SelectedIndexChanged += new System.EventHandler(this.cboQuiniela_SelectedIndexChanged);

            // dgvRanking
            this.dgvRanking.Location = new System.Drawing.Point(10, 40);
            this.dgvRanking.Size = new System.Drawing.Size(830, 390);
            this.dgvRanking.ReadOnly = true;
            this.dgvRanking.AllowUserToAddRows = false;
            this.dgvRanking.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // btnCerrar
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.Location = new System.Drawing.Point(750, 500);
            this.btnCerrar.Size = new System.Drawing.Size(120, 35);
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);

            // pronosticoForm
            this.ClientSize = new System.Drawing.Size(900, 560);
            this.Name = "PronosticoForm";
            this.Text = "Pronósticos";
            this.Load += new System.EventHandler(this.PronosticoForm_Load);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnCerrar);
            this.tabControl.ResumeLayout(false);
            this.tabPronosticar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPronosticar;
        private System.Windows.Forms.ListBox lstPartidosPendientes;
        private System.Windows.Forms.Label lblLocal;
        private System.Windows.Forms.Label lblVs;
        private System.Windows.Forms.Label lblVisitante;
        private System.Windows.Forms.NumericUpDown numGolesLocal;
        private System.Windows.Forms.NumericUpDown numGolesVisitante;
        private System.Windows.Forms.Button btnPronosticar;
        private System.Windows.Forms.TabPage tabHistorial;
        private System.Windows.Forms.DataGridView dgvHistorial;
        private System.Windows.Forms.TabPage tabRanking;
        private System.Windows.Forms.ComboBox cboQuiniela;
        private System.Windows.Forms.DataGridView dgvRanking;
        private System.Windows.Forms.Button btnCerrar;
    }
}