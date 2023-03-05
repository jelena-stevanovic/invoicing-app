import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import {LoginRequest} from "./models/login-request";
import {LoginResponse} from "./models/login-response";
import {RegisterRequest} from "../register/register-request";
import {UserResponse} from "./models/user-response";

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private httpClient: HttpClient) { }

  login(loginRequest: LoginRequest): Observable<LoginResponse> {
    return this.httpClient.post<LoginResponse>(`${environment.apiUrl}/account/login`, loginRequest);
  }

  signup(registerRequest: RegisterRequest) {
    return this.httpClient.post(`${environment.apiUrl}/account/register`, registerRequest, { responseType: 'text'}); // response type specified, because the API response here is just a plain text (email address) not JSON
  }

  getUserInfo(): Observable<UserResponse> {
    return this.httpClient.get<UserResponse>(`${environment.apiUrl}/account/info`);
  }

}
