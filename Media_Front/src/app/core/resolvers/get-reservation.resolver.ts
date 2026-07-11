import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { Reservation } from '../../domains/reservations/models/reservation';
import { ReservationApi } from '../services/reservation-api';

export const getReservationResolver: ResolveFn<Reservation[]> = (route, state) => {
  const reservationApi = inject(ReservationApi);
  let reservations: Reservation[] = [];
  reservationApi.getReservations().subscribe(r => reservations = r);

  return reservations;
};
