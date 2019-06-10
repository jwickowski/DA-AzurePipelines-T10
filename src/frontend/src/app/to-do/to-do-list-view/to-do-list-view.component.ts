import { Component, OnInit } from '@angular/core';
import { ToDoItemsDataSourceService } from '../to-do-items-data-source.service';
import { Observable } from 'rxjs';
import { ToDoItem } from './to-do-item.model';

@Component({
  selector: 'app-to-do-list-view',
  templateUrl: './to-do-list-view.component.html'
})
export class ToDoListViewComponent implements OnInit {
  constructor(public toDoItemsDataSourceService: ToDoItemsDataSourceService) { }
  public toDoItems$: Observable<ToDoItem[]>;
  
  public newToDoItem: string;

  ngOnInit() {
    this.toDoItems$ = this.toDoItemsDataSourceService.getToDoItems$();
  }

  public setAsDone(toDoItem: ToDoItem) {
    this.toDoItemsDataSourceService.setAsDone(toDoItem);
  }

  public onKey(vaue: string) {
    this.newToDoItem = vaue;
  }

  public addNewItem(){
    if(this.newToDoItem){
      this.toDoItemsDataSourceService.addItem(this.newToDoItem);
      this.newToDoItem = "";
    }
  }
}
