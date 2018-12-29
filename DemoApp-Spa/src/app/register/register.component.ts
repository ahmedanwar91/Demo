import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  
  @Output() cancelRegister=new EventEmitter();
  model:any={};
  constructor(private authservice:AuthService) { }

  ngOnInit() {
  }

  register(){
    this.authservice.register(this.model).subscribe(
      ()=>{console.log('sucess regsiter')},
      error=>{console.log('faild regsiter')}
 
    )
   
     
  }

  cancel(){
    console.log("sucess cancel");
    this.cancelRegister.emit(false);
  }

}
