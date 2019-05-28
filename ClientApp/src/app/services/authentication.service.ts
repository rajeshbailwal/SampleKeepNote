import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { JwtHelper } from 'angular2-jwt';


@Injectable()
export class AuthenticationService {

  myAppUrl: string = "http://localhost:5003/";

  logout: BehaviorSubject<boolean>;

  constructor(private _httpClient: HttpClient) {
    this.logout = new BehaviorSubject(false);
   }

  get validateToken()
  {
    return this.logout.asObservable();
  }

  checkToken()
    {
      let jwtHelper: JwtHelper = new JwtHelper();
      var token = localStorage.getItem("bearerToken");
      
      console.log(token);
      if (token){
        console.log('token exists');
        if(!jwtHelper.isTokenExpired(token))
        {
          console.log('token not expired');
          this.logout.next(true);
          return true;
        }
        else
        {
          this.logout.next(false);
          console.log('token expired');
        }
      }
      
      console.log('logout: ' + this.logout);
      return false;
    }
    

  registeruser(data) {
    // to check the user authentication
    return this._httpClient.post(`${this.myAppUrl}auth/register` , data);
  }
  authenticateUser(data) {
    return this._httpClient.post(this.myAppUrl + 'auth/Login', data);
  }

  setBearerToken(token,name) {
    localStorage.setItem('bearerToken', token);
    localStorage.setItem('username', name);
    this.logout.next(true);
  }

  getBearerToken() {
    //localStorage.clear();
    return localStorage.getItem('bearerToken');
    //localStorage.clear();
  }

  clearBearerToken() {
    //localStorage.clear();
    this.logout.next(false);
    localStorage.removeItem('bearerToken');
    
    return true;
    //localStorage.clear();
  }

  getUserName(){
    return localStorage.getItem('username');
  }

  isUserAuthenticated(token): Promise<boolean> {
    const authUrl = 'http://localhost:3000/auth/v1/isAuthenticated';

    return this._httpClient.post(authUrl, token, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${token}`)
    }).pipe(
      map(res => res['isAuthenticated'])
    ).toPromise();
  }
}