import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { CreateReservation } from '../models/create-reservation';
import { GetReservation } from '../models/get-reservation';
import { Reservation } from '../models/reservation';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
  private readonly apiUrl = environment.apiBaseUrl;

  constructor(private httpClient: HttpClient) { }

  public getReservations(): Observable<Reservation[]> {
    return this.httpClient.get<GetReservation[]>(`${this.apiUrl}/reservations`)
      .pipe(
        map(r => {
          const reservations: Reservation[] = r.map(rs => {
            const [startDate, _] = rs.startDate.split('T');

            return { ...rs, date: startDate, startTime: rs.startDate, endTime: rs.endDate }
          });

          console.log(reservations);

          return reservations;
        })
      );
  }

  createReservation(data: CreateReservation): Observable<CreateReservation> {
    return this.httpClient.post<CreateReservation>(`${this.apiUrl}/reservations`, data);
  }
}
