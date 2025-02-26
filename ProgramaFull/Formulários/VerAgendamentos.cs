    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Text.Json;
    using System.Drawing.Text;
    using System.Diagnostics;

    namespace ProgramaFull.Formulários
    {
    public partial class VerAgendamentos : Form
    {
        public VerAgendamentos()
        {
            InitializeComponent();
        }

        private void comboBoxFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtroSelecionado = comboBoxFiltro.SelectedItem.ToString();
            string diretorio = @"P:\INFORMATICA\programas\FULL\KelvinV2\agendamentos";

            LoadPastas(diretorio, filtroSelecionado);
        }

        private void VerAgendamentos_Load(object sender, EventArgs e)
        {
            comboBoxFiltro.Items.Add("Todos");
            comboBoxFiltro.Items.Add("Pré Conferência");
            comboBoxFiltro.Items.Add("Embalar");
            comboBoxFiltro.Items.Add("Etiquetagem");
            comboBoxFiltro.Items.Add("Expedição");

            comboBoxFiltro.SelectedIndex = 0;
            comboBoxFiltro.SelectedIndexChanged += comboBoxFiltro_SelectedIndexChanged;

            string diretorio = @"P:\INFORMATICA\programas\FULL\KelvinV2\agendamentos";
            LoadPastas(diretorio);
        }


        public void LoadPastas(string diretorio, string filtro = "Todos")
        {
            if (Directory.Exists(diretorio))
            {
                painelAgendamentos.Controls.Clear();
                string[] pastas = Directory.GetDirectories(diretorio);

                foreach (var pasta in pastas)
                {
                    string jsonPath = Path.Combine(pasta, "info.json");
                    string statusStatus = "Pré Conferência"; // Valor padrão inicial
                    string statusEmpresa = "Indefinido"; // Valor padrão para empresa

                    if (File.Exists(jsonPath))
                    {
                        try
                        {
                            string jsonContent = File.ReadAllText(jsonPath);
                            using JsonDocument document = JsonDocument.Parse(jsonContent);
                            JsonElement root = document.RootElement;

                            string[] etapas = { "Expedição", "Encaixotar", "Embalar", "Pré Conferência" };

                            foreach (string etapa in etapas)
                            {
                                if (root.TryGetProperty(etapa, out JsonElement etapaArray) &&
                                    etapaArray.ValueKind == JsonValueKind.Array &&
                                    etapaArray.GetArrayLength() > 0)
                                {
                                    var primeiroItem = etapaArray[0];

                                    if (primeiroItem.TryGetProperty("Empresa", out JsonElement empresa))
                                    {
                                        statusEmpresa = empresa.GetString();
                                    }

                                    if (primeiroItem.TryGetProperty("Concluído", out JsonElement concluidoElement))
                                    {
                                        bool concluidoValor = concluidoElement.ValueKind switch
                                        {
                                            JsonValueKind.True => true,
                                            JsonValueKind.False => false,
                                            JsonValueKind.String => concluidoElement.GetString()?.Equals("true", StringComparison.OrdinalIgnoreCase) ?? false,
                                            _ => false
                                        };

                                        bool todasConcluidas = etapaArray.EnumerateArray().All(item =>
                                        {
                                            if (item.TryGetProperty("Concluído", out JsonElement concluidoElement))
                                            {
                                                return concluidoElement.ValueKind == JsonValueKind.True ||
                                                       (concluidoElement.ValueKind == JsonValueKind.String && concluidoElement.GetString()?.Equals("true", StringComparison.OrdinalIgnoreCase) == true);
                                            }
                                            return false; // Se não existir a chave "Concluído", considera como não concluído
                                        });

                                        if (todasConcluidas && etapa != "Concluído")
                                        {
                                            Dictionary<string, string> proximaEtapa = new Dictionary<string, string>
                                            {
                                                { "Pré Conferência", "Embalar" },
                                                { "Embalar", "Encaixotar" },
                                                { "Encaixotar", "Expedição" },
                                                { "Expedição", "Concluído" }
                                            };
                                            statusStatus = proximaEtapa.ContainsKey(etapa) ? proximaEtapa[etapa] : etapa;
                                        }

                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            statusStatus = "Pré Conferência";
                            statusEmpresa = "Indefinido";
                        }
                    }

                    if (filtro != "Todos" && statusStatus != filtro)
                    {
                        continue;
                    }

                    GroupBox groupBoxClone = new GroupBox()
                    {
                        Width = caixaAg.Width - 20,
                        Height = caixaAg.Height,
                        Text = Path.GetFileName(pasta),
                    };

                    Label statusName = new Label
                    {
                        Text = "Status:",
                        Location = new Point(58, 29),
                        Font = new Font("MS Reference Sans Serif", 12, FontStyle.Regular)
                    };

                    Label statusValue = new Label
                    {
                        Text = statusStatus,  // Exibe o status atual
                        Location = new Point(119, 32),
                        Font = new Font("MS Reference Sans Serif", 9, FontStyle.Bold),
                        AutoSize = true
                    };

                    PictureBox imagemPasta = new PictureBox
                    {
                        Size = new Size(51, 50),
                        Location = new Point(6, 16),
                        Image = Properties.Resources.Pasta,
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };

                    Label empresaName = new Label
                    {
                        Text = "Empresa:",
                        Location = new Point(261, 28),
                        Font = new Font("MS Reference Sans Serif", 12, FontStyle.Regular),
                        AutoSize = true
                    };

                    Label empresaValue = new Label
                    {
                        Text = statusEmpresa,
                        Location = new Point(345, 31),
                        TextAlign = ContentAlignment.MiddleRight,
                        Font = new Font("MS Reference Sans Serif", 9.75F, FontStyle.Bold),
                        AutoSize = true
                    };

                    Button buttonVer = new Button
                    {
                        Width = 66,
                        Height = 23,
                        Location = new Point(560, 28),
                        Text = "Ver",
                        TabStop = false
                    };
                    buttonVer.Click += (s, e) => buttonVer_Click(s, e, Path.GetFileName(pasta), statusStatus);

                    Button buttonEditar = new Button
                    {
                        Width = 83,
                        Height = 23,
                        Location = new Point(630, 28),
                        Text = "Editar",
                        TabStop = false,
                        Enabled = false
                    };

                    bool concluido = false;
                    string buttonText = "Começar";

                    if (File.Exists(jsonPath))
                    {
                        string jsonContent = File.ReadAllText(jsonPath);
                        using JsonDocument document = JsonDocument.Parse(jsonContent);
                        JsonElement root = document.RootElement;

                        if (root.TryGetProperty(statusStatus, out JsonElement etapaArray) &&
                            etapaArray.ValueKind == JsonValueKind.Array &&
                            etapaArray.GetArrayLength() > 0)
                        {
                            // Verifica se todas as entradas daquela etapa foram concluídas
                            bool todasConcluidas = etapaArray.EnumerateArray().All(item =>
                                item.TryGetProperty("Concluído", out JsonElement concluidoElement) &&
                                (
                                    concluidoElement.ValueKind == JsonValueKind.True || // Se for um booleano true
                                    (concluidoElement.ValueKind == JsonValueKind.String && concluidoElement.GetString()?.Equals("true", StringComparison.OrdinalIgnoreCase) == true) // Se for uma string "true"
                                )
                            );


                            if (todasConcluidas)
                            {
                                concluido = true;
                                buttonText = "Continuar";
                            }
                        }
                    }

                    // Determina a etapa anterior com base na ordem das etapas
                    Dictionary<string, string> etapaAnteriorMap = new Dictionary<string, string>
                    {
                        { "Embalar", "Pré Conferência" },
                        { "Encaixotar", "Embalar" },
                        { "Expedição", "Encaixotar" }
                    };

                    bool etapaAnteriorConcluida = false;

                    if (File.Exists(jsonPath))
                    {
                        string jsonContent = File.ReadAllText(jsonPath);
                        using JsonDocument document = JsonDocument.Parse(jsonContent);
                        JsonElement root = document.RootElement;

                        if (etapaAnteriorMap.ContainsKey(statusStatus) && root.TryGetProperty(etapaAnteriorMap[statusStatus], out JsonElement etapaAnteriorArray))
                        {
                            // Verifica se todas as entradas da etapa anterior estão concluídas
                            etapaAnteriorConcluida = etapaAnteriorArray.EnumerateArray().All(item =>
                                item.TryGetProperty("Concluído", out JsonElement concluidoElement) &&
                                (concluidoElement.ValueKind == JsonValueKind.True || concluidoElement.GetString()?.Equals("true", StringComparison.OrdinalIgnoreCase) == true)
                            );
                        }
                    }

                    // Agora, o botão só será habilitado se a etapa anterior estiver concluída
                    Button buttonComecar = new Button
                    {
                        Width = 109,
                        Height = 23,
                        Location = new Point(720, 28),
                        Text = buttonText,
                        Tag = statusStatus,
                        TabStop = false,
                        Anchor = AnchorStyles.Right,
                        Enabled = etapaAnteriorConcluida || statusStatus == "Pré Conferência" // Pré Conferência não precisa verificar etapa anterior
                    };


                    buttonComecar.Click += (s, e) => buttonComecar_click(s, e, Path.GetFileName(pasta), statusStatus);

                    groupBoxClone.Controls.Add(imagemPasta);
                    groupBoxClone.Controls.Add(statusValue);
                    groupBoxClone.Controls.Add(buttonVer);
                    groupBoxClone.Controls.Add(buttonEditar);
                    groupBoxClone.Controls.Add(buttonComecar);
                    groupBoxClone.Controls.Add(statusName);
                    groupBoxClone.Controls.Add(empresaName);
                    groupBoxClone.Controls.Add(empresaValue);

                    painelAgendamentos.Controls.Add(groupBoxClone);
                }
            }
        }


        private void buttonComecar_click(object sender, EventArgs e, string nomePasta, string statusStatus)
        {
            Close();
            // Verifica se o nomePasta pode ser convertido para um número válido
            if (int.TryParse(nomePasta, out int numeroAgendamento))
            {
                // Armazena o número do agendamento e o status atual no programa
                Program.nomePasta = numeroAgendamento;
                Program.statusAtual = statusStatus;

                Button clickedButton = sender as Button; // Identifica o botão que foi clicado

                if (clickedButton != null && clickedButton.Text == "Começar")
                {
                    // Dicionário com a ordem das etapas
                    Dictionary<string, string> proximaEtapa = new Dictionary<string, string>
                    {
                        { "Pré Conferência", "Embalar" },
                        { "Embalar", "Encaixotar" },
                        { "Encaixotar", "Expedição" },
                        { "Expedição", "Concluído" }
                    };

                    if (statusStatus == "Pré Conferência")
                    {
                        // Abre a janela para pedir o nome da empresa
                        pedirEmpresa janelaEmpresa = new pedirEmpresa();
                        janelaEmpresa.Show();
                        this.Close(); // Fecha a janela atual
                    }
                    else if (proximaEtapa.ContainsKey(statusStatus))
                    {
                        // Abre a janela para solicitar o nome do colaborador
                        pedirColaborador janelaColaborador = new pedirColaborador();
                        if (janelaColaborador.ShowDialog() == DialogResult.OK)
                        {
                            string colaborador = Program.nomeColaborador;
                             
                            // Atualiza o JSON para a próxima etapa
                            Program.AtualizarInfoJson(nomePasta, proximaEtapa[statusStatus], colaborador);

                            // Determina qual formulário abrir na próxima etapa
                            Form proximoFormulario = proximaEtapa[statusStatus] switch
                            {
                                "Embalar" => new EmbalarEtiquetagemBIPE(),
                                //"Encaixotar" => new EncaixotarForm(),
                                //"Expedição" => new ExpedicaoForm(),
                                _ => null
                            };

                            if (proximoFormulario != null)
                            {
                                proximoFormulario.Show();
                            }
                        }
                    }
                }
                else
                {
                    // Caso o botão não seja "Começar", solicita apenas o nome do colaborador
                    pedirColaborador janelaColaborador = new pedirColaborador();
                    Program.confirmarEmpresa = false; // Define que não será necessário confirmar a empresa
                    janelaColaborador.Show();
                    this.Close();
                }
            }
            else
            {
                // Exibe mensagem de erro caso o nomePasta não seja um número válido
                MessageBox.Show("O número do agendamento precisa ser um número válido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AtualizarInfoJsonParaEmbalar(string nomePasta, string colaborador)
        {
            // Define o caminho para o arquivo JSON
            string caminhoJson = Path.Combine("P:\\INFORMATICA\\programas\\FULL\\KelvinV2\\agendamentos", nomePasta, "info.json");

            if (!File.Exists(caminhoJson))
            {
                // Exibe mensagem de erro caso o arquivo JSON não seja encontrado
                MessageBox.Show("Arquivo info.json não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string jsonContent = File.ReadAllText(caminhoJson); // Lê o conteúdo do arquivo JSON
            var options = new JsonSerializerOptions { WriteIndented = true }; // Define opções para formatação do JSON
            var dados = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonContent); // Deserializa o JSON para um dicionário

            if (dados == null)
            {
                // Exibe mensagem de erro caso o JSON não seja válido
                MessageBox.Show("Erro ao ler o conteúdo do arquivo JSON.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string empresa = ""; // Inicializa a variável para armazenar o nome da empresa
            if (dados.ContainsKey("Pré Conferência"))
            {
                // Tenta extrair o nome da empresa do JSON da etapa "Pré Conferência"
                var preConferencia = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(dados["Pré Conferência"].ToString());
                if (preConferencia != null && preConferencia.Count > 0 && preConferencia[0].ContainsKey("Empresa"))
                {
                    empresa = preConferencia[0]["Empresa"].ToString();
                }
            }

            // Cria uma nova entrada para a etapa "Embalar"
            var embalarArray = new List<Dictionary<string, object>>();
            var novoItem = new Dictionary<string, object>
                {
                    { "Status", "Embalar" },
                    { "Empresa", empresa },
                    { "Concluído", false },
                    { "Colaborador", colaborador }
                };

            embalarArray.Add(novoItem);
            dados["Embalar"] = embalarArray; // Adiciona a nova entrada ao dicionário principal

            string novoJson = JsonSerializer.Serialize(dados, options); // Serializa o dicionário atualizado
            File.WriteAllText(caminhoJson, novoJson); // Escreve o conteúdo atualizado no arquivo

            // Exibe mensagem de sucesso
            MessageBox.Show("Arquivo JSON atualizado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AbrirFormularioEtiquetagem()
        {
            // Abre o formulário para etiquetagem
            EmbalarEtiquetagemBIPE etiquetagemForm = new EmbalarEtiquetagemBIPE();
            etiquetagemForm.Show();
        }

        private void buttonVer_Click(object sender, EventArgs e, string nomePasta, string statusStatus)
        {
            try
            {
                // Obtém o caminho do arquivo baseado no status
                string caminhoArquivo = Program.ObterCaminhoArquivo(nomePasta, statusStatus);

                // Log para depuração
                MessageBox.Show($"Tentando acessar o arquivo: {caminhoArquivo}");

                if (!File.Exists(caminhoArquivo))
                {
                    MessageBox.Show($"O arquivo {Path.GetFileName(caminhoArquivo)} não foi encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lê o conteúdo do JSON
                string jsonContent = File.ReadAllText(caminhoArquivo);

                // Abre a janela json2tabela e passa os dados do JSON
                json2tabela janelaTabela = new json2tabela();
                janelaTabela.CarregarDados(jsonContent); // Método para carregar os dados
                janelaTabela.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao processar o arquivo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
            Menu menu = new Menu();
            menu.Show();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void buttonVer_Click(object sender, EventArgs e)
        {

        }

        private void labStatus_Click(object sender, EventArgs e)
        {

        }

        private void painelAgendamentos_Paint(object sender, PaintEventArgs e)
        {

        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            string diretorio = @"P:\INFORMATICA\programas\FULL\KelvinV2\agendamentos";

            // Obtém o filtro atual do comboBox
            string filtroSelecionado = comboBoxFiltro.SelectedItem.ToString();

            // Chama LoadPastas() novamente com o filtro atual
            LoadPastas(diretorio, filtroSelecionado);

        }
    }
}