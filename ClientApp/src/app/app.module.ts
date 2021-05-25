import { AddCategoryComponent } from './categories/add-category/add-category.component';
import { AddQuestionComponent } from './home/search-question/add-question/add-question.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgbModalModule, NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { SideNavComponent } from './side-nav/side-nav.component';
import { MainNavComponent } from './main-nav/main-nav.component';
import { SearchQuestionComponent } from './home/search-question/search-question.component';
import { QuestionsComponent } from './question-answer/questions/questions.component';
import { AnswersComponent } from './question-answer/answers/answers.component';
import { SignupComponent } from './signup/signup.component';
import { NgxEditorModule } from 'ngx-editor';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { CategoriesComponent } from './categories/categories.component';
import { UsersComponent } from './users/users.component';
import { UserDetailsComponent } from './users/user-details/user-details.component';
import { QuestionAnswerComponent } from './question-answer/question-answer.component';
import { MomentModule } from 'ngx-moment';
import { QuestionComponent } from './question-answer/questions/question/question.component';
import { AnswerComponent } from './question-answer/answers/answer/answer.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    SideNavComponent,
    MainNavComponent,
    SearchQuestionComponent,
    QuestionsComponent,
    AnswersComponent,
    AddQuestionComponent,
    HomeComponent,
    SignupComponent,
    LoginComponent,
    AddCategoryComponent,
    CategoriesComponent,
    UsersComponent,
    UserDetailsComponent,
    QuestionAnswerComponent,
    QuestionComponent,
    AnswerComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    HttpClientModule,
    MomentModule,
    NgbModalModule,
    NgbDropdownModule,
    NgxEditorModule.forRoot({
      locals: {
        // menu
        bold: 'Bold',
        italic: 'Italic',
        code: 'Code',
        blockquote: 'Blockquote',
        underline: 'Underline',
        strike: 'Strike',
        bullet_list: 'Bullet List',
        ordered_list: 'Ordered List',
        heading: 'Heading',
        h1: 'Header 1',
        h2: 'Header 2',
        h3: 'Header 3',
        h4: 'Header 4',
        h5: 'Header 5',
        h6: 'Header 6',
        align_left: 'Left Align',
        align_center: 'Center Align',
        align_right: 'Right Align',
        align_justify: 'Justify',
        text_color: 'Text Color',
        background_color: 'Background Color',

        // popups, forms, others...
        url: 'URL',
        text: 'Text',
        openInNewTab: 'Open in new tab',
        insert: 'Insert',
        altText: 'Alt Text',
        title: 'Title',
        remove: 'Remove',
      },
    }),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
