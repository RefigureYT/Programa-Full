using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Reflection.Metadata.BlobBuilder;

namespace ProgramaFull.Formulários
{
    public partial class ObservacoesPreConf : Form
    {
        private preConfForm _preConfForm;
        public ObservacoesPreConf(preConfForm preConfForm)
        {
            InitializeComponent();
            _preConfForm = preConfForm;
        }

        private void ObservacoesPreConf_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            _preConfForm.BtnObsEnable();
            Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // Caminho do diretório e do arquivo
            string directoryPath = $"P:\\INFORMATICA\\programas\\FULL\\KelvinV2\\agendamentos\\{Program.nomePasta}";
            string filePath = Path.Combine(directoryPath, $"{Program.nomePasta}_ObsPreConf.json");

            // Criar o diretório se não existir
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Estrutura do arquivo JSON
            var obsData = new Dictionary<string, object>();

            if (File.Exists(filePath))
            {
                // Ler o conteúdo existente do arquivo
                string existingContent = File.ReadAllText(filePath);

                if (!string.IsNullOrWhiteSpace(existingContent))
                {
                    // Desserializar o JSON existente
                    var jsonDocument = JsonDocument.Parse(existingContent);
                    if (jsonDocument.RootElement.TryGetProperty("OBS", out var obsElement))
                    {
                        // Converter JsonElement para o tipo desejado
                        obsData["OBS"] = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(obsElement.GetRawText());
                    }
                    else
                    {
                        // Inicializar OBS se não existir no arquivo
                        obsData["OBS"] = new List<Dictionary<string, object>>();
                    }
                }
            }
            else
            {
                // Adicionar a chave OBS se o arquivo não existir
                obsData["OBS"] = new List<Dictionary<string, object>>();
            }

            // Obter a lista OBS
            var obsList = obsData["OBS"] as List<Dictionary<string, object>>;

            // Obter a data e hora atual
            string currentDateTime = DateTime.Now.ToString("dd-MM-yyyy HH'h' mm'm' ss's'");

            // Adicionar nova observação
            var newEntry = new Dictionary<string, object>
            {
                [currentDateTime] = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["Observação"] = insiraObs.Text,
                        ["Colaborador"] = Program.nomeColaborador,
                        ["DEV"] = "Kelvin"
                    }
                }
            };

            obsList.Add(newEntry);

            // Atualizar o conteúdo do arquivo
            obsData["OBS"] = obsList;
            string updatedContent = JsonSerializer.Serialize(obsData, new JsonSerializerOptions { WriteIndented = true });

            // Salvar o arquivo
            File.WriteAllText(filePath, updatedContent);

            if (MessageBox.Show("Observação salva com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
            {
                _preConfForm.BtnObsEnable();
                Close();
            }
        }
    }
}
