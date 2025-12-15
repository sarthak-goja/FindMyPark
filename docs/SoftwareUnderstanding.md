
# Software Understanding Document (SUD)
**Project**: FindMyPark
**Version**: 1.0

## 1. System Architecture
FindMyPark follows a Monolithic Client-Server architecture, containerized for cloud deployment.

### 1.1 High Level Diagram
`[Angular UI] <--(HTTP/JSON)--> [ASP.NET Core API] <--> [SQLite Database]`

### 1.2 Technology Stack
- **Frontend**: Angular 18 (Standalone Components, Signals).
- **Backend**: .NET 8 Web API.
- **Database**: SQLite (for Test/Dev), Oracle (Production Ready).
- **Maps**: Mapbox GL JS.
- **Infrastructure**: Docker for containerization.

## 2. Key Modules
1.  **AuthService**: Handles JWT generation and Role Management.
2.  **BookingEngine**: Manages slot reservation, price calculation, and concurrency.
3.  **PricingService**: Implements dynamic pricing logic based on demand and time.
4.  **AdminDashboard**: Monitoring tool for business metrics (Heatmaps, KYC).

## 3. Data Flow (Booking)
1.  User selects a spot on Map -> `GET /api/Listings`.
2.  User confirms time -> `GET /api/Bookings/calculate-price`.
3.  User pays -> `POST /api/Bookings` (Transaction created).
4.  Confirmation sent -> `POST /api/Notifications`.

## 4. Code Structure
- **FindMyPark.UI**: Source code for the Frontend.
- **FindMyPark.API**: Source code for the Backend.
- **.github/workflows**: CI/CD pipeline definitions.
