import { Rateuser } from './../Shared/Models/RateUser';
import { environment } from './../../environments/environment';
import { User } from './../Shared/Models/User';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

  searchUsers(keyword: string) {
    return this.http.get<User[]>(
      `${environment.URL}/api/users/search?keyword=${keyword}`
    );
  }

  getUserById(id: string) {
    return this.http.get<User>(`${environment.URL}/api/users/${id}`);
  }

  getUserRating(user1, user2) {
    return this.http.get<Rateuser>(
      `${environment.URL}/api/users/getratings?userRated=${user1}&ratingUser=${user2}`
    );
  }

  giveUserRating(user1, user2, liked, disliked) {
    return this.http.get<Rateuser>(
      `${environment.URL}/api/users/rate?userRated=${user1}&ratingUser=${user2}&liked=${liked}&disliked=${disliked}`
    );
  }
}
