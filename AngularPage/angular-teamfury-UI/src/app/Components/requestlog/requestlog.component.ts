import { Component } from '@angular/core';
import { AuthService } from 'src/app/Services/auth.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-requestlog',
  templateUrl: './requestlog.component.html',
  styleUrls: ['./requestlog.component.css']
})
export class RequestlogComponent {
  userId:any;

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
  constructor(private authService:AuthService, private userService:UserService){
  }
  ngOnInit(){
    this.userId = this.authService.getUserId();
    this.getRequestLogs();
  }

  getRequestLogs(){
    this.userService.getRequestLog(this.userId).subscribe(response => {this.requests = response.result; })
  }

  getStatusType(statusType:any){
    if(statusType == 0){
      return 'Pending'
    }
    if(statusType == 1){
      return 'Accepted'
    }
    if(statusType == 2){
      return 'Declined'
    }
    return null;
  }
}
