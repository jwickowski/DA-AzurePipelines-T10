import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReplaySubject, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class EnvConfigService {
    private apiUrlSubject$:  ReplaySubject<string> = new ReplaySubject<string>(1);
    constructor(private httpClient: HttpClient) {

    }
    public init() {
        this.httpClient.get("env.json").subscribe((x: any) => {
            console.log(x.apiUrl);
            this.apiUrlSubject$.next(x.apiUrl);
        });
    }

    public getApiUrl$(): Observable<string>{
        return this.apiUrlSubject$.asObservable();
    }


}
