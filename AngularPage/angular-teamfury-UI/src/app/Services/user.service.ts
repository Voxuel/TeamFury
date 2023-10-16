import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


const APIUrlAuth = "https://localhost:7177/api/login";

@Injectable({
  providedIn: 'root'
})
export class UserService extends DataService {

  constructor(http:HttpClient, private httpPrivate: HttpClient) {
    super(APIUrlAuth, http)
  }

  logIn(data: {username:string, password:string}):Observable<any>{
    console.log(data)
    return this.httpPrivate.post(APIUrlAuth, data)
  }
}
