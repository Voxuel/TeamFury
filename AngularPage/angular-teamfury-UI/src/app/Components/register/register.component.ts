import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, FormControlDirective, AbstractControl, Validators } from '@angular/forms';
import { AdminService } from 'src/app/Services/admin.service';
import { DataService } from 'src/app/Services/data.service';
import { Employee } from 'src/app/models/employee';
import { User } from 'src/app/models/user';

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

  constructor(private adminService:AdminService, private builder:FormBuilder){

      this.form = builder.group({
        username: ['', Validators.required],
        password: ['', Validators.required, Validators.minLength(5)],
        email: ['', Validators.required],
        phoneNumber: ['', Validators.required],
        role: ['', Validators.required]
      })
    }

    onSubmit(): void{
      if(this.form.invalid){
        return;
      }
      this.adminService.createUser(this.employee).subscribe();
    }

}
