# Loft Code - Processo Seletivo

Projeto desenvolvido para a etapa de coding do processo seletivo da Loft.

## 🏗️ Arquitetura do Projeto

Este projeto foi estruturado seguindo os princípios de **Clean Architecture** e **Clean Code**, organizando o código em camadas bem definidas para garantir manutenibilidade, testabilidade e separação de responsabilidades.

### Estrutura de Projetos

```
src/
├── Loft.Code.Api/              # Camada de Apresentação
│   ├── Controllers/            # Endpoints da API
│   └── Program.cs             # Configuração e inicialização
│
├── Loft.Code.Application/      # Camada de Aplicação
│   └── DependencyInjection.cs # Registro de serviços da aplicação
│
├── Loft.Code.Domain/           # Camada de Domínio
│   ├── Entities/              # Entidades de negócio
│   ├── Interfaces/            # Contratos e abstrações
│   └── Repository/            # Base para repositórios
│
├── Loft.Code.Infra/            # Camada de Infraestrutura
│   ├── Context/               # Fábrica de contextos
│   └── Repository/            # Implementações genéricas
│
└── Loft.Code.Infra.Postgres/   # Implementação específica do PostgreSQL
    └── Context/               # DbContext do PostgreSQL
```

### Camadas e Responsabilidades

#### 🎯 **Loft.Code.Domain** (Núcleo da Aplicação)

- Contém as regras de negócio e entidades
- Define interfaces e contratos (`IRepositoryBase`, `IAppDbContext`)
- Não possui dependências externas
- Implementa padrões como Repository Pattern

#### 💼 **Loft.Code.Application** (Casos de Uso)

- Orquestra a lógica de aplicação
- Implementa serviços e casos de uso
- Depende apenas do Domain

#### 🔌 **Loft.Code.Infra** (Infraestrutura Base)

- Implementações genéricas de infraestrutura
- Factory Pattern para criação de contextos
- Repositórios genéricos reutilizáveis

#### 🐘 **Loft.Code.Infra.Postgres** (Infraestrutura Específica)

- Implementação concreta para PostgreSQL
- Configurações específicas do Entity Framework Core
- Migrations e contexto de dados

#### 🌐 **Loft.Code.Api** (Interface Web)

- Controllers e endpoints REST
- Configuração de middleware
- Injeção de dependências
- Swagger/OpenAPI

### Benefícios da Arquitetura

✅ **Separação de Responsabilidades**: Cada camada tem um propósito bem definido  
✅ **Testabilidade**: Facilita a criação de testes unitários e de integração  
✅ **Manutenibilidade**: Código organizado e fácil de entender  
✅ **Flexibilidade**: Fácil trocar implementações (ex: PostgreSQL por outro banco)  
✅ **Escalabilidade**: Estrutura preparada para crescimento do projeto

## 🐳 Configuração do Ambiente

### Pré-requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

### Subindo o Ambiente com Docker

1. **Inicie o banco de dados PostgreSQL:**

```bash
cd docker
docker-compose up -d
```

2. **Verifique se o container está rodando:**

```bash
docker ps
```

Você deverá ver o container `postgres` ativo na porta `5432`.

3. **Configuração do Banco de Dados:**

O Docker Compose já configura automaticamente:

- **Usuário**: `postgres`
- **Senha**: `postgres`
- **Database**: `loftcode`
- **Porta**: `5432`

### Executando a Aplicação

1. **Restaure as dependências:**

```bash
dotnet restore
```

2. **Execute as migrations** (aplicadas automaticamente na inicialização):

```bash
dotnet run --project src/Loft.Code.Api/Loft.Code.Api.csproj
```

3. **Acesse a documentação Swagger:**

```
https://localhost:<porta>/swagger
```

### Parando o Ambiente

```bash
cd docker
docker-compose down
```

Para remover também os volumes (dados do banco):

```bash
docker-compose down -v
```

## 🛠️ Tecnologias Utilizadas

- **.NET 8.0** - Framework principal
- **Entity Framework Core** - ORM
- **PostgreSQL** - Banco de dados
- **Swagger/OpenAPI** - Documentação da API
- **Docker** - Containerização

## 📝 Funcionalidades

- ✅ Migrations automáticas na inicialização
- ✅ Arquitetura em camadas
- ✅ Repository Pattern
- ✅ Dependency Injection
- ✅ Documentação interativa com Swagger
