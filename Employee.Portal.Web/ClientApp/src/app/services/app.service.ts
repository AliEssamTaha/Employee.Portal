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

  register(obj): Observable<any> {
    return this.httpClient.post<any>(`${this.apiServer}/api/Authentication/Register`, JSON.stringify(obj));
  }

  addEmployee(obj): Observable<any> {
    return this.httpClient.post<any>(`${this.apiServer}/api/Employee`, JSON.stringify(obj));
  }

  updateEmployee(obj): Observable<any> {
    return this.httpClient.put<any>(`${this.apiServer}/api/Employee`, JSON.stringify(obj));
  }

  deleteEmployee(id): Observable<any> {
    return this.httpClient.delete<any>(`${this.apiServer}/api/Employee/${id}`);
  }

  getEmployeesById(id): Observable<any> {
    return this.httpClient.get<any>(`${this.apiServer}/api/Employee/${id}`);
  }

  getEmployees(): Observable<any> {
    return this.httpClient.get<any>(`${this.apiServer}/api/Employee`);
  }
}
