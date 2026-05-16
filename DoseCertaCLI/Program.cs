using DoseCertaCLI.Services;

using Spectre.Console;

Console.Title = "DoseCerta CLI";

MedicamentoService medicamentoService = new();

HidratacaoService hidratacaoService = new();

ApiMedicamentoService apiMedicamentoService = new();

bool executando = true;

while (executando)
{
    Console.Clear();

    medicamentoService.VerificarMedicamentosPendentes();

    Console.WriteLine("=================================");
    Console.WriteLine("         DOSECERTA CLI          ");
    Console.WriteLine("=================================");
    Console.WriteLine("1 - Cadastrar medicamento");
    Console.WriteLine("2 - Listar medicamentos");
    Console.WriteLine("3 - Marcar como tomado");
    Console.WriteLine("4 - Registrar hidratação");
    Console.WriteLine("5 - Ver progresso hidratação");
    Console.WriteLine("6 - Consultar medicamento online");
    Console.WriteLine("0 - Sair");

    Console.Write("\nEscolha uma opção: ");

    string? opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            medicamentoService.CadastrarMedicamento();
            break;

        case "2":
            medicamentoService.ListarMedicamentos();
            break;

        case "3":
            medicamentoService.MarcarComoTomado();
            break;

        case "4":
            hidratacaoService.RegistrarConsumo();
            break;

        case "5":
            hidratacaoService.ExibirProgresso();
            break;

        case "6":
            await apiMedicamentoService
                .ConsultarMedicamentoOnline();
            break;

        case "0":
            executando = false;
            break;

        default:
            Console.WriteLine("\nOpção inválida.");
            break;
    }

    if (executando)
    {
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
}