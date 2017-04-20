import { UserService } from 'app/Shared/user.service';
import { ShowPhotosComponentComponent } from './photos/show-photos-component/show-photos-component.component';
import { UploadPhotoComponentComponent } from './photos/upload-photo-component/upload-photo-component.component';
import { RegisterMemberComponent } from './login/register-member/register-member.component';
import { PhotoManagementClient } from './Shared/Controllers.generated';
import { MemberManagementClient, API_BASE_URL} from './Shared/Controllers.generated';
import { UploadService } from './Shared/uploadService';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { OpaqueToken } from '@angular/core';
import { AppRoutingModule } from './app-routing.module'


import { AppComponent } from './app.component';
import { DOCUMENT } from '@angular/platform-browser';
import { LoginComponent } from './login/login/login.component';

let API_BASE_URL2 = 'http://NB-000953:8200';


if (window != null && window.location != null) {
      const loc = window.location;
      const server = loc.hostname;
      let port =  loc.port;
      if (server === 'localhost' && port === '4200') {
        port = '8200';
      }
      API_BASE_URL2 = loc.protocol + '//' +  server + ':' + port;
}



@NgModule({
  declarations: [
    AppComponent,
    RegisterMemberComponent,
    UploadPhotoComponentComponent,
    ShowPhotosComponentComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    AppRoutingModule
  ],
  providers: [
    PhotoManagementClient,
    MemberManagementClient,
    UploadService,
    UserService,
    {provide: API_BASE_URL, useValue: API_BASE_URL2},
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
