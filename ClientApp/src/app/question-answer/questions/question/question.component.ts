import { UserQuestions } from './../../../Shared/Models/UserQuestions';
import { AuthService } from './../../../Services/auth.service';
import { QuestionService } from './../../../Services/question.service';
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { faChevronUp, faEye } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.scss'],
})
export class QuestionComponent implements OnInit {
  faChevronUp = faChevronUp;
  faEye = faEye;
  @Input() question: UserQuestions;
  @Output() changeQuestion = new EventEmitter<number>();

  isUpvoted = false;
  canUpvote = false;
  userId: string;

  constructor(
    private questionService: QuestionService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.userId = this.authService.getUserInfo().id;
    this.canUpvote = this.question.userId !== this.userId;
    this.getUpvoteInfo();
  }

  changeSelectedQuestion() {
    this.changeQuestion.emit(this.question.id);
    this.questionService
      .increaseQuestionViews(this.question.id, this.userId)
      .subscribe(() => {
        this.updateQuestion();
      });
  }

  getUpvoteInfo() {
    this.questionService
      .getUpvoteInfo(this.question.id, this.userId)
      .subscribe((upvoteInfo) => {
        if (upvoteInfo == null) {
          this.isUpvoted = false;
        } else {
          this.isUpvoted = upvoteInfo.upvote;
        }
      });
  }
  changeUpvoteStatus() {
    if (this.canUpvote) {
      this.isUpvoted = !this.isUpvoted;
      this.questionService
        .upvoteQuestion(this.question.id, this.userId, this.isUpvoted)
        .subscribe(() => {
          this.updateQuestion();
        });
    }
  }
  updateQuestion() {
    this.questionService
      .getQuestionById(this.question.id)
      .subscribe((question) => {
        this.question.views = question.views;
        this.question.upVotes = question.upVotes;
      });
  }
}
