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
        
        private void codigoProdutoTxtBox_KeyDown(object sender, KeyEventArgs e)
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
                            MessageBox.Show($"Produto Bipado: {produtoBipado}\n\nEstá presente nos anúncios: {anunciosLista}",
                                            "Produto Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    }
}
