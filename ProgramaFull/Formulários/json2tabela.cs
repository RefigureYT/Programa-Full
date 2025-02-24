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
    public partial class json2tabela : Form
    {
        public json2tabela()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            VerAgendamentos verAgendamentos = new VerAgendamentos();
            verAgendamentos.Show();
            Close();
        }

        public void CarregarDados(string jsonContent)
        {
            try
            {
                // Desserializa o JSON para uma lista de dicionários
                var dados = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonContent);

                // Preenche o DataTable
                var dataTable = new DataTable();

                if (dados != null && dados.Count > 0)
                {
                    // Obtém as colunas a partir do primeiro item
                    foreach (var coluna in dados.First().Keys)
                    {
                        dataTable.Columns.Add(coluna);
                    }

                    // Adiciona os dados à tabela
                    foreach (var item in dados)
                    {
                        var row = dataTable.NewRow();
                        foreach (var coluna in item.Keys)
                        {
                            row[coluna] = item[coluna];
                        }
                        dataTable.Rows.Add(row);
                    }
                }

                // Vincula o DataTable ao DataGridView
                var dataGridView = new DataGridView
                {
                    DataSource = dataTable,
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                };

                painelTabela.Controls.Clear();
                painelTabela.Controls.Add(dataGridView);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao processar os dados do JSON: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void json2tabela_Load(object sender, EventArgs e)
        {
            Label placeholder = new Label
            {
                Text = "Selecione um agendamento para visualizar os dados.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 14, FontStyle.Bold)
            };

            painelTabela.Controls.Clear();
            painelTabela.Controls.Add(placeholder);
        }
    }
}
