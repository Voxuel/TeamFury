import { Component } from '@angular/core';
import { FormBuilder, FormGroup, AbstractControl, FormGroupDirective, Validators } from '@angular/forms';
import { AdminService } from 'src/app/Services/admin.service';
import { AuthService } from 'src/app/Services/auth.service';
import { UserService } from 'src/app/Services/user.service';
import { Employee } from 'src/app/models/employee';
import { UserViewModel } from 'src/app/models/user.view.model';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {

  users:UserViewModel[] = [];

  constructor(private adminService:AdminService){}

  ngOnInit(){
    this.getAllEmployees();
  }
  getAllEmployees(){
    return this.adminService.getAllUsers().subscribe(response => {this.users = response});
  }
}
