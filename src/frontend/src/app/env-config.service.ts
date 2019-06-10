import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class EnvConfigService {
    public apiUrl: string;
    constructor(private httpClient: HttpClient) {

    }
    public init() {
        this.httpClient.get("env.json").subscribe((x: any) => {
            console.log(x);
            this.apiUrl = x.apiUrl;
        });
    }


}
