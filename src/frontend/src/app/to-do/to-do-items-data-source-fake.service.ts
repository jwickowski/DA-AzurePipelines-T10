import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { ToDoItem } from './to-do-list-view/to-do-item.model';
import { ToDoItemsDataSourceService } from './to-do-items-data-source.service';


@Injectable({
  providedIn: 'root'
})
export class ToDoItemsDataSourceFakeService implements ToDoItemsDataSourceService {

  private items: BehaviorSubject<ToDoItem[]> = new BehaviorSubject<ToDoItem[]>([]);

  constructor() {
    var tempIndex = 1;
    this.addItem("temp item number: " + tempIndex++);
    setInterval(() => {
      this.addItem("temp item number: " + tempIndex++);
    }, 2500);
  }

  public getToDoItems$(): Observable<ToDoItem[]> {
    return this.items.asObservable();
  }

  public addItem(itemName: string) {
    var newArray: ToDoItem[] = this.items.value.slice(0);
    newArray.push(<ToDoItem>{ id: '', name: itemName });
    this.items.next(newArray);
  }

  public setAsDone(toDoItem: ToDoItem) {
    var newArray: ToDoItem[] = this.items.value.slice(0);
    var index = newArray.indexOf(toDoItem);
    if (index === -1) {
      return;
    }

    newArray.splice(index, 1);
    this.items.next(newArray);
  }

}
