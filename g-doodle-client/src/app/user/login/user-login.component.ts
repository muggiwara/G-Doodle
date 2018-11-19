import { Component } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { User } from '../user';
import { UserService } from '../user.service';
import { BsModalRef } from 'ngx-bootstrap';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent {

  userForm: FormGroup;
  valid = false;
  user: User;
  errors: string[];

  constructor(private _userService: UserService,
    public bsModalRef: BsModalRef,
    private fb: FormBuilder) {
    this.userForm = new FormGroup({
      'login': new FormControl('', [Validators.required]),
      'password': new FormControl('', [Validators.required, Validators.minLength(8)])
    });
  }

  onExit() {
    this.bsModalRef.hide();
    this.valid = false;
  }

  onSubmit() {
    this.errors = [];
    this._userService.login(this.userForm.value.login, this.userForm.value.password).subscribe((user: User) => {
      this.user = user;
      this.bsModalRef.hide();
      this.valid = true;
    }, err => {
      if (err.error) {
        this.errors = [err.error];
      }
    });
  }

}
