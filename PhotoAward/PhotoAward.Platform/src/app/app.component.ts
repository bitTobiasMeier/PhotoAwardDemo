import { MemberManagementClient, MemberDto } from './Shared/Controllers.generated';
import { Component } from '@angular/core';

@Component({
  selector: 'pac-root',
  templateUrl: './app.component.html',

  styles: []
})
export class AppComponent {
  title = 'PhotoAward Client';
  email = 'tobias.meier@bridging-it.de';
  firstname: string;
  surname: string;
  id: string;

constructor(private _memberManagementClient: MemberManagementClient) {

}

  async login(value: any)
  {
    let that = this;
    console.log(value);
    let dto  = await this._memberManagementClient.get(this.email).subscribe (
      (result: MemberDto) => {
         that.firstname = result.firstName;
         that.surname = result.surname;
         that.id = result.id;
      }

    );
  }


  async register(value: any)
  {
    const that = this;
    const dto = new MemberDto();
      dto.firstName =  this.firstname;
      dto.surname =  this.surname;
      dto.email = this.email;

    const dtoResult  = await this._memberManagementClient.add(dto).subscribe (
      (result: MemberDto) => {
         that.firstname = result.firstName;
         that.surname = result.surname;
         that.id = result.id;
      }

    );
  }

}
