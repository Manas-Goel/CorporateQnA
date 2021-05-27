import { UserQuestions } from './../../../Shared/Models/UserQuestions';
import { QuestionAnswers } from './../../../Shared/Models/QuestionAnswers';
import { QuestionService } from './../../../Services/question.service';
import { AnswerService } from './../../../Services/answer.service';
import { AuthService } from './../../../Services/auth.service';
import { Component, Input, OnInit } from '@angular/core';
import { faThumbsDown, faThumbsUp } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-answer',
  templateUrl: './answer.component.html',
})
export class AnswerComponent implements OnInit {
  @Input() answer: QuestionAnswers;
  @Input() questionUserId;
  @Input() question: UserQuestions;

  faThumbsUp = faThumbsUp;
  faThumbsDown = faThumbsDown;
  activeUserId: string;
  liked: boolean = false;
  disliked: boolean = false;
  canRate: boolean;
  canMarkBestAnswer = false;
  isBestAnswer: boolean;

  constructor(
    private authService: AuthService,
    private answerService: AnswerService,
    private questionService: QuestionService
  ) {}

  ngOnInit(): void {
    this.activeUserId = this.authService.getUserInfo().id;
    this.canMarkBestAnswer =
      this.activeUserId === this.questionUserId &&
      this.answer.userId !== this.questionUserId;
    this.canRate = this.answer.userId !== this.activeUserId;
    this.isBestAnswer = this.answer.isBestSolution;
    this.getAnswerRating();
  }

  getAnswerRating() {
    this.answerService
      .getAnswerRating(this.answer.id, this.activeUserId)
      .subscribe((rating) => {
        if (rating != null) {
          this.liked = rating.liked;
          this.disliked = rating.disliked;
        }
      });
  }

  loadAnswer() {
    this.answerService.getAnswerById(this.answer.id).subscribe((answer) => {
      if (answer != null) {
        this.answer.likes = answer.likes;
        this.answer.dislikes = answer.dislikes;
        this.answer.isBestSolution = answer.isBestSolution;
      }
    });
  }

  rateAnswer(liked, disliked) {
    if (this.canRate) {
      if (liked) {
        this.liked = !this.liked;
        this.disliked = false;
      } else if (disliked) {
        this.disliked = !this.disliked;
        this.liked = false;
      }
      this.answerService
        .giveAnswerRating(
          this.answer.id,
          this.activeUserId,
          this.liked,
          this.disliked
        )
        .subscribe(() => {
          this.loadAnswer();
        });
    }
  }
  markBestAnswer(event) {
    this.isBestAnswer = event.target.checked;
    this.answerService
      .markBestAnswer(this.answer.id, this.answer.questionId, this.isBestAnswer)
      .subscribe(() => {
        this.updateQuestionResolved();
      });
  }

  updateQuestionResolved() {
    this.questionService
      .getQuestionById(this.question.id)
      .subscribe((question) => {
        this.question.isResolved = question.isResolved;
      });
  }
}
