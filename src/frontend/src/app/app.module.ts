import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ToDoModule } from './to-do/to-do.module';
import { EnvConfigService } from './env-config.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ToDoModule
  ],
  providers: [EnvConfigService],
  bootstrap: [AppComponent]
})
export class AppModule { }
