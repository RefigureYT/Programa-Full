namespace ProgramaFull.Formulários
{
    partial class Configuracoes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            labelEtiqueta = new Label();
            labelRelatorio = new Label();
            impressoraAtualRelatorioVisual = new Label();
            btnSelecionarImpressoraRelatorio = new Button();
            button1 = new Button();
            impressoraAtualEtiquetaVisual = new Label();
            btnVoltar = new Button();
            imprimirPaginaTeste = new Button();
            imprimirPaginaTeste2 = new Button();
            panel4 = new Panel();
            panel3 = new Panel();
            panel2 = new Panel();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.impressoraRelatorio;
            pictureBox1.Location = new Point(22, 21);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(113, 105);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.impressoraEtiqueta;
            pictureBox2.Location = new Point(22, 197);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(113, 105);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // labelEtiqueta
            // 
            labelEtiqueta.AutoSize = true;
            labelEtiqueta.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelEtiqueta.Location = new Point(160, 197);
            labelEtiqueta.Name = "labelEtiqueta";
            labelEtiqueta.Size = new Size(166, 17);
            labelEtiqueta.TabIndex = 6;
            labelEtiqueta.Text = "Impressora etiqueta atual";
            // 
            // labelRelatorio
            // 
            labelRelatorio.AutoSize = true;
            labelRelatorio.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelRelatorio.Location = new Point(160, 21);
            labelRelatorio.Name = "labelRelatorio";
            labelRelatorio.Size = new Size(168, 17);
            labelRelatorio.TabIndex = 7;
            labelRelatorio.Text = "Impressora relatório atual";
            // 
            // impressoraAtualRelatorioVisual
            // 
            impressoraAtualRelatorioVisual.AutoSize = true;
            impressoraAtualRelatorioVisual.Location = new Point(154, 65);
            impressoraAtualRelatorioVisual.Name = "impressoraAtualRelatorioVisual";
            impressoraAtualRelatorioVisual.Size = new Size(186, 15);
            impressoraAtualRelatorioVisual.TabIndex = 8;
            impressoraAtualRelatorioVisual.Text = "Nenhuma impressora selecionada";
            // 
            // btnSelecionarImpressoraRelatorio
            // 
            btnSelecionarImpressoraRelatorio.Location = new Point(182, 103);
            btnSelecionarImpressoraRelatorio.Name = "btnSelecionarImpressoraRelatorio";
            btnSelecionarImpressoraRelatorio.Size = new Size(123, 23);
            btnSelecionarImpressoraRelatorio.TabIndex = 9;
            btnSelecionarImpressoraRelatorio.TabStop = false;
            btnSelecionarImpressoraRelatorio.Text = "Escolher impressora";
            btnSelecionarImpressoraRelatorio.UseVisualStyleBackColor = true;
            btnSelecionarImpressoraRelatorio.Click += btnSelecionarImpressoraRelatorio_Click;
            // 
            // button1
            // 
            button1.Location = new Point(182, 279);
            button1.Name = "button1";
            button1.Size = new Size(123, 23);
            button1.TabIndex = 10;
            button1.TabStop = false;
            button1.Text = "Escolher impressora";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // impressoraAtualEtiquetaVisual
            // 
            impressoraAtualEtiquetaVisual.AutoSize = true;
            impressoraAtualEtiquetaVisual.Location = new Point(154, 239);
            impressoraAtualEtiquetaVisual.Name = "impressoraAtualEtiquetaVisual";
            impressoraAtualEtiquetaVisual.Size = new Size(186, 15);
            impressoraAtualEtiquetaVisual.TabIndex = 11;
            impressoraAtualEtiquetaVisual.Text = "Nenhuma impressora selecionada";
            // 
            // btnVoltar
            // 
            btnVoltar.Location = new Point(387, 323);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(124, 32);
            btnVoltar.TabIndex = 12;
            btnVoltar.TabStop = false;
            btnVoltar.Text = "Voltar";
            btnVoltar.UseVisualStyleBackColor = true;
            btnVoltar.Click += btnVoltar_Click;
            // 
            // imprimirPaginaTeste
            // 
            imprimirPaginaTeste.Location = new Point(387, 285);
            imprimirPaginaTeste.Name = "imprimirPaginaTeste";
            imprimirPaginaTeste.Size = new Size(124, 32);
            imprimirPaginaTeste.TabIndex = 13;
            imprimirPaginaTeste.TabStop = false;
            imprimirPaginaTeste.Text = "Página Teste (R)";
            imprimirPaginaTeste.UseVisualStyleBackColor = true;
            imprimirPaginaTeste.Visible = false;
            imprimirPaginaTeste.Click += imprimirPaginaTeste_Click;
            // 
            // imprimirPaginaTeste2
            // 
            imprimirPaginaTeste2.Location = new Point(387, 247);
            imprimirPaginaTeste2.Name = "imprimirPaginaTeste2";
            imprimirPaginaTeste2.Size = new Size(124, 32);
            imprimirPaginaTeste2.TabIndex = 14;
            imprimirPaginaTeste2.TabStop = false;
            imprimirPaginaTeste2.Text = "Página Teste (E)";
            imprimirPaginaTeste2.UseVisualStyleBackColor = true;
            imprimirPaginaTeste2.Visible = false;
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.ActiveCaptionText;
            panel4.Location = new Point(520, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(3, 400);
            panel4.TabIndex = 18;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ActiveCaptionText;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(3, 400);
            panel3.TabIndex = 17;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaptionText;
            panel2.Location = new Point(0, 364);
            panel2.Name = "panel2";
            panel2.Size = new Size(530, 3);
            panel2.TabIndex = 16;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaptionText;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(530, 3);
            panel1.TabIndex = 15;
            // 
            // Configuracoes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(523, 367);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(imprimirPaginaTeste2);
            Controls.Add(imprimirPaginaTeste);
            Controls.Add(btnVoltar);
            Controls.Add(impressoraAtualEtiquetaVisual);
            Controls.Add(button1);
            Controls.Add(btnSelecionarImpressoraRelatorio);
            Controls.Add(impressoraAtualRelatorioVisual);
            Controls.Add(labelRelatorio);
            Controls.Add(labelEtiqueta);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Configuracoes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Configuracoes";
            Load += Configuracoes_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label labelEtiqueta;
        private Label labelRelatorio;
        private Label impressoraAtualRelatorioVisual;
        private Button btnSelecionarImpressoraRelatorio;
        private Button button1;
        private Label impressoraAtualEtiquetaVisual;
        private Button btnVoltar;
        private Button imprimirPaginaTeste;
        private Button imprimirPaginaTeste2;
        private Panel panel4;
        private Panel panel3;
        private Panel panel2;
        private Panel panel1;
    }
}