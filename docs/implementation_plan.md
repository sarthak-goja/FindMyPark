# FindMyPark - Implementation Plan

## Goal Description
Build a comprehensive, enterprise-grade parking application for Delhi NCR that connects drivers with parking hosts. The system includes real-time search, booking, dynamic pricing, and role-based dashboards (Driver, Host, Admin).

## User Review Required
> [!IMPORTANT]
> **Tech Stack Update**: As requested, switching to **Angular** for the frontend and **.NET Web API** for the backend.
> **Styling**: Strictly **Vanilla CSS** with modern variables and flexbox/grid layouts.
> **Database**: **Oracle Database** (via Oracle.EntityFrameworkCore).

## Proposed Changes

### Project Structure
Root directory `FindMyPark` containing:
- `FindMyPark.UI`: Angular Client application.
- `FindMyPark.API`: ASP.NET Core Web API.
- `FindMyPark.sln`: Solution file managing both projects.

### Core Components
#### [NEW] [Frontend - Angular]
- **Layouts**: `MainLayoutComponent`, `DashboardLayoutComponent`.
- **Services**: `AuthService`, `ParkingService`, `BookingService`.
- **Styles**: Global `styles.css` with CSS variables.

#### [NEW] [Backend - .NET API]
- **Controllers**: 
    - `AuthController` (Login, OTP, KYC)
    - `ListingsController` (CRUD, Availability, GPS Verify)
    - `BookingsController` (Reserve, Cancel, History)
    - `WalletController` (Balance, Transactions)
    - `AdminController` (Approvals, Promotions, Announcements)
    - `IoTController` (Sensor data ingestion)
    - `SupportController` (Ticket/Chat management)
    - `CorporateController` (Fleet management, Bulk booking)
    - `TenantController` (Theme/Branding config for White-label)
    - `VehicleController` (RTO Challan API mock, Insurance tracker)
    - `EVController` (OCPI standard integration for charger status)
    - `DeveloperController` (Feature Flags, System Health, Logs)
- **Services**:
    - `PricingService`: Dynamic pricing logic (Peak hours/Events).
    - `PaymentService`: Aggregator for UPI, Cards, and **FASTag**.
    - `VehicleService`: Integrations with Vaahan/Parivahan (mock).
    - `FeatureFlagService`: Dynamic runtime configuration. 
        - Flags: `EnableEV`, `EnableCarServices`, `EnableChallan`, `EnableAds`, `EnableSupport`.
    - `NotificationService`: Push/SMS/Email triggers.
    - `AIService`: Placeholder for demand prediction/recommendations.
    - `GeoService`: Validation of coordinates and anti-spoofing.
- **Models**: `User` (Driver/Host), `Listing` (Attributes: EV, Covered), `Booking`, `Review`, `Transaction`, `Promotion`.
- **Data**: `AppDbContext` (EF Core with **Oracle** Provider).
#### [NEW] [Features]
- **Authentication & Trust**: 
    - E-KYC Flow (Aadhaar/PAN upload mock).
    - Two-way Rating System.
- **Search & Discovery**: 
    - Map-centric UI with "Find My Car".
    - Filters: Price, Vehicle Type (2W/4W), Amenities.
- **Booking & Revenue**: 
    - Hourly vs Monthly Subscription logic.
    - Dynamic Pricing display.
    - Commission calculation.
- **Dashboards**: 
    - **Host**: Recurring availability scheduler, Revenue charts.
    - **Driver**: History, Favorites, Wallet.
    - **Admin**: Content updates (City announcements), User management.
    - **Support Agent**: Specialized dashboard to view/reply to tickets.
    - **Developer**: Master Control Panel for Feature Flags and System Config.

#### [NEW] [Advanced Features]
- **Localization**: `ngx-translate` or similar for Hindi/English toggling.
- **IoT & Smart Cities**: Endpoints to receive data from parking sensors/CCTV.
- **Event Mode**: Admin capability to geofence areas (e.g., Stadiums) and apply temporary rules.
- **Monetization**:
    - Ad Banner Component (Placeholders).
    - "Promoted Spot" logic in search results.
- **Enterprise & B2B**:
    - Fleet Dashboard: Manage multiple vehicles and bookings.
    - Tenant Configuration Service: Load logos/colors based on domain.
- **Mobile First**:
    - PWA Service Worker: Cache active booking ticket for offline viewing.

#### [NEW] [Security]
- **Anti-Spoofing**: Checks for mock location flags or impossible travel times.
### Phase 3: B2B & Operations (New)
#### [NEW] [B2B Module - Fleet Management]
- **Goal**: Allow corporate clients (taxi companies, logistics) to manage multiple vehicles and bookings from a single dashboard.
- **Backend**:
    - **Models**:
        - `Fleet`: Represents a company (Name, GST, AdminId).
        - `User`: Update to include `FleetId` (nullable) to link drivers to a fleet.
    - **Controller**: `CorporateController`
        - `POST /register`: Register a new Fleet.
        - `POST /vehicle`: Add vehicle to fleet.
        - `GET /dashboard/{fleetId}`: Aggregated view of all fleet bookings and expenses.
- **Frontend**:
    - **Route**: `/corporate` (Protected, requires `FleetManager` role).
    - **Components**:
        - `FleetDashboardComponent`: Stats (Total Spent, Active Vehicles).
        - `VehicleManagementComponent`: Table to Add/Edit/Remove fleet vehicles.
        - `BulkBookingComponent`: Book multiple spots for a list of vehicles.

#### [NEW] [Support Module]
- **Goal**: Enable users to report issues and admins/agents to resolve them.
- **Backend**:
    - **Models**:
        - `SupportTicket`: Id, UserId, Subject, Description, Status (Open/In-Progress/Resolved), CreatedAt.
    - **Controller**: `SupportController`
        - `POST /`: Create Ticket.
        - `GET /user/{userId}`: Get tickets for a user.
        - `GET /all`: Get all tickets (Agent only).
        - `PUT /{id}/status`: Update ticket status.
- **Frontend**:
    - **Route**: `/support` (User view), `/admin/support` (Agent view).
    - **Components**:
        - `SupportDashboardComponent`: For agents to view/filter tickets.
        - `CreateTicketComponent`: Modal or page for users.

#### [NEW] [Event Management - Surge Pricing]
- **Goal**: Admins can define temporary high-demand zones to trigger surge pricing.
- **Backend**:
    - **Models**:
        - `EventZone`: Id, Name, Lat, Lng, Radius (meters), Multiplier (e.g. 2.0), StartTime, EndTime.
    - **Controller**: `EventsController`
        - `POST /`: Create Event Zone.
        - `GET /active`: Get currently active events.
    - **Logic**: Update `PricingService` to check if a Listing is within an `EventZone` and applies the multiplier.
- **Frontend**:
    - **Route**: `/admin/events`.
    - **Components**:
    - **Components**:
        - `EventInterceptorComponent`: Admin form to set location, radius, and surge factor.

#### [NEW] [Multi-tenancy - White Labeling]
- **Goal**: Allow corporate locations (e.g., Malls, Tech Parks) to have custom branding.
- **Backend**:
    - **Models**:
        - `Tenant`: Id, Name, Domain (e.g. `dlf.findmypark.com`), PrimaryColor, LogoUrl.
    - **Controller**: `TenantController` (Get Config by Domain).
- **Frontend**:
    - **ThemeService**: Dynamic CSS variable updates (`--color-primary`, `--color-bg-brand`).
    - **Logic**: On App Init, check hostname/subdomain, fetch Tenant config, apply styles.

### Phase 4: Intelligence & Platform Scaling (New)
#### [NEW] [AI & Data Module]
- **Goal**: enhance user experience and revenue with predictive intelligence.
- **Backend (AIService)**:
    - **Prediction**: Simple linear regression or moving average on `Booking` history to predict `Occupancy`.
    - **Endpoint**: `GET /api/ai/prediction?listingId={id}&date={date}`.
- **Frontend**:
    - **Visualization**: Show "Busy Times" graph on Listing Details page.

#### [NEW] [Developer Platform]
- **Goal**: Runtime control over system features.
- **Backend (DeveloperController)**:
    - **Feature Flags**: Store key-value pairs (`EnableAds`=true).
    - **Health Check**: DB connection status, IoT gateway latency.
- **Frontend**:
    - **Route**: `/developer` (SuperAdmin only).
    - **Panel**: Toggle switches for flags, Status indicators.

#### [NEW] [Ad Module]
- **Goal**: Monetize high-traffic pages with banners.
- **Backend**:
    - **Models**: `Ad` (Id, ImageUrl, LinkUrl, TargetCity, Priority).
    - **Controller**: `AdController` (Get ads by city/context).
- **Frontend**:
    - **Component**: `AdBannerComponent` (inputs: `slot`).
    - **Placements**: Home Page (Hero bottom), Search Results (Every 5th item), Booking Confirmation.

#### [NEW] [Knowledge Base & Chatbot]
- **Goal**: Auto-resolve common user queries to reduce support load.
- **Backend**:
    - **Models**: `FaqItem` (Question, Answer, Category).
    - **Controller**: `SupportController` (Get FAQs).
- **Frontend**:
    - **Component**: `ChatWidgetComponent` (Floating action button).
    - **Logic**: Display FAQs, simplistic decision tree for "Refunds", "Cancellations".

#### [NEW] [Global Configuration]
- **Goal**: Centralized control of system parameters.
- **Backend**:
    - **Controller**: `DeveloperController`.
    - **Endpoint**: `POST /config` (Update MaintenanceMode, MaxBookingLimit).
- **Frontend**:
    - **Update**: `DeveloperDashboardComponent` to include inputs for global settings.

### Phase 5: Release Engineering (New)
#### [NEW] [Dockerization]
- **Goal**: Containerize the application for consistent deployment.
- **Backend**:
    - `Dockerfile`: Multi-stage build for .NET 8 API.
- **Frontend**:
    - `Dockerfile`: Build Angular and serve via Nginx (alpine).
    - `docker-compose.yml`: Orchestrate API, UI, and Oracle DB (mock).

#### [NEW] [CI/CD Pipeline]
- **Goal**: Automate testing and building.
- **Config**: `.github/workflows/main.yml`.
    - Steps: Checkout, Restore/Install, Build, Run Tests.

### Phase 6: Future Innovations (New)
#### [NEW] [Car Services Module]
- **Goal**: Increase order value by selling add-ons.
- **Backend**:
    - **Models**:
        - `ServiceItem`: Id, Name (e.g. "Eco Wash"), Price, Icon.
        - `BookingService`: Link table `BookingId` - `ServiceItemId`.
    - **Controller**: `ServicesController` (GET /available, POST /add).
- **Frontend**:
    - **UI**: "Add-ons" section in `BookingPageComponent`.
    - **Logic**: Update `totalPrice` when services are selected.

#### [NEW] [EV Charging Grid]
- **Goal**: Real-time charger availability via OCPI simulation.
- **Backend**:
    - **Models**: `Charger` (Id, Type: CCS2/Type2, Power: 50kW, Status: Available/Charging).
    - **Controller**: `EVController` (GET /chargers/{listingId}).
- **Frontend**:
    - **UI**: "EV Station" badge and details on Listing Page.
    - **Filter**: "EV Only" toggle in Search.

#### [NEW] [Localization (Multi-language)]
- **Goal**: Make the app accessible to Hindi speaking users.
- **Frontend**:
    - **Service**: `LanguageService` to toggle between 'en' and 'hi'.
    - **Pipes**: `TranslatePipe` to look up keys in a JSON dictionary.
    - **UI**: Language switcher in Navbar.

#### [NEW] [Advanced Analytics]
- **Goal**: Visualize user behavior for Admins.
- **Frontend**:
    - **Component**: `HeatmapComponent` in Admin Dashboard.
    - **Library**: `leaflet.heat` (mocked) to show "Hot Zones" of search activity on the map.

### Data Model (Conceptual)
- **Users**: ID, Name, Role (Driver/Host/Admin), KYC_Status, **FleetId**.
- **Fleet**: ID, Name, AdminUserID.
- **Listings**: ID, HostID, Location (Lat/Lng), Price, Amenities.
- **Bookings**: ID, UserID, ListingID, StartTime, EndTime, Status.

## Verification Plan
### Automated Tests
- Build verification using `npm run build`.
- Lint checks using `npm run lint`.

### Manual Verification
- **User Flow**: Register as Host -> Create Listing -> Switch to Driver -> Search -> Book.
- **Responsive Check**: Verify UI on mobile and desktop breakpoints.
- **Visual Check**: Ensure premium aesthetics (gradients, glassmorphism).
