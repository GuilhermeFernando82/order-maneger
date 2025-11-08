# 🚀 Desafio Técnico - Desenvolvedor .NET Pleno

Bem-vindo ao desafio técnico para a posição de Desenvolvedor .NET Pleno! Este é um teste prático que avalia suas habilidades em desenvolvimento de APIs REST com .NET.

## 📋 Sobre o Desafio

Você irá desenvolver uma **API REST para gestão de pedidos** de uma loja online. O sistema deve permitir criar pedidos, calcular totais automaticamente e aplicar descontos baseados em regras de negócio específicas.

**⏱️ Tempo estimado:** 60-90 minutos

## 🎯 Objetivos

Este desafio avalia suas competências em:

- ✅ Desenvolvimento de APIs REST com .NET
- ✅ Aplicação de princípios SOLID e Clean Code
- ✅ Implementação de regras de negócio
- ✅ Arquitetura em camadas
- ✅ Validação e tratamento de erros
- ✅ Boas práticas de desenvolvimento

## 📦 Requisitos Técnicos

### Obrigatório

- .NET 8
- ASP.NET Core Web API
- Injeção de Dependência
- Padrão Repository ou Service Layer
- Tratamento adequado de exceções
- Validação de dados de entrada

### Diferencial (Opcional)

- Entity Framework Core (pode usar InMemory Database), Se houver tempo sera um grande diferencial!
- Testes unitários (xUnit, NUnit ou MSTest)
- FluentValidation
- AutoMapper
- Swagger/OpenAPI
- Docker
- Padrões avançados (CQRS, MediatR)

## 🏗️ Estrutura do Sistema

### Entidades

**Produto**
```csharp
- Id: int
- Nome: string
- Preço: decimal
- Categoria: enum (Eletrônicos, Roupas, Alimentos, Livros)
```

**ItemPedido**
```csharp
- ProdutoId: int
- Quantidade: int
- PrecoUnitario: decimal
```

**Pedido**
```csharp
- Id: int
- DataCriacao: DateTime
- Itens: List<ItemPedido>
- ValorTotal: decimal
- ValorDesconto: decimal
- Status: enum (Pendente, Aprovado, Cancelado)
```

### Endpoints Necessários

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `POST` | `/api/pedidos` | Criar novo pedido |
| `GET` | `/api/pedidos/{id}` | Buscar pedido por ID |
| `GET` | `/api/pedidos` | Listar todos os pedidos |
| `PUT` | `/api/pedidos/{id}/status` | Atualizar status |

## 💰 Regras de Negócio - Descontos

Implemente as seguintes regras de desconto (podem ser **cumulativas**):

1. **Desconto por Quantidade**
   - 10% de desconto se o pedido tiver 5 ou mais itens no total

2. **Desconto por Valor**
   - 15% de desconto se o valor total (antes dos descontos) ultrapassar R$ 500,00

3. **Desconto por Categoria**
   - 5% adicional se houver produtos da categoria "Livros" no pedido

### Exemplo de Cálculo

```
Pedido com:
- 6 itens (Desconto: 10%)
- Valor total: R$ 600,00 (Desconto: 15%)
- Contém livros (Desconto: 5%)

Descontos cumulativos: 10% + 15% + 5% = 30%
Valor final: R$ 600,00 - R$ 180,00 = R$ 420,00
```

## ✅ Validações Obrigatórias

- Quantidade de itens deve ser maior que zero
- Preço unitário deve ser maior que zero
- Pedido deve conter pelo menos 1 item
- Não permitir alterar status de pedidos já "Aprovados" ou "Cancelados"
- Validar dados de entrada dos endpoints

## 📁 Estrutura de Pastas Sugerida

```
PedidosAPI/
│
├── Controllers/
│   └── PedidosController.cs
│
├── Models/
│   ├── Pedido.cs
│   ├── ItemPedido.cs
│   ├── Produto.cs
│   └── Enums/
│       ├── StatusPedido.cs
│       └── CategoriaProduto.cs
│
├── Services/
│   ├── Interfaces/
│   │   └── IPedidoService.cs
│   └── PedidoService.cs
│
├── Repositories/
│   ├── Interfaces/
│   │   └── IPedidoRepository.cs
│   └── PedidoRepository.cs
│
├── DTOs/
│   ├── Requests/
│   │   ├── CriarPedidoRequest.cs
│   │   └── AtualizarStatusRequest.cs
│   └── Responses/
│       └── PedidoResponse.cs
│
├── Validators/
│   └── CriarPedidoValidator.cs
│
└── Exceptions/
    └── CustomExceptions.cs
```

## 📝 Exemplos de Requisições

### Criar Pedido

**Request:**
```json
POST /api/pedidos
Content-Type: application/json

{
  "itens": [
    {
      "produtoId": 1,
      "nomeProduto": "Notebook Dell",
      "quantidade": 1,
      "precoUnitario": 3500.00,
      "categoria": "Eletrônicos"
    },
    {
      "produtoId": 2,
      "nomeProduto": "Mouse Logitech",
      "quantidade": 2,
      "precoUnitario": 150.00,
      "categoria": "Eletrônicos"
    }
  ]
}
```

**Response (201 Created):**
```json
{
  "id": 1,
  "dataCriacao": "2025-10-03T14:30:00",
  "itens": [
    {
      "produtoId": 1,
      "nomeProduto": "Notebook Dell",
      "quantidade": 1,
      "precoUnitario": 3500.00,
      "categoria": "Eletrônicos"
    },
    {
      "produtoId": 2,
      "nomeProduto": "Mouse Logitech",
      "quantidade": 2,
      "precoUnitario": 150.00,
      "categoria": "Eletrônicos"
    }
  ],
  "valorSubtotal": 3800.00,
  "valorDesconto": 0.00,
  "valorTotal": 3800.00,
  "status": "Pendente",
  "descontosAplicados": []
}
```

### Buscar Pedido por ID

**Request:**
```
GET /api/pedidos/1
```

**Response (200 OK):**
```json
{
  "id": 1,
  "dataCriacao": "2025-10-03T14:30:00",
  "itens": [...],
  "valorSubtotal": 3800.00,
  "valorDesconto": 0.00,
  "valorTotal": 3800.00,
  "status": "Pendente",
  "descontosAplicados": []
}
```

### Atualizar Status

**Request:**
```json
PUT /api/pedidos/1/status
Content-Type: application/json

{
  "novoStatus": "Aprovado"
}
```

**Response (200 OK):**
```json
{
  "id": 1,
  "status": "Aprovado",
  "dataAtualizacao": "2025-10-03T15:00:00"
}
```

## 🚀 Como Começar

### 1. Clone o repositório
```bash
git clone https://github.com/Miguel084/Teste-Tecnico-Blux.git
```

### 2. Crie sua branch
```bash
git checkout -b feature/seu-nome
```

### 3. Desenvolva a solução
```bash
dotnet new webapi -n PedidosAPI
cd PedidosAPI
dotnet run
```

### 4. Teste sua API
Acesse: `https://localhost:7XXX/swagger` (se configurou Swagger)

### 5. Envie sua solução
```bash
git add .
git commit -m "Implementação do desafio técnico"
git push origin feature/seu-nome
```

Abra um Pull Request com sua solução.

## 📊 Critérios de Avaliação

| Critério | Peso | Descrição |
|----------|------|-----------|
| **Funcionamento** | 30% | A API funciona conforme especificado |
| **Qualidade do Código** | 25% | Clean Code, SOLID, legibilidade |
| **Arquitetura** | 20% | Organização em camadas, separação de responsabilidades |
| **Tratamento de Erros** | 10% | Validações e exceções bem tratadas |
| **Testes** | 10% | Cobertura e qualidade dos testes |
| **Boas Práticas** | 5% | Convenções .NET, documentação |

## 💡 Dicas

- ✨ **Priorize o funcionamento** sobre a perfeição
- 📖 Pode consultar documentação e Google durante o desenvolvimento
- 💬 Comente partes complexas do código
- 🧪 Se tiver tempo, adicione testes para as regras críticas
- 📝 Faça commits incrementais
- ❓ Não hesite em fazer perguntas sobre os requisitos

## 🎤 Pontos de Discussão

Após a implementação, esteja preparado para discutir:

1. Como você organizou a arquitetura da aplicação?
2. Como implementou o cálculo de descontos? A solução é extensível?
3. Quais patterns você utilizou e por quê?
4. Como garantiria a performance com milhares de pedidos simultâneos?
5. Como lidaria com transações e concorrência?
6. Quais melhorias implementaria com mais tempo?
7. Como escalaria essa solução para produção?
"# order-maneger" 
