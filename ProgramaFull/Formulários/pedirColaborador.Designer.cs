namespace ProgramaFull.Formulários
{
    partial class pedirColaborador
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
            textoObs = new Label();
            buttonOK = new Button();
            labelTesteAgendamento = new Label();
            labelPedirColaborador = new Label();
            colaboradorJanela = new Label();
            btnQuit = new Button();
            panel1 = new Panel();
            panel5 = new Panel();
            caixaTexto = new TextBox();
            panel4 = new Panel();
            panel3 = new Panel();
            panel2 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // textoObs
            // 
            textoObs.Location = new Point(3, 116);
            textoObs.Name = "textoObs";
            textoObs.Size = new Size(276, 53);
            textoObs.TabIndex = 12;
            textoObs.Text = "Antes de preencher com o nome colaborador\r\nconfirme o\r\nnúmero do agendamento";
            textoObs.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // buttonOK
            // 
            buttonOK.AllowDrop = true;
            buttonOK.Location = new Point(101, 201);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 23);
            buttonOK.TabIndex = 10;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // labelTesteAgendamento
            // 
            labelTesteAgendamento.AutoSize = true;
            labelTesteAgendamento.BackColor = SystemColors.HotTrack;
            labelTesteAgendamento.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTesteAgendamento.ForeColor = SystemColors.ControlLightLight;
            labelTesteAgendamento.Location = new Point(21, 95);
            labelTesteAgendamento.Name = "labelTesteAgendamento";
            labelTesteAgendamento.Size = new Size(236, 21);
            labelTesteAgendamento.TabIndex = 8;
            labelTesteAgendamento.Text = "Agendamento nº 387965224";
            labelTesteAgendamento.Visible = false;
            labelTesteAgendamento.Click += labelTesteAgendamento_Click;
            // 
            // labelPedirColaborador
            // 
            labelPedirColaborador.AutoSize = true;
            labelPedirColaborador.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPedirColaborador.Location = new Point(49, 31);
            labelPedirColaborador.Name = "labelPedirColaborador";
            labelPedirColaborador.Size = new Size(175, 42);
            labelPedirColaborador.TabIndex = 7;
            labelPedirColaborador.Text = "Por favor digite o\r\nnome do colaborador";
            labelPedirColaborador.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // colaboradorJanela
            // 
            colaboradorJanela.AutoSize = true;
            colaboradorJanela.Location = new Point(8, 6);
            colaboradorJanela.Name = "colaboradorJanela";
            colaboradorJanela.Size = new Size(126, 15);
            colaboradorJanela.TabIndex = 13;
            colaboradorJanela.Text = "Nome do Colaborador";
            // 
            // btnQuit
            // 
            btnQuit.BackColor = Color.FromArgb(192, 0, 0);
            btnQuit.Font = new Font("Gadugi", 12F, FontStyle.Bold);
            btnQuit.ForeColor = SystemColors.ControlLightLight;
            btnQuit.ImageAlign = ContentAlignment.MiddleLeft;
            btnQuit.Location = new Point(249, 3);
            btnQuit.Name = "btnQuit";
            btnQuit.Size = new Size(25, 25);
            btnQuit.TabIndex = 12;
            btnQuit.Text = "X";
            btnQuit.UseVisualStyleBackColor = false;
            btnQuit.Click += btnQuit_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(colaboradorJanela);
            panel1.Controls.Add(btnQuit);
            panel1.Controls.Add(panel5);
            panel1.Location = new Point(-3, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(286, 28);
            panel1.TabIndex = 11;
            // 
            // panel5
            // 
            panel5.BackColor = SystemColors.ActiveCaptionText;
            panel5.Location = new Point(6, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(395, 3);
            panel5.TabIndex = 14;
            // 
            // caixaTexto
            // 
            caixaTexto.Location = new Point(12, 172);
            caixaTexto.Name = "caixaTexto";
            caixaTexto.Size = new Size(258, 23);
            caixaTexto.TabIndex = 13;
            caixaTexto.TextAlign = HorizontalAlignment.Center;
            caixaTexto.KeyDown += caixaTexto_KeyDown;
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.ActiveCaptionText;
            panel4.Location = new Point(279, -2);
            panel4.Name = "panel4";
            panel4.Size = new Size(3, 235);
            panel4.TabIndex = 17;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ActiveCaptionText;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(3, 235);
            panel3.TabIndex = 16;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaptionText;
            panel2.Location = new Point(-112, 228);
            panel2.Name = "panel2";
            panel2.Size = new Size(395, 3);
            panel2.TabIndex = 15;
            // 
            // pedirColaborador
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(282, 231);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(caixaTexto);
            Controls.Add(textoObs);
            Controls.Add(buttonOK);
            Controls.Add(labelTesteAgendamento);
            Controls.Add(labelPedirColaborador);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "pedirColaborador";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "pedirColaborador";
            Load += caixaTexto_Load;
            Shown += caixaTexto_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label textoObs;
        private Button buttonOK;
        private Label labelTesteAgendamento;
        private Label labelPedirColaborador;
        private Label colaboradorJanela;
        private Button btnQuit;
        private Panel panel1;
        private TextBox caixaTexto;
        private Panel panel4;
        private Panel panel3;
        private Panel panel2;
        private Panel panel5;
    }
}