import { Component } from '@angular/core';
import { UserService } from 'src/app/Services/user.service';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent {
  leaveDays:any[] = [];
  userId:any;
  requestId:any;
  leaveDay:any = {
    daysLeft: '',
    leaveType: ''
  }
  activeRequests:any;
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
  constructor(private userService: UserService, private authService:AuthService){

  }

  public async ngOnInit(){
    this.userId = this.authService.getUserId();
    this.getLeaveDays();
    this.getActiveRequests();
  }


  getLeaveDays(){
    this.userService.getLeaveDays(this.userId).subscribe(response => {this.leaveDays = response.result; console.log(this.leaveDays)})
  }

  getActiveRequests(){
    this.userService.getActiveRequests(this.userId).subscribe(response => {
      if(typeof(response.result) == 'string'){
        return;
      }
      this.requests = response.result
    })
  }
  
  deleteRequest(id:any){
    this.userService.deleteRequest(id).subscribe(response => {this.requestId = response.result.requestId})
    this.requests = this.requests.filter(request => request.requestId != id)
  }

  getStatusType(statusRequest:any){
    if(statusRequest == 0){
      return 'Pending'
    }
    if(statusRequest == 1){
      return 'Accepted'
    }
    if(statusRequest == 2){
      return 'Declined'
    }
    return null;
  }

  sortEndDate(){
    this.requests = this.requests.sort((b, a) => {
      return new Date(b.endDate).getTime() - new Date(a.endDate).getTime()
    })
  }
  sortEndDateUp(){
    this.requests = this.requests.sort((b, a) => {
      return new Date(a.endDate).getTime() - new Date(b.endDate).getTime()
    })
  }
  sortStartDate(){
    this.requests = this.requests.sort((b, a) => {
      return new Date(b.startDate).getTime() - new Date(a.startDate).getTime()
    })
  }
  sortStartDateUp(){
    this.requests = this.requests.sort((b, a) => {
      return new Date(a.startDate).getTime() - new Date(b.startDate).getTime()
    })
  }
  sortRequestDate(){
    this.requests = this.requests.sort((b, a) => {
      return new Date(b.requestSent).getTime() - new Date(a.requestSent).getTime()
    })
  }
  sortRequestDateUp(){
    this.requests = this.requests.sort((b, a) => {
      return new Date(a.requestSent).getTime() - new Date(b.requestSent).getTime()
    })
  }
  sortTypeDown(){
    this.requests.sort((b, a) => b.requestType.name.localeCompare(a.requestType.name))
  }
  sortTypeUp(){
    this.requests.sort((b, a) => a.requestType.name.localeCompare(b.requestType.name))
  }
}