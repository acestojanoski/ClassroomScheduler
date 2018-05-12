import { Component, OnInit } from '@angular/core';
import { AuthService } from 'services/auth.service';

@Component({
  selector: 'crs-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  public loggedIn = false;
  public user: any;

  constructor(
    private auth: AuthService
  ) { }

  ngOnInit() {
    this.auth.user.subscribe(res => {
      this.user = res;
      this.loggedIn = !!res;
    });
  }

  logout() {
    this.auth.destroySession();
  }
}
