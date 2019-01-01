import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import{map} from 'rxjs/operators';
import{JwtHelperService} from '@auth0/angular-jwt';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  JwtHelper=new JwtHelperService()
  decodedtoken:any;
baseurl="http://localhost:5000/api/Auth/";
constructor(private http:HttpClient) { }

login(model:any){
  return this.http.post(this.baseurl+'login',model).pipe(
    map((response:any)=>{
      const user=response;
      if(user){localStorage.setItem('token',user.token);
      this.decodedtoken=this.JwtHelper.decodeToken(user.token);
      console.log(this.decodedtoken);
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
