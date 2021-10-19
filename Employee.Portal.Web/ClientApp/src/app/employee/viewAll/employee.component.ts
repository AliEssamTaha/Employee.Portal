import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../core/authentication/authentication.service';
import { AppService } from '../../services/app.service';

@Component({
  templateUrl: './employee.component.html',
})
export class EmployeeComponent {
  data = [];

  constructor(private appService: AppService, private authService: AuthService, private router: Router) {
    this.getEmployees();
  }

  getEmployees() {
    this.appService.getEmployees().subscribe((data) => {
      debugger
      this.data = data;
    })
  }
   

}
