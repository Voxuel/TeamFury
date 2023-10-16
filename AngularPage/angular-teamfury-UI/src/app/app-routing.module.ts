import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { PageUserComponent } from './page-user/page-user.component';
import { PageAdminComponent } from './page-admin/page-admin.component';
import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'home', component: HomeComponent},
  {path: 'user', component: PageUserComponent},
  {path: 'admin', component: PageAdminComponent},
  {path: 'profile', component: ProfileComponent},
  {path: '', redirectTo: 'login', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
