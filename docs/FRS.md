
# Functional Requirement Specification (FRS)
**Project**: FindMyPark
**Version**: 1.0
**Date**: 2025-12-15

## 1. Introduction
The Functional Requirement Specification (FRS) defines the specific behaviors and functions that the FindMyPark system supports.

## 2. Authentication Module
- **FRS-01**: The system shall allow Users (Drivers/Hosts) to register with Name, Email, and Password.
- **FRS-02**: The system shall allow Users to login using valid credentials.
- **FRS-03**: The system shall validate passwords (min 6 chars, alphanumeric compatibility).
- **FRS-04**: The system shall support Role-Based Access Control (RBAC) for Admin, Host, and Driver.

## 3. Search & Booking Module
- **FRS-05**: The system shall display an interactive map (Mapbox) showing available parking spots.
- **FRS-06**: The system shall filter results by Location, Price, and Amenities (EV, Covered).
- **FRS-07**: The system must calculate dynamic prices based on duration and "Surge" logic.
- **FRS-08**: The system shall allow Drivers to book a spot for a specific time range.
- **FRS-09**: The system shall prevent double-booking of the same spot for overlapping times.

## 4. Wallet & Payments
- **FRS-10**: The system shall maintain a virtual wallet for each user.
- **FRS-11**: The system shall deduct booking amounts from the Driver's wallet.
- **FRS-12**: The system shall credit 90% of the booking fee to the Host's wallet (Platform Fee: 10%).

## 5. Host Management
- **FRS-13**: Hosts shall be able to add new listings with Photos, Address, and Features.
- **FRS-14**: Hosts shall view a dashboard of active bookings and total earnings.

## 6. Admin Module
- **FRS-15**: Admins shall approve or reject new listings.
- **FRS-16**: Admins shall view a heatmap of search demand.
- **FRS-17**: Admins shall manage KYC verifications.

## 7. System Interfaces
- **FRS-18**: The system shall expose a REST API over HTTP/HTTPS.
- **FRS-19**: The system shall use SQLite for data persistence in the current environment.
