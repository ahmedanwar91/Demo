import { Component, OnInit } from '@angular/core';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { User } from '../../_models/user';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
users:User[];
  constructor(public userservice:UserService,private alertify:AlertifyService,private route:ActivatedRoute) { }

  ngOnInit() {
    //this.loadusers();
    this.route.data.subscribe(
      data=>{this.users=data['users']}
    )
  }
  /*loadusers(){
    this.userservice.getUsers().subscribe((users:User[])=>
       {this.users=users},
      error=>{this.alertify.error(error)} 
      
 
    )
   }*/
}
