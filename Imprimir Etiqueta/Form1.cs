using System.Drawing.Printing;
using System.Drawing;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Text;

namespace Imprimir_Etiqueta
{
    public partial class Form1 : Form
    {
        private List<Etiqueta> etiquetas;
        private string impressoraEtiqueta = string.Empty;

        public Form1()
        {
            InitializeComponent();
            GetImpressora();
            EtiquetasNaComboBox();
        }

        private void GetImpressora()
        {
            foreach (string impressoras in PrinterSettings.InstalledPrinters)
            {
                listBoxImpressoras.Items.Add(impressoras);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (listBoxImpressoras.SelectedItem == null)
            {
                MessageBox.Show("Por favor selecione uma impressora primeiro!", "Campo impressoras vazio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxEtiqueta.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecione uma etiqueta.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(impressoraEtiqueta))
            {
                MessageBox.Show("Por favor, selecione uma impressora válida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string etiquetaSelecionada = comboBoxEtiqueta.SelectedItem.ToString();
            Etiqueta etiqueta = etiquetas.FirstOrDefault(etq => etq.EtiquetaId == etiquetaSelecionada);

            if (etiqueta == null)
            {
                MessageBox.Show($"Etiqueta '{etiquetaSelecionada}' não encontrada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Seleciona o primeiro produto da etiqueta para geração de labels
                var produto = etiqueta.Produtos.FirstOrDefault();
                if (produto == null)
                {
                    MessageBox.Show("Nenhum produto encontrado para esta etiqueta.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Gera ZPL usando a quantidade de etiquetas da etiqueta
                string conteudoZPL = GerarEtiquetas(etiqueta, produto);

                if (!EnviarArquivoZPLParaImpressora(impressoraEtiqueta, conteudoZPL))
                {
                    throw new Exception("Erro ao enviar o arquivo ZPL para a impressora.");
                }

                MessageBox.Show("Etiquetas enviadas para a impressora com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar ou enviar etiquetas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static bool EnviarArquivoZPLParaImpressora(string printerName, string fileContent)
        {
            IntPtr hPrinter = IntPtr.Zero;
            int dwWritten = 0;
            int lastError;

            try
            {
                if (!OpenPrinter(printerName, out hPrinter, IntPtr.Zero))
                {
                    lastError = Marshal.GetLastWin32Error();
                    throw new Exception($"Não foi possível abrir a impressora. Erro: {lastError}");
                }

                DOCINFO di = new DOCINFO
                {
                    pDocName = "ZPL Document",
                    pDataType = "RAW",
                    pOutputFile = null
                };

                if (!StartDocPrinter(hPrinter, 1, ref di))
                {
                    lastError = Marshal.GetLastWin32Error();
                    throw new Exception($"Não foi possível iniciar o documento na impressora. Erro: {lastError}");
                }

                if (!StartPagePrinter(hPrinter))
                {
                    lastError = Marshal.GetLastWin32Error();
                    throw new Exception($"Não foi possível iniciar a página na impressora. Erro: {lastError}");
                }

                byte[] bytes = Encoding.ASCII.GetBytes(fileContent);
                IntPtr pUnmanagedBytes = Marshal.AllocCoTaskMem(bytes.Length);
                Marshal.Copy(bytes, 0, pUnmanagedBytes, bytes.Length);

                if (!WritePrinter(hPrinter, pUnmanagedBytes, bytes.Length, out dwWritten))
                {
                    lastError = Marshal.GetLastWin32Error();
                    Marshal.FreeCoTaskMem(pUnmanagedBytes);
                    throw new Exception($"Erro ao enviar dados para a impressora. Erro: {lastError}");
                }

                Marshal.FreeCoTaskMem(pUnmanagedBytes);

                if (!EndPagePrinter(hPrinter))
                {
                    lastError = Marshal.GetLastWin32Error();
                    throw new Exception($"Não foi possível finalizar a página na impressora. Erro: {lastError}");
                }

                if (!EndDocPrinter(hPrinter))
                {
                    lastError = Marshal.GetLastWin32Error();
                    throw new Exception($"Não foi possível finalizar o documento na impressora. Erro: {lastError}");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao imprimir: {ex.Message}");
            }
            finally
            {
                if (hPrinter != IntPtr.Zero)
                {
                    ClosePrinter(hPrinter);
                }
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DOCINFO
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }

        [DllImport("winspool.drv", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool OpenPrinter(string pPrinterName, out IntPtr phPrinter, IntPtr pDefault);

        [DllImport("winspool.drv", SetLastError = true)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, int level, ref DOCINFO pDocInfo);

        [DllImport("winspool.drv", SetLastError = true)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

        [DllImport("winspool.drv", SetLastError = true)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        public static bool SendStringToPrinter(string printerName, string data)
        {
            IntPtr hPrinter = IntPtr.Zero;
            IntPtr pBytes;
            int dwCount = data.Length;
            DOCINFO docInfo = new DOCINFO { pDocName = "ZPL Document", pDataType = "RAW" };

            try
            {
                if (!OpenPrinter(printerName, out hPrinter, IntPtr.Zero))
                    throw new Exception("Não foi possível abrir a impressora.");

                if (!StartDocPrinter(hPrinter, 1, ref docInfo))
                    throw new Exception("Não foi possível iniciar o documento na impressora.");

                pBytes = Marshal.StringToCoTaskMemAnsi(data);

                if (!WritePrinter(hPrinter, pBytes, dwCount, out _))
                    throw new Exception("Erro ao enviar dados para a impressora.");

                Marshal.FreeCoTaskMem(pBytes);
                EndDocPrinter(hPrinter);
                ClosePrinter(hPrinter);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void btnSalvarImpressora_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(listBoxImpressoras.Text))
            {
                impressoraEtiqueta = listBoxImpressoras.Text;
                MessageBox.Show($"Sua impressora agora é a {impressoraEtiqueta}", "Impressora selecionada com sucesso.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Por favor selecione uma impressora primeiro!", "Campo impressoras vazio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GerarEtiquetas(Etiqueta etiqueta, Produto produto)
        {
            StringBuilder zplCompleto = new StringBuilder();
            int xColunaEsquerda = 30;
            int xColunaDireita = 350;
            int etiquetasGeradas = 0;
            int linhasNecessarias = (int)Math.Ceiling(etiqueta.QtdEtiquetas / 2.0);

            for (int linha = 0; linha < linhasNecessarias; linha++)
            {
                // Início de cada label set
                zplCompleto.Append("^XA^CI28\n");
                zplCompleto.Append("^LH0,0\n");

                // Primeira etiqueta da linha (Coluna Esquerda)
                if (etiquetasGeradas < etiqueta.QtdEtiquetas)
                {
                    zplCompleto.Append($@"
^FO{xColunaEsquerda},15^BY2,,0^BCN,54,N,N^FD{etiqueta.EtiquetaId}^FS
^FO{xColunaEsquerda + 75},75^A0N,20,25^FH^FD{etiqueta.EtiquetaId}^FS
^FO{xColunaEsquerda + 75},76^A0N,20,25^FH^FD{etiqueta.EtiquetaId}^FS
^FO{xColunaEsquerda - 14},115^A0N,18,18^FB300,2,2,L^FH^FD{etiqueta.Anuncio}^FS
^FO{xColunaEsquerda - 14},153^A0N,18,18^FB300,1,0,L^FH^FD{produto.NomeProduto}^FS
^FO{xColunaEsquerda - 14},172^A0N,18,18^FH^FDSKU: {produto.SKU}^FS");
                    etiquetasGeradas++;
                }

                // Segunda etiqueta da linha (Coluna Direita)
                if (etiquetasGeradas < etiqueta.QtdEtiquetas)
                {
                    zplCompleto.Append($@"
^FO{xColunaDireita},15^BY2,,0^BCN,54,N,N^FD{etiqueta.EtiquetaId}^FS
^FO{xColunaDireita + 75},75^A0N,20,25^FH^FD{etiqueta.EtiquetaId}^FS
^FO{xColunaDireita + 75},76^A0N,20,25^FH^FD{etiqueta.EtiquetaId}^FS
^FO{xColunaDireita - 14},115^A0N,18,18^FB300,2,2,L^FH^FD{etiqueta.Anuncio}^FS
^FO{xColunaDireita - 14},153^A0N,18,18^FB300,1,0,L^FH^FD{produto.NomeProduto}^FS
^FO{xColunaDireita - 14},172^A0N,18,18^FH^FDSKU: {produto.SKU}^FS");
                    etiquetasGeradas++;
                }

                // Fim de cada label set
                zplCompleto.Append("\n^XZ\n");
            }

            return zplCompleto.ToString();
        }


        private void EtiquetasNaComboBox()
        {
            string caminhoArquivo = @"P:\\INFORMATICA\\programas\\FULL\\KelvinV2\\agendamentos\\Teste2\\Teste2_Embalar.json";

            if (!File.Exists(caminhoArquivo))
            {
                MessageBox.Show("Arquivo JSON não encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string json = File.ReadAllText(caminhoArquivo);
                etiquetas = JsonDesserializar(json);

                comboBoxEtiqueta.Items.Clear();

                foreach (var etq in etiquetas)
                {
                    comboBoxEtiqueta.Items.Add(etq.EtiquetaId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao processar o arquivo JSON: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static List<Etiqueta> JsonDesserializar(string json)
        {
            try
            {
                var parsedJson = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);
                var etiquetas = new List<Etiqueta>();

                foreach (var item in parsedJson)
                {
                    foreach (var key in item.Keys)
                    {
                        if (key == "Anuncio" || key == "Qtd Etiquetas")
                            continue;

                        var produtos = JsonConvert.DeserializeObject<List<Produto>>(item[key].ToString());
                        var etiqueta = new Etiqueta
                        {
                            EtiquetaId = key,
                            Produtos = produtos,
                            Anuncio = item.ContainsKey("Anuncio") ? item["Anuncio"].ToString() : null,
                            QtdEtiquetas = item.ContainsKey("Qtd Etiquetas") ?
                                int.Parse(item["Qtd Etiquetas"].ToString()) :
                                (produtos?.FirstOrDefault()?.Quantidade ?? 0)
                        };
                        etiquetas.Add(etiqueta);
                    }
                }

                return etiquetas;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return new List<Etiqueta>();
            }
        }

        public class Etiqueta
        {
            public string EtiquetaId { get; set; }
            public List<Produto> Produtos { get; set; }
            public string Anuncio { get; set; }
            public int QtdEtiquetas { get; set; } // New field to store the number of labels
        }

        public class Produto
        {
            public string NomeProduto { get; set; }
            public string SKU { get; set; }
            public string Codebar { get; set; }
            public int Quantidade { get; set; }
        }
    }
}