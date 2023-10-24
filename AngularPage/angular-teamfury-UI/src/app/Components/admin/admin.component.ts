import { Component } from '@angular/core';
import { FormBuilder, FormGroup, AbstractControl, FormGroupDirective, Validators } from '@angular/forms';
import { AdminService } from 'src/app/Services/admin.service';
import { AuthService } from 'src/app/Services/auth.service';
import { UserService } from 'src/app/Services/user.service';
import { Employee } from 'src/app/models/employee';
import { LeaveDaysTotal } from 'src/app/models/leaveDaysTotal';
import { UserViewModel } from 'src/app/models/user.view.model';
import { saveAs } from 'file-saver';
import { Request } from 'src/app/models/request.model';
import { RequestViewModel } from 'src/app/models/requestViewModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RequestTypeCreate } from 'src/app/models/requestTypeCreate';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {

  H1Title='LEAVEDAYS';
  formTitle='ADD NEW LEAVE TYPE';
  requestHead='ALL REQUESTS'

leavedays:LeaveDaysTotal[] = []
allRequests:RequestViewModel[] = []
form:FormGroup
leaveType:RequestTypeCreate ={
  name:'',
  maxDays:''
}
errors:any[] = []

constructor(private adminService:AdminService, private builder:FormBuilder, private _snackBar:MatSnackBar){
  this.form = builder.group({
    name:'',
    maxdays:''
  })
}

  ngOnInit():void{
    this.getTotalLeavedays();
    this.getRequests();
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
  getRequests(){
    this.adminService.getAllRequests().subscribe(response => {this.allRequests = response})
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

}
