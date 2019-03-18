import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import{map} from 'rxjs/operators';
import{JwtHelperService} from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import {BehaviorSubject} from 'rxjs'
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  JwtHelper=new JwtHelperService()
  decodedtoken:any;
  currentUser:User;
baseurl=environment.aPiUrl+"Auth/";
photoUrl = new BehaviorSubject<string>('../../assets/userlogo.png');
currentPhotoUrl = this.photoUrl.asObservable();
constructor(private http:HttpClient) { }
 
changeMemberPhoto(newPhotoUrl:string){
  this.photoUrl.next(newPhotoUrl);
}
login(model:any){
  return this.http.post(this.baseurl+'login',model).pipe(
    map((response:any)=>{
      const user=response;
      if (user) { 
        localStorage.setItem('token', user.token);
        localStorage.setItem('user',JSON.stringify(user.user))
    this.decodedtoken=this.JwtHelper.decodeToken(user.token);
    this.currentUser = user.user;
    this.changeMemberPhoto(this.currentUser.photoUrl);
    
    }
      
    }))
}

register(model:any){
  return this.http.post(this.baseurl+'register',model);
}
loggedIn(){
  try{
    const token=localStorage.getItem('token');
    return !this.JwtHelper.isTokenExpired(token);
  }
  catch{
    return false;
  }
  
}

}
