import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormBuilder, Validators, FormGroupDirective } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
import { RouterService } from '../services/router.service';
import { UserEntity } from '../models/userentity';
import { ToasterCustomService } from '../services/toaster.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  submitMessage: string;

  @ViewChild(FormGroupDirective)
  formGroupDirective: FormGroupDirective;

  username = new FormControl('', [Validators.required, Validators.minLength(5), Validators.pattern('[a-zA-Z0-9_]*')]);
  password = new FormControl('', [Validators.required, Validators.minLength(6)]);

  constructor(private formBuilder: FormBuilder, private authService: AuthenticationService, private routerService: RouterService
    , private toasterService: ToasterCustomService) { }

  ngOnInit(): void {
  }

  // Validations on username
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

  // Validations on password
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

  loginSubmit() {
    //console.log();
    const user = new UserEntity(this.username.value, this.password.value);

    this.authService.authenticateUser(user).subscribe(res => {
      //console.log(res);
      this.authService.setBearerToken(res['token'],user.username);
      this.routerService.routeToDashboard();
    },
      err => {
        //console.log(err);
        this.toasterService.showError(err.error==null ? err.message : err.error);
      }
    );
  }

  registeruser(){
    this.routerService.routeToRegister();
  }
}
