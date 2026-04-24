# Gerenciador de Endereços

Aplicação web completa desenvolvida em ASP.NET MVC para gerenciamento de endereços com autenticação de usuários, integração com API externa e exportação de dados.

---

## Sumário

- [Sobre o Projeto](#sobre-o-projeto)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Tecnologias NÃO Utilizadas](#tecnologias-não-utilizadas)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Banco de Dados](#banco-de-dados)
- [Funcionalidades](#funcionalidades)
- [Bootstrap](#bootstrap)
- [Como Rodar](#como-rodar)
---

## Sobre o Projeto

O Gerenciador de Endereços é uma aplicação web que permite ao usuário realizar login, cadastrar-se e gerenciar um CRUD completo de endereços. O sistema permite inserir endereços manualmente ou buscar automaticamente os dados através do CEP utilizando a API pública do ViaCEP. Também é possível exportar os endereços salvos para um arquivo CSV.

---

## Tecnologias Utilizadas

### Backend
- **ASP.NET MVC (.NET 8)** — Framework principal da aplicação, responsável pelo padrão MVC (Model, View, Controller)
- **Entity Framework Core** — ORM utilizado para interação com o banco de dados, permitindo criar, ler, atualizar e deletar registros sem escrever SQL manualmente
- **C#** — Linguagem de programação utilizada em todo o backend
- **BCrypt.Net-Next** — Utilizado para criptografar as senhas dos usuários antes de salvar no banco de dados. O BCrypt aplica um hash seguro com salt aleatório, garantindo que mesmo senhas iguais gerem hashes diferentes. Na autenticação, o método `BCrypt.Verify()` compara a senha digitada com o hash salvo no banco.

### Frontend
- **HTML5** — Estrutura das páginas
- **CSS3** — Estilização das páginas com variáveis CSS para suporte a tema claro e escuro
- **JavaScript** — Utilizado para a integração com a API do ViaCEP e para o toggle de tema claro/escuro
- **Bootstrap 5** — Framework CSS utilizado para componentes prontos e responsividade

### Banco de Dados
- **SQL Server (LocalDB)** — Banco de dados relacional utilizado para armazenar usuários e endereços

### Pacotes NuGet
- **Microsoft.EntityFrameworkCore.SqlServer** — Provider do EF Core para SQL Server
- **Microsoft.EntityFrameworkCore.Tools** — Ferramentas para criar e rodar migrations
- **Microsoft.AspNetCore.Authentication.Cookies** — Autenticação baseada em cookies de sessão
- **BCrypt.Net-Next** — Criptografia de senhas com hash seguro

### API Externa
- **ViaCEP** (https://viacep.com.br/) — API pública e gratuita utilizada para buscar dados de endereço a partir do CEP informado pelo usuário

---

## Tecnologias NÃO Utilizadas

Algumas tecnologias foram instaladas ou consideradas mas não foram utilizadas na versão final:

- **CsvHelper** — Pacote instalado para exportação de CSV, porém a exportação foi implementada manualmente usando `StringBuilder` do próprio C#, sem necessidade do pacote.
- **Startup.cs** — Arquivo de configuração tradicional do ASP.NET, substituído pelo padrão moderno do .NET 6+ usando `Program.cs` com `WebApplication.CreateBuilder`.

---

## Segurança

### Criptografia de Senhas com BCrypt
As senhas dos usuários são criptografadas com BCrypt antes de serem salvas no banco de dados:

- **No Registro** — A senha é convertida em hash antes de salvar:
```csharp
Senha = BCrypt.Net.BCrypt.HashPassword(model.Senha)
```

- **No Login** — A senha digitada é comparada com o hash salvo:
```csharp
BCrypt.Net.BCrypt.Verify(model.Senha, usuario.Senha)
```

- **Por que BCrypt?** — Mesmo que o banco de dados seja comprometido, as senhas não podem ser revertidas para texto puro. Cada hash é único graças ao salt aleatório aplicado automaticamente.

### Proteção de Rotas
O atributo `[Authorize]` foi aplicado no `EnderecoController`, impedindo que usuários não autenticados acessem qualquer página de endereços.

### Isolamento de Dados
Cada operação do CRUD verifica se o endereço pertence ao usuário logado através do `UsuarioId`, impedindo que um usuário acesse dados de outro.

---

## Estrutura do Projeto

```
MeuSiteEmMVC/
├── Controllers/
│   ├── ContaController.cs       -> Login, Logout e Registro de usuários
│   └── EnderecoController.cs    -> CRUD de endereços, busca de CEP e exportação CSV
├── Data/
│   └── AppDbContext.cs          -> Contexto do Entity Framework
├── Migrations/                  -> Migrations geradas pelo EF Core
├── Models/
│   ├── Endereco.cs              -> Model da tabela Enderecos
│   └── Usuario.cs               -> Model da tabela Usuarios
├── Scripts/
│   └── SQL/
│       └── CriacaoTabelas.sql   -> Script SQL de criação das tabelas
├── Services/
│   └── ViaCepService.cs         -> Serviço de integração com a API ViaCEP
├── ViewModels/
│   ├── EnderecoViewModel.cs     -> Dados do formulário de endereço com validações
│   ├── LoginViewModel.cs        -> Dados do formulário de login
│   └── RegistroViewModel.cs     -> Dados do formulário de registro com confirmação de senha
├── Views/
│   ├── Conta/
│   │   ├── Login.cshtml         -> Tela de login
│   │   └── Registro.cshtml      -> Tela de registro
│   ├── Endereco/
│   │   ├── Index.cshtml         -> Listagem de endereços com botão de exportar CSV
│   │   ├── Criar.cshtml         -> Formulário de novo endereço com busca por CEP
│   │   ├── Editar.cshtml        -> Formulário de edição com busca por CEP
│   │   └── Deletar.cshtml       -> Confirmação de exclusão
│   └── Shared/
│       └── _Layout.cshtml       -> Layout principal com navbar e footer
├── wwwroot/
│   └── css/
│       └── site.css             -> Estilos personalizados com tema claro/escuro
├── appsettings.json             -> String de conexão com o banco de dados
└── Program.cs                   -> Configuração da aplicação
```

---

## Banco de Dados

O banco de dados foi criado usando **Entity Framework Core Migrations**, que gerou automaticamente as tabelas a partir dos Models C#.

### Tabela `Usuarios`

| Coluna | Tipo          | Descrição                  |
|--------|---------------|----------------------------|
| Id     | INT (PK)      | Identificador único        |
| Nome   | NVARCHAR(100) | Nome completo do usuário   |
| Login  | NVARCHAR(50)  | Nome de usuário para login |
| Senha  | NVARCHAR(255) | Senha criptografada BCrypt  |

### Tabela `Enderecos`

| Coluna      | Tipo          | Descrição                       |
|-------------|---------------|---------------------------------|
| Id          | INT (PK)      | Identificador único             |
| Cep         | NVARCHAR(10)  | CEP do endereço                 |
| Logradouro  | NVARCHAR(200) | Nome da rua/avenida             |
| Complemento | NVARCHAR(100) | Complemento (opcional)          |
| Bairro      | NVARCHAR(100) | Bairro                          |
| Cidade      | NVARCHAR(100) | Cidade                          |
| Uf          | NVARCHAR(2)   | Estado (sigla com 2 caracteres) |
| Numero      | NVARCHAR(20)  | Número do endereço              |
| UsuarioId   | INT (FK)      | Referência ao usuário dono      |

O campo `UsuarioId` é uma **chave estrangeira** que referencia a tabela `Usuarios`, garantindo que cada endereço pertence a um único usuário. A exclusão em cascata foi configurada, ou seja, se um usuário for deletado, seus endereços também serão.

---

## Funcionalidades

### Autenticação
- **Login** — O usuário informa seu login e senha. O sistema busca o usuário no banco pelo login e verifica a senha usando BCrypt. Se válido, cria um cookie de autenticação com nome e ID do usuário. O cookie não é persistente, ou seja, ao fechar o navegador o usuário é deslogado.
- **Registro** — O usuário preenche nome, login, senha e confirmação de senha. O sistema valida se o login já existe e criptografa a senha com BCrypt antes de salvar.
- **Logout** — Remove o cookie de autenticação e redireciona para a tela de login.
- **Proteção de rotas** — O atributo `[Authorize]` foi aplicado no `EnderecoController`, impedindo que usuários não autenticados acessem as páginas de endereço.

### CRUD de Endereços
- **Listar (Index)** — Busca todos os endereços do usuário logado usando o ID armazenado no cookie.
- **Criar** — Formulário com todos os campos do endereço. Ao salvar, o sistema vincula automaticamente o endereço ao usuário logado.
- **Editar** — Carrega os dados do endereço selecionado e permite alterá-los. O sistema verifica se o endereço pertence ao usuário logado antes de editar.
- **Deletar** — Exibe uma tela de confirmação com os dados do endereço antes de excluir. O sistema também verifica se o endereço pertence ao usuário logado.

### Integração com ViaCEP
- O usuário digita o CEP no formulário e sai do campo (evento `blur` do JavaScript).
- O JavaScript chama o endpoint `/Endereco/BuscarCep?cep=XXXXXXXX`.
- O `EnderecoController` repassa a requisição para o `ViaCepService`, que chama a API `https://viacep.com.br/ws/{cep}/json/`.
- Os dados retornados (logradouro, bairro, cidade, UF) são preenchidos automaticamente nos campos do formulário.

### Exportação CSV
- O usuário clica no botão "Exportar CSV" na listagem de endereços.
- O sistema busca todos os endereços do usuário logado.
- Usando `StringBuilder`, monta o conteúdo do arquivo CSV linha por linha.
- Retorna o arquivo para download com o nome `enderecos.csv`.

### Tema Claro/Escuro
- Implementado com variáveis CSS e o atributo `data-theme` no elemento `<html>`.
- O JavaScript alterna entre `data-theme="dark"` e `data-theme="light"` ao clicar no botão.
- A preferência é salva no `localStorage` do navegador para persistir entre sessões.

---

## Bootstrap

O projeto utilizou o **Bootstrap 5** carregado via CDN. Os principais componentes utilizados foram:

### Tabela (`table`)
Utilizada na tela de listagem de endereços:
- `table` — Classe base da tabela
- `table-striped` — Linhas alternadas com cores diferentes para melhor leitura

### Botões (`btn`)
- `btn btn-primary` — Botão principal (salvar, entrar)
- `btn btn-success` — Botão de exportar CSV
- `btn btn-warning` — Botão de editar
- `btn btn-danger` — Botão de deletar
- `btn btn-secondary` — Botão de cancelar
- `btn btn-outline-secondary` — Botão de "Já tenho conta"
- `btn btn-outline-light` — Botão de sair da navbar
- `w-100` — Faz o botão ocupar 100% da largura

### Formulários
- `form-control` — Inputs estilizados
- `form-label` — Labels dos campos
- `mb-3` — Margem inferior entre os campos

### Layout e Grid
- `container` — Centraliza o conteúdo com largura máxima
- `d-flex` — Display flex
- `justify-content-between` — Espaço entre os elementos
- `align-items-center` — Alinhamento vertical centralizado
- `ms-auto` — Margem esquerda automática (empurra para a direita)
- `gap-3` — Espaço entre elementos flex
- `mt-4`, `mt-5`, `mt-2` — Margens superiores
- `py-3` — Padding vertical

### Navbar
- `navbar` — Barra de navegação
- `navbar-expand-lg` — Expande em telas grandes
- `navbar-brand` — Logo/nome do sistema

### Utilitários
- `text-danger` — Texto vermelho para erros de validação
- `text-muted` — Texto cinza para informações secundárias
- `text-center` — Centraliza o texto
- `border-top` — Borda superior (usado no footer)

---

## Como Rodar

### Pré-requisitos
- Visual Studio 2022 ou superior
- .NET 8 SDK
- SQL Server ou SQL Server LocalDB

### Passos

1. Clone o repositório:
```bash
git clone https://github.com/Kay0ox/GerenciadordeEnderecos.git
```

2. Abra o projeto no Visual Studio

3. Configure a string de conexão no `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=GerenciadorEnderecos;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

4. No Package Manager Console, rode as migrations:
```
Update-Database
```

5. Rode o projeto com `F5`

6. Crie uma conta pela tela de registro do sistema

---
