import { CanActivate, ActivatedRouteSnapshot, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../Services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import jwt_decode from 'jwt-decode';
import { MatSnackBar } from '@angular/material/snack-bar';


@Injectable({
  providedIn: 'root', 
})


export class authGuard implements CanActivate {
  constructor(
    public authService: AuthService,
    public router: Router,
    private _snackBar: MatSnackBar,
    private jwtHelper: JwtHelperService
  ) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):Observable<boolean> | Promise<boolean> | boolean{
    const jwtToken = this.authService.getToken();
    const decodedToken:any = this.authService.getToken() != null ? jwt_decode(jwtToken as string) : null;
    const userRole = decodedToken != null ? decodedToken.Role : null;

    if(!jwtToken || this.jwtHelper.isTokenExpired(jwtToken)){
      if(this.jwtHelper.isTokenExpired(this.authService.getToken())){
        this._snackBar.open(
        'Your session has expired. Please log in again.',
        '❌'
        );
        this.authService.signOut();
        this.router.navigate(['/login'], {
          queryParams: { returnUrl: state.url },
        });
      }
      else{
        if(route.data['role'] && route.data['role'].indexof(userRole) === -1) {
          this._snackBar.open('Access denied! Role Not Granted.', '❌');
          this.router.navigate(['/login'], {
            queryParams: {returnUrl: state.url }
          });
          return false;
        }
        else {
          return true;
        }
      }
    }
    return true;
  }
}
