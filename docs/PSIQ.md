
# Project Software Installation Qualification (PSIQ)
**Project**: FindMyPark
**Version**: 1.0

## 1. Objective
To verify that the *FindMyPark* application is correctly installed and configured in the target environment.

## 2. Environment Prerequisites
| Component | Requirement | Verified (Y/N) |
| :--- | :--- | :--- |
| **OS** | Windows 10/11 or Ubuntu 20.04+ | Y |
| **Runtime** | Docker Desktop 4.0+ | Y |
| **Ports** | 8080, 4200 (or 80) available | Y |
| **Network** | Internet Access for API/Mapbox | Y |

## 3. Installation Steps
1.  **Clone Repository**
    - Command: `git clone https://github.com/sarthak-goja/FindMyPark.git`
    - Expected Result: `FindMyPark` directory created.

2.  **Configuration Check**
    - Navigate to `FindMyPark.UI/src/environments`.
    - Verification: `environment.ts` contains valid API URL (`http://localhost:5175`).

3.  **Deployment**
    - Command: `docker-compose up -d --build`
    - Expected Logs: `Container findmypark-api Started`, `Container findmypark-ui Started`.

## 4. Verification Check
- [ ] Navigate to `http://localhost:4200` -> Login Page Loads.
- [ ] Navigate to `http://localhost:5175/weatherforecast` -> JSON Response.
