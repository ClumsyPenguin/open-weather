import { Component, OnInit } from '@angular/core';
import { TemperatureService } from './temperature.service';
import { CommonModule } from '@angular/common';
import { LocationService } from '../../services/location.service';

@Component({
  selector: 'app-temperature',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './temperature.component.html',
  styleUrls: ['./temperature.component.scss']
})
export class TemperatureComponent implements OnInit {
  temperatureValue?: number;
  errorMessage?: string;
  isLoading = true;

  constructor(private temperatureService: TemperatureService, private locationService: LocationService) {}

  ngOnInit(): void {
    this.fetchTemperature();
  }

  async fetchTemperature(): Promise<void> {
    const position = await this.locationService.getCurrentLocationAsync();
    this.temperatureService.getCurrentTemperature(position.longitude, position.latitude).subscribe({
      next: (response: number) => {
        console.log('Temperature Response:', response);
        this.temperatureValue = response;
        this.isLoading = false;
      },
      error: (error: Error) => {
        console.error('Error calling getCurrentTemperature:', error);
        this.errorMessage = error.message;
        this.isLoading = false;
      }
    });
  }
}
