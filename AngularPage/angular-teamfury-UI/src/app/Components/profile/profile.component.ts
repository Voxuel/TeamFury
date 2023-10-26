import { Component } from '@angular/core';
import { AuthService } from 'src/app/Services/auth.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {

  userName:any;
  userId:any;
  role:any;
  leaveDays:any[] = [];
  leaveDay:any = {
    daysLeft: '',
    leaveType: ''
  }
  requests:any[] = [];
  request:any = {
    requestId:'',
    startDate:'',
    endDate:'',
    requestSent:'',
    messageForDecline:'',
    requestType: {
      name:''
    },
    statusRequest:'',
    adminName:''
  }
  employee:any = {
    email:'',
    phoneNumber:''
  }

  constructor(private authService:AuthService, private userService:UserService){

  }
  ngOnInit(){
    this.userName = this.authService.getUser();
    this.userId = this.authService.getUserId();
    this.role = this.authService.getRole();
    this.getLeaveDays();
    this.getActiveRequests();
    this.getActiveUser(this.userId);
  }
  getUserName(){
  }

  getLeaveDays(){
    this.userService.getLeaveDays(this.userId).subscribe(response => {this.leaveDays = response.result; })
  }

  getActiveRequests(){
    this.userService.getActiveRequests(this.userId).subscribe(response => {
      if(typeof(response.result) == 'string'){
        return;
      }
      this.requests = response.result
    })
  }

  filterRequests(leaveDay:any){
    const filtered = this.requests.filter(x => x.requestType.name == leaveDay.leaveType).length
    return filtered;
  }

  getActiveUser(userId:string){
    this.userService.getActiveUser(userId).subscribe(response => {this.employee = response.result;})
  }
}
