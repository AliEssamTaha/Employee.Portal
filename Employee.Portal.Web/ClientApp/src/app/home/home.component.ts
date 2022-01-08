import { Component } from '@angular/core';
import { AuthService } from '../core/authentication/authentication.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor(private auth: AuthService) {

  }
}
