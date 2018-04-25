import { Building } from './building.model';

export interface ClassRoom {
  id: number;
  name: string;
  building: Building;
}
