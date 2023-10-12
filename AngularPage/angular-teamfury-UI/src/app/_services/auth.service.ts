import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';


const AUTH_API = 'https://localhost:7177/api/login';

const httpOptions = {
  headers: new HttpHeaders({'Content-Type': 'application/json'})
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  Login(userName: string, password:string):Observable<any>{
    return this.http.post(AUTH_API + 'signin',{
      userName,
      password
    }, httpOptions);
  }
}
