import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ToDoItem } from './to-do-list-view/to-do-item.model';

@Injectable({
  providedIn: 'root'
})
export abstract class ToDoItemsDataSourceService {
  public abstract getToDoItems$(): Observable<ToDoItem[]>;

  public abstract addItem(itemName: string): void;

  public abstract setAsDone(toDoItem: ToDoItem) : void;
}
