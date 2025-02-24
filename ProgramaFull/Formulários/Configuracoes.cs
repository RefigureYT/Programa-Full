using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProgramaFull.Formulários
{
    public partial class Configuracoes : Form
    {

        // Construtor do formulário
        public Configuracoes()
        {
            InitializeComponent();
        }


        // Evento para salvar a impressora selecionada
        private void btnSalvar_Click(object sender, EventArgs e)
        {

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            Close();
        }

        private void btnSelecionarImpressoraRelatorio_Click(object sender, EventArgs e)
        {
            ChoseImpressoraRelatorio impressoraRelatorio = new ChoseImpressoraRelatorio();
            impressoraRelatorio.Show();

            Close();
        }

        private void Configuracoes_Load(object sender, EventArgs e)
        {
            if (Program.impressoraRelatorio == string.Empty)
            {
                impressoraAtualRelatorioVisual.Text = "Nenhuma impressora selecionada";
            }
            else if (Program.impressoraRelatorio != string.Empty)
            {
                impressoraAtualRelatorioVisual.Text = Program.impressoraRelatorio;
            }

            if (Program.impressoraEtiqueta == string.Empty)
            {
                impressoraAtualEtiquetaVisual.Text = "Nenhuma impressora selecionada";
            }
            else if (Program.impressoraEtiqueta != string.Empty)
            {
                impressoraAtualEtiquetaVisual.Text = Program.impressoraEtiqueta;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChoseImpressoraEtiqueta formEtiqueta = new ChoseImpressoraEtiqueta();
            formEtiqueta.Show();

            Close();
        }

        private void imprimirPaginaTeste_Click(object sender, EventArgs e)
        {
            PrintDocument imprimir = new PrintDocument();

            imprimir.PrinterSettings.PrinterName = Program.impressoraRelatorio;

            if (imprimir.PrinterSettings.IsValid)
            {
                imprimir.PrintPage += (s, ev) =>
                {
                    ev.Graphics.DrawString("Este é um relatório (A4)\n\n Página teste ;-)", new Font("Arial", 16), Brushes.Black, 100, 100);
                };
                imprimir.Print();

                MessageBox.Show("Página teste está sendo impressa. Verifique a fila de impressão!", "Imprimindo...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Impressora de relatório inválida ou não encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void AtualizarImpressoraPadraoArquivo()
        {
            string directoryPath = "C:\\FULL\\configuracoes";
            string filePath = Path.Combine(directoryPath, "config.json");

            // Certifica-se de que o diretório existe
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            Dictionary<string, List<Dictionary<string, string>>> configData;

            if (File.Exists(filePath))
            {
                string existingContent = File.ReadAllText(filePath);
                configData = JsonSerializer.Deserialize<Dictionary<string, List<Dictionary<string, string>>>>(existingContent);
            }
            else
            {
                configData = new Dictionary<string, List<Dictionary<string, string>>>();
            }

            if (!configData.ContainsKey("Impressoras"))
            {
                configData["Impressoras"] = new List<Dictionary<string, string>>();
            }

            var impressoraData = configData["Impressoras"].FirstOrDefault();

            if (impressoraData == null)
            {
                impressoraData = new Dictionary<string, string>
                {
                    { "ImpressoraRelatorio", Program.impressoraRelatorio },
                    { "ImpressoraEtiqueta", Program.impressoraEtiqueta }
                };
                configData["Impressoras"].Add(impressoraData);
            }
            else
            {
                impressoraData["ImpressoraRelatorio"] = Program.impressoraRelatorio;
                impressoraData["ImpressoraEtiqueta"] = Program.impressoraEtiqueta;
            }

            string updatedContent = JsonSerializer.Serialize(configData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, updatedContent);
        }
    }
}