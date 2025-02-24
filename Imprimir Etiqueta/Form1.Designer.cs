namespace Imprimir_Etiqueta
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnImprimir = new Button();
            textoAlt = new Label();
            pictureBox1 = new PictureBox();
            listBoxImpressoras = new ComboBox();
            btnSalvarImpressora = new Button();
            comboBoxEtiqueta = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnImprimir
            // 
            btnImprimir.Location = new Point(576, 4);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(75, 23);
            btnImprimir.TabIndex = 0;
            btnImprimir.Text = "Imprimir";
            btnImprimir.UseVisualStyleBackColor = true;
            btnImprimir.Click += btnImprimir_Click;
            // 
            // textoAlt
            // 
            textoAlt.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            textoAlt.AutoSize = true;
            textoAlt.Location = new Point(521, -252);
            textoAlt.Name = "textoAlt";
            textoAlt.Size = new Size(110, 15);
            textoAlt.TabIndex = 1;
            textoAlt.Text = "Impressão desejada";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            pictureBox1.Image = Properties.Resources.label;
            pictureBox1.Location = new Point(-134, -227);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(776, 261);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.Visible = false;
            // 
            // listBoxImpressoras
            // 
            listBoxImpressoras.FormattingEnabled = true;
            listBoxImpressoras.Location = new Point(12, 4);
            listBoxImpressoras.Name = "listBoxImpressoras";
            listBoxImpressoras.Size = new Size(303, 23);
            listBoxImpressoras.TabIndex = 3;
            // 
            // btnSalvarImpressora
            // 
            btnSalvarImpressora.Location = new Point(321, 4);
            btnSalvarImpressora.Name = "btnSalvarImpressora";
            btnSalvarImpressora.Size = new Size(135, 23);
            btnSalvarImpressora.TabIndex = 4;
            btnSalvarImpressora.Text = "Salvar Impressora";
            btnSalvarImpressora.UseVisualStyleBackColor = true;
            btnSalvarImpressora.Click += btnSalvarImpressora_Click;
            // 
            // comboBoxEtiqueta
            // 
            comboBoxEtiqueta.FormattingEnabled = true;
            comboBoxEtiqueta.Location = new Point(462, 5);
            comboBoxEtiqueta.Name = "comboBoxEtiqueta";
            comboBoxEtiqueta.Size = new Size(108, 23);
            comboBoxEtiqueta.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(654, 35);
            Controls.Add(comboBoxEtiqueta);
            Controls.Add(btnSalvarImpressora);
            Controls.Add(listBoxImpressoras);
            Controls.Add(pictureBox1);
            Controls.Add(textoAlt);
            Controls.Add(btnImprimir);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnImprimir;
        private Label textoAlt;
        private PictureBox pictureBox1;
        private ComboBox listBoxImpressoras;
        private Button btnSalvarImpressora;
        private ComboBox comboBoxEtiqueta;
    }
}
