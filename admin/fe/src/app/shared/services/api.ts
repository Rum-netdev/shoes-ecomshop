import { HttpClient, HttpHeaders } from "@angular/common/http";
import { ChangeDetectorRef, Injectable } from "@angular/core";
import { finalize, Observable } from "rxjs";
import { environment } from "../../../environments/environment";

@Injectable({
	providedIn: 'root',
})
export class Api {
    parent?: {
        cdr: ChangeDetectorRef
    };

    http: HttpClient;

    constructor(http: HttpClient) {
        this.http = http;
    }

    req<T>(method: string, path: string, body: any = undefined, customHeaders: any = undefined): Observable<T> {
        const content = body ? JSON.stringify(body) : "";
        let headers = {
            'Content-Type': 'application/json',
        };

        if(customHeaders) {
            headers = { ...headers, ...customHeaders };
        }

        let options: any = {
            body: content,
            headers: new HttpHeaders(headers)
        };

        return this.http.request<T>(method, environment.apiUrl + path, options)
            .pipe(finalize(() => {
                setTimeout(() => {
                    if(this.parent)
                        this.parent.cdr.detectChanges();
                });
            })) as any;
    }

    post<T>(url: string, body: any, customHeaders: any = undefined): Observable<T> {
		return this.req<T>('post', url, body, customHeaders) as any;
	}

	paramsUrl(url: string, params: any) {
		if (params != null) {
			const values: any[] = Object.values(params);
			if (values.length > 0) {
				url += '?' + Object.keys(params).map((x, i) => {
					// if (values[i] instanceof Date) {
					// 	return x + '=' + (values[i] as Date).toStringWithFormat(serverDate);
					// }
					if (values[i] instanceof Array) {
						return (values[i] as any[]).map(v => x + '=' + encodeURIComponent(v)).join('&')
					}
					if (!values[i] && values[i] != 0 && values[i] != false) {
						values[i] = null;
					}
					return x + '=' + encodeURIComponent(values[i] + "");
				}).join('&');
			}
		}

		return url;
	}

	getWithParams<T>(url: string, params: any = null, customHeaders: any = undefined): Observable<T> {
		url = this.paramsUrl(url, params);
		return this.req<T>('get', url, undefined, customHeaders) as any;
	}


	postWithParams<T>(url: string, params: any = null,body: any = undefined, customHeaders: any = undefined): Observable<T> {
		url = this.paramsUrl(url, params);
		return this.req<T>('post', url, body, customHeaders) as any;
	}
}