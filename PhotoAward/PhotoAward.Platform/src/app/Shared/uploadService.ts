import {Observable} from 'rxjs/Rx';
import { PhotoManagementClient, API_BASE_URL } from './Controllers.generated';
import { Component, Injectable, Inject, Optional, OpaqueToken} from '@angular/core';


@Injectable()
export class UploadService {
  progress$: any;
  progress: any;
  progressObserver: any;
  private _baseUrl:string;

  constructor( @Optional() @Inject(API_BASE_URL) baseUrl?: string ) {
    this._baseUrl = baseUrl ? baseUrl : "";
    this.progress$ = Observable.create(observer => {
        this.progressObserver = observer;
    }).share();
   }

 uploadFile(email:string, title:string, filename:string, params: string[], files: File[])  {

   return new Promise<string>((resolve, reject) => {

   const url = this._baseUrl + '/api/photo/uploadPhoto';
   const formData: FormData = new FormData(),
            xhr: XMLHttpRequest = new XMLHttpRequest();

        for (let i = 0; i < files.length; i++) {
            formData.append("uploads[]", files[i], files[i].name);
        }

        xhr.onreadystatechange = () => {
          if (xhr.readyState ===1) {
            xhr.setRequestHeader("email",email);
            xhr.setRequestHeader("title", title);
            xhr.setRequestHeader("filename", filename);
          }
            if (xhr.readyState === 4) {
                if (xhr.status === 200) {
                    resolve(xhr.responseText); //OK
                } else {
                    reject(xhr.statusText); //Error
                }
            }
        };


        xhr.upload.onprogress = (event) => {
            this.progress = Math.round(event.loaded / event.total * 100);

            this.progressObserver.next(this.progress);
        };

        xhr.open('POST', url, true);
        const serverFileName = xhr.send(formData);
        return serverFileName;
   });  //Promise
  }
}
