import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../core/authentication/authentication.service';
import { AppService } from '../../services/app.service';

@Component({
  templateUrl: './department.component.html',
})
export class DepartmentComponent {
  departments = [];

  constructor(private appService: AppService, private authService: AuthService, private router: Router) {
    this.getDepartments();
  }

  getDepartments() {
    this.appService.getDepartments().subscribe((data) => {
      debugger
      this.departments = data;
    })
  }
   

}
