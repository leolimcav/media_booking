import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { getReservationResolver } from './get-reservation.resolver';
import { Reservation } from 'src/app/domains/reservations/models/reservation';

describe('getReservationResolver', () => {
  const executeResolver: ResolveFn<Reservation[]> = (...resolverParameters) =>
    TestBed.runInInjectionContext(() => getReservationResolver(...resolverParameters));
  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
