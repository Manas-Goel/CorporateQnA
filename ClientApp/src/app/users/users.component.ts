import { Router } from '@angular/router';
import { UserService } from './../Services/user.service';
import { User } from './../Shared/Models/User';
import {
  faSearch,
  faThumbsDown,
  faThumbsUp,
} from '@fortawesome/free-solid-svg-icons';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
})
export class UsersComponent implements OnInit {
  faSearch = faSearch;
  faThumbsUp = faThumbsUp;
  faThumbsDown = faThumbsDown;
  keyword: string = '';
  loading = false;
  users: User[] = [];

  constructor(private userService: UserService, private router: Router) {}

  ngOnInit(): void {
    this.updateUsers();
  }

  onSubmit(event) {
    this.keyword = event.target.value;
    this.updateUsers();
  }

  updateUsers() {
    this.loading = true;
    this.userService.searchUsers(this.keyword).subscribe(
      (users) => {
        this.loading = false;
        this.users = users;
      },
      () => {
        this.loading = false;
      }
    );
  }

  loadUserDetails(userId) {
    this.router.navigate(['users', userId]);
  }
}
