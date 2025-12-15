# FindMyPark - Application Runbook & Walkthrough

**Version:** 1.0.0
**Date:** 2025-12-15

This runbook provides a comprehensive guide to the FindMyPark application, detailing every screen, workflow, and functionality with visual aids.

---

## Table of Contents
1. [Authentication](#authentication)
2. [Driver Workflow](#driver-workflow)
3. [Host Workflow](#host-workflow)
4. [Admin & Operations](#admin--operations)
5. [Support & Corporate](#support--corporate)

---

## 1. Authentication

### Login Page
The entry point for all users.
*(Visual: Login form with Email and Password fields)*

**Workflow:**
1. User enters **Email** and **Password**.
2. Click **Login**.
3. On success, user is redirected to the home page.
4. If not registered, click **Register** to create a new account.

---

## 2. Driver Workflow

### Home Page
The landing page after login, providing quick access to search.

### Search
Drivers can search for parking spots by location.

**Features:**
- **Map View:** Interactive map showing available spots.
- **List View:** Details of parking spots (Price, Features).
- **Filters:** Filter by EV Charging, Covered Parking, etc.

### Booking
Detailed view of a parking spot to confirm reservation.

**Workflow:**
1. View spot details (Amenities, Price).
2. Select **Start Time** and **End Time**.
3. (Optional) Add services like Car Wash.
4. Apply **Promo Code** (e.g., `SAVE50`).
5. Click **Pay & Reserve**.

### Wallet
Manage payment methods and viewing balance.

---

## 3. Host Workflow

### Host Dashboard (Earnings)
Overview of listings and revenue.

**Key Metrics:**
- Total Earnings
- Active Listings
- Occupancy Rate

### Add Listing
Form to onboard a new parking spot.

**Steps:**
1. Enter Title (e.g., "Downtown Garage").
2. Set Address and Location (Map Pin).
3. Define Price per Hour.
4. Toggle features (CCTV, EV Charger).
5. Submit for Admin Approval.

---

## 4. Admin & Operations

### Admin Dashboard
Central control tower for platform operations.

**Modules:**
- **Pending Approvals:** Review and approve new host listings.
- **Heatmap:** Visualize high-demand areas.
- **KYC Verification:** Verify host identities.

---

