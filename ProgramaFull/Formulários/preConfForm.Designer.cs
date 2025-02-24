namespace ProgramaFull.Formulários
{
    partial class preConfForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(preConfForm));
            panel1 = new Panel();
            panel3 = new Panel();
            labelNomeJanela = new Label();
            panel6 = new Panel();
            panel7 = new Panel();
            painelInfoEmpresa = new Panel();
            groupBoxInfoEmpresa = new GroupBox();
            labelExDataEHora = new Label();
            labelExColaborador = new Label();
            labelExAgendamento = new Label();
            labelExEmpresa = new Label();
            labelData = new Label();
            labelColaborador = new Label();
            labelAgendamento = new Label();
            labelEmpresa = new Label();
            panel8 = new Panel();
            groupBoxInfoAg = new GroupBox();
            labelTempoEstimado = new Label();
            labelTempo = new Label();
            labelTotalDeUnidades = new Label();
            labelTempoPreConferencia = new Label();
            textBoxCODE = new TextBox();
            btnQuitDEV = new Button();
            labelCODE = new Label();
            textQTD = new TextBox();
            labelQTD = new Label();
            labelOpcional = new Label();
            panelConcluidos = new Panel();
            JauPesca = new PictureBox();
            btnQuit = new Button();
            btnVoltar = new Button();
            panelProdutos = new Panel();
            btnFinalizarAgDEV = new Button();
            labelSKUCodebarDEV = new Label();
            textBoxSKUDEV = new TextBox();
            btnMenosUn = new Button();
            UnidadesProduto = new TextBox();
            btnMaisUn = new Button();
            btnSalvarNewUn = new Button();
            btnAtualizarLabels = new Button();
            btnObs = new Button();
            panelDev = new Panel();
            limpaSKUCodebar = new RadioButton();
            naoLimpaSKUCodebar = new RadioButton();
            panel3.SuspendLayout();
            painelInfoEmpresa.SuspendLayout();
            groupBoxInfoEmpresa.SuspendLayout();
            panel8.SuspendLayout();
            groupBoxInfoAg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)JauPesca).BeginInit();
            panelDev.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Left;
            panel1.BackColor = SystemColors.MenuHighlight;
            panel1.Location = new Point(0, -225);
            panel1.Name = "panel1";
            panel1.Size = new Size(10, 1500);
            panel1.TabIndex = 53;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top;
            panel3.BackColor = SystemColors.MenuHighlight;
            panel3.Controls.Add(labelNomeJanela);
            panel3.Location = new Point(-243, -2);
            panel3.Name = "panel3";
            panel3.Size = new Size(2000, 30);
            panel3.TabIndex = 54;
            // 
            // labelNomeJanela
            // 
            labelNomeJanela.Anchor = AnchorStyles.None;
            labelNomeJanela.AutoSize = true;
            labelNomeJanela.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelNomeJanela.ForeColor = SystemColors.ControlLightLight;
            labelNomeJanela.Location = new Point(249, 5);
            labelNomeJanela.Name = "labelNomeJanela";
            labelNomeJanela.Size = new Size(298, 21);
            labelNomeJanela.TabIndex = 0;
            labelNomeJanela.Text = "Pré Conferência - Retirada do estoque";
            labelNomeJanela.Click += labelNomeJanela_Click;
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.Right;
            panel6.BackColor = SystemColors.MenuHighlight;
            panel6.Location = new Point(1490, -146);
            panel6.Name = "panel6";
            panel6.Size = new Size(10, 2000);
            panel6.TabIndex = 54;
            // 
            // panel7
            // 
            panel7.Anchor = AnchorStyles.Bottom;
            panel7.BackColor = SystemColors.MenuHighlight;
            panel7.Location = new Point(-230, 790);
            panel7.Name = "panel7";
            panel7.Size = new Size(2000, 10);
            panel7.TabIndex = 55;
            // 
            // painelInfoEmpresa
            // 
            painelInfoEmpresa.Anchor = AnchorStyles.Right;
            painelInfoEmpresa.Controls.Add(groupBoxInfoEmpresa);
            painelInfoEmpresa.Location = new Point(1171, 35);
            painelInfoEmpresa.Name = "painelInfoEmpresa";
            painelInfoEmpresa.Size = new Size(313, 183);
            painelInfoEmpresa.TabIndex = 57;
            // 
            // groupBoxInfoEmpresa
            // 
            groupBoxInfoEmpresa.Anchor = AnchorStyles.Right;
            groupBoxInfoEmpresa.Controls.Add(labelExDataEHora);
            groupBoxInfoEmpresa.Controls.Add(labelExColaborador);
            groupBoxInfoEmpresa.Controls.Add(labelExAgendamento);
            groupBoxInfoEmpresa.Controls.Add(labelExEmpresa);
            groupBoxInfoEmpresa.Controls.Add(labelData);
            groupBoxInfoEmpresa.Controls.Add(labelColaborador);
            groupBoxInfoEmpresa.Controls.Add(labelAgendamento);
            groupBoxInfoEmpresa.Controls.Add(labelEmpresa);
            groupBoxInfoEmpresa.Location = new Point(3, 3);
            groupBoxInfoEmpresa.Name = "groupBoxInfoEmpresa";
            groupBoxInfoEmpresa.Size = new Size(306, 176);
            groupBoxInfoEmpresa.TabIndex = 0;
            groupBoxInfoEmpresa.TabStop = false;
            groupBoxInfoEmpresa.Text = "Informações";
            // 
            // labelExDataEHora
            // 
            labelExDataEHora.AutoSize = true;
            labelExDataEHora.BackColor = SystemColors.Control;
            labelExDataEHora.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelExDataEHora.ForeColor = SystemColors.ActiveCaptionText;
            labelExDataEHora.Location = new Point(119, 149);
            labelExDataEHora.Name = "labelExDataEHora";
            labelExDataEHora.Size = new Size(147, 15);
            labelExDataEHora.TabIndex = 7;
            labelExDataEHora.Text = "09/12/2024 16h 43m 27s";
            // 
            // labelExColaborador
            // 
            labelExColaborador.AutoSize = true;
            labelExColaborador.BackColor = SystemColors.Control;
            labelExColaborador.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelExColaborador.ForeColor = SystemColors.ActiveCaptionText;
            labelExColaborador.Location = new Point(132, 110);
            labelExColaborador.Name = "labelExColaborador";
            labelExColaborador.Size = new Size(42, 15);
            labelExColaborador.TabIndex = 6;
            labelExColaborador.Text = "Kelvin";
            // 
            // labelExAgendamento
            // 
            labelExAgendamento.AutoSize = true;
            labelExAgendamento.BackColor = SystemColors.Control;
            labelExAgendamento.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelExAgendamento.ForeColor = SystemColors.ActiveCaptionText;
            labelExAgendamento.Location = new Point(145, 69);
            labelExAgendamento.Name = "labelExAgendamento";
            labelExAgendamento.Size = new Size(63, 15);
            labelExAgendamento.TabIndex = 5;
            labelExAgendamento.Text = "36524789";
            // 
            // labelExEmpresa
            // 
            labelExEmpresa.AutoSize = true;
            labelExEmpresa.BackColor = SystemColors.Control;
            labelExEmpresa.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelExEmpresa.ForeColor = SystemColors.ActiveCaptionText;
            labelExEmpresa.Location = new Point(93, 30);
            labelExEmpresa.Name = "labelExEmpresa";
            labelExEmpresa.Size = new Size(64, 15);
            labelExEmpresa.TabIndex = 4;
            labelExEmpresa.Text = "PESCAJAU";
            // 
            // labelData
            // 
            labelData.AutoSize = true;
            labelData.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelData.Location = new Point(3, 142);
            labelData.Name = "labelData";
            labelData.Size = new Size(119, 25);
            labelData.TabIndex = 3;
            labelData.Text = "Data e hora:";
            // 
            // labelColaborador
            // 
            labelColaborador.AutoSize = true;
            labelColaborador.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelColaborador.Location = new Point(4, 102);
            labelColaborador.Name = "labelColaborador";
            labelColaborador.Size = new Size(131, 25);
            labelColaborador.TabIndex = 2;
            labelColaborador.Text = "Colaborador:";
            // 
            // labelAgendamento
            // 
            labelAgendamento.AutoSize = true;
            labelAgendamento.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelAgendamento.Location = new Point(3, 62);
            labelAgendamento.Name = "labelAgendamento";
            labelAgendamento.Size = new Size(144, 25);
            labelAgendamento.TabIndex = 1;
            labelAgendamento.Text = "Agendamento:";
            // 
            // labelEmpresa
            // 
            labelEmpresa.AutoSize = true;
            labelEmpresa.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelEmpresa.Location = new Point(3, 22);
            labelEmpresa.Name = "labelEmpresa";
            labelEmpresa.Size = new Size(92, 25);
            labelEmpresa.TabIndex = 0;
            labelEmpresa.Text = "Empresa:";
            // 
            // panel8
            // 
            panel8.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panel8.Controls.Add(groupBoxInfoAg);
            panel8.Location = new Point(1171, 221);
            panel8.Name = "panel8";
            panel8.Size = new Size(313, 106);
            panel8.TabIndex = 58;
            // 
            // groupBoxInfoAg
            // 
            groupBoxInfoAg.Anchor = AnchorStyles.Right;
            groupBoxInfoAg.Controls.Add(labelTempoEstimado);
            groupBoxInfoAg.Controls.Add(labelTempo);
            groupBoxInfoAg.Controls.Add(labelTotalDeUnidades);
            groupBoxInfoAg.Controls.Add(labelTempoPreConferencia);
            groupBoxInfoAg.Location = new Point(3, 3);
            groupBoxInfoAg.Name = "groupBoxInfoAg";
            groupBoxInfoAg.Size = new Size(306, 98);
            groupBoxInfoAg.TabIndex = 0;
            groupBoxInfoAg.TabStop = false;
            groupBoxInfoAg.Text = "Agendamento";
            // 
            // labelTempoEstimado
            // 
            labelTempoEstimado.AutoSize = true;
            labelTempoEstimado.Font = new Font("Segoe UI", 11F);
            labelTempoEstimado.Location = new Point(162, 65);
            labelTempoEstimado.Name = "labelTempoEstimado";
            labelTempoEstimado.Size = new Size(92, 20);
            labelTempoEstimado.TabIndex = 4;
            labelTempoEstimado.Text = "00h 00m 00s";
            // 
            // labelTempo
            // 
            labelTempo.AutoSize = true;
            labelTempo.Font = new Font("Segoe UI", 11F);
            labelTempo.Location = new Point(81, 25);
            labelTempo.Name = "labelTempo";
            labelTempo.Size = new Size(92, 20);
            labelTempo.TabIndex = 3;
            labelTempo.Text = "00h 00m 00s";
            // 
            // labelTotalDeUnidades
            // 
            labelTotalDeUnidades.AutoSize = true;
            labelTotalDeUnidades.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelTotalDeUnidades.Location = new Point(2, 60);
            labelTotalDeUnidades.Name = "labelTotalDeUnidades";
            labelTotalDeUnidades.Size = new Size(163, 25);
            labelTotalDeUnidades.TabIndex = 2;
            labelTotalDeUnidades.Text = "Tempo estimado:";
            // 
            // labelTempoPreConferencia
            // 
            labelTempoPreConferencia.AutoSize = true;
            labelTempoPreConferencia.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelTempoPreConferencia.Location = new Point(2, 20);
            labelTempoPreConferencia.Name = "labelTempoPreConferencia";
            labelTempoPreConferencia.Size = new Size(77, 25);
            labelTempoPreConferencia.TabIndex = 1;
            labelTempoPreConferencia.Text = "Tempo:";
            // 
            // textBoxCODE
            // 
            textBoxCODE.Location = new Point(623, 60);
            textBoxCODE.Name = "textBoxCODE";
            textBoxCODE.Size = new Size(542, 23);
            textBoxCODE.TabIndex = 59;
            textBoxCODE.TextChanged += textBoxCODE_TextChanged;
            textBoxCODE.KeyDown += textBoxCODE_KeyDown;
            // 
            // btnQuitDEV
            // 
            btnQuitDEV.Anchor = AnchorStyles.Bottom;
            btnQuitDEV.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnQuitDEV.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnQuitDEV.Location = new Point(623, 598);
            btnQuitDEV.Name = "btnQuitDEV";
            btnQuitDEV.Size = new Size(346, 23);
            btnQuitDEV.TabIndex = 61;
            btnQuitDEV.TabStop = false;
            btnQuitDEV.Text = "Sair do modo desenvolvedor";
            btnQuitDEV.UseVisualStyleBackColor = true;
            btnQuitDEV.Visible = false;
            btnQuitDEV.Click += btnQuitDEV_Click;
            // 
            // labelCODE
            // 
            labelCODE.AutoSize = true;
            labelCODE.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCODE.Location = new Point(755, 35);
            labelCODE.Name = "labelCODE";
            labelCODE.Size = new Size(286, 20);
            labelCODE.TabIndex = 62;
            labelCODE.Text = "Insira o código de barras ou SKU abaixo";
            // 
            // textQTD
            // 
            textQTD.Location = new Point(888, 90);
            textQTD.Name = "textQTD";
            textQTD.Size = new Size(277, 23);
            textQTD.TabIndex = 63;
            textQTD.KeyDown += textQTD_KeyDown;
            textQTD.KeyPress += textQTD_KeyPress;
            // 
            // labelQTD
            // 
            labelQTD.AutoSize = true;
            labelQTD.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            labelQTD.Location = new Point(623, 91);
            labelQTD.Name = "labelQTD";
            labelQTD.Size = new Size(198, 20);
            labelQTD.TabIndex = 64;
            labelQTD.Text = "Insira a quantidade ao lado";
            // 
            // labelOpcional
            // 
            labelOpcional.AutoSize = true;
            labelOpcional.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            labelOpcional.Location = new Point(822, 93);
            labelOpcional.Name = "labelOpcional";
            labelOpcional.Size = new Size(59, 15);
            labelOpcional.TabIndex = 65;
            labelOpcional.Text = "(opcional)";
            // 
            // panelConcluidos
            // 
            panelConcluidos.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panelConcluidos.BackColor = SystemColors.ControlLightLight;
            panelConcluidos.BorderStyle = BorderStyle.FixedSingle;
            panelConcluidos.Location = new Point(975, 333);
            panelConcluidos.Name = "panelConcluidos";
            panelConcluidos.Size = new Size(513, 449);
            panelConcluidos.TabIndex = 66;
            // 
            // JauPesca
            // 
            JauPesca.Anchor = AnchorStyles.Bottom;
            JauPesca.Image = (Image)resources.GetObject("JauPesca.Image");
            JauPesca.Location = new Point(623, 627);
            JauPesca.Name = "JauPesca";
            JauPesca.Size = new Size(346, 155);
            JauPesca.SizeMode = PictureBoxSizeMode.Zoom;
            JauPesca.TabIndex = 67;
            JauPesca.TabStop = false;
            // 
            // btnQuit
            // 
            btnQuit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnQuit.BackColor = Color.FromArgb(192, 0, 0);
            btnQuit.Font = new Font("Gadugi", 12F, FontStyle.Bold);
            btnQuit.ForeColor = SystemColors.ControlLightLight;
            btnQuit.ImageAlign = ContentAlignment.MiddleLeft;
            btnQuit.Location = new Point(1472, 2);
            btnQuit.Name = "btnQuit";
            btnQuit.Size = new Size(25, 25);
            btnQuit.TabIndex = 68;
            btnQuit.TabStop = false;
            btnQuit.Text = "X";
            btnQuit.UseVisualStyleBackColor = false;
            btnQuit.Click += btnQuit_Click;
            // 
            // btnVoltar
            // 
            btnVoltar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVoltar.BackgroundImage = Properties.Resources.setaVoltando;
            btnVoltar.BackgroundImageLayout = ImageLayout.Zoom;
            btnVoltar.Location = new Point(1446, 2);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(25, 25);
            btnVoltar.TabIndex = 1;
            btnVoltar.TabStop = false;
            btnVoltar.UseVisualStyleBackColor = true;
            btnVoltar.Click += btnVoltar_Click;
            // 
            // panelProdutos
            // 
            panelProdutos.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            panelProdutos.BackColor = SystemColors.ControlLightLight;
            panelProdutos.BorderStyle = BorderStyle.FixedSingle;
            panelProdutos.Location = new Point(17, 35);
            panelProdutos.Name = "panelProdutos";
            panelProdutos.Size = new Size(600, 747);
            panelProdutos.TabIndex = 56;
            // 
            // btnFinalizarAgDEV
            // 
            btnFinalizarAgDEV.Location = new Point(623, 569);
            btnFinalizarAgDEV.Name = "btnFinalizarAgDEV";
            btnFinalizarAgDEV.Size = new Size(177, 23);
            btnFinalizarAgDEV.TabIndex = 69;
            btnFinalizarAgDEV.Text = "Finalizar Agendamento";
            btnFinalizarAgDEV.UseVisualStyleBackColor = true;
            btnFinalizarAgDEV.Visible = false;
            btnFinalizarAgDEV.Click += btnFinalizarAgDEV_Click;
            // 
            // labelSKUCodebarDEV
            // 
            labelSKUCodebarDEV.AutoSize = true;
            labelSKUCodebarDEV.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelSKUCodebarDEV.Location = new Point(3, 2);
            labelSKUCodebarDEV.Name = "labelSKUCodebarDEV";
            labelSKUCodebarDEV.Size = new Size(108, 20);
            labelSKUCodebarDEV.TabIndex = 70;
            labelSKUCodebarDEV.Text = "SKU/Codebar:";
            // 
            // textBoxSKUDEV
            // 
            textBoxSKUDEV.Location = new Point(116, 2);
            textBoxSKUDEV.Name = "textBoxSKUDEV";
            textBoxSKUDEV.Size = new Size(251, 23);
            textBoxSKUDEV.TabIndex = 1;
            textBoxSKUDEV.KeyDown += textBoxSKUDEV_KeyDown;
            // 
            // btnMenosUn
            // 
            btnMenosUn.Location = new Point(400, 1);
            btnMenosUn.Name = "btnMenosUn";
            btnMenosUn.Size = new Size(25, 25);
            btnMenosUn.TabIndex = 72;
            btnMenosUn.TabStop = false;
            btnMenosUn.Text = "-";
            btnMenosUn.UseVisualStyleBackColor = true;
            btnMenosUn.Click += btnMenosUn_Click;
            // 
            // UnidadesProduto
            // 
            UnidadesProduto.Location = new Point(431, 2);
            UnidadesProduto.Name = "UnidadesProduto";
            UnidadesProduto.Size = new Size(76, 23);
            UnidadesProduto.TabIndex = 2;
            UnidadesProduto.TextAlign = HorizontalAlignment.Center;
            UnidadesProduto.KeyDown += UnidadesProduto_KeyDown;
            // 
            // btnMaisUn
            // 
            btnMaisUn.Location = new Point(513, 2);
            btnMaisUn.Name = "btnMaisUn";
            btnMaisUn.Size = new Size(25, 25);
            btnMaisUn.TabIndex = 74;
            btnMaisUn.TabStop = false;
            btnMaisUn.Text = "+";
            btnMaisUn.UseVisualStyleBackColor = true;
            btnMaisUn.Click += btnMaisUn_Click;
            // 
            // btnSalvarNewUn
            // 
            btnSalvarNewUn.Location = new Point(400, 33);
            btnSalvarNewUn.Name = "btnSalvarNewUn";
            btnSalvarNewUn.Size = new Size(138, 23);
            btnSalvarNewUn.TabIndex = 75;
            btnSalvarNewUn.Text = "Salvar";
            btnSalvarNewUn.UseVisualStyleBackColor = true;
            btnSalvarNewUn.Click += btnSalvarNewUn_Click;
            // 
            // btnAtualizarLabels
            // 
            btnAtualizarLabels.Location = new Point(147, 30);
            btnAtualizarLabels.Name = "btnAtualizarLabels";
            btnAtualizarLabels.Size = new Size(138, 23);
            btnAtualizarLabels.TabIndex = 76;
            btnAtualizarLabels.TabStop = false;
            btnAtualizarLabels.Text = "Atualizar Labels";
            btnAtualizarLabels.UseVisualStyleBackColor = true;
            btnAtualizarLabels.Click += btnAtualizarLabels_Click;
            // 
            // btnObs
            // 
            btnObs.Location = new Point(3, 31);
            btnObs.Name = "btnObs";
            btnObs.Size = new Size(138, 23);
            btnObs.TabIndex = 77;
            btnObs.TabStop = false;
            btnObs.Text = "Adicionar Observações";
            btnObs.UseVisualStyleBackColor = true;
            btnObs.Click += btnObs_Click;
            // 
            // panelDev
            // 
            panelDev.Controls.Add(naoLimpaSKUCodebar);
            panelDev.Controls.Add(limpaSKUCodebar);
            panelDev.Controls.Add(labelSKUCodebarDEV);
            panelDev.Controls.Add(btnObs);
            panelDev.Controls.Add(textBoxSKUDEV);
            panelDev.Controls.Add(btnAtualizarLabels);
            panelDev.Controls.Add(btnMenosUn);
            panelDev.Controls.Add(btnSalvarNewUn);
            panelDev.Controls.Add(UnidadesProduto);
            panelDev.Controls.Add(btnMaisUn);
            panelDev.Location = new Point(623, 118);
            panelDev.Name = "panelDev";
            panelDev.Size = new Size(542, 209);
            panelDev.TabIndex = 78;
            panelDev.Visible = false;
            // 
            // limpaSKUCodebar
            // 
            limpaSKUCodebar.AutoSize = true;
            limpaSKUCodebar.Checked = true;
            limpaSKUCodebar.Location = new Point(427, 160);
            limpaSKUCodebar.Name = "limpaSKUCodebar";
            limpaSKUCodebar.Size = new Size(86, 19);
            limpaSKUCodebar.TabIndex = 78;
            limpaSKUCodebar.TabStop = true;
            limpaSKUCodebar.Text = "Limpar SKU";
            limpaSKUCodebar.UseVisualStyleBackColor = true;
            // 
            // naoLimpaSKUCodebar
            // 
            naoLimpaSKUCodebar.AutoSize = true;
            naoLimpaSKUCodebar.Location = new Point(427, 185);
            naoLimpaSKUCodebar.Name = "naoLimpaSKUCodebar";
            naoLimpaSKUCodebar.Size = new Size(111, 19);
            naoLimpaSKUCodebar.TabIndex = 79;
            naoLimpaSKUCodebar.Text = "Não Limpar SKU";
            naoLimpaSKUCodebar.UseVisualStyleBackColor = true;
            // 
            // preConfForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1500, 800);
            Controls.Add(panelDev);
            Controls.Add(btnFinalizarAgDEV);
            Controls.Add(panelProdutos);
            Controls.Add(btnVoltar);
            Controls.Add(btnQuit);
            Controls.Add(JauPesca);
            Controls.Add(panelConcluidos);
            Controls.Add(labelOpcional);
            Controls.Add(labelQTD);
            Controls.Add(textQTD);
            Controls.Add(labelCODE);
            Controls.Add(btnQuitDEV);
            Controls.Add(textBoxCODE);
            Controls.Add(panel8);
            Controls.Add(painelInfoEmpresa);
            Controls.Add(panel7);
            Controls.Add(panel6);
            Controls.Add(panel3);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "preConfForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "preConfForm";
            Load += preConfForm_Load;
            Shown += foco_Shown;
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            painelInfoEmpresa.ResumeLayout(false);
            groupBoxInfoEmpresa.ResumeLayout(false);
            groupBoxInfoEmpresa.PerformLayout();
            panel8.ResumeLayout(false);
            groupBoxInfoAg.ResumeLayout(false);
            groupBoxInfoAg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)JauPesca).EndInit();
            panelDev.ResumeLayout(false);
            panelDev.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion
        private Panel panel1;
        private Panel panelProdutos;
        private Panel panel3;
        private Panel panel6;
        private Panel panel7;
        private Panel painelInfoEmpresa;
        private GroupBox groupBoxInfoEmpresa;
        private Label labelEmpresa;
        private Label labelExDataEHora;
        private Label labelExColaborador;
        private Label labelExAgendamento;
        private Label labelExEmpresa;
        private Label labelData;
        private Label labelColaborador;
        private Label labelAgendamento;
        private Panel panel8;
        private GroupBox groupBoxInfoAg;
        private Label labelTempoPreConferencia;
        private Label labelTotalDeUnidades;
        private Panel panel12;
        private Panel panel11;
        private Panel panel10;
        private Panel panel9;
        private TextBox textBoxCODE;
        private Button btnQuitDEV;
        private Label labelCODE;
        private TextBox textQTD;
        private Label labelQTD;
        private Label labelOpcional;
        private Panel panelConcluidos;
        private PictureBox JauPesca;
        private Button btnQuit;
        private Label labelNomeJanela;
        private Button btnVoltar;
        private Label labelTempoEstimado;
        private Label labelTempo;
        private Panel panel5;
        private Button btnFinalizarAgDEV;
        private Label labelSKUCodebarDEV;
        private TextBox textBoxSKUDEV;
        private Button btnMenosUn;
        private TextBox UnidadesProduto;
        private Button btnMaisUn;
        private Button btnSalvarNewUn;
        private Button btnAtualizarLabels;
        private Button btnObs;
        private Panel panelDev;
        private RadioButton naoLimpaSKUCodebar;
        private RadioButton limpaSKUCodebar;
    }
}