import { Component, OnInit } from '@angular/core';
import { EnvConfigService } from './env-config.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'ToDoListApp';
  isAppReady$: Observable<boolean>;
  constructor(private envConfigService: EnvConfigService){
  }

  ngOnInit(): void {
    this.envConfigService.init();
    this.isAppReady$ = this.envConfigService.isConfigReady$();
  }
}
