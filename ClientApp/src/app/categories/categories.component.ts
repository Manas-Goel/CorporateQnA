import { FormGroup, FormControl } from '@angular/forms';
import { CategoryService } from './../Services/category.service';
import { Category } from './../Shared/Models/Category';
import { faRedo, faSearch } from '@fortawesome/free-solid-svg-icons';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss'],
})
export class CategoriesComponent implements OnInit {
  faSearch = faSearch;
  faRedo = faRedo;
  categories: Category[] = [];
  sortOptions;
  loading = false;

  searchCategoryForm: FormGroup;

  constructor(private categoryService: CategoryService) {
    this.sortOptions = [
      {
        id: 1,
        name: 'Popular',
      },
    ];

    this.searchCategoryForm = new FormGroup({
      keyword: new FormControl(''),
      sortBy: new FormControl(this.sortOptions[0].name),
      sortById: new FormControl(this.sortOptions[0].id),
    });
  }

  ngOnInit(): void {
    this.updateCategories();
  }

  updateCategories() {
    this.loading = true;
    this.categoryService.getCategories().subscribe(
      (categories) => {
        this.categories = categories;
        this.loading = false;
      },
      () => {
        this.loading = false;
      }
    );
  }

  changeSortOption(sortOption) {
    this.searchCategoryForm.patchValue({
      sortBy: sortOption.name,
      sortById: sortOption.id,
    });
    this.searchCategories();
  }
  resetFields() {
    this.searchCategoryForm.patchValue({
      keyword: '',
      sortBy: this.sortOptions[0].name,
      sortById: this.sortOptions[0].id,
    });
    this.searchCategories();
  }

  searchCategories() {
    this.loading = true;
    this.categoryService
      .searchCategories(
        this.searchCategoryForm.value.keyword,
        this.searchCategoryForm.value.sortById
      )
      .subscribe(
        (categories) => {
          this.categories = categories;
          this.loading = false;
        },
        () => {
          alert('Error searching categories');
          this.loading = false;
        }
      );
  }

  onSubmit() {
    this.searchCategories();
  }
}
