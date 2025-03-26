namespace ProgramaFull.Formulários
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            btnQuit = new Button();
            btnConfig = new Button();
            btnVerAg = new Button();
            btnUploadDrive = new Button();
            JauPesca = new PictureBox();
            textoMenu = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            labelTypeVersion = new Label();
            ((System.ComponentModel.ISupportInitialize)JauPesca).BeginInit();
            SuspendLayout();
            // 
            // btnQuit
            // 
            btnQuit.BackColor = Color.FromArgb(192, 0, 0);
            btnQuit.Font = new Font("Gadugi", 12F, FontStyle.Bold);
            btnQuit.ForeColor = SystemColors.ControlLightLight;
            btnQuit.ImageAlign = ContentAlignment.MiddleLeft;
            btnQuit.Location = new Point(392, 12);
            btnQuit.Name = "btnQuit";
            btnQuit.Size = new Size(25, 25);
            btnQuit.TabIndex = 11;
            btnQuit.TabStop = false;
            btnQuit.Text = "X";
            btnQuit.UseVisualStyleBackColor = false;
            btnQuit.Click += btnQuit_Click;
            // 
            // btnConfig
            // 
            btnConfig.BackColor = SystemColors.Control;
            btnConfig.BackgroundImage = Properties.Resources.conf1;
            btnConfig.BackgroundImageLayout = ImageLayout.Zoom;
            btnConfig.Font = new Font("Gadugi", 12F, FontStyle.Bold);
            btnConfig.Location = new Point(15, 12);
            btnConfig.Name = "btnConfig";
            btnConfig.Size = new Size(26, 25);
            btnConfig.TabIndex = 10;
            btnConfig.TabStop = false;
            btnConfig.Text = "\r\n";
            btnConfig.TextAlign = ContentAlignment.MiddleRight;
            btnConfig.UseVisualStyleBackColor = false;
            btnConfig.Click += btnConfig_Click;
            // 
            // btnVerAg
            // 
            btnVerAg.Font = new Font("Gadugi", 12F, FontStyle.Bold);
            btnVerAg.Location = new Point(25, 350);
            btnVerAg.Name = "btnVerAg";
            btnVerAg.Size = new Size(381, 48);
            btnVerAg.TabIndex = 9;
            btnVerAg.TabStop = false;
            btnVerAg.Text = "Ver Agendamentos";
            btnVerAg.UseVisualStyleBackColor = true;
            btnVerAg.Click += btnVerAg_Click;
            // 
            // btnUploadDrive
            // 
            btnUploadDrive.Font = new Font("Gadugi", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUploadDrive.Location = new Point(22, 404);
            btnUploadDrive.Name = "btnUploadDrive";
            btnUploadDrive.Size = new Size(384, 48);
            btnUploadDrive.TabIndex = 8;
            btnUploadDrive.TabStop = false;
            btnUploadDrive.Text = "Upload Agendamentos";
            btnUploadDrive.UseVisualStyleBackColor = true;
            btnUploadDrive.Click += btnUploadDrive_Click;
            // 
            // JauPesca
            // 
            JauPesca.Image = (Image)resources.GetObject("JauPesca.Image");
            JauPesca.Location = new Point(40, 76);
            JauPesca.Name = "JauPesca";
            JauPesca.Size = new Size(350, 144);
            JauPesca.SizeMode = PictureBoxSizeMode.AutoSize;
            JauPesca.TabIndex = 7;
            JauPesca.TabStop = false;
            JauPesca.DoubleClick += JauPesca_DoubleClick;
            // 
            // textoMenu
            // 
            textoMenu.AutoSize = true;
            textoMenu.Font = new Font("Arial", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textoMenu.Location = new Point(79, 41);
            textoMenu.Name = "textoMenu";
            textoMenu.Size = new Size(271, 32);
            textoMenu.TabIndex = 6;
            textoMenu.Text = "Agendamentos Full";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaptionText;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(10, 500);
            panel1.TabIndex = 12;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaptionText;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(500, 10);
            panel2.TabIndex = 13;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ActiveCaptionText;
            panel3.Location = new Point(426, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(10, 500);
            panel3.TabIndex = 13;
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.ActiveCaptionText;
            panel4.Location = new Point(-2, 456);
            panel4.Name = "panel4";
            panel4.Size = new Size(500, 10);
            panel4.TabIndex = 14;
            // 
            // labelTypeVersion
            // 
            labelTypeVersion.AutoSize = true;
            labelTypeVersion.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTypeVersion.Location = new Point(171, 20);
            labelTypeVersion.Name = "labelTypeVersion";
            labelTypeVersion.Size = new Size(44, 17);
            labelTypeVersion.TabIndex = 20;
            labelTypeVersion.Text = "Alpha";
            labelTypeVersion.Visible = false;
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(436, 466);
            ControlBox = false;
            Controls.Add(labelTypeVersion);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Controls.Add(btnQuit);
            Controls.Add(btnConfig);
            Controls.Add(btnVerAg);
            Controls.Add(btnUploadDrive);
            Controls.Add(JauPesca);
            Controls.Add(textoMenu);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Menu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menu";
            Load += Menu_Load;
            ((System.ComponentModel.ISupportInitialize)JauPesca).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnQuit;
        private Button btnConfig;
        private Button btnVerAg;
        private Button btnUploadDrive;
        private PictureBox JauPesca;
        private Label textoMenu;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Label labelTypeVersion;
    }
}