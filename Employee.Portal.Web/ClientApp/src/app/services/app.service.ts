import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";


@Injectable({
  providedIn: 'root'
})
export class AppService {
   
  private apiServer = "http://localhost:55777";

  constructor(private httpClient: HttpClient) { }

  signIn(obj): Observable<any> {
    return this.httpClient.post<any>(`${this.apiServer}/api/Authentication/login/`, JSON.stringify(obj));
  }

  signout(): Observable<any> {
    return this.httpClient.delete<any>(`${this.apiServer}/api/Authentication/logout`);
  }

  register(obj): Observable<any> {
    return this.httpClient.post<any>(`${this.apiServer}/api/Authentication/Register`, JSON.stringify(obj));
  }

  getDepartments(): Observable<any> {
    return this.httpClient.get<any>(`${this.apiServer}/api/Department`);
  }


  addDepartment(obj): Observable<any> {
    return this.httpClient.post<any>(`${this.apiServer}/api/Department`,JSON.stringify(obj));
  }


  addEmployee(obj): Observable<any> {
    return this.httpClient.post<any>(`${this.apiServer}/api/Employee`, JSON.stringify(obj));
  }

  getEmployees(): Observable<any> {
    return this.httpClient.get<any>(`${this.apiServer}/api/Employee`);
  }
}
