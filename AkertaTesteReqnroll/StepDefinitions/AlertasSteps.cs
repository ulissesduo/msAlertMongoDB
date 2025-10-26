using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkertaTesteReqnroll.StepDefinitions
{
    [Binding]
    public class AlertasSteps
    {
        private readonly WebApplicationFactory<Program> _factory;
        private HttpResponseMessage _response;

        public AlertasSteps()
        {
            // Cria uma instância da sua API para testes em memória
            _factory = new WebApplicationFactory<Program>();
        }

        [Given(@"a API está rodando")]
        public void GivenAApiEstaRodando()
        {
            // Nada a fazer — só garante que a API está "ligada"
        }

        [When(@"eu faço uma requisição GET para ""(.*)""")]
        public async Task WhenEuFacoUmaRequisicaoGETPara(string endpoint)
        {
            var client = _factory.CreateClient();
            _response = await client.GetAsync(endpoint);
        }

        [Then(@"a resposta deve conter status (.*)")]
        public void ThenARespostaDeveConterStatus(int statusCode)
        {
            ((int)_response.StatusCode).Should().Be(statusCode);
        }
    }
}
