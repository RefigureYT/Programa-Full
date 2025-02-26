namespace ProgramaFull.Formulários
{
    partial class EmbalarEtiquetagemBIPE
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
            btnQuit = new Button();
            panel7 = new Panel();
            panel6 = new Panel();
            panel3 = new Panel();
            labelNomeJanela = new Label();
            panel1 = new Panel();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // btnVoltar
            // 
            btnVoltar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVoltar.BackgroundImage = Properties.Resources.setaVoltando;
            btnVoltar.BackgroundImageLayout = ImageLayout.Zoom;
            btnVoltar.Location = new Point(1428, 2);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(25, 25);
            btnVoltar.TabIndex = 69;
            btnVoltar.TabStop = false;
            btnVoltar.UseVisualStyleBackColor = true;
            // 
            // btnQuit
            // 
            btnQuit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnQuit.BackColor = Color.FromArgb(192, 0, 0);
            btnQuit.Font = new Font("Gadugi", 12F, FontStyle.Bold);
            btnQuit.ForeColor = SystemColors.ControlLightLight;
            btnQuit.ImageAlign = ContentAlignment.MiddleLeft;
            btnQuit.Location = new Point(1454, 2);
            btnQuit.Name = "btnQuit";
            btnQuit.Size = new Size(25, 25);
            btnQuit.TabIndex = 74;
            btnQuit.TabStop = false;
            btnQuit.Text = "X";
            btnQuit.UseVisualStyleBackColor = false;
            // 
            // panel7
            // 
            panel7.Anchor = AnchorStyles.Bottom;
            panel7.BackColor = SystemColors.MenuHighlight;
            panel7.Location = new Point(-248, 751);
            panel7.Name = "panel7";
            panel7.Size = new Size(2000, 10);
            panel7.TabIndex = 73;
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.Right;
            panel6.BackColor = SystemColors.MenuHighlight;
            panel6.Location = new Point(1474, -144);
            panel6.Name = "panel6";
            panel6.Size = new Size(10, 2000);
            panel6.TabIndex = 71;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top;
            panel3.BackColor = SystemColors.MenuHighlight;
            panel3.Controls.Add(labelNomeJanela);
            panel3.Location = new Point(-259, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(2000, 30);
            panel3.TabIndex = 72;
            // 
            // labelNomeJanela
            // 
            labelNomeJanela.Anchor = AnchorStyles.None;
            labelNomeJanela.AutoSize = true;
            labelNomeJanela.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelNomeJanela.ForeColor = SystemColors.ControlLightLight;
            labelNomeJanela.Location = new Point(1149, -30);
            labelNomeJanela.Name = "labelNomeJanela";
            labelNomeJanela.Size = new Size(298, 21);
            labelNomeJanela.TabIndex = 0;
            labelNomeJanela.Text = "Pré Conferência - Retirada do estoque";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Left;
            panel1.BackColor = SystemColors.MenuHighlight;
            panel1.Location = new Point(0, -222);
            panel1.Name = "panel1";
            panel1.Size = new Size(10, 1500);
            panel1.TabIndex = 70;
            // 
            // EmbalarEtiquetagemBIPE
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            ClientSize = new Size(1484, 761);
            Controls.Add(btnVoltar);
            Controls.Add(btnQuit);
            Controls.Add(panel7);
            Controls.Add(panel6);
            Controls.Add(panel3);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "EmbalarEtiquetagemBIPE";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EmbalarEtiquetagemBIPE";
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnVoltar;
        private Button btnQuit;
        private Panel panel7;
        private Panel panel6;
        private Panel panel3;
        private Label labelNomeJanela;
        private Panel panel1;
    }
}