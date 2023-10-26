import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AdminService } from './admin.service';
import { Request } from '../models/request.model'


const APIUrlAuth = "https://teamfury.azurewebsites.net/api/";


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

  getRequestTypes():Observable<any>{
    return this.httpPrivate.get<any>(APIUrlAuth + "requesttype")
  }

  getActiveRequests(id:string):Observable<any>{
    const requests = this.httpPrivate.get<any>(APIUrlAuth + "user/request/" + id)
    return requests;
  }

  deleteRequest(id:number):Observable<any>{
    return this.httpPrivate.delete<any>(APIUrlAuth + "request/" + id)
  }

  addRequest(id:string, request:Request):Observable<any>{
    return this.httpPrivate.post<any>(APIUrlAuth + "user/request/" + id, request)
  }

  getRequestLog(id:string):Observable<any>{
    return this.httpPrivate.get<any>(APIUrlAuth + "user/request/log/" + id)
  }

  getActiveUser(id:string):Observable<any>{
    return this.httpPrivate.get<any>(APIUrlAuth + "shared/employee/" + id)
  }
}
