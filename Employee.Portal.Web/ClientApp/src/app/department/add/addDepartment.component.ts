import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../core/authentication/authentication.service';
import { AppService } from '../../services/app.service';

@Component({
  templateUrl: './addDepartment.component.html',
})
export class AddDepartmentComponent {
  name;
  address;

  constructor(private appService: AppService, private authService: AuthService, private router: Router) {
  }

  save() {
    let department = {
      name: this.name,
      address : this.address
    };
    if (!department.name) {
      alert("Please enter all required fields !!");
      return;
    }
    this.appService.addDepartment(department).subscribe(() => {
      this.router.navigate(['/department']);
    });
  }

}
