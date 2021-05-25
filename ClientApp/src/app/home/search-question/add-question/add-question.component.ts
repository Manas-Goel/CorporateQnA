import { AuthService } from '../../../Services/auth.service';
import { QuestionService } from '../../../Services/question.service';
import { CategoryService } from '../../../Services/category.service';
import { Validators } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Editor, Toolbar, toHTML } from 'ngx-editor';

@Component({
  selector: 'app-add-question',
  templateUrl: './add-question.component.html',
  styleUrls: ['./add-question.component.scss'],
})
export class AddQuestionComponent implements OnInit {
  @Output() questionAdded = new EventEmitter();

  modalReference: NgbModalRef;
  addQuestionForm: FormGroup;
  loading = false;
  categories = [];
  editor: Editor;
  toolbar: Toolbar = [
    [{ heading: ['h1', 'h2', 'h3', 'h4', 'h5', 'h6'] }],
    ['bold', 'italic', 'underline'],
    ['ordered_list', 'bullet_list'],
    ['blockquote', 'link'],
  ];
  constructor(
    private modalService: NgbModal,
    private categoryService: CategoryService,
    private questionService: QuestionService,
    private authService: AuthService
  ) {
    this.addQuestionForm = new FormGroup({
      Title: new FormControl('', Validators.required),
      Description: new FormControl('', Validators.required),
      CategoryId: new FormControl('', Validators.required),
    });
  }

  get formControls() {
    return this.addQuestionForm.controls;
  }

  open(content) {
    this.modalReference = this.modalService.open(content, {
      ariaLabelledBy: 'modal-basic-title',
      size: 'lg',
    });
    this.modalReference.result.then(
      () => {
        this.resetForm();
      },
      () => {
        this.resetForm();
      }
    );
    this.editor = new Editor({});
    this.loadCategories();
  }

  ngOnInit() {
    this.loadCategories();
  }

  onSubmit() {
    this.loading = true;
    let userId = this.authService.getUserInfo().id;
    this.questionService
      .addQuestion({ ...this.addQuestionForm.value, userId })
      .subscribe(
        (data) => {
          this.loading = false;
          this.resetForm();
          this.questionAdded.emit();
        },
        (error) => {
          this.loading = false;
          alert('Could not add question. Try Again.');
          this.resetForm();
        }
      );
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe((categories) => {
      this.categories = categories;
    });
  }

  resetForm() {
    this.addQuestionForm.reset();
    this.modalReference.close();
    this.editor.destroy();
  }
}
