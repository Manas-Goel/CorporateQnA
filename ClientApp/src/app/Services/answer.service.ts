import { QuestionAnswers } from './../Shared/Models/QuestionAnswers';
import { RateAnswer } from './../Shared/Models/RateAnswer';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Answer } from '../Shared/Models/Answer';

@Injectable({
  providedIn: 'root',
})
export class AnswerService {
  constructor(private http: HttpClient) {}

  getAllAnswersByQuestionId(questionId: number) {
    return this.http.get<QuestionAnswers[]>(
      `${environment.URL}/api/answers/all?questionId=${questionId}`
    );
  }

  getAnswerById(answerId: number) {
    return this.http.get<Answer>(`${environment.URL}/api/answers/${answerId}`);
  }

  addAnswer(answer: Answer) {
    return this.http.post<Answer>(`${environment.URL}/api/answers`, answer);
  }

  getAnswerRating(answerId: number, userId: string) {
    return this.http.get<RateAnswer>(
      `${environment.URL}/api/answers/getratings?answerId=${answerId}&userId=${userId}`
    );
  }

  giveAnswerRating(
    answerId: number,
    userId: string,
    liked: boolean,
    disliked: boolean
  ) {
    return this.http.get<RateAnswer>(
      `${environment.URL}/api/answers/rate?answerId=${answerId}&userId=${userId}&liked=${liked}&disliked=${disliked}`
    );
  }

  markBestAnswer(answerId: number, questionId: number, isBest: boolean) {
    return this.http.get(
      `${environment.URL}/api/answers/markbest?answerId=${answerId}&questionId=${questionId}&isBest=${isBest}`
    );
  }
}
