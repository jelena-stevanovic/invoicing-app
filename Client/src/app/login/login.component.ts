import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {LoginRequest} from "../auth/models/login-request";
import {ErrorResponse} from "../auth/models/error-response";
import {AccountService} from "../auth/account.service";
import {TokenService} from "../auth/token.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginRequest: LoginRequest = {
    email: "",
    password: ""
  };

  isLoggedIn = false;
  isLoginFailed = false;
  error: ErrorResponse = { error: '', errorCode: 0 };
  constructor(private userService: AccountService, private tokenService: TokenService, private router: Router) { }

  ngOnInit(): void {
    let isLoggedIn = this.tokenService.isLoggedIn();
    console.log(`isLoggedIn: ${isLoggedIn}`);
    if (isLoggedIn) {
      this.isLoggedIn = true;

      this.router.navigate(['invoices']);
    }
  }

  onSubmit(): void {

    this.userService.login(this.loginRequest).subscribe({
      next: (data => {
        console.debug(`logged in successfully ${data}`);
        this.tokenService.saveSession(data);
        this.isLoggedIn = true;
        this.isLoginFailed = false;
        this.reloadPage();
      }),
      error: ((error: ErrorResponse) => {
        this.error = error;
        this.isLoggedIn = false;
        this.isLoginFailed = true;
      })

    });
  }
  reloadPage(): void {
    window.location.reload();
  }


}
