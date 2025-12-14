# TestIdentity API

[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-blue.svg)](https://dotnet.microsoft.com/apps/aspnet)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0-green.svg)](https://docs.microsoft.com/ef/core/)


**TestIdentity API**, ASP.NET Core Identity'nin tüm özelliklerini kapsayan, Clean Architecture prensiplerine uygun olarak geliştirilmiş kapsamlı bir kimlik doğrulama ve yetkilendirme API'sidir.

**TestIdentity API** is a comprehensive authentication and authorization API that covers all features of ASP.NET Core Identity, developed in accordance with Clean Architecture principles.

---

## 📋 İçindekiler / Table of Contents

* [Özellikler / Features](#-özellikler--features)
* [Mimari / Architecture](#-mimari--architecture)
* [Teknolojiler / Technologies](#-teknolojiler--technologies)
* [Kurulum / Installation](#-kurulum--installation)
* [Yapılandırma / Configuration](#-yapılandırma--configuration)
* [API Endpoints](#-api-endpoints)
* [Kullanım Örnekleri / Usage Examples](#-kullanım-örnekleri--usage-examples)
* [Proje Yapısı / Project Structure](#-proje-yapısı--project-structure)
* [Katkıda Bulunma / Contributing](#-katkıda-bulunma--contributing)

---

## ✨ Özellikler / Features

### 🔐 Authentication (Kimlik Doğrulama)

* Kullanıcı kaydı / User Registration
* Kullanıcı girişi (Username/Email) / User Login (Username/Email)
* JWT Token Authentication
* Refresh Token desteği / Refresh Token support
* E-posta onayı / Email Confirmation
* Şifre unutma / Forgot Password
* Şifre sıfırlama / Password Reset
* Şifre değiştirme / Change Password
* İki faktörlü kimlik doğrulama (2FA) / Two-Factor Authentication (2FA)
* Hesap kilitleme (Brute Force koruması) / Account Lockout

### 👥 User Management (Kullanıcı Yönetimi)

* Kullanıcı listeleme / List Users
* Kullanıcı detayı / User Details
* Kullanıcı güncelleme / Update User
* Kullanıcı silme (Soft Delete) / Delete User (Soft Delete)
* Kullanıcı aktif/pasif yapma / Activate/Deactivate User
* Kullanıcı rolleri yönetimi / User Roles Management

### 🎭 Role Management (Rol Yönetimi)

* Rol oluşturma / Create Role
* Rol listeleme / List Roles
* Rol güncelleme / Update Role
* Rol silme / Delete Role
* Rol atama/kaldırma / Assign/Remove Role
* Rol bazlı yetkilendirme / Role-based Authorization

### 🏗️ Architecture (Mimari)

* Clean Architecture
* Repository Pattern
* Dependency Injection
* Result Pattern
* DTO Pattern
* Swagger / OpenAPI

---

## 🏛️ Mimari / Architecture

```text
TestIdentity/
├── TestIdentity.Domain/
├── TestIdentity.Infrastructure/
├── TestIdentity.Application/
└── TestIdentity.API/
```

---

## 🛠️ Teknolojiler / Technologies

* .NET 8.0
* ASP.NET Core 8.0
* Entity Framework Core 8.0
* ASP.NET Core Identity
* JWT Bearer Authentication
* SQL Server
* Swagger / OpenAPI
* Mapster
* CORS

---

## 📦 Kurulum / Installation

### Gereksinimler / Requirements

* .NET 8 SDK
* SQL Server / Express
* Visual Studio 2022 veya VS Code

### Projeyi Klonlama

```bash
git clone https://github.com/yourusername/TestIdentity.git
cd TestIdentity
```

### Connection String

```json
{
  "ConnectionStrings": {
    "AppConnectionDev": "Server=YOUR_SERVER;Database=TestIdentity;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
  }
}
```

### JWT Ayarları

```json
{
  "Jwt": {
    "Key": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "TestIdentityAPI",
    "Audience": "TestIdentityAPI",
    "ExpireMinutes": 60
  }
}
```

---

## 📡 API Endpoints

### Auth

* `POST /api/Auth/register`
* `POST /api/Auth/login`
* `POST /api/Auth/refresh-token`
* `POST /api/Auth/change-password`

### Users

* `GET /api/User`
* `GET /api/User/{id}`
* `PUT /api/User`

### Roles

* `POST /api/Role`
* `POST /api/User/{userId}/roles/{roleName}`

---

## 📁 Proje Yapısı / Project Structure

```text
TestIdentity/
├── Domain
├── Infrastructure
├── Application
└── API
```

---

## 🔒 Güvenlik / Security

* HTTPS kullanın
* Güçlü JWT secret
* CORS sınırlandırması
* Rate limiting
* Input validation

---


## ⭐ Star Vermeyi Unutmayın

Beğendiyseniz ⭐ bırakmayı unutmayın.
