import { UploadService } from './Shared/uploadService';
import { PhotoManagementClient } from './Shared/Controllers.generated';
import { MemberManagementClient, MemberDto, PhotoUploadData, PhotoManagementData } from './Shared/Controllers.generated';
import { Component, Injectable, OpaqueToken} from '@angular/core';

@Component({
  selector: 'pac-root',
  templateUrl: './app.component.html',

  styles: []
})
export class AppComponent {
  title = 'PhotoAward Client';
  email = 'tobias.meier@bridging-it.de';
  firstname: string;
  surname: string;
  id: string;
  filetitle: string;
  notMember: boolean = true;
  private  _filesToUpload : any;
  photos: PhotoManagementData[];


constructor(private _memberManagementClient: MemberManagementClient,
 private _photoManagementClient: PhotoManagementClient, private _uploadService: UploadService) {

}

  async login(value: any)  {
    const that = this;
    const dto  = await this._memberManagementClient.get(this.email).subscribe (
      (result: MemberDto) => {
         that.firstname = result.firstName;
         that.surname = result.surname;
         that.id = result.id;
         that.notMember = false;
         that.showImagesOfMember(that.email);
      },
      (error)=> {
        that.notMember = true;
        console.log(error);
      }

    );


  }

  showImagesOfMember (email: string){
      const that = this;
      console.log("Loading images for user " + email);
      this._photoManagementClient.getImagesOfMember (email).subscribe (
      images => {
        console.log ("Bilder ermittelt: " );
        if (images == null) {
          console.log ("Keine bilder!");
        } else {
          console.log ("Bilder Anzahl: " + images.length);
        }
        that.photos = images;
      }, (error)=> {
        console.log(error);

      }
    );
  }


  async register(value: any) {
    const that = this;
    const dto = new MemberDto();
      dto.firstName =  this.firstname;
      dto.surname =  this.surname;
      dto.email = this.email;

    const dtoResult  = await this._memberManagementClient.add(dto).subscribe (
      (result: MemberDto) => {
         that.firstname = result.firstName;
         that.surname = result.surname;
         that.id = result.id;
      });

  }

  fileChangeEvent (fileInput: any){
    this._filesToUpload = <Array<File>> fileInput.target.files;
  }

  async upload () {
    if (this._filesToUpload.length > 0){
      const files =  this._filesToUpload;
           const filename = files[0].name;
           const result = await this._uploadService.uploadFile(this.email, this.filetitle, filename,  [], files);
           await this.showImagesOfMember(this.email);
           console.log("Upload beendet!");

    }
  }

}
