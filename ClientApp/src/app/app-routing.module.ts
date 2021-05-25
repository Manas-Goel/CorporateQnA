import { UserDetailsComponent } from './users/user-details/user-details.component';
import { UsersComponent } from './users/users.component';
import { CategoriesComponent } from './categories/categories.component';
import { AuthGuard } from './auth.guard';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'home',
  },
  {
    path: 'categories',
    pathMatch: 'full',
    component: CategoriesComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'users',
    pathMatch: 'full',
    component: UsersComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'users/:id',
    component: UserDetailsComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'home',
    pathMatch: 'full',
    component: HomeComponent,
    canActivate: [AuthGuard],
  },
  { path: 'signup', component: SignupComponent },
  {
    path: 'login',
    component: LoginComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
