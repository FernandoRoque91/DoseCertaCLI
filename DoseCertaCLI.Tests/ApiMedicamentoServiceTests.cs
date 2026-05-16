using System.Net;
using System.Text;
using DoseCertaCLI.Services;

namespace DoseCertaCLI.Tests;

public class ApiMedicamentoServiceTests
{
    [Fact]
    public async Task BuscarInformacoesMedicamento_DeveRetornarDescricao()
    {
        string jsonFake = """
        {
          "results": [
            {
              "description": [
                "Medicamento utilizado para dores e febre."
              ]
            }
          ]
        }
        """;

        var handler = new FakeHttpMessageHandler(
            jsonFake,
            HttpStatusCode.OK);

        HttpClient client = new(handler);

        ApiMedicamentoService service =
            new(client);

        string resultado =
            await service.BuscarInformacoesMedicamento(
                "dipirona");

        Assert.Contains(
            "dores e febre",
            resultado);
    }
}

public class FakeHttpMessageHandler : HttpMessageHandler
{
    private readonly string responseContent;

    private readonly HttpStatusCode statusCode;

    public FakeHttpMessageHandler(
        string responseContent,
        HttpStatusCode statusCode)
    {
        this.responseContent = responseContent;

        this.statusCode = statusCode;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        HttpResponseMessage response = new()
        {
            StatusCode = statusCode,

            Content = new StringContent(
                responseContent,
                Encoding.UTF8,
                "application/json")
        };

        return Task.FromResult(response);
    }
}