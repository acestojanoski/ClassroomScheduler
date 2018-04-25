import { User } from './user.model';

export interface Course {
  id: number;
  name: string;
  semester: number;
  professors: User[];
}
