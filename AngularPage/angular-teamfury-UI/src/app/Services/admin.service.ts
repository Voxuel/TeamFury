import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Employee } from '../models/employee';
import { Observable } from 'rxjs';
import { UserViewModel } from '../models/user.view.model';
import { ApiResponse } from '../models/api-response';
import { map } from 'rxjs';
import { User } from '../models/user';
import { LeaveDaysTotal } from '../models/leaveDaysTotal';
import { RequestViewModel } from '../models/requestViewModel';
import { RequestTypeCreate } from '../models/requestTypeCreate';
import { RequestTypeBase } from '../models/requestTypeBase';
import { RequestUpdate } from '../models/requestUpdate';


const APIUrlAuth = "https://localhost:7177/api/"

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  employee:Employee = {
    username: "",
    password: "",
    email: "",
    phoneNumber: "",
    role: ""
  };


  constructor(private http:HttpClient) { }

  // User services as admin.

  createUser(employee:Employee):Observable<Employee>{
    return this.http.post<Employee>(`${APIUrlAuth}admin/employee`, employee)
  }

  getAllUsers():Observable<UserViewModel[]>{
    return this.http.get<ApiResponse>(`${APIUrlAuth}admin/employee/view`)
    .pipe(map((data) => data.result))
  }
  getUserById(id:string):Observable<any>{
    return this.http.get<ApiResponse>(`${APIUrlAuth}shared/employee/${id}`)
    .pipe(map((data) => data.result))
  }

  updateUser(employee:UserViewModel):Observable<UserViewModel>{
    return this.http.put<any>(`${APIUrlAuth}admin/employee`,employee)
  }

  deleteUser(id:string):Observable<Employee>{
    return this.http.delete<Employee>(`${APIUrlAuth}admin/employee/${id}`)
  }


  getTotalUsedLeavedays():Observable<RequestTypeBase[]>{
    return this.http.get<ApiResponse>(`${APIUrlAuth}admin/leavedays/totalused`)
    .pipe(map((data) => data.result))
  }

  createRequestType(rtCreate:RequestTypeCreate):Observable<RequestTypeCreate>{
    return this.http.post<RequestTypeCreate>(`${APIUrlAuth}admin/type`, rtCreate)
  }
  deleteRequestType(id:string):Observable<RequestTypeBase>{
    return this.http.delete<any>(`${APIUrlAuth}admin/type/${id}`)
  }
  updateRequestType(rtUpdate:RequestTypeBase):Observable<RequestTypeBase>{
    return this.http.put<any>(`${APIUrlAuth}admin/type/${rtUpdate.requestTypeId}`, rtUpdate)
  }

  // Request services as admin.

  getAllRequests():Observable<RequestViewModel[]>{
    return this.http.get<ApiResponse>(`${APIUrlAuth}admin/request`)
    .pipe(map((data) => data.result))
  }
  adminUpdateRequest(reqUpdate:RequestUpdate):Observable<RequestUpdate>{
    return this.http.put<RequestUpdate>(`${APIUrlAuth}admin/request`, reqUpdate)
  }
}
