import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { RouterService } from './services/router.service';
import { AuthenticationService } from './services/authentication.service';
import { JwtHelper } from 'angular2-jwt';

@Injectable()
export class CanActivateRouteGuard implements CanActivate {

  constructor(private _authService: AuthenticationService, private _routerService: RouterService) {
    //_authService.validateToken.subscribe(n => this.userAuthenticated = n);
   }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

      console.log('checking');
    // check user is authericated or not
    
    //localStorage.removeItem("bearerToken");
    // let jwtHelper: JwtHelper = new JwtHelper();
    // var token = localStorage.getItem("bearerToken");
    
    // console.log(token);
    // if (token){
    //   console.log('token exists');
    //   if(!jwtHelper.isTokenExpired(token))
    //   {
    //     console.log('token not expired');
    //     return true;
    //   }
    //   else
    //   {
    //     console.log('token expired');
    //   }
    // }

    // console.log('removing token');
    // localStorage.removeItem("bearerToken");
    //this.routerService.routeToLogin();

    let userAuthenticated= this._authService.checkToken();
    //console.log('userAuthenticated: '+ userAuthenticated);
    //var token = localStorage.getItem("bearerToken");
    //console.log(token);
    if(userAuthenticated)
    {
      
      //console.log('userAutheticated');
      return true;
    }
    else
    {
      //console.log('User not authenticated'); 
      localStorage.removeItem("bearerToken");
    this._routerService.routeToLogin();
    return false;
    }
  }
}
