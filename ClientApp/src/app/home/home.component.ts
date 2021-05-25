import { UserQuestions } from './../Shared/Models/UserQuestions';
import { QuestionService } from './../Services/question.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  questions: UserQuestions[] = [];
  loading = false;

  constructor(private questionService: QuestionService) {}

  ngOnInit() {}

  loadQuestions(event) {
    this.loading = true;
    let { keyword, categoryId, searchCriteria, searchTime, userId } = event;
    this.questionService
      .searchQuestion(keyword, categoryId, searchCriteria, searchTime, userId)
      .subscribe(
        (questions: UserQuestions[]) => {
          this.loading = false;
          this.questions = questions;
        },
        () => {
          this.loading = false;
          alert('Error searching questions');
        }
      );
  }
}
