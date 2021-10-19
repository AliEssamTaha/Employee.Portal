import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../authentication/authentication.service';


@Injectable()
export class AuthGuardService implements CanActivate {

  constructor(private router: Router,private authService : AuthService) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    if (!this.authService.IsLoggedIn()) {
      this.router.navigate(['login']);
    }
    return true;
  }

}
