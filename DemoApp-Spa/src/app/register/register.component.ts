import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  
  @Output() cancelRegister=new EventEmitter();
  model:any={};
  constructor(private authservice:AuthService,private alertify:AlertifyService) { }

  ngOnInit() {
  }

  register(){
    this.authservice.register(this.model).subscribe(
      ()=>{this.alertify.success("تم الاشتراك بنجاح")},
      error=>{this.alertify.error(error)}
 
    )
   
     
  }

  cancel(){
    console.log("sucess cancel");
    this.cancelRegister.emit(false);
  }

}
