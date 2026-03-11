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

A API utiliza **JWT (JSON Web Token)** para autenticação. Configure as chaves no `appsettings.json`:

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

### Clientes — `/api/cliente`

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/api/cliente` | Lista todos os clientes |
| `GET` | `/api/cliente/{id}` | Busca cliente por ID |
| `GET` | `/api/cliente/cpf/{cpf}` | Busca cliente por CPF |
| `POST` | `/api/cliente` | Cria um novo cliente |
| `PUT` | `/api/cliente/{id}` | Atualiza um cliente |
| `DELETE` | `/api/cliente/{id}` | Remove um cliente |

### Produtos — `/api/produto`

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/api/produto` | Lista todos os produtos |
| `GET` | `/api/produto/{id}` | Busca produto por ID |
| `POST` | `/api/produto` | Cria um novo produto |
| `PUT` | `/api/produto/{id}` | Atualiza um produto |
| `DELETE` | `/api/produto/{id}` | Remove um produto |

### Vendas — `/api/venda`

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/api/venda` | Lista todas as vendas |
| `GET` | `/api/venda/{id}` | Busca venda por ID |
| `GET` | `/api/venda/cpf/{cpf}` | Busca vendas por CPF do cliente |
| `POST` | `/api/venda` | Inicia uma nova venda |
| `PUT` | `/api/venda/{id}` | Atualiza uma venda |

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

## 🧠 Regras de Negócio

- **CPF** é validado antes do cadastro do cliente
- **E-mail** é validado por formato no cadastro e atualização
- **CPF duplicado** não é permitido no sistema
- **Estoque** é verificado antes de confirmar uma venda
- Não é possível iniciar uma venda sem produtos ou sem o CPF do cliente

---

## 👤 Autor

Desenvolvido por **[GapNeves](https://github.com/GapNeves)**
