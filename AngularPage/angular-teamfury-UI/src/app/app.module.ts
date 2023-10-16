import { CUSTOM_ELEMENTS_SCHEMA , NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MaterialModule } from './material.module';
import { JwtModule } from '@auth0/angular-jwt';
import { authInterceptorProviders } from './Interceptor/auth.interceptor';
import { DataService } from './Services/data.service';
import { UserService } from './Services/user.service';
import { AuthService } from './Services/auth.service';
import { AdminComponent } from './Components/admin/admin.component';
import { UserComponent } from './Components/user/user.component';
import { LoginComponent } from './Components/login/login.component';
import { MAT_SNACK_BAR_DEFAULT_OPTIONS } from '@angular/material/snack-bar'
import { ErrorsStateMatcher } from './ErrorsStateMatcher';

export function tokenGetter(){
  return sessionStorage.getItem("TOKEN_KEY");
}

@NgModule({
  declarations: [
    AppComponent,
    AdminComponent,
    UserComponent,
    LoginComponent
  ],
  imports: [
    MaterialModule,
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["http://localhost:4200/"],
        disallowedRoutes: [],
      }
    })
  ],
  providers: [
    authInterceptorProviders,
    DataService,
    UserService,
    AuthService,
    {provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: {duration: 3500}},
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
