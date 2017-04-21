
import { UserService } from './../../Shared/user.service';
import { PhotoManagementData, PhotoManagementClient } from './../../Shared/Controllers.generated';
import { Component, OnInit, Input } from '@angular/core';
import { Router } from "@angular/router/";


@Component({
  selector: 'pac-show-photos-component',
  templateUrl: './show-photos-component.component.html',
  styles: []
})

export class ShowPhotosComponentComponent implements OnInit {
  @Input() email: string;
  photos: PhotoManagementData[] = [];

  constructor(private _photoManagementClient: PhotoManagementClient, private _userService: UserService, private router: Router) { }

  ngOnInit() {
    if (this._userService && this._userService.user && this._userService.user.notMember === false) {
      this.email = this._userService.user.email;
      if (this.email) {
        this.loadThumbnails();
        return;
      }
    }
    this.router.navigateByUrl('/');


  }

  loadThumbnails() {
    this.showImagesOfMember(this.email);
  }

  showImagesOfMember(email: string) {
    const that = this;
    console.log("Loading images for user " + email);
    this._photoManagementClient.getThumbnailsOfMember(email).subscribe(
      images => {
        console.log("Bilder ermittelt: ");
        if (images == null) {
          console.log("Keine bilder!");
        } else {
          console.log("Bilder Anzahl: " + images.length);
        }
        that.photos = images;
      }, (error) => {
        console.log(error);

      }
    );
  }

}
