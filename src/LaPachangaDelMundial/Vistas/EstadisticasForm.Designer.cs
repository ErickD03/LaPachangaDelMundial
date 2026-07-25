namespace LaPachangaDelMundial.Views
{
    partial class EstadisticasForm
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
            this.lblDesde = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.lblMasApostado = new System.Windows.Forms.Label();
            this.lblMasApostadoVal = new System.Windows.Forms.Label();
            this.lblResultado = new System.Windows.Forms.Label();
            this.lblResultadoVal = new System.Windows.Forms.Label();
            this.lblMasAciertos = new System.Windows.Forms.Label();
            this.lblMasAciertosVal = new System.Windows.Forms.Label();
            this.lblUsuarioAciertos = new System.Windows.Forms.Label();
            this.lblUsuarioAciertosVal = new System.Windows.Forms.Label();
            this.lblMasPronosticos = new System.Windows.Forms.Label();
            this.lblMasPronosticosVal = new System.Windows.Forms.Label();
            this.lblPromedio = new System.Windows.Forms.Label();
            this.lblPromedioVal = new System.Windows.Forms.Label();
            this.lblSorpresa = new System.Windows.Forms.Label();
            this.lblSorpresaVal = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblTitulo
            this.lblTitulo.Text = "Estadísticas del Mundial 2026";
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(200, 20);
            this.lblTitulo.Size = new System.Drawing.Size(400, 30);

            // lblDesde
            this.lblDesde.Text = "Desde:";
            this.lblDesde.Location = new System.Drawing.Point(30, 70);
            this.lblDesde.Size = new System.Drawing.Size(60, 20);

            // dtpDesde
            this.dtpDesde.Location = new System.Drawing.Point(100, 67);
            this.dtpDesde.Size = new System.Drawing.Size(180, 20);
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Value = new System.DateTime(2026, 6, 11);

            // lblHasta
            this.lblHasta.Text = "Hasta:";
            this.lblHasta.Location = new System.Drawing.Point(300, 70);
            this.lblHasta.Size = new System.Drawing.Size(60, 20);

            // dtpHasta
            this.dtpHasta.Location = new System.Drawing.Point(370, 67);
            this.dtpHasta.Size = new System.Drawing.Size(180, 20);
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Value = new System.DateTime(2026, 7, 5);

            // btnCalcular
            this.btnCalcular.Text = "Calcular";
            this.btnCalcular.Location = new System.Drawing.Point(570, 63);
            this.btnCalcular.Size = new System.Drawing.Size(100, 28);
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);

            // Etiquetas estáticas y dinámicas
            int y = 120;
            int espaciado = 45;

            this.lblMasApostado.Text = "Equipo más apostado:";
            this.lblMasApostado.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblMasApostado.Location = new System.Drawing.Point(30, y);
            this.lblMasApostado.Size = new System.Drawing.Size(200, 20);
            this.lblMasApostadoVal.Text = "-";
            this.lblMasApostadoVal.Location = new System.Drawing.Point(240, y);
            this.lblMasApostadoVal.Size = new System.Drawing.Size(400, 20);
            y += espaciado;

            this.lblResultado.Text = "Resultado más repetido:";
            this.lblResultado.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblResultado.Location = new System.Drawing.Point(30, y);
            this.lblResultado.Size = new System.Drawing.Size(200, 20);
            this.lblResultadoVal.Text = "-";
            this.lblResultadoVal.Location = new System.Drawing.Point(240, y);
            this.lblResultadoVal.Size = new System.Drawing.Size(400, 20);
            y += espaciado;

            this.lblMasAciertos.Text = "Partido con más aciertos:";
            this.lblMasAciertos.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblMasAciertos.Location = new System.Drawing.Point(30, y);
            this.lblMasAciertos.Size = new System.Drawing.Size(200, 20);
            this.lblMasAciertosVal.Text = "-";
            this.lblMasAciertosVal.Location = new System.Drawing.Point(240, y);
            this.lblMasAciertosVal.Size = new System.Drawing.Size(400, 20);
            y += espaciado;

            this.lblUsuarioAciertos.Text = "Usuario con más aciertos:";
            this.lblUsuarioAciertos.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblUsuarioAciertos.Location = new System.Drawing.Point(30, y);
            this.lblUsuarioAciertos.Size = new System.Drawing.Size(200, 20);
            this.lblUsuarioAciertosVal.Text = "-";
            this.lblUsuarioAciertosVal.Location = new System.Drawing.Point(240, y);
            this.lblUsuarioAciertosVal.Size = new System.Drawing.Size(400, 20);
            y += espaciado;

            this.lblMasPronosticos.Text = "Partido con más pronósticos:";
            this.lblMasPronosticos.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblMasPronosticos.Location = new System.Drawing.Point(30, y);
            this.lblMasPronosticos.Size = new System.Drawing.Size(200, 20);
            this.lblMasPronosticosVal.Text = "-";
            this.lblMasPronosticosVal.Location = new System.Drawing.Point(240, y);
            this.lblMasPronosticosVal.Size = new System.Drawing.Size(400, 20);
            y += espaciado;

            this.lblPromedio.Text = "Promedio de goles:";
            this.lblPromedio.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblPromedio.Location = new System.Drawing.Point(30, y);
            this.lblPromedio.Size = new System.Drawing.Size(200, 20);
            this.lblPromedioVal.Text = "-";
            this.lblPromedioVal.Location = new System.Drawing.Point(240, y);
            this.lblPromedioVal.Size = new System.Drawing.Size(400, 20);
            y += espaciado;

            this.lblSorpresa.Text = "Equipo sorpresa:";
            this.lblSorpresa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblSorpresa.Location = new System.Drawing.Point(30, y);
            this.lblSorpresa.Size = new System.Drawing.Size(200, 20);
            this.lblSorpresaVal.Text = "-";
            this.lblSorpresaVal.Location = new System.Drawing.Point(240, y);
            this.lblSorpresaVal.Size = new System.Drawing.Size(400, 20);

            // btnCerrar
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.Location = new System.Drawing.Point(650, 460);
            this.btnCerrar.Size = new System.Drawing.Size(120, 35);
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);

            // EstadisticasForm
            this.ClientSize = new System.Drawing.Size(800, 520);
            this.Name = "EstadisticasForm";
            this.Text = "Estadísticas del Mundial 2026";
            this.Load += new System.EventHandler(this.EstadisticasForm_Load);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblDesde);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.lblHasta);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.lblMasApostado);
            this.Controls.Add(this.lblMasApostadoVal);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.lblResultadoVal);
            this.Controls.Add(this.lblMasAciertos);
            this.Controls.Add(this.lblMasAciertosVal);
            this.Controls.Add(this.lblUsuarioAciertos);
            this.Controls.Add(this.lblUsuarioAciertosVal);
            this.Controls.Add(this.lblMasPronosticos);
            this.Controls.Add(this.lblMasPronosticosVal);
            this.Controls.Add(this.lblPromedio);
            this.Controls.Add(this.lblPromedioVal);
            this.Controls.Add(this.lblSorpresa);
            this.Controls.Add(this.lblSorpresaVal);
            this.Controls.Add(this.btnCerrar);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.Label lblMasApostado;
        private System.Windows.Forms.Label lblMasApostadoVal;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.Label lblResultadoVal;
        private System.Windows.Forms.Label lblMasAciertos;
        private System.Windows.Forms.Label lblMasAciertosVal;
        private System.Windows.Forms.Label lblUsuarioAciertos;
        private System.Windows.Forms.Label lblUsuarioAciertosVal;
        private System.Windows.Forms.Label lblMasPronosticos;
        private System.Windows.Forms.Label lblMasPronosticosVal;
        private System.Windows.Forms.Label lblPromedio;
        private System.Windows.Forms.Label lblPromedioVal;
        private System.Windows.Forms.Label lblSorpresa;
        private System.Windows.Forms.Label lblSorpresaVal;
        private System.Windows.Forms.Button btnCerrar;
    }
}