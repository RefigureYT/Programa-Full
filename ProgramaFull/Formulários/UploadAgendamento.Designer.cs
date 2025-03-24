namespace ProgramaFull.Formulários
{
    partial class UploadAgendamento
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
            textoSimples = new Label();
            panel1 = new Panel();
            btnPreConferencia = new Button();
            btnEmbalar = new Button();
            btnEncaixotar = new Button();
            btnExpedicao = new Button();
            btnVoltar = new Button();
            panel4 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel5 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // textoSimples
            // 
            textoSimples.AutoSize = true;
            textoSimples.BackColor = SystemColors.ControlLightLight;
            textoSimples.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textoSimples.Location = new Point(0, 8);
            textoSimples.Name = "textoSimples";
            textoSimples.Size = new Size(309, 25);
            textoSimples.TabIndex = 0;
            textoSimples.Text = "Clique na etapa do procedimento";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaptionText;
            panel1.Controls.Add(textoSimples);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(309, 43);
            panel1.TabIndex = 1;
            // 
            // btnPreConferencia
            // 
            btnPreConferencia.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPreConferencia.Location = new Point(66, 88);
            btnPreConferencia.Name = "btnPreConferencia";
            btnPreConferencia.Size = new Size(204, 54);
            btnPreConferencia.TabIndex = 1;
            btnPreConferencia.Text = "Pré Conferência";
            btnPreConferencia.UseVisualStyleBackColor = true;
            btnPreConferencia.Click += btnPreConferencia_Click;
            // 
            // btnEmbalar
            // 
            btnEmbalar.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEmbalar.Location = new Point(66, 148);
            btnEmbalar.Name = "btnEmbalar";
            btnEmbalar.Size = new Size(204, 54);
            btnEmbalar.TabIndex = 0;
            btnEmbalar.Text = "Embalar";
            btnEmbalar.UseVisualStyleBackColor = true;
            btnEmbalar.Click += btnEmbalar_Click;
            // 
            // btnEncaixotar
            // 
            btnEncaixotar.Enabled = false;
            btnEncaixotar.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEncaixotar.Location = new Point(66, 208);
            btnEncaixotar.Name = "btnEncaixotar";
            btnEncaixotar.Size = new Size(204, 54);
            btnEncaixotar.TabIndex = 0;
            btnEncaixotar.TabStop = false;
            btnEncaixotar.Text = "Encaixotar";
            btnEncaixotar.UseVisualStyleBackColor = true;
            btnEncaixotar.Click += btnEncaixotar_Click;
            // 
            // btnExpedicao
            // 
            btnExpedicao.Enabled = false;
            btnExpedicao.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExpedicao.Location = new Point(66, 268);
            btnExpedicao.Name = "btnExpedicao";
            btnExpedicao.Size = new Size(204, 54);
            btnExpedicao.TabIndex = 0;
            btnExpedicao.TabStop = false;
            btnExpedicao.Text = "Expedição";
            btnExpedicao.UseVisualStyleBackColor = true;
            btnExpedicao.Click += btnExpedicao_Click;
            // 
            // btnVoltar
            // 
            btnVoltar.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVoltar.Location = new Point(12, 342);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(86, 28);
            btnVoltar.TabIndex = 2;
            btnVoltar.TabStop = false;
            btnVoltar.Text = "Voltar";
            btnVoltar.UseVisualStyleBackColor = true;
            btnVoltar.Click += btnVoltar_Click;
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.ActiveCaptionText;
            panel4.Location = new Point(-14, 372);
            panel4.Name = "panel4";
            panel4.Size = new Size(350, 10);
            panel4.TabIndex = 18;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaptionText;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(350, 10);
            panel2.TabIndex = 16;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ActiveCaptionText;
            panel3.Location = new Point(323, 6);
            panel3.Name = "panel3";
            panel3.Size = new Size(10, 400);
            panel3.TabIndex = 17;
            // 
            // panel5
            // 
            panel5.BackColor = SystemColors.ActiveCaptionText;
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(10, 400);
            panel5.TabIndex = 15;
            // 
            // UploadAgendamento
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(333, 382);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(panel3);
            Controls.Add(panel5);
            Controls.Add(btnVoltar);
            Controls.Add(btnExpedicao);
            Controls.Add(btnEncaixotar);
            Controls.Add(btnEmbalar);
            Controls.Add(btnPreConferencia);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "UploadAgendamento";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "UploadAgendamento";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label textoSimples;
        private Panel panel1;
        private Button btnPreConferencia;
        private Button btnEmbalar;
        private Button btnEncaixotar;
        private Button btnExpedicao;
        private Button btnVoltar;
        private Panel panel4;
        private Panel panel2;
        private Panel panel3;
        private Panel panel5;
    }
}