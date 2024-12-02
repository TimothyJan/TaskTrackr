import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { UserListComponent } from './components/user-list/user-list.component';
import { UserModalComponent } from './components/user-list/user-modal/user-modal.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ProjectListComponent } from './components/project-list/project-list.component';
import { ProjectTaskComponent } from './components/project-list/project-task/project-task.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProjectTaskModalComponent } from './components/project-list/project-task-modal/project-task-modal.component';
import { ProjectComponent } from './components/project-list/project/project.component';
import { ProjectModalComponent } from './components/project-list/project-modal/project-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    UserListComponent,
    UserModalComponent,
    ProjectListComponent,
    ProjectTaskComponent,
    ProjectTaskModalComponent,
    ProjectComponent,
    ProjectModalComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule,
    CommonModule,
    FormsModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
