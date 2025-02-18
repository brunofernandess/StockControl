# Stock Control API 📦 

API para gerenciamento de estoque de produtos dermocosméticos, inicialmente focado em perfumes para um projeto pessoal de um familiar. Permite a criação, consulta, filtragem e exclusão de produtos. Desenvolvida com .NET 8 e Entity Framework Core.

## **Índice**
1. [Descrição](#descrição)
2. [Tecnologias Utilizadas](#tecnologias-utilizadas)
3. [Como Executar o Projeto](#como-executar-o-projeto)
4. [Endpoints da API](#endpoints-da-api)
   - [Get All Products](#1-get-all-products)
   - [Filter Products](#2-filter-products)
   - [Add Product](#3-add-product)
   - [Delete Product](#4-delete-product)
5. [Estrutura do Banco de Dados](#estrutura-do-banco-de-dados)
6. [Testes](#testes)


-----------------------------------------------------------------
## **Descrição**

Essa API foi criada para gerenciar estoques de produtos dermocosméticos. Os principais recursos incluem:
- Listar todos os produtos.
- Filtrar produtos por marca, código e data de validade.
- Adicionar novos produtos ao estoque.
- Excluir produtos do estoque.

-----------------------------------------------------------------

## **Tecnologias Utilizadas**

- **.NET 8** (Framework Backend)
- **Entity Framework Core** (ORM para o banco de dados)
- **SQL Server** (Banco de Dados Relacional)
- **Postman** (Testes e Documentação da API)

-----------------------------------------------------------------

## **Como Executar o Projeto**

### **1. Pré-requisitos**
- Instale o **.NET SDK** 8 ou superior.
- Configure o **SQL Server** para o banco de dados.
- Instale uma ferramenta para API, como o **Postman**.

 ### **2. Configurar o Banco de Dados**
1. Abra o arquivo `appsettings.json` e configure a string de conexão:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=SEU_SERVIDOR;Database=StockControlDb;Trusted_Connection=True;"
   }
   
### 3. Execute o comando para aplicar as migrações e criar o banco:
 ```
 dotnet ef database update
  ```

### 4. Executando a aplicação

1. No terminal, vá para o diretório do projeto:
```
cd StockControl
 ```
 
2. Execute o comando:
```
dotnet run
 ```
 
3. A porta de execução será exibida no terminal, algo como:

```
Now listening on: http://localhost:5000

 ```
Acesse a API no navegador ou no Postman utilizando o endereço exibido. Exemplo:
```
http://localhost:{PORTA}/api/Produtos
 ```
 **substitua PORTA pelo valor informado no terminal**

-----------------------------------------------------------------

## Endpoints da API
-----------------------------------------------------------------
## 1. Get All Products

### Método: GET

##### URL: /api/Produtos

##### Descrição: Retorna todos os produtos cadastrados.

Exemplo de Resposta:

```
json
[
  {
    "id": 1,
    "nome": "Natura Homem Essence",
    "marca": "Natura",
    "codigoProduto": "1234",
    "dataValidade": "2025-12-31T00:00:00"
  },
  {
    "id": 2,
    "nome": "The Blend Cardamom",
    "marca": "Boticário",
    "codigoProduto": "5678",
    "dataValidade": "2024-06-01T00:00:00"
  }
  ]
 ```
 -----------------------------------------------------------------
 
## 2. Filter Products

## Método: GET

#### URL: /api/Produtos/filter

##### Query Parameters:


`marca` (string): Marca do produto.

`codigoProduto` (string): Código do produto.

`dataValidade` (DateTime): Data de validade do produto.

##### Descrição: Filtra produtos com base nos parâmetros fornecidos.

Exemplo de Resposta:

```
json
[
  {
    "id": 1,
    "nome": "Produto 1",
    "marca": "Natura",
    "codigoProduto": "1234",
    "dataValidade": "2025-12-31T00:00:00"
  }
]
 ```
 -----------------------------------------------------------------
 
 ## 3. Add Product
 
 ## Método: POST
#### URL: /api/Produtos

##### Corpo da Requisição:

```
json
{
  "nome": "Essencial Oud Masculino",
  "marca": "Natura",
  "codigoProduto": "9999",
  "dataValidade": "2026-01-01T00:00:00"
}
 ```
 
#####  Descrição: Adiciona um novo produto ao estoque.
Exemplo de Resposta:

```
json
{
  "id": 3,
  "nome": "Produto 3",
  "marca": "Avon",
  "codigoProduto": "9999",
  "dataValidade": "2026-01-01T00:00:00"
}
 ```
 
  -----------------------------------------------------------------
 
 ## 4. Delete Product
## Método: DELETE
#### URL: /api/Produtos/{id}

###### 
Parâmetros:
`id (int)`: O identificador único do produto que será excluído.
##### Descrição: Exclui um produto com base no ID fornecido.

Exemplo de requisição:
```
DELETE /api/Produtos/1

 ```
Exemplo de Resposta:
```
Status 204 (No Content): Produto removido com sucesso.
 ```
 
 

## Estrutura do Banco de Dados

### Tabela: Produtos

| Coluna         | Tipo      | Descrição                |
|-----------------|-----------|--------------------------|
| `Id`           | int       | Identificador único      |
| `Nome`         | string    | Nome do produto          |
| `Marca`        | string    | Marca do produto         |
| `CodigoProduto`| string    | Código único do produto  |
| `DataValidade` | DateTime  | Data de validade         |

  -----------------------------------------------------------------
  
## Testes
#### Testes Unitários

O projeto inclui testes unitários para os controladores da API.

Execute os testes com o seguinte comando:
```
dotnet test
  ```
