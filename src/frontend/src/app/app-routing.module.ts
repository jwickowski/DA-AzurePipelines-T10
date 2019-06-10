import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ToDoListViewComponent } from './to-do/to-do-list-view/to-do-list-view.component';

const routes: Routes = [
  { path: '', component: ToDoListViewComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

