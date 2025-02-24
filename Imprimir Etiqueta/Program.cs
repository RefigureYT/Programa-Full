using System.Runtime.InteropServices;

namespace Imprimir_Etiqueta
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Inicializa a configura��o da aplica��o.
            ApplicationConfiguration.Initialize();

            // Chama a fun��o para enviar o arquivo ZPL para a impressora.
            string caminhoArquivoZPL = "C:\\caminho\\para\\seu\\arquivo.zpl";
            string nomeImpressora = "NomeDaImpressora";

            try
            {
                EnviarArquivoParaImpressora(nomeImpressora, caminhoArquivoZPL);
                Console.WriteLine("Arquivo ZPL enviado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar arquivo para a impressora: {ex.Message}");
            }

            // Executa o formul�rio principal da aplica��o.
            Application.Run(new Form1());
        }

        public static void EnviarArquivoParaImpressora(string nomeImpressora, string caminhoArquivoZPL)
        {
            if (!File.Exists(caminhoArquivoZPL))
                throw new Exception($"Arquivo ZPL n�o encontrado: {caminhoArquivoZPL}");

            string conteudoZPL = File.ReadAllText(caminhoArquivoZPL);

            IntPtr hPrinter = IntPtr.Zero;
            IntPtr pBytes;
            int dwCount = conteudoZPL.Length;

            DOCINFO docInfo = new DOCINFO
            {
                pDocName = "Arquivo ZPL",
                pDataType = "RAW"
            };

            if (!OpenPrinter(nomeImpressora, out hPrinter, IntPtr.Zero))
                throw new Exception("N�o foi poss�vel abrir a impressora.");

            if (!StartDocPrinter(hPrinter, 1, ref docInfo))
                throw new Exception("N�o foi poss�vel iniciar o documento na impressora.");

            try
            {
                pBytes = Marshal.StringToCoTaskMemAnsi(conteudoZPL);
                if (!WritePrinter(hPrinter, pBytes, dwCount, out _))
                    throw new Exception("Erro ao enviar dados para a impressora.");

                Marshal.FreeCoTaskMem(pBytes);
            }
            finally
            {
                EndDocPrinter(hPrinter);
                ClosePrinter(hPrinter);
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DOCINFO
        {
            [MarshalAs(UnmanagedType.LPTStr)] public string pDocName;
            [MarshalAs(UnmanagedType.LPTStr)] public string pOutputFile;
            [MarshalAs(UnmanagedType.LPTStr)] public string pDataType;
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
    }
}
