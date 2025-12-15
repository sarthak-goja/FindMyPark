
# Validation Software Installation Qualification (VSIQ)
**Project**: FindMyPark
**Version**: 1.0

## 1. Objective
To validate that the installed software functions according to the specifications defined in the FRS.

## 2. Test Cases (Summary)
| ID | Feature | Test Description | Expected Result | Pass/Fail |
| :--- | :--- | :--- | :--- | :--- |
| **VSIQ-01** | Auth | Login with invalid credentials | Error "Invalid Password" | Pass |
| **VSIQ-02** | Search | Load Map page | Pins displayed for New Delhi | Pass |
| **VSIQ-03** | Booking | Complete a booking flow | Booking ID generated | Pass |
| **VSIQ-04** | API | GET `/api/Listings` | JSON Array of spots | Pass |

## 3. Defect Log
- **Defect 1**: Black map rendering (Fixed in Build 1.0.1 via Token Update).
- **Defect 2**: CORS Error on Port 5037 (Fixed in Build 1.0.2 via Port Standardization).

## 4. Conclusion
The software Build 1.0.2 PASSED critical validation steps.
