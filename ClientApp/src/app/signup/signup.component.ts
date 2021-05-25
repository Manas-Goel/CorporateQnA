import { AuthService } from './../Services/auth.service';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { User } from '../Shared/Models/User';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
})
export class SignupComponent implements OnInit {
  signUpForm: FormGroup;
  loading = false;

  constructor(
    private cd: ChangeDetectorRef,
    private authService: AuthService,
    private router: Router
  ) {
    this.signUpForm = new FormGroup({
      Name: new FormControl('', Validators.required),
      Email: new FormControl('', [Validators.required, Validators.email]),
      Password: new FormControl('', [
        Validators.required,
        Validators.minLength(6),
      ]),
      JobProfile: new FormControl('', Validators.required),
      Department: new FormControl('', Validators.required),
      Location: new FormControl('', Validators.required),
      ProfileImage: new FormControl('', Validators.required),
      ProfileImageUrl: new FormControl(''),
    });
  }

  get formControls() {
    return this.signUpForm.controls;
  }

  onImageChange(event: any) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      const [image] = event.target.files;
      reader.readAsDataURL(image);
      if (image.size / 1024 > 1000) {
        alert('Image size should be less than 1000 kb');
        this.signUpForm.patchValue({ ProfileImage: '' });
        return;
      }

      reader.onload = () => {
        let imageData = reader.result as string;
        this.signUpForm.patchValue({
          ProfileImageUrl: imageData,
        });
      };
      this.cd.markForCheck();
    }
  }

  ngOnInit() {}

  onSubmit() {
    this.loading = true;
    this.authService.signupUser(this.signUpForm).subscribe(
      (data) => {
        this.loading = false;
        this.authService.authenticateUser(data);
        this.router.navigate(['']);
      },
      (error) => {
        console.log(error);
        this.loading = false;
        alert(error.error.message[0].description);
        this.signUpForm.reset();
      }
    );
  }
}
