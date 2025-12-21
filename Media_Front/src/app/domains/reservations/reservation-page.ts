import { DatePipe, NgOptimizedImage } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, inject, input, linkedSignal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ReservationApi } from 'src/app/core/services/reservation-api';
import { CreateReservation } from './models/create-reservation';
import { Reservation } from './models/reservation';

@Component({
  selector: 'app-reservation-page',
  imports: [ReactiveFormsModule, DatePipe, NgOptimizedImage],
  templateUrl: './reservation-page.html',
})
export class ReservationPage {
  reservationService = inject(ReservationApi)
  route = inject(ActivatedRoute);
  nameInputErrors: string[] = [];
  deviceInputErrors: string[] = [];
  classroomInputErrors: string[] = [];
  dateInputErrors: string[] = [];
  startTimeInputErrors: string[] = [];
  endTimeInputErrors: string[] = [];

  data = toSignal(this.route.data);
  reservationsInput = input.required<Reservation[]>();
  reservations = linkedSignal(() => this.reservationsInput() ?? []);

  reservationForm = new FormGroup({
    name: new FormControl('', Validators.required),
    device: new FormControl('', Validators.required),
    classroom: new FormControl('', Validators.required),
    date: new FormControl('', Validators.required),
    startTime: new FormControl('', Validators.required),
    endTime: new FormControl('', Validators.required)
  });

  private handleError(error: HttpErrorResponse) {
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
        this.reservationService.getReservations().subscribe(r => this.reservations.update(() => r));
        this.reservationForm.reset();
      }
    });
  }
}
