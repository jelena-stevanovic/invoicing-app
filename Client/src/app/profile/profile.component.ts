import {Component, OnInit} from '@angular/core';
import {UserResponse} from "../auth/models/user-response";
import {AccountService} from "../auth/account.service";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user: UserResponse = {
    email: '',
    firstName: '',
    lastName: '',
    phoneNumber: ''
  }

  constructor(private userService: AccountService) {
  }

  ngOnInit(): void {
    this.userService.getUserInfo().subscribe(
      {
        next: (data => {
          this.user = data;
        }),
        error: (() => {
          console.log('failed to get the use info');
        })
      }
    );
  }

}
