import { UserQuestions } from './../../Shared/Models/UserQuestions';
import { QuestionAnswers } from './../../Shared/Models/QuestionAnswers';
import { Answer } from './../../Shared/Models/Answer';
import { AuthService } from './../../Services/auth.service';
import { AnswerService } from './../../Services/answer.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, Input, OnDestroy, OnInit, DoCheck } from '@angular/core';
import {
  faExclamationCircle,
  faArrowLeft,
  faExpandAlt,
  faPaperPlane,
  faCompressAlt,
} from '@fortawesome/free-solid-svg-icons';
import { Editor, Toolbar } from 'ngx-editor';

@Component({
  selector: 'app-answers',
  templateUrl: './answers.component.html',
  styleUrls: ['./answers.component.scss'],
})
export class AnswersComponent implements OnInit, OnDestroy {
  faExclamationCircle = faExclamationCircle;
  faArrowLeft = faArrowLeft;
  faExpandAlt = faExpandAlt;
  faPaperPlane = faPaperPlane;
  faCompressAlt = faCompressAlt;

  addAnswerForm: FormGroup;
  expandAnswer = false;
  answers: QuestionAnswers[] = [];
  addingAnswer = false;

  editor: Editor;
  editor2: Editor;
  toolbar: Toolbar = [
    [{ heading: ['h1', 'h2', 'h3', 'h4', 'h5', 'h6'] }],
    ['bold', 'italic', 'underline'],
    ['ordered_list', 'bullet_list'],
    ['blockquote', 'link'],
  ];

  @Input() selectedQuestion: UserQuestions;

  constructor(
    private answerService: AnswerService,
    private authService: AuthService
  ) {
    this.addAnswerForm = new FormGroup({
      description: new FormControl('', Validators.required),
    });
  }

  ngOnChanges() {
    this.addAnswerForm.reset();
    this.loadAnswers();
  }

  ngOnInit() {
    this.editor = new Editor();
  }

  get formControls() {
    return this.addAnswerForm.controls;
  }

  expandAnswerForm(expand: boolean) {
    if (expand) {
      this.editor.destroy();
      this.editor2 = new Editor();
      this.editor2.setContent(this.addAnswerForm.value.description);
    } else {
      this.editor2.destroy();
      this.editor = new Editor();
      this.editor.setContent(this.addAnswerForm.value.description);
    }
    this.expandAnswer = expand;
  }

  loadAnswers() {
    if (this.selectedQuestion) {
      this.answerService
        .getAllAnswersByQuestionId(this.selectedQuestion.id)
        .subscribe((answers) => {
          this.answers = answers;
        });
    }
  }

  addAnswer() {
    if (!this.addAnswerForm.valid || this.addingAnswer) {
      return;
    }
    this.addingAnswer = true;
    let answer: Answer = {
      description: this.addAnswerForm.value.description,
      questionId: this.selectedQuestion.id,
      userId: this.authService.getUserInfo().id,
      likes: 0,
      dislikes: 0,
      id: 0,
      createdOn: new Date(),
      isBestSolution: false,
    };
    this.answerService.addAnswer(answer).subscribe(
      () => {
        this.addingAnswer = false;
        this.editor.setContent('');
        this.addAnswerForm.reset();
        this.loadAnswers();
        this.selectedQuestion.totalAnswers++;
        this.expandAnswerForm(false);
      },
      () => (this.addingAnswer = false)
    );
  }

  ngOnDestroy() {
    this.editor.destroy();
  }
}
