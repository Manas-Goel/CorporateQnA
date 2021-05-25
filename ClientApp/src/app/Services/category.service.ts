import { environment } from './../../environments/environment';
import { Category } from './../Shared/Models/Category';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private http: HttpClient) {}

  getCategories() {
    return this.http.get<Category[]>(`${environment.URL}/api/categories`);
  }

  addCategory(category: Category) {
    return this.http.post<Category>(
      `${environment.URL}/api/categories`,
      category
    );
  }

  searchCategories(keyword: string, sortBy: number) {
    return this.http.get<Category[]>(
      `${environment.URL}/api/categories/search?keyword=${keyword}&sortBy=${sortBy}`
    );
  }
}
