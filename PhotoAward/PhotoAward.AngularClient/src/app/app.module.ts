import { UserService } from 'app/Shared/user.service';
import { ShowPhotosComponentComponent } from './photos/show-photos-component/show-photos-component.component';
import { UploadPhotoComponentComponent } from './photos/upload-photo-component/upload-photo-component.component';
import { RegisterMemberComponent } from './login/register-member/register-member.component';
import { PhotoManagementClient } from './Shared/Controllers.generated';
import { MemberManagementClient, API_BASE_URL} from './Shared/Controllers.generated';
import { TokenService, API_BASE_URL as TokenServiceAPI} from './Shared/tokenservice';
import { UploadService } from './Shared/uploadService';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Injectable } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule, BaseRequestOptions, RequestOptionsArgs, RequestOptions, Headers } from '@angular/http';
import { OpaqueToken } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';


import { AppComponent } from './app.component';
import { DOCUMENT } from '@angular/platform-browser';
import { LoginComponent } from './login/login/login.component';
import { PhotoDetailComponent } from './photos/photo-detail/photo-detail.component';
import { AddCommentComponent } from './photos/add-comment/add-comment.component';
import { ShowCommentsComponent } from './photos/show-comments/show-comments.component';
import { ShowCommentComponent } from './photos/show-comment/show-comment.component';



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

@Injectable()
export class DefaultRequestOptions extends BaseRequestOptions {
  headers = new Headers({
     'Author':'BridgingIT GmbH',
  });

  constructor (private _userService: UserService) {
      super();
  }

  merge(options?: RequestOptionsArgs): RequestOptions {
    const newOptions = super.merge(options);
    if (this._userService.token)    {
        newOptions.headers.set('Authorization','Bearer ' +this._userService.token.access_token);
    }else {
        newOptions.headers.delete('bearer');
    }
    return newOptions;
  }
}


@NgModule({
  declarations: [
    AppComponent,
    RegisterMemberComponent,
    UploadPhotoComponentComponent,
    ShowPhotosComponentComponent,
    LoginComponent,
    PhotoDetailComponent,
    AddCommentComponent,
    ShowCommentsComponent,
    ShowCommentComponent

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
    TokenService,
    UserService,
    {provide: API_BASE_URL, useValue: API_BASE_URL2},
    {provide: TokenServiceAPI, useValue: API_BASE_URL2},
     {provide: RequestOptions, useClass: DefaultRequestOptions }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
