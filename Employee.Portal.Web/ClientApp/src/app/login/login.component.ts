import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../core/authentication/authentication.service';
import { AppService } from '../services/app.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent {
  userName: string;
  password: string;

  constructor(private appService: AppService, private authService: AuthService, private router: Router) {

  }

  signin() {
    let signInObj = {
      userName: this.userName,
      password: this.password
    }
    this.appService.signIn(signInObj).subscribe((data) => {
      this.authService.SetToken(data);
      this.router.navigate(['']);
    });
  }

  register() {
    this.router.navigate(['register']);
  }
}
