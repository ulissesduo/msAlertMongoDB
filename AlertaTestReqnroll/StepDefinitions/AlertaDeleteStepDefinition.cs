using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NJsonSchema;
using Reqnroll;

namespace AlertaTestReqnroll.StepDefinitions
{
    [Binding]
    public class AlertaDeleteStepDefinition
    {
        private HttpResponseMessage _response;

        [Given(@"a API rodando no docker")]
        public void GivenAApiRodandoNoDocker()
        {
            // Nothing to do, just a placeholder for clarity.
        }

        [When(@"executar requisição DELETE para ""(.*)""")]
        public async Task WhenExecutarRequisicaoDELETEPara(string endpoint)
        {
            var client = new HttpClient();
            var baseUrl = "http://localhost:8080"; 
            _response = await client.DeleteAsync(baseUrl + endpoint);
        }

        [Then(@"a resposta deve ser (.*) No Content")]
        public async Task ThenARespostaDeveSerNoContent(int expectedStatusCode)
        {

            ((int)_response.StatusCode).Should().Be(expectedStatusCode);

            var content = await _response.Content.ReadAsStringAsync();

            if (!string.IsNullOrWhiteSpace(content))
            {
                var schema = await JsonSchema.FromFileAsync("Schemas/AlertaDeleteResponse.json");
                var errors = schema.Validate(content);
                errors.Should().BeEmpty("JSON response should match schema");
            }
        }
    }
}
