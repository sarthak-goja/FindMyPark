import { Routes } from '@angular/router';
import { MainLayoutComponent } from './components/layout/main-layout/main-layout.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/auth/login/login.component';
import { RegisterComponent } from './pages/auth/register/register.component';
import { AddListingComponent } from './pages/host/add-listing/add-listing.component';
import { MyListingsComponent } from './pages/host/my-listings/my-listings.component';
import { SearchComponent } from './pages/search/search.component';
import { BookingPageComponent } from './pages/booking/booking-page/booking-page.component';
import { MyBookingsComponent } from './pages/booking/my-bookings/my-bookings.component';
import { HostDashboardComponent } from './pages/dashboard/host-dashboard/host-dashboard.component';
import { AdminDashboardComponent } from './pages/dashboard/admin-dashboard/admin-dashboard.component';
import { WalletComponent } from './pages/wallet/wallet.component';
import { FindMyCarComponent } from './pages/find-my-car/find-my-car.component';
import { RewardsComponent } from './pages/rewards/rewards.component';
import { MyVehiclesComponent } from './pages/vehicles/my-vehicles/my-vehicles.component';
import { KycUploadComponent } from './pages/kyc/kyc-upload/kyc-upload.component';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    {
        path: '',
        component: MainLayoutComponent,
        canActivate: [authGuard],
        children: [
            { path: '', component: HomeComponent },

            // Host Routes
            { path: 'add-listing', component: AddListingComponent },
            { path: 'my-listings', component: MyListingsComponent },
            { path: 'earnings', component: HostDashboardComponent },

            // Driver Routes
            { path: 'search', component: SearchComponent },
            { path: 'book', component: BookingPageComponent },
            { path: 'bookings', component: MyBookingsComponent },
            { path: 'wallet', component: WalletComponent },
            { path: 'find-my-car', component: FindMyCarComponent },
            { path: 'rewards', component: RewardsComponent },
            { path: 'vehicles', component: MyVehiclesComponent },
            { path: 'kyc-upload', component: KycUploadComponent },
            { path: 'corporate', loadComponent: () => import('./pages/corporate/fleet-dashboard/fleet-dashboard.component').then(m => m.FleetDashboardComponent) },

            // Support Routes
            { path: 'support/dashboard', loadComponent: () => import('./pages/support/support-dashboard/support-dashboard.component').then(m => m.SupportDashboardComponent) },
            { path: 'support/create', loadComponent: () => import('./pages/support/create-ticket/create-ticket.component').then(m => m.CreateTicketComponent) },

            // Admin Routes
            { path: 'admin', component: AdminDashboardComponent },
            { path: 'developer', loadComponent: () => import('./pages/dashboard/developer-dashboard/developer-dashboard.component').then(m => m.DeveloperDashboardComponent) }
        ]
    }
];
