import { Component, OnInit } from '@angular/core';
import { createChannel, createClient } from 'nice-grpc-web';
import {
  GetTemperatureQuery,
  TemperatureServiceClient,
  TemperatureServiceDefinition
} from './temperature';

@Component({
  selector: 'app-temperature',
  imports: [],
  templateUrl: './temperature.component.html',
  styleUrl: './temperature.component.scss'
})
export class TemperatureComponent implements OnInit {
  channel = createChannel('https://localhost:7249');

  client: TemperatureServiceClient = createClient(
    TemperatureServiceDefinition,
    this.channel
  );

  temperatureValue?: number;

  constructor() {}

  async ngOnInit(): Promise<void> {
    try {
      const response = await this.client.getTemperature(
        GetTemperatureQuery.create({})
      );
      console.log('Temperature Response:', response);
      this.temperatureValue = response.temperature;
    } catch (error) {
      console.error('Error calling getTemperature:', error);
    }
  }
}
