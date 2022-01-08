import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService, TokenModel } from '../core/authentication/authentication.service';
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

  ngOnInit(){
    if(this.authService.IsLoggedIn()){
      this.router.navigate(['']);
    }
  }

  signin() {
    let signInObj = {
      userName: this.userName,
      password: this.password
    }
    this.appService.signIn(signInObj).subscribe((data : TokenModel) => {
      data.userName = this.userName;
      this.authService.SetToken(data);
      this.router.navigate(['']);
    });
  }

  register() {
    this.router.navigate(['register']);
  }
}
