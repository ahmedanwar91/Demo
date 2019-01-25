import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

 
@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseurl=environment.aPiUrl+"users/";
constructor(private http:HttpClient) { }

getUsers():Observable<User[]>{
 return this.http.get<User[]>(this.baseurl );
}

getUser(id):Observable<User>{
  return this.http.get<User>(this.baseurl+id);
 }

 updateUser(id:number,user:User){
  return this.http.put(this.baseurl+id,user);
 }

}
