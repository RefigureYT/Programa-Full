using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Runtime.Intrinsics.X86;
using System.Globalization;
using ProgramaFull.Formulários;
using System.Text.Json.Serialization;

namespace ProgramaFull.Formulários
{
    public partial class preConfForm : Form
    {
        private TimeSpan tempoPreConferencia = TimeSpan.Zero;
        private System.Windows.Forms.Timer timer;

        private List<ProdutosJson> produtosOriginais;
        private Dictionary<string, int> produtosPorSKU = new Dictionary<string, int>();
        private Dictionary<long, int> produtosPorCodebar = new Dictionary<long, int>();
        private Dictionary<string, int> produtosBipados = new Dictionary<string, int>();
        private List<ProdutosJson> produtos;
        static int tempoSegundos;
        string bipadosFilePath;
        string originalFilePath;
        private string nomePasta = Program.nomePasta.ToString();

        public preConfForm()
        {
            InitializeComponent();
            this.Load += preConfForm_Load;
            ConfigurarControles();

            bipadosFilePath = $"P:\\INFORMATICA\\programas\\FULL\\KelvinV2\\agendamentos\\{Program.nomePasta}\\{Program.nomePasta}_bipadosPreConf.json";
            originalFilePath = $"P:\\INFORMATICA\\programas\\FULL\\KelvinV2\\agendamentos\\{Program.nomePasta}\\{Program.nomePasta}_PreConf.json";

            // Carrega os produtos originais
            produtosOriginais = CarregarProdutos(originalFilePath);
        }

        private void ConfigurarControles()
        {
            panelProdutos.AutoScroll = true;
            panelConcluidos.AutoScroll = true;
        }

        private void GerarRelatorioFinal()
        {
            if (!panelProdutos.Controls.OfType<Label>().Any())
            {
                var informacoes = new
                {
                    Informacoes = new
                    {
                        Agendamento = Program.nomePasta,
                        Empresa = Program.nomeEmpresa,
                        DataInicio = Program.dataHoraInicioRelatorio,
                        DataTermino = DateTime.Now.ToString("dd/MM/yyyy HH'h' mm'm' ss's'"),
                        Permanencia = Program.permanencia
                    },
                    Colaboradores = ObterColaboradoresLista(),
                    Relatorio = ObterProdutosConcluidos()
                };

                string jsonPath = $"P:\\INFORMATICA\\programas\\FULL\\KelvinV2\\agendamentos\\{Program.nomePasta}\\{Program.nomePasta}_RelatorioPreConf.json";
                string jsonContent = JsonSerializer.Serialize(informacoes, new JsonSerializerOptions { WriteIndented = true });

                try
                {
                    File.WriteAllText(jsonPath, jsonContent);
                    if (MessageBox.Show("Relatório final gerado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        string caminhoArquivo = $"P:\\INFORMATICA\\programas\\FULL\\KelvinV2\\agendamentos\\{Program.nomePasta}\\info.json";
                        FinalizarAgendamento(caminhoArquivo);

                        Menu menu = new Menu();
                        menu.Show();

                        Close();
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"Erro ao salvar o relatório: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private List<object> ObterColaboradoresLista()
        {
            string caminhoArquivo = $"P:\\INFORMATICA\\programas\\FULL\\KelvinV2\\agendamentos\\{Program.nomePasta}\\info.json";
            var colaboradores = ObterColaboradoresDetalhado(caminhoArquivo);
            return colaboradores.Split(',').Select(c => (object)new { Colaborador = c.Trim() }).ToList();
        }

        private string ObterColaboradoresDetalhado(string caminhoArquivo)
        {
            if (!File.Exists(caminhoArquivo))
            {
                return string.Empty;
            }

            try
            {
                string jsonContent = File.ReadAllText(caminhoArquivo);
                var colaboradoresJson = JsonSerializer.Deserialize<Dictionary<string, List<Dictionary<string, object>>>>(jsonContent);

                var colaboradores = colaboradoresJson["Pré Conferência"]
                    .Where(item => item.ContainsKey("Colaborador") && item["Colaborador"] != null)
                    .Select(item => item["Colaborador"].ToString().Trim())
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                return string.Join(", ", colaboradores);
            }
            catch
            {
                return string.Empty;
            }
        }

        private List<object> ObterProdutosConcluidos()
        {
            var produtosConcluidos = new List<object>();

            foreach (Label label in panelConcluidos.Controls.OfType<Label>())
            {
                if (label.Tag is string sku)
                {
                    var produtoOriginal = produtosOriginais?.FirstOrDefault(p => p.SKU == sku);
                    var produtoBipado = produtos.FirstOrDefault(p => p.SKU == sku);

                    if (produtoOriginal != null && produtoBipado != null)
                    {
                        produtosConcluidos.Add(new
                        {
                            Produto = produtoOriginal.Produto,
                            SKU = sku,
                            Quantidade = produtoOriginal.Quantidade, // Quantidade original
                            Bipados = produtosBipados.ContainsKey(sku) ? produtosBipados[sku] : 0
                        });
                    }
                }
            }

            return produtosConcluidos;
        }

        private string ObterColaboradoresCompleto(string caminhoArquivo)
        {
            if (!File.Exists(caminhoArquivo))
            {
                return string.Empty;
            }

            try
            {
                string jsonContent = File.ReadAllText(caminhoArquivo);
                var colaboradoresJson = JsonSerializer.Deserialize<Dictionary<string, List<Dictionary<string, object>>>>(jsonContent);

                var colaboradores = colaboradoresJson["Pré Conferência"]
                    .Where(item => item.ContainsKey("Colaborador") && item["Colaborador"] != null)
                    .Select(item => item["Colaborador"].ToString().Trim())
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                return string.Join(", ", colaboradores);
            }
            catch
            {
                return string.Empty;
            }
        }

        private void CarregarProdutosDoJSON(string jsonContent)
        {
            produtos = JsonSerializer.Deserialize<List<ProdutosJson>>(jsonContent);
            int yOffset = 10;
            tempoSegundos = 0;
            foreach (var produto in produtos)
            {
                if (EncontrarLabel(produto.SKU) != null)
                {
                    continue;
                }

                produtosPorSKU[produto.SKU] = produto.Quantidade;
                produtosBipados[produto.SKU] = 0; // Inicializa a contagem de bipados

                if (produto.Codebar != 0)
                {
                    produtosPorCodebar[produto.Codebar] = produto.Quantidade;
                }

                if (produto.Quantidade <= 25)
                {
                    tempoSegundos += (5 * produto.Quantidade);
                }
                else
                {
                    tempoSegundos += 100;
                }
                Label labelProduto = CriarLabel(produto, yOffset);
                panelProdutos.Controls.Add(labelProduto);
                yOffset += 40;
            }
        }

        private Label CriarLabel(ProdutosJson produto, int yOffset)
        {
            return new Label
            {
                Text = AtualizarTextoLabel(produto.SKU),
                AutoSize = false,
                Width = panelProdutos.Width - 40,
                Height = 38,
                Location = new Point(12, yOffset),
                BackColor = Color.FromArgb(215, 215, 215),
                ForeColor = Color.FromArgb(0, 0, 0),
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9.75F, FontStyle.Bold),
                Margin = new Padding(3),
                Tag = produto.SKU
            };
        }

        private void AtualizarQuantidade(string key, bool isSKU)
        {
            string sku;
            int quantidadeSubtrair = !string.IsNullOrWhiteSpace(textQTD.Text) && int.TryParse(textQTD.Text, out int qtd) ? qtd : 1;

            if (isSKU)
            {
                sku = key;

                if (produtosPorSKU[sku] - quantidadeSubtrair < 0)
                {
                    MessageBox.Show($"O valor que você está tentando colocar excede a quantidade presente no agendamento. SKU: {sku}.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                produtosPorSKU[sku] -= quantidadeSubtrair;
                produtosBipados[sku] += quantidadeSubtrair;
            }
            else
            {
                long codebar = long.Parse(key);
                if (produtosPorCodebar[codebar] - quantidadeSubtrair < 0)
                {
                    MessageBox.Show($"O valor que você está tentando colocar excede a quantidade presente no agendamento. Código de barras: {codebar}.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                produtosPorCodebar[codebar] -= quantidadeSubtrair;

                var produto = produtos.First(p => p.Codebar == codebar);
                sku = produto.SKU;
                produtosPorSKU[sku] = produtosPorCodebar[codebar];
                produtosBipados[sku] += quantidadeSubtrair;
            }

            Label label = EncontrarLabel(sku);
            if (label == null) return;

            label.Text = AtualizarTextoLabel(sku);

            int quantidade = produtosPorSKU[sku];
            if (quantidade == 0)
            {
                MoverParaPainelConcluidos(label);
            }
        }

        private Label EncontrarLabel(string sku)
        {
            return panelProdutos.Controls.OfType<Label>().FirstOrDefault(l => l.Tag?.ToString() == sku) ??
                   panelConcluidos.Controls.OfType<Label>().FirstOrDefault(l => l.Tag?.ToString() == sku);
        }

        private string AtualizarTextoLabel(string sku)
        {
            var produto = produtos.First(p => p.SKU == sku);
            int quantidade = produtosPorSKU[sku];
            int bipados = produtosBipados.ContainsKey(sku) ? produtosBipados[sku] : 0;

            return Program.modoDEVpreConfForm == "ON"
                ? $"{produto.Produto} - SKU: {sku} - Qtd: {quantidade} - Bipados: {bipados}"
                : $"{produto.Produto} - SKU: {sku} - Bipados: {bipados}";
        }

        private void MoverParaPainelConcluidos(Label label)
        {
            if (label.Parent == panelProdutos)
            {
                panelProdutos.Controls.Remove(label);
            }

            label.BackColor = Color.LightGreen;
            label.Width = panelConcluidos.Width - 20;

            if (!panelConcluidos.Controls.Contains(label))
            {
                panelConcluidos.Controls.Add(label);
            }

            ReorganizarLabels(panelProdutos);
            ReorganizarLabels(panelConcluidos);
        }

        private void ReorganizarLabels(Panel panel)
        {
            int yOffset = 10;

            foreach (Label label in panel.Controls.OfType<Label>().OrderBy(l => l.Tag))
            {
                label.Location = new Point(12, yOffset);
                yOffset += 40;
            }
        }

        private void AtualizarTodasLabels()
        {
            foreach (Label label in panelProdutos.Controls.OfType<Label>()
                .Concat(panelConcluidos.Controls.OfType<Label>()))
            {
                if (label.Tag is string sku)
                {
                    label.Text = AtualizarTextoLabel(sku);
                }
            }
        }

        private void FinalizarAgendamento(string caminhoArquivo)
        {
            try
            {
                // Verifica se o arquivo existe
                if (!File.Exists(caminhoArquivo))
                {
                    MessageBox.Show("Arquivo JSON do agendamento não encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lê o conteúdo do arquivo JSON
                string jsonContent = File.ReadAllText(caminhoArquivo);

                // Desserializa o conteúdo para um dicionário
                var agendamento = JsonSerializer.Deserialize<Dictionary<string, List<Dictionary<string, object>>>>(jsonContent);

                if (agendamento != null && agendamento.ContainsKey("Pré Conferência"))
                {
                    // Atualiza o valor de "Concluído" para true em todos os itens
                    foreach (var item in agendamento["Pré Conferência"])
                    {
                        if (item.ContainsKey("Concluído"))
                        {
                            item["Concluído"] = true;
                        }
                    }

                    // Serializa o objeto de volta para JSON
                    string jsonAtualizado = JsonSerializer.Serialize(agendamento, new JsonSerializerOptions { WriteIndented = true });

                    // Salva o conteúdo atualizado de volta no arquivo
                    File.WriteAllText(caminhoArquivo, jsonAtualizado);

                    //MessageBox.Show("Agendamento finalizado com sucesso! O arquivo foi atualizado.", "Concluído", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Estrutura do arquivo JSON inválida!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao finalizar o agendamento: {ex.Message}\n\n Por favor comunique o administrador!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxCODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                string input = textBoxCODE.Text.Trim();
                if (string.IsNullOrEmpty(input)) return;

                if (produtosPorSKU.ContainsKey(input))
                {
                    AtualizarQuantidade(input, true);
                    textQTD.Text = "";
                }
                else if (long.TryParse(input, out long codebar) && produtosPorCodebar.ContainsKey(codebar))
                {
                    AtualizarQuantidade(codebar.ToString(), false);
                    textQTD.Text = "";
                }
                else
                {
                    MessageBox.Show("O código de barras ou SKU inserido é inválido. Verifique se o código está correto.",
                                  "Código Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textQTD.Text = "";
                }

                textBoxCODE.Clear();
                textBoxCODE.Focus();

                GerarRelatorioFinal();
            }
        }

        public void imprimir()
        {
            PrintDocument imprimirRelatorio = new PrintDocument();
            imprimirRelatorio.PrinterSettings.PrinterName = Program.impressoraRelatorio;

            if (imprimirRelatorio.PrinterSettings.IsValid)
            {
                DateTime terminoPreConferencia = DateTime.Now;
                string caminhoArquivo = $"P:\\INFORMATICA\\programas\\FULL\\KelvinV2\\agendamentos\\{Program.nomePasta}\\info.json";
                string caminhoArquivoJSON = $"P:\\INFORMATICA\\programas\\FULL\\KelvinV2\\agendamentos\\{Program.nomePasta}\\{Program.nomePasta}_PreConf.json";
                string colaboradores = ObterColaboradores(caminhoArquivo);

                List<ProdutosJson> listaProdutos = CarregarProdutos(caminhoArquivoJSON);

                if (listaProdutos == null || listaProdutos.Count == 0)
                {
                    MessageBox.Show("Nenhum produto encontrado para o agendamento!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Configura o evento PrintPage
                imprimirRelatorio.PrintPage += (s, ev) =>
                {
                    Font font = new Font("Arial", 12);
                    Font fontProduto = new Font("Arial", 9.75F);
                    Brush brush = Brushes.Black;
                    float eixoY = ev.MarginBounds.Top;
                    float eixoX = ev.MarginBounds.Left;
                    float alturaLinha = font.GetHeight(ev.Graphics);

                    // Cabeçalho
                    ev.Graphics.DrawString($"Agendamento nº: {Program.nomePasta}", font, brush, eixoX, eixoY);
                    eixoY += alturaLinha * 1.2F;

                    ev.Graphics.DrawString($"Empresa: {Program.nomeEmpresa}", font, brush, eixoX, eixoY);
                    eixoY += alturaLinha * 1.2F;

                    ev.Graphics.DrawString($"Data e hora de início: {Program.dataHoraInicioRelatorio}", font, brush, eixoX, eixoY);
                    eixoY += alturaLinha * 1.2F;

                    ev.Graphics.DrawString($"Data e hora de término: {terminoPreConferencia:dd/MM/yyyy HH'h' mm'm' ss's'}", font, brush, eixoX, eixoY);
                    eixoY += alturaLinha * 1.2F;

                    ev.Graphics.DrawString($"Permanência: {Program.permanencia}", font, brush, eixoX, eixoY);
                    eixoY += alturaLinha * 1.2F;

                    ev.Graphics.DrawString($"Colaboradores: {colaboradores}", font, brush, eixoX, eixoY);
                    eixoY += alturaLinha * 2;

                    // Produtos
                    ev.Graphics.DrawString("Produtos do Agendamento:", new Font("Arial", 13, FontStyle.Bold), brush, eixoX, eixoY);
                    eixoY += alturaLinha * 2;

                    foreach (var produto in listaProdutos)
                    {
                        ev.Graphics.DrawString($"-------------------------------------------------------------------", fontProduto, brush, eixoX, eixoY);
                        eixoY += alturaLinha * 1.5F;

                        ev.Graphics.DrawString($"Produto: {produto.Produto}", fontProduto, brush, eixoX, eixoY);
                        eixoY += alturaLinha * 1.2F;

                        ev.Graphics.DrawString($"SKU: {produto.SKU}", fontProduto, brush, eixoX, eixoY);
                        eixoY += alturaLinha * 1.2F;

                        ev.Graphics.DrawString($"Quantidade: {produto.Quantidade}", fontProduto, brush, eixoX, eixoY);
                        eixoY += alturaLinha * 1.2F;

                        // Verifica se deve mudar de página
                        if (eixoY + alturaLinha > ev.MarginBounds.Bottom)
                        {
                            ev.HasMorePages = true;
                            return;
                        }
                    }

                    ev.HasMorePages = false; // Última página
                };

                PrintPreviewDialog printPreview = new PrintPreviewDialog
                {
                    Document = imprimirRelatorio,
                    Width = 800,
                    Height = 600,
                    StartPosition = FormStartPosition.CenterScreen
                };

                if (printPreview.ShowDialog() == DialogResult.OK)
                {
                    imprimirRelatorio.Print();
                    MessageBox.Show("O relatório está sendo impresso.\n\nVerifique a fila de impressão!",
                                    "Imprimindo...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Impressora de relatório inválida ou não encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (MessageBox.Show("Deseja adicionar uma impressora e tentar novamente?", "Adicione uma impressora", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ChoseImpressoraRelatorioEMERGENCIA impressoraRelatorio = new ChoseImpressoraRelatorioEMERGENCIA();
                    impressoraRelatorio.Show();
                }
            }
        }

        private List<ProdutosJson> CarregarProdutos(string caminhoArquivo)
        {
            try
            {
                if (!File.Exists(caminhoArquivo))
                {
                    MessageBox.Show("Arquivo de produtos não encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new List<ProdutosJson>();
                }

                string jsonContent = File.ReadAllText(caminhoArquivo);
                var listaProdutos = JsonSerializer.Deserialize<List<ProdutosJson>>(jsonContent);
                return listaProdutos ?? new List<ProdutosJson>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar produtos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<ProdutosJson>();
            }
        }

        private void textBoxCODE_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCODE.Text == Program.modoDevCODE)
            {
                Program.modoDEVpreConfForm = "ON";
                ModoDev();
            }
        }

        private void btnQuitDEV_Click(object sender, EventArgs e)
        {
            Program.modoDEVpreConfForm = "OFF";
            ModoDev();
        }

        private void preConfForm_Load(object sender, EventArgs e)
        {
            string dataHora = DateTime.Now.ToString("dd/MM/yyyy HH'h' mm'm' ss's'");
            InfoEmpresa(this, Program.nomeEmpresa, Program.nomePasta.ToString(), Program.nomeColaborador, dataHora);
            Program.dataHoraInicioRelatorio = dataHora;
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }

            timer = new System.Windows.Forms.Timer { Interval = 1000 };
            timer.Tick += (s, args) =>
            {
                if (Program.modoDEVpreConfForm != "ON")
                {
                    tempoPreConferencia = tempoPreConferencia.Add(TimeSpan.FromSeconds(1));
                    Program.permanencia = $"{tempoPreConferencia.Hours:D2}h {tempoPreConferencia.Minutes:D2}m {tempoPreConferencia.Seconds:D2}s";
                    labelTempo.Text = Program.permanencia;
                }
            };
            timer.Start();
            AtualizarTempoEstimado();

            string filePath = $"P:\\INFORMATICA\\programas\\FULL\\KelvinV2\\agendamentos\\{Program.nomePasta}\\{Program.nomePasta}_PreConf.json";

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                MessageBox.Show("Erro ao acessar o diretório do arquivo JSON!", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                new VerAgendamentos().Show();
                return;
            }

            string jsonContent = File.ReadAllText(filePath);

            // Carregar apenas se os produtos ainda não foram carregados
            if (produtos == null || produtos.Count == 0)
            {
                CarregarProdutosDoJSON(jsonContent);
            }
            else
            {
                AtualizarTodasLabels(); // Apenas atualiza os Labels existentes, se necessário
            }
        }

        private void AtualizarTempoEstimado()
        {
            int horas = tempoSegundos / 3600;
            int minutos = (tempoSegundos % 3600) / 60;
            int segundos = tempoSegundos % 60;

            labelTempoEstimado.Text = $"{horas:D2}h {minutos:D2}m {segundos:D2}s";
        }

        private void textQTD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxCODE.Focus();
            }
        }

        private void textQTD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void foco_Shown(object sender, EventArgs e)
        {
            textBoxCODE.Focus();
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

        private void btnVoltar_Click(object sender, EventArgs e)
        { // Deseja realizar um backup para continuar mais tarde
            if (MessageBox.Show("Você vai voltar para a janela de agendamentos. VOCÊ TEM CERTEZA QUE DESEJA SAIR?\n\n Você perderá tudo que fez até o momento... (função backup ainda em desenvolvimento)", "Deseja voltar?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                //MessageBox.Show("Backup Realizado");
                this.Close();                    // Precisa Criar a lógica de backup
                new VerAgendamentos().Show();
            }
            else
            {
                //MessageBox.Show("Não será salvo o backup ;-)");
                //this.Close();
                //new VerAgendamentos().Show();
            }
        }

        public void InfoEmpresa(Form form, string nomeEmpresa, string nomePasta, string nomeColaborador, string dataHora)
        {
            labelExAgendamento.Text = nomePasta;
            labelExColaborador.Text = nomeColaborador;
            labelExDataEHora.Text = dataHora;
            labelExEmpresa.Text = nomeEmpresa;
            painelInfoEmpresa.Anchor = AnchorStyles.Right;

            form.Refresh();
        }

        private class ProdutosJson
        {
            public string Produto { get; set; }
            public string SKU { get; set; }
            public long Codebar { get; set; }
            public int Quantidade { get; set; }
        }

        private string ObterColaboradores(string caminhoArquivo)
        {
            // Verifica se o arquivo existe
            if (!File.Exists(caminhoArquivo))
            {
                MessageBox.Show("Arquivo de colaboradores não encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }

            try
            {
                // Lê o conteúdo do arquivo JSON
                string jsonContent = File.ReadAllText(caminhoArquivo);

                // Desserializa o JSON para uma lista dinâmica
                var colaboradoresJson = JsonSerializer.Deserialize<Dictionary<string, List<Dictionary<string, object>>>>(jsonContent);

                // Filtra e normaliza os colaboradores
                var colaboradores = colaboradoresJson["Pré Conferência"]
                    .Where(item => item.ContainsKey("Colaborador") && item["Colaborador"] != null)
                    .Select(item => item["Colaborador"].ToString().Trim()) // Garante que não há espaços extras
                    .Distinct(StringComparer.OrdinalIgnoreCase)            // Remove duplicatas ignorando maiúsculas/minúsculas
                    .Select(nome => char.ToUpper(nome[0]) + nome.Substring(1).ToLower()) // Normaliza para "Primeira Letra Maiúscula"
                    .ToList();

                // Formata a lista conforme especificação
                if (colaboradores.Count == 0) return string.Empty;
                if (colaboradores.Count == 1) return colaboradores[0];
                if (colaboradores.Count == 2) return $"{colaboradores[0]} e {colaboradores[1]}";
                if (colaboradores.Count == 3) return $"{colaboradores[0]}, {colaboradores[1]} e {colaboradores[2]}";

                // Para 4 ou mais colaboradores
                string primeirosNomes = string.Join(", ", colaboradores.Take(4));
                return $"{primeirosNomes}... e {colaboradores.Last()}";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao processar colaboradores: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        public class ProdutoOriginal
        {
            public string Produto { get; set; }
            public string SKU { get; set; }
            public string Codebar { get; set; }
            public int Quantidade { get; set; }
        }

        private void groupBoxInfoAg_Enter(object sender, EventArgs e)
        {

        }

        private void labelNomeJanela_Click(object sender, EventArgs e)
        {

        }

        private void btnFinalizarAgDEV_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Você está prestes a finalizar o agendamento.\n" +
                "Fazendo isso será gerado e impresso o relatório.\n" +
                "Certeza de que deseja prosseguir com isso?", "Atenção você está prestes a finalizar o agendamento", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                GerarRelatorioFinal();
            }
        }

        private void textBoxSKUDEV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Evita o som de "beep"

                string input = textBoxSKUDEV.Text.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    MessageBox.Show("Por favor, insira um SKU ou Código de Barras.", "Entrada Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verifica se o input é um SKU
                if (produtosPorSKU.ContainsKey(input))
                {
                    UnidadesProduto.Text = produtosPorSKU[input].ToString();
                }
                // Verifica se o input é um Código de Barras
                else if (long.TryParse(input, out long codebar) && produtosPorCodebar.ContainsKey(codebar))
                {
                    UnidadesProduto.Text = produtosPorCodebar[codebar].ToString();
                }
                else
                {
                    MessageBox.Show("O SKU ou Código de Barras inserido não foi encontrado.", "Não Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    UnidadesProduto.Text = "";
                }

                //textBoxSKUDEV.Clear();
                UnidadesProduto.Focus();
            }
        }

        private void btnMaisUn_Click(object sender, EventArgs e)
        {
            int valor;

            if (int.TryParse(UnidadesProduto.Text, out int result))
            {
                valor = result + 1;
                UnidadesProduto.Text = valor.ToString();
            }
        }

        private void btnMenosUn_Click(object sender, EventArgs e)
        {
            int valor;

            if (int.TryParse(UnidadesProduto.Text, out int result))
            {
                valor = result - 1;
                UnidadesProduto.Text = valor.ToString();
            }
        }

        private void btnSalvarNewUn_Click(object sender, EventArgs e)
        {
            string un = UnidadesProduto.Text.Trim();
            string idProd = textBoxSKUDEV.Text.Trim();

            if (string.IsNullOrEmpty(idProd))
            {
                MessageBox.Show("Insira o SKU ou código de barras de um produto primeiro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(un))
            {
                MessageBox.Show("Insira um valor inteiro positivo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (int.TryParse(un, out int novaQuantidade) && novaQuantidade >= 0)
            {
                if (produtosPorSKU.ContainsKey(idProd))
                {
                    produtosPorSKU[idProd] = novaQuantidade;
                    Label label = EncontrarLabel(idProd);

                    if (label != null)
                    {
                        label.Text = AtualizarTextoLabel(idProd);
                        MessageBox.Show("Quantidade atualizada com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("O SKU informado não foi encontrado nos rótulos existentes.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("O SKU informado não existe nos produtos carregados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Insira um valor válido e inteiro para a quantidade.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            textBoxSKUDEV.Focus();

            if (limpaSKUCodebar.Checked == true)
            {
                textBoxSKUDEV.Text = "";
            }
        }

        private void btnAtualizarLabels_Click(object sender, EventArgs e)
        {
            foreach (Label label in panelProdutos.Controls.OfType<Label>().ToList())
            {
                if (label.Tag is string sku && produtosPorSKU.ContainsKey(sku))
                {
                    int quantidade = produtosPorSKU[sku];
                    label.Text = AtualizarTextoLabel(sku);

                    if (quantidade == 0)
                    {
                        MoverParaPainelConcluidos(label);
                    }
                }
            }
        }

        private void ModoDev()
        {
            if (Program.modoDEVpreConfForm == "ON")
            {
                textBoxCODE.Text = "MODO DESENVOLVEDOR DESBLOQUEADO";
                textQTD.Text = "DEV";
                textBoxCODE.Enabled = false;
                textQTD.Enabled = false;
                btnQuitDEV.Show();
                AtualizarTodasLabels();
                panelDev.Visible = true;
            }
            else if (Program.modoDEVpreConfForm == "OFF")
            {
                textBoxCODE.Clear();
                textBoxCODE.Enabled = true;
                textQTD.Clear();
                textQTD.Enabled = true;
                btnQuitDEV.Hide();
                panelDev.Visible = false;
                AtualizarTodasLabels();
            }
        }

        public void BtnObsEnable()
        {
            btnObs.Enabled = true;
        }

        private void btnObs_Click(object sender, EventArgs e)
        {
            ObservacoesPreConf obsForm = new ObservacoesPreConf(this);
            obsForm.Show();
            btnObs.Enabled = false;
        }

        // Classes para mapear o JSON
        private class RelatorioFinal
        {
            public Informacoes Informacoes { get; set; }
            public List<ColaboradorRelatorio> Colaboradores { get; set; }
            public List<ProdutoRelatorio> Relatorio { get; set; }
        }

        private class Informacoes
        {
            public JsonElement Agendamento { get; set; } // Flexível para números ou strings
            public string Empresa { get; set; }
            public string DataInicio { get; set; }
            public string DataTermino { get; set; }
            public string Permanencia { get; set; }
        }

        private class ColaboradorRelatorio
        {
            [JsonPropertyName("Colaborador")]
            public string Colaborador { get; set; }
        }

        private class ProdutoRelatorio
        {
            [JsonPropertyName("Produto")]
            public string Produto { get; set; }
            public string SKU { get; set; }
            public int Quantidade { get; set; }
            public int Bipados { get; set; }
        }

        //Tirado porque não vai mais ser necessário imprimir (fora que estava dando muito problema)

        //private void ImprimirRelatorioFinal()
        //{
        //    string jsonPath = $"P:\\INFORMATICA\\programas\\FULL\\KelvinV2\\agendamentos\\{Program.nomePasta}\\{Program.nomePasta}_RelatorioPreConf.json";
        //    string caminhoArquivo = $"P:\\INFORMATICA\\programas\\FULL\\KelvinV2\\agendamentos\\{Program.nomePasta}\\info.json";

        //    if (!File.Exists(jsonPath))
        //    {
        //        MessageBox.Show("O arquivo do relatório não foi encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    string jsonContent = File.ReadAllText(jsonPath);

        //    var options = new JsonSerializerOptions
        //    {
        //        PropertyNameCaseInsensitive = true,
        //        NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString
        //    };

        //    RelatorioFinal relatorio;
        //    try
        //    {
        //        relatorio = JsonSerializer.Deserialize<RelatorioFinal>(jsonContent, options);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Erro ao desserializar o relatório: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    if (relatorio == null)
        //    {
        //        MessageBox.Show("Erro ao carregar o relatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    PrintDocument documento = new PrintDocument();

        //    if (!string.IsNullOrEmpty(Program.impressoraRelatorio))
        //    {
        //        documento.PrinterSettings.PrinterName = Program.impressoraRelatorio;

        //        if (!documento.PrinterSettings.IsValid)
        //        {
        //            MessageBox.Show("A impressora selecionada não é válida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }
        //    }

        //    int totalPaginas = 0;

        //    documento.PrintPage += (sender, e) =>
        //    {
        //        float yOffset = 30;
        //        float xOffset = 15;
        //        float lineHeight = new Font("Arial", 10).GetHeight(e.Graphics);

        //        // Cabeçalho
        //        var agendamento = relatorio.Informacoes.Agendamento;
        //        e.Graphics.DrawString($"Agendamento nº: {agendamento.ToString()}", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, xOffset, yOffset);
        //        yOffset += lineHeight * 2;

        //        e.Graphics.DrawString($"Empresa: {relatorio.Informacoes?.Empresa ?? "N/A"}", new Font("Arial", 10), Brushes.Black, xOffset, yOffset);
        //        yOffset += lineHeight;

        //        e.Graphics.DrawString($"Data e hora de início: {relatorio.Informacoes?.DataInicio ?? "N/A"}", new Font("Arial", 10), Brushes.Black, xOffset, yOffset);
        //        yOffset += lineHeight;

        //        e.Graphics.DrawString($"Data e hora de término: {relatorio.Informacoes?.DataTermino ?? "N/A"}", new Font("Arial", 10), Brushes.Black, xOffset, yOffset);
        //        yOffset += lineHeight;

        //        e.Graphics.DrawString($"Permanência: {relatorio.Informacoes?.Permanencia ?? "N/A"}", new Font("Arial", 10), Brushes.Black, xOffset, yOffset);
        //        yOffset += lineHeight * 2;

        //        e.Graphics.DrawString("Produtos do Agendamento:", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, xOffset, yOffset);
        //        yOffset += lineHeight * 2;

        //        foreach (var produto in relatorio.Relatorio)
        //        {
        //            e.Graphics.DrawString($"Produto: {produto.Produto ?? "Produto sem nome"}", new Font("Arial", 10), Brushes.Black, xOffset, yOffset);
        //            yOffset += lineHeight;

        //            e.Graphics.DrawString($"SKU: {produto.SKU}", new Font("Arial", 10), Brushes.Black, xOffset, yOffset);
        //            yOffset += lineHeight;

        //            e.Graphics.DrawString($"Quantidade: {produto.Quantidade}", new Font("Arial", 10), Brushes.Black, xOffset, yOffset);
        //            yOffset += lineHeight;

        //            e.Graphics.DrawString($"Bipados: {produto.Bipados}", new Font("Arial", 10), Brushes.Black, xOffset, yOffset);
        //            yOffset += lineHeight * 2;

        //            if (yOffset + lineHeight > e.MarginBounds.Bottom)
        //            {
        //                e.HasMorePages = true;
        //                totalPaginas++;
        //                return;
        //            }
        //        }

        //        e.HasMorePages = false;
        //        totalPaginas++;
        //    };

        //    // Simulação de páginas
        //    documento.PrintController = new PreviewPrintController();
        //    documento.Print();

        //    MessageBox.Show($"Número estimado de páginas: {totalPaginas}", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //    PrintPreviewDialog printPreview = new PrintPreviewDialog
        //    {
        //        Document = documento,
        //        Width = 800,
        //        Height = 600,
        //        StartPosition = FormStartPosition.CenterScreen
        //    };

        //    if (printPreview.ShowDialog() == DialogResult.OK)
        //    {
        //        documento.PrintController = new StandardPrintController();
        //        documento.Print();
        //        FinalizarAgendamento(caminhoArquivo);
        //    }
        //}
        private void UnidadesProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSalvarNewUn.Focus();
            }
        }
    }
}

