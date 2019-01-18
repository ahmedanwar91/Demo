import { Photo } from "./photo";

export interface User {

     id :number ;
     username:string;
     gender:string;
     age:number;
     knownAs:string;
     created:Date;
     lastActive:Date;
     photoUrl:string;
     city:string;
     country:string;
     introduction?:string;
     lookingFor?:string;
     interests?:string;
     photos?:Photo[];

}
