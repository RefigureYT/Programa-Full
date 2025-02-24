using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramaFull.Formulários
{
    public partial class ChoseImpressoraEtiqueta : Form
    {
        public ChoseImpressoraEtiqueta()
        {
            InitializeComponent();
            GetImpressora();
        }

        private void GetImpressora()
        {
            foreach (string impressora in PrinterSettings.InstalledPrinters)
            {
                listaImpressoras.Items.Add(impressora);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Configuracoes configuracoes = new Configuracoes();
            configuracoes.Show();

            Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if(listaImpressoras != null)
            {
                Program.impressoraEtiqueta = listaImpressoras.Text;

                Configuracoes configuracoes = new Configuracoes();
                configuracoes.Show();

                Configuracoes.AtualizarImpressoraPadraoArquivo();
                Close();
            }
        }
    }
}
