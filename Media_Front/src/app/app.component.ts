import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

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

  reservations: Reservation[] = [];

  form = this.formBuilder.group({
    name: '',
    device: '',
    classroom: '',
    date: ''
  });

  constructor(
    private formBuilder: FormBuilder,
    private httpClient: HttpClient
  ) {}

  ngOnInit() {
    this.getReservations().subscribe(r => this.reservations = r);
  }

  createReservation(): void {
    const payload = this.form.value;
    console.log(payload);
    this.httpClient.post<CreateReservation>("https://localhost:3001/api/reservations", payload).subscribe(
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
  }

  getReservations() : Observable<Reservation[]> {
    return this.httpClient.get<Reservation[]>("https://localhost:3001/api/reservations");
  }
}

interface CreateReservation {
  name: string;
  device: string;
  classroom: string;
  date: string;
}

interface Reservation {
  id: number;
  name: string;
  device: string;
  classroom: string;
  date: string;
}
