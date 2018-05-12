import { Building } from 'models/building.model';

export interface ClassRoom {
  id: number;
  name: string;
  building: Building;
}
