import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AdminService } from 'src/app/Services/admin.service';
import { UserViewModel } from 'src/app/models/user.view.model';

@Component({
  selector: 'app-detailed',
  templateUrl: './detailed.component.html',
  styleUrls: ['./detailed.component.css']
})
export class DetailedComponent {

  user:UserViewModel = {
    id:'',
    username: '',
    password: '',
    email: '',
    phoneNumber: '',
    role: []
  }

  users:UserViewModel[] = []

  form:FormGroup 

  obj:any;

  constructor(private ar:ActivatedRoute, private adminService:AdminService, private builder:FormBuilder){
    this.obj = JSON.parse(ar.snapshot.params['user']);
    this.form = this.builder.group({
      id: this.user.id,
      username: this.user.username,
      password: this.user.password,
      email: this.user.email,
      phone: this.user.phoneNumber,
      role: this.user.role
    })
  }

  ngOnInit():void{
    this.user = this.obj;
  }

  getAllUsers(){
    this.adminService.getAllUsers().subscribe(response => {this.users = response})
  }

  updateUser(user:UserViewModel){
    this.adminService.updateUser(user).subscribe(response => {this.getAllUsers()})
  }

  onSubmit(){
    this.updateUser(this.user);
  }
}
