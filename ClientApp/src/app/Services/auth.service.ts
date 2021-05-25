import { Router } from '@angular/router';
import { environment } from './../../environments/environment';
import { User } from './../Shared/Models/User';
import { FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient, private router: Router) {}

  isAuthenticated() {
    return localStorage.getItem('CorporateQnA') != null;
  }

  authenticateUser(user: User) {
    localStorage.setItem('CorporateQnA', JSON.stringify(user));
  }

  getUserInfo() {
    return JSON.parse(localStorage.getItem('CorporateQnA'));
  }

  signupUser(signupForm: FormGroup) {
    return this.http.post<User>(
      `${environment.URL}/authentication/signup`,
      signupForm.value
    );
  }

  loginUser(loginForm: FormGroup) {
    return this.http.post<User>(
      `${environment.URL}/authentication/login`,
      loginForm.value
    );
  }

  logout() {
    localStorage.removeItem('CorporateQnA');
    this.router.navigate(['/login']);
  }
}
