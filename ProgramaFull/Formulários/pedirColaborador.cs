using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ProgramaFull;

namespace ProgramaFull.Formulários
{
    public partial class pedirColaborador : Form
    {
        public pedirColaborador()
        {
            InitializeComponent();
            Label numeroAg = new Label
            {
                Text = "Agendamento nº" + Program.nomePasta.ToString(),
                Location = new Point(21, 95),
                Font = new Font("Segoe UI Black", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                AutoSize = false,
                Size = new Size(236, 21),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            this.Controls.Add(numeroAg);
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

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(caixaTexto.Text))
            {
                Program.nomeColaborador = caixaTexto.Text;

                // Atualiza as informações e abre a próxima janela diretamente
                Program.AtualizarInfoJson();
                preConfForm janelaPreConferencia = new preConfForm();
                janelaPreConferencia.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Você não pode enviar um valor vazio, coloque o nome do colaborador.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void caixaTexto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK_Click(sender, e); // Chama o buttonOK_Click
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void caixaTexto_TextChanged(object sender, EventArgs e)
        {

        }

        private void caixaTexto_Load(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            caixaTexto.Focus();
        }
    }
}
