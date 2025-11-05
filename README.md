# Sistema de Cadastro de Clientes

Console app em C# (.NET 8) para CRUD de clientes com persistência em JSON.

## Como rodar
1. dotnet build
2. dotnet run --project ClienteApp

Os dados ficam em 'ClienteApp/Data/customers.json'.

## Estrutura
- Models: Customer
- Repositories: JsonCustomerRepository
- Services: CustomerService

## Testes
dotnet test

## Licença
MIT
