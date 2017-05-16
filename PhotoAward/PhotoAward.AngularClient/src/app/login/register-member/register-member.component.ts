import { MemberDto, MemberManagementClient } from './../../Shared/Controllers.generated';
import { Headers } from '@angular/http';
import { UploadService } from './../../Shared/uploadService';
import { Observable } from 'rxjs/Observable';
import { Component, OnInit, Injectable, OpaqueToken } from '@angular/core';

@Component({
  selector: 'pac-register-member',
  templateUrl: './register-member.component.html',
  styles: []
})
export class RegisterMemberComponent implements OnInit {

  email: string;
  password: string;
  password2: string;
  firstname: string;
  surname: string;
  id: string;
  message: string;

  constructor(private _memberManagementClient: MemberManagementClient) { }

  ngOnInit() {
    this.email = "tobias.meier@bridging-it.de";
  }

  async register(value: any) {
    if (this.password !== this.password2) {
      this.message = "Passwörter sind unterschiedlich";
      return;
    }
    this.message = "Die Registrierung wird übermittelt ...";
    const that = this;
    const dto = new MemberDto();
    dto.firstName = this.firstname;
    dto.surname = this.surname;
    dto.email = this.email;
    dto.password = this.password;

    const dtoResult = await this._memberManagementClient.add(dto).subscribe(
      (result: MemberDto) => {
        that.firstname = result.firstName ? result.firstName : "";
        that.surname = result.surname ? result.surname : "";
        that.id = result.id;
        that.message = "Die Registrierung war erfolgreich. Bitte melden Sie sich nun an.";
      }, (e)=> {
         that.message = "Fehler: " + e.message;
        });

  }
}
