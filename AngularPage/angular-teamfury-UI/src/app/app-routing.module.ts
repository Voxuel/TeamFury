import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { SecureInnerPagesGuard } from './Guards/secure-inner-pages.guard';
import { UserComponent } from './Components/user/user.component';
import { authGuard } from './Guards/auth.guard';
import { AdminComponent } from './Components/admin/admin.component';

const routes: Routes = [
  {path: 'login', component: LoginComponent, canActivate: [SecureInnerPagesGuard]},
  {path: 'User', component: UserComponent, canActivate: [authGuard], data: {role: ['User', 'Admin']}},
  {path: 'Admin',component: AdminComponent,canActivate: [authGuard], data: {role: ['Admin'] }},
  {path: '', redirectTo: 'login', pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
