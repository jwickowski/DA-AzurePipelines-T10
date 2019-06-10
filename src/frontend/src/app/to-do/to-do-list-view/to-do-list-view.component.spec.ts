import { ToDoListViewComponent } from './to-do-list-view.component';
import { ToDoItemsDataSourceService } from '../to-do-items-data-source.service';
import { of } from 'rxjs';
import { Spy } from 'jasmine-core';
import { ToDoItem } from './to-do-item.model';

describe('ToDoListViewComponent', () => {
  let component: ToDoListViewComponent;
  let toDoItemsDataSourceService: ToDoItemsDataSourceService;

  beforeEach(() => {
    toDoItemsDataSourceService = jasmine.createSpyObj('ToDoItemsDataSourceService',
      ['getToDoItems$', 'setAsDone', 'addItem']);
    component = new ToDoListViewComponent(toDoItemsDataSourceService)
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('should get items on init', () => {
    let result$ = of([]);
    (<Spy>toDoItemsDataSourceService.getToDoItems$).and.returnValue(result$);
    component.ngOnInit();
    expect(result$).toBe(component.toDoItems$);
  });

  it('should add item on addNewItem', () => {
    let newItemname = 'newItemname';
    component.newToDoItem = newItemname;
    component.addNewItem();
    expect(toDoItemsDataSourceService.addItem).toHaveBeenCalledWith(newItemname);
  });

  it('should set as done item on setAdDone', () => {
    let newItemname:ToDoItem = {id: '2', name: "foo"};
    component.setAsDone(newItemname);
    expect(toDoItemsDataSourceService.setAsDone).toHaveBeenCalledWith(newItemname);
  })
});
