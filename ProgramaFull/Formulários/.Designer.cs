namespace ProgramaFull.Formulários
{
    partial class DevelopersMenuPreConferencia
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DevelopersMenuPreConferencia));
            comboBoxProdutos = new ComboBox();
            labelProduto = new Label();
            labelTextoQuantidade = new Label();
            textBox1 = new TextBox();
            btnMaisQTD = new Button();
            btnMenosQTD = new Button();
            textBox2 = new TextBox();
            labelTextoProduto = new Label();
            btnSalvar = new Button();
            button1 = new Button();
            // 
            // comboBoxProdutos
            // 
            comboBoxProdutos.FormattingEnabled = true;
            comboBoxProdutos.Location = new Point(12, 44);
            comboBoxProdutos.Name = "comboBoxProdutos";
            comboBoxProdutos.Size = new Size(287, 23);
            comboBoxProdutos.TabIndex = 0;
            // 
            // labelProduto
            // 
            labelProduto.AutoSize = true;
            labelProduto.Font = new Font("Arial", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelProduto.Location = new Point(46, 18);
            labelProduto.Name = "labelProduto";
            labelProduto.Size = new Size(216, 16);
            labelProduto.TabIndex = 1;
            labelProduto.Text = "Selecione o produto para edição";
            // 
            // labelTextoQuantidade
            // 
            labelTextoQuantidade.AutoSize = true;
            labelTextoQuantidade.Font = new Font("Arial", 9.75F, FontStyle.Bold);
            labelTextoQuantidade.Location = new Point(110, 70);
            labelTextoQuantidade.Name = "labelTextoQuantidade";
            labelTextoQuantidade.Size = new Size(81, 16);
            labelTextoQuantidade.TabIndex = 2;
            labelTextoQuantidade.Text = "Quantidade";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(101, 88);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 3;
            // 
            // btnMaisQTD
            // 
            btnMaisQTD.Location = new Point(207, 88);
            btnMaisQTD.Name = "btnMaisQTD";
            btnMaisQTD.Size = new Size(25, 23);
            btnMaisQTD.TabIndex = 4;
            btnMaisQTD.Text = "+";
            btnMaisQTD.UseVisualStyleBackColor = true;
            // 
            // btnMenosQTD
            // 
            btnMenosQTD.Location = new Point(70, 88);
            btnMenosQTD.Name = "btnMenosQTD";
            btnMenosQTD.Size = new Size(25, 23);
            btnMenosQTD.TabIndex = 5;
            btnMenosQTD.Text = "-";
            btnMenosQTD.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(12, 136);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(287, 23);
            textBox2.TabIndex = 6;
            // 
            // labelTextoProduto
            // 
            labelTextoProduto.AutoSize = true;
            labelTextoProduto.Font = new Font("Arial", 9.75F, FontStyle.Bold);
            labelTextoProduto.Location = new Point(122, 118);
            labelTextoProduto.Name = "labelTextoProduto";
            labelTextoProduto.Size = new Size(57, 16);
            labelTextoProduto.TabIndex = 7;
            labelTextoProduto.Text = "Produto";
            // 
            // btnSalvar
            // 
            btnSalvar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSalvar.Location = new Point(20, 381);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(75, 23);
            btnSalvar.TabIndex = 8;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.Location = new Point(207, 381);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 9;
            button1.Text = "Excluir";
            button1.UseVisualStyleBackColor = true;
        }

        #endregion

        private ComboBox comboBoxProdutos;
        private Label labelProduto;
        private Label labelTextoQuantidade;
        private TextBox textBox1;
        private Button btnMaisQTD;
        private Button btnMenosQTD;
        private TextBox textBox2;
        private Label labelTextoProduto;
        private Button btnSalvar;
        private Button button1;
    }
}