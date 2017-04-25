import { UserService } from 'app/Shared/user.service';
import { MemberDto } from 'app/Shared/Controllers.generated';
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
  message: string;
  email:string;
  user: User;
  constructor(private _memberManagementClient: MemberManagementClient, private _userService: UserService) { }



  ngOnInit() {
      type Person = Record<'firstname' | 'surname' | 'email', string>;

      const P1 = <Person> {
        firstname:"Tobias",
        surname:"Meier",
        email:"tobias.meier@bridging-it.de"
      };

      type OnlinePerson = Pick<Person, 'email'>;
      const P2 = <OnlinePerson> P1;
      console.log(P2.email);

      const clone = {...P2};
    console.log( clone === P2);
    console.log (clone.email);


  }

  async login(value: any) {
    this.message = "Anmeldung gestartet ....";
    const that = this;
    const dto = await this._memberManagementClient.get(this.email).subscribe(
      (result: MemberDto) => {
        that.user = new User();
        that.user.firstname = result.firstName ? result.firstName : "";
        that.user.surname = result.surname ? result.surname : "";
        that.user.id = result.id ;
        that.user.notMember = false;
        that.user.email = result.email ? result.email: "";
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


  }

}
