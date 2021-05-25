import { UserQuestions } from './../Shared/Models/UserQuestions';
import { Upvote } from './../Shared/Models/Upvote';
import { Question } from './../Shared/Models/Question';
import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class QuestionService {
  constructor(private http: HttpClient) {}

  addQuestion(question: Question) {
    return this.http.post<Question>(
      `${environment.URL}/api/questions`,
      question
    );
  }

  getQuestionsByUserId(userId: string) {
    return this.http.get<Question[]>(
      `${environment.URL}/api/questions/user?userId=${userId}`
    );
  }

  getQuestionById(questionId: number) {
    return this.http.get<Question>(
      `${environment.URL}/api/questions/${questionId}`
    );
  }

  increaseQuestionViews(questionId: number, userId: string) {
    return this.http.get(
      `${environment.URL}/api/questions/view?questionId=${questionId}&userId=${userId}`
    );
  }

  getUpvoteInfo(questionId: number, userId: string) {
    return this.http.get<Upvote>(
      `${environment.URL}/api/questions/getupvoteinfo?userId=${userId}&questionId=${questionId}`
    );
  }

  upvoteQuestion(questionId: number, userId: string, isUpvoted: boolean) {
    return this.http.get(
      `${environment.URL}/api/questions/upvotequestion?userId=${userId}&questionId=${questionId}&upvote=${isUpvoted}`
    );
  }

  searchQuestion(
    keyword: string,
    categoryId: number,
    searchCriteria: number,
    searchTime: number,
    userId: string
  ) {
    return this.http.get<UserQuestions[]>(
      `${environment.URL}/api/questions/search?keyword=${keyword}&categoryId=${categoryId}&searchCriteria=${searchCriteria}&searchTime=${searchTime}&userId=${userId}`
    );
  }
}
