using System.Text.Json;

using DoseCertaCLI.Models;

namespace DoseCertaCLI.Services;

public class ApiMedicamentoService
{
    private readonly HttpClient httpClient;

    public ApiMedicamentoService(
        HttpClient? client = null)
    {
        httpClient = client ?? new HttpClient();
    }

    public async Task<string> BuscarInformacoesMedicamento(
        string nomeMedicamento)
    {
        try
        {
            string url =
                $"https://api.fda.gov/drug/label.json?search=openfda.brand_name:{nomeMedicamento}&limit=1";

            HttpResponseMessage response =
                await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return "Medicamento não encontrado.";
            }

            string json =
                await response.Content.ReadAsStringAsync();

            OpenFdaResponse? dados =
                JsonSerializer.Deserialize<OpenFdaResponse>(json);

            if (dados?.Results == null ||
                dados.Results.Count == 0)
            {
                return "Nenhuma informação encontrada.";
            }

            MedicamentoApiResult resultado =
                dados.Results[0];

            string descricao =
                resultado.Description?.FirstOrDefault()
                ?? resultado.Indicacoes?.FirstOrDefault()
                ?? "Descrição não disponível.";

            return descricao;
        }
        catch
        {
            return "Erro ao consultar API.";
        }
    }

    public async Task ConsultarMedicamentoOnline()
    {
        Console.Clear();

        Console.WriteLine(
            "=== Consulta Online de Medicamentos ===\n");

        Console.Write("Digite o nome do medicamento: ");

        string? nome = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("\nNome inválido.");
            return;
        }

        Console.WriteLine("\nConsultando API...\n");

        string resultado =
            await BuscarInformacoesMedicamento(nome);

        Console.WriteLine(
            "=== Informações do Medicamento ===\n");

        Console.WriteLine(
            $"Medicamento consultado: {nome}\n");

        Console.WriteLine(
            "Informação encontrada na base internacional:\n");

        Console.WriteLine(resultado); ;

    }
}