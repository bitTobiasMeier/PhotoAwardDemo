import { PhotoManagementData , PhotoManagementClient} from './../../Shared/Controllers.generated';
import { Component, OnInit, Input  } from '@angular/core';

@Component({
  selector: 'pac-show-photos-component',
  templateUrl: './show-photos-component.component.html',
  styles: []
})

export class ShowPhotosComponentComponent implements OnInit {
  @Input() email:string;
  photos: PhotoManagementData[] = [];

  constructor(private _photoManagementClient: PhotoManagementClient) { }

  ngOnInit() {
  }

  loadImages (){
    this.showImagesOfMember(this.email);
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

}
