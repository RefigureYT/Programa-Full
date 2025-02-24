using System.Linq.Expressions;
using System.Text.Json;
using ProgramaFull.Formul�rios;
namespace ProgramaFull
{
    internal static class Program
    {
        public static int nomePasta;
        public static string nomeColaborador;
        public static string nomeEmpresa;
        public static string statusAtual;
        public static string modoDEVpreConfForm = "OFF";
        public static string modoDevCODE = "2802200824012011153264897123456789147258369";
        public static bool confirmarEmpresa;
        public static string impressoraRelatorio = string.Empty;
        public static string impressoraEtiqueta = string.Empty;
        public static string dataHoraInicioRelatorio = string.Empty;
        public static string permanencia = string.Empty;

        /// <summary>
        ///  The main entry point for    the application. 
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Menu());
        }

        // M�todo para criar ou atualizar o arquivo info.json
        public static void AtualizarJsonParaNovaEtapa(string etapaAtual)
        {
            // Caminho do arquivo
            string diretorio = $@"P:\INFORMATICA\programas\FULL\KelvinV2\agendamentos\{nomePasta}\";
            string caminhoArquivo = Path.Combine(diretorio, "info.json");

            // Cria o diret�rio, se n�o existir
            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            try
            {
                Dictionary<string, List<Dictionary<string, object>>> jsonData = new();

                if (File.Exists(caminhoArquivo))
                {
                    // L� o arquivo existente
                    string jsonContent = File.ReadAllText(caminhoArquivo);
                    jsonData = JsonSerializer.Deserialize<Dictionary<string, List<Dictionary<string, object>>>>(jsonContent);
                }

                // Verifica a empresa e o colaborador existentes na etapa atual
                string empresaAtual = nomeEmpresa;
                string colaboradorAtual = nomeColaborador;

                if (jsonData.ContainsKey(etapaAtual) && jsonData[etapaAtual].Count > 0)
                {
                    var ultimaEntrada = jsonData[etapaAtual].Last();
                    if (ultimaEntrada.ContainsKey("Empresa"))
                    {
                        empresaAtual = ultimaEntrada["Empresa"].ToString();
                    }
                    if (ultimaEntrada.ContainsKey("Colaborador"))
                    {
                        colaboradorAtual = ultimaEntrada["Colaborador"].ToString();
                    }
                }

                // Define a pr�xima etapa
                string proximaEtapa = etapaAtual switch
                {
                    "Pr� Confer�ncia" => "Embalar",
                    "Embalar" => "Encaixotar",
                    "Encaixotar" => "Expedi��o",
                    _ => "Conclu�do"
                };

                // Adiciona a pr�xima etapa ao JSON, se n�o existir
                if (!jsonData.ContainsKey(proximaEtapa))
                {
                    jsonData[proximaEtapa] = new List<Dictionary<string, object>>();
                }

                jsonData[proximaEtapa].Add(new Dictionary<string, object>
        {
            { "Status", proximaEtapa },
            { "Empresa", empresaAtual },
            { "Conclu�do", false },
            { "Colaborador", colaboradorAtual }
        });

                // Salva o arquivo JSON atualizado
                string jsonAtualizado = JsonSerializer.Serialize(jsonData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(caminhoArquivo, jsonAtualizado);

                //MessageBox.Show("Arquivo info.json atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar o arquivo JSON: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static int ConverterStatusParaNumero(string status)
        {
            return status switch
            {
                "Pr� Confer�ncia" => 4,
                "Embalar" => 3,
                "Encaixotar" => 2,
                "Expedi��o" => 1,
                "Conclu�do" => 0,
                _ => -1 // Retorna -1 para valores inv�lidos
            };
        }

        public static string ObterCaminhoArquivo(string nomePasta, string status)
        {
            int statusNumero = ConverterStatusParaNumero(status);

            if (statusNumero == -1)
            {
                throw new Exception($"Status inv�lido: {status}");
            }

            // Caminho completo para o arquivo
            string caminhoArquivo = $@"P:\INFORMATICA\programas\FULL\KelvinV2\agendamentos\{nomePasta}\{nomePasta}_{statusNumero}.json";

            return caminhoArquivo;
        }

        public static void AtualizarInfoJson()
        {
            // Caminho do arquivo
            string diretorio = $@"P:\INFORMATICA\programas\FULL\KelvinV2\agendamentos\{nomePasta}\";
            string caminhoArquivo = Path.Combine(diretorio, "info.json");

            // Normaliza a chave de status para evitar inconsist�ncias
            string chaveStatus = statusAtual.Replace("_", " ");

            // Objeto base a ser adicionado
            var novaEntrada = new Dictionary<string, object>
    {
        { "Status", statusAtual },
        { "Empresa", nomeEmpresa },
        { "Conclu�do", false },
        { "Colaborador", nomeColaborador }
    };

            try
            {
                // Dicion�rio que representar� o JSON
                Dictionary<string, List<Dictionary<string, object>>> jsonData;

                if (File.Exists(caminhoArquivo))
                {
                    // L� o conte�do do JSON existente
                    string conteudoAtual = File.ReadAllText(caminhoArquivo);
                    jsonData = JsonSerializer.Deserialize<Dictionary<string, List<Dictionary<string, object>>>>(conteudoAtual);

                    // Verifica se a chave existe no JSON
                    if (jsonData.ContainsKey(chaveStatus))
                    {
                        // Atualiza ou adiciona a entrada na lista correspondente
                        var lista = jsonData[chaveStatus];

                        // Verifica se j� existe uma entrada com o colaborador atual
                        var existente = lista.FirstOrDefault(item =>
                            item.ContainsKey("Colaborador") &&
                            item["Colaborador"].ToString() == nomeColaborador);

                        if (existente == null)
                        {
                            // Adiciona uma nova entrada � lista se o colaborador n�o existe
                            lista.Add(novaEntrada);
                        }

                        // Atualiza a vari�vel `nomeEmpresa` com o valor da primeira ocorr�ncia da empresa
                        var primeiraEmpresa = lista.FirstOrDefault(item => item.ContainsKey("Empresa"));
                        if (primeiraEmpresa != null && primeiraEmpresa.ContainsKey("Empresa"))
                        {
                            nomeEmpresa = primeiraEmpresa["Empresa"].ToString();
                        }
                    }
                    else
                    {
                        // Adiciona uma nova chave com a entrada
                        jsonData[chaveStatus] = new List<Dictionary<string, object>> { novaEntrada };
                    }
                }
                else
                {
                    // Cria um novo JSON se o arquivo n�o existir
                    jsonData = new Dictionary<string, List<Dictionary<string, object>>>
            {
                { chaveStatus, new List<Dictionary<string, object>> { novaEntrada } }
            };

                    // Atualiza `nomeEmpresa` com o valor inicial
                    nomeEmpresa = novaEntrada["Empresa"].ToString();
                }

                // Salva o arquivo JSON atualizado
                string jsonAtualizado = JsonSerializer.Serialize(jsonData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(caminhoArquivo, jsonAtualizado);

                //MessageBox.Show("Arquivo info.json atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar o arquivo JSON: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}