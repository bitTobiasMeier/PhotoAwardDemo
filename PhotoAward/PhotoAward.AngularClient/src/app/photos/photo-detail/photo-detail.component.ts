import { PhotoManagementData, PhotoManagementClient } from './../../Shared/Controllers.generated';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'pac-photo-detail',
  templateUrl: './photo-detail.component.html',
  styles: []
})
export class PhotoDetailComponent implements OnInit {
   @Input() photo : PhotoManagementData;
   photoData : any;
ShowBigImage = false;
  constructor(private _photoManagementClient: PhotoManagementClient) { }

  ngOnInit() {
  }

   displayDetail(){
     const that = this;
     this.ShowBigImage = true;
     if (!this.photoData) {
       this._photoManagementClient.getImage(this.photo.id).subscribe( (data)=>{
          that.photoData = data;
       });
     }
  }

   closeImage (){
     this.ShowBigImage = false;
   }

}
