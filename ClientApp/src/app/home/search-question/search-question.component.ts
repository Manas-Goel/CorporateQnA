import { AuthService } from './../../Services/auth.service';
import { FormGroup, FormControl } from '@angular/forms';
import { Category } from './../../Shared/Models/Category';
import { CategoryService } from './../../Services/category.service';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { faRedo, faSearch } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-search-question',
  templateUrl: './search-question.component.html',
  styleUrls: ['./search-question.component.scss'],
})
export class SearchQuestionComponent implements OnInit {
  faRedo = faRedo;
  faSearch = faSearch;
  categories: Category[] = [];
  shows;
  sortBy;

  searchQuestionForm: FormGroup;
  @Output() search = new EventEmitter<Object>();

  constructor(
    private categoryService: CategoryService,
    private authService: AuthService
  ) {
    this.searchQuestionForm = new FormGroup({
      keyword: new FormControl(''),
      categoryName: new FormControl('All'),
      categoryId: new FormControl(0),
      searchCriteriaName: new FormControl('All'),
      searchCriteria: new FormControl(0),
      searchTimeName: new FormControl('All'),
      searchTime: new FormControl(0),
    });
    this.shows = [
      { id: 0, name: 'All' },
      { id: 1, name: 'My Questions' },
      { id: 2, name: 'My Participation' },
      { id: 3, name: 'Solved' },
      { id: 4, name: 'Unsolved' },
    ];
    this.sortBy = [
      { id: 0, name: 'All' },
      { id: 10, name: 'Last 10 Days' },
      { id: 30, name: 'Last 30 Days' },
    ];
  }

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe((categories) => {
      this.categories = categories;
      this.categories.unshift({
        id: 0,
        name: 'All',
        description: '',
        questionsTagged: 0,
      });
    });
    this.searchQuestion();
  }

  changeCategory(category: Category) {
    this.searchQuestionForm.patchValue({
      categoryName: category.name,
      categoryId: category.id,
    });
    this.searchQuestion();
  }

  changeShow(show) {
    this.searchQuestionForm.patchValue({
      searchCriteriaName: show.name,
      searchCriteria: show.id,
    });
    this.searchQuestion();
  }

  changeSortBy(sortBy) {
    this.searchQuestionForm.patchValue({
      searchTimeName: sortBy.name,
      searchTime: sortBy.id,
    });
    this.searchQuestion();
  }

  searchQuestion() {
    let { keyword, categoryId, searchCriteria, searchTime } =
      this.searchQuestionForm.value;
    let userId = this.authService.getUserInfo().id;
    this.search.emit({
      keyword,
      categoryId,
      searchCriteria,
      searchTime,
      userId,
    });
  }

  resetFields() {
    this.searchQuestionForm.patchValue({
      keyword: '',
      categoryName: 'All',
      categoryId: 0,
      searchCriteriaName: 'All',
      searchCriteria: 0,
      searchTimeName: 'All',
      searchTime: 0,
    });
    this.searchQuestion();
  }
}
