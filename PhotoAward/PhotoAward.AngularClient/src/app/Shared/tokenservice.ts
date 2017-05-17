import { ITokenService } from './tokenservice';

import 'rxjs/add/observable/fromPromise';
import 'rxjs/add/observable/of';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/mergeMap';
import 'rxjs/add/operator/catch';

import { Observable } from 'rxjs/Observable';
import { Injectable, Inject, Optional, OpaqueToken } from '@angular/core';
import { Http, Headers, ResponseContentType, Response } from '@angular/http';

export class BearerToken  {
  access_token:string;
  token_type:string;
  expires_in: string;

  static fromJS(data: any): BearerToken {
        const result = new BearerToken();
        result.init(data);
        return result;
    }

    init(data?: any) {
        if (data) {
            this.access_token = data["access_token"];
            this.token_type = data["token_type"];
            this.expires_in = data["expires_in"];
        }
    }
}

export const API_BASE_URL = new OpaqueToken('API_BASE_URL');

export interface ITokenService {

    login(email: string, password: string): Observable<BearerToken | null>;
}

@Injectable()
export class TokenService implements ITokenService {
    private http: Http;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(Http) http: Http, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl ? baseUrl : "";
    }


    login(email: string, password: string): Observable<BearerToken | null> {
        const url_ = this.baseUrl + "/token";
        if (email === undefined){
            throw new Error("The parameter 'email' must be defined.");
        }
        if (password === undefined){
            throw new Error("The parameter 'password' must be defined.");
        }

        const content_ = "username=" + email +"&password=" + password +"&grant_type=password"     ;

        const options_ = {
            body: content_,
            method: "post",
            headers: new Headers({
                "Content-Type": "application/x-www-form-urlencoded",
                "Accept": "application/json; charset=UTF-8"
            })
        };

        return this.http.request(url_, options_).flatMap((response_) => {
            return this.processLogin(response_);
        }).catch((response_: any) => {
            if (response_ instanceof Response) {
                try {
                    return this.processLogin(response_);
                } catch (e) {
                    return <Observable<BearerToken>><any>Observable.throw(e);
                }
            } else {
                return <Observable<BearerToken>><any>Observable.throw(response_);
            }
        });
    }

    protected processLogin(response: Response): Observable<BearerToken | null> {
        const status = response.status;
                if (status === 200) {
            const responseText = response.text();
            let result200: BearerToken | null = null;
            const resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            result200 = resultData200 ? BearerToken.fromJS(resultData200) : <any>null;
            return Observable.of(result200);
        } else if (status !== 200 && status !== 204) {
            const responseText = response.text();
            return throwException("An unexpected server error occurred.", status, responseText);
        }
        return Observable.of<BearerToken | null>(<any>null);
    }


}

function throwException(message: string, status: number, response: string, result?: any): Observable<any> {
    return Observable.throw(new SwaggerException(message, status, response, result));
}

export class SwaggerException extends Error {
    message: string;
    status: number;
    response: string;
    result: any;

    constructor(message: string, status: number, response: string, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.result = result;
    }
}
