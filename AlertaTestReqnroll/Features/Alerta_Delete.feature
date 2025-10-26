Feature: Deletar alerta por ID
  Testa o comportamento do endpoint DELETE /api/alerta/{id} quando o alerta existe

  Scenario: Deletar alerta pelo ID
    Given a API rodando no docker
    When executar requisição DELETE para "/api/alerta/68fd5454d16bdedfe36c22f4"
    Then a resposta deve ser 204 No Content
