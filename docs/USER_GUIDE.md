# FindMyPark - User Guide 🚗

Welcome to **FindMyPark**, the smartest parking solution for cities.

## 🚀 Getting Started

### Accessing the App
- **Web**: Open [localhost:4200](http://localhost:4200) in your browser.
- **Mobile**: Add to Home Screen to install the PWA.

### Roles
1. **Driver**: Search and book spots.
2. **Host**: List empty parking spaces.
3. **Fleet Manager**: Manage corporate vehicles.
4. **Admin**: Platform oversight.

---

## 🅿️ For Drivers

### finding a Spot
1.  **Search**: Use the Map or "Find My Car" GPS button.
2.  **Filter**: Select **EV**, **Covered**, or **24/7** spots.
3.  **Busy Times**: Check the **AI Forecast Graph** on the booking page to avoid crowds.

### Booking & Payment
1.  **Reserve**: Select hours and click "Pay & Reserve".
2.  **Payment**: Use Wallet, UPI, or **FASTag** (auto-deducted).
3.  **Navigate**: Click "Get Directions" on the confirmation screen.

---

## 🏠 For Hosts

### Listing a Spot
1.  Go to **Host Dashboard** > **Add Listing**.
2.  Set location (Pin on Map) and Hourly Price.
3.  **Pro Tip**: Enable "Smart Pricing" to let AI adjust rates during events.

---

## 🏢 Corporate Fleets
1.  Login as **Fleet Manager**.
2.  Go to `Menu > Business`.
3.  **Bulk Booking**: Select multiple vehicles and book a zone instantly.

---

## 🛠️ For Developers (Admin)

### God Mode (Developer Panel)
- Access at `/developer`.
- **System Health**: Check DB status and Active User count.
- **Global Config**:
    - **Maintenance Mode**: Stop all new bookings.
    - **Max Bookings**: Limit users to `N` active bookings.
- **Reference**: API Documentation is available at `/swagger/index.html`.
