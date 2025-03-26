using System.Linq.Expressions;
using System.Text.Json;
using ProgramaFull.Formul�rios;
using Npgsql;

namespace ProgramaFull
{
    internal static class Program
    {
        public static int nomePasta;
        public static string nomeColaborador = string.Empty;
        public static string nomeEmpresa = string.Empty;
        public static string statusAtual = string.Empty;
        public static string modoDEVpreConfForm = "OFF";
        public static string modoDevCODE = "aZC1u1lW0en0dSEZ";
        public static bool confirmarEmpresa;
        public static string impressoraRelatorio = string.Empty;
        public static string impressoraEtiqueta = string.Empty;
        public static string dataHoraInicioRelatorio = string.Empty;
        public static string permanencia = string.Empty;
        public static string accessTokenTinyV3 = string.Empty;
        public static string linkReportButton = "https://encurtador.com.br/XVIM3";


        public static string typeVersion = "alpha";
        public static string version = "1.0.4";
        // Vers�es:
        // alpha  = vers�o inicial, inst�vel, para testes internos (Com muitos bugs)
        // beta   = mais est�vel, com funcionalidades completas, ainda em testes (Com menos bugs)
        // rc     = candidato � vers�o final, sem bugs cr�ticos
        // release = vers�o est�vel, pronta para produ��o

        public static List<int> kitsConfirmados = new List<int>();

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

        public static void AtualizarInfoJson(string nomePasta, string etapaAtual, string colaborador)
        {
            string caminhoJson = Path.Combine("P:\\INFORMATICA\\programas\\FULL\\KelvinV2\\agendamentos", nomePasta, "info.json");

            if (!File.Exists(caminhoJson))
            {
                // Cria estrutura b�sica
                var estruturaInicial = new Dictionary<string, List<Dictionary<string, object>>>();
                var jsonInicial = JsonSerializer.Serialize(estruturaInicial, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(caminhoJson, jsonInicial);
            }


            string jsonContent = File.ReadAllText(caminhoJson);
            var options = new JsonSerializerOptions { WriteIndented = true };

            // Corrigido: Agora for�amos `dados` a ser um `Dictionary`
            Dictionary<string, List<Dictionary<string, object>>> dados =
                JsonSerializer.Deserialize<Dictionary<string, List<Dictionary<string, object>>>>(jsonContent) ?? new Dictionary<string, List<Dictionary<string, object>>>();

            // Obt�m a empresa a partir da etapa "Pr� Confer�ncia" (se dispon�vel)
            // Tenta obter a empresa do JSON, sen�o usa o valor atual do programa
            string empresa = string.IsNullOrWhiteSpace(Program.nomeEmpresa) ? "Indefinido" : Program.nomeEmpresa; // <-- usa o valor salvo em mem�ria caso n�o encontre nada ele define como "Indefinido"
            if (dados.ContainsKey("Pr� Confer�ncia") && dados["Pr� Confer�ncia"].Count > 0)
            {
                if (dados["Pr� Confer�ncia"][0].ContainsKey("Empresa") &&
                    !string.IsNullOrEmpty(dados["Pr� Confer�ncia"][0]["Empresa"]?.ToString()))
                {
                    empresa = dados["Pr� Confer�ncia"][0]["Empresa"].ToString();
                }
            }

            // Se a etapa n�o existir ainda, cria um novo array vazio
            if (!dados.ContainsKey(etapaAtual))
            {
                dados[etapaAtual] = new List<Dictionary<string, object>>();
            }

            // Verifica se a entrada j� existe para evitar duplica��o
            bool entradaJaExiste = dados[etapaAtual].Any(entry =>
                entry.ContainsKey("Colaborador") && entry["Colaborador"].ToString() == colaborador &&
                entry.ContainsKey("Empresa") && entry["Empresa"].ToString() == empresa
            );

            if (!entradaJaExiste)
            {
                dados[etapaAtual].Add(new Dictionary<string, object>
                {
                    { "Status", etapaAtual },
                    { "Empresa", empresa },
                    { "Conclu�do", false },
                    { "Colaborador", colaborador }
                });
            }

            // Serializa e grava o JSON atualizado
            string novoJson = JsonSerializer.Serialize(dados, options);
            File.WriteAllText(caminhoJson, novoJson);
        }

        public static async Task BuscarAccessTokenTinyAsync()
        {
            string connString = "Host=192.168.15.200;Port=5432;Username=root;Password=T3Jetm3Mz4N8CDK1ovj3XhC2w6n0PTeEb189Q2D2AjobuxP2Me;Database=n8n_geral_db";

            using (var conn = new NpgsqlConnection(connString))
            {
                try
                {
                    await conn.OpenAsync();
                    string query = "SELECT access_token FROM public.api_tokens WHERE nome = 'Silvio'";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        object result = await cmd.ExecuteScalarAsync();
                        accessTokenTinyV3 = result?.ToString(); // Define a vari�vel global com o access_token

                        if (string.IsNullOrEmpty(accessTokenTinyV3))
                        {
                            MessageBox.Show("Access Token n�o encontrado no banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao conectar ao banco de dados:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}