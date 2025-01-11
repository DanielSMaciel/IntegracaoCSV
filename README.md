# Projeto IntegracaoCSV

Este projeto realiza a integração de arquivos CSV, processando seus dados e permitindo a interação via API.

## Estrutura do Projeto

- IntegracaoCSV: Projeto principal contendo os controladores e lógica de negócio.
- IntegracaoCSV.Core: Contém os modelos e regras de negócio.
- IntegracaoCSV.Infra: Contém a infraestrutura de dados, como repositórios.
- IntegracaoCSV.Test: Projeto de testes, incluindo testes de integração para validar os principais casos de uso.

## Pré-requisitos

- .NET SDK 8.0 ou superior
- Banco de dados SQLite (configurado automaticamente)

## Como Rodar o Projeto

1. Clone o Repositório:

   git clone https://github.com/DanielSMaciel/IntegracaoCSV.git
   cd IntegracaoCSV

2. Restaure as Dependências:

   dotnet restore

3. Compile o Projeto:

   dotnet build

4. Execute o Projeto:
   
   dotnet run --project IntegracaoCSV

   Por padrão, o projeto estará disponível em `http://localhost:5000`.

## Testes de Integração

Os testes de integração validam a interação completa entre os componentes do sistema.

1. Navegue até o Diretório de Testes:

   cd IntegracaoCSV.Test


2. Execute os Testes:

   dotnet test

## Endpoints Disponíveis

### `POST /IntegracaoPorArquivo`
- Descrição: Processa um arquivo CSV enviado.
- Exemplo de Requisição:

  curl -X POST -F "Arquivo=@caminho/do/arquivo.csv" http://localhost:5000/IntegracaoPorArquivo


### `GET /IndicadosPiorFilme`
- Descrição: Retorna o produtor com maior intervalo entre dois prêmios consecutivos(producer_longest_interval), e o que obteve dois prêmios mais rápido(producer_two_awards_fastest).
- Exemplo de Requisição:

  curl -X GET http://localhost:5000/IndicadosPiorFilme
