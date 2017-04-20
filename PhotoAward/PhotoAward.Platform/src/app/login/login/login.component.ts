import { UserService } from './../../Shared/user.service';
import { MemberDto } from './../../Shared/Controllers.generated';
import { Component, OnInit } from '@angular/core';
import { MemberManagementClient } from "app/Shared/Controllers.generated";
import { User } from "app/Shared/user/user";

@Component({
  selector: 'pac-login',
  templateUrl: './login.component.html',
  providers: [],
  styles: []
})
export class LoginComponent implements OnInit {
  email = '';
  message:string;
  firstname: string;
  surname: string;
  notMember = false;
  id: string;
  user: User;
  constructor(private _memberManagementClient: MemberManagementClient, private _userService: UserService) { }

  ngOnInit() {
  }

   async login(value: any)  {
    this.message = "Anmeldung gestartet ....";
    const that = this;
    const dto  = await this._memberManagementClient.get(this.email).subscribe (
      (result: MemberDto) => {
         that.user = new User () ;
         that.user.firstname = result.firstName;
         that.user.surname = result.surname;
         that.user.id = result.id;
         that.user.notMember = false;
         that.user.email = result.email;
         that.message = "Willkommen " + that.user.firstname +" " + that.user.surname;
         this._userService.user = that.user;
      },
      (error)=> {
        that.notMember = true;
        that.message = error.message;
        console.log(error);
      }

    );


  }

}
