import { Component, OnInit } from '@angular/core';

import { AuthService } from 'services/auth.service';

@Component({
  selector: 'crs-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  public loggedIn: boolean;

  constructor(private auth: AuthService) {
    auth.validateToken();
  }

  ngOnInit() {
    this.auth.user.subscribe(res => this.loggedIn = !!res);
  }

}

