using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json.Serialization;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using static ProgramaFull.Formulários.EmbalarEtiquetagemBIPE;
using System.Security.Cryptography.X509Certificates;
using ProgramaFull.Properties;

namespace ProgramaFull.Formulários
{
    public partial class EmbalarEtiquetagemBIPE : Form
    {
        public EmbalarEtiquetagemBIPE()
        {
            InitializeComponent();
            CarregarAnuncios(); // Chama o método ao inicializar o formulário
        }

        [DllImport("winspool.drv", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool OpenPrinter(string pPrinterName, out IntPtr phPrinter, IntPtr pDefault);

        [DllImport("winspool.drv", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool StartDocPrinter(IntPtr hPrinter, int Level, ref DOCINFO pDocInfo);

        [DllImport("winspool.drv", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct DOCINFO
        {
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pDataType;
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
            new VerAgendamentos().Show();
        } // OK

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Você está prestes a fechar essa o programa, pressione \"OK\" se estiver de acordo.", "AVISO", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
            }
        } // OK

        private void finalizarBtn_Click(object sender, EventArgs e)
        {
            CarregarAnuncios();
            if (listBoxAnuncios.Items.Count == 0)
            {
                VerificarFinalizacaoAgendamento();
            }
            else
            {
                MessageBox.Show("Ainda existem etiquetas que precisam ser impressas!", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void codigoProdutoTxtBox_KeyDown(object sender, KeyEventArgs e) // OK
        {

            //if (codigoProdutoTxtBox.Text.Trim() == Program.modoDevCODE)
            //{
            //    CarregarAnuncios();
            //    codigoProdutoTxtBox.Text = "";
            //}

            if (e.KeyCode == Keys.Enter)
            {
                VerificarFinalizacaoAgendamento();
                string codigoBipado = codigoProdutoTxtBox.Text.Trim();

                if (!string.IsNullOrEmpty(codigoBipado))
                {
                    // Define o caminho do arquivo JSON com base no número do agendamento
                    string caminhoJson = Path.Combine(@"P:\INFORMATICA\programas\FULL\KelvinV2\agendamentos",
                                                      $"{Program.nomePasta}",
                                                      $"{Program.nomePasta}_Embalar.json");

                    try
                    {
                        // Lê e desserializa o JSON
                        string jsonContent = File.ReadAllText(caminhoJson);
                        var dados = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonContent);

                        // 🔹 Buscar os produtos no JSON
                        var produtosEncontrados = BuscarProdutoNoJson(codigoBipado, dados);

                        if (produtosEncontrados.Count > 0)
                        {
                            var embalarForm = Application.OpenForms.OfType<EmbalarEtiquetagemBIPE>().FirstOrDefault();

                            // Chama o próximo método, passando a lista de objetos
                            ProcessarProdutosEncontrados(produtosEncontrados, embalarForm);
                        }
                        else
                        {
                            MessageBox.Show($"O código '{codigoBipado}' não foi encontrado no agendamento.\n\n" +
                                            "Verifique se digitou corretamente ou se o arquivo JSON está atualizado.",
                                            "Produto Não Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao processar o arquivo JSON: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Limpa o campo de texto após a verificação
                codigoProdutoTxtBox.Clear();
                e.SuppressKeyPress = true; // Impede o som do "beep" do sistema ao pressionar Enter
            }
        }
        private List<Dictionary<string, object>> BuscarProdutoNoJson(string codigoBipado, List<Dictionary<string, object>> dados) // OK
        {
            List<Dictionary<string, object>> produtosEncontrados = new List<Dictionary<string, object>>();

            // Percorre cada entrada no JSON
            foreach (var entrada in dados)
            {
                if (entrada.ContainsKey("Anuncio") && entrada.ContainsKey("Etiqueta") && entrada.ContainsKey("Qtd Etiquetas"))
                {
                    bool encontrado = false;

                    // 🔹 Caso 1: Produto é um KIT (tem "Composicao")
                    if (entrada.ContainsKey("Composicao") && entrada["Composicao"] is JsonElement composicaoElement && composicaoElement.ValueKind == JsonValueKind.Array)
                    {
                        foreach (JsonElement item in composicaoElement.EnumerateArray())
                        {
                            if ((item.TryGetProperty("SKU", out JsonElement skuElement) && skuElement.GetString() == codigoBipado) ||
                                (item.TryGetProperty("Codebar", out JsonElement codebarElement) && codebarElement.GetString() == codigoBipado))
                            {
                                encontrado = true; // Indica que o produto foi encontrado, mas continua verificando outros anúncios!
                            }
                        }
                    }
                    else
                    {
                        // 🔹 Caso 2: Produto Simples (sem "Composicao")
                        if ((entrada.ContainsKey("SKU") && entrada["SKU"].ToString() == codigoBipado) ||
                            (entrada.ContainsKey("Codebar") && entrada["Codebar"].ToString() == codigoBipado))
                        {
                            encontrado = true;
                        }
                    }

                    // Se encontrou, adiciona na lista de objetos (e não interrompe a busca!)
                    if (encontrado)
                    {
                        var produtoInfo = new Dictionary<string, object>
                        {
                            {"Etiqueta", entrada.ContainsKey("Etiqueta") ? entrada["Etiqueta"] : ""},
                            {"Anuncio", entrada["Anuncio"]},
                            {"ID", entrada["ID"]},
                            {"SKU", entrada.ContainsKey("SKU") ? entrada["SKU"] : ""},
                            {"Qtd Etiquetas", entrada["Qtd Etiquetas"]},
                            {"Composicao", entrada.ContainsKey("Composicao") ? "Kit" : "Simples"}
                        };

                        produtosEncontrados.Add(produtoInfo);
                    }
                }
            }

            return produtosEncontrados;
        }

        private async Task ProcessarProdutosEncontrados(List<Dictionary<string, object>> produtosEncontrados, Form embalarForm) // OK
        {
            // 🔹 Fechar qualquer formulário aberto antes de abrir um novo
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Text == "Kits Encontrados")
                {
                    openForm.Close();
                    break;
                }
            }

            // 🔹 Criar o formulário
            Form formKits = new Form
            {
                //Text = "Kits Encontrados",
                StartPosition = FormStartPosition.CenterScreen,
                Size = new Size(480, 670),
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.FromArgb(51, 153, 255)
            };

            // Painel principal onde vai ficar tudo (até os outros painéis)
            Panel painelPrincipal = new Panel
            {
                Size = new Size(460, 630),
                Location = new Point(10, 10),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // 🔹 Criar um painel com rolagem
            Panel scrollPanel = new Panel
            {
                AutoScroll = true,
                Dock = DockStyle.Fill
            };

            // 🔹 Criar um painel para exibir os kits e produtos
            FlowLayoutPanel panel = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(10),
                WrapContents = false
            };

            Panel panelBtnCancel = new Panel
            {
                Size = new Size(100, 30),
                Location = new Point(371, 637)
            };

            Button btnCancel = new Button
            {
                Text = "Cancelar",
                Size = new Size(60, 25),
                Dock = DockStyle.Bottom,
                Cursor = Cursors.Hand,
                BackColor = Color.WhiteSmoke,
                TabIndex = 0,
                TabStop = false
            };
            btnCancel.Click += (s, e) =>
            {
                embalarForm.Show();
                formKits.Close();
            };

            panelBtnCancel.Controls.Add(btnCancel);
            // 🔹 Adicionar título ao formulário
            Label titulo = new Label
            {
                Text = "O produto bipado foi encontrado nos Anúncios:",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.Black
            };
            panel.Controls.Add(titulo);

            // 🔹 Criar um HashSet para evitar anúncios duplicados
            HashSet<string> anunciosExibidos = new HashSet<string>();

            // 🔹 Caminho da pasta de etiquetas impressas
            string caminhoEtiquetas = Path.Combine(@"P:\INFORMATICA\programas\FULL\KelvinV2\agendamentos", $"{Program.nomePasta}", "Etiquetas");

            // 🔹 Adicionar os kits e produtos ao painel
            foreach (var produto in produtosEncontrados)
            {
                string idProduto = produto["ID"].ToString();
                string nomeAnuncio = produto["Anuncio"].ToString();
                string etiqueta = produto.ContainsKey("Etiqueta") ? produto["Etiqueta"].ToString() : "Sem etiqueta";
                string qtdEtiquetas = produto.ContainsKey("Qtd Etiquetas") ? produto["Qtd Etiquetas"].ToString() : "N/A";
                string tipoProduto = produto["Composicao"].ToString(); // Kit ou Simples

                // Evita adicionar o mesmo anúncio mais de uma vez, agora usando "Etiqueta" como chave única
                string anuncioUnico = $"{nomeAnuncio}-{etiqueta}";
                if (!anunciosExibidos.Contains(anuncioUnico))
                {
                    anunciosExibidos.Add(anuncioUnico);

                    // 🔹 Buscar a imagem do produto
                    string imagemUrl = await BuscarImagemProdutoTiny(idProduto);

                    // 🔹 Verifica se a etiqueta já foi impressa
                    bool jaImpresso = !string.IsNullOrEmpty(etiqueta) && File.Exists(Path.Combine(caminhoEtiquetas, $"{etiqueta}_Etiquetas.txt"));

                    // 🔹 Criar o painel do produto/kit
                    Panel kitPanel = new Panel
                    {
                        BorderStyle = BorderStyle.FixedSingle,
                        Width = 400,
                        Height = 130,
                        Padding = new Padding(5),
                        BackColor = jaImpresso ? Color.LightGray : Color.White, // Muda a cor do fundo se já foi impresso
                        Cursor = Cursors.Hand
                    };

                    // 🔹 Criar PictureBox para exibir a imagem
                    PictureBox pictureBox = new PictureBox
                    {
                        Size = new Size(100, 100),
                        Location = new Point(5, 5),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        ImageLocation = imagemUrl,
                        Cursor = Cursors.Hand
                    };

                    // 🔹 Criar Label com informações do produto
                    Label label = new Label
                    {
                        Text = $"Anúncio: {nomeAnuncio}\nEtiqueta: {etiqueta}\nQtd Etiquetas: {qtdEtiquetas}\nTipo: {tipoProduto}",
                        Location = new Point(110, 10),
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        ForeColor = Color.Black,
                        AutoSize = true,
                        MaximumSize = new Size(280, 0),
                        Cursor = Cursors.Hand
                    };

                    // 🔹 Se a etiqueta já foi impressa, adicionar um aviso visual
                    if (jaImpresso)
                    {
                        Label labelImpresso = new Label
                        {
                            Text = "JÁ FOI IMPRESSO",
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            ForeColor = Color.Red,
                            AutoSize = true,
                            Location = new Point(270, 100) // Posiciona no canto inferior direito
                        };
                        kitPanel.Controls.Add(labelImpresso);

                        // Adiciona um evento de clique que exibe a mensagem
                        //kitPanel.Click += (s, e) => MessageBox.Show("Já foi impresso pow", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //pictureBox.Click += (s, e) => MessageBox.Show("Já foi impresso pow", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //label.Click += (s, e) => MessageBox.Show("Já foi impresso pow", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //labelImpresso.Click += (s, e) => MessageBox.Show("Já foi impresso pow", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        string etiquetaLocal = etiqueta;

                        kitPanel.Click += (s, e) =>
                        {
                            //formKits.Visible = false;
                            VerificarEImprimirEtiquetas(etiquetaLocal);   
                        };

                        pictureBox.Click += (s, e) =>
                        {
                            //formKits.Visible = false;
                            VerificarEImprimirEtiquetas(etiquetaLocal);
                        };

                        label.Click += (s, e) => 
                        {
                            //formKits.Visible = false;
                            VerificarEImprimirEtiquetas(etiquetaLocal); 
                        };

                        labelImpresso.Click += (s, e) => 
                        {
                            //formKits.Visible = false;
                            VerificarEImprimirEtiquetas(etiquetaLocal);
                        };
                    }
                    else
                    {

                        // Cria cópias locais das variáveis dentro do loop para evitar captura errada
                        string idProdutoLocal = idProduto;
                        string nomeAnuncioLocal = nomeAnuncio;
                        string imagemUrlLocal = imagemUrl;
                        string etiquetaLocal = etiqueta;

                        // Cria cópia específica para cada produto no evento de clique
                        Dictionary<string, object> produtoSelecionado = new Dictionary<string, object>(produto);
                        var embalarEtiquetagemBIPE = Application.OpenForms.OfType<EmbalarEtiquetagemBIPE>().FirstOrDefault();
                        // 🔹 Evento de clique para abrir o formulário de confirmação
                        kitPanel.Click += (s, e) =>
                        {
                            formKits.Hide();
                            AbrirFormularioConfirmacao(idProdutoLocal, nomeAnuncioLocal, imagemUrlLocal, new List<Dictionary<string, object>> { produtoSelecionado }, formKits, embalarEtiquetagemBIPE);
                        };

                        pictureBox.Click += (s, e) => 
                        {
                            formKits.Hide();
                            AbrirFormularioConfirmacao(idProdutoLocal, nomeAnuncioLocal, imagemUrlLocal, new List<Dictionary<string, object>> { produtoSelecionado }, formKits, embalarEtiquetagemBIPE);
                        };

                        label.Click += (s, e) => 
                        {
                            formKits.Hide();
                           AbrirFormularioConfirmacao(idProdutoLocal, nomeAnuncioLocal, imagemUrlLocal, new List<Dictionary<string, object>> { produtoSelecionado }, formKits, embalarEtiquetagemBIPE);
                        };

                    }

                    // 🔹 Adicionar elementos ao painel do produto
                    kitPanel.Controls.Add(pictureBox);
                    kitPanel.Controls.Add(label);
                    panel.Controls.Add(kitPanel);
                }
            }

            // 🔹 Adicionar o painel de rolagem e exibir o formulário
            scrollPanel.Controls.Add(panel);
            formKits.Controls.Add(painelPrincipal);
            formKits.Controls.Add(panelBtnCancel);
            painelPrincipal.Controls.Add(scrollPanel);

            embalarForm.Hide();
            formKits.ShowDialog();
        }

        private async void AbrirFormularioConfirmacao(string idProduto, string nomeAnuncio, string imagemUrl, List<Dictionary<string, object>> produtosEncontrados, Form formKits, Form embalarEtiquetagemBIPE)
        {
            // 🔹 Encontrar os dados do produto no JSON
            var produtoData = produtosEncontrados.Where(p => p["ID"].ToString() == idProduto).ToList();

            if (produtoData == null || produtoData.Count == 0)
            {
                MessageBox.Show("Erro ao encontrar os dados do produto no JSON.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 🔹 Verificar se é um KIT ou um produto simples
            bool isKit = produtoData.Any(p => p.ContainsKey("Composicao"));

            // 🔹 Criar o formulário
            Form formConfirmacao = new Form
            {
                //Text = isKit ? "Confirmação do Kit" : "Confirmação do Produto",
                StartPosition = FormStartPosition.CenterScreen,
                AutoSize = false, // Kelvin você precisa terminar o Frontend em relação aos kits, formulário de kits criado dinâmicamente
                Size = new Size(460, 590),
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.FromArgb(51, 153, 255)
            };

            FlowLayoutPanel panel = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(10),
                WrapContents = false,
            };

            // Painel principal onde vai ficar tudo (até os outros painéis)
            Panel painelPrincipal = new Panel
            {
                Size = new Size(440, 570),
                Location = new Point(10, 10),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // 🔹 Adiciona título ao formulário
            Label titulo = new Label
            {
                Text = isKit ? "Confirme os produtos do kit" : "Confirme o produto bipado",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Size = new Size(440, 20),
                Location = new Point(0, 0),
                ForeColor = Color.Black,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.TopCenter
            };

            // 🔹 Adiciona imagem do produto/kit
            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(440, 250),
                Location = new Point(0, 30),
                SizeMode = PictureBoxSizeMode.Zoom,
                ImageLocation = imagemUrl,
            };

            // 🔹 Criar painel para os produtos do kit ou produto simples
            FlowLayoutPanel composicaoPanel = new FlowLayoutPanel
            {
                AutoSize = false,
                Size = new Size(420, 200),
                Location = new Point(10, 290),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGray,
                AutoScroll = true
            };

            Dictionary<string, Label> produtosLabels = new Dictionary<string, Label>();
            Dictionary<string, int> quantidadeBipada = new Dictionary<string, int>();
            List<ProdutoComposicao> composicao = new List<ProdutoComposicao>();

            if (isKit)
            {
                // Buscar a composição do kit em tempo real
                composicao = await BuscarComposicaoDoKit(idProduto) ?? new List<ProdutoComposicao>();

                if (composicao.Count == 0)
                {
                    //MessageBox.Show("Erro ao buscar a composição do kit. Tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return;
                    // Se for um produto simples, impedir a execução de código que busca composição
                    var embalarForm = Application.OpenForms.OfType<EmbalarEtiquetagemBIPE>().FirstOrDefault();
                    ExibirFormularioProdutoSimples(produtoData.First(), imagemUrl, formKits, embalarForm);
                    return;
                }

                //// Painel Principal onde vai ter todas as coisas e dar uma bordinha maneira
                //Panel painelPrincipal = new Panel
                //{
                //    Size = new Size(422, 410),
                //    Location = new Point(10, 10),
                //    BackColor = Color.White,
                //    BorderStyle = BorderStyle.FixedSingle
                //};

                foreach (var produto in composicao)
                {
                    quantidadeBipada[produto.SKU] = 0;

                    Label produtoLabel = new Label
                    {
                        Text = $"{produto.Descricao} - {produto.Quantidade} Unidades",
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        ForeColor = Color.Red,
                        AutoSize = true                        
                    };

                    produtosLabels[produto.SKU] = produtoLabel;
                    composicaoPanel.Controls.Add(produtoLabel);
                }

                //Location = new Point(355, 534),
                //Size = new Size(75, 26),
                // Botão cancelar infos

                // Botão para atualizar a composição em tempo real
                Button btnAtualizar = new Button
                {
                    Text = "",
                    AutoSize = false,
                    Size = new Size(26, 26),
                    Location = new Point(324, 534),
                    BackColor = Color.WhiteSmoke,
                    BackgroundImage = Resources.btnRefresh,
                    BackgroundImageLayout = ImageLayout.Zoom,
                    TabStop = false                
                };
                btnAtualizar.Click += async (s, e) =>
                {
                    composicaoPanel.Controls.Clear();
                    composicao = await BuscarComposicaoDoKit(idProduto);

                    foreach (var produto in composicao)
                    {
                        quantidadeBipada[produto.SKU] = 0;

                        Label produtoLabel = new Label
                        {
                            Text = $"{produto.Descricao} - {produto.Quantidade} Unidades",
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            ForeColor = Color.Red,
                            AutoSize = true
                        };

                        produtosLabels[produto.SKU] = produtoLabel;
                        composicaoPanel.Controls.Add(produtoLabel);
                    }
                };
                painelPrincipal.Controls.Add(btnAtualizar);
            }

            // 🔹 Criar TextBox para bipagem
            TextBox textBoxBipagem = new TextBox
            {
                Width = 420,
                Location = new Point(10, 500)
            };
            textBoxBipagem.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string bipado = textBoxBipagem.Text.Trim();
                    bool produtoEncontrado = false;

                    if (isKit)
                    {
                        foreach (var produto in quantidadeBipada.Keys.ToList())
                        {
                            if (produto == bipado)
                            {
                                quantidadeBipada[produto]++;

                                if (quantidadeBipada[produto] >= composicao.First(p => p.SKU == produto).Quantidade)
                                {
                                    produtosLabels[produto].ForeColor = Color.Green;
                                }

                                produtoEncontrado = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (var produto in produtoData)
                        {
                            if (produto["SKU"].ToString() == bipado || (produto.ContainsKey("Codebar") && produto["Codebar"].ToString() == bipado))
                            {
                                produtoEncontrado = true;
                                break;
                            }
                        }
                    }

                    textBoxBipagem.Clear();

                    if (!produtoEncontrado)
                    {
                        MessageBox.Show("Produto inválido ou já bipado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (isKit && quantidadeBipada.All(p => p.Value >= composicao.First(c => c.SKU == p.Key).Quantidade))
                    {
                        MessageBox.Show("Kit confirmado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Criar o objeto da etiqueta com os detalhes do kit
                        Etiqueta etiquetaObj = new Etiqueta
                        {
                            EtiquetaId = produtoData.First()["Etiqueta"].ToString(),
                            Anuncio = produtoData.First()["Anuncio"].ToString(),
                            QtdEtiquetas = int.Parse(produtoData.First()["Qtd Etiquetas"].ToString())
                        };

                        // Criar objeto do produto baseado no kit
                        Produto produtoObj = new Produto
                        {
                            SKU = produtoData.First()["SKU"].ToString(),
                            NomeProduto = produtoData.First()["Anuncio"].ToString(),
                        };

                        // Caminho do JSON
                        string caminhoJson = Path.Combine(@"P:\INFORMATICA\programas\FULL\KelvinV2\agendamentos", Program.nomePasta.ToString(), $"{Program.nomePasta}_Embalar.json");

                        // Gerar arquivo de etiquetas para o kit
                        string etiquetasZPL = GerarEtiquetas(etiquetaObj, produtoObj, caminhoJson);

                        // Exibir confirmação
                        MessageBox.Show("Arquivo de etiquetas do kit gerado com sucesso!", "Etiqueta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 🔹 Envia para a impressora
                        EnviarArquivoParaImpressora(etiquetasZPL);

                        CarregarAnuncios();
                        formConfirmacao.Close();
                        formKits.Close();
                        VerificarFinalizacaoAgendamento();
                    }
                }
            };


            // Botão de Cancelar
            Button btnCancelar = new Button
            {
                Text = "Cancelar",
                AutoSize = false,
                Location = new Point(355, 534),
                Size = new Size(75, 26),
                BackColor = Color.WhiteSmoke,
                TabStop = false
            };
            btnCancelar.Click += (s, e) =>
            {
                embalarEtiquetagemBIPE?.Show();
                formConfirmacao.Close();
            };

            painelPrincipal.Controls.Add(btnCancelar);

            painelPrincipal.Controls.Add(titulo);
            painelPrincipal.Controls.Add(pictureBox);
            painelPrincipal.Controls.Add(composicaoPanel);
            painelPrincipal.Controls.Add(textBoxBipagem);            

            formConfirmacao.Controls.Add(painelPrincipal);
            formConfirmacao.Controls.Add(panel);
            formConfirmacao.ShowDialog();
        }

        private void ExibirFormularioProdutoSimples(Dictionary<string, object> produto, string imagemUrl, Form formKits, Form embalarEtiquetagemBIPE)
        {
            Form formProdutoSimples = new Form
            {
                //Text = "Confirmação do Produto",
                StartPosition = FormStartPosition.CenterScreen,
                AutoSize = false,
                Size = new Size(442, 450),
                // AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.FromArgb(51, 153, 255)
            };

            FlowLayoutPanel panel = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(10),
                WrapContents = false
            };

            // 🔹 Painel principal, onde vai ser colocado tudo
            Panel painelPrincipal = new Panel
            {
                Size = new Size(422, 410),
                Location = new Point(10, 10),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // 🔹 Painel para colocar o botão lá embaixo
            Panel painelBtnCancel = new Panel
            {
                AutoSize = false,
                Size = new Size(75, 30),
                Location = new Point(357, 420),
                BackColor = Color.FromArgb(51, 153, 255)
            };

            Panel panelInfoProduto = new Panel
            {
                AutoSize = false,
                Size = new Size(400, 50),
                Margin = new Padding(0)
            };

            // 🔹 Adicionar título
            Label titulo = new Label
            {
                Text = "Confirme o produto bipado:",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(250, 25),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.TopCenter,
                ForeColor = Color.Black
            };

            // 🔹 Adicionar imagem do produto
            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(250, 250),
                SizeMode = PictureBoxSizeMode.Zoom,
                ImageLocation = imagemUrl,
                Dock = DockStyle.Top
            };            

            // 🔹 Exibir informações do produto
            // Nome do Anúncio
            Label produtoInfoAnuncio = new Label
            {
                Text = $"Anúncio: ",
                Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular),
                ForeColor = Color.Black,
                AutoSize = false,
                Size = new Size(64, 25)
            };
            Label produtoAnuncio = new Label 
            {
                Text = $"{produto["Anuncio"]}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(produtoInfoAnuncio.Width - 4, 0)
            };

            // Código da Etiqueta
            Label produtoInfoEtiqueta = new Label
            {
                Text = $"Etiqueta: ",
                Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular),
                ForeColor = Color.Black,
                AutoSize = false,
                Size = new Size(63, 20),
                Location = new Point(0, produtoInfoAnuncio.Height) 
            };
            Label produtoEtiqueta = new Label
            {
                Text = $"{produto["Etiqueta"]}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(produtoInfoEtiqueta.Width - 4, produtoInfoAnuncio.Height)
            };

            // Quantidade Etiqueta
            Label produtoInfoQtdEtiquetas = new Label
            {
                Text = $"Qtd Etiquetas: ",
                Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular),
                ForeColor = Color.Black,
                AutoSize = false,
                Size = new Size(100, 25),
                Location = new Point(produtoInfoEtiqueta.Width + 190, produtoInfoAnuncio.Height)
            };
            
            Label produtoQtdEtiquetas = new Label
            {
                Text = $"{produto["Qtd Etiquetas"]}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(produtoInfoQtdEtiquetas.Width + 248, produtoInfoAnuncio.Height)
            };
            
            // 🔹 Criar TextBox para bipagem do produto
            TextBox textBoxBipagem = new TextBox 
            { 
                Width = 400 ,
                Dock = DockStyle.Bottom
            };

            textBoxBipagem.KeyDown += async (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string bipado = textBoxBipagem.Text.Trim();
                    // Buscar código de barras atualizado via API
                    string codigoBarrasTiny = await BuscarCodigoBarrasProdutoTiny(produto["ID"].ToString());

                    if (bipado == produto["SKU"].ToString() || bipado == codigoBarrasTiny)
                    {
                        MessageBox.Show("Produto confirmado!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        formProdutoSimples.Close();

                        // Monta os dados da etiqueta e do produto
                        Etiqueta etiquetaObj = new Etiqueta
                        {
                            EtiquetaId = produto["Etiqueta"].ToString(),
                            Anuncio = produto["Anuncio"].ToString(),
                            QtdEtiquetas = int.Parse(produto["Qtd Etiquetas"].ToString())
                        };

                        Produto produtoObj = new Produto
                        {
                            SKU = produto["SKU"].ToString(),
                            NomeProduto = produto["Anuncio"].ToString(),
                            CodigoBarras = produto.ContainsKey("Codebar") ? produto["Codebar"].ToString() : ""
                        };

                        // Caminho do JSON atual
                        string caminhoJson = Path.Combine(@"P:\INFORMATICA\programas\FULL\KelvinV2\agendamentos", Program.nomePasta.ToString(), $"{Program.nomePasta}_Embalar.json");

                        // Gera o arquivo de etiquetas
                        string etiquetasZPL = GerarEtiquetas(etiquetaObj, produtoObj, caminhoJson);

                        // (Opcional) Exibir confirmação ou log
                        MessageBox.Show("Arquivo de etiquetas gerado com sucesso!", "Etiqueta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 🔹 Envia para a impressora
                        EnviarArquivoParaImpressora(etiquetasZPL);

                        CarregarAnuncios();
                        formProdutoSimples.Close();
                        formKits.Close();
                        VerificarFinalizacaoAgendamento();
                    }
                    else
                    {
                        MessageBox.Show("Código de barras ou SKU incorreto!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxBipagem.Clear();
                    }
                }
            };

            // 🔹 Botão de cancelar
            Button btnCancelar = new Button
            {
                Text = "Cancelar",
                AutoSize = false,
                //Dock = DockStyle.Right,
                //TextAlign = ContentAlignment.MiddleCenter,                
                Size = new Size(75, 26),
                BackColor = Color.WhiteSmoke,
                TabStop = false,
                Cursor = Cursors.Hand
            };
            btnCancelar.Click += (s, e) =>
            {
                embalarEtiquetagemBIPE.Show(); // Provisório
                formProdutoSimples.Close();
            };
            

            panel.Controls.Add(titulo);
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(panelInfoProduto);
            panel.Controls.Add(textBoxBipagem);
            panelInfoProduto.Controls.Add(produtoAnuncio);
            panelInfoProduto.Controls.Add(produtoInfoAnuncio);
            panelInfoProduto.Controls.Add(produtoInfoEtiqueta);
            panelInfoProduto.Controls.Add(produtoEtiqueta);
            panelInfoProduto.Controls.Add(produtoInfoQtdEtiquetas);
            panelInfoProduto.Controls.Add(produtoQtdEtiquetas);
            painelPrincipal.Controls.Add(panel);
            painelBtnCancel.Controls.Add(btnCancelar);
            formProdutoSimples.Controls.Add(painelBtnCancel);
            formProdutoSimples.Controls.Add(painelPrincipal);            
            formProdutoSimples.Show();
        }

        private string GerarEtiquetas(Etiqueta etiqueta, Produto produto, string caminhoJson)
        {
            StringBuilder zplCompleto = new StringBuilder();
            int xColunaEsquerda = 30;
            int xColunaDireita = 350;
            int etiquetasGeradas = 0;
            int linhasNecessarias = (int)Math.Ceiling(etiqueta.QtdEtiquetas / 2.0);

            for (int linha = 0; linha < linhasNecessarias; linha++)
            {
                // Início de cada label set
                zplCompleto.Append("^XA^CI28\n");
                zplCompleto.Append("^LH0,0\n");

                // Primeira etiqueta da linha (Coluna Esquerda)
                if (etiquetasGeradas < etiqueta.QtdEtiquetas)
                {
                    zplCompleto.Append($@"
^FO{xColunaEsquerda},15^BY2,,0^BCN,54,N,N^FD{etiqueta.EtiquetaId}^FS
^FO{xColunaEsquerda + 75},75^A0N,20,25^FH^FD{etiqueta.EtiquetaId}^FS
^FO{xColunaEsquerda + 75},76^A0N,20,25^FH^FD{etiqueta.EtiquetaId}^FS
^FO{xColunaEsquerda - 14},115^A0N,18,18^FB300,2,2,L^FH^FD{etiqueta.Anuncio}^FS
^FO{xColunaEsquerda - 14},172^A0N,18,18^FH^FDSKU: {produto.SKU}^FS");
                    etiquetasGeradas++;
                }

                // Segunda etiqueta da linha (Coluna Direita)
                if (etiquetasGeradas < etiqueta.QtdEtiquetas)
                {
                    zplCompleto.Append($@"
^FO{xColunaDireita},15^BY2,,0^BCN,54,N,N^FD{etiqueta.EtiquetaId}^FS
^FO{xColunaDireita + 75},75^A0N,20,25^FH^FD{etiqueta.EtiquetaId}^FS
^FO{xColunaDireita + 75},76^A0N,20,25^FH^FD{etiqueta.EtiquetaId}^FS
^FO{xColunaDireita - 14},115^A0N,18,18^FB300,2,2,L^FH^FD{etiqueta.Anuncio}^FS
^FO{xColunaDireita - 14},172^A0N,18,18^FH^FDSKU: {produto.SKU}^FS");
                    etiquetasGeradas++;
                }

                // Fim de cada label set
                zplCompleto.Append("\n^XZ\n");
            }

            // Criar diretório "Etiquetas" se não existir
            string diretorio = Path.GetDirectoryName(caminhoJson);
            string dirEtiquetas = Path.Combine(diretorio, "Etiquetas");
            if (!Directory.Exists(dirEtiquetas))
            {
                Directory.CreateDirectory(dirEtiquetas);
            }


            // Salvar o conteúdo ZPL no diretório "Etiquetas" para depuração

            string nomeArquivo = Path.Combine(dirEtiquetas, $"{etiqueta.EtiquetaId}_Etiquetas.txt");
            if (!File.Exists(nomeArquivo))
            {
                File.WriteAllText(nomeArquivo, zplCompleto.ToString());
                return zplCompleto.ToString();
            }

            return zplCompleto.ToString();
        }

        public static bool EnviarArquivoParaImpressora(string fileContent)
        {
            string printerName = Program.impressoraEtiqueta; // Impressora definida no programa
            if (string.IsNullOrEmpty(printerName))
            {
                MessageBox.Show("Nenhuma impressora configurada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            IntPtr hPrinter = IntPtr.Zero;
            int dwWritten = 0;
            int lastError;

            try
            {
                // Abre a impressora
                if (!OpenPrinter(printerName, out hPrinter, IntPtr.Zero))
                {
                    lastError = Marshal.GetLastWin32Error();
                    throw new Exception($"Não foi possível abrir a impressora {printerName}. Erro: {lastError}");
                }

                // Configura os parâmetros do documento
                DOCINFO di = new DOCINFO
                {
                    pDocName = "Etiqueta ZPL",
                    pDataType = "RAW",
                    pOutputFile = null
                };

                // Inicia o documento na impressora
                if (!StartDocPrinter(hPrinter, 1, ref di))
                {
                    lastError = Marshal.GetLastWin32Error();
                    throw new Exception($"Erro ao iniciar o documento na impressora. Erro: {lastError}");
                }

                if (!StartPagePrinter(hPrinter))
                {
                    lastError = Marshal.GetLastWin32Error();
                    throw new Exception($"Erro ao iniciar a página na impressora. Erro: {lastError}");
                }

                // Converte a string para bytes
                byte[] bytes = Encoding.ASCII.GetBytes(fileContent);
                IntPtr pUnmanagedBytes = Marshal.AllocCoTaskMem(bytes.Length);
                Marshal.Copy(bytes, 0, pUnmanagedBytes, bytes.Length);

                // Envia os dados para a impressora
                if (!WritePrinter(hPrinter, pUnmanagedBytes, bytes.Length, out dwWritten))
                {
                    lastError = Marshal.GetLastWin32Error();
                    Marshal.FreeCoTaskMem(pUnmanagedBytes);
                    throw new Exception($"Erro ao enviar dados para a impressora. Erro: {lastError}");
                }

                Marshal.FreeCoTaskMem(pUnmanagedBytes);

                // Finaliza a página e o documento
                EndPagePrinter(hPrinter);
                EndDocPrinter(hPrinter);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao imprimir etiquetas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (hPrinter != IntPtr.Zero)
                {
                    ClosePrinter(hPrinter);
                }
            }
        }

        public static async Task<ProdutoComposicao> BuscarDetalhesProdutoTiny(string idProduto)
        {
            if (string.IsNullOrEmpty(Program.accessTokenTinyV3))
            {
                await Program.BuscarAccessTokenTinyAsync();
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Program.accessTokenTinyV3);

                    string url = $"https://api.tiny.com.br/public-api/v3/produtos/{idProduto}";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        await Program.BuscarAccessTokenTinyAsync();
                        return await BuscarDetalhesProdutoTiny(idProduto);
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var produto = JsonSerializer.Deserialize<ProdutoTinyResponse>(jsonResponse, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        return new ProdutoComposicao
                        {
                            ID = produto.Id,
                            SKU = produto.Sku,
                            Descricao = produto.Descricao,
                            CodigoBarras = produto.Gtin
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar detalhes do produto: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public static async Task<string> BuscarCodigoBarrasProdutoTiny(string idProduto)
        {
            if (string.IsNullOrEmpty(Program.accessTokenTinyV3))
            {
                // Buscar o Access Token se ainda não foi buscado
                await Program.BuscarAccessTokenTinyAsync();
            }

            // Verifica se o token foi obtido
            if (string.IsNullOrEmpty(Program.accessTokenTinyV3))
            {
                MessageBox.Show("Erro ao obter o Access Token.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty; // Retorna uma string vazia caso falhe
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Program.accessTokenTinyV3);

                    string url = $"https://api.tiny.com.br/public-api/v3/produtos/{idProduto}";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var produto = JsonSerializer.Deserialize<ProdutoTinyResponse>(jsonResponse, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (produto != null)
                        {
                            string codigoBarras = produto.Gtin ?? string.Empty;
                            return codigoBarras;
                        }

                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        // Caso a chave de API esteja inválida, buscar um novo token e tentar novamente
                        await Program.BuscarAccessTokenTinyAsync();
                        return await BuscarCodigoBarrasProdutoTiny(idProduto); // Tenta novamente
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar código de barras no Tiny: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return string.Empty; // Retorna uma string vazia caso não encontre
        }

        private async Task<List<ProdutoComposicao>> BuscarComposicaoDoKit(string idProduto)
        {
            if (string.IsNullOrEmpty(Program.accessTokenTinyV3))
            {
                await Program.BuscarAccessTokenTinyAsync();
            }

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Program.accessTokenTinyV3);
                string url = $"https://api.tiny.com.br/public-api/v3/produtos/{idProduto}";

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    // Tentar novamente buscando nova chave
                    await Program.BuscarAccessTokenTinyAsync();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Program.accessTokenTinyV3);
                    response = await client.GetAsync(url);
                }

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var produto = JsonSerializer.Deserialize<ProdutoTinyResponse>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (produto != null && produto.Kit != null && produto.Kit.Count > 0)
                    {
                        return produto.Kit.Select(p => new ProdutoComposicao
                        {
                            ID = p.Produto.ID,
                            SKU = p.Produto.SKU,
                            Descricao = p.Produto.Descricao,
                            Quantidade = p.Quantidade
                        }).ToList();
                    }
                }
            }
            return null; // Retorna null se não for um kit
        }

        private void VerificarEImprimirEtiquetas(string etiqueta)
        {
            DialogResult resultado = MessageBox.Show(
                "As etiquetas já foram impressas anteriormente. Deseja reimprimir?",
                "Reimpressão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                // 🔹 Solicitar senha antes de reimprimir
                string senhaDigitada = Microsoft.VisualBasic.Interaction.InputBox("Digite a senha para continuar:", "Autenticação", "", -1, -1);

                if (senhaDigitada == Program.modoDevCODE)
                {
                    // 🔹 Exibir formulário de seleção de impressão
                    ExibirFormularioSelecaoEtiquetas(etiqueta);
                }
                else
                {
                    MessageBox.Show("Senha incorreta. Operação cancelada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            // }
            // else
            // {
            // Se não existe o arquivo, gerar e imprimir normalmente
            // ImprimirEtiquetasNovas();
            // }
        }

        private void ExibirFormularioSelecaoEtiquetas(string etiquetaId)
        {
            Form formSelecao = new Form()
            {
                Text = "Selecionar Quantidade de Etiquetas",
                StartPosition = FormStartPosition.CenterScreen,
                Size = new Size(400, 200),
                FormBorderStyle = FormBorderStyle.FixedDialog
            };

            Label labelPergunta = new Label()
            {
                Text = "Quantas etiquetas deseja imprimir?",
                Location = new Point(10, 10),
                AutoSize = true
            };

            RadioButton radioTodas = new RadioButton()
            {
                Text = "Todas as etiquetas",
                Location = new Point(10, 40),
                Checked = true
            };

            RadioButton radioPersonalizado = new RadioButton()
            {
                Text = "Escolher quantidade:",
                Location = new Point(10, 70)
            };

            TextBox txtQuantidade = new TextBox()
            {
                Location = new Point(150, 68),
                Width = 50,
                Enabled = false
            };

            radioPersonalizado.CheckedChanged += (s, e) => txtQuantidade.Enabled = radioPersonalizado.Checked;

            Button btnImprimir = new Button()
            {
                Text = "Enviar para Impressão",
                Location = new Point(10, 110),
                Width = 150
            };

            btnImprimir.Click += (s, e) =>
            {
                string caminhoArquivo = $@"P:\INFORMATICA\programas\FULL\KelvinV2\agendamentos\{Program.nomePasta}\Etiquetas\{etiquetaId}_Etiquetas.txt";
                string fileContent;

                if (radioTodas.Checked)
                {
                    // 🔹 Verifica se o arquivo já existe antes de imprimir
                    if (!File.Exists(caminhoArquivo))
                    {
                        MessageBox.Show("O arquivo de etiquetas não foi encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    fileContent = File.ReadAllText(caminhoArquivo);
                    EnviarArquivoParaImpressora(fileContent);
                }
                else if (radioPersonalizado.Checked && int.TryParse(txtQuantidade.Text, out int qtd) && qtd > 0)
                {
                    // Buscar os dados reais do produto pelo etiquetaId
                    string caminhoJsonBusca = Path.Combine(@"P:\INFORMATICA\programas\FULL\KelvinV2\agendamentos", $"{Program.nomePasta}", $"{Program.nomePasta}_Embalar.json");

                    if (!File.Exists(caminhoJsonBusca))
                    {
                        MessageBox.Show("Arquivo JSON não encontrado para buscar os dados do produto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string jsonContent = File.ReadAllText(caminhoJsonBusca);
                    var dados = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonContent);

                    var entrada = dados.FirstOrDefault(p => p.ContainsKey("Etiqueta") && p["Etiqueta"].ToString() == etiquetaId);

                    if (entrada != null)
                    {
                        Etiqueta novaEtiqueta = new Etiqueta
                        {
                            EtiquetaId = etiquetaId,
                            Anuncio = entrada.ContainsKey("Anuncio") ? entrada["Anuncio"].ToString() : "Sem Anúncio",
                            QtdEtiquetas = qtd
                        };

                        Produto produto = new Produto
                        {
                            SKU = entrada.ContainsKey("SKU") ? entrada["SKU"].ToString() : "Sem SKU",
                            NomeProduto = novaEtiqueta.Anuncio
                        };

                        fileContent = GerarEtiquetas(novaEtiqueta, produto, caminhoArquivo);

                        if (!string.IsNullOrEmpty(fileContent))
                        {
                            EnviarArquivoParaImpressora(fileContent);
                        }
                        else
                        {
                            MessageBox.Show("Erro ao gerar etiquetas!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível localizar os dados do produto no JSON.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                MessageBox.Show("Etiquetas enviadas para impressão!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                formSelecao.Close();
            };

            formSelecao.Controls.Add(labelPergunta);
            formSelecao.Controls.Add(radioTodas);
            formSelecao.Controls.Add(radioPersonalizado);
            formSelecao.Controls.Add(txtQuantidade);
            formSelecao.Controls.Add(btnImprimir);
            formSelecao.Show();
        }

        public static async Task<string> BuscarImagemProdutoTiny(string idProduto)
        {
            if (string.IsNullOrEmpty(Program.accessTokenTinyV3))
            {
                await Program.BuscarAccessTokenTinyAsync();
            }

            if (string.IsNullOrEmpty(Program.accessTokenTinyV3))
            {
                MessageBox.Show("Erro ao obter o Access Token.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "https://i.imgur.com/tWvwe0s.png"; // Imagem padrão caso falhe
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Program.accessTokenTinyV3);
                    string url = $"https://api.tiny.com.br/public-api/v3/produtos/{idProduto}";

                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var produto = JsonSerializer.Deserialize<ProdutoTinyResponse>(jsonResponse, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (produto != null)
                        {
                            // ✅ Se o produto tem anexos (imagens), retorna a primeira disponível
                            if (produto.Anexos != null && produto.Anexos.Count > 0)
                            {
                                return produto.Anexos.First().Url;
                            }

                            // ✅ Se for um kit e não tiver imagem própria, busca a imagem do primeiro item do kit
                            if (produto.Kit != null && produto.Kit.Count > 0)
                            {
                                string idProdutoKit = produto.Kit.First().Produto.ID.ToString();
                                return await BuscarImagemProdutoTiny(idProdutoKit); // Busca a imagem do primeiro item do kit
                            }
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        await Program.BuscarAccessTokenTinyAsync();
                        return await BuscarImagemProdutoTiny(idProduto);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar imagem no Tiny: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return "https://i.imgur.com/tWvwe0s.png"; // Imagem padrão caso não encontre
        }

        /// <summary>
        /// Método público para atualizar a listBoxAnuncios verificando etiquetas já impressas.
        /// </summary>
        /// 

        public void CarregarAnuncios()
        {
            string caminhoJson = Path.Combine(@"P:\INFORMATICA\programas\FULL\KelvinV2\agendamentos", $"{Program.nomePasta}", $"{Program.nomePasta}_Embalar.json");
            string caminhoEtiquetas = Path.Combine(@"P:\INFORMATICA\programas\FULL\KelvinV2\agendamentos", $"{Program.nomePasta}", "Etiquetas");

            try
            {
                if (!File.Exists(caminhoJson))
                {
                    MessageBox.Show("Arquivo JSON de anúncios não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ Lê e desserializa o JSON corretamente
                string jsonContent = File.ReadAllText(caminhoJson, Encoding.UTF8);
                var dados = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, // Ignora diferenças de maiúsculas e minúsculas nas chaves
                    AllowTrailingCommas = true // Permite JSON mal formatado com vírgulas extras
                });

                if (dados == null || dados.Count == 0)
                {
                    MessageBox.Show("O arquivo JSON está vazio ou corrompido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Obtém a lista de etiquetas já impressas
                List<string> etiquetasImpressas = new List<string>();

                if (Directory.Exists(caminhoEtiquetas))
                {
                    string[] arquivosEtiquetas = Directory.GetFiles(caminhoEtiquetas, "*_Etiquetas.txt")
                                                         .Select(Path.GetFileNameWithoutExtension) // Remove extensão para comparar só o nome
                                                         .ToArray();

                    etiquetasImpressas = arquivosEtiquetas.Select(nome => nome.Split('_')[0]).ToList();
                }

                // ✅ Limpa a listBox antes de adicionar novos itens
                listBoxAnuncios.Items.Clear();

                foreach (var entrada in dados)
                {
                    try
                    {
                        if (!entrada.ContainsKey("Anuncio") || !entrada.ContainsKey("Qtd Etiquetas") || !entrada.ContainsKey("Etiqueta"))
                            continue;

                        string etiqueta = entrada["Etiqueta"]?.ToString()?.Trim() ?? "";
                        string anuncio = entrada["Anuncio"]?.ToString()?.Trim() ?? "";
                        string qtdEtiquetas = entrada["Qtd Etiquetas"]?.ToString()?.Trim() ?? "";

                        if (!string.IsNullOrEmpty(etiqueta) && !etiquetasImpressas.Contains(etiqueta))
                        {
                            listBoxAnuncios.Items.Add($"{anuncio} - {qtdEtiquetas} Unidades");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao processar entrada: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar os anúncios: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async void VerificarEtiquetasImpressas() // No momento não está sendo usado, mas vou deixar de exemplo para buscar as etiquetas de um anúncio específico que está presente na listbox
        {
            while (true) // Loop contínuo para verificação constante
            {
                try
                {
                    string caminhoEtiquetas = Path.Combine($@"P:\INFORMATICA\programas\FULL\KelvinV2\agendamentos\{Program.nomePasta}\Etiquetas");

                    if (Directory.Exists(caminhoEtiquetas))
                    {
                        // Obtém a lista de arquivos de etiquetas já impressas
                        string[] arquivosEtiquetas = Directory.GetFiles(caminhoEtiquetas, "*_Etiquetas.txt")
                                                             .Select(Path.GetFileNameWithoutExtension) // Remove extensão para comparar só o nome
                                                             .ToArray();

                        List<string> etiquetasImpressas = arquivosEtiquetas.Select(nome => nome.Split('_')[0]).ToList();

                        // Criar uma lista de índices para remoção segura
                        List<int> indicesParaRemover = new List<int>();

                        // Percorre a listBox para encontrar anúncios que já têm etiquetas impressas
                        for (int i = 0; i < listBoxAnuncios.Items.Count; i++)
                        {
                            string item = listBoxAnuncios.Items[i].ToString();

                            // Extrai a chave (etiqueta) correspondente ao anúncio
                            string etiqueta = ExtrairEtiquetaDoAnuncio(item);

                            if (!string.IsNullOrEmpty(etiqueta) && etiquetasImpressas.Contains(etiqueta))
                            {
                                indicesParaRemover.Add(i);
                            }
                        }

                        // Remover os itens encontrados de forma segura dentro da thread da UI
                        if (indicesParaRemover.Count > 0)
                        {
                            listBoxAnuncios.Invoke((MethodInvoker)delegate
                            {
                                for (int i = indicesParaRemover.Count - 1; i >= 0; i--)
                                {
                                    listBoxAnuncios.Items.RemoveAt(indicesParaRemover[i]);
                                }
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao verificar etiquetas impressas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Aguarda 5 segundos antes de repetir a verificação para evitar sobrecarga
                await Task.Delay(5000);
            }
        }

        private string ExtrairEtiquetaDoAnuncio(string item) // Recebe "item" (sla oq é isso, tem exemplo no método VerificarEtiquetasImpressas()) e retorna como string a etiqueta do anúncio
        {
            // O nome do anúncio está no formato: "Anúncio - X Unidades"
            // Precisamos encontrar a etiqueta correspondente no JSON original

            string caminhoJson = Path.Combine(@"P:\INFORMATICA\programas\FULL\KelvinV2\agendamentos", $"{Program.nomePasta}", $"{Program.nomePasta}_Embalar.json");

            try
            {
                string jsonContent = File.ReadAllText(caminhoJson);
                var dados = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonContent);

                foreach (var entrada in dados)
                {
                    if (entrada.ContainsKey("Etiqueta") && entrada["Anuncio"].ToString().Trim() == item.Split('-')[0].Trim())
                    {
                        return entrada["Etiqueta"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao extrair etiqueta: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return string.Empty;
        }

        private void VerificarFinalizacaoAgendamento()
        {
            // Verifica se a listBox está vazia
            if (listBoxAnuncios.Items.Count == 0)
            {
                DialogResult result = MessageBox.Show(
                    "Todos os anúncios foram confirmados e impressos a etiqueta!\nGostaria de finalizar o agendamento?",
                    "Finalizar Agendamento",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Caminho do arquivo info.json
                        string caminhoJson = Path.Combine(@"P:\INFORMATICA\programas\FULL\KelvinV2\agendamentos", Program.nomePasta.ToString(), "info.json");

                        if (!File.Exists(caminhoJson))
                        {
                            MessageBox.Show("Arquivo info.json não encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Ler e desserializar o JSON
                        string jsonContent = File.ReadAllText(caminhoJson, Encoding.UTF8);
                        var dados = JsonSerializer.Deserialize<Dictionary<string, List<Dictionary<string, object>>>>(jsonContent, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        // Verifica se o JSON contém a chave "Embalar"
                        if (dados != null && dados.ContainsKey("Embalar") && dados["Embalar"] != null)
                        {
                            bool atualizado = false;

                            // Percorre a lista "Embalar" e altera "Concluído" para `true`
                            foreach (var item in dados["Embalar"])
                            {
                                if (item.ContainsKey("Concluído") && item["Concluído"] is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.False)
                                {
                                    item["Concluído"] = true;
                                    atualizado = true;
                                }
                                else if (item.ContainsKey("Concluído") && item["Concluído"] is bool concluido && !concluido)
                                {
                                    item["Concluído"] = true;
                                    atualizado = true;
                                }
                            }

                            if (atualizado)
                            {
                                // Serializa de volta para JSON e salva no arquivo
                                string novoJson = JsonSerializer.Serialize(dados, new JsonSerializerOptions { WriteIndented = true });
                                File.WriteAllText(caminhoJson, novoJson);

                                MessageBox.Show("Agendamento finalizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Fecha o formulário
                                this.Close();
                                VerAgendamentos verAgendamentos = new VerAgendamentos();
                                verAgendamentos.Show();
                            }
                            else
                            {
                                MessageBox.Show("Nenhuma alteração foi necessária, pois todos já estavam finalizados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("A chave 'Embalar' não foi encontrada no arquivo info.json!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao finalizar o agendamento: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public class ProdutoResponse
        {
            [JsonPropertyName("anexos")]
            public List<Anexo> Anexos { get; set; } // Lista de anexos/imagens do produto
        }

        public class Anexo
        {
            [JsonPropertyName("url")]
            public string Url { get; set; }  // URL da imagem

            [JsonPropertyName("externo")]
            public bool Externo { get; set; }  // Indica se é um anexo externo
        }

        public class ProdutoTinyResponse
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("sku")]
            public string Sku { get; set; }

            [JsonPropertyName("descricao")]
            public string Descricao { get; set; }

            [JsonPropertyName("tipo")]
            public string Tipo { get; set; } // Verifica se é um kit ("K") ou produto simples ("S")

            [JsonPropertyName("gtin")]
            public string Gtin { get; set; } // Código de barras

            [JsonPropertyName("anexos")]
            public List<AnexoTiny> Anexos { get; set; }

            [JsonPropertyName("kit")]
            public List<ProdutoComposicao> Kit { get; set; }
        }

        // Classe auxiliar para armazenar anexos (imagens)
        public class AnexoTiny
        {
            [JsonPropertyName("url")]
            public string Url { get; set; }
        }

        public class KitProduto
        {
            [JsonPropertyName("produto")]
            public ProdutoInfo Produto { get; set; }

            [JsonPropertyName("quantidade")]
            public int Quantidade { get; set; }
        }

        public class ProdutoInfo
        {
            [JsonPropertyName("id")]
            public int ID { get; set; }

            [JsonPropertyName("sku")]
            public string SKU { get; set; }

            [JsonPropertyName("descricao")]
            public string Descricao { get; set; }
        }

        public class ProdutoComposicao
        {
            [JsonPropertyName("produto")]
            public ProdutoInfo Produto { get; set; }

            [JsonPropertyName("id")]
            public int ID { get; set; }

            [JsonPropertyName("sku")]
            public string SKU { get; set; }

            [JsonPropertyName("descricao")]
            public string Descricao { get; set; }

            [JsonPropertyName("quantidade")]
            public int Quantidade { get; set; }

            [JsonPropertyName("gtin")]
            public string CodigoBarras { get; set; } // Código de barras do produto
        }

        public class Etiqueta
        {
            public string EtiquetaId { get; set; }

            [JsonPropertyName("Anuncio")]
            public string Anuncio { get; set; }

            [JsonPropertyName("Qtd Etiquetas")]
            public int QtdEtiquetas { get; set; }

            [JsonExtensionData]
            public Dictionary<string, JsonElement> Produtos { get; set; }
        }

        public class Produto
        {
            [JsonPropertyName("Produto")]
            public string NomeProduto { get; set; }

            [JsonPropertyName("SKU")]
            public string SKU { get; set; }

            [JsonPropertyName("Codebar")]
            public string CodigoBarras { get; set; }

            [JsonPropertyName("Quantidade")]
            public int Quantidade { get; set; }

            [JsonPropertyName("Anuncio")]
            public string Anuncio { get; set; } // Corrigido para string
        }

        private void codigoProdutoTxtBox_TextChanged(object sender, EventArgs e)
        {
            if (codigoProdutoTxtBox.Text.Trim() == Program.modoDevCODE)
            {
                codigoProdutoTxtBox.Text = "";
                ModoDEVEmbalar modoDev = new ModoDEVEmbalar(this);
                modoDev.ShowDialog();
            }
        }
    }
}
