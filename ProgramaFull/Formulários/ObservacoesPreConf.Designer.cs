namespace ProgramaFull.Formulários
{
    partial class ObservacoesPreConf
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObservacoesPreConf));
            obsFoto = new PictureBox();
            insiraObs = new RichTextBox();
            btnSalvar = new Button();
            btnCancelar = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            panel5 = new Panel();
            ((System.ComponentModel.ISupportInitialize)obsFoto).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // obsFoto
            // 
            obsFoto.Image = Properties.Resources.Obsdnv;
            obsFoto.Location = new Point(18, 21);
            obsFoto.Name = "obsFoto";
            obsFoto.Size = new Size(100, 88);
            obsFoto.SizeMode = PictureBoxSizeMode.Zoom;
            obsFoto.TabIndex = 0;
            obsFoto.TabStop = false;
            // 
            // insiraObs
            // 
            insiraObs.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            insiraObs.Location = new Point(124, 21);
            insiraObs.Name = "insiraObs";
            insiraObs.Size = new Size(414, 178);
            insiraObs.TabIndex = 1;
            insiraObs.Text = "";
            // 
            // btnSalvar
            // 
            btnSalvar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSalvar.Location = new Point(473, 205);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(75, 26);
            btnSalvar.TabIndex = 2;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancelar.Location = new Point(392, 205);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 26);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlText;
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(5, 243);
            panel1.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ControlText;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(5, 243);
            panel2.TabIndex = 5;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ControlText;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(560, 5);
            panel3.TabIndex = 6;
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.ControlText;
            panel4.Location = new Point(555, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(5, 243);
            panel4.TabIndex = 6;
            // 
            // panel5
            // 
            panel5.BackColor = SystemColors.ControlText;
            panel5.Location = new Point(0, 238);
            panel5.Name = "panel5";
            panel5.Size = new Size(560, 5);
            panel5.TabIndex = 7;
            // 
            // ObservacoesPreConf
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 243);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Controls.Add(btnCancelar);
            Controls.Add(btnSalvar);
            Controls.Add(insiraObs);
            Controls.Add(obsFoto);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ObservacoesPreConf";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Insira as observações";
            Load += ObservacoesPreConf_Load;
            ((System.ComponentModel.ISupportInitialize)obsFoto).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox obsFoto;
        private RichTextBox insiraObs;
        private Button btnSalvar;
        private Button btnCancelar;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
    }
}