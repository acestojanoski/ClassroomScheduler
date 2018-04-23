import { Component } from '@angular/core';
import { environment } from '../environments/environment';

@Component({
  selector: 'crs-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public apiUrl: string = environment.apiUrl;
}
