namespace LaPachangaDelMundial.Views
{
    partial class PartidosForm
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
            this.tabUltimos = new System.Windows.Forms.TabPage();
            this.lstUltimos = new System.Windows.Forms.ListBox();
            this.tabProximos = new System.Windows.Forms.TabPage();
            this.lstProximos = new System.Windows.Forms.ListBox();
            this.tabGrupos = new System.Windows.Forms.TabPage();
            this.cboGrupo = new System.Windows.Forms.ComboBox();
            this.dgvTabla = new System.Windows.Forms.DataGridView();
            this.tabEliminatoria = new System.Windows.Forms.TabPage();
            this.lstEliminatoria = new System.Windows.Forms.ListBox();
            this.lblFechaSimulada = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabGrupos.SuspendLayout();
            this.SuspendLayout();

            // tabControl
            this.tabControl.Location = new System.Drawing.Point(10, 40);
            this.tabControl.Size = new System.Drawing.Size(860, 450);
            this.tabControl.Controls.Add(this.tabUltimos);
            this.tabControl.Controls.Add(this.tabProximos);
            this.tabControl.Controls.Add(this.tabGrupos);
            this.tabControl.Controls.Add(this.tabEliminatoria);
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);

            // tabUltimos
            this.tabUltimos.Text = "Últimos 5";
            this.tabUltimos.Controls.Add(this.lstUltimos);

            // lstUltimos
            this.lstUltimos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUltimos.Font = new System.Drawing.Font("Courier New", 9F);

            // tabProximos
            this.tabProximos.Text = "Próximos 24h";
            this.tabProximos.Controls.Add(this.lstProximos);

            // lstProximos
            this.lstProximos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstProximos.Font = new System.Drawing.Font("Courier New", 9F);

            // tabGrupos
            this.tabGrupos.Text = "Tabla de Grupos";
            this.tabGrupos.Controls.Add(this.dgvTabla);
            this.tabGrupos.Controls.Add(this.cboGrupo);

            // cboGrupo
            this.cboGrupo.Location = new System.Drawing.Point(10, 10);
            this.cboGrupo.Size = new System.Drawing.Size(100, 21);
            this.cboGrupo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGrupo.SelectedIndexChanged += new System.EventHandler(this.cboGrupo_SelectedIndexChanged);

            // dgvTabla
            this.dgvTabla.Location = new System.Drawing.Point(10, 40);
            this.dgvTabla.Size = new System.Drawing.Size(830, 360);
            this.dgvTabla.ReadOnly = true;
            this.dgvTabla.AllowUserToAddRows = false;
            this.dgvTabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // tabEliminatoria
            this.tabEliminatoria.Text = "Fase Eliminatoria";
            this.tabEliminatoria.Controls.Add(this.lstEliminatoria);

            // lstEliminatoria
            this.lstEliminatoria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstEliminatoria.Font = new System.Drawing.Font("Courier New", 9F);

            // lblFechaSimulada
            this.lblFechaSimulada.Text = "Fecha simulada:";
            this.lblFechaSimulada.Location = new System.Drawing.Point(10, 10);
            this.lblFechaSimulada.Size = new System.Drawing.Size(400, 20);
            this.lblFechaSimulada.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Italic);

            // btnCerrar
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.Location = new System.Drawing.Point(750, 500);
            this.btnCerrar.Size = new System.Drawing.Size(120, 35);
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);

            // PartidosForm
            this.ClientSize = new System.Drawing.Size(900, 560);
            this.Name = "PartidosForm";
            this.Text = "Partidos del Mundial 2026";
            this.Load += new System.EventHandler(this.PartidosForm_Load);
            this.Controls.Add(this.lblFechaSimulada);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnCerrar);
            this.tabControl.ResumeLayout(false);
            this.tabGrupos.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabUltimos;
        private System.Windows.Forms.ListBox lstUltimos;
        private System.Windows.Forms.TabPage tabProximos;
        private System.Windows.Forms.ListBox lstProximos;
        private System.Windows.Forms.TabPage tabGrupos;
        private System.Windows.Forms.ComboBox cboGrupo;
        private System.Windows.Forms.DataGridView dgvTabla;
        private System.Windows.Forms.TabPage tabEliminatoria;
        private System.Windows.Forms.ListBox lstEliminatoria;
        private System.Windows.Forms.Label lblFechaSimulada;
        private System.Windows.Forms.Button btnCerrar;
    }
}