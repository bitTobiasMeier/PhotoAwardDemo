
import { UploadService } from './Shared/uploadService';
import { PhotoManagementClient } from './Shared/Controllers.generated';
import { MemberManagementClient, MemberDto, PhotoUploadData, PhotoManagementData } from './Shared/Controllers.generated';
import { Component, Injectable, OpaqueToken } from '@angular/core';
import { UserService } from "./Shared/user.service";

@Component({
  selector: 'pac-root',
  templateUrl: './app.component.html',
  providers: [],
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


constructor(
   public userservice : UserService,
   private _photoManagementClient: PhotoManagementClient, private _uploadService: UploadService) {

  this.firstname = '';
  this.surname = '';
}



  imageUploaded (message:string ){
    const e= this.email;
    this.email = "";
    this.email = e;
  }






}
