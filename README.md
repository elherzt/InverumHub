# InverumHub

InverumHub is a modular backend starter project built with **ASP.NET Core**, designed to serve as a solid foundation for applications that require **authentication, authorization, and multi-application role management**.

The project is intentionally lightweight, easy to run, and easy to adapt. It can be used as a starting point for internal tools, SaaS platforms, or custom enterprise systems.

---

## Key Features

- JWT authentication using **RSA keys**
- Role-based authorization **per application**
- Clean separation between API, Core, and Infrastructure layers
- SQLite database for quick setup and portability
- RESTful API design
- Entity Framework Core
- AutoMapper for DTO mapping
- Swagger / OpenAPI integration

---

## Tech Stack

- **.NET 9 / ASP.NET Core Web API**
- **Entity Framework Core**
- **SQLite**
- **JWT (RSA + public/private keys)**
- **AutoMapper**
- **Swagger**
- **Repository Pattern**

---

## Project Philosophy

This project is designed as a **starter or bootstrap backend**, not a final product.

Key decisions:
- **SQLite** is used by default to keep the setup simple and dependency-free.  
  It can be easily replaced with SQL Server, PostgreSQL, or any other provider.
- Authentication and authorization are implemented in a **generic and extensible way**, allowing the system to scale to multiple applications sharing the same identity source.
- The structure favors clarity, maintainability, and real-world patterns over framework magic.

---

## Architecture Overview

The solution is organized into clear layers:

- **InverumHub.Api**  
  HTTP endpoints, authentication configuration, middleware, and Swagger.

- **InverumHub.Core**  
  Domain entities, DTOs, interfaces, and business rules.

- **InverumHub.Infrastructure**  
  Database context, EF Core mappings, repositories, and data access.

This separation allows each layer to evolve independently.

---

## Security

- JWT tokens are signed using **RSA private keys**
- Public keys are used for token validation
- Tokens include:
  - User identity
  - Application context
  - Roles per application
- Authorization is enforced using standard ASP.NET Core `[Authorize]` attributes

---

## Getting Started

### Prerequisites

- .NET 9 SDK

No external services or databases are required.

---

### Running the Project

1. Clone the repository

2. Navigate to the API project:
```bash
cd InverumHub.Api
```

3. Restore dependencies:
```bash
dotnet restore
```

4. Run the application:
```bash
dotnet run
```

5. Open Swagger:
```bash
https://localhost:{port}/swagger
```

6. Use the bootstrap endpoint to configure the initial admin user (one-time setup).
---

### Configuration

JWT settings are configured via .env file, copy .env.example and adjust 


This project uses **RSA public/private keys** to sign and validate JWT tokens.

#### 1. Generate RSA Keys

From a terminal (Windows, Linux, or macOS), run:

```bash
# Generate private key
openssl genrsa -out ssot-private.key 2048

# Generate public key from private key
openssl rsa -in ssot-private.key -pubout -out ssot-public.key
```

RSA keys are stored locally under the keys/ directory, replace with new public key.
Only the public key is intended to be committed to source control.

#### 2. Configure the Private Key
Convert the private key to Base64 and store it in your .env file:

```bash
openssl base64 -in ssot-private.key -out ssot-private.base64
```

Then add it as a single line in .env:

```bash
JWTConfig__SecretKeyBase64=MIICXQIBAAKBgQ...
```

---


## Roadmap

This project is intentionally minimal. Future improvements may include:

- Automated test coverage for core domain logic
- Refresh token support
- External identity providers (OAuth / OpenID Connect)
- Audit log for security events
- Rate limiting and abuse protection
- Multi-tenant support


