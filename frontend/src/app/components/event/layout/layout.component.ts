import { Component, OnInit } from '@angular/core';

import { AuthService } from 'services/auth.service';

@Component({
  selector: 'crs-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {

  public loggedIn: boolean;

  constructor(private auth: AuthService) { }

  ngOnInit() {
    this.auth.user.subscribe(res => this.loggedIn = !!res);
  }

}
