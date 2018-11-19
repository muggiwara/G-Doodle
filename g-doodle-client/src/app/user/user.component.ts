import { Component, OnInit, OnDestroy } from '@angular/core';
import { UserService } from './user.service';
import { User, UserStatus } from './user';
import { SessionStorage, SessionStorageService } from 'ngx-store';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { UserEditComponent } from './edit/user-edit.component';
import { UserLoginComponent } from './login/user-login.component';
import { UserSignalRService } from './user-signal-r.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit, OnDestroy {


  user: User;
  isCollapsed = true;
  isConnected = false;
  modalRef: BsModalRef;
  UserStatus = UserStatus;

  constructor(private _userService: UserService,
    private _modalService: BsModalService,
    private _sessionService: SessionStorageService,
    private _userSignalRService: UserSignalRService) { }

  ngOnInit() {
    this._userSignalRService.start().then(() => {
      this.user = this._sessionService.get('user');
      if (this.user) {
        this._userSignalRService.connect(this.user.id, this.user.status);
        this.isConnected = true;
      }
    }).catch(reason => {
      console.log(reason);
    });
  }

  ngOnDestroy(): void {
    this._userSignalRService.connect(this.user.id, UserStatus.OFFLINE);
    this._userSignalRService.stop();
  }

  logIn() {
    this.modalRef = this._modalService.show(UserLoginComponent,
      { class: 'modal-sm text-center', backdrop: true, ignoreBackdropClick: true });
    this._modalService.onHide.subscribe((reason: string) => {
      const valid = this.modalRef.content.valid;
      if (valid) {
        this.user = this.modalRef.content.user;
        this._userSignalRService.connect(this.user.id, this.user.status);
        this._sessionService.set('user', this.user);
        this.isConnected = true;
      }
      this.modalRef = null;
    });
  }

  logOut() {
    this._sessionService.remove('user');
    this.isConnected = false;
  }

  changeStatus(status: UserStatus) {
    const oldstatus = this.user.status;
    this.user.status = status;
    this._userService.update(this.user).subscribe(res => {
      if (res) {
        this._sessionService.set('user', this.user);
      }
    },
    err => {
      this.user.status = oldstatus;
      this._sessionService.set('user', this.user);
    });
  }

  edit(isCreate = false) {
    this.modalRef = this._modalService.show(UserEditComponent,
      { class: 'modal-sm text-center', backdrop: true, ignoreBackdropClick: true });
    this.modalRef.content.init(isCreate ? null : this.user, isCreate);
    this._modalService.onHide.subscribe((reason: string) => {
      const valid = this.modalRef.content.valid;
      if (valid) {
        this.user = this.modalRef.content.user;
        if (isCreate) {
          this.isConnected = true;
          this._userSignalRService.connect(this.user.id, this.user.status);
        } else {
          this._userSignalRService.update(this.user.id);
        }
      }
      this.modalRef = null;
    });
  }
}
