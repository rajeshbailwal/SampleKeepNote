import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
import { RouterService } from '../services/router.service';
import { Register } from '../models/register';
import { ToasterCustomService } from '../services/toaster.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  errorMessage:string;
  username = new FormControl('', [Validators.required, Validators.minLength(5), Validators.pattern('[a-zA-Z0-9_]*')]);
  password = new FormControl('', [Validators.required, Validators.minLength(6)]);
  constructor(private authservice: AuthenticationService, private routerservice: RouterService, private toasterService: ToasterCustomService) { }

  ngOnInit() {
  }

  clear(){
    this.username.reset();
    this.password.reset();
  }

  loginpage()
  {
    this.routerservice.routeToLogin();
  }

  registerUser(){
    const userDetail = new Register(this.username.value, this.password.value);
    if(this.username.valid && this.password.valid){
      this.authservice.registeruser(userDetail).subscribe(
      response=>{
        this.toasterService.showSuccess("User added successfully");
        this.routerservice.routeToLogin();
        //console.log(response);
      },
      error=>{
        //console.log(err);
        if(error.status==409)
        {
          this.toasterService.showError(error.error==null ? error.message : error.error);
          //this.routerservice.routeToLogin();
        }
        else{
        
        this.toasterService.showError(error.error==null ? error.message : error.error);
      }}
      );
    }
  }


  userNameErrorMessage() {
    const userNameControl = this.username;

    if (userNameControl.touched && userNameControl.hasError('required')) {
      return 'Enter username';
    }

    if (userNameControl.touched && userNameControl.hasError('minlength')) {
      return 'UserName must be min 5 characters';
    }

    return userNameControl.hasError('pattern') ? 'Only alphabets, numbers and _ character is allowed' : '';
  }
  
  passwordErrorMessage() {
  const passwordControl = this.password;

  if (passwordControl.touched && passwordControl.hasError('required')) {
    return 'Enter password';
  }

  if (passwordControl.touched && passwordControl.hasError('minlength')) {
    return 'Password must be min 6 characters';
  }

  return '';
  }
}
