import { Component } from '@angular/core';
import { AdminService } from 'src/app/Services/admin.service';
import { UserViewModel } from 'src/app/models/user.view.model';
import { ActivatedRoute, Router, Route } from '@angular/router';

@Component({
  selector: 'app-user-manager',
  templateUrl: './user-manager.component.html',
  styleUrls: ['./user-manager.component.css']
})
export class UserManagerComponent {

  users:UserViewModel[] = [];

  constructor(private adminService:AdminService, private router:Router){}

  ngOnInit(){
    this.getAllEmployees();
  }
  getAllEmployees(){
    return this.adminService.getAllUsers().subscribe(response => {this.users = response});
  }
  inspectUser(selectedUser:UserViewModel){
    this.router.navigate(['/detailed', JSON.stringify(selectedUser)])
  }


}
