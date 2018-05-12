import { EventType } from 'models/event-type.model';
import { Course } from 'models/course.model';
import { ClassRoom } from 'models/class-room.model';

export interface Event {
  id: number;
  description: string;
  startTime: any;
  endTime: any;
  repeat: boolean;
  repeatTimes: number;
  repeatInterval: string;
  eventType: EventType;
  course: Course;
  classRoom: ClassRoom;
}
