import { UserType } from './user-type.model';

export interface User {
  firstName: string;
  lastName: string;
  userType: UserType;
  id: string;
  userName: string;
  email: string;
}
