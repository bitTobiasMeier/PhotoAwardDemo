import { User } from 'app/Shared/user/user';
import { Injectable } from '@angular/core';

@Injectable()
export class UserService {
  user : User;
  constructor() {
    this.user = new User ();
    this.user.notMember = true;
  }

}
