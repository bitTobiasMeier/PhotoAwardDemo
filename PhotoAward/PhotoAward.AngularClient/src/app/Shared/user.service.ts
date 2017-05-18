import { BearerToken } from './tokenservice';
import { Router } from '@angular/router';
import { User } from 'app/Shared/user/user';
import { Injectable } from '@angular/core';


@Injectable()
export class UserService {
  user: Readonly<User>;
  token: BearerToken;

  constructor(private router: Router) {
    const user = new User();
    user.notMember = true;
    this.user = user;
  }

  logout() {
    if (this.user == null) {
      this.user = new User();
    } else {
      const user = new User();
      user.notMember = true;
      this.user = user;
      this.token = null;
    }
    this.router.navigateByUrl('/');
  }

}

