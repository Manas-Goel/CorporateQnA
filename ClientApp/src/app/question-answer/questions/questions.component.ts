import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { UserQuestions } from 'src/app/Shared/Models/UserQuestions';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.scss'],
})
export class QuestionsComponent implements OnInit {
  @Input() questions: UserQuestions[];
  @Input() loading: boolean;
  @Output() selectedQuestion = new EventEmitter<number>();
  constructor() {}

  ngOnInit(): void {}

  changeSelectedQuestion(questionId) {
    this.selectedQuestion.emit(questionId);
  }
}
