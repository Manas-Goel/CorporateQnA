import { Component, DoCheck, OnInit } from '@angular/core';
import { faSun } from '@fortawesome/free-regular-svg-icons';
import { User } from '../Shared/Models/User';
import { AuthService } from './../Services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements DoCheck {
  faSun = faSun;
  isAuthenticated: boolean;
  userInfo: User;
  date: Date;

  constructor(private authService: AuthService) {
    this.date = new Date();
  }

  ngDoCheck() {
    this.isAuthenticated = this.authService.isAuthenticated();
    if (this.isAuthenticated) {
      this.userInfo = this.authService.getUserInfo();
    } else {
      this.userInfo = null;
    }
  }

  logout() {
    this.authService.logout();
  }
}
