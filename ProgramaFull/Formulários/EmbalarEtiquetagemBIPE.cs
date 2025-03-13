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
            if (MessageBox.Show("Você vai voltar para a janela de agendamentos. VOCÊ TEM CERTEZA QUE DESEJA SAIR?\n\n Você perderá tudo que fez até o momento... (função backup ainda em desenvolvimento)", "Deseja voltar?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                //MessageBox.Show("Backup Realizado");
                this.Close();                    // Precisa Criar a lógica de backup
                new VerAgendamentos().Show();
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Você está prestes a fechar essa o programa, pressione \"OK\" se estiver de acordo.", "AVISO", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (MessageBox.Show("Você tem mesmo certeza? Você está no meio de um processo muito importante!", "ATENÇÃO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (MessageBox.Show("VOCÊ TEM CERTEZA QUE DESEJA SAIR?\n\n Você perderá tudo que fez até o momento... (função backup ainda em desenvolvimento)", "VAI SAIR MESMO????", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        //MessageBox.Show("Backup Realizado");
                        Application.Exit();
                    }
                    //else
                    //{
                    //    MessageBox.Show("Não será salvo o backup ;-)");
                    //    Application.Exit();
                    //}
                }
            }
        }
        
        private async void codigoProdutoTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
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

                        List<string> anunciosEncontrados = new List<string>();
                        string produtoBipado = "";

                        // Percorre cada entrada no JSON
                        foreach (var entrada in dados)
                        {
                            if (entrada.ContainsKey("Anuncio") && entrada.ContainsKey("Qtd Etiquetas"))
                            {
                                string anuncio = entrada["Anuncio"].ToString();

                                foreach (var chave in entrada.Keys)
                                {
                                    // Verifica se é uma chave que contém uma lista de produtos (evita "Anuncio" e "Qtd Etiquetas")
                                    if (entrada[chave] is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Array)
                                    {
                                        foreach (JsonElement item in jsonElement.EnumerateArray())
                                        {
                                            if (item.TryGetProperty("SKU", out JsonElement skuElement) &&
                                                skuElement.GetString() == codigoBipado)
                                            {
                                                anunciosEncontrados.Add(anuncio);
                                                produtoBipado = item.GetProperty("Produto").GetString();
                                            }
                                            else if (item.TryGetProperty("Codebar", out JsonElement codebarElement) &&
                                                     codebarElement.GetString() == codigoBipado)
                                            {
                                                anunciosEncontrados.Add(anuncio);
                                                produtoBipado = item.GetProperty("Produto").GetString();
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        // Se o SKU ou Código de Barras foi encontrado em algum anúncio
                        if (anunciosEncontrados.Count > 0)
                        {
                            string anunciosLista = string.Join(" | ", anunciosEncontrados);
                            await ExibirFormularioKitsAsync(produtoBipado, anunciosEncontrados, dados, codigoBipado);
                        }
                        else
                        {
                            MessageBox.Show($"O código '{codigoBipado}' não foi encontrado em nenhum anúncio.\n\n" +
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

        private async Task ExibirFormularioKitsAsync(string produtoBipado, List<string> anunciosEncontrados, List<Dictionary<string, object>> dados, string codigoBipado)
        {
            Form formKits = new Form
            {
                Text = "Kits Encontrados",
                StartPosition = FormStartPosition.CenterScreen,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                BackColor = Color.White
            };

            FlowLayoutPanel panel = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(10),
                WrapContents = false
            };

            Label titulo = new Label
            {
                Text = $"Produto Bipado: {produtoBipado}",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.Black
            };
            panel.Controls.Add(titulo);

            List<string> idsProdutos = new List<string>(); // Lista para armazenar os IDs dos produtos

            // Adicionar os kits dinamicamente
            foreach (var entrada in dados)
            {
                if (entrada.ContainsKey("Anuncio") && entrada.ContainsKey("ID"))
                {
                    string nomeAnuncio = entrada["Anuncio"].ToString();
                    string idProduto = entrada["ID"].ToString(); // Captura o ID do produto

                    foreach (var chave in entrada.Keys)
                    {
                        if (entrada[chave] is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Array)
                        {
                            foreach (JsonElement item in jsonElement.EnumerateArray())
                            {
                                if ((item.TryGetProperty("SKU", out JsonElement skuElement) && skuElement.GetString() == codigoBipado) ||
                                    (item.TryGetProperty("Codebar", out JsonElement codebarElement) && codebarElement.GetString() == codigoBipado))
                                {
                                    if (!anunciosEncontrados.Contains(nomeAnuncio))
                                    {
                                        anunciosEncontrados.Add(nomeAnuncio);
                                    }
                                    idsProdutos.Add(idProduto); // Adiciona o ID correspondente
                                }
                            }
                        }
                    }
                }
            }

            // Buscar imagens e adicionar ao formulário
            foreach (var idProduto in idsProdutos)
            {
                string imagemUrl = await BuscarImagemProdutoTiny(idProduto);

                Panel kitPanel = new Panel
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    Width = 350,
                    Height = 120,
                    Padding = new Padding(5)
                };

                kitPanel.Click += (s, e) => AbrirFormularioConfirmacaoKit(
                    idProduto,
                    anunciosEncontrados[idsProdutos.IndexOf(idProduto)],
                    imagemUrl,
                    dados
                );

                PictureBox pictureBox = new PictureBox
                {
                    Size = new Size(100, 100),
                    Location = new Point(5, 5),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    ImageLocation = imagemUrl
                };

                Label label = new Label
                {
                    Text = anunciosEncontrados[idsProdutos.IndexOf(idProduto)],
                    Location = new Point(110, 10),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.Black,
                    AutoSize = true,
                    MaximumSize = new Size(200, 0)
                };

                kitPanel.Controls.Add(pictureBox);
                kitPanel.Controls.Add(label);
                panel.Controls.Add(kitPanel);
            }

            formKits.Controls.Add(panel);
            formKits.ShowDialog();
        }

        private async void AbrirFormularioConfirmacaoKit(string idProduto, string nomeKit, string imagemUrl, List<Dictionary<string, object>> dados)
        {
            // Se o kit já foi confirmado, impedir a reabertura
            if (int.TryParse(idProduto, out int idProdutoInt) && Program.kitsConfirmados.Contains(idProdutoInt))
            {
                MessageBox.Show("Este kit já foi confirmado anteriormente!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Exibir tela de carregamento
            Form telaCarregamento = new Form
            {
                Text = "Carregando...",
                Size = new Size(300, 150),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                ControlBox = false
            };
            Label labelCarregando = new Label
            {
                Text = "Aguarde enquanto buscamos a composição...",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            telaCarregamento.Controls.Add(labelCarregando);
            telaCarregamento.Show();

            // Buscar composição do kit
            var composicao = await BuscarComposicaoDoKit(idProduto);
            telaCarregamento.Close(); // Fechar tela de carregamento

            if (composicao == null)
            {
                telaCarregamento.Close();

                // Encontrar os dados do produto no JSON
                var produtoData = dados.FirstOrDefault(d =>
                    d.ContainsKey("ID") &&
                    d["ID"].ToString() == idProduto &&
                    d.ContainsKey("Anuncio") &&
                    d["Anuncio"].ToString() == nomeKit);

                if (produtoData == null)
                {
                    MessageBox.Show("Erro ao encontrar os dados do produto simples no JSON.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Pegar o nome da chave do array como EtiquetaId
                string etiquetaIdSimples = produtoData.Keys.FirstOrDefault(k =>
                    k != "Anuncio" && k != "ID" && k != "SKU" && k != "Qtd Etiquetas");

                if (string.IsNullOrEmpty(etiquetaIdSimples) || !produtoData.ContainsKey(etiquetaIdSimples))
                {
                    MessageBox.Show("Erro ao capturar a etiqueta do produto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Acessar a lista de produtos dentro da chave da etiqueta
                var listaProdutos = produtoData[etiquetaIdSimples] as JsonElement?;
                if (listaProdutos == null || listaProdutos?.ValueKind != JsonValueKind.Array || listaProdutos?.GetArrayLength() == 0)
                {
                    MessageBox.Show("Erro ao encontrar a lista de produtos dentro da etiqueta.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Pegar o primeiro produto da lista
                var produtoInfo = listaProdutos?.EnumerateArray().FirstOrDefault();

                if (produtoInfo == null || !produtoInfo.Value.TryGetProperty("SKU", out JsonElement skuElement))
                {
                    MessageBox.Show("Erro: O produto não contém um SKU válido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string skuProduto = skuElement.GetString();
                string codigoBarrasProduto = produtoInfo.Value.TryGetProperty("Codebar", out JsonElement codebarElement) ? codebarElement.GetString() : "";

                if (MessageBox.Show($"Deseja imprimir as etiquetas do anúncio \"{nomeKit}\"?", "Produto Simples", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Form formBipagem = new Form
                    {
                        Text = "Confirmação de Produto",
                        StartPosition = FormStartPosition.CenterScreen,
                        AutoSize = true,
                        AutoSizeMode = AutoSizeMode.GrowAndShrink,
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        BackColor = Color.White
                    };

                    FlowLayoutPanel painel = new FlowLayoutPanel
                    {
                        AutoSize = true,
                        FlowDirection = FlowDirection.TopDown,
                        Padding = new Padding(10),
                        WrapContents = false
                    };

                    Label labelInstrucao = new Label
                    {
                        Text = "Bipe o código de barras ou SKU para confirmar a impressão.",
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                        AutoSize = true
                    };
                    painel.Controls.Add(labelInstrucao);

                    TextBox textBoxBipagemSimples = new TextBox { Width = 200 };
                    painel.Controls.Add(textBoxBipagemSimples);

                    formBipagem.Controls.Add(painel);
                    textBoxBipagemSimples.Focus();

                    textBoxBipagemSimples.KeyDown += (sender, e) =>
                    {
                        if (e.KeyCode == Keys.Enter)
                        {
                            string bipado = textBoxBipagemSimples.Text.Trim();

                            if (bipado == skuProduto || (!string.IsNullOrEmpty(codigoBarrasProduto) && bipado == codigoBarrasProduto))
                            {
                                Etiqueta etiqueta = new Etiqueta
                                {
                                    EtiquetaId = etiquetaIdSimples, // Agora a etiqueta é o nome do array!
                                    Anuncio = produtoData["Anuncio"].ToString(),
                                    QtdEtiquetas = int.Parse(produtoData["Qtd Etiquetas"].ToString())
                                };

                                Produto produto = new Produto
                                {
                                    NomeProduto = produtoData["Anuncio"].ToString(),
                                    SKU = skuProduto,
                                    CodigoBarras = codigoBarrasProduto
                                };

                                formBipagem.Close();

                                MessageBox.Show($"Etiqueta Confirmada!\n\nEtiqueta: {etiqueta.EtiquetaId}\nAnúncio: {etiqueta.Anuncio}\nSKU: {produto.SKU}\nQuantidade: {etiqueta.QtdEtiquetas}",
                                                "Confirmação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (MessageBox.Show("Deseja imprimir?", "Status Impressão", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                { 
                                    ImprimirEtiquetas(etiqueta, produto); 
                                }
                                else
                                {
                                    MessageBox.Show("Não será impresso ;-)", "Status Impressão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Código de barras ou SKU incorreto!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                textBoxBipagemSimples.Clear();
                            }
                        }
                    };

                    formBipagem.ShowDialog();
                }

                return;
            }

            // Encontrar o dicionário correto para o kit atual
            var kitData = dados.FirstOrDefault(d =>
                d.ContainsKey("ID") &&
                d["ID"].ToString() == idProduto &&
                d.ContainsKey("Anuncio") &&
                d["Anuncio"].ToString() == nomeKit);

            if (kitData == null)
            {
                telaCarregamento.Close();
                MessageBox.Show("Erro ao encontrar dados do kit no JSON.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Obtendo a EtiquetaId dinamicamente do JSON (a chave que representa a etiqueta)
            string etiquetaId = kitData.Keys
                .FirstOrDefault(k =>
                    k != "Anuncio" &&
                    k != "ID" &&
                    k != "Qtd Etiquetas" &&
                    kitData[k] is JsonElement element &&
                    element.ValueKind == JsonValueKind.Array);

            if (string.IsNullOrEmpty(etiquetaId))
            {
                telaCarregamento.Close();
                MessageBox.Show("Erro ao capturar a etiqueta do kit.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Criando a Etiqueta com os valores corretos
            Etiqueta etiqueta = new Etiqueta
            {
                EtiquetaId = etiquetaId,
                Anuncio = kitData["Anuncio"].ToString(),
                QtdEtiquetas = int.Parse(kitData["Qtd Etiquetas"].ToString())
            };

            // Lista de produtos com seus detalhes (ID, SKU, descrição, GTIN)
            List<ProdutoComposicao> composicaoDetalhada = new List<ProdutoComposicao>();

            // Para cada produto na composição, buscar mais detalhes
            foreach (var produto in composicao)
            {
                var detalhesProduto = await BuscarDetalhesProdutoTiny(produto.ID.ToString());
                if (detalhesProduto != null)
                {
                    composicaoDetalhada.Add(new ProdutoComposicao
                    {
                        ID = detalhesProduto.ID,
                        SKU = detalhesProduto.SKU,
                        Descricao = detalhesProduto.Descricao,
                        CodigoBarras = detalhesProduto.CodigoBarras, // Código de barras
                        Quantidade = produto.Quantidade
                    });
                }
            }

            telaCarregamento.Close(); // Fechar tela de carregamento

            // Criar novo formulário para exibir a composição
            Form formConfirmacao = new Form
            {
                Text = "Confirmação do Kit",
                StartPosition = FormStartPosition.CenterScreen,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FormBorderStyle = FormBorderStyle.FixedDialog
            };

            FlowLayoutPanel panel = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(10),
                WrapContents = false
            };

            // Adiciona título
            Label titulo = new Label
            {
                Text = "Por favor, confirme a composição",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true
            };
            panel.Controls.Add(titulo);

            // Adiciona imagem do kit
            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(250, 250),
                SizeMode = PictureBoxSizeMode.Zoom,
                ImageLocation = imagemUrl
            };
            panel.Controls.Add(pictureBox);

            // Lista os produtos do kit
            Label labelComposicao = new Label
            {
                Text = "Composição do Kit:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true
            };
            panel.Controls.Add(labelComposicao);

            // Criar lista para armazenar os SKUs e GTINs que precisam ser bipados
            Dictionary<string, int> quantidadeBipada = new Dictionary<string, int>();

            foreach (var produto in composicaoDetalhada)
            {
                quantidadeBipada[produto.SKU] = 0; // Inicia com 0 bipagens

                Label produtoLabel = new Label
                {
                    Text = $"{produto.Descricao} - {produto.Quantidade} Unidades",
                    Font = new Font("Segoe UI", 10, FontStyle.Regular),
                    ForeColor = Color.Red, // Começa vermelho
                    AutoSize = true
                };

                panel.Controls.Add(produtoLabel);
            }

            // Campo de entrada para bipar os produtos
            TextBox textBoxBipagem = new TextBox { Width = 200 };
            textBoxBipagem.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string bipado = textBoxBipagem.Text.Trim();
                    bool produtoEncontrado = false;

                    foreach (var produto in composicaoDetalhada)
                    {
                        if (produto.SKU == bipado || produto.CodigoBarras == bipado)
                        {
                            quantidadeBipada[produto.SKU]++;

                            // Atualiza a cor do label correspondente ao produto bipado
                            foreach (Control control in panel.Controls)
                            {
                                if (control is Label label && label.Text.Contains(produto.Descricao))
                                {
                                    if (quantidadeBipada[produto.SKU] >= produto.Quantidade)
                                    {
                                        label.ForeColor = Color.Green; // Quando atinge a quantidade necessária, fica verde
                                    }
                                }
                            }

                            produtoEncontrado = true;
                            break;
                        }
                    }

                    textBoxBipagem.Clear();

                    if (!produtoEncontrado)
                    {
                        MessageBox.Show("Este produto não faz parte da composição ou já foi bipado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (composicaoDetalhada.All(p => quantidadeBipada.ContainsKey(p.SKU) && quantidadeBipada[p.SKU] >= p.Quantidade))
                    {
                        if (int.TryParse(idProduto, out int idProdutoInt))
                        {
                            Program.kitsConfirmados.Add(idProdutoInt);
                        }
                        else
                        {
                            MessageBox.Show($"Aviso: Não foi possível registrar o kit como confirmado. ID inválido: {idProduto}",
                                          "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (MessageBox.Show("Kit confirmado com sucesso!\nDeseja imprimir as etiquetas agora?", "Sucesso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            // Pegando o primeiro produto válido para impressão
                            var produto = composicaoDetalhada.FirstOrDefault();

                            if (produto != null)
                            {
                                Produto produtoEtiqueta = new Produto
                                {
                                    NomeProduto = produto.Descricao,
                                    SKU = produto.SKU,
                                    CodigoBarras = produto.CodigoBarras,
                                    Quantidade = produto.Quantidade
                                };

                                ImprimirEtiquetas(etiqueta, produtoEtiqueta);
                            }
                            else
                            {
                                MessageBox.Show("Nenhum produto encontrado para impressão da etiqueta.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Não será impresso ;-)", "Status Impressão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            };

            panel.Controls.Add(textBoxBipagem);
            formConfirmacao.Controls.Add(panel);
            formConfirmacao.ShowDialog();
        }

        private void ImprimirEtiquetas(Etiqueta etiqueta, Produto produto)
        {
            if (string.IsNullOrEmpty(Program.impressoraEtiqueta))
            {
                MessageBox.Show("Nenhuma impressora de etiquetas selecionada.\nConfigure a impressora antes de prosseguir.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string caminhoJson = $@"P:\INFORMATICA\programas\FULL\KelvinV2\agendamentos\{Program.nomePasta}\Etiquetas";
                string fileContent = GerarEtiquetas(etiqueta, produto, caminhoJson);
                string printerName = Program.impressoraEtiqueta;
                if (MessageBox.Show($"Impressora selecionada para a impressão da etiqueta: {printerName}\n\n Deseja prosseguir?", "Impressora Etiqueta", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (!EnviarArquivoParaImpressora(fileContent))
                    {
                        throw new Exception("Erro ao enviar o arquivo ZPL para a impressora.");
                    }
                }

                MessageBox.Show("Etiquetas enviadas para impressão com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao imprimir etiquetas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            File.WriteAllText(nomeArquivo, zplCompleto.ToString());

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

        public static async Task<string> BuscarImagemProdutoTiny(string idProduto)
        {
            if (string.IsNullOrEmpty(Program.accessTokenTinyV3))
            {
                // Buscar o Access Token
                await Program.BuscarAccessTokenTinyAsync();
            }

            // Verifica se o token foi obtido
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

                    // Se for erro 403, tenta buscar a chave de novo e refazer a requisição
                    if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        MessageBox.Show("Erro 403: Acesso negado. Tentando buscar nova chave de API...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        await Program.BuscarAccessTokenTinyAsync();

                        // Refaz a requisição com a nova chave
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Program.accessTokenTinyV3);
                        response = await client.GetAsync(url);
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var produto = JsonSerializer.Deserialize<ProdutoResponse>(jsonResponse, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (produto != null && produto.Anexos != null && produto.Anexos.Count > 0)
                        {
                            return produto.Anexos.First().Url; // Retorna a primeira imagem disponível
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar imagem no Tiny: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return "https://i.imgur.com/tWvwe0s.png"; // Imagem padrão caso não encontre
        }

        private void CarregarAnuncios()
        {
            // Define o caminho do arquivo JSON com base no número do agendamento
            string caminhoJson = Path.Combine(@"P:\INFORMATICA\programas\FULL\KelvinV2\agendamentos", $"{Program.nomePasta}", $"{Program.nomePasta}_Embalar.json");

            try
            {
                // Lê e desserializa o JSON
                string jsonContent = File.ReadAllText(caminhoJson);
                var dados = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonContent);

                // Limpa a listBox antes de adicionar novos itens
                listBoxAnuncios.Items.Clear();

                // Processa os anúncios do JSON
                foreach (var entrada in dados)
                {
                    if (entrada.ContainsKey("Anuncio") && entrada.ContainsKey("Qtd Etiquetas"))
                    {
                        string anuncio = entrada["Anuncio"].ToString();
                        string qtdEtiquetas = entrada["Qtd Etiquetas"].ToString();

                        // Adiciona à listBox no formato desejado
                        listBoxAnuncios.Items.Add($"{anuncio} - {qtdEtiquetas} Unidades");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar os anúncios: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
