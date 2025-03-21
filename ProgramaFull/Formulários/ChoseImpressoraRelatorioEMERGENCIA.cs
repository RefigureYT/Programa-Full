﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramaFull.Formulários
{
    public partial class ChoseImpressoraRelatorioEMERGENCIA : Form
    {
        public ChoseImpressoraRelatorioEMERGENCIA()
        {
            InitializeComponent();
            GetImpressoras();
        }

        private void GetImpressoras()
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
            if (listaImpressoras != null)
            {
                Program.impressoraRelatorio = listaImpressoras.Text;

                preConfForm form = new preConfForm();
                form.imprimir();

                Close();
            }
        }
    }
}
