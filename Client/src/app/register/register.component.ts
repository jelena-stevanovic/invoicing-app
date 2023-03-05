import {Component, OnInit} from '@angular/core';
import {RegisterRequest} from "./register-request";
import {AccountService} from "../auth/account.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerRequest: RegisterRequest = {
    email: "",
    password: "",
    confirmPassword: "",
    firstName: "",
    lastName: "",
    phoneNumber: ""
  };
  isSuccessful = false;
  isRegisterFailed = false;
  errorMessage = '';

  constructor(private userService: AccountService) {
  }

  ngOnInit(): void {
  }

  onSubmit(): void {
    console.log(JSON.stringify(this.registerRequest));

    this.userService.signup(this.registerRequest).subscribe({
      next: data => {
        console.log(data);
        this.isSuccessful = true;
        this.isRegisterFailed = false;
      },
      error: err => {
        this.errorMessage = err.error?.message ?? "Error while registering";
        this.isRegisterFailed = true;
      }
    });
  }

}
