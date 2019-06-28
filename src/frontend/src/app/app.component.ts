import { Component, OnInit } from '@angular/core';
import { EnvConfigService } from './env-config.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'ToDoListApp';
  
  constructor(private envConfigService: EnvConfigService){
  }

  ngOnInit(): void {
    this.envConfigService.init();
  }
}
