import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AdminService } from './admin.service';



const APIUrlAuth = "https://localhost:7177/api/";


@Injectable({
  providedIn: 'root'
})
export class UserService extends DataService {

  constructor(http:HttpClient, private httpPrivate: HttpClient) {
    super(APIUrlAuth, http)
  }

  logIn(data: {username:string, password:string}):Observable<any>{
        return this.httpPrivate.post(APIUrlAuth + 'login', data)
  }

  getLeaveDays(id:string):Observable<any>{
    return this.httpPrivate.get<any>(APIUrlAuth + "user/leavedays/" + id)
  }

  getActiveRequests(id:string):Observable<any>{
    return this.httpPrivate.get<any>(APIUrlAuth + "user/request/" + id)
  }

  deleteRequest(id:number):Observable<any>{
    return this.httpPrivate.delete<any>(APIUrlAuth + "request/" + id)
  }
}
