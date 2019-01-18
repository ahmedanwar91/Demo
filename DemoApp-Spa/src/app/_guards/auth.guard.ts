import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
 

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
   
  constructor(private authservice:AuthService,private router:Router,private alertify:AlertifyService) {
    

  }
  canActivate():  boolean {
    if(this.authservice.loggedIn()){
      return true;
    }else{
      this.alertify.error("يجب عليك تسجيل الدخول اولا");
      this.router.navigate(['']);
      return false;

    }
   
  }
}
