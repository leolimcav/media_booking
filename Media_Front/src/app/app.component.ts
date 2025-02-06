import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable, map } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Sistema de Reservas';
  nameInputErrors: string[] = [];
  deviceInputErrors: string[] = [];
  classroomInputErrors: string[] = [];
  dateInputErrors: string[] = [];
  startTimeInputErrors: string[] = [];
  endTimeInputErrors: string[] = [];

  reservations: Reservation[] = [];

  reservationForm = new FormGroup({
    name: new FormControl('', Validators.required),
    device: new FormControl('', Validators.required),
    classroom: new FormControl('', Validators.required),
    date: new FormControl('', Validators.required),
    startTime: new FormControl('', Validators.required),
    endTime: new FormControl('', Validators.required)
  });

  constructor(
    private httpClient: HttpClient
  ) { }

  ngOnInit() {
    this.getReservations().subscribe(r => this.reservations = r);
  }

  createReservation(): void {
    const formData = this.reservationForm.value;
    const startDate = new Date(`${formData.date}T${formData.startTime}`).toISOString();
    const endDate = new Date(`${formData.date}T${formData.endTime}`).toISOString();

    console.log(startDate, endDate);

    const createReservation = { ...formData, startDate, endDate }
    console.log(createReservation);
    this.httpClient.post<CreateReservation>("https://localhost:3001/api/reservations", createReservation)
      .subscribe({
        next: (r: CreateReservation) => console.log(r),
        error: (err: HttpErrorResponse) => this.handleError(err),
        complete: () => {
          console.log("Request completed");
          this.getReservations().subscribe(r => this.reservations = r);
        }
      });
    this.reservationForm.reset();
  }

  handleError(error: HttpErrorResponse) {
    console.log("erro aconteceu: ", error.error.errors);

    const errors = error.error.errors;
    this.nameInputErrors = errors.name;
    this.deviceInputErrors = errors.device;
    this.classroomInputErrors = errors.classroom;
    this.dateInputErrors = errors.date;
    this.startTimeInputErrors = errors.startTime;
    this.endTimeInputErrors = errors.endTime;
  }

  getReservations(): Observable<Reservation[]> {
    return this.httpClient.get<GetReservationResponse[]>("https://localhost:3001/api/reservations")
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
}

interface CreateReservation {
  name: string;
  device: string;
  classroom: string;
  startDate: string;
  endDate: string;
}

interface GetReservationResponse {
  id: number;
  name: string;
  device: string;
  classroom: string;
  startDate: string;
  endDate: string;
}

interface Reservation {
  id: number;
  name: string;
  device: string;
  classroom: string;
  date: string;
  startTime: string;
  endTime: string;
}
