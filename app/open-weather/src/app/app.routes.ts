import { Routes } from '@angular/router';
import { TemperatureComponent } from './components/temperature/temperature.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: TemperatureComponent },
  { path: '**', component: PageNotFoundComponent }
];
