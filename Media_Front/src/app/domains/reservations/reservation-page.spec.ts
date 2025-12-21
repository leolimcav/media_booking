import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationPage } from './reservation-page';

describe('ReservationPageComponent', () => {
  let component: ReservationPage;
  let fixture: ComponentFixture<ReservationPage>;
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReservationPage]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ReservationPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
