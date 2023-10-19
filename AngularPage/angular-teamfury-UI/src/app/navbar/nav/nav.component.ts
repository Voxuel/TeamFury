import { Component } from '@angular/core';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  title = "TIMEPORTAL";
  isLoggedIn!:boolean;

  constructor(private authService: AuthService) { }

  ngOnInit():void{
    this.isLoggedIn = !!this.authService.getToken();
  }

  getUserRole(){
    return this.authService.getRole();
  }

  signOut(){
    this.authService.signOut();
  }

  getUserName(){
    return this.authService.getUser();
  }
}
