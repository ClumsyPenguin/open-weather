import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TemperatureComponent } from "./components/temperature/temperature.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TemperatureComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'open-weather';
}
