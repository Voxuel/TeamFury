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

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {

  H1Title='LEAVEDAYS';
  formTitle='Add new leave type';

leavedays:LeaveDaysTotal[] = []
allRequests:RequestViewModel[] = []
form:FormGroup
leaveType:any ={
  name:'',
  maxdays:''
}

constructor(private adminService:AdminService, private builder:FormBuilder){
  this.form = builder.group({
    name:'',
    maxdays:''
  })
}

  ngOnInit():void{
    this.getTotalLeavedays();
    this.getRequests();
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
    
  }

}
