namespace ProgramaFull.Formulários
{
    partial class VerAgendamentos
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
            btnVoltar = new Button();
            caixaAg = new GroupBox();
            buttonVer = new Button();
            btnEditar = new Button();
            btnComecar = new Button();
            empresa = new Label();
            labEmpresa = new Label();
            status = new Label();
            labStatus = new Label();
            pictureBox1 = new PictureBox();
            painelAgendamentos = new FlowLayoutPanel();
            panel5 = new Panel();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            panel6 = new Panel();
            panel7 = new Panel();
            panel8 = new Panel();
            comboBoxFiltro = new ComboBox();
            caixaAg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            painelAgendamentos.SuspendLayout();
            SuspendLayout();
            // 
            // btnVoltar
            // 
            btnVoltar.Anchor = AnchorStyles.Left;
            btnVoltar.Location = new Point(14, 421);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(75, 23);
            btnVoltar.TabIndex = 1;
            btnVoltar.TabStop = false;
            btnVoltar.Text = "Voltar";
            btnVoltar.UseVisualStyleBackColor = true;
            btnVoltar.Click += btnVoltar_Click;
            // 
            // caixaAg
            // 
            caixaAg.Anchor = AnchorStyles.None;
            caixaAg.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            caixaAg.Controls.Add(buttonVer);
            caixaAg.Controls.Add(btnEditar);
            caixaAg.Controls.Add(btnComecar);
            caixaAg.Controls.Add(empresa);
            caixaAg.Controls.Add(labEmpresa);
            caixaAg.Controls.Add(status);
            caixaAg.Controls.Add(labStatus);
            caixaAg.Controls.Add(pictureBox1);
            caixaAg.Location = new Point(7, 3);
            caixaAg.Name = "caixaAg";
            caixaAg.Size = new Size(856, 72);
            caixaAg.TabIndex = 2;
            caixaAg.TabStop = false;
            caixaAg.Visible = false;
            caixaAg.Enter += groupBox1_Enter;
            // 
            // buttonVer
            // 
            buttonVer.Location = new Point(552, 28);
            buttonVer.Name = "buttonVer";
            buttonVer.Size = new Size(66, 23);
            buttonVer.TabIndex = 8;
            buttonVer.Text = "Ver";
            buttonVer.UseVisualStyleBackColor = true;
            buttonVer.Click += buttonVer_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(624, 28);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(83, 23);
            btnEditar.TabIndex = 7;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            // 
            // btnComecar
            // 
            btnComecar.Location = new Point(713, 28);
            btnComecar.Name = "btnComecar";
            btnComecar.Size = new Size(109, 23);
            btnComecar.TabIndex = 6;
            btnComecar.Text = "Começar";
            btnComecar.UseVisualStyleBackColor = true;
            // 
            // empresa
            // 
            empresa.AutoSize = true;
            empresa.Font = new Font("MS Reference Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            empresa.Location = new Point(339, 31);
            empresa.Name = "empresa";
            empresa.Size = new Size(82, 16);
            empresa.TabIndex = 5;
            empresa.Text = "PESCAJAU";
            // 
            // labEmpresa
            // 
            labEmpresa.AutoSize = true;
            labEmpresa.Font = new Font("MS Reference Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labEmpresa.Location = new Point(261, 28);
            labEmpresa.Name = "labEmpresa";
            labEmpresa.Size = new Size(86, 20);
            labEmpresa.TabIndex = 4;
            labEmpresa.Text = "Empresa:";
            // 
            // status
            // 
            status.AutoSize = true;
            status.Font = new Font("MS Reference Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            status.Location = new Point(119, 32);
            status.Name = "status";
            status.Size = new Size(126, 16);
            status.TabIndex = 3;
            status.Text = "Pré Conferência";
            // 
            // labStatus
            // 
            labStatus.AutoSize = true;
            labStatus.Font = new Font("MS Reference Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labStatus.Location = new Point(58, 29);
            labStatus.Name = "labStatus";
            labStatus.Size = new Size(69, 20);
            labStatus.TabIndex = 2;
            labStatus.Text = "Status:";
            labStatus.Click += labStatus_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Pasta;
            pictureBox1.Location = new Point(6, 16);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(51, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // painelAgendamentos
            // 
            painelAgendamentos.AutoScroll = true;
            painelAgendamentos.BackColor = SystemColors.ButtonHighlight;
            painelAgendamentos.Controls.Add(caixaAg);
            painelAgendamentos.FlowDirection = FlowDirection.RightToLeft;
            painelAgendamentos.Location = new Point(14, 14);
            painelAgendamentos.Name = "painelAgendamentos";
            painelAgendamentos.Size = new Size(866, 401);
            painelAgendamentos.TabIndex = 3;
            painelAgendamentos.Paint += painelAgendamentos_Paint;
            // 
            // panel5
            // 
            panel5.BackColor = SystemColors.ActiveCaptionText;
            panel5.Location = new Point(14, 411);
            panel5.Name = "panel5";
            panel5.Size = new Size(866, 5);
            panel5.TabIndex = 6;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaptionText;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1000, 5);
            panel1.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaptionText;
            panel2.Location = new Point(-2, 454);
            panel2.Name = "panel2";
            panel2.Size = new Size(1000, 5);
            panel2.TabIndex = 5;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ActiveCaptionText;
            panel3.Location = new Point(0, -10);
            panel3.Name = "panel3";
            panel3.Size = new Size(5, 500);
            panel3.TabIndex = 6;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel4.BackColor = SystemColors.ActiveCaptionText;
            panel4.Location = new Point(898, -13);
            panel4.Name = "panel4";
            panel4.Size = new Size(5, 500);
            panel4.TabIndex = 7;
            // 
            // panel6
            // 
            panel6.BackColor = SystemColors.ActiveCaptionText;
            panel6.Location = new Point(14, 13);
            panel6.Name = "panel6";
            panel6.Size = new Size(866, 5);
            panel6.TabIndex = 7;
            // 
            // panel7
            // 
            panel7.BackColor = SystemColors.ActiveCaptionText;
            panel7.Location = new Point(879, 13);
            panel7.Name = "panel7";
            panel7.Size = new Size(5, 403);
            panel7.TabIndex = 7;
            // 
            // panel8
            // 
            panel8.BackColor = SystemColors.ActiveCaptionText;
            panel8.Location = new Point(14, 13);
            panel8.Name = "panel8";
            panel8.Size = new Size(5, 403);
            panel8.TabIndex = 8;
            // 
            // comboBoxFiltro
            // 
            comboBoxFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxFiltro.FormattingEnabled = true;
            comboBoxFiltro.Location = new Point(95, 421);
            comboBoxFiltro.Name = "comboBoxFiltro";
            comboBoxFiltro.Size = new Size(121, 23);
            comboBoxFiltro.TabIndex = 9;
            // 
            // VerAgendamentos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(903, 459);
            Controls.Add(comboBoxFiltro);
            Controls.Add(panel8);
            Controls.Add(panel7);
            Controls.Add(panel6);
            Controls.Add(panel4);
            Controls.Add(panel5);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(painelAgendamentos);
            Controls.Add(btnVoltar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "VerAgendamentos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "VerAgendamentos";
            Load += VerAgendamentos_Load;
            caixaAg.ResumeLayout(false);
            caixaAg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            painelAgendamentos.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnVoltar;
        private GroupBox caixaAg;
        private PictureBox pictureBox1;
        private Label empresa;
        private Label labEmpresa;
        private Label status;
        private Label labStatus;
        private Button buttonVer;
        private Button btnEditar;
        private Button btnComecar;
        private FlowLayoutPanel painelAgendamentos;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private Panel panel7;
        private Panel panel6;
        private Panel panel8;
        private ComboBox comboBoxFiltro;
    }
}