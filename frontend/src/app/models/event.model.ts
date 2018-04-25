import { EventType } from './event-type.model';
import { ClassRoom } from './class-room.model';
import { User } from './user.model';
import { Course } from './course.model';

export interface Event {
  id: number;
  startTime: any;
  endTime: any;
  repeat: boolean;
  repeatTimes: number;
  eventType: EventType;
  course: Course;
  classRoom: ClassRoom;
  description: string;
  createdBy: User;
}
