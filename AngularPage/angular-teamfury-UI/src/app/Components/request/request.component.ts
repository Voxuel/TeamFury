import { Component } from '@angular/core';
import { UserService } from 'src/app/Services/user.service';
import { AuthService } from 'src/app/Services/auth.service';
import { Request } from 'src/app/models/request.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.css']
})
export class RequestComponent {
requestId:any;
userId:any;
requestTypes:any[] = [];
requestType:any = {
  requestTypeID:'',
  name:''
}
requestyTypeID:any;
leaveDays:any[] = [];
leaveDay:any = {
  maxDays: '',
  name: ''
}
requests:Request[] = [];
request:any = {
  startDate:'',
  endDate:'',
  requestTypeID:'',
  messageForDecline:''
}
  constructor(private authService:AuthService, private userService:UserService, private router:Router){
  }

  ngOnInit(){
    this.userId = this.authService.getUserId();
    this.getActiveRequests();
    this.getRequestTypes();
  }

  getActiveRequests(){
    this.userService.getActiveRequests(this.userId).subscribe(response => {this.requests = response.result; })
  }

  getRequestTypes(){
    this.userService.getRequestTypes().subscribe(response => {this.requestTypes = response.result})
  }

  addRequest(){
    this.userService.addRequest(this.userId, this.request).subscribe(response => {this.request = response.result; 
    if(response.result == null){
      alert("You can't apply for leave within these dates.")
      location.reload();
    }})
  }
  
  onSubmit(){
    this.addRequest()
    setTimeout(() => {
      this.router.navigate(["/User"]);
    }, 500);
  }
}
