import { Injectable, inject } from '@angular/core';
import { Router, CanActivateFn } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
    const authService = inject(AuthService);
    const router = inject(Router);

    // Check for token in localStorage
    if (typeof window !== 'undefined' && localStorage.getItem('token')) {
        return true;
    }

    // Allow access to login/register without redirection loop
    if (state.url.includes('/login') || state.url.includes('/register')) {
        return true;
    }

    return router.createUrlTree(['/login']);
};
