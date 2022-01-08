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
      this.data = data;
    })
  }

  delete(id) {
    if (confirm(`Are you sure you want remove this employee ?`)) {
      this.appService.deleteEmployee(id).subscribe((result) => {
        if (result) {
          this.getEmployees();
          alert("Successfully Deleted."); 
        }
        else {
          alert("Error occcured while saving , please try again!!");
        }
      });
    }
  }
   

}
