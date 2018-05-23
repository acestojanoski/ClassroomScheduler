import { Component, OnInit } from '@angular/core';

import 'rxjs/add/operator/finally';

import { UserType } from 'models/user-type.model';
import { UserTypeService } from 'services/user-type.service';

@Component({
  selector: 'crs-user-type-layout',
  templateUrl: './user-type-layout.component.html',
  styleUrls: ['./user-type-layout.component.scss']
})
export class UserTypeLayoutComponent implements OnInit {

  public userTypes: UserType[] = [];
  public loading = true;

  constructor(private userTypeService: UserTypeService) { }

  ngOnInit() {
    this.getUserTypes();
  }

  public getUserTypes() {
<<<<<<< HEAD
    this.userTypeService.getUserTypes().subscribe(res => {
      this.userTypes = res;
      this.loading = false;
    }, err => console.error(err));
=======
    this.userTypeService.getUserTypes()
      .finally(() => this.loading = false)
      .subscribe(res => this.userTypes = res, err => console.error(err));
>>>>>>> fdb0abd760e9dca40d5e5de26ebba0fdb6ffd387
  }

}
