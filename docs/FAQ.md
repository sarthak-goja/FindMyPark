
# Frequently Asked Questions (FAQ)
**Project**: FindMyPark
**Version**: 1.0

## 1. General Questions

**Q: What is FindMyPark?**
A: FindMyPark is a smart parking management platform that connects drivers looking for parking spots with hosts (individuals or businesses) who have available parking space.

**Q: Is the app available on mobile?**
A: Yes, the web application is fully responsive and supports PWA (Progressive Web App) features, allowing you to install it on your mobile home screen.

**Q: Which cities are supported?**
A: Currently, we are live in New Delhi and Gurugram (NCR). We plan to expand to other major cities soon.

## 2. For Drivers

**Q: How do I book a parking spot?**
A:
1.  Log in to the app.
2.  Go to "Find Parking" and search for your location.
3.  Click on a green marker to view details.
4.  Select "Book Now", choose your time, and pay via Wallet.

**Q: Can I cancel a booking?**
A: Yes, you can cancel a booking from the "My Bookings" page. Refunds are processed to your wallet instantly if cancelled 1 hour before the start time.

**Q: How does the "Surge Pricing" work?**
A: During high-demand periods (like events or rush hours), prices may increase slightly (e.g., 1.5x). This helps ensure availability turnover. You will always see the total price *before* you confirm.

**Q: What if I reach the spot and it's occupied?**
A: Please use the "Support" section to raise a ticket immediately. We will refund your amount and penalize the host.

## 3. For Hosts

**Q: How do I list my parking spot?**
A:
1.  Go to "Host Dashboard" -> "Add Listing".
2.  Fill in the details (Address, Price/Hour, Photos).
3.  Submit for Admin Verification. Once approved, your spot goes live.

**Q: When do I get paid?**
A: Earnings are credited to your in-app Wallet immediately after a booking is completed. You can request a payout to your bank account weekly.

**Q: What is "KYC" and why is it needed?**
A: KYC (Know Your Customer) helps us verify your identity using Aadhaar/PAN. This creates a safe and trusted platform for all users. You cannot list a spot without verified KYC.

**Q: Can I block my spot for personal use?**
A: Yes, you can toggle your listing status to "Inactive" from the dashboard at any time.

## 4. Technical & Troubleshooting

**Q: I'm getting a "Location Access Denied" error.**
A: Please ensure you have allowed Location permissions in your browser settings. The map relies on GPS to find spots near you.

**Q: I registered but didn't receive an OTP.**
A: In the current test environment, we use a mock OTP (usually `123456`) or it is auto-filled. In production, check your SMS/Email spam folder.

**Q: How do I change my language to Hindi?**
A: Click the "EN/HI" toggle switch in the top navigation bar.

**Q: Who do I contact for support?**
A: You can raise a ticket via the "Support" link in the sidebar or email us at `support@findmypark.com`.
