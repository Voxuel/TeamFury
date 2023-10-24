import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { SecureInnerPagesGuard } from './Guards/secure-inner-pages.guard';
import { UserComponent } from './Components/user/user.component';
import { authGuard } from './Guards/auth.guard';
import { AdminComponent } from './Components/admin/admin.component';
import { ProfileComponent } from './Components/profile/profile.component';
import { RegisterComponent } from './Components/register/register.component';
import { UserManagerComponent } from './Components/user-manager/user-manager.component';
import { DetailedComponent } from './Components/detailed/detailed.component';
import { RequestComponent } from './Components/request/request.component';
import { RequestlogComponent } from './Components/requestlog/requestlog.component';


const routes: Routes = [
  {path: 'login', component: LoginComponent, canActivate: [SecureInnerPagesGuard]},
  {path: 'User', component: UserComponent, canActivate: [authGuard], data: {role: ['EMPLOYEE']}},
  {path: 'Admin',component: AdminComponent,canActivate: [authGuard], data: {role: ['ADMIN'] }},
  {path: 'Profile', component:ProfileComponent, canActivate: [authGuard], data:{role:['ADMIN', 'EMPLOYEE']}},
  {path: 'Request', component:RequestComponent, canActivate:[authGuard], data:{role:['EMPLOYEE']}},
  {path: 'RequestLog', component:RequestlogComponent, canActivate:[authGuard], data:{role:['EMPLOYEE']}},
  {path: 'register',component: RegisterComponent, canActivate:[authGuard], data:{role:['ADMIN']}},
  {path: 'user-manager', component:UserManagerComponent, canActivate:[authGuard], data:{role:['ADMIN']}},
  {path: 'detailed/:user', component:DetailedComponent, canActivate:[authGuard], data:{role:['ADMIN']}},
  {path: '', redirectTo: 'login', pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
