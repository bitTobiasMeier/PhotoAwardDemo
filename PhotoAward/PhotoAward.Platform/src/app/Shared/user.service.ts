import { Router } from '@angular/router';
import { User } from 'app/Shared/user/user';
import { Injectable } from '@angular/core';

@Injectable()
export class UserService {
  user : User;
  constructor(private router: Router) {
    this.user = new User ();
    this.user.notMember = true;
  }

  logout () {
    if (this.user == null) {
        this.user = new User ();
    } else {
      this.user.notMember = true;
      this.user.email = "";
      this.user.firstname = "";
      this.user.surname = "";
      this.user.id = "";
    }
     this.router.navigateByUrl('/');
  }

}
