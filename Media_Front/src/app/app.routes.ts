import { Routes } from '@angular/router';
import { getReservationResolver } from './core/resolvers/get-reservation.resolver';

export const routes: Routes = [
    {
        path: '',
        loadComponent: () => import('./domains/reservations/reservation-page').then(m => m.ReservationPage),
        resolve: {
            reservations: getReservationResolver
        }
    }
]