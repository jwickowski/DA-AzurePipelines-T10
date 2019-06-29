import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class EnvConfigService {
    private isConfigReadySubject$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    private config: EnvConfig;

    constructor(private httpClient: HttpClient) {

    }

    public init() {
        this.getData()
    }

    private getData() {
        this.httpClient.get("env.json").subscribe((data: EnvConfig) => {
            this.parseBooleans(data);
            this.config = data;
            this.isConfigReadySubject$.next(true);
        });
    }

    private parseBooleans(data: EnvConfig) {
        data.isRealStorageEnabled = this.parseBoolean(data.isRealStorageEnabled)
    }

    private parseBoolean(dataItem: any): boolean {
        if (dataItem === true) {
            return true;
        }

        if(typeof dataItem == "string")
        {
            if (dataItem.toLowerCase() === "true")
            {
                return true;
            }
        }


        return false;
    }

    public getConfig(): EnvConfig {
        return this.config;
    }

    public isConfigReady$(): Observable<boolean> {
        return this.isConfigReadySubject$.asObservable();
    }
}

export interface EnvConfig {
    apiUrl: string;
    isRealStorageEnabled: boolean
}
