import { QuestionService } from './../../Services/question.service';
import { Question } from './../../Shared/Models/Question';
import { AuthService } from './../../Services/auth.service';
import { UserService } from './../../Services/user.service';
import { User } from './../../Shared/Models/User';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import {
  faArrowLeft,
  faThumbsDown,
  faThumbsUp,
} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
})
export class UserDetailsComponent implements OnInit {
  faArrowLeft = faArrowLeft;
  faThumbsUp = faThumbsUp;
  faThumbsDown = faThumbsDown;
  loading = false;
  user: User;
  allQuestionSelected = false;
  questionsLoading = false;

  activeUserId: string;
  selectedUserId: string;
  liked: boolean = false;
  disliked: boolean = false;
  canRate: boolean;

  questions: Question[] = [];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private userService: UserService,
    private authService: AuthService,
    private questionService: QuestionService
  ) {}

  ngOnInit(): void {
    this.activeUserId = this.authService.getUserInfo().id;
    this.route.params.subscribe((params) => {
      this.selectedUserId = params['id'];
      this.canRate = this.selectedUserId !== this.activeUserId;
      this.getUserRating();
      this.loading = true;
      this.loadUserDetails();
      this.loadQuestions(true);
    });
  }

  backToUsers() {
    this.router.navigate(['/users']);
  }

  rateUser(liked, disliked) {
    if (this.canRate) {
      if (liked) {
        this.liked = !this.liked;
        this.disliked = false;
      } else if (disliked) {
        this.disliked = !this.disliked;
        this.liked = false;
      }
      this.userService
        .giveUserRating(
          this.selectedUserId,
          this.activeUserId,
          this.liked,
          this.disliked
        )
        .subscribe(() => {
          this.loadUserDetails();
        });
    }
  }

  getUserRating() {
    this.userService
      .getUserRating(this.selectedUserId, this.activeUserId)
      .subscribe((rating) => {
        if (rating != null) {
          this.liked = rating.liked;
          this.disliked = rating.disliked;
        }
      });
  }

  loadUserDetails() {
    this.userService.getUserById(this.selectedUserId).subscribe(
      (user) => {
        this.loading = false;
        this.user = user;
      },
      (error) => {
        this.loading = false;
        alert('Cannot find user with given id');
        this.backToUsers();
      }
    );
  }

  loadQuestions(allSelected: boolean) {
    if (allSelected != this.allQuestionSelected) {
      this.questionsLoading = true;
      if (allSelected) {
        this.questionService
          .getQuestionsByUserId(this.selectedUserId)
          .subscribe(
            (questions) => {
              this.questionsLoading = false;
              this.questions = questions;
            },
            () => (this.questionsLoading = false)
          );
      } else {
        this.questionService
          .searchQuestion('', 0, 2, 0, this.selectedUserId)
          .subscribe(
            (questions: any[]) => {
              this.questionsLoading = false;
              this.questions = questions;
            },
            () => {
              this.questionsLoading = false;
            }
          );
      }
    }
    this.allQuestionSelected = allSelected;
  }
}
