
# 🅿️ FindMyPark - Smart Parking Management System

**FindMyPark** is a next-generation platform connecting drivers with available parking spots. Built with modern web technologies, it offers a seamless experience for booking, payments, and parking management.

![Status](https://img.shields.io/badge/Status-Live-green)
![Version](https://img.shields.io/badge/Version-1.0.0-blue)
![Stack](https://img.shields.io/badge/Stack-Angular%20%7C%20.NET%208-purple)

## 🚀 Features

### 🚗 For Drivers
- **Interactive Map**: Visualize parking spots with Mapbox GL JS.
- **Smart Booking**: Reserve spots in advance with dynamic pricing.
- **Wallet**: Integrated digital wallet for seamless payments.
- **EV Support**: Filter for Electric Vehicle charging stations (Level 1 & 2).

### 🏠 For Hosts
- **Easy Listing**: Monetize unused driveways or garages.
- **Dashboard**: Track earnings and occupancy in real-time.
- **KYC**: Secure identity verification for trust and safety.

### 🛡️ Platform
- **Admin Dashboard**: Heatmaps, User Management, and Approvals.
- **Localization**: Full support for English and Hindi (🇮🇳).
- **Security**: Zero-Trust Authentication and Role-Based Access Control.

## 🛠️ Tech Stack

- **Frontend**: Angular 18 (Signals, Standalone Components, Tailwind/Vanilla CSS)
- **Backend**: .NET 8 Web API (C#)
- **Database**: SQLite (Dev) / Oracle (Prod)
- **Maps**: Mapbox GL JS
- **Infrastructure**: Docker & Nginx

## 📚 Documentation

Detailed documentation is available in the [`docs/`](./docs) directory:

- **[Functional Requirements (FRS)](./docs/FRS.md)**
- **[User Requirements (URS)](./docs/URS.md)**
- **[Validation Guide (PSIQ/VSIQ)](./docs/PSIQ.md)**
- **[Software Architecture](./docs/SoftwareUnderstanding.md)**
- **[FAQ](./docs/FAQ.md)**

## 🏁 Getting Started

### Prerequisites
- Node.js 20+
- .NET 8 SDK
- Docker Desktop

### Quick Start (Docker)
Run the entire stack with a single command:
```bash
docker-compose up -d --build
```
- **UI**: http://localhost:4200
- **API**: http://localhost:5175

### Manual Setup
**1. Backend**
```bash
cd FindMyPark.API
dotnet restore
dotnet run
```

**2. Frontend**
```bash
cd FindMyPark.UI
npm install
ng serve
```

## 📄 License
This project is licensed under the MIT License.
