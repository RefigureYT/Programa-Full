using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramaFull.Formulários
{
    public partial class UploadAgendamento : Form
    {
        public UploadAgendamento()
        {
            InitializeComponent();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Visible = true;
            Close();
        }

        private void btnEmbalar_Click(object sender, EventArgs e)
        {
            string driveEmbalar = "https://drive.google.com/drive/folders/1uJO5NKdtDcUl9uLTWdexmkoOx4apDa0Y";

            try
            {
                // Abre o link no navegador padrão
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = driveEmbalar,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao abrir o link: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            JanelaCarregamento();
        }


        private void btnEncaixotar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ainda em desenvolvimento...", "Em breve", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnExpedicao_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ainda em desenvolvimento...", "Em breve", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void BtnPreConferencia_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnPreConferencia_Click(object sender, EventArgs e)
        {

            string drivePreConferencia = "https://drive.google.com/drive/folders/1Q31xHdS1xniJoMjwgeP2x3d1O3rYHzhx";

            try
            {
                // Abre o link no navegador padrão
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = drivePreConferencia,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao abrir o link: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            JanelaCarregamento();
        }

        private static void JanelaCarregamento()
        {
            
        }
    }
}
