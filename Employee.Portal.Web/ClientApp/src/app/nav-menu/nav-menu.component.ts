import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../core/authentication/authentication.service';
import { AppService } from '../services/app.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(private authService: AuthService, private appService: AppService, private router: Router) {

  }

  logout() {
    this.authService.RemoveToken();
    this.router.navigate(['login']);
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
