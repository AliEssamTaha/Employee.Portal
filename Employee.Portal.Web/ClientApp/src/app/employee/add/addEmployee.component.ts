import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../core/authentication/authentication.service';
import { AppService } from '../../services/app.service';

@Component({
  templateUrl: './addEmployee.component.html',
})
export class AddEmployeeComponent {
  id: number;
  firstName;
  lastName;
  phone;
  address;
  birthDate;

  constructor(private appService: AppService, private authService: AuthService, private router: Router, private activatedRoute: ActivatedRoute) {
  }

  ngOnInit() {
    debugger
    const id = this.activatedRoute.snapshot.params["id"];
    if (id) {
      this.id = id;
      this.getById(id);
    }
  }

  save() {
    debugger
    let employee = {
      id: null,
      firstName: this.firstName,
      lastName: this.lastName,
      phone: this.phone,
      address: this.address,
      birthDate: this.birthDate
    };
    if (!employee.firstName || !employee.phone || !employee.birthDate) {
      alert("Please enter all required fields !!");
      return;
    }

    if (this.id) {
      employee.id = this.id;
      this.update(employee);
    }
    else {
      this.add(employee);
    }
  }

  add(employee) {

    this.appService.addEmployee(employee).subscribe((result) => {
      if (result) {
        this.router.navigate(['/employee']);
      }
      else {
        alert("Error occcured while saving , please try again!!");
      }
    });
  }

  update(employee) {

    this.appService.updateEmployee(employee).subscribe((result) => {
      if (result) {
        this.router.navigate(['/employee']);
      }
      else {
        alert("Error occcured while saving , please try again!!");
      }
    });
  }


  getById(id) {
    this.appService.getEmployeesById(id).subscribe((result) => {
      if (result) {
        this.firstName = result.firstName;
        this.lastName = result.lastName;
        this.phone = result.phone;
        this.address = result.address;
        this.birthDate = result.birthDate;
      }
      else {
        alert("Error occcured while saving , please try again!!");
      }
    });
  }

}
