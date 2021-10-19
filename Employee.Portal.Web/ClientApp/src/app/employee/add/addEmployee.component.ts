import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../core/authentication/authentication.service';
import { AppService } from '../../services/app.service';

@Component({
  templateUrl: './addEmployee.component.html',
})
export class AddEmployeeComponent {
  departments = [];
  firstName;
  lastName;
  phone;
  address;
  birthDate;
  departmentId = '';

  constructor(private appService: AppService, private authService: AuthService, private router: Router) {
    this.getDepartments();
  }

  getDepartments() {
    this.appService.getDepartments().subscribe((data) => {
      debugger
      this.departments = data;
    })
  }

  save() {
    debugger
    let employee = {
      firstName: this.firstName,
      lastName: this.lastName,
      phone: this.phone,
      address: this.address,
      birthDate:this.birthDate,
      departmentId:this.departmentId
    };
    if (!employee.firstName || !employee.phone || !employee.birthDate || !employee.departmentId ) {
      alert("Please enter all required fields !!");
      return;
    }
    this.appService.addEmployee(employee).subscribe(() => {
      this.router.navigate(['/employee']);
    });
  }

}
