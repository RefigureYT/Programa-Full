namespace ProgramaFull.Formulários
{
    partial class ChoseImpressoraRelatorioEMERGENCIA
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
            btnSalvar = new Button();
            btnCancel = new Button();
            pictureBox1 = new PictureBox();
            listaImpressoras = new ComboBox();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(220, 41);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(90, 23);
            btnSalvar.TabIndex = 7;
            btnSalvar.TabStop = false;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(316, 41);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 23);
            btnCancel.TabIndex = 6;
            btnCancel.TabStop = false;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.impressoraRelatorio;
            pictureBox1.Location = new Point(15, 14);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(60, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // listaImpressoras
            // 
            listaImpressoras.BackColor = SystemColors.MenuBar;
            listaImpressoras.FormattingEnabled = true;
            listaImpressoras.Location = new Point(96, 14);
            listaImpressoras.Name = "listaImpressoras";
            listaImpressoras.Size = new Size(309, 23);
            listaImpressoras.TabIndex = 4;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaptionText;
            panel1.Location = new Point(-18, 75);
            panel1.Name = "panel1";
            panel1.Size = new Size(547, 100);
            panel1.TabIndex = 8;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaptionText;
            panel2.Location = new Point(-19, -95);
            panel2.Name = "panel2";
            panel2.Size = new Size(547, 100);
            panel2.TabIndex = 9;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ActiveCaptionText;
            panel3.Location = new Point(417, -8);
            panel3.Name = "panel3";
            panel3.Size = new Size(547, 100);
            panel3.TabIndex = 10;
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.ActiveCaptionText;
            panel4.Location = new Point(-542, -8);
            panel4.Name = "panel4";
            panel4.Size = new Size(547, 100);
            panel4.TabIndex = 10;
            // 
            // ChoseImpressoraRelatorioEMERGENCIA
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(421, 79);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(btnSalvar);
            Controls.Add(btnCancel);
            Controls.Add(pictureBox1);
            Controls.Add(listaImpressoras);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ChoseImpressoraRelatorioEMERGENCIA";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ChoseImpressoraRelatorioEMERGENCIA";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnSalvar;
        private Button btnCancel;
        private PictureBox pictureBox1;
        private ComboBox listaImpressoras;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
    }
}