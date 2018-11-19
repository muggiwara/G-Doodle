import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { User } from '../user';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent {
  userForm: FormGroup;
  title: string;
  valid = false;
  user: User;
  errors: string[];
  private _isCreate = false;

  constructor(private _userService: UserService,
    public bsModalRef: BsModalRef,
    private fb: FormBuilder) {
    this.userForm = new FormGroup({
      'name': new FormControl('', [Validators.required]),
      'email': new FormControl('', [Validators.required, Validators.email]),
      'passwords': new FormGroup({
        'password': new FormControl('', [Validators.required, Validators.minLength(8)]),
        'confirm': new FormControl('', [Validators.required, Validators.minLength(8)])
      }, [this.confirmPassord])
    });
  }

  private confirmPassord(group: FormGroup) {
    if (group.controls['password'].valid && group.controls['confirm'].valid &&
      group.controls['password'].value === group.controls['confirm'].value) {
      return null;
    }
    return {
      mismatch: true
    };
  }

  init(user: User, isCreate: boolean) {
    if (user) {
      this.user = user;
      this.userForm = new FormGroup({
        'name': new FormControl(user.name, [Validators.required]),
        'email': new FormControl(user.email, [Validators.required, Validators.email]),
        'passwords': new FormGroup({
          'password': new FormControl(user.password, [Validators.required, Validators.minLength(8)]),
          'confirm': new FormControl(user.password, [Validators.required, Validators.minLength(8)])
        }, [this.confirmPassord])
      });
    }
    this._isCreate = isCreate;
    this.title = isCreate ? 'Sig-in' : 'Edit user';
  }

  onExit() {
    this.bsModalRef.hide();
    this.valid = false;
  }

  onSubmit() {
    const usr = new User();
    usr.name = this.userForm.value.name;
    usr.password = this.userForm.value.passwords.password;
    usr.email = this.userForm.value.email;
    this.errors = [];
    if (this._isCreate) {
      this._userService.create(usr).subscribe((user: User) => {
        this.user = user;
        this.bsModalRef.hide();
        this.valid = true;
      }, err => {
        if (err.error) {
          this.errors = err.error;
        }
      });
    } else {
      this._userService.update(usr).subscribe((res: any) => {
        this.bsModalRef.hide();
        this.valid = true;
      }, err => {
        this.errors = ['name or email allready use, please try something else'];
      });
    }

  }
}
