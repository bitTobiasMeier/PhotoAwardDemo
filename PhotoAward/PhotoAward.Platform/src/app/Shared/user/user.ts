export class User {
  firstname: string;
  surname: string;
  email: string;
  notMember = false;
  id: string;
}

const user =<Partial<User>> new User();
user.id = "";
