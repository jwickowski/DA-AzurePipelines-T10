import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReplaySubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class EnvConfigService {
    public apiUrl$:  ReplaySubject<string> = new ReplaySubject<string>(1);
    constructor(private httpClient: HttpClient) {

    }
    public init() {
        this.httpClient.get("env.json").subscribe((x: any) => {
            console.log(x);
            this.apiUrl$.next(x.apiUrl);
        });
    }


}
