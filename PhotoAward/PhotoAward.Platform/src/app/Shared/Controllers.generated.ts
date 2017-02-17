﻿/* tslint:disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v9.2.6249.30964 (NJsonSchema v8.1.6249.15627) (http://NSwag.org)
// </auto-generated>
//----------------------

import 'rxjs/Rx'; 
import {Observable} from 'rxjs/Observable';
import {Injectable, Inject, Optional, OpaqueToken} from '@angular/core';
import {Http, Headers, Response, RequestOptionsArgs} from '@angular/http';

export const API_BASE_URL = new OpaqueToken('API_BASE_URL');

export interface IMemberManagementClient {
    add(member: MemberDto): Observable<MemberDto>;
    get(email: string): Observable<MemberDto>;
}

@Injectable()
export class MemberManagementClient implements IMemberManagementClient {
    private http: Http = null; 
    private baseUrl: string = undefined; 
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(Http) http: Http, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http; 
        this.baseUrl = baseUrl ? baseUrl : ""; 
    }

    add(member: MemberDto): Observable<MemberDto> {
        let url_ = this.baseUrl + "/api/Member/Add";

        const content_ = JSON.stringify(member ? member.toJS() : null);
        
        let options_ = {
            body: content_,
            method: "post",
            headers: new Headers({
                "Content-Type": "application/json; charset=UTF-8", 
                "Accept": "application/json; charset=UTF-8"
            })
        };

        return this.http.request(url_, options_).map((response) => {
            return this.processAdd(response);
        }).catch((response: any, caught: any) => {
            if (response instanceof Response) {
                try {
                    return Observable.of(this.processAdd(response));
                } catch (e) {
                    return <Observable<MemberDto>><any>Observable.throw(e);
                }
            } else
                return <Observable<MemberDto>><any>Observable.throw(response);
        });
    }

    protected processAdd(response: Response): MemberDto {
        const responseText = response.text();
        const status = response.status; 

        if (status === 200) {
            let result200: MemberDto = null;
            let resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            result200 = resultData200 ? MemberDto.fromJS(resultData200) : null;
            return result200;
        } else if (status !== 200 && status !== 204) {
            this.throwException("An unexpected server error occurred.", status, responseText);
        }
        return null;
    }

    get(email: string): Observable<MemberDto> {
        let url_ = this.baseUrl + "/api/Member/Get/{email}";
        if (email === undefined || email === null)
            throw new Error("The parameter 'email' must be defined.");
        url_ = url_.replace("{email}", encodeURIComponent("" + email));

        const content_ = "";
        
        let options_ = {
            body: content_,
            method: "get",
            headers: new Headers({
                "Content-Type": "application/json; charset=UTF-8", 
                "Accept": "application/json; charset=UTF-8"
            })
        };

        return this.http.request(url_, options_).map((response) => {
            return this.processGet(response);
        }).catch((response: any, caught: any) => {
            if (response instanceof Response) {
                try {
                    return Observable.of(this.processGet(response));
                } catch (e) {
                    return <Observable<MemberDto>><any>Observable.throw(e);
                }
            } else
                return <Observable<MemberDto>><any>Observable.throw(response);
        });
    }

    protected processGet(response: Response): MemberDto {
        const responseText = response.text();
        const status = response.status; 

        if (status === 200) {
            let result200: MemberDto = null;
            let resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            result200 = resultData200 ? MemberDto.fromJS(resultData200) : null;
            return result200;
        } else if (status !== 200 && status !== 204) {
            this.throwException("An unexpected server error occurred.", status, responseText);
        }
        return null;
    }

    protected throwException(message: string, status: number, response: string, result?: any): any {
        throw new SwaggerException(message, status, response, result);
    }
}

export interface IPhotoManagementClient {
    add(uploadData: PhotoUploadData): Observable<PhotoManagementData>;
    delete(photoId: string): Observable<void>;
    get(id: string): Observable<PhotoManagementData>;
    getThumbnailsOfMember(email: string): Observable<PhotoManagementData[]>;
    getImagesOfMember(): Observable<PhotoMemberInfo[]>;
    getComments(photoId: string): Observable<CommentData[]>;
    addComment(uploadData: CommentUploadData): Observable<CommentData>;
    uploadPhoto(): Observable<any>;
}

@Injectable()
export class PhotoManagementClient implements IPhotoManagementClient {
    private http: Http = null; 
    private baseUrl: string = undefined; 
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(Http) http: Http, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http; 
        this.baseUrl = baseUrl ? baseUrl : ""; 
    }

    add(uploadData: PhotoUploadData): Observable<PhotoManagementData> {
        let url_ = this.baseUrl + "/api/Photo/Add";

        const content_ = JSON.stringify(uploadData ? uploadData.toJS() : null);
        
        let options_ = {
            body: content_,
            method: "post",
            headers: new Headers({
                "Content-Type": "application/json; charset=UTF-8", 
                "Accept": "application/json; charset=UTF-8"
            })
        };

        return this.http.request(url_, options_).map((response) => {
            return this.processAdd(response);
        }).catch((response: any, caught: any) => {
            if (response instanceof Response) {
                try {
                    return Observable.of(this.processAdd(response));
                } catch (e) {
                    return <Observable<PhotoManagementData>><any>Observable.throw(e);
                }
            } else
                return <Observable<PhotoManagementData>><any>Observable.throw(response);
        });
    }

    protected processAdd(response: Response): PhotoManagementData {
        const responseText = response.text();
        const status = response.status; 

        if (status === 200) {
            let result200: PhotoManagementData = null;
            let resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            result200 = resultData200 ? PhotoManagementData.fromJS(resultData200) : null;
            return result200;
        } else if (status !== 200 && status !== 204) {
            this.throwException("An unexpected server error occurred.", status, responseText);
        }
        return null;
    }

    delete(photoId: string): Observable<void> {
        let url_ = this.baseUrl + "/api/Photo/Delete?";
        if (photoId === undefined || photoId === null)
            throw new Error("The parameter 'photoId' must be defined and cannot be null.");
        else
            url_ += "photoId=" + encodeURIComponent("" + photoId) + "&";

        const content_ = "";
        
        let options_ = {
            body: content_,
            method: "post",
            headers: new Headers({
                "Content-Type": "application/json; charset=UTF-8", 
                "Accept": "application/json; charset=UTF-8"
            })
        };

        return this.http.request(url_, options_).map((response) => {
            return this.processDelete(response);
        }).catch((response: any, caught: any) => {
            if (response instanceof Response) {
                try {
                    return Observable.of(this.processDelete(response));
                } catch (e) {
                    return <Observable<void>><any>Observable.throw(e);
                }
            } else
                return <Observable<void>><any>Observable.throw(response);
        });
    }

    protected processDelete(response: Response): void {
        const responseText = response.text();
        const status = response.status; 

        if (status === 204) {
            return null;
        } else if (status !== 200 && status !== 204) {
            this.throwException("An unexpected server error occurred.", status, responseText);
        }
        return null;
    }

    get(id: string): Observable<PhotoManagementData> {
        let url_ = this.baseUrl + "/api/Photo/Get/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));

        const content_ = "";
        
        let options_ = {
            body: content_,
            method: "get",
            headers: new Headers({
                "Content-Type": "application/json; charset=UTF-8", 
                "Accept": "application/json; charset=UTF-8"
            })
        };

        return this.http.request(url_, options_).map((response) => {
            return this.processGet(response);
        }).catch((response: any, caught: any) => {
            if (response instanceof Response) {
                try {
                    return Observable.of(this.processGet(response));
                } catch (e) {
                    return <Observable<PhotoManagementData>><any>Observable.throw(e);
                }
            } else
                return <Observable<PhotoManagementData>><any>Observable.throw(response);
        });
    }

    protected processGet(response: Response): PhotoManagementData {
        const responseText = response.text();
        const status = response.status; 

        if (status === 200) {
            let result200: PhotoManagementData = null;
            let resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            result200 = resultData200 ? PhotoManagementData.fromJS(resultData200) : null;
            return result200;
        } else if (status !== 200 && status !== 204) {
            this.throwException("An unexpected server error occurred.", status, responseText);
        }
        return null;
    }

    getThumbnailsOfMember(email: string): Observable<PhotoManagementData[]> {
        let url_ = this.baseUrl + "/api/Photo/GetThumbnailsOfMember/{email}";
        if (email === undefined || email === null)
            throw new Error("The parameter 'email' must be defined.");
        url_ = url_.replace("{email}", encodeURIComponent("" + email));

        const content_ = "";
        
        let options_ = {
            body: content_,
            method: "get",
            headers: new Headers({
                "Content-Type": "application/json; charset=UTF-8", 
                "Accept": "application/json; charset=UTF-8"
            })
        };

        return this.http.request(url_, options_).map((response) => {
            return this.processGetThumbnailsOfMember(response);
        }).catch((response: any, caught: any) => {
            if (response instanceof Response) {
                try {
                    return Observable.of(this.processGetThumbnailsOfMember(response));
                } catch (e) {
                    return <Observable<PhotoManagementData[]>><any>Observable.throw(e);
                }
            } else
                return <Observable<PhotoManagementData[]>><any>Observable.throw(response);
        });
    }

    protected processGetThumbnailsOfMember(response: Response): PhotoManagementData[] {
        const responseText = response.text();
        const status = response.status; 

        if (status === 200) {
            let result200: PhotoManagementData[] = null;
            let resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            if (resultData200 && resultData200.constructor === Array) {
                result200 = [];
                for (let item of resultData200)
                    result200.push(PhotoManagementData.fromJS(item));
            }
            return result200;
        } else if (status !== 200 && status !== 204) {
            this.throwException("An unexpected server error occurred.", status, responseText);
        }
        return null;
    }

    getImagesOfMember(): Observable<PhotoMemberInfo[]> {
        let url_ = this.baseUrl + "/api/Photo/GetImagesOfMember";

        const content_ = "";
        
        let options_ = {
            body: content_,
            method: "get",
            headers: new Headers({
                "Content-Type": "application/json; charset=UTF-8", 
                "Accept": "application/json; charset=UTF-8"
            })
        };

        return this.http.request(url_, options_).map((response) => {
            return this.processGetImagesOfMember(response);
        }).catch((response: any, caught: any) => {
            if (response instanceof Response) {
                try {
                    return Observable.of(this.processGetImagesOfMember(response));
                } catch (e) {
                    return <Observable<PhotoMemberInfo[]>><any>Observable.throw(e);
                }
            } else
                return <Observable<PhotoMemberInfo[]>><any>Observable.throw(response);
        });
    }

    protected processGetImagesOfMember(response: Response): PhotoMemberInfo[] {
        const responseText = response.text();
        const status = response.status; 

        if (status === 200) {
            let result200: PhotoMemberInfo[] = null;
            let resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            if (resultData200 && resultData200.constructor === Array) {
                result200 = [];
                for (let item of resultData200)
                    result200.push(PhotoMemberInfo.fromJS(item));
            }
            return result200;
        } else if (status !== 200 && status !== 204) {
            this.throwException("An unexpected server error occurred.", status, responseText);
        }
        return null;
    }

    getComments(photoId: string): Observable<CommentData[]> {
        let url_ = this.baseUrl + "/api/Photo/GetComments/{photoId}";
        if (photoId === undefined || photoId === null)
            throw new Error("The parameter 'photoId' must be defined.");
        url_ = url_.replace("{photoId}", encodeURIComponent("" + photoId));

        const content_ = "";
        
        let options_ = {
            body: content_,
            method: "get",
            headers: new Headers({
                "Content-Type": "application/json; charset=UTF-8", 
                "Accept": "application/json; charset=UTF-8"
            })
        };

        return this.http.request(url_, options_).map((response) => {
            return this.processGetComments(response);
        }).catch((response: any, caught: any) => {
            if (response instanceof Response) {
                try {
                    return Observable.of(this.processGetComments(response));
                } catch (e) {
                    return <Observable<CommentData[]>><any>Observable.throw(e);
                }
            } else
                return <Observable<CommentData[]>><any>Observable.throw(response);
        });
    }

    protected processGetComments(response: Response): CommentData[] {
        const responseText = response.text();
        const status = response.status; 

        if (status === 200) {
            let result200: CommentData[] = null;
            let resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            if (resultData200 && resultData200.constructor === Array) {
                result200 = [];
                for (let item of resultData200)
                    result200.push(CommentData.fromJS(item));
            }
            return result200;
        } else if (status !== 200 && status !== 204) {
            this.throwException("An unexpected server error occurred.", status, responseText);
        }
        return null;
    }

    addComment(uploadData: CommentUploadData): Observable<CommentData> {
        let url_ = this.baseUrl + "/api/Photo/AddComment";

        const content_ = JSON.stringify(uploadData ? uploadData.toJS() : null);
        
        let options_ = {
            body: content_,
            method: "post",
            headers: new Headers({
                "Content-Type": "application/json; charset=UTF-8", 
                "Accept": "application/json; charset=UTF-8"
            })
        };

        return this.http.request(url_, options_).map((response) => {
            return this.processAddComment(response);
        }).catch((response: any, caught: any) => {
            if (response instanceof Response) {
                try {
                    return Observable.of(this.processAddComment(response));
                } catch (e) {
                    return <Observable<CommentData>><any>Observable.throw(e);
                }
            } else
                return <Observable<CommentData>><any>Observable.throw(response);
        });
    }

    protected processAddComment(response: Response): CommentData {
        const responseText = response.text();
        const status = response.status; 

        if (status === 200) {
            let result200: CommentData = null;
            let resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            result200 = resultData200 ? CommentData.fromJS(resultData200) : null;
            return result200;
        } else if (status !== 200 && status !== 204) {
            this.throwException("An unexpected server error occurred.", status, responseText);
        }
        return null;
    }

    uploadPhoto(): Observable<any> {
        let url_ = this.baseUrl + "/api/Photo/UploadPhoto";

        const content_ = "";
        
        let options_ = {
            body: content_,
            method: "post",
            headers: new Headers({
                "Content-Type": "application/json; charset=UTF-8", 
                "Accept": "application/json; charset=UTF-8"
            })
        };

        return this.http.request(url_, options_).map((response) => {
            return this.processUploadPhoto(response);
        }).catch((response: any, caught: any) => {
            if (response instanceof Response) {
                try {
                    return Observable.of(this.processUploadPhoto(response));
                } catch (e) {
                    return <Observable<any>><any>Observable.throw(e);
                }
            } else
                return <Observable<any>><any>Observable.throw(response);
        });
    }

    protected processUploadPhoto(response: Response): any {
        const responseText = response.text();
        const status = response.status; 

        if (status === 200) {
            let result200: any = null;
            let resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            result200 = resultData200 !== undefined ? resultData200 : null;
            return result200;
        } else if (status !== 200 && status !== 204) {
            this.throwException("An unexpected server error occurred.", status, responseText);
        }
        return null;
    }

    protected throwException(message: string, status: number, response: string, result?: any): any {
        throw new SwaggerException(message, status, response, result);
    }
}

export class MemberDto { 
    firstName?: string; 
    surname?: string; 
    email?: string; 
    entryDate?: Date; 
    lastUpdate?: Date; 
    id: string;
    constructor(data?: any) {
        if (data !== undefined) {
            this.firstName = data["FirstName"] !== undefined ? data["FirstName"] : undefined;
            this.surname = data["Surname"] !== undefined ? data["Surname"] : undefined;
            this.email = data["Email"] !== undefined ? data["Email"] : undefined;
            this.entryDate = data["EntryDate"] ? new Date(data["EntryDate"].toString()) : undefined;
            this.lastUpdate = data["LastUpdate"] ? new Date(data["LastUpdate"].toString()) : undefined;
            this.id = data["Id"] !== undefined ? data["Id"] : undefined;
        }
    }

    static fromJS(data: any): MemberDto {
        return new MemberDto(data);
    }

    toJS(data?: any) {
        data = data === undefined ? {} : data;
        data["FirstName"] = this.firstName !== undefined ? this.firstName : undefined;
        data["Surname"] = this.surname !== undefined ? this.surname : undefined;
        data["Email"] = this.email !== undefined ? this.email : undefined;
        data["EntryDate"] = this.entryDate ? this.entryDate.toISOString() : undefined;
        data["LastUpdate"] = this.lastUpdate ? this.lastUpdate.toISOString() : undefined;
        data["Id"] = this.id !== undefined ? this.id : undefined;
        return data; 
    }

    toJSON() {
        return JSON.stringify(this.toJS());
    }

    clone() {
        const json = this.toJSON();
        return new MemberDto(JSON.parse(json));
    }
}

export class PhotoUploadData { 
    data?: string; 
    fileName?: string; 
    title?: string; 
    email?: string;
    constructor(data?: any) {
        if (data !== undefined) {
            this.data = data["Data"] !== undefined ? data["Data"] : undefined;
            this.fileName = data["FileName"] !== undefined ? data["FileName"] : undefined;
            this.title = data["Title"] !== undefined ? data["Title"] : undefined;
            this.email = data["Email"] !== undefined ? data["Email"] : undefined;
        }
    }

    static fromJS(data: any): PhotoUploadData {
        return new PhotoUploadData(data);
    }

    toJS(data?: any) {
        data = data === undefined ? {} : data;
        data["Data"] = this.data !== undefined ? this.data : undefined;
        data["FileName"] = this.fileName !== undefined ? this.fileName : undefined;
        data["Title"] = this.title !== undefined ? this.title : undefined;
        data["Email"] = this.email !== undefined ? this.email : undefined;
        return data; 
    }

    toJSON() {
        return JSON.stringify(this.toJS());
    }

    clone() {
        const json = this.toJSON();
        return new PhotoUploadData(JSON.parse(json));
    }
}

export class PhotoManagementData { 
    fileName?: string; 
    thumbnailBytes?: string; 
    title?: string; 
    id?: string;
    constructor(data?: any) {
        if (data !== undefined) {
            this.fileName = data["FileName"] !== undefined ? data["FileName"] : undefined;
            this.thumbnailBytes = data["ThumbnailBytes"] !== undefined ? data["ThumbnailBytes"] : undefined;
            this.title = data["Title"] !== undefined ? data["Title"] : undefined;
            this.id = data["Id"] !== undefined ? data["Id"] : undefined;
        }
    }

    static fromJS(data: any): PhotoManagementData {
        return new PhotoManagementData(data);
    }

    toJS(data?: any) {
        data = data === undefined ? {} : data;
        data["FileName"] = this.fileName !== undefined ? this.fileName : undefined;
        data["ThumbnailBytes"] = this.thumbnailBytes !== undefined ? this.thumbnailBytes : undefined;
        data["Title"] = this.title !== undefined ? this.title : undefined;
        data["Id"] = this.id !== undefined ? this.id : undefined;
        return data; 
    }

    toJSON() {
        return JSON.stringify(this.toJS());
    }

    clone() {
        const json = this.toJSON();
        return new PhotoManagementData(JSON.parse(json));
    }
}

export class PhotoMemberInfo { 
    email?: string; 
    fileName?: string; 
    title?: string; 
    photoId?: string;
    constructor(data?: any) {
        if (data !== undefined) {
            this.email = data["Email"] !== undefined ? data["Email"] : undefined;
            this.fileName = data["FileName"] !== undefined ? data["FileName"] : undefined;
            this.title = data["Title"] !== undefined ? data["Title"] : undefined;
            this.photoId = data["PhotoId"] !== undefined ? data["PhotoId"] : undefined;
        }
    }

    static fromJS(data: any): PhotoMemberInfo {
        return new PhotoMemberInfo(data);
    }

    toJS(data?: any) {
        data = data === undefined ? {} : data;
        data["Email"] = this.email !== undefined ? this.email : undefined;
        data["FileName"] = this.fileName !== undefined ? this.fileName : undefined;
        data["Title"] = this.title !== undefined ? this.title : undefined;
        data["PhotoId"] = this.photoId !== undefined ? this.photoId : undefined;
        return data; 
    }

    toJSON() {
        return JSON.stringify(this.toJS());
    }

    clone() {
        const json = this.toJSON();
        return new PhotoMemberInfo(JSON.parse(json));
    }
}

export class CommentData { 
    comment?: string; 
    authorId?: string; 
    photoId: string; 
    commentDate: Date; 
    id?: string;
    constructor(data?: any) {
        if (data !== undefined) {
            this.comment = data["Comment"] !== undefined ? data["Comment"] : undefined;
            this.authorId = data["AuthorId"] !== undefined ? data["AuthorId"] : undefined;
            this.photoId = data["PhotoId"] !== undefined ? data["PhotoId"] : undefined;
            this.commentDate = data["CommentDate"] ? new Date(data["CommentDate"].toString()) : undefined;
            this.id = data["Id"] !== undefined ? data["Id"] : undefined;
        }
    }

    static fromJS(data: any): CommentData {
        return new CommentData(data);
    }

    toJS(data?: any) {
        data = data === undefined ? {} : data;
        data["Comment"] = this.comment !== undefined ? this.comment : undefined;
        data["AuthorId"] = this.authorId !== undefined ? this.authorId : undefined;
        data["PhotoId"] = this.photoId !== undefined ? this.photoId : undefined;
        data["CommentDate"] = this.commentDate ? this.commentDate.toISOString() : undefined;
        data["Id"] = this.id !== undefined ? this.id : undefined;
        return data; 
    }

    toJSON() {
        return JSON.stringify(this.toJS());
    }

    clone() {
        const json = this.toJSON();
        return new CommentData(JSON.parse(json));
    }
}

export class CommentUploadData { 
    comment?: string; 
    email?: string; 
    photoId: string; 
    createDate: Date;
    constructor(data?: any) {
        if (data !== undefined) {
            this.comment = data["Comment"] !== undefined ? data["Comment"] : undefined;
            this.email = data["Email"] !== undefined ? data["Email"] : undefined;
            this.photoId = data["PhotoId"] !== undefined ? data["PhotoId"] : undefined;
            this.createDate = data["CreateDate"] ? new Date(data["CreateDate"].toString()) : undefined;
        }
    }

    static fromJS(data: any): CommentUploadData {
        return new CommentUploadData(data);
    }

    toJS(data?: any) {
        data = data === undefined ? {} : data;
        data["Comment"] = this.comment !== undefined ? this.comment : undefined;
        data["Email"] = this.email !== undefined ? this.email : undefined;
        data["PhotoId"] = this.photoId !== undefined ? this.photoId : undefined;
        data["CreateDate"] = this.createDate ? this.createDate.toISOString() : undefined;
        return data; 
    }

    toJSON() {
        return JSON.stringify(this.toJS());
    }

    clone() {
        const json = this.toJSON();
        return new CommentUploadData(JSON.parse(json));
    }
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