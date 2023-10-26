import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, FormControlDirective, AbstractControl, Validators } from '@angular/forms';
import { AdminService } from 'src/app/Services/admin.service';
import { DataService } from 'src/app/Services/data.service';
import { Employee } from 'src/app/models/employee';
import { User } from 'src/app/models/user';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  form:FormGroup

  employee:Employee = {
    username: "",
    password: "",
    email: "",
    phoneNumber: "",
    role: ""
  };

  succsses = false;
  submitted = false;

  constructor(private adminService:AdminService, builder:FormBuilder, private _snackBar:MatSnackBar){

      this.form = builder.group({
        username: ['', Validators.required],
        password: ['', Validators.required],
        email: ['', Validators.required],
        phoneNumber: ['', Validators.required],
        role: ['', Validators.required]
      })
    }

    onSubmit(): void{
      if(this.form.invalid){
        this._snackBar.open("This form is not filled out correctly", '❌')
        return;
      }
      this.adminService.createUser(this.employee).subscribe();
      this._snackBar.open("New user was created", '✔️')
    }

    get f():{
      [key:string]:AbstractControl
    }{
      return this.form.controls;
    }

}
