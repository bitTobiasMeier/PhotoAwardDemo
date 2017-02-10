import { MemberManagementClient, API_BASE_URL } from './Shared/Controllers.generated';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { OpaqueToken } from '@angular/core';

import { AppComponent } from './app.component';
import { DOCUMENT } from '@angular/platform-browser'

let API_BASE_URL2 = 'http://NB-000953:8200';

if (window != null && window.location != null)
{
      const loc = window.location;
      const server = loc.hostname;
      let port =  loc.port;
      if (server === 'localhost' && port === '4200')
      {
        port = '8200';
      }
      API_BASE_URL2 = loc.protocol +'//' +  server + ':' + port;
}


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  providers: [
    MemberManagementClient,
    {provide: API_BASE_URL, useValue: API_BASE_URL2}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
