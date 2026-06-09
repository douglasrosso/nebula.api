# Nebula API — Backend

API REST da plataforma Nebula, uma loja de jogos digitais com autenticação JWT, chat em tempo real via SignalR e integração com PostgreSQL.

> Frontend: [nebula.client](../nebula.client/README.md)

---

## Stack

| | Tecnologia |
|---|---|
| Runtime | .NET 10 / ASP.NET Core |
| ORM | Entity Framework Core + Npgsql |
| Banco | PostgreSQL |
| Auth | JWT via HttpOnly Cookie |
| Tempo real | SignalR |
| Mapeamento | AutoMapper |
| Documentação | Swagger / OpenAPI |

---

## Pré-requisitos

- .NET 10 SDK
- PostgreSQL 12+

---

## Configuração

Crie um arquivo `.env` na raiz do projeto:

```env
JWT_KEY=sua-chave-secreta-minimo-32-caracteres!!
JWT_ISSUER=nebula.api
JWT_AUDIENCE=nebula.client
CONNECTION_STRING=Host=localhost;Port=5432;Database=nebula;Username=postgres;Password=sua-senha
ALLOWED_ORIGIN=http://localhost:3000
```

Aplique as migrations e suba o servidor:

```bash
dotnet ef database update
dotnet run
```

- API: `http://localhost:5000`
- Swagger: `http://localhost:5000/swagger`

---

## Credenciais de teste

```
Email:    admin@nebula.com
Senha:    Admin@123
```

---

## Entidades

### UserEntity
Usuário da plataforma. Possui nome, email, username, avatar, nível, XP, país e bio. Ponto central do sistema — relaciona-se com todas as demais entidades.

### GameEntity
Jogo do catálogo. Possui título, descrição, preço, desconto, imagens, developer, publisher, tags, features, requisitos de sistema (armazenados como JSON) e estatísticas de avaliação.

### GenreEntity / GameGenreEntity
`GenreEntity` representa um gênero (ex: RPG, Ação). `GameGenreEntity` é a tabela intermediária do N:N entre `Game` e `Genre`.

### ReviewEntity
Avaliação de um jogo por um usuário. Registra se é positiva ou negativa, horas jogadas, conteúdo e votos de "helpful" e "funny".

### OrderEntity / OrderItemEntity
`OrderEntity` representa uma compra finalizada. `OrderItemEntity` é cada jogo dentro do pedido com o preço pago na época.

### CartItemEntity
Item no carrinho. Chave composta (UserId, GameId).

### WishlistItemEntity
Item na lista de desejos. Chave composta (UserId, GameId).

### UserLibraryEntity
Jogo adquirido (biblioteca do usuário). Chave composta (UserId, GameId).

### FriendshipEntity
Relação de amizade entre dois usuários com status `pending` ou `accepted`. Referencia `User` duas vezes: `Requester` e `Receiver`.

### MessageEntity
Mensagem privada entre dois usuários. Possui conteúdo, status de leitura e timestamp. Referencia `User` duas vezes: `Sender` e `Receiver`.

---

## Relacionamentos

```
User 1──N Review
User 1──N Order ──── 1──N OrderItem N──1 Game
User 1──N CartItem N──1 Game
User 1──N WishlistItem N──1 Game
User 1──N UserLibrary N──1 Game
User 1──N Friendship (Requester ou Receiver)
User 1──N Message (Sender ou Receiver)
Game N──N Genre (via GameGenre)
```

---

## Endpoints

### Auth
| Método | Rota | Descrição |
|---|---|---|
| POST | `/api/auth/login` | Login — seta cookie JWT |
| POST | `/api/auth/logout` | Logout — limpa cookie |

### Users
| Método | Rota | Auth | Descrição |
|---|---|---|---|
| GET | `/api/users/me` | ✓ | Perfil do usuário autenticado |
| POST | `/api/users` | — | Cadastro |

### Games
| Método | Rota | Auth | Descrição |
|---|---|---|---|
| GET | `/api/games` | — | Listar jogos (paginação, filtros, ordenação) |
| GET | `/api/games/{id}` | — | Detalhe de um jogo |
| GET | `/api/games/genres` | — | Listar gêneros |

### Reviews
| Método | Rota | Auth | Descrição |
|---|---|---|---|
| GET | `/api/reviews/game/{gameId}` | — | Reviews de um jogo |
| POST | `/api/reviews` | ✓ | Criar review |
| PUT | `/api/reviews/{id}` | ✓ | Editar review |
| DELETE | `/api/reviews/{id}` | ✓ | Deletar review |
| POST | `/api/reviews/{id}/helpful` | ✓ | Votar como útil |
| POST | `/api/reviews/{id}/funny` | ✓ | Votar como engraçado |

### Cart
| Método | Rota | Auth | Descrição |
|---|---|---|---|
| GET | `/api/cart` | ✓ | Ver carrinho |
| POST | `/api/cart/{gameId}` | ✓ | Adicionar ao carrinho |
| DELETE | `/api/cart/{gameId}` | ✓ | Remover item |
| DELETE | `/api/cart` | ✓ | Limpar carrinho |

### Wishlist
| Método | Rota | Auth | Descrição |
|---|---|---|---|
| GET | `/api/wishlist` | ✓ | Ver wishlist |
| POST | `/api/wishlist/{gameId}` | ✓ | Adicionar |
| DELETE | `/api/wishlist/{gameId}` | ✓ | Remover |

### Library
| Método | Rota | Auth | Descrição |
|---|---|---|---|
| GET | `/api/library` | ✓ | Ver biblioteca |
| GET | `/api/library/{gameId}/owned` | ✓ | Verificar posse do jogo |

### Orders
| Método | Rota | Auth | Descrição |
|---|---|---|---|
| GET | `/api/orders` | ✓ | Histórico de pedidos |
| GET | `/api/orders/{id}` | ✓ | Detalhe de um pedido |
| POST | `/api/orders/checkout` | ✓ | Finalizar compra (a partir do carrinho) |

### Friends
| Método | Rota | Auth | Descrição |
|---|---|---|---|
| GET | `/api/friends` | ✓ | Lista de amigos aceitos |
| GET | `/api/friends/requests` | ✓ | Solicitações pendentes |
| GET | `/api/friends/search?q=` | ✓ | Buscar usuários |
| POST | `/api/friends/{userId}` | ✓ | Enviar solicitação |
| PUT | `/api/friends/{userId}/accept` | ✓ | Aceitar solicitação |
| DELETE | `/api/friends/{friendId}` | ✓ | Remover amigo |

### Messages
| Método | Rota | Auth | Descrição |
|---|---|---|---|
| GET | `/api/messages/{friendId}` | ✓ | Buscar conversa (paginada) |
| POST | `/api/messages/{friendId}` | ✓ | Enviar mensagem |
| PUT | `/api/messages/{friendId}/read` | ✓ | Marcar como lidas |

### SignalR — `/hubs/chat`
| Direção | Evento | Descrição |
|---|---|---|
| Client → Server | `SendMessage(receiverId, content)` | Envia mensagem em tempo real |
| Server → Client | `ReceiveMessage` | Nova mensagem recebida |
| Server → Client | `MessagesRead` | Mensagens marcadas como lidas |
| Server → Client | `FriendRequestReceived` | Nova solicitação de amizade |
| Server → Client | `FriendRequestAccepted` | Solicitação aceita |

---

## Arquitetura

```
nebula.api/
├── src/
│   ├── Controllers/     # Recebe requisições HTTP, delega para Services
│   ├── Services/        # Regras de negócio
│   ├── Repositories/    # Acesso ao banco via EF Core
│   ├── Entities/        # Modelos mapeados para o banco
│   ├── DTOs/            # Entrada e saída (nunca expõe entidades diretas)
│   ├── Data/            # DbContext e configurações de relacionamentos
│   ├── Hubs/            # SignalR ChatHub
│   ├── Extensions/      # DI, Swagger, JWT, Exception Handler
│   └── Profiles/        # AutoMapper (Entity ↔ DTO)
├── Migrations/          # Migrations EF Core
├── Program.cs           # Pipeline e ponto de entrada
└── .env                 # Variáveis de ambiente (não versionar)
```

**Fluxo de uma requisição:**
```
HTTP Request → Controller → Service → Repository → DbContext → PostgreSQL
                                           ↓
                                     AutoMapper
                                           ↓
                                   HTTP Response (DTO)
```

**Autenticação:**  
O JWT é gerado no login e armazenado em cookie HttpOnly (`NebulaAuthToken`). O middleware extrai o token do cookie automaticamente — não é necessário header `Authorization: Bearer`.

---

## Exemplos de Request/Response

### Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "admin@nebula.com",
  "password": "Admin@123"
}
```
```json
{
  "id": "3fa85f64-...",
  "name": "Admin",
  "email": "admin@nebula.com",
  "username": "admin"
}
```

### Listar jogos com filtros
```http
GET /api/games?search=souls&minPrice=0&maxPrice=200&sortBy=rating&page=1&pageSize=10
```
```json
{
  "items": [...],
  "total": 3,
  "page": 1,
  "pageSize": 10
}
```

### Criar review
```http
POST /api/reviews
Content-Type: application/json

{
  "gameId": "3fa85f64-...",
  "rating": "positive",
  "hoursPlayed": 120,
  "content": "Jogo incrível, recomendo!"
}
```
