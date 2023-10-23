import { Component } from '@angular/core';
import { FormBuilder, FormGroup, AbstractControl, FormGroupDirective, Validators } from '@angular/forms';
import { AdminService } from 'src/app/Services/admin.service';
import { AuthService } from 'src/app/Services/auth.service';
import { UserService } from 'src/app/Services/user.service';
import { Employee } from 'src/app/models/employee';
import { LeaveDaysTotal } from 'src/app/models/leaveDaysTotal';
import { UserViewModel } from 'src/app/models/user.view.model';
import { saveAs } from 'file-saver';
import { RequestType } from 'src/app/models/requestType';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {

leavedays:LeaveDaysTotal[] = []
requestType:RequestType = {
  name:'',
  maxDays:''
}
rtForm:FormGroup

constructor(private adminService:AdminService, builder:FormBuilder, private _snackBar:MatSnackBar){
  this.rtForm = builder.group({
    name:'',
    maxDays:''
  })
}

  ngOnInit():void{
    this.getTotalLeavedays();
  }


  getTotalLeavedays(){
    this.adminService.getTotalUsedLeavedays().subscribe(response => {this.leavedays = response})
  }

  submitRequestTypeForm(){
    if(this.rtForm.invalid){
      return;
    }
    this.adminService.createType(this.requestType).subscribe()
    this._snackBar.open("New Request-Type was created", '✔️')
  }


  downloadRapport(){
    const ele = document.getElementById('table-data')!;
    let blob = new Blob([ele.innerText], {
      type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
    });
    saveAs(blob, "rapport-data.csv");
  }



}
