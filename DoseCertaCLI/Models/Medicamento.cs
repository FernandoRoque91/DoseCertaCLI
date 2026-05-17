using System.Linq;
using System.Text.Json;
namespace DoseCertaCLI.Models;

public class Medicamento
{
    public string Nome { get; set; } = string.Empty;

    public string Dosagem { get; set; } = string.Empty;

    public List<string> Horarios { get; set; } = [];

    public List<string> HorariosTomados { get; set; } = [];

    public DateTime? DataUltimaDose { get; set; }
}