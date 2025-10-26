using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertaTestReqnroll.StepDefinitions
{
    [Binding]
    public class AlertaGetByIdStepDefinition
    {
        private HttpResponseMessage _response;

        [Given(@"a API está no servidor")]
        public void GivenAApiEstaRodando()
        {
            // Garante que a API está rodando
        }

        [When(@"executar requisição GET para ""(.*)""")]
        public async Task WhenEuFacoUmaRequisicaoGETPara(string endpoint)
        {
            var client = new HttpClient();
            var baseUrl = "http://localhost:8080";
            _response = await client.GetAsync(baseUrl + endpoint);
        }

        [Then(@"a resposta deve ser 404 Not Found")]
        public async Task ThenARespostaDeveSer404NotFound()
        {
            var content = await _response.Content.ReadAsStringAsync();
            Console.WriteLine("Response content:");
            Console.WriteLine(content);

            ((int)_response.StatusCode).Should().Be(404);
        }

    }
}
