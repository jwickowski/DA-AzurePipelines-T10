import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoListViewComponent } from './to-do-list-view/to-do-list-view.component';
import { ToDoItemsDataSourceService } from './to-do-items-data-source.service';
import { FormsModule }  from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { ToDoItemsDataSourceServiceFactory } from './to-do-items-data-source.service.factory';
import { EnvConfigService } from '../env-config.service';

@NgModule({
  declarations: [
    ToDoListViewComponent
  ],
  imports: [
    CommonModule,
    FormsModule,  
    BrowserModule,
    HttpClientModule
  ],
  exports: [
    ToDoListViewComponent
  ],
  providers: [
    {
      provide: ToDoItemsDataSourceService, 
      useFactory: ToDoItemsDataSourceServiceFactory,
    deps: [EnvConfigService, HttpClient]}
  ]
})
export class ToDoModule { }
