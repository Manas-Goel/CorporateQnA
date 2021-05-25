import { AuthService } from './Services/auth.service';
import { Component, DoCheck } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements DoCheck {
  title = 'CorporateQnA';
  isAuthenticated = false;

  constructor(private authService: AuthService) {}

  ngDoCheck() {
    this.isAuthenticated = this.authService.isAuthenticated();
  }
}
