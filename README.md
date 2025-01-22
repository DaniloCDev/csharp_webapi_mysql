# Person API

A **Person API** é uma aplicação Web API desenvolvida para gerenciar informações de pessoas. Ela permite realizar operações CRUD (Criar, Ler, Atualizar e Deletar) sobre os dados de pessoas, utilizando um banco de dados MySQL como armazenamento, deixo bem aqui que todos os dados inseridos desde da criação ate a postagem deste projeto é apenas para aprender sobre .Net Core e tecnologias 
OBS: Foi colocado e testado o entity framework, eu estava estudando como ficaria e gostei bastante!


## Informações do Projeto

- **Nome do Projeto**: Person API
- **Versão**: 1.0
- **Tecnologia**: C# (ASP.NET Core Web API)
- **Banco de Dados**: MySQL
- **Documentação**: A API está documentada de acordo com a especificação OpenAPI 3.0
- **URL da Documentação**: [Swagger UI](http://localhost:5099/swagger)

## Funcionalidades

A API disponibiliza as seguintes funcionalidades para manipulação dos dados de pessoas:

1. **POST /Person** - Cria uma nova pessoa no banco de dados.
2. **GET /Person** - Retorna a lista de todas as pessoas cadastradas.
3. **GET /Person/{codigo}** - Retorna os detalhes de uma pessoa específica, buscando pelo código.
4. **PUT /Person/{codigo}** - Atualiza os dados de uma pessoa específica, identificada pelo código.
5. **DELETE /Person/{codigo}** - Deleta uma pessoa específica, identificada pelo código.

## Estrutura do Objeto Person

A entidade **Person** possui a seguinte estrutura:

```json
{
  "codigo": 0,
  "nome": "string",
  "cidade": "string",
  "idade": 0
}
```

- `codigo` (int): Identificador único da pessoa.
- `nome` (string): Nome completo da pessoa.
- `cidade` (string): Cidade onde a pessoa reside.
- `idade` (int): Idade da pessoa.

## Requisitos

Para rodar a API localmente, é necessário ter o seguinte:

- **.NET Core 6.0 ou superior**
- **MySQL**: A API está configurada para usar o MySQL como banco de dados. Você precisa ter uma instância do MySQL rodando e configurar a string de conexão no arquivo `appsettings.json`.

## Como Rodar a API Localmente

1. **Clone o repositório**:

   ```bash
   git clone https://github.com/DaniloCDev/csharp_webapi_mysql.git
   cd csharp_webapi_mysql
   ```

2. **Instale as dependências**:

   Execute o comando abaixo para restaurar os pacotes NuGet necessários:

   ```bash
   dotnet restore
   ```

3. **Configure o Banco de Dados**:

   No arquivo `appsettings.json`, configure a string de conexão do MySQL conforme a sua instalação.

   Exemplo de configuração:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=persondb;User=root;Password=sua-senha;"
   }
   ```

4. **Crie o banco de dados**:

   Se ainda não tiver o banco de dados configurado, você pode executar o script para criá-lo manualmente ou deixar que a API crie a estrutura automaticamente com base nas migrações.

5. **Execute a API**:

   Para rodar a API localmente, execute o comando abaixo:

   ```bash
   dotnet run
   ```

6. **Acesse a documentação**:

   Após rodar a API, você pode acessar a documentação interativa da API no seguinte endereço:

   [http://localhost:5099/swagger](http://localhost:5099/swagger)

## Não foi utilizado Docker

Vale destacar que, neste projeto, **não foi utilizado Docker**. A API foi desenvolvida para ser executada localmente ou em servidores que tenham o ambiente .NET Core e MySQL configurados.

## Conclusão

A **Person API** é uma aplicação simples, mas poderosa, para gerenciar informações sobre pessoas, utilizando a tecnologia C# e o banco de dados MySQL. Com uma documentação clara e completa via Swagger, a API oferece uma maneira fácil de realizar operações CRUD.
