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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmbalarEtiquetagemBIPE));
            btnVoltar = new Button();
            btnQuit = new Button();
            panel7 = new Panel();
            panel6 = new Panel();
            panel3 = new Panel();
            headerForm = new Label();
            labelNomeJanela = new Label();
            panel1 = new Panel();
            codigoProdutoTxtBox = new TextBox();
            labelTutorial = new Label();
            listBoxAnuncios = new ListBox();
            JauPesca = new PictureBox();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)JauPesca).BeginInit();
            SuspendLayout();
            // 
            // btnVoltar
            // 
            btnVoltar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVoltar.BackgroundImage = Properties.Resources.setaVoltando;
            btnVoltar.BackgroundImageLayout = ImageLayout.Zoom;
            btnVoltar.Location = new Point(438, 5);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(25, 25);
            btnVoltar.TabIndex = 69;
            btnVoltar.TabStop = false;
            btnVoltar.UseVisualStyleBackColor = true;
            btnVoltar.Click += btnVoltar_Click;
            // 
            // btnQuit
            // 
            btnQuit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnQuit.BackColor = Color.FromArgb(192, 0, 0);
            btnQuit.Font = new Font("Gadugi", 12F, FontStyle.Bold);
            btnQuit.ForeColor = SystemColors.ControlLightLight;
            btnQuit.ImageAlign = ContentAlignment.MiddleLeft;
            btnQuit.Location = new Point(464, 5);
            btnQuit.Name = "btnQuit";
            btnQuit.Size = new Size(25, 25);
            btnQuit.TabIndex = 74;
            btnQuit.TabStop = false;
            btnQuit.Text = "X";
            btnQuit.UseVisualStyleBackColor = false;
            btnQuit.Click += btnQuit_Click;
            // 
            // panel7
            // 
            panel7.Anchor = AnchorStyles.None;
            panel7.BackColor = SystemColors.MenuHighlight;
            panel7.Location = new Point(0, 590);
            panel7.Name = "panel7";
            panel7.Size = new Size(500, 10);
            panel7.TabIndex = 73;
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.None;
            panel6.BackColor = SystemColors.MenuHighlight;
            panel6.Location = new Point(490, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(10, 600);
            panel6.TabIndex = 71;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.None;
            panel3.BackColor = SystemColors.MenuHighlight;
            panel3.Controls.Add(headerForm);
            panel3.Controls.Add(labelNomeJanela);
            panel3.Controls.Add(btnVoltar);
            panel3.Controls.Add(btnQuit);
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(500, 35);
            panel3.TabIndex = 72;
            // 
            // headerForm
            // 
            headerForm.AutoSize = true;
            headerForm.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            headerForm.ForeColor = SystemColors.ActiveCaptionText;
            headerForm.Location = new Point(17, 7);
            headerForm.Name = "headerForm";
            headerForm.Size = new Size(66, 20);
            headerForm.TabIndex = 76;
            headerForm.Text = "Embalar";
            // 
            // labelNomeJanela
            // 
            labelNomeJanela.Anchor = AnchorStyles.None;
            labelNomeJanela.AutoSize = true;
            labelNomeJanela.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelNomeJanela.ForeColor = SystemColors.ControlLightLight;
            labelNomeJanela.Location = new Point(399, -28);
            labelNomeJanela.Name = "labelNomeJanela";
            labelNomeJanela.Size = new Size(298, 21);
            labelNomeJanela.TabIndex = 0;
            labelNomeJanela.Text = "Pré Conferência - Retirada do estoque";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.None;
            panel1.BackColor = SystemColors.MenuHighlight;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(10, 600);
            panel1.TabIndex = 70;
            // 
            // codigoProdutoTxtBox
            // 
            codigoProdutoTxtBox.Anchor = AnchorStyles.None;
            codigoProdutoTxtBox.Location = new Point(17, 161);
            codigoProdutoTxtBox.Name = "codigoProdutoTxtBox";
            codigoProdutoTxtBox.Size = new Size(467, 23);
            codigoProdutoTxtBox.TabIndex = 75;
            codigoProdutoTxtBox.KeyDown += codigoProdutoTxtBox_KeyDown;
            // 
            // labelTutorial
            // 
            labelTutorial.AutoSize = true;
            labelTutorial.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTutorial.Location = new Point(154, 138);
            labelTutorial.Name = "labelTutorial";
            labelTutorial.Size = new Size(178, 20);
            labelTutorial.TabIndex = 77;
            labelTutorial.Text = "Bipe o primeiro produto";
            // 
            // listBoxAnuncios
            // 
            listBoxAnuncios.FormattingEnabled = true;
            listBoxAnuncios.Location = new Point(16, 190);
            listBoxAnuncios.Name = "listBoxAnuncios";
            listBoxAnuncios.Size = new Size(468, 394);
            listBoxAnuncios.TabIndex = 78;
            // 
            // JauPesca
            // 
            JauPesca.Anchor = AnchorStyles.Bottom;
            JauPesca.Image = (Image)resources.GetObject("JauPesca.Image");
            JauPesca.Location = new Point(16, 41);
            JauPesca.Name = "JauPesca";
            JauPesca.Size = new Size(468, 94);
            JauPesca.SizeMode = PictureBoxSizeMode.Zoom;
            JauPesca.TabIndex = 79;
            JauPesca.TabStop = false;
            // 
            // EmbalarEtiquetagemBIPE
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            ClientSize = new Size(500, 600);
            Controls.Add(JauPesca);
            Controls.Add(listBoxAnuncios);
            Controls.Add(labelTutorial);
            Controls.Add(codigoProdutoTxtBox);
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
            ((System.ComponentModel.ISupportInitialize)JauPesca).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnVoltar;
        private Button btnQuit;
        private Panel panel7;
        private Panel panel6;
        private Panel panel3;
        private Label labelNomeJanela;
        private Panel panel1;
        private TextBox codigoProdutoTxtBox;
        private Label headerForm;
        private Label labelTutorial;
        private ListBox listBoxAnuncios;
        private PictureBox JauPesca;
    }
}