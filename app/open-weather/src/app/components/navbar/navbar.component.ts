import { Component } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-navbar',
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent {
  currentTheme: string = 'light';

  toggleTheme(event: Event) {
    const checkbox = event.target as HTMLInputElement;
    if (checkbox.checked) {
      this.currentTheme = 'dark';
      document.documentElement.setAttribute('data-theme', 'dark');
    } else {
      this.currentTheme = 'light';
      document.documentElement.setAttribute('data-theme', 'light');
    }
  }
}
