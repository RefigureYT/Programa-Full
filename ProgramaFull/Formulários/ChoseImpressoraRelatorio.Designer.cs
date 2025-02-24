namespace ProgramaFull.Formulários
{
    partial class ChoseImpressoraRelatorio
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
            listaImpressoras = new ComboBox();
            pictureBox1 = new PictureBox();
            btnCancel = new Button();
            btnSalvar = new Button();
            panel4 = new Panel();
            panel3 = new Panel();
            panel2 = new Panel();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // listaImpressoras
            // 
            listaImpressoras.BackColor = SystemColors.MenuBar;
            listaImpressoras.FormattingEnabled = true;
            listaImpressoras.Location = new Point(93, 12);
            listaImpressoras.Name = "listaImpressoras";
            listaImpressoras.Size = new Size(309, 23);
            listaImpressoras.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.impressoraRelatorio;
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(60, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(313, 39);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 23);
            btnCancel.TabIndex = 2;
            btnCancel.TabStop = false;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(217, 39);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(90, 23);
            btnSalvar.TabIndex = 3;
            btnSalvar.TabStop = false;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.ActiveCaptionText;
            panel4.Location = new Point(418, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(3, 80);
            panel4.TabIndex = 15;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ActiveCaptionText;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(3, 80);
            panel3.TabIndex = 14;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaptionText;
            panel2.Location = new Point(0, 76);
            panel2.Name = "panel2";
            panel2.Size = new Size(500, 3);
            panel2.TabIndex = 13;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaptionText;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(500, 3);
            panel1.TabIndex = 12;
            // 
            // ChoseImpressoraRelatorio
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
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
            Name = "ChoseImpressoraRelatorio";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ChoseImpressoraRelatorio";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox listaImpressoras;
        private PictureBox pictureBox1;
        private Button btnCancel;
        private Button btnSalvar;
        private Panel panel4;
        private Panel panel3;
        private Panel panel2;
        private Panel panel1;
    }
}