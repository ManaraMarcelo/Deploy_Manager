
#  👾 DeployManager

Sistema simples de gerenciamento de aplicações para fins de estudo, desenvolvido com **.NET 8**, **Entity Framework Core** e **SQL Server**.

---

## 📚 Descrição

O **DeployManager** é uma aplicação web minimalista que permite realizar operações CRUD (Create, Read, Update e Delete) sobre uma entidade chamada `Application`. Essa aplicação foi desenvolvida com foco em aprendizado, cobrindo desde a criação da base de dados até a exposição de endpoints via API RESTful utilizando os recursos modernos do ASP.NET Core.

---

## 🛠️ Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/en-us/)
- [ASP.NET Core Minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server)
- [Swagger (Swashbuckle)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

---

## 🧩 Estrutura do Projeto

- `Models/` → Contém as entidades da aplicação (`Application.cs`)
- `Data/` → Contém o contexto do EF Core (`DeployManagerContext.cs`)
- `Controllers/` → Contém os mapeamentos dos endpoints REST (`ApplicationsController.cs`)
- `Program.cs` → Configura o app, banco e middlewares

---

## 🧪 Endpoints Disponíveis

Base URL: `https://localhost:7192/api/Application`

| Verbo  | Rota                 | Descrição                         |
|--------|----------------------|-----------------------------------|
| GET    | `/`                  | Lista todas as aplicações         |
| GET    | `/{id}`              | Retorna uma aplicação por ID      |
| POST   | `/`                  | Cria uma nova aplicação           |
| PUT    | `/{id}`              | Atualiza uma aplicação existente  |
| DELETE | `/{id}`              | Remove uma aplicação              |

---

## 🖥️ Como Executar Localmente

1. **Clone o projeto:**

   ```bash
   git clone https://github.com/seu-usuario/DeployManager.git
   cd DeployManager
   ```

2. **Configure sua string de conexão** em `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=DeployManagerDB;User Id=seuUsuario;Password=suaSenha;"
   }
   ```

3. **Execute os seguintes comandos no Package Manager Console** para gerar o banco de dados:

   ```bash
   Add-Migration InitialCreate
   Update-Database
   ```

4. **Execute a aplicação (F5 ou Ctrl+F5)**  
   O navegador abrirá com a interface do **Swagger**, onde você pode testar todos os endpoints da API.

---

## 🧠 Conhecimentos Praticados

- Migrations e estruturação de banco de dados com EF Core
- Mapeamento de rotas com Minimal APIs
- Operações assíncronas com banco de dados
- Testes via Swagger UI
- Separação de camadas e boas práticas em projetos ASP.NET Core

---

## 💡 Melhorias Futuras

- Autenticação e autorização com Identity
- Interface front-end com React ou Blazor
- Deploy em ambiente de nuvem (ex: Azure ou AWS)
- Logging e monitoramento

---

## 📄 Licença

Distribuído sob a licença MIT. Veja `LICENSE` para mais informações.

---

## 🙋‍♂️ Autor

Feito com 💙 por [Marcelo Manara](https://github.com/ManaraMarcelo)
