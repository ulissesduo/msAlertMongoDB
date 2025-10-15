# 🌱 msAlertaMongoDB

Microserviço responsável por **gerenciar alertas ambientais** 🌍.  
Faz parte de um sistema maior de **Licenciamento Ambiental** 🏭📜, auxiliando organizações no **monitoramento e acompanhamento de alertas** relacionados a licenças e atividades ambientais.

---

## 🚀 Funcionalidades

- 📡 **Criar Alerta** → Registra novos alertas no MongoDB.  
- 🔎 **Buscar Alertas** → Pesquisa alertas por ID ou lista todos os disponíveis.  
- ✏️ **Atualizar Alerta** → Modifica informações de alertas existentes.  
- ❌ **Excluir Alerta** → Remove alertas que não são mais necessários.

---

## 🛠️ Stack Tecnológica

| Categoria | Tecnologia |
|------------|-------------|
| **Linguagem / Framework** | .NET 8 (ASP.NET WebAPI) |
| **Banco de Dados** | MongoDB |
| **Testes Automatizados** | xUnit |
| **Mapeamento DTO ↔ Entidade** | AutoMapper |
| **Documentação** | Swagger |
| **Containerização** | Docker / Docker Compose |
| **CI/CD** | GitHub Actions |
| **Deploy** | Azure App Service |

---

## 📂 Estrutura do Projeto

msAlertaMongoDB/
├── Controllers/ # Endpoints da API (ex: AlertaController.cs)
├── DTO/ # Objetos de Transferência de Dados (Request/Response)
├── Entity/ # Entidades de domínio (ex: Alerta.cs)
├── Repository/ # Lógica de persistência (MongoDB)
├── Service/ # Camada de regras de negócio
├── msAlertaMongoDBTest/ # Testes automatizados (xUnit)
├── Program.cs # Ponto de entrada da aplicação
├── appsettings.json # Configurações (MongoDB, Logging, etc)
└── Dockerfile




---

## 🔄 DTOs

### `AlertaRequestDto` 📥  
Usado para **criação e atualização** de alertas (sem ID).

### `AlertaResponseDto` 📤  
Usado para **retorno de dados** de alertas (contém o ID gerado).

---

## 📖 Exemplo de Uso da API

### ▶️ Criar Alerta

**POST** `/api/alerta`

```json
{
  "titulo": "Emissão excessiva de CO2 detectada",
  "descricao": "Níveis de CO2 excederam o limite seguro",
  "nivel": "ALTO"
}



{
  "id": "671fe2aabc902fbd203f1d44",
  "titulo": "Emissão excessiva de CO2 detectada",
  "descricao": "Níveis de CO2 excederam o limite seguro",
  "nivel": "ALTO"
}




🐳 Como Executar Localmente com Docker
Pré-requisitos

.NET 8 SDK

Docker Desktop

Git

Passos

1 - Clonar o repositório

git clone https://github.com/seuusuario/msAlertaMongoDB.git
cd msAlertaMongoDB


2 - Subir os containers

docker-compose up --build


3 - Acessar a API

Swagger: http://localhost:5000/swagger

4 - Executar testes

dotnet test





⚙️ Pipeline CI/CD

A aplicação utiliza GitHub Actions para automatizar o ciclo de vida de integração e deploy.

🔧 Etapas do Pipeline

Build Automático

Executa dotnet restore e dotnet build

Garante que o projeto ASP.NET compila corretamente

Testes Automatizados

Executa dotnet test com o xUnit

Falhas interrompem o pipeline imediatamente

Containerização

Cria a imagem Docker da aplicação (docker build)

Usa a tag do commit (${{ github.sha }}) como identificador

Deploy Automatizado

Staging: deploy inicial de homologação no Azure App Service
→ https://msalerta-staging.azurewebsites.net

Produção: deploy final
→ https://msalerta.azurewebsites.net







🐋 Containerização

A aplicação é containerizada com Docker para garantir portabilidade, isolamento e escalabilidade.

Dockerfile
# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy .csproj directly from build context
COPY ["msAlertaMongoDB.csproj", "./"]
RUN dotnet restore "./msAlertaMongoDB.csproj"

# Copy all source code
COPY . . 
WORKDIR "/src"
RUN dotnet build "./msAlertaMongoDB.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./msAlertaMongoDB.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "msAlertaMongoDB.dll"]


docker-compose.yml
services:
  # MongoDB service
  mongodb:
    image: mongo
    container_name: mongodb
    restart: always
    ports:
      - "27017:27017"
    environment:
      MONGO_INITDB_ROOT_USERNAME: admin
      MONGO_INITDB_ROOT_PASSWORD: admin123
    volumes:
      - mongo_data:/data/db

  # ASP.NET API service
  msalertamongodb:
    build:
      context: ./msAlertaMongoDB   
      dockerfile: Dockerfile
    container_name: alertaapi
    ports:
      - "8080:8080"
    environment:
      ASPNETCORE_ENVIRONMENT: Development
      ASPNETCORE_URLS: http://+:8080
      ConnectionStrings__MongoDb: mongodb://admin:admin123@mongodb:27017
      DatabaseName: alerta
    depends_on:
      - mongodb

volumes:
  mongo_data:

