import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../core/authentication/authentication.service';
import { AppService } from '../services/app.service';

@Component({
  templateUrl: './register.component.html',
})
export class RegisterComponent {
  email;
  username;
  password;
  confirmPassword;

  constructor(private appService: AppService, private authService: AuthService, private router: Router) {
  }

  save() {
    debugger
    let user = {
      email: this.email,
      username: this.username,
      password: this.password,
      confirmPassword: this.confirmPassword
    };
    if (!user.email || !user.username || !user.password || !user.confirmPassword ) {
      alert("Please enter all required fields !!");
      return;
    }

    this.appService.register(user).subscribe(() => {
      this.router.navigate(['/login']);
    });
  }

}
