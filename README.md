# GerenciadorEnderecos

Aplicação web **ASP.NET Core MVC** para cadastro de usuários e gerenciamento de endereços pessoais, com autenticação por sessão e consulta de CEP via **ViaCEP** no navegador.

## Descrição

O sistema permite registrar usuários, fazer login, manter uma sessão ativa e realizar o CRUD completo de endereços vinculados ao usuário logado. Os formulários de endereço podem preencher logradouro, bairro, cidade e UF automaticamente a partir do CEP, usando `fetch` contra a API pública do ViaCEP.

## Tecnologias

| Camada | Tecnologia |
|--------|------------|
| Framework | ASP.NET Core MVC (.NET 10) |
| ORM | Entity Framework Core 10 |
| Banco de dados | SQL Server |
| Front-end | Bootstrap 5, JavaScript (`fetch`) |
| API externa | [ViaCEP](https://viacep.com.br/) |

## Funcionalidades

- Cadastro de usuários
- Login e logout com sessão (`UsuarioId`, nome exibido)
- CRUD de endereços (listagem, detalhes, criação, edição e exclusão) restrito ao usuário autenticado
- Busca de endereço por CEP nos formulários de criar/editar endereço (JavaScript)
- Alternância mostrar/ocultar senha nos formulários de login e cadastro

## Como executar

### Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)
- SQL Server ou [LocalDB](https://learn.microsoft.com/sql/database-engine/configure-sql/sql-server-express-localdb) (a connection string padrão usa LocalDB)

### Passos

1. Clone o repositório e abra a pasta do projeto:

   ```bash
   cd GerenciadorEnderecos
   ```

2. Ajuste a connection string em `appsettings.json`, se necessário.

3. Aplique o banco (migrations já versionadas no repositório):

   ```bash
   dotnet ef database update
   ```

   Se a ferramenta `dotnet-ef` não estiver instalada:

   ```bash
   dotnet tool install --global dotnet-ef
   ```

4. Execute a aplicação:

   ```bash
   dotnet run
   ```

5. Acesse a URL exibida no terminal (por exemplo `https://localhost:7xxx`).

## Migrations (Entity Framework)

Na pasta do projeto:

| Ação | Comando |
|------|---------|
| Aplicar migrations ao banco | `dotnet ef database update` |
| Criar uma nova migration | `dotnet ef migrations add NomeDaMigration` |
| Remover última migration (não aplicada) | `dotnet ef migrations remove` |

Certifique-se de que o pacote `Microsoft.EntityFrameworkCore.Design` está referenciado (já incluído neste projeto para uso com `dotnet ef`).

## Estrutura do projeto

```
GerenciadorEnderecos/
├── Controllers/       # MVC: Home, Login, Registro, Endereco
├── Data/              # ApplicationDbContext (EF Core)
├── Migrations/        # Histórico de migrations
├── Models/            # Entidades (Usuario, Endereco, etc.)
├── Views/             # Razor (.cshtml) por controller + Shared
├── wwwroot/           # CSS, JS e arquivos estáticos
├── Program.cs         # Configuração (MVC, sessão, EF, pipeline)
└── appsettings.json   # Connection strings e logging
```

## Autor

Gabriel

---

© 2026 GerenciadorEnderecos
