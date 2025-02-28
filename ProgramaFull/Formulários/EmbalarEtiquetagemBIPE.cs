﻿using System;
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

namespace ProgramaFull.Formulários
{
    public partial class EmbalarEtiquetagemBIPE : Form
    {
        public EmbalarEtiquetagemBIPE()
        {
            InitializeComponent();
            CarregarAnuncios(); // Chama o método ao inicializar o formulário
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

                kitPanel.Click += (s, e) => AbrirFormularioConfirmacaoKit(idProduto, anunciosEncontrados[idsProdutos.IndexOf(idProduto)], imagemUrl);

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

        private async void AbrirFormularioConfirmacaoKit(string idProduto, string nomeKit, string imagemUrl)
        {
            // Se o kit já foi confirmado, impedir a reabertura
            if (Program.kitsConfirmados.Contains(int.Parse(idProduto)))
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

            // Se não for um kit, mostrar mensagem e sair
            if (composicao == null)
            {
                MessageBox.Show("Este produto não é um kit e não pode ser confirmado dessa forma.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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

            // Criar lista para armazenar os SKUs que precisam ser bipados
            List<string> produtosFaltantes = new List<string>();

            foreach (var produto in composicao)
            {
                Label produtoLabel = new Label
                {
                    Text = $"{produto.Descricao} - {produto.Quantidade} Unidades",
                    Font = new Font("Segoe UI", 10, FontStyle.Regular),
                    ForeColor = Program.kitsConfirmados.Contains(produto.ID) ? Color.Green : Color.Red,
                    AutoSize = true
                };

                panel.Controls.Add(produtoLabel);

                if (!Program.kitsConfirmados.Contains(produto.ID))
                {
                    produtosFaltantes.Add(produto.SKU);
                }
            }

            // Campo de entrada para bipar os produtos
            TextBox textBoxBipagem = new TextBox { Width = 200 };
            textBoxBipagem.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string bipado = textBoxBipagem.Text.Trim();
                    if (produtosFaltantes.Contains(bipado))
                    {
                        produtosFaltantes.Remove(bipado);
                        textBoxBipagem.Clear();

                        if (produtosFaltantes.Count == 0)
                        {
                            // Todos os produtos foram bipados, confirmar o kit
                            Program.kitsConfirmados.Add(int.Parse(idProduto));
                            MessageBox.Show("Kit confirmado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            formConfirmacao.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Este produto não faz parte da composição ou já foi bipado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            };

            panel.Controls.Add(textBoxBipagem);
            formConfirmacao.Controls.Add(panel);
            formConfirmacao.ShowDialog();
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
            public List<Anexo> Anexos { get; set; } // Lista de anexos/imagens do produto
        }

        public class Anexo
        {
            public string Url { get; set; }  // URL da imagem
            public bool Externo { get; set; }  // Indica se é um anexo externo
        }

        public class ProdutoTinyResponse
        {
            public int Id { get; set; }
            public string Sku { get; set; }
            public string Descricao { get; set; }
            public List<KitProduto> Kit { get; set; }
        }

        public class KitProduto
        {
            public ProdutoInfo Produto { get; set; }
            public int Quantidade { get; set; }
        }

        public class ProdutoInfo
        {
            public int ID { get; set; }
            public string SKU { get; set; }
            public string Descricao { get; set; }
        }

        public class ProdutoComposicao
        {
            public int ID { get; set; }
            public string SKU { get; set; }
            public string Descricao { get; set; }
            public int Quantidade { get; set; }
        }

    }
}
