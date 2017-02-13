import { UploadService } from './../../Shared/uploadService';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'pac-upload-photo-component',
  templateUrl: './upload-photo-component.component.html',
  styles: []
})
export class UploadPhotoComponentComponent implements OnInit {
    @Input() email:string;
    private  _filesToUpload : any;
    filetitle: string;

  constructor(private _uploadService: UploadService) { }

  ngOnInit() {
  }


   fileChangeEvent (fileInput: any){
    this._filesToUpload = <Array<File>> fileInput.target.files;
  }

  async upload () {
    if (this._filesToUpload.length > 0){
      const files =  this._filesToUpload;
           const filename = files[0].name;
           const result = await this._uploadService.uploadFile(this.email, this.filetitle, filename,  [], files);
           //ToDo: ShowImages
           //await this.showImagesOfMember(this.email);
           console.log("Upload beendet!");

    }
  }

}
