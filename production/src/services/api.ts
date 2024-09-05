import { HttpClient } from "@angular/common/http";
import { ChangeDetectorRef, Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class Api {
    parent?: {
        cdr: ChangeDetectorRef
    }

    http: HttpClient

    req<T>(method: string, path: string, body: any = undefined, customHeaders: any = undefined): Observable<T> {
        const content = body ? JSON.stringify(body) : "";
        let headers = {
            'Content-Type': 'application/json'
        };

        if(customHeaders)
            headers = { ...headers, ...customHeaders }
    }
}