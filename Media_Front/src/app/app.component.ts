import { HttpErrorResponse } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ReservationApi } from './core/services/reservation-api';
import { Reservation } from './domains/reservations/models/reservation';
import { CreateReservation } from './domains/reservations/models/create-reservation';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: false
})
export class AppComponent implements OnInit {
  title = 'Sistema de Reservas';
  reservationService = inject(ReservationApi)
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

  ngOnInit() {
    this.reservationService.getReservations().subscribe(r => this.reservations = r);
  }

  handleError(error: HttpErrorResponse) {
    const errors = error.error.errors;
    this.nameInputErrors = errors.name;
    this.deviceInputErrors = errors.device;
    this.classroomInputErrors = errors.classroom;
    this.dateInputErrors = errors.date;
    this.startTimeInputErrors = errors.startTime;
    this.endTimeInputErrors = errors.endTime;
  }

  public submit() {
    const formData = this.reservationForm.value;
    const startDate = new Date(`${formData.date}T${formData.startTime}`).toISOString();
    const endDate = new Date(`${formData.date}T${formData.endTime}`).toISOString();

    const data: CreateReservation = {
      name: formData.name!,
      classroom: formData.classroom!,
      device: formData.device!,
      startDate,
      endDate
    };

    this.reservationService.createReservation(data).subscribe({
      next: (r: CreateReservation) => console.log(r),
      error: (err: HttpErrorResponse) => this.handleError(err),
      complete: () => {
        this.reservationService.getReservations().subscribe(r => this.reservations = r);
        this.reservationForm.reset();
      }
    });
  }
}
