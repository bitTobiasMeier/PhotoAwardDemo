﻿import { UploadService } from './Shared/uploadService';
import { PhotoManagementClient } from './Shared/Controllers.generated';
import { MemberManagementClient, MemberDto, PhotoUploadData, PhotoManagementData } from './Shared/Controllers.generated';
import { Component, Injectable, OpaqueToken} from '@angular/core';

@Component({
  selector: 'pac-root',
  templateUrl: './app.component.html',

  styles: []
})
export class AppComponent {
  title = 'Photo Award Client';
  email = '';
  firstname: string;
  surname: string;
  id: string;
  filetitle: string;
  notMember: boolean = true;
  message:string;

  photos: PhotoManagementData[];


constructor(private _memberManagementClient: MemberManagementClient,
 private _photoManagementClient: PhotoManagementClient, private _uploadService: UploadService) {

  this.firstname = '';
  this.surname = '';
}

  async login(value: any)  {
    this.message = "Anmeldung gestartet ....";
    const that = this;
    const dto  = await this._memberManagementClient.get(this.email).subscribe (
      (result: MemberDto) => {
         that.firstname = result.firstName;
         that.surname = result.surname;
         that.id = result.id;
         that.notMember = false;
         that.message = "Willkommen " + that.firstname +" " + that.surname;

      },
      (error)=> {
        that.notMember = true;
        that.message = error.message;
        console.log(error);
      }

    );


  }

  imageUploaded (message:string ){
    const e= this.email;
    this.email = "";
    this.email = e;
  }






}
