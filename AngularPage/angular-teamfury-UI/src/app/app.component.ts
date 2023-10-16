import { Component } from '@angular/core';
import { AuthService } from './Services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'angular-teamfury-UI';
  isLoggedIn!:boolean;

  constructor(private authService: AuthService) { }

  ngOnInit():void{
    this.isLoggedIn = !!this.authService.getToken();
  }
  getUserName(){
    return this.authService.getUser();
  }
  signOut(){
    this.authService.signOut();
  }
}
