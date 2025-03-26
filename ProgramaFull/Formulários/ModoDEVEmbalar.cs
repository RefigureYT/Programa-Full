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
    public partial class ModoDEVEmbalar : Form
    {
        private EmbalarEtiquetagemBIPE embalarForm;

        public ModoDEVEmbalar(EmbalarEtiquetagemBIPE embalarFormRef)
        {
            InitializeComponent();
            embalarForm = embalarFormRef;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            embalarForm.CarregarAnuncios();
        }
    }
}
