import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import jwt_decode from 'jwt-decode';

const TOKEN_KEY = 'TOKEN_KEY';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  constructor(private router: Router) { }
  
  
  public saveToken(token:string):void{
    window.sessionStorage.removeItem(TOKEN_KEY);
    window.sessionStorage.setItem(TOKEN_KEY, token)
  }
  public getToken():string|null{
    return window.sessionStorage.getItem(TOKEN_KEY) !== null ? window.sessionStorage.getItem(TOKEN_KEY) : null;
  }
  public getUser():string|null{
    const jwtToken = this.getToken();
      const decodedToken: any = this.getToken() != null ? jwt_decode(jwtToken as string):null;
      const username = decodedToken != null ? decodedToken?.name : null;
    return username;
  }
  public getUserId():string|null{
    const jwtToken = this.getToken();
      const decodedToken: any = this.getToken() != null ? jwt_decode(jwtToken as string) : null;
      const userId = decodedToken != null ? decodedToken?.sub : null;
  return userId;
  }
  public getUserEmail():string|null{
    const jwtToken = this.getToken();
    const decodedToken:any = this.getToken() != null ? jwt_decode(jwtToken as string) : null;
    const userEmail = decodedToken != null ? decodedToken?.email : null
    return userEmail;
  }
  public getRole(){
    const jwtToken = this.getToken();
      const decodedToken: any = this.getToken() != null ? jwt_decode(jwtToken as string) : null;
      const userRole = decodedToken != null ? decodedToken?.role : null;
    return userRole;
  }
  signOut(): void {
    window.sessionStorage.clear();
    this.router.navigate(['/login'])
    .then(() => {
      window.location.reload();
    });
  }
}
