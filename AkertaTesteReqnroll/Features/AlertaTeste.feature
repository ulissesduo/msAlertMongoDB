Feature: Alertas API
  Testa o comportamento da API de Alertas

  Scenario: Buscar todos os alertas com sucesso
    Given a API está rodando
    When eu faço uma requisição GET para "/api/alertas"
    Then a resposta deve conter status 200
