# Microservices DevOps Platform

Hands-on microservices platform built with **.NET 9**, **Docker Compose** and **PostgreSQL 16**.
Demonstrates service-to-service JWT authentication, database-per-service
isolation and fully containerized deployment.

## 🏗️ Architecture

    AuthService (:8080) --JWT--> ProductService (:8081)
         |                              |
      auth-db (Postgres 16)       product-db (Postgres 16)

- **AuthService** — register/login, BCrypt password hashing, JWT issuing
- **ProductService** — CRUD protected with [Authorize], validates AuthService tokens
- **Database-per-service** — each service owns its own PostgreSQL database
- **Scalar UI** — API docs at /scalar

## 🧰 Tech Stack
.NET 9 Web API • EF Core 9 (Npgsql) • PostgreSQL 16 (Alpine)
Docker & Docker Compose • JWT Bearer (HS256) + BCrypt • Scalar API Reference

## 🚀 Run
Prerequisite: Docker Desktop

    docker compose up -d --build

- Auth API docs:    http://localhost:8080/scalar
- Product API docs: http://localhost:8081/scalar

## 🔐 Auth Flow
1. POST /api/Auth/register  → create user
2. POST /api/Auth/login     → returns JWT
3. GET  /api/Product        → header: Authorization: Bearer <token>

## 📌 Lessons Learned
- HMAC key-size rules (HS256 ≥ 32 bytes vs HS512 ≥ 64 bytes)
- Docker port mapping & container networking
- Debugging auth between services (tokens, keys, middleware order)

— Maksat (MAKSAT-DEL)