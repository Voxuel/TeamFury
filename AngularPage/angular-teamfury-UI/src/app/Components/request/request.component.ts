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
  daysLeft: '',
  leaveType: ''
}
requests:Request[] = [];
request:any = {
  startDate:'',
  endDate:'',
  requestTypeID:'',
}
  constructor(private authService:AuthService, private userService:UserService, private router:Router){
  }

  ngOnInit(){
    this.userId = this.authService.getUserId();
    this.getActiveRequests();
    this.getRequestTypes();
  }

  getActiveRequests(){
    this.userService.getActiveRequests(this.userId).subscribe(response => {this.requests = response.result; console.log(this.requests)})
  }

  getRequestTypes(){
    this.userService.getRequestTypes().subscribe(response => {this.requestTypes = response.result})
  }

  onSubmit(){
    this.userService.addRequest(this.userId, this.request).subscribe()
    this.router.navigate(["/User"]);
    console.log(this.request)
  }
}
