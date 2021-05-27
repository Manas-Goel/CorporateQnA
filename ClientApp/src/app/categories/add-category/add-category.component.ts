import { CategoryService } from '../../Services/category.service';
import { Validators } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
})
export class AddCategoryComponent implements OnInit {
  modalReference: NgbModalRef;
  addCategoryForm: FormGroup;
  loading = false;
  @Output() categoryAdded = new EventEmitter<boolean>();
  constructor(
    private modalService: NgbModal,
    private categoryService: CategoryService
  ) {
    this.addCategoryForm = new FormGroup({
      Name: new FormControl('', Validators.required),
      Description: new FormControl('', Validators.required),
    });
  }

  get formControls() {
    return this.addCategoryForm.controls;
  }

  open(content) {
    this.modalReference = this.modalService.open(content, {
      ariaLabelledBy: 'modal-basic-title',
      size: 'lg',
    });
    this.modalReference.result.then(
      () => {
        this.addCategoryForm.reset();
      },
      () => {
        this.addCategoryForm.reset();
      }
    );
  }

  ngOnInit() {}

  onSubmit() {
    this.loading = true;
    this.categoryService.addCategory(this.addCategoryForm.value).subscribe(
      (data) => {
        this.loading = false;
        this.resetForm();
        this.categoryAdded.emit();
      },
      (error) => {
        this.loading = false;
        alert('Could not add category. Try Again.');
        this.resetForm();
      }
    );
  }

  resetForm() {
    this.addCategoryForm.reset();
    this.modalReference.close();
  }
}
