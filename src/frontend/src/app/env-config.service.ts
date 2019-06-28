import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class EnvConfigService {
    private isConfigReady$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    private config: EnvConfig;

    constructor(private httpClient: HttpClient) {

    }

    public init() {
        this.httpClient.get("env.json").subscribe((data: EnvConfig) => {
            this.config = data;
            this.isConfigReady$.next(true);
        });
    }

    public getConfig(): EnvConfig {
        return this.config;
    }
}

export interface EnvConfig {
    apiUrl: string;
    isRealStorageEnabled: boolean
}
