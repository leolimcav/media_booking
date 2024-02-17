import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
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

  form = this.formBuilder.group({
    name: '',
    device: '',
    classroom: '',
    date: '',
    startTime: '',
    endTime: ''
  });

  constructor(
    private formBuilder: FormBuilder,
    private httpClient: HttpClient
  ) {}

  ngOnInit() {
    this.getReservations().subscribe(r => this.reservations = r);
  }

  createReservation(): void {
    const formData = this.form.value;
    const startDate = new Date(`${formData.date}T${formData.startTime}:00`).toUTCString();
    const endDate = new Date(`${formData.date}T${formData.endTime}:00`).toUTCString();

    console.log(startDate, endDate);

    const createReservation = { ...formData, startDate, endDate }
    console.log(createReservation);
    this.httpClient.post<CreateReservation>("https://localhost:3001/api/reservations", createReservation)
      .subscribe(
        (r: CreateReservation) => console.log(r),
        (err: HttpErrorResponse) => this.handleError(err),
        () => console.log("Request completed"));
    this.form.reset();
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

  getReservations() : Observable<Reservation[]> {
    return this.httpClient.get<GetReservationResponse[]>("https://localhost:3001/api/reservations")
      .pipe(
        map(r => {
          const reservations : Reservation[] = r.map(rs => {
            const [ startDate, _ ] = rs.startDate.split('T');

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
