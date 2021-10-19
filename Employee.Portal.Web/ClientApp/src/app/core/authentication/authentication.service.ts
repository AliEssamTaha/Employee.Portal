import { Injectable } from "@angular/core";


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public RemoveToken() {
    localStorage.removeItem("token")
  }

  public SetToken(token: TokenModel) {
    localStorage.setItem("token", JSON.stringify(token))
  }

  public GetToken() : TokenModel {
    return JSON.parse(localStorage.getItem("token"));
  }

  public IsLoggedIn(): boolean {
    return this.GetToken() ? true : false;
  }
}
export interface TokenModel {
  accessToken: string,
  refreshToken: string,
  accessTokenExpirationTime : number
}
