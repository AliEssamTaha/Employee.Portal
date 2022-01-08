import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AppService } from './services/app.service';
import { HttpConfigInterceptor } from './core/interceptor/interceptor';
import { AuthService } from './core/authentication/authentication.service';
import { AuthGuardService } from './core/guard/authGuard.service';
import { EmployeeComponent } from './employee/viewAll/employee.component';
import { AddEmployeeComponent } from './employee/add/addEmployee.component';
import { RegisterComponent } from './register/register.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    EmployeeComponent,
    AddEmployeeComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuardService] },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'employee', component: EmployeeComponent, canActivate: [AuthGuardService] },
      { path: 'addEmployee', component: AddEmployeeComponent, canActivate: [AuthGuardService] },
      { path: 'editEmployee/:id', component: AddEmployeeComponent, canActivate: [AuthGuardService] }
    ])
  ],
  providers: [
    AppService,
    AuthService,
    AuthGuardService,
    { provide: HTTP_INTERCEPTORS, useClass: HttpConfigInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
