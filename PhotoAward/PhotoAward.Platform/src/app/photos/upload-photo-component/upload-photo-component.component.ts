import { UserService } from 'app/Shared/user.service';
import { UploadService } from './../../Shared/uploadService';
import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { Router } from "@angular/router/";

@Component({
  selector: 'pac-upload-photo-component',
  templateUrl: './upload-photo-component.component.html',
  styles: []
})
export class UploadPhotoComponentComponent implements OnInit {
    @Output() imageUploaded = new EventEmitter<string>();
    private  _filesToUpload : any;
    filetitle: string;
    message ="";

  constructor(private _uploadService: UploadService, private _userService: UserService, private router: Router) { }

  ngOnInit() {
    if (this._userService  && this._userService.user && this._userService.user.notMember === false ) {
      return;
    }
     this.router.navigateByUrl('/');
  }


   fileChangeEvent (fileInput: any){
    this._filesToUpload = <Array<File>> fileInput.target.files;
  }

  async upload () {
    if (this._filesToUpload.length > 0){
      const that = this;
      const files =  this._filesToUpload;
           const filename = files[0].name;
           that.message ="Uploading image " + filename;
           const result = await this._uploadService.uploadFile(this._userService.user.email, this.filetitle, filename,  [], files).then(
             e=> {
               that.message="Bild wurde hochgeladen";
               that.imageUploaded.emit("");
               that.filetitle = "";
               that._filesToUpload =[];
             }
           ).catch(
             (error=> {
                that.message ="Upload fehlgeschlagen: " + error;
             })
           ) ;
           //ToDo: ShowImages
           //await this.showImagesOfMember(this.email);
           console.log("Upload beendet!");

    }
  }

}
