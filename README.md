# 🛒 Store API

API RESTful para gerenciamento de uma loja, desenvolvida com **.NET 9** e arquitetura em camadas. O sistema permite o cadastro e gerenciamento de **clientes**, **produtos** e **vendas**, com autenticação via **JWT**.

---

## 🏗️ Arquitetura

O projeto segue o padrão de **Arquitetura em Camadas**, organizado da seguinte forma:

```
Store/
├── src/
│   ├── 1-Host/                         # Camada de apresentação (API)
│   │   └── Store.Host/
│   │       └── Controllers/            # Endpoints REST
│   ├── 2-AppService/                   # Camada de aplicação
│   │   ├── Store.AppService/           # Implementações dos serviços de aplicação
│   │   └── Store.AppService.Interfaces/# Contratos dos serviços de aplicação
│   ├── 3-Domain/                       # Camada de domínio
│   │   ├── Store.Domain/               # Regras de negócio
│   │   ├── Store.Domain.Interfaces/    # Contratos do domínio
│   │   └── Store.Domain.Models/        # Entidades e modelos
│   └── 4-Infra/                        # Camada de infraestrutura
│       ├── Store.Infra.Data.NoSql/     # Repositórios com LiteDB
│       └── Store.Infra.Interfaces/     # Contratos dos repositórios
```

### Fluxo de dependências

```
Controller → AppService → DomainService → Repository → LiteDB
```

---

## 🚀 Tecnologias Utilizadas

| Tecnologia | Descrição |
|---|---|
| [.NET 9](https://dotnet.microsoft.com/) | Plataforma de desenvolvimento |
| [ASP.NET Core](https://learn.microsoft.com/aspnet/core) | Framework Web |
| [LiteDB](https://www.litedb.org/) | Banco de dados NoSQL embarcado |
| [JWT Bearer](https://jwt.io/) | Autenticação e autorização |
| C# 13 | Linguagem de programação |

---

## 📦 Funcionalidades

### 👤 Clientes
- Cadastrar novo cliente (com validação de CPF e e-mail)
- Buscar cliente por ID ou CPF
- Listar todos os clientes
- Atualizar dados do cliente
- Remover cliente

### 🛍️ Produtos
- Cadastrar novo produto
- Buscar produto por ID
- Listar todos os produtos
- Atualizar produto
- Remover produto

### 🧾 Vendas
- Iniciar uma venda vinculada a um cliente
- Validação de estoque no momento da venda
- Buscar venda por ID ou CPF do cliente
- Listar todas as vendas
- Atualizar venda

---

## 🔐 Autenticação

A API utiliza **JWT (JSON Web Token)** com expiração de **15 minutos** e **sem tolerância de clock skew**. As senhas são armazenadas com hash **BCrypt**.

### Perfis de Acesso

| Perfil | Valor | Descrição |
|---|---|---|
| `Cliente` | `1` | Acesso padrão para clientes da loja |
| `Administrador` | `2` | Acesso total ao sistema |

### Políticas de Autorização

| Política | Perfis permitidos |
|---|---|
| `AdminOnly` | Administrador |
| `ClienteOnly` | Cliente |
| `Everyone` | Administrador e Cliente |

> Novos clientes cadastrados sem um perfil definido recebem automaticamente o perfil `Cliente`.

Configure as chaves no `appsettings.json`:

```json
{
  "Jwt": {
    "Key": "sua-chave-secreta",
    "Issuer": "seu-issuer",
    "Audience": "sua-audience"
  },
  "LiteDb": {
    "DatabasePath": "Data/Store.db"
  }
}
```

---

## 📡 Endpoints

---

### 👤 Clientes — `/api/cliente`

| Método | Rota | Política | Descrição |
|---|---|---|---|
| `GET` | `/api/cliente` | `AdminOnly` | Lista todos os clientes |
| `GET` | `/api/cliente/{id}` | `AdminOnly` | Busca cliente por ID |
| `GET` | `/api/cliente/cpf/{cpf}` | `Everyone` | Busca cliente por CPF |
| `POST` | `/api/cliente` | `Everyone` | Cria um novo cliente |
| `PUT` | `/api/cliente/{id}` | `Everyone` | Atualiza um cliente |
| `DELETE` | `/api/cliente/{id}` | `AdminOnly` | Remove um cliente |

---

### 🛍️ Produtos — `/api/produto`

| Método | Rota | Política | Descrição |
|---|---|---|---|
| `GET` | `/api/produto` | `Everyone` | Lista todos os produtos |
| `GET` | `/api/produto/{id}` | `Everyone` | Busca produto por ID |
| `POST` | `/api/produto` | `AdminOnly` | Cria um novo produto |
| `PUT` | `/api/produto/{id}` | `AdminOnly` | Atualiza um produto |
| `DELETE` | `/api/produto/{id}` | `AdminOnly` | Remove um produto |

---

### 🧾 Vendas — `/api/venda`

| Método | Rota | Política | Descrição |
|---|---|---|---|
| `GET` | `/api/venda` | `AdminOnly` | Lista todas as vendas |
| `GET` | `/api/venda/{id}` | `Everyone` | Busca venda por ID |
| `GET` | `/api/venda/cliente/{cpf}` | `Everyone` | Busca vendas por CPF do cliente |
| `POST` | `/api/venda` | `Everyone` | Inicia uma nova venda |
| `PUT` | `/api/venda/{id}` | `Everyone` | Atualiza uma venda |

---

## 📋 Modelos

### Cliente
```json
{
  "id": "guid",
  "cpf": "string",
  "nome": "string",
  "email": "string",
  "senha": "string"
}
```

### Produto
```json
{
  "id": "guid",
  "nome": "string",
  "descricao": "string",
  "preco": 0.00,
  "qtdEstoque": 0
}
```

### Venda
```json
{
  "id": "guid",
  "cpfCliente": "string",
  "nomeCliente": "string",
  "produtos": [
    {
      "idProduto": "guid",
      "nomeProduto": "string",
      "qtdProduto": 0
    }
  ]
}
```

---

## ⚙️ Como Executar

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

### Passos

1. **Clone o repositório**
   ```bash
   git clone https://github.com/GapNeves/Store.git
   cd Store
   ```

2. **Configure o `appsettings.json`** com suas chaves JWT

3. **Execute a aplicação**
   ```bash
   cd src/1-Host/Store.Host
   dotnet run
   ```

4. **Acesse a documentação OpenAPI** em:
   ```
   http://localhost:{porta}/openapi
   ```

---

## 🗄️ Banco de Dados

O projeto utiliza o **LiteDB**, um banco de dados NoSQL embarcado que não requer instalação de servidor externo. O arquivo `.db` é gerado automaticamente no caminho configurado em `LiteDb:DatabasePath` (padrão: `Data/Store.db`).

---

## 🛡️ Tratamento de Erros

A API conta com um **middleware global de exceções** (`ExceptionMiddleware`) que mapeia automaticamente os erros para os códigos HTTP corretos:

| Exceção | HTTP Status |
|---|---|
| `ArgumentException` | `400 Bad Request` |
| `KeyNotFoundException` | `404 Not Found` |
| `UnauthorizedAccessException` | `401 Unauthorized` |
| `InvalidOperationException` | `409 Conflict` |
| Demais exceções | `500 Internal Server Error` |

---

## 🧠 Regras de Negócio

- **CPF** é validado antes do cadastro do cliente
- **E-mail** é validado por formato no cadastro e atualização
- **CPF duplicado** não é permitido no sistema
- **Estoque** é verificado antes de confirmar uma venda
- Não é possível iniciar uma venda sem produtos ou sem o CPF do cliente

---

## 👤 Autor

Desenvolvido por **[GapNeves](https://github.com/GapNeves)**
