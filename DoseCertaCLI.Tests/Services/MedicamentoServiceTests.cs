using DoseCertaCLI.Models;
using DoseCertaCLI.Services;

namespace DoseCertaCLI.Tests.Services;

public class MedicamentoServiceTests
{
    [Fact]
    public void DeveRetornarMedicamentoPendente()
    {
        // Arrange
        MedicamentoService service = new();

        List<Medicamento> medicamentos =
        [
            new Medicamento
            {
                Nome = "Dipirona",
                Dosagem = "500mg",
                Horarios = ["08:00"],
                HorariosTomados = []
            }
        ];

        TimeOnly horarioAtual = new(10, 0);

        // Act
        List<string> resultado =
            service.ObterMedicamentosPendentes(
                medicamentos,
                horarioAtual);

        // Assert
        Assert.Single(resultado);

        Assert.Contains(
            "Dipirona → 08:00",
            resultado);
    }

    [Fact]
    public void NaoDeveRetornarMedicamentoJaTomado()
    {
        // Arrange
        MedicamentoService service = new();

        List<Medicamento> medicamentos =
        [
            new Medicamento
            {
                Nome = "Dipirona",
                Dosagem = "500mg",
                Horarios = ["08:00"],
                HorariosTomados = ["08:00"]
            }
        ];

        TimeOnly horarioAtual = new(10, 0);

        // Act
        List<string> resultado =
            service.ObterMedicamentosPendentes(
                medicamentos,
                horarioAtual);

        // Assert
        Assert.Empty(resultado);
    }
}