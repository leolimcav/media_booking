import { provideHttpClient } from '@angular/common/http';
import { bootstrapApplication, platformBrowser } from '@angular/platform-browser';
import { App } from './app/app';
import { provideZonelessChangeDetection } from '@angular/core';

bootstrapApplication(App, {
  providers: [provideHttpClient(), provideZonelessChangeDetection()]
})