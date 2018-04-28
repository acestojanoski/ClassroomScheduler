import { Component, OnInit } from '@angular/core';
import { AuthService } from 'services/auth.service';

@Component({
  selector: 'crs-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  public loggedIn = false;

  constructor(
    private auth: AuthService
  ) { }

  ngOnInit() {
    this.auth.loggedIn.subscribe(res => this.loggedIn = res);
  }

  logout() {
    this.auth.destroySession();
  }
}
