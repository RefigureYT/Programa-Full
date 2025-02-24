using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ProgramaFull.Program;

namespace ProgramaFull.Formulários
{
    public partial class Menu : Form
    {

        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            string configFilePath = "C:\\FULL\\configuracoes\\config.json";

            if (File.Exists(configFilePath))
            {
                string jsonContent = File.ReadAllText(configFilePath);
                var configData = JsonSerializer.Deserialize<Dictionary<string, List<Dictionary<string, string>>>>(jsonContent);

                if (configData != null && configData.ContainsKey("Impressoras"))
                {
                    var impressoraConfig = configData["Impressoras"].FirstOrDefault();

                    if (impressoraConfig != null)
                    {
                        impressoraRelatorio = impressoraConfig.ContainsKey("ImpressoraRelatorio") ? impressoraConfig["ImpressoraRelatorio"] : string.Empty;
                        impressoraEtiqueta = impressoraConfig.ContainsKey("ImpressoraEtiqueta") ? impressoraConfig["ImpressoraEtiqueta"] : string.Empty;
                    }
                }
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Você está prestes a fechar essa aba, pressione \"OK\" se estiver de acordo.", "AVISO", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void btnVerAg_Click(object sender, EventArgs e)
        {
            VerAgendamentos agendamentos = new VerAgendamentos();

            agendamentos.Show();
            this.Visible = false;
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            Configuracoes configuracoes = new Configuracoes();
            configuracoes.Show();
            this.Visible = false;
        }

        private void btnUploadDrive_Click(object sender, EventArgs e)
        {
            UploadAgendamento uploadAgendamento = new UploadAgendamento();
            uploadAgendamento.Show();

            this.Visible = false;
        }
    }
}
