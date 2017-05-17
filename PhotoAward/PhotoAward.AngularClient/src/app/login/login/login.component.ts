import { TokenService, BearerToken } from './../../Shared/tokenservice';
import { UserService } from 'app/Shared/user.service';
import { Component, OnInit } from '@angular/core';
import { MemberManagementClient, MemberDto } from "./../../Shared/Controllers.generated";
import { User } from "app/Shared/user/user";



@Component({
  selector: 'pac-login',
  templateUrl: './login.component.html',
  providers: [],
  styles: [],
})
export class LoginComponent implements OnInit {
  message: string;
  email: string;
  password: string;
  user: User;
  constructor(private _memberManagementClient: MemberManagementClient, private _userService: UserService,
    private tokenService: TokenService) { }

  ngOnInit() {
  }
  async login(value: any) {
    this.message = "Anmeldung gestartet ....";
    const that = this;

    await this.tokenService.login(this.email, this.password).subscribe((token: BearerToken) => {
      this._userService.token =token;
      const dto = that._memberManagementClient.get(this.email).subscribe(
        (result: MemberDto) => {
          that.user = new User();
          that.user.firstname = result.firstName ? result.firstName : "";
          that.user.surname = result.surname ? result.surname : "";
          that.user.id = result.id;
          that.user.notMember = false;
          that.user.email = result.email ? result.email : "";
          that.message = "Willkommen " + that.user.firstname + " " + that.user.surname;
          this._userService.user = that.user;
        },
        (error) => {
          that.user = new User();
          that.user.notMember = true;
          that._userService.user = that.user;
          that.message = error.message;
          console.log(error);
        }

      );

    });

  }

}
