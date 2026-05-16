using System.Text.Json.Serialization;

namespace DoseCertaCLI.Models;

public class OpenFdaResponse
{
    [JsonPropertyName("results")]
    public List<MedicamentoApiResult>? Results { get; set; }
}

public class MedicamentoApiResult
{
    [JsonPropertyName("description")]
    public List<string>? Description { get; set; }

    [JsonPropertyName("indications_and_usage")]
    public List<string>? Indicacoes { get; set; }
}