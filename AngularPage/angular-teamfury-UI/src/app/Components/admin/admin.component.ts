import { Component } from '@angular/core';
import { FormBuilder, FormGroup, AbstractControl, FormGroupDirective, Validators } from '@angular/forms';
import { AdminService } from 'src/app/Services/admin.service';
import { AuthService } from 'src/app/Services/auth.service';
import { UserService } from 'src/app/Services/user.service';
import { Employee } from 'src/app/models/employee';
import { LeaveDaysTotal } from 'src/app/models/leaveDaysTotal';
import { UserViewModel } from 'src/app/models/user.view.model';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {

leavedays:LeaveDaysTotal[] = []

constructor(private adminService:AdminService){}

  ngOnInit():void{
    this.getTotalLeavedays();
  }


  getTotalLeavedays(){
    this.adminService.getTotalUsedLeavedays().subscribe(response => {this.leavedays = response})
  }

  downloadRapport(){
    const ele = document.getElementById('table-data')!;
    let blob = new Blob([ele.innerText], {
      type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
    });
    saveAs(blob, "rapport-data.csv");
  }


}
