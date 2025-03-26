namespace ProgramaFull.Formulários
{
    partial class ModoDEVEmbalar
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
            labelUpdate = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // labelUpdate
            // 
            labelUpdate.AutoSize = true;
            labelUpdate.Location = new Point(12, 9);
            labelUpdate.Name = "labelUpdate";
            labelUpdate.Size = new Size(100, 15);
            labelUpdate.TabIndex = 0;
            labelUpdate.Text = "Atualizar a listBox";
            // 
            // button1
            // 
            button1.Location = new Point(23, 27);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Update";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ModoDEVEmbalar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(labelUpdate);
            Name = "ModoDEVEmbalar";
            Text = "ModoDEVEmbalar";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelUpdate;
        private Button button1;
    }
}