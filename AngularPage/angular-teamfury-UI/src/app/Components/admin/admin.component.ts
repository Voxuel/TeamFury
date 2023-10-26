import { Component } from '@angular/core';
import { FormBuilder, FormGroup, AbstractControl, FormGroupDirective, Validators } from '@angular/forms';
import { AdminService } from 'src/app/Services/admin.service';
import { AuthService } from 'src/app/Services/auth.service';
import { UserService } from 'src/app/Services/user.service';
import { Employee } from 'src/app/models/employee';
import { LeaveDaysByUserList, LeaveDaysTotal } from 'src/app/models/leaveDaysTotal';
import { UserViewModel } from 'src/app/models/user.view.model';
import { saveAs } from 'file-saver';
import { Request } from 'src/app/models/request.model';
import { RequestViewModel } from 'src/app/models/requestViewModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RequestTypeCreate } from 'src/app/models/requestTypeCreate';
import { RequestTypeBase } from 'src/app/models/requestTypeBase';
import { Router } from '@angular/router';
import { RequestUpdate } from 'src/app/models/requestUpdate';
import { timeout } from 'rxjs';
import { RequestWithUser } from 'src/app/models/requestWithUser';
import { MatDialog, MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {


  H1Title='LEAVEDAY TYPES';
  formTitle='ADD NEW LEAVE TYPE';

leavedays:RequestTypeBase[] = []
allRequests:RequestWithUser[] = []
allPending:RequestWithUser[] = []
requestsWithUsers:RequestWithUser[] = []
form:FormGroup
leaveType:RequestTypeCreate ={
  name:'',
  maxDays:''
}
req:RequestUpdate = {
  requestID: '',
  messageForDecline: '',
  adminName: '',
  statusRequest: 0
}
days:number = 0
reqIncoming:RequestWithUser = {
  request: {
    requestID: '',
    startDate: '',
    endDate: '',
    requestSent: '',
    messageForDecline: '',
    requestType: {
      requestTypeID: '',
      name: '',
      maxdays: '',
    },
    statusRequest: '',
    adminName: '',
  },
  userId:'',
  userName:'',
  DaysLeft:{
    requestTypeId:'',
    daysLeft:0,
    leaveType:''
  }
}
daysByUser:LeaveDaysByUserList[] = []



constructor(private adminService:AdminService, private builder:FormBuilder, private _snackBar:MatSnackBar,
  private router:Router,private authService:AuthService, private userService:UserService){
  this.form = builder.group({
    name:'',
    maxdays:''
  })
}


  ngOnInit():void{
    this.getTotalLeavedays();
    this.getAllRequestsWithUser();
    
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

  getTotalLeavedays(){
    this.adminService.getTotalUsedLeavedays().subscribe(response => {this.leavedays = response})
  }
  getAllRequestsWithUser(){
    this.adminService.getAllRequestsWithuser().subscribe(response => {
      response.forEach(element => {
        if(element.request.statusRequest == '0'){
          this.allPending.push(element)
        }
      });
      this.allRequests = response
    })
  }

  getSelectedDaysLeft(item:RequestWithUser){
  
  }
  


  setStatus(input:any, reqSubmit:any, msg:string){
    this.req.statusRequest = input
    this.req.messageForDecline = msg
    this.req.requestID = reqSubmit.request.requestID,
    this.req.adminName = this.authService.getUser()!
    this.UpdateRequest(this.req)
  }
  UpdateRequest(reqUpdate:RequestUpdate){
    this.adminService.adminUpdateRequest(reqUpdate).subscribe()
    setTimeout(() => {
      location.reload();
    }, 500);
  }



  downloadRapport(){
    const ele = document.getElementById('table-data')!;
    let blob = new Blob([ele.innerText], {
      type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
    });
    saveAs(blob, "rapport-data.csv");
  }

  onSubmit(){
    if(this.form.invalid){
      this._snackBar.open("This form is not filled out correctly", '❌')
      return;
    }
    this.adminService.createRequestType(this.leaveType).subscribe()
    this._snackBar.open('Created')
  }
  updateRt(rtUpdate:RequestTypeBase){
    this.router.navigate(['/detailed', JSON.stringify(rtUpdate)])
  }
}

