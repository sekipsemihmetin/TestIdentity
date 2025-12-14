# TestIdentity API

[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-blue.svg)](https://dotnet.microsoft.com/apps/aspnet)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0-green.svg)](https://docs.microsoft.com/ef/core/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

**TestIdentity API**, ASP.NET Core Identity'nin tüm özelliklerini kapsayan, Clean Architecture prensiplerine uygun olarak geliştirilmiş kapsamlı bir kimlik doğrulama ve yetkilendirme API'sidir.

**TestIdentity API** is a comprehensive authentication and authorization API that covers all features of ASP.NET Core Identity, developed in accordance with Clean Architecture principles.

## 📋 İçindekiler / Table of Contents

- [Özellikler / Features](#-özellikler--features)
- [Mimari / Architecture](#-mimari--architecture)
- [Teknolojiler / Technologies](#-teknolojiler--technologies)
- [Kurulum / Installation](#-kurulum--installation)
- [Yapılandırma / Configuration](#-yapılandırma--configuration)
- [API Endpoints](#-api-endpoints)
- [Kullanım Örnekleri / Usage Examples](#-kullanım-örnekleri--usage-examples)
- [Proje Yapısı / Project Structure](#-proje-yapısı--project-structure)
- [Katkıda Bulunma / Contributing](#-katkıda-bulunma--contributing)

## ✨ Özellikler / Features

### 🔐 Authentication (Kimlik Doğrulama)
- ✅ Kullanıcı kaydı / User Registration
- ✅ Kullanıcı girişi (Username/Email) / User Login (Username/Email)
- ✅ JWT Token Authentication
- ✅ Refresh Token desteği / Refresh Token support
- ✅ E-posta onayı / Email Confirmation
- ✅ Şifre unutma / Forgot Password
- ✅ Şifre sıfırlama / Password Reset
- ✅ Şifre değiştirme / Change Password
- ✅ İki faktörlü kimlik doğrulama (2FA) / Two-Factor Authentication (2FA)
- ✅ Hesap kilitleme (Brute Force koruması) / Account Lockout (Brute Force protection)

### 👥 User Management (Kullanıcı Yönetimi)
- ✅ Kullanıcı listeleme / List Users
- ✅ Kullanıcı detayı / User Details
- ✅ Kullanıcı güncelleme / Update User
- ✅ Kullanıcı silme (Soft Delete) / Delete User (Soft Delete)
- ✅ Kullanıcı aktif/pasif yapma / Activate/Deactivate User
- ✅ Kullanıcı rolleri yönetimi / User Roles Management

### 🎭 Role Management (Rol Yönetimi)
- ✅ Rol oluşturma / Create Role
- ✅ Rol listeleme / List Roles
- ✅ Rol güncelleme / Update Role
- ✅ Rol silme / Delete Role
- ✅ Rol atama/kaldırma / Assign/Remove Role
- ✅ Rol bazlı yetkilendirme / Role-based Authorization

### 🏗️ Architecture (Mimari)
- ✅ Clean Architecture (Katmanlı Mimari)
- ✅ Repository Pattern
- ✅ Dependency Injection
- ✅ Result Pattern
- ✅ DTO Pattern
- ✅ Swagger/OpenAPI Documentation

## 🏛️ Mimari / Architecture

Proje, **Clean Architecture** prensiplerine uygun olarak 4 ana katmandan oluşmaktadır:

The project consists of 4 main layers following **Clean Architecture** principles:

```
TestIdentity/
├── TestIdentity.Domain/          # Domain Layer (Entities, Enums, Interfaces)
├── TestIdentity.Infrastructure/  # Infrastructure Layer (Data Access, Repositories)
├── TestIdentity.Aplication/      # Application Layer (Services, DTOs)
└── TestIdentity.API/             # Presentation Layer (Controllers, API)
```

### Katman Açıklamaları / Layer Descriptions

#### 1. Domain Layer (TestIdentity.Domain)
- **Entities**: `ApplicationUser`, `ApplicationRole`, `Test`
- **Enums**: `Status`, `Roles`
- **Interfaces**: `IEntity`, `IResult`, `IDataResult`
- **Utilities**: Result pattern implementasyonları

#### 2. Infrastructure Layer (TestIdentity.Infrastructure)
- **Data Access**: Entity Framework Core implementasyonu
- **Repositories**: Generic repository pattern
- **DbContext**: `AppDbContext` (IdentityDbContext)
- **Configurations**: Entity konfigürasyonları

#### 3. Application Layer (TestIdentity.Aplication)
- **Services**: Business logic implementasyonları
  - `AuthService`: Kimlik doğrulama işlemleri
  - `UserService`: Kullanıcı yönetimi
  - `RoleService`: Rol yönetimi
- **DTOs**: Data Transfer Objects
- **Mappings**: Mapster kullanılarak DTO-Entity mapping

#### 4. API Layer (TestIdentity.API)
- **Controllers**: RESTful API endpoints
- **Configuration**: Swagger, CORS, JWT yapılandırması

## 🛠️ Teknolojiler / Technologies

- **.NET 8.0** - Framework
- **ASP.NET Core 8.0** - Web API Framework
- **Entity Framework Core 8.0** - ORM
- **ASP.NET Core Identity** - Authentication & Authorization
- **JWT Bearer Authentication** - Token-based Authentication
- **SQL Server** - Database
- **Swagger/OpenAPI** - API Documentation
- **Mapster** - Object Mapping
- **CORS** - Cross-Origin Resource Sharing

## 📦 Kurulum / Installation

### Gereksinimler / Requirements

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) veya SQL Server Express
- [Visual Studio 2022](https://visualstudio.microsoft.com/) veya [Visual Studio Code](https://code.visualstudio.com/)

### Adım 1: Projeyi Klonlayın / Clone the Project

```bash
git clone https://github.com/yourusername/TestIdentity.git
cd TestIdentity
```

### Adım 2: Veritabanı Bağlantı String'ini Yapılandırın / Configure Database Connection String

`TestIdentity.API/appsettings.json` dosyasını açın ve connection string'i güncelleyin:

Open `TestIdentity.API/appsettings.json` file and update the connection string:

```json
{
  "ConnectionStrings": {
    "AppConnectionDev": "Server=YOUR_SERVER; Database=TestIdentity; User Id=YOUR_USER; Password=YOUR_PASSWORD; TrustServerCertificate=True;"
  }
}
```

### Adım 3: JWT Ayarlarını Yapılandırın / Configure JWT Settings

`appsettings.json` dosyasında JWT ayarlarını güncelleyin:

Update JWT settings in `appsettings.json` file:

```json
{
  "Jwt": {
    "Key": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "TestIdentityAPI",
    "Audience": "TestIdentityAPI",
    "ExpireMinutes": "60"
  }
}
```

**Önemli / Important**: Production ortamında güçlü bir secret key kullanın!

### Adım 4: NuGet Paketlerini Yükleyin / Install NuGet Packages

```bash
dotnet restore
```

### Adım 5: Veritabanı Migration'larını Uygulayın / Apply Database Migrations

```bash
cd TestIdentity.Infrastructure
dotnet ef migrations add InitialIdentityMigration --startup-project ../TestIdentity.API
dotnet ef database update --startup-project ../TestIdentity.API
```

### Adım 6: Projeyi Çalıştırın / Run the Project

```bash
cd TestIdentity.API
dotnet run
```

API şu adreslerde çalışacaktır:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `https://localhost:5001/swagger`

## ⚙️ Yapılandırma / Configuration

### Identity Ayarları / Identity Settings

Identity ayarları `TestIdentity.Infrastructure/Extentions/DependencyInjection.cs` dosyasında yapılandırılmıştır:

Identity settings are configured in `TestIdentity.Infrastructure/Extentions/DependencyInjection.cs`:

```csharp
services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    // Password settings / Şifre ayarları
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
    
    // Lockout settings / Kilitlenme ayarları
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    
    // User settings / Kullanıcı ayarları
    options.User.RequireUniqueEmail = true;
})
```

### CORS Ayarları / CORS Settings

CORS ayarları `Program.cs` dosyasında yapılandırılmıştır. Development ortamında tüm origin'lere izin verilir:

CORS settings are configured in `Program.cs`. In development environment, all origins are allowed:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

**Production ortamında güvenlik için belirli origin'leri belirtin!**

**For security in production environment, specify specific origins!**

## 📡 API Endpoints

### 🔐 Authentication Endpoints

#### Kullanıcı Kaydı / User Registration
```http
POST /api/Auth/register
Content-Type: application/json

{
  "userName": "johndoe",
  "email": "john@example.com",
  "password": "Password123!",
  "confirmPassword": "Password123!",
  "firstName": "John",
  "lastName": "Doe",
  "phoneNumber": "+905551234567"
}
```

#### Kullanıcı Girişi / User Login
```http
POST /api/Auth/login
Content-Type: application/json

{
  "userNameOrEmail": "johndoe",
  "password": "Password123!",
  "rememberMe": false
}
```

**Response:**
```json
{
  "isSuccess": true,
  "message": "Giriş başarılı",
  "data": {
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "refreshToken": "refresh_token_here",
    "expiresAt": "2024-12-14T18:00:00Z",
    "tokenType": "Bearer"
  }
}
```

#### Token Yenileme / Refresh Token
```http
POST /api/Auth/refresh-token
Content-Type: application/json

{
  "refreshToken": "refresh_token_here"
}
```

#### Şifre Değiştirme / Change Password
```http
POST /api/Auth/change-password
Authorization: Bearer {token}
Content-Type: application/json

{
  "currentPassword": "OldPassword123!",
  "newPassword": "NewPassword123!",
  "confirmPassword": "NewPassword123!"
}
```

### 👥 User Management Endpoints

#### Tüm Kullanıcıları Listele / List All Users
```http
GET /api/User
Authorization: Bearer {token}
```

#### Kullanıcı Detayı / User Details
```http
GET /api/User/{id}
Authorization: Bearer {token}
```

#### Kullanıcı Güncelle / Update User
```http
PUT /api/User
Authorization: Bearer {token}
Content-Type: application/json

{
  "id": "user-id-here",
  "firstName": "John",
  "lastName": "Doe",
  "phoneNumber": "+905551234567"
}
```

### 🎭 Role Management Endpoints

#### Rol Oluştur / Create Role
```http
POST /api/Role
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Manager",
  "description": "Manager role with extended permissions"
}
```

#### Kullanıcıya Rol Ata / Assign Role to User
```http
POST /api/User/{userId}/roles/{roleName}
Authorization: Bearer {token}
```

## 💡 Kullanım Örnekleri / Usage Examples

### 1. Kullanıcı Kaydı ve Girişi / User Registration and Login

```bash
# 1. Kullanıcı kaydı / Register user
curl -X POST https://localhost:5001/api/Auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "johndoe",
    "email": "john@example.com",
    "password": "Password123!",
    "confirmPassword": "Password123!",
    "firstName": "John",
    "lastName": "Doe"
  }'

# 2. Giriş yap / Login
curl -X POST https://localhost:5001/api/Auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "userNameOrEmail": "johndoe",
    "password": "Password123!"
  }'

# 3. Token ile korumalı endpoint'e eriş / Access protected endpoint with token
curl -X GET https://localhost:5001/api/User \
  -H "Authorization: Bearer YOUR_ACCESS_TOKEN"
```

### 2. Swagger UI Kullanımı / Using Swagger UI

1. Projeyi çalıştırın / Run the project
2. Tarayıcıda `https://localhost:5001/swagger` adresine gidin / Navigate to `https://localhost:5001/swagger`
3. "Authorize" butonuna tıklayın / Click "Authorize" button
4. Login endpoint'ini kullanarak token alın / Get token using login endpoint
5. Token'ı "Bearer {token}" formatında girin / Enter token in "Bearer {token}" format
6. Artık tüm korumalı endpoint'lere erişebilirsiniz / Now you can access all protected endpoints

### 3. Postman Kullanımı / Using Postman

1. **Collection Oluştur / Create Collection**: TestIdentity API
2. **Environment Oluştur / Create Environment**: 
   - `baseUrl`: `https://localhost:5001`
   - `token`: (Login sonrası otomatik doldurulacak)
3. **Pre-request Script** (Login için):
```javascript
pm.sendRequest({
    url: pm.environment.get("baseUrl") + "/api/Auth/login",
    method: 'POST',
    header: {'Content-Type': 'application/json'},
    body: {
        mode: 'raw',
        raw: JSON.stringify({
            userNameOrEmail: "johndoe",
            password: "Password123!"
        })
    }
}, function (err, res) {
    if (res.json().isSuccess) {
        pm.environment.set("token", res.json().data.accessToken);
    }
});
```

## 📁 Proje Yapısı / Project Structure

```
TestIdentity/
│
├── TestIdentity.Domain/                    # Domain Layer
│   ├── Core/
│   │   ├── BaseEntites/                   # Base entities
│   │   ├── BaseEntityConfigurations/       # Entity configurations
│   │   └── Interfaces/                    # Domain interfaces
│   ├── Entities/                          # Domain entities
│   │   ├── ApplicationUser.cs
│   │   ├── ApplicationRole.cs
│   │   └── Test.cs
│   ├── Enums/                             # Enumerations
│   └── Utilities/                         # Result pattern
│
├── TestIdentity.Infrastructure/            # Infrastructure Layer
│   ├── AppContext/                        # DbContext
│   ├── Configurations/                    # EF Configurations
│   ├── DataAccess/                        # Repository pattern
│   │   ├── EntityFramework/
│   │   └── Interfaces/
│   ├── Extentions/                        # DI Extensions
│   └── Repositories/                      # Repositories
│
├── TestIdentity.Aplication/                # Application Layer
│   ├── DTOs/                              # Data Transfer Objects
│   │   ├── AuthDTOs/
│   │   ├── UserDTOs/
│   │   └── RoleDTOs/
│   ├── Services/                          # Business logic
│   │   ├── AuthServices/
│   │   ├── UserServices/
│   │   └── RoleServices/
│   └── Extentions/                        # DI Extensions
│
└── TestIdentity.API/                       # API Layer
    ├── Controllers/                       # API Controllers
    │   ├── AuthController.cs
    │   ├── UserController.cs
    │   └── RoleController.cs
    ├── Program.cs                         # Startup configuration
    └── appsettings.json                  # Configuration
```

## 🔒 Güvenlik / Security

### Öneriler / Recommendations

1. **JWT Secret Key**: Production ortamında güçlü ve rastgele bir secret key kullanın
2. **HTTPS**: Production'da mutlaka HTTPS kullanın
3. **CORS**: Production'da sadece gerekli origin'lere izin verin
4. **Password Policy**: Güçlü şifre politikaları uygulayın
5. **Rate Limiting**: API'ye rate limiting ekleyin
6. **Input Validation**: Tüm input'ları validate edin
7. **SQL Injection**: Entity Framework Core kullanıldığı için otomatik korunuyor
8. **XSS Protection**: ASP.NET Core otomatik XSS koruması sağlar

## 🧪 Test Etme / Testing

### Swagger UI ile Test / Testing with Swagger UI

1. Projeyi çalıştırın / Run the project
2. `https://localhost:5001/swagger` adresine gidin
3. Endpoint'leri test edin / Test endpoints

### Postman Collection

Postman collection örneği için `docs/postman` klasörüne bakın.

For Postman collection example, check `docs/postman` folder.

## 📝 API Response Format

Tüm API yanıtları standart bir formatta döner:

All API responses return in a standard format:

### Başarılı Yanıt / Success Response
```json
{
  "isSuccess": true,
  "message": "İşlem başarılı",
  "data": { ... }
}
```

### Hata Yanıtı / Error Response
```json
{
  "isSuccess": false,
  "message": "Hata mesajı"
}
```

## 🐛 Sorun Giderme / Troubleshooting

### Migration Hatası / Migration Error

```bash
# Migration'ları sıfırlayın / Reset migrations
dotnet ef database drop --startup-project ../TestIdentity.API
dotnet ef migrations remove --startup-project ../TestIdentity.API
dotnet ef migrations add InitialIdentityMigration --startup-project ../TestIdentity.API
dotnet ef database update --startup-project ../TestIdentity.API
```

### JWT Token Hatası / JWT Token Error

- `appsettings.json` dosyasında JWT Key'in en az 32 karakter olduğundan emin olun
- Token'ın süresi dolmuş olabilir, refresh token kullanın

### Veritabanı Bağlantı Hatası / Database Connection Error

- Connection string'in doğru olduğundan emin olun
- SQL Server'ın çalıştığından emin olun
- Firewall ayarlarını kontrol edin

## 🤝 Katkıda Bulunma / Contributing

Katkılarınızı bekliyoruz! Lütfen şu adımları izleyin:

We welcome your contributions! Please follow these steps:

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 Lisans / License

Bu proje MIT lisansı altında lisanslanmıştır.

This project is licensed under the MIT License.

## 👨‍💻 Geliştirici / Developer

Proje, Clean Architecture ve ASP.NET Core Identity best practices kullanılarak geliştirilmiştir.

The project is developed using Clean Architecture and ASP.NET Core Identity best practices.

## 📚 Ek Kaynaklar / Additional Resources

- [ASP.NET Core Identity Documentation](https://docs.microsoft.com/aspnet/core/security/authentication/identity)
- [JWT Authentication](https://jwt.io/)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Entity Framework Core](https://docs.microsoft.com/ef/core/)

## ⭐ Yıldız Vermeyi Unutmayın! / Don't Forget to Star!

Bu projeyi beğendiyseniz, yıldız vermeyi unutmayın! ⭐

If you liked this project, don't forget to give it a star! ⭐

---

**Not / Note**: Bu proje eğitim amaçlıdır. Production kullanımı için ek güvenlik önlemleri alınmalıdır.

**Note**: This project is for educational purposes. Additional security measures should be taken for production use.

#   T e s t I d e n t i t y 
 
 
