# ProjetoTioLU — atividade de repositórios e testes

## Projetos da solução

- `AcademiaDoZe.Domain`: entidades, Value Objects, regras de domínio, `IAggregateRoot` e contratos de repositório.
- `AcademiaDoZe.Console`: projeto executável para demonstrações simples.
- `AcademiaDoZe.Domain.Tests`: projeto xUnit com mais de 120 testes.

## Executar os testes

No terminal, a partir da pasta que contém `AcademiaDoZe.sln`, execute:

```powershell
dotnet restore
dotnet build
dotnet test --project .\AcademiaDoZe.Domain.Tests\AcademiaDoZe.Domain.Tests.csproj
```

Se o comando `dotnet test --project` não for aceito pela versão instalada, entre na pasta de testes e execute:

```powershell
cd .\AcademiaDoZe.Domain.Tests
dotnet test
```

O Test Explorer do Visual Studio ou do VS Code deve reconhecer o projeto `AcademiaDoZe.Domain.Tests` e exibir os testes executados.

## Entrega

A atividade exige print da solução com os projetos e diretórios expandidos, print do gerenciador de testes com pelo menos 120 testes aprovados e arquivos `.cs` com comentário contendo o nome do aluno na primeira linha. Não envie `bin` e `obj`.
