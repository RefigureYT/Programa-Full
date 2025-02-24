using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ProgramaFull.Formulários
{
    public partial class pedirEmpresa : Form
    {
        public pedirEmpresa()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            Label numeroAg = new Label
            {
                Text = "Agendamento nº" + Program.nomePasta.ToString(),
                Location = new Point(21, 95),
                Font = new Font("Segoe UI Black", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                AutoSize = false,
                Size = new Size(236, 21),
                ForeColor = Color.White,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            this.Controls.Add(numeroAg);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                Program.nomeEmpresa = comboBox1.SelectedItem.ToString();
                pedirColaborador janela = new pedirColaborador();
                janela.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Você deve selecionar uma empresa!", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pedirEmpresa_Load(object sender, EventArgs e)
        {

        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            VerAgendamentos verAgendamentos = new VerAgendamentos();
            verAgendamentos.Show();
            Close();
        }

        private void labelTesteAgendamento_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
