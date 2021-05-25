import { UserQuestions } from './../Shared/Models/UserQuestions';
import { Component, DoCheck, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-question-answer',
  templateUrl: './question-answer.component.html',
  styleUrls: ['./question-answer.component.scss'],
})
export class QuestionAnswerComponent implements OnInit, DoCheck {
  @Input() questionsList: UserQuestions[];
  @Input() loading: boolean;

  selectedQuestion: UserQuestions = null;
  constructor() {}

  ngOnInit(): void {}

  ngDoCheck() {
    if (this.loading) {
      this.selectedQuestion = null;
    }
  }

  changeSelectedQuestion(questionId) {
    this.selectedQuestion = this.questionsList.find(
      (question) => questionId === question.id
    );
  }
}
