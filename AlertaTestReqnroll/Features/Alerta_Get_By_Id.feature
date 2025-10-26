Feature: Buscar alerta por ID inexistente
  Testa o comportamento do endpoint GET /api/alerta/{id} quando o alerta não existe

  Scenario: Buscar alerta inexistente pelo ID
    Given a API está no servidor
    When executar requisição GET para "/api/alerta/672c9f8e5e9d3b4a50d8c111"
    Then a resposta deve ser 404 Not Found
