// temperature.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment'; // Adjust the path as necessary

@Injectable({
  providedIn: 'root'
})
export class TemperatureService {
  private apiUrl = `${environment.apiUrl}/temperature/current-temperature`;

  constructor(private http: HttpClient) {}

  getCurrentTemperature(longitude: number, latitude: number): Observable<number> {
    return this.http.get<number>(this.apiUrl + `?long=${longitude}&lat=${latitude}`).pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An unknown error occurred!';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `A client-side error occurred: ${error.error.message}`;
    } else {
      errorMessage = `Backend returned code ${error.status}, body was: ${JSON.stringify(error.error)}`;
    }
    console.error(errorMessage);
    return throwError(() => new Error(errorMessage));
  }
}
