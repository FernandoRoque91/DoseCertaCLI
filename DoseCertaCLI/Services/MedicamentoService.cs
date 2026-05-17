using System.Text.Json;

using DoseCertaCLI.Models;

namespace DoseCertaCLI.Services;

public class MedicamentoService
{
    private readonly string caminhoArquivo = "Data/medicamentos.json";

    public List<Medicamento> ObterMedicamentos()
    {
        if (!File.Exists(caminhoArquivo))
        {
            return [];
        }

        string json = File.ReadAllText(caminhoArquivo);

        if (string.IsNullOrWhiteSpace(json))
        {
            return [];
        }

        return JsonSerializer.Deserialize<List<Medicamento>>(json) ?? [];
    }

    public void SalvarMedicamentos(List<Medicamento> medicamentos)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(medicamentos, options);

        File.WriteAllText(caminhoArquivo, json);
    }

    public void CadastrarMedicamento()
    {
        List<Medicamento> medicamentos = ObterMedicamentos();

        Console.Clear();
        Console.WriteLine("=== Cadastro de Medicamento ===\n");

        Console.Write("Nome: ");
        string nome = Console.ReadLine() ?? "";

        Console.Write("Dosagem: ");
        string dosagem = Console.ReadLine() ?? "";

        Console.Write("Horários (separados por vírgula): ");
        string horariosInput = Console.ReadLine() ?? "";

        List<string> horarios = horariosInput
            .Split(',')
            .Select(h => h.Trim())
            .ToList();

        Medicamento medicamento = new()
        {
            Nome = nome,
            Dosagem = dosagem,
            Horarios = horarios,
            HorariosTomados = []
        };

        medicamentos.Add(medicamento);

        SalvarMedicamentos(medicamentos);

        Console.WriteLine("\nMedicamento cadastrado com sucesso!");
    }

    public void ListarMedicamentos()
    {
        List<Medicamento> medicamentos = ObterMedicamentos();

        Console.Clear();
        Console.WriteLine("=== Lista de Medicamentos ===\n");

        if (medicamentos.Count == 0)
        {
            Console.WriteLine("Nenhum medicamento cadastrado.");
            return;
        }

        foreach (var medicamento in medicamentos)
        {
            Console.WriteLine($"Nome: {medicamento.Nome}");
            Console.WriteLine($"Dosagem: {medicamento.Dosagem}");
            Console.WriteLine($"Horários: {string.Join(", ", medicamento.Horarios)}");
            Console.WriteLine($"Horários tomados: {string.Join(", ", medicamento.HorariosTomados)}");
            Console.WriteLine("----------------------------------");
        }
    }

    public void MarcarComoTomado()
    {
        List<Medicamento> medicamentos = ObterMedicamentos();

        Console.Clear();
        Console.WriteLine("=== Marcar Medicamento Como Tomado ===\n");

        if (medicamentos.Count == 0)
        {
            Console.WriteLine("Nenhum medicamento cadastrado.");
            return;
        }

        for (int i = 0; i < medicamentos.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {medicamentos[i].Nome}");
        }

        Console.Write("\nEscolha o medicamento: ");

        bool numeroValido = int.TryParse(Console.ReadLine(), out int escolha);

        if (!numeroValido || escolha < 1 || escolha > medicamentos.Count)
        {
            Console.WriteLine("\nOpção inválida.");
            return;
        }

        Medicamento medicamento = medicamentos[escolha - 1];

        Console.WriteLine("\nHorários disponíveis:");

        for (int i = 0; i < medicamento.Horarios.Count; i++)
        {
            string horario = medicamento.Horarios[i];

            bool tomado = medicamento.HorariosTomados
                .Any(h => h.StartsWith(horario));

            Console.WriteLine($"{i + 1} - {horario} {(tomado ? "(Tomado)" : "")}");
        }

        Console.Write("\nEscolha o horário: ");

        bool horarioValido = int.TryParse(Console.ReadLine(), out int horarioEscolhido);

        if (!horarioValido ||
            horarioEscolhido < 1 ||
            horarioEscolhido > medicamento.Horarios.Count)
        {
            Console.WriteLine("\nHorário inválido.");
            return;
        }

        string horarioSelecionado = medicamento.Horarios[horarioEscolhido - 1];

        bool horarioJaTomado = medicamento.HorariosTomados
            .Any(h => h.StartsWith(horarioSelecionado));

        if (!horarioJaTomado)
        {
            string registroTomado =
                $"{horarioSelecionado} - {DateTime.Now:dd/MM/yyyy HH:mm}";

            medicamento.HorariosTomados.Add(registroTomado);
        }

        SalvarMedicamentos(medicamentos);

        Console.WriteLine("\nMedicamento marcado como tomado!");
    }
    public void VerificarMedicamentosPendentes()
    {
        List<Medicamento> medicamentos = ObterMedicamentos();

        TimeOnly horarioAtual = TimeOnly.FromDateTime(DateTime.Now);

        List<string> pendentes =
            ObterMedicamentosPendentes(medicamentos, horarioAtual);

        if (pendentes.Count > 0)
        {
            Console.WriteLine("⚠ Medicamentos pendentes:\n");

            foreach (var pendente in pendentes)
            {
                Console.WriteLine($"- {pendente}");
            }

            Console.WriteLine();
        }
    }
    public List<string> ObterMedicamentosPendentes(
        List<Medicamento> medicamentos,
        TimeOnly horarioAtual)
    {
        List<string> pendentes = [];

        foreach (var medicamento in medicamentos)
        {
            foreach (var horario in medicamento.Horarios)
            {
                bool horarioValido = TimeOnly.TryParse(
                    horario,
                    out TimeOnly horarioMedicamento);

                if (!horarioValido)
                {
                    continue;
                }

                bool horarioPassou = horarioAtual >= horarioMedicamento;

                bool foiTomado = medicamento.HorariosTomados
                    .Any(h => h.StartsWith(horario));

                if (horarioPassou && !foiTomado)
                {
                    pendentes.Add($"{medicamento.Nome} → {horario}");
                }
            }
        }

        return pendentes;
    }
}