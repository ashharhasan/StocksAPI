# 🚀 Modern .NET 8 Web API (Work in Progress)

A production-ready backend with PostgreSQL, Docker, and JWT auth — built for learning and scalability.

![.NET 8](https://img.shields.io/badge/.NET-8.0-blueviolet)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16.0-blue)
![Docker](https://img.shields.io/badge/Docker-Containerized-2496ED)
![Status](https://img.shields.io/badge/Status-WIP-yellow)

---

## 🔥 Current Features

- ✅ **.NET 8** – Latest LTS version with performance optimizations.
- ✅ **PostgreSQL + EF Core** – Cross-platform database with migrations.
- ✅ **JWT Authentication** – Secure auth via `JsonWebTokenHandler`.
- ✅ **Docker Support 📦** – Containerized and confirmed working on port `8080`.
- ✅ **Swagger Docs** – Interactive API testing.

---

## 🚧 Upcoming Features (Planned for Learning!)

- **React Frontend** – Modern UI (coming soon).
- **Unit/Integration Tests** – `xUnit` + `Moq` (WIP).
- **CI/CD Pipelines** – GitHub Actions / Azure DevOps.

---

## 🛠️ Getting Started

### Prerequisites

- .NET 8 SDK  
- Docker (optional)  
- PostgreSQL

### Run Locally

Clone the repo:

```bash
git clone https://github.com/your-username/your-repo.git<br>
cd your-repo
'''

Configure PostgreSQL:

Update `appsettings.json` with your connection string.

Run migrations:

```bash
dotnet ef database update
```

Start the API:

```bash
dotnet run
```

Swagger UI: [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

### Run with Docker

```bash
docker build -t stocks-api .<br>
docker run -d -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Production stocks-api:latest
```

Access API: [http://localhost:8080/swagger](http://localhost:8080/swagger)

---

## 📂 Project Structure

<code>
src/<br>
├── StocksApi.Web/           # API Layer (Controllers, DI)<br>
├── StocksApi.Core/          # Domain Models<br>
├── StocksApi.Infrastructure/ # EF Core, Repositories
</code>

---

## 🤝 Contributing

PRs welcome! This is a learning project—feel free to suggest improvements.
