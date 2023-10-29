import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { AdminService } from 'src/app/Services/admin.service';
import { AuthService } from 'src/app/Services/auth.service';
import { RequestTypeBase } from 'src/app/models/requestTypeBase';
import { UserViewModel } from 'src/app/models/user.view.model';
import {instanceOfUser} from 'src/app/models/user.view.model'
import { instanceOfRequestType } from 'src/app/models/requestTypeBase';
import { RequestViewModel } from 'src/app/models/requestViewModel';
import { RequestWithUser } from 'src/app/models/requestWithUser';

@Component({
  selector: 'app-detailed',
  templateUrl: './detailed.component.html',
  styleUrls: ['./detailed.component.css']
})
export class DetailedComponent {

  incomingType:string = ''

  user:UserViewModel = {
    id:'',
    username: '',
    password: '',
    email: '',
    phoneNumber: '',
    role: []
  }
  requestType:RequestTypeBase = {
    requestTypeId:'',
    name:'',
    maxDays:''
  }

  users:UserViewModel[] = []
  daysLeftOfTypes:RequestTypeBase[] = [];
  requestIncomming:RequestWithUser = {
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
    userName:''
  }
  form:FormGroup
  rtForm:FormGroup

  obj:any;


  constructor(private ar:ActivatedRoute, private adminService:AdminService, private builder:FormBuilder,
    private authService:AuthService, private _snackBar:MatSnackBar){
    this.obj = JSON.parse(ar.snapshot.params['incomming']);
    this.rtForm = this.builder.group({
      requestTypeID: this.requestType.requestTypeId,
      name: this.requestType.name,
      maxDays: this.requestType.maxDays
    })
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
    if(instanceOfUser(this.obj)){
      this.user = this.obj
      this.incomingType = '0'
      return;
    }
    else if(instanceOfRequestType(this.obj)){
      this.incomingType = '1'
      this.requestType = this.obj
      return;
    }
    this.incomingType = '2'
    this.requestIncomming = this.obj
    this.getDaysLeftForUser()
  }

  getDaysLeftForUser(){
    this.adminService.getDaysLeftForUser(this.requestIncomming.userId).subscribe(response => {this.daysLeftOfTypes = response})
  }

  getAllUsers(){
    this.adminService.getAllUsers().subscribe(response => {this.users = response})
  }

  updateUser(user:UserViewModel){
    this.adminService.updateUser(user).subscribe(response => {this.getAllUsers()})

    this._snackBar.open("User updated", '✔️')
  }
  deleteUser(id:string){
    if(this.authService.getUserId() == this.user.id){
      alert("Can't delete yourself lol")
      return;
    }
    this.adminService.deleteUser(id).subscribe()
    this._snackBar.open("User deleted", '🗑️')
  }

  updateRequestType(rtUpdate:RequestTypeBase){
    this.adminService.updateRequestType(rtUpdate).subscribe()
  }
  deleteRt(id:string){
    this.adminService.deleteRequestType(id).subscribe()
  }

  onSubmit(){
    if(instanceOfUser(this.obj)){
      this.updateUser(this.user);
    }
    else if(instanceOfRequestType(this.obj)){
      this.updateRequestType(this.requestType)
    }
  }
}
