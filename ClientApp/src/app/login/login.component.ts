import { Router } from '@angular/router';
import { AuthService } from './../Services/auth.service';
import { Validators } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  error = false;
  loading = false;

  constructor(private authService: AuthService, private router: Router) {
    this.loginForm = new FormGroup({
      Email: new FormControl('', [Validators.required, Validators.email]),
      Password: new FormControl('', Validators.required),
    });
  }

  get formControls() {
    return this.loginForm.controls;
  }

  ngOnInit() {}

  onSubmit() {
    this.loading = true;
    this.authService.loginUser(this.loginForm).subscribe(
      (data) => {
        this.error = false;
        this.authService.authenticateUser(data);
        this.loading = false;
        this.router.navigate(['']);
      },
      (error) => {
        this.error = true;
        this.loading = false;
      }
    );
  }
}
