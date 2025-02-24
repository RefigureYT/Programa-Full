namespace ProgramaFull.Formulários
{
    partial class pedirEmpresa
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
            labelPedirEmpresa = new Label();
            labelTesteAgendamento = new Label();
            comboBox1 = new ComboBox();
            buttonOK = new Button();
            panel1 = new Panel();
            empresaJanela = new Label();
            btnQuit = new Button();
            textoObs = new Label();
            panel4 = new Panel();
            panel2 = new Panel();
            panel5 = new Panel();
            panel3 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // labelPedirEmpresa
            // 
            labelPedirEmpresa.AutoSize = true;
            labelPedirEmpresa.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPedirEmpresa.Location = new Point(57, 31);
            labelPedirEmpresa.Name = "labelPedirEmpresa";
            labelPedirEmpresa.Size = new Size(156, 42);
            labelPedirEmpresa.TabIndex = 0;
            labelPedirEmpresa.Text = "Por favor selecione\r\na empresa";
            labelPedirEmpresa.TextAlign = ContentAlignment.MiddleCenter;
            labelPedirEmpresa.Click += label1_Click;
            // 
            // labelTesteAgendamento
            // 
            labelTesteAgendamento.BackColor = SystemColors.HotTrack;
            labelTesteAgendamento.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTesteAgendamento.ForeColor = SystemColors.ControlLightLight;
            labelTesteAgendamento.Location = new Point(21, 95);
            labelTesteAgendamento.Name = "labelTesteAgendamento";
            labelTesteAgendamento.Size = new Size(236, 21);
            labelTesteAgendamento.TabIndex = 2;
            labelTesteAgendamento.Text = "Agendamento nº 387965224";
            labelTesteAgendamento.TextAlign = ContentAlignment.MiddleCenter;
            labelTesteAgendamento.Visible = false;
            labelTesteAgendamento.Click += labelTesteAgendamento_Click;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "PESCAJAU", "LTSPORTS", "JAUFISHING" });
            comboBox1.Location = new Point(12, 166);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(252, 29);
            comboBox1.TabIndex = 3;
            // 
            // buttonOK
            // 
            buttonOK.AllowDrop = true;
            buttonOK.Location = new Point(101, 201);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 23);
            buttonOK.TabIndex = 4;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(empresaJanela);
            panel1.Controls.Add(btnQuit);
            panel1.Location = new Point(-3, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(286, 28);
            panel1.TabIndex = 5;
            // 
            // empresaJanela
            // 
            empresaJanela.AutoSize = true;
            empresaJanela.Location = new Point(8, 6);
            empresaJanela.Name = "empresaJanela";
            empresaJanela.Size = new Size(114, 15);
            empresaJanela.TabIndex = 13;
            empresaJanela.Text = "Selecione a empresa";
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
            // textoObs
            // 
            textoObs.AutoSize = true;
            textoObs.Location = new Point(27, 133);
            textoObs.Name = "textoObs";
            textoObs.Size = new Size(216, 30);
            textoObs.TabIndex = 6;
            textoObs.Text = "Antes de colocar a empresa, confirme o\r\nnúmero do agendamento";
            textoObs.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.ActiveCaptionText;
            panel4.Location = new Point(273, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(3, 235);
            panel4.TabIndex = 21;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaptionText;
            panel2.Location = new Point(-121, 235);
            panel2.Name = "panel2";
            panel2.Size = new Size(404, 3);
            panel2.TabIndex = 19;
            // 
            // panel5
            // 
            panel5.BackColor = SystemColors.ActiveCaptionText;
            panel5.Location = new Point(-3, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(404, 3);
            panel5.TabIndex = 18;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ActiveCaptionText;
            panel3.Location = new Point(-9, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(12, 235);
            panel3.TabIndex = 20;
            // 
            // pedirEmpresa
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(276, 238);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(panel5);
            Controls.Add(panel3);
            Controls.Add(textoObs);
            Controls.Add(panel1);
            Controls.Add(buttonOK);
            Controls.Add(comboBox1);
            Controls.Add(labelTesteAgendamento);
            Controls.Add(labelPedirEmpresa);
            FormBorderStyle = FormBorderStyle.None;
            Name = "pedirEmpresa";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Selecione a Empresa";
            Load += pedirEmpresa_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelPedirEmpresa;
        private Label labelTesteAgendamento;
        private ComboBox comboBox1;
        private Button buttonOK;
        private Panel panel1;
        private Label empresaJanela;
        private Button btnQuit;
        private Label textoObs;
        private Panel panel4;
        private Panel panel2;
        private Panel panel5;
        private Panel panel3;
    }
}