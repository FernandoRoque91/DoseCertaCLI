using System.Text.Json;

using DoseCertaCLI.Models;

namespace DoseCertaCLI.Services;

public class HidratacaoService
{
    private readonly string caminhoArquivo = "Data/hidratacao.json";

    public Hidratacao ObterDados()
    {
        if (!File.Exists(caminhoArquivo))
        {
            return new Hidratacao
            {
                MetaDiaria = 2000,
                ConsumidoHoje = 0
            };
        }

        string json = File.ReadAllText(caminhoArquivo);

        return JsonSerializer.Deserialize<Hidratacao>(json)
            ?? new Hidratacao
            {
                MetaDiaria = 2000,
                ConsumidoHoje = 0
            };
    }

    public void SalvarDados(Hidratacao hidratacao)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json =
            JsonSerializer.Serialize(hidratacao, options);

        File.WriteAllText(caminhoArquivo, json);
    }

    public void RegistrarConsumo()
    {
        Hidratacao hidratacao = ObterDados();

        Console.Clear();

        Console.WriteLine("=== Registrar Consumo de Água ===\n");

        Console.Write("Quantidade em ml: ");

        bool valorValido =
            int.TryParse(Console.ReadLine(), out int quantidade);

        if (!valorValido || quantidade <= 0)
        {
            Console.WriteLine("\nValor inválido.");
            return;
        }

        hidratacao.ConsumidoHoje += quantidade;

        SalvarDados(hidratacao);

        Console.WriteLine("\nConsumo registrado com sucesso!");
    }

    public void ExibirProgresso()
    {
        Hidratacao hidratacao = ObterDados();

        Console.Clear();

        Console.WriteLine("=== Hidratação ===\n");

        int percentual =
            (hidratacao.ConsumidoHoje * 100)
            / hidratacao.MetaDiaria;

        Console.WriteLine(
            $"Meta diária: {hidratacao.MetaDiaria}ml");

        Console.WriteLine(
            $"Consumido hoje: {hidratacao.ConsumidoHoje}ml");

        Console.WriteLine(
            $"Progresso: {percentual}%");
    }
}